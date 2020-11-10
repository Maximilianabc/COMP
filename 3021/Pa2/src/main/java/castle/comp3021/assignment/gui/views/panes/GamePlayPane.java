package castle.comp3021.assignment.gui.views.panes;

import castle.comp3021.assignment.gui.DurationTimer;
import castle.comp3021.assignment.gui.FXJesonMor;
import castle.comp3021.assignment.gui.ViewConfig;
import castle.comp3021.assignment.gui.controllers.AudioManager;
import castle.comp3021.assignment.gui.controllers.Renderer;
import castle.comp3021.assignment.gui.controllers.SceneManager;
import castle.comp3021.assignment.gui.views.BigButton;
import castle.comp3021.assignment.gui.views.BigVBox;
import castle.comp3021.assignment.gui.views.GameplayInfoPane;
import castle.comp3021.assignment.gui.views.SideMenuVBox;
import castle.comp3021.assignment.piece.Knight;
import castle.comp3021.assignment.player.ConsolePlayer;
import castle.comp3021.assignment.protocol.*;
import castle.comp3021.assignment.protocol.io.Serializer;
import javafx.beans.property.IntegerProperty;
import javafx.beans.property.SimpleIntegerProperty;
import javafx.geometry.Pos;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.control.*;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.HBox;
import javafx.scene.text.Text;
import org.jetbrains.annotations.NotNull;

import java.io.IOException;
import java.util.Arrays;
import java.util.Optional;
import java.util.Timer;
import java.util.TimerTask;

/**
 * fxJesonMor class implements the main playing function of Jeson Mor
 * The necessary components have been already defined (e.g., topBar, title, buttons).
 * Basic functions:
 *      - Start game and play, update scores
 *      - Restart the game
 *      - Return to main menu
 *      - Elapsed Timer (ticking from 00:00 -> 00:01 -> 00:02 -> ...)
 *          - The format is defined in {@link GameplayInfoPane#formatTime(int)}
 * Requirement:
 *      - The game should be initialized by configuration passed from {@link GamePane}, instead of the default configuration
 *      - The information of the game (including scores, current player name, ect.) is implemented in {@link GameplayInfoPane}
 *      - The center canvas (defined as gamePlayCanvas) should be disabled when current player is computer
 * Bonus:
 *      - A countdown timer (if fxJesonMor is implemented, then elapsed timer can be either kept or removed)
 *      - The format of countdown timer is defined in {@link GameplayInfoPane#countdownFormat(int)}
 *      - If one player runs out of time of each round {@link DurationTimer#getDefaultEachRound()}, then the player loses the game.
 * Hint:
 *      - You may find it useful to synchronize javafx UI-thread using {@link javafx.application.Platform#runLater}
 */

public class GamePlayPane extends BasePane {
    @NotNull
    private final HBox topBar = new HBox(20);
    @NotNull
    private final SideMenuVBox leftContainer = new SideMenuVBox();
    @NotNull
    private final Label title = new Label("Jeson Mor");
    @NotNull
    private final Text parameterText = new Text();
    @NotNull
    private final BigButton returnButton = new BigButton("Return");
    @NotNull
    private final BigButton startButton = new BigButton("Start");
    @NotNull
    private final BigButton restartButton = new BigButton("Restart");
    @NotNull
    private final BigVBox centerContainer = new BigVBox();
    @NotNull
    private final Label historyLabel = new Label("History");

    @NotNull
    private final Text historyFiled = new Text();
    @NotNull
    private final ScrollPane scrollPane = new ScrollPane();

    /**
     * time passed in seconds
     * Hint:
     *      - Bind it to time passed in {@link GameplayInfoPane}
     */
    private final IntegerProperty ticksElapsed = new SimpleIntegerProperty();

    @NotNull
    private final Canvas gamePlayCanvas = new Canvas();

    private GameplayInfoPane infoPane = null;

