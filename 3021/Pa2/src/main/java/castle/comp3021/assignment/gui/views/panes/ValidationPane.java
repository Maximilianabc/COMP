package castle.comp3021.assignment.gui.views.panes;

import castle.comp3021.assignment.gui.FXJesonMor;
import castle.comp3021.assignment.gui.ViewConfig;
import castle.comp3021.assignment.gui.controllers.Renderer;
import castle.comp3021.assignment.gui.controllers.SceneManager;
import castle.comp3021.assignment.protocol.*;
import castle.comp3021.assignment.protocol.io.Deserializer;
import castle.comp3021.assignment.textversion.JesonMor;
import javafx.beans.property.BooleanProperty;
import javafx.beans.property.SimpleBooleanProperty;
import javafx.concurrent.Task;
import javafx.scene.canvas.Canvas;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.layout.VBox;
import javafx.stage.FileChooser;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import castle.comp3021.assignment.gui.views.BigButton;
import castle.comp3021.assignment.gui.views.BigVBox;

import java.io.File;
import java.io.FileNotFoundException;
import java.nio.file.Path;
import java.util.*;
import java.util.stream.Collectors;


public class ValidationPane extends BasePane{
    @NotNull
    private final VBox leftContainer = new BigVBox();
    @NotNull
    private final BigVBox centerContainer = new BigVBox();
    @NotNull
    private final Label title = new Label("Jeson Mor");
    @NotNull
    private final Label explanation = new Label("Upload and validation the game history.");
    @NotNull
    private final Button loadButton = new BigButton("Load file");
    @NotNull
    private final Button validationButton = new BigButton("Validate");
    @NotNull
    private final Button replayButton = new BigButton("Replay");
    @NotNull
    private final Button returnButton = new BigButton("Return");

    private Canvas gamePlayCanvas = new Canvas();

    /**
     * store the loaded information
     */
    private Configuration loadedConfiguration;
    private Integer[] storedScores;
    private FXJesonMor loadedGame;
    private Place loadedcentralPlace;
    private ArrayList<MoveRecord> loadedMoveRecords = new ArrayList<>();

    private BooleanProperty isValid = new SimpleBooleanProperty(false);


    public ValidationPane() {
        connectComponents();
        styleComponents();
        setCallbacks();
    }

    @Override
    void connectComponents() {
        leftContainer.getChildren().addAll(title, explanation, loadButton, validationButton, replayButton, returnButton);
        setLeft(leftContainer);
        gamePlayCanvas.setWidth(ViewConfig.WIDTH);
        gamePlayCanvas.setHeight(ViewConfig.HEIGHT);
        centerContainer.getChildren().add(gamePlayCanvas);
        setCenter(centerContainer);
    }

    @Override
    void styleComponents() {
        title.getStyleClass().add("head-size");
    }

    /**
     * Add callbacks to each buttons.
     * Initially, replay button is disabled, gamePlayCanvas is empty
     * When validation passed, replay button is enabled.
     */
    @Override
    void setCallbacks() {
        loadButton.setOnAction((e) -> {
            try {
                if (loadFromFile()) {
                    validationButton.setDisable(false);
                }
            } catch (FileNotFoundException fileNotFoundException) {
                fileNotFoundException.printStackTrace();
            }
        });
        validationButton.setDisable(true);
        validationButton.setOnAction((e) -> onClickValidationButton());
        replayButton.setDisable(true);
        replayButton.setOnAction((e) -> onClickReplayButton());
        returnButton.setOnAction((e) -> returnToMainMenu());
    }

    /**
     * load From File and deserializer the game by two steps:
     *      - {@link ValidationPane#getTargetLoadFile}
     *      - {@link Deserializer}
     * Hint:
     *      - Get file from {@link ValidationPane#getTargetLoadFile}
     *      - Instantiate an instance of {@link Deserializer} using the file's path
     *      - Using {@link Deserializer#parseGame()}
     *      - Initialize {@link ValidationPane#loadedConfiguration}, {@link ValidationPane#loadedcentralPlace},
     *                   {@link ValidationPane#loadedGame}, {@link ValidationPane#loadedMoveRecords}
     *                   {@link ValidationPane#storedScores}
     * @return whether the file and information have been loaded successfully.
     */
    private boolean loadFromFile() throws FileNotFoundException
    {
        File target = getTargetLoadFile();
        if (target == null) return false;
        Deserializer d = new Deserializer(Path.of(target.getPath()));
        d.parseGame();
        loadedConfiguration = d.getLoadedConfiguration();
        loadedMoveRecords = d.getMoveRecords();
        loadedcentralPlace = d.getLoadedConfiguration().getCentralPlace();
        storedScores = d.getStoredScores();
        loadedGame = new FXJesonMor(loadedConfiguration);
        return true;
    }

    /**
     * When click validation button, validate the loaded game configuration and move history
     * Hint:
     *      - if nothing loaded, call {@link ValidationPane#showErrorMsg}
     *      - if loaded, check loaded content by calling {@link ValidationPane#validateHistory}
     *      - When the loaded file has passed validation, the "replay" button is enabled.
     */
    private void onClickValidationButton(){
        if (loadedMoveRecords.stream().count() == 0) {
            showErrorMsg();
        } else {
            if (validateHistory()) {
                passValidationWindow();
                replayButton.setDisable(false);
            }
        }
    }