    /**
     * You can add more necessary variable here.
     * Hint:
     *      - the passed in {@link FXJesonMor}
     *      - other global variable you want to note down.
     */
    private FXJesonMor fxJesonMor = null;
    private Configuration c = null;
    private Move lastMove = null;
    private Piece lastPiece = null;
    private Timer timer = null;
    private int ox;
    private int oy;
    private int numMove = 0;
    private boolean isWon = false;

    public GamePlayPane() {
        connectComponents();
        styleComponents();
        setCallbacks();
    }

    /**
     * Components are added, adjust it by your own choice
     */
    @Override
    void connectComponents() {
        topBar.setAlignment(Pos.BOTTOM_CENTER);
        topBar.getChildren().add(title);
        setTop(topBar);
        leftContainer.getChildren().addAll(parameterText, historyLabel, scrollPane, startButton, restartButton, returnButton);
        setLeft(leftContainer);
        centerContainer.getChildren().add(gamePlayCanvas);
        setCenter(centerContainer);
    }

    /**
     * style of title and scrollPane have been set up, no need to add more
     */
    @Override
    void styleComponents() {
        title.getStyleClass().add("head-size");
        scrollPane.setFitToWidth(true);
        scrollPane.setPrefSize(ViewConfig.WIDTH / 4.0, ViewConfig.HEIGHT / 3.0 );
        scrollPane.setContent(historyFiled);
    }

    /**
     * The listeners are added here.
     */
    @Override
    void setCallbacks() {
        startButton.setOnAction((e) -> startGame());
        restartButton.setOnAction((e) -> onRestartButtonClick());
        returnButton.setOnAction((e) -> doQuitToMenuAction());
        gamePlayCanvas.setOnMouseDragged((e) -> onCanvasDragged(e));
        gamePlayCanvas.setOnMousePressed((e) -> onCanvasPressed(e));
        gamePlayCanvas.setOnMouseReleased((e) -> onCanvasReleased(e));
    }

    /**
     * Set up necessary initialization.
     * Hint:
     *      - Set buttons enable/disable
     *          - Start button: enable
     *          - restart button: disable
     *      - fxJesonMor function can be invoked before {@link GamePlayPane#startGame()} for setting up
     *
     * @param fxJesonMor pass in an instance of {@link FXJesonMor}
     */
    void initializeGame(@NotNull FXJesonMor fxJesonMor) {
        isWon = false;
        this.fxJesonMor = fxJesonMor;
        c = fxJesonMor.getConfiguration();
        startButton.setDisable(false);
        restartButton.setDisable(true);
        infoPane = new GameplayInfoPane(
        fxJesonMor.getPlayer1Score(),
        fxJesonMor.getPlayer2Score(),
        fxJesonMor.getCurPlayerName(),
        ticksElapsed);
        centerContainer.getChildren().add(infoPane);

        Configuration c = fxJesonMor.getConfiguration();
        parameterText.setText(String.format("Parameters:\n\nSize of board: %1$d\nNum of protection moves: %2$d\n"
        + "Player %3$s (%4$s)\nPlayer %5$s (%6$s)\n",
        c.getSize(), c.getNumMovesProtection(), c.getPlayers()[0].getName(),
        c.isFirstPlayerHuman() ? "Human" : "Computer", c.getPlayers()[1].getName(),
        c.isSecondPlayerHuman() ? "Human" : "Computer"));
        c.setAllInitialPieces();

        gamePlayCanvas.setWidth(c.getSize() * ViewConfig.PIECE_SIZE);
        gamePlayCanvas.setHeight(c.getSize() * ViewConfig.PIECE_SIZE);
        Renderer.renderChessBoard(gamePlayCanvas, c.getSize(), c.getCentralPlace());
        Renderer.renderPieces(gamePlayCanvas, c.getInitialBoard());
        historyFiled.textProperty().setValue("");
        disnableCanvas();
    }

    /**
     * enable canvas clickable
     */
    private void enableCanvas(){
        gamePlayCanvas.setDisable(false);
    }

    /**
     * disable canvas clickable
     */
    private void disnableCanvas(){
        gamePlayCanvas.setDisable(true);
    }

    /**
     * After click "start" button, everything will start from here
     * No explicit skeleton is given here.
     * Hint:
     *      - Give a carefully thought to how to activate next round of play
     *      - When a new {@link Move} is acquired, it needs to be check whether fxJesonMor move is valid.
     *          - If is valid, make the move, render the {@link GamePlayPane#gamePlayCanvas}
     *          - If is invalid, abort the move
     *          - Update score, add the move to {@link GamePlayPane#historyFiled}, also record the move
     *          - Move forward to next player
     *      - The player can be either computer or human, when the computer is playing, disable {@link GamePlayPane#gamePlayCanvas}
     *      - You can add a button to enable next move once current move finishes.
     *          - or you can add handler when mouse is released
     *          - or you can take advantage of timer to automatically change player. (Bonus)
     */
    public void startGame() {
        if (isWon) {
            endGame();
            return;
        }

        timer = new Timer();
        timer.scheduleAtFixedRate(new TimerTask() {
            @Override
            public void run()
            {
                ticksElapsed.set(ticksElapsed.getValue() + 1);
            }
        }, 0, 1000);

        fxJesonMor.startCountdown();
        enableCanvas();
        c = fxJesonMor.getConfiguration();
        var player = c.getPlayers()[numMove % c.getPlayers().length];
        fxJesonMor.getCurPlayerName().setValue(player.getName());
        var availableMoves = fxJesonMor.getAvailableMoves(player);
        if (availableMoves.length <= 0) {
            isWon = true;
            if (c.getPlayers()[0].getScore() < c.getPlayers()[1].getScore()) {
                createWinPopup(c.getPlayers()[0].getName());
            } else if (c.getPlayers()[0].getScore() > c.getPlayers()[1].getScore()) {
                createWinPopup(c.getPlayers()[1].getName());
            } else {
                createWinPopup(player.getName());
            }
        } else if (!(player instanceof ConsolePlayer)) {
            disnableCanvas();
            var move = player.nextMove(fxJesonMor, availableMoves);
            var movedPiece = fxJesonMor.getPiece(move.getSource());

            fxJesonMor.movePiece(move);
            lastMove = move;
            lastPiece = movedPiece;
            numMove++;
            fxJesonMor.updateScore(player, movedPiece, move);
            updateHistoryField(move);
            Renderer.renderChessBoard(gamePlayCanvas, c.getSize(), c.getCentralPlace());
            Renderer.renderPieces(gamePlayCanvas, c.getInitialBoard());
            checkWinner();
            enableCanvas();
            startGame();
        }
    }

    /**
     * Restart the game
     * Hint: end the current game and start a new game
     */
    private void onRestartButtonClick() {
        endGame();
        startGame();
    }

    /**
     * Add mouse pressed handler here.
     * Play click.mp3
     * draw a rectangle at clicked board tile to show which tile is selected
     * Hint:
     *      - Highlight the selected board cell using {@link Renderer#drawRectangle(GraphicsContext, double, double)}
     *      - Refer to {@link GamePlayPane#toBoardCoordinate(double)} for help
     * @param event mouse click
     */
    private void onCanvasPressed(MouseEvent event){
        AudioManager.getInstance().playSound(AudioManager.SoundRes.CLICK);
        int x = toBoardCoordinate(event.getX());
        int y = toBoardCoordinate(event.getY());
        Renderer.drawRectangle(gamePlayCanvas.getGraphicsContext2D(), x * ViewConfig.PIECE_SIZE, y * ViewConfig.PIECE_SIZE);
        ox = x;
        oy = y;
    }