    /**
     * Display the history of recorded move.
     * Hint:
     *      - You can add a "next" button to render each move, or
     *      - Or you can refer to {@link Task} for implementation.
     */
    private void onClickReplayButton() {

        Configuration c = loadedConfiguration;
        FXJesonMor jm = new FXJesonMor(c);
        for (MoveRecord mr : loadedMoveRecords) {
            Renderer.renderChessBoard(gamePlayCanvas, c.getSize(), c.getCentralPlace());
            Renderer.renderPieces(gamePlayCanvas, c.getInitialBoard());
            jm.movePiece(mr.getMove());
        }
    }

    /**
     * Validate the {@link ValidationPane#loadedConfiguration}, {@link ValidationPane#loadedcentralPlace},
     *              {@link ValidationPane#loadedGame}, {@link ValidationPane#loadedMoveRecords}
     *              {@link ValidationPane#storedScores}
     * Hint:
     *      - validate configuration of game
     *      - whether each move is valid
     *      - whether scores are correct
     */
    private boolean validateHistory(){
        Configuration c = loadedConfiguration;
        int size = c.getSize();
        int numProtection = c.getNumMovesProtection();
        if (size < 3) {
            showErrorConfiguration(ViewConfig.MSG_BAD_SIZE_NUM);
            return false;
        }
        if (size % 2 != 1) {
            showErrorConfiguration(ViewConfig.MSG_ODD_SIZE_NUM);
            return false;
        }
        if (size > 26) {
            showErrorConfiguration(ViewConfig.MSG_UPPERBOUND_SIZE_NUM);
            return false;
        }
        if (numProtection < 0) {
            showErrorConfiguration(ViewConfig.MSG_NEG_PROT);
            return false;
        }

        c.setAllInitialPieces();
        FXJesonMor jm = new FXJesonMor(c);
        for (MoveRecord mr : loadedMoveRecords) {
            Move m = mr.getMove();
            Player cur = Arrays.stream(c.getPlayers()).filter(p -> p.equals(mr.getPlayer())).findFirst().get();
            String ruleMsg = cur.validateMove(jm, m);
            if (ruleMsg != null) {
                showErrorConfiguration(String.format("Move %1$s->%2$s of player %3$s violated the rules \"%4$s\"",
                m.getSource().toString(), m.getDestination().toString(), mr.getPlayer().getName(), ruleMsg));
                return false;
            }
            jm.movePiece(m);
            jm.updateScore(cur, jm.getPiece(m.getDestination()), m);
        }

        Player p1 = c.getPlayers()[0];
        if (p1.getScore() != storedScores[0]) {
            showErrorConfiguration(String.format("Unmatched score for %3$s detected, stored: %1$d, found: %2$d",
            storedScores[0], p1.getScore(), p1.getName()));
            return false;
        }

        Player p2 = c.getPlayers()[1];
        if (c.getPlayers()[1].getScore() != storedScores[1]) {
            showErrorConfiguration(String.format("Unmatched score for %3$s detected, stored: %1$d, found: %2$d",
            storedScores[1], p2.getScore(), p2.getName()));
            return false;
        }
        return true;
    }

    /**
     * Popup window show error message
     * Hint:
     *      - title: Invalid configuration or game process!
     *      - HeaderText: Due to following reason(s):
     *      - ContentText: errorMsg
     * @param errorMsg error message
     */
    private void showErrorConfiguration(String errorMsg){
        Alert a = new Alert(Alert.AlertType.ERROR);
        a.setTitle("Invalid configuration or game process!");
        a.setHeaderText("Due to following reason(s):");
        a.setContentText(errorMsg);
        a.showAndWait();
    }

    /**
     * Pop up window to warn no record has been uploaded.
     * Hint:
     *      - title: Error!
     *      - ContentText: You haven't loaded a record, Please load first.
     */
    private void showErrorMsg(){
        Alert a = new Alert(Alert.AlertType.ERROR);
        a.setTitle("Error!");
        a.setContentText("You haven't loaded a record, Please load first.");
        a.showAndWait();
    }

    /**
     * Pop up window to show pass the validation
     * Hint:
     *     - title: Confirm
     *     - HeaderText: Pass validation!
     */
    private void passValidationWindow(){
        Alert a = new Alert(Alert.AlertType.INFORMATION);
        a.setTitle("Confirm");
        a.setHeaderText("Pass validation!");
        a.showAndWait();
    }

    /**
     * Return to Main menu
     * Hint:
     *  - Before return, clear the rendered canvas, and clear stored information
     */
    private void returnToMainMenu(){
        gamePlayCanvas.getGraphicsContext2D().clearRect(0, 0, gamePlayCanvas.getWidth(), gamePlayCanvas.getHeight());
        SceneManager.getInstance().showPane(MainMenuPane.class);
    }


    /**
     * Prompts the user for the file to load.
     * <p>
     * Hint:
     * Use {@link FileChooser} and {@link FileChooser#setSelectedExtensionFilter(FileChooser.ExtensionFilter)}.
     *
     * @return {@link File} to load, or {@code null} if the operation is canceled.
     */
    @Nullable
    private File getTargetLoadFile() {
        FileChooser fc = new FileChooser();
        fc.setTitle("Load History");
        fc.setSelectedExtensionFilter(new FileChooser.ExtensionFilter("Text Files", "*.txt"));
        return fc.showOpenDialog(getScene().getWindow());
    }

}