    /**
     * When mouse dragging, draw a path
     * Hint:
     *      - When mouse dragging, you can use {@link Renderer#drawOval(GraphicsContext, double, double)} to show the path
     *      - Refer to {@link GamePlayPane#toBoardCoordinate(double)} for help
     * @param event mouse position
     */
    private void onCanvasDragged(MouseEvent event) {
        Renderer.drawOval(gamePlayCanvas.getGraphicsContext2D(), event.getX(), event.getY());
    }

    /**
     * Mouse release handler
     * Hint:
     *      - When mouse released, a {@link Move} is completed, you can either validate and make the move here, or somewhere else.
     *      - Refer to {@link GamePlayPane#toBoardCoordinate(double)} for help
     *      - If the piece has been successfully moved, play place.mp3 here (or somewhere else)
     * @param event mouse release
     */
    private void onCanvasReleased(MouseEvent event) {
        gamePlayCanvas.getGraphicsContext2D().clearRect(0, 0, gamePlayCanvas.getWidth(), gamePlayCanvas.getHeight());
        int x = toBoardCoordinate(event.getX());
        int y = toBoardCoordinate(event.getY());
        Place op = new Place(ox, oy);
        Place p = new Place(x, y);

        Piece pe = fxJesonMor.getPiece(op);
        Piece pn = fxJesonMor.getPiece(p);
        if (pe.getPlayer().equals(fxJesonMor.getCurrentPlayer())) {
            Move[] moves = fxJesonMor.getAvailableMoves(fxJesonMor.getCurrentPlayer());
            Move move = Arrays.stream(moves).filter(m -> m.getSource().equals(op)
            && m.getDestination().equals(p)).findFirst().orElse(null);
            if (move != null) {
                fxJesonMor.movePiece(move);
                AudioManager.getInstance().playSound(AudioManager.SoundRes.PLACE);
                if (pn != null && !pn.getPlayer().equals(fxJesonMor.getCurrentPlayer())) {
                    AudioManager.getInstance().playSound(AudioManager.SoundRes.KILL);
                }
                lastMove = move;
                lastPiece = pe;
                fxJesonMor.updateScore(pe.getPlayer(), pe, move);
                updateHistoryField(move);
                numMove++;
                checkWinner();
            } else {
                showInvalidMoveMsg((pe.getLabel() == 'K' ? "Knight" : "Archer") + " move rule is violated.");
            }
        } else {
            showInvalidMoveMsg("The piece selected is not yours.");
        }

        Renderer.renderChessBoard(gamePlayCanvas, c.getSize(), c.getCentralPlace());
        Renderer.renderPieces(gamePlayCanvas, c.getInitialBoard());
        ox = -1;
        oy = -1;
        startGame();
    }

    /**
     * Creates a popup which tells the winner
     */
    private void createWinPopup(String winnerName){
        isWon = true;
        AudioManager.getInstance().playSound(AudioManager.SoundRes.WIN);
        ButtonType startNew = new ButtonType("Start New Game", ButtonBar.ButtonData.YES);
        ButtonType export = new ButtonType("Export Move Records", ButtonBar.ButtonData.NO);
        ButtonType returnToMain = new ButtonType("Return to Main menu", ButtonBar.ButtonData.OK_DONE);
        Alert a = new Alert(
        Alert.AlertType.CONFIRMATION,
        String.format("%1$s wins!", winnerName),
        startNew, export, returnToMain);
        a.setTitle("Congratulations!");
        a.setHeaderText("Confirmation");

        Optional<ButtonType> result = a.showAndWait();
        if (result.orElse(returnToMain).equals(startNew)) {
            onRestartButtonClick();
        } else if (result.orElse(returnToMain).equals(export)) {
            try {
                Serializer.getInstance().saveToFile(fxJesonMor);
                endGame();
            } catch (IOException e) {
                e.printStackTrace();
            }
        } else {
            doQuitToMenuAction();
        }
    }


    /**
     * check winner, if winner comes out, then play the win.mp3 and popup window.
     * The window has three options:
     *      - Start New Game: the same function as clicking "restart" button
     *      - Export Move Records: Using {@link castle.comp3021.assignment.protocol.io.Serializer} to write game's configuration to file
     *      - Return to Main menu, using {@link GamePlayPane#doQuitToMenuAction()}
     */
    private void checkWinner(){
        if (numMove <= c.getNumMovesProtection()) {
            return;
        }

        if ((lastPiece instanceof Knight) && lastMove.getSource().equals(c.getCentralPlace())
        && !lastMove.getDestination().equals(c.getCentralPlace())) {
            createWinPopup(fxJesonMor.getCurPlayerName().getValue());
        } else {
            Player remainingPlayer = null;
            for (int i = 0; i < c.getSize(); i++) {
                for (int j = 0; j < c.getSize(); j++) {
                    var piece = fxJesonMor.getPiece(i, j);
                    if (piece == null) {
                        continue;
                    }
                    if (remainingPlayer == null) {
                        remainingPlayer = piece.getPlayer();
                    } else if (remainingPlayer != piece.getPlayer()) {
                        return;
                    }
                }
            }
            createWinPopup(remainingPlayer.getName());
        }
    }

    /**
     * Popup a window showing invalid move information
     * @param errorMsg error string stating why fxJesonMor move is invalid
     */
    private void showInvalidMoveMsg(String errorMsg) {
        Alert a = new Alert(Alert.AlertType.ERROR);
        a.setTitle("Invalid Move");
        a.setHeaderText("Your movement is invalid due to following reason(s):");
        a.setContentText(errorMsg);
        a.showAndWait();
    }

    /**
     * Before actually quit to main menu, popup a alert window to double check
     * Hint:
     *      - title: Confirm
     *      - HeaderText: Return to menu?
     *      - ContentText: Game progress will be lost.
     *      - Buttons: CANCEL and OK
     *  If click OK, then refer to {@link GamePlayPane#doQuitToMenu()}
     *  If click Cancle, than do nothing.
     */
    private void doQuitToMenuAction() {
        Alert a = new Alert(Alert.AlertType.CONFIRMATION);
        a.setTitle("Confirm");
        a.setHeaderText("Return to menu?");
        a.setContentText("Game progress will be lost.");
        Optional<ButtonType> result = a.showAndWait();
        if (result.get() == ButtonType.OK) {
            doQuitToMenu();
        }
    }

    /**
     * Update the move to the historyFiled
     * @param move the last move that has been made
     */
    private void updateHistoryField(Move move) {
        String ot = historyFiled.textProperty().getValue();
        historyFiled.textProperty().setValue(ot + String.format(
        "player: %1$s; move: %2$s->%3$s\n",
        fxJesonMor.getCurPlayerName().getValue(),
        move.getSource().toString(),
        move.getDestination().toString()));
    }

    /**
     * Go back to main menu
     * Hint: before quit, you need to end the game
     */
    private void doQuitToMenu() {
        endGame();
        SceneManager.getInstance().showPane(MainMenuPane.class);
    }

    /**
     * Converting a vertical or horizontal coordinate x to the coordinate in board
     * Hint:
     *      The pixel size of every piece is defined in {@link ViewConfig#PIECE_SIZE}
     * @param x coordinate of mouse click
     * @return the coordinate on board
     */
    private int toBoardCoordinate(double x){
        return (int)(x / ViewConfig.PIECE_SIZE);
    }

    /**
     * Handler of ending a game
     * Hint:
     *      - Clear the board, history text field
     *      - Reset buttons
     *      - Reset timer
     *
     */
    private void endGame() {
        gamePlayCanvas.getGraphicsContext2D().clearRect(0, 0, gamePlayCanvas.getWidth(), gamePlayCanvas.getHeight());
        startButton.setDisable(false);
        restartButton.setDisable(true);
        historyFiled.textProperty().setValue("");
        fxJesonMor.stopCountdown();
        timer.cancel();
        ticksElapsed.set(0);
    }
}
