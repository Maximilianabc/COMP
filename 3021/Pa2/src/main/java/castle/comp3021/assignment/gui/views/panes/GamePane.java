package castle.comp3021.assignment.gui.views.panes;

import castle.comp3021.assignment.gui.FXJesonMor;
import castle.comp3021.assignment.gui.ViewConfig;
import castle.comp3021.assignment.gui.controllers.SceneManager;
import castle.comp3021.assignment.gui.views.BigButton;
import castle.comp3021.assignment.gui.views.BigVBox;
import castle.comp3021.assignment.gui.views.NumberTextField;
import castle.comp3021.assignment.protocol.Configuration;
import castle.comp3021.assignment.protocol.Player;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.VBox;
import org.jetbrains.annotations.NotNull;

import java.util.Optional;

public class GamePane extends BasePane {
    @NotNull
    private final VBox container = new BigVBox();
    @NotNull
    private final Label title = new Label("Jeson Mor");
    @NotNull
    private final Button playButton = new BigButton("Play");
    @NotNull
    private final Button returnButton = new BigButton("Return");
    @NotNull
    private final Button useDefaultButton = new BigButton("Use Default");
    @NotNull
    private final Button isHumanPlayer1Button = new BigButton("");
    @NotNull
    private final Button isHumanPlayer2Button = new BigButton("");

    @NotNull
    private final NumberTextField sizeFiled = new NumberTextField("");


    private final BorderPane sizeBox = new BorderPane(null, null, sizeFiled, null, new Label("Size of Board:"));

    @NotNull
    private final NumberTextField numMovesProtectionField = new NumberTextField("");

    @NotNull
    private final BorderPane numMovesProtectionBox = new BorderPane(null, null, numMovesProtectionField, null,
    new Label("Protection Moves:"));


    private FXJesonMor fxJesonMor = null;

    public GamePane() {
        fillValues();
        connectComponents();
        styleComponents();
        setCallbacks();
    }

    @Override
    void connectComponents() {
        container.getChildren().addAll(title, sizeBox, numMovesProtectionBox, isHumanPlayer1Button,
        isHumanPlayer2Button, useDefaultButton, playButton, returnButton);
        setCenter(container);
    }

    @Override
    void styleComponents() {
        title.getStyleClass().add("head-size");
    }

    /**
     * Set callbacks to buttons
     * Hint:
     * -  When fill in board size and step of protections, numbers need to validate
     * -  useDefaultButton: use default value for {@link GamePane#sizeFiled}, {@link GamePane#numMovesProtectionField}, and two players
     *    as they are saved in {@link SettingPane}
     * -  The current configuration (including {@link GamePane#sizeFiled}, {@link GamePane#numMovesProtectionField} and two players role)
     *    should not affect the default settings.
     * -  After clicking "play" button, the handler is implemented in {@link GamePane#startGame(FXJesonMor)},
     *    which links to {@link GamePlayPane} using the current configuration.
     */
    @Override
    void setCallbacks() {
        sizeFiled.textProperty().addListener((o, ov, nv) -> {
            try {
                Optional<String> msg = validate(Integer.parseInt(nv), 1);
                if (msg.isEmpty()) {
                    sizeFiled.textProperty().setValue(nv);
                } else {
                    showError(String.valueOf(msg));
                }
            } catch (NumberFormatException e) {
                showError("Size has to be an integer.");
            }
        });
        numMovesProtectionField.textProperty().addListener((o, ov, nv) -> {
            try {
                Optional<String> msg = validate(3, Integer.parseInt(nv));
                if (msg.isEmpty()) {
                    numMovesProtectionField.textProperty().setValue(nv);
                } else {
                    showError(String.valueOf(msg));
                }
            } catch (NumberFormatException e) {
                showError("Number of protected moves has to be an integer.");
            }
        });
        useDefaultButton.setOnAction((e) -> {
            sizeFiled.textProperty().setValue(Integer.toString(globalConfiguration.getSize()));
            numMovesProtectionField.textProperty().setValue(Integer.toString(globalConfiguration.getNumMovesProtection()));
            changePlayerType(1, globalConfiguration.isFirstPlayerHuman());
            changePlayerType(2, globalConfiguration.isSecondPlayerHuman());
        });
        isHumanPlayer1Button.setOnAction((e) -> changePlayerType(1, !isHumanPlayer1Button.textProperty().getValue().contains("Human")));
        isHumanPlayer2Button.setOnAction((e) -> changePlayerType(2, !isHumanPlayer2Button.textProperty().getValue().contains("Human")));
        playButton.setOnAction((e) -> {
            Configuration config = new Configuration(sizeFiled.getValue(), new Player[2], numMovesProtectionField.getValue());
            config.setFirstPlayerHuman(isHumanPlayer1Button.textProperty().getValue().contains("Human"));
            config.setSecondPlayerHuman(isHumanPlayer2Button.textProperty().getValue().contains("Human"));
            startGame(new FXJesonMor(config));
        });
        returnButton.setOnAction((e) -> SceneManager.getInstance().showPane(MainMenuPane.class));
    }

    /**
     * Handler when clicking "play" button, using the current configuration to pass a {@link FXJesonMor} instance
     * Hint:
     *      - You may need to initialize and set up {@link GamePlayPane} by passing {@link FXJesonMor}
     * @param fxJesonMor an instance of {@link FXJesonMor}
     */
    void startGame(@NotNull FXJesonMor fxJesonMor) {
        final var gameplayPane = SceneManager.getInstance().<GamePlayPane>getPane(GamePlayPane.class);
        this.fxJesonMor = fxJesonMor;
        gameplayPane.initializeGame(fxJesonMor);
        SceneManager.getInstance().showPane(GamePlayPane.class);
    }

    /**
     * Fill in the default values for all editable fields.
     */
    void fillValues(){
        sizeFiled.setText("9");
        numMovesProtectionField.setText("1");
        isHumanPlayer1Button.textProperty().setValue("Player 1: Computer");
        isHumanPlayer2Button.textProperty().setValue("Player 2: Computer");
    }

    /**
     * Validate the text fields
     * The useful msgs are predefined in {@link ViewConfig#MSG_BAD_SIZE_NUM}, etc.
     * @param size number in {@link GamePane#sizeFiled}
     * @param numProtection number in {@link GamePane#numMovesProtectionField}
     * @return If validation failed, {@link Optional} containing the reason message; An empty {@link Optional}
     *      * otherwise.
     */
    public static Optional<String> validate(int size, int numProtection) {
        if (size < 3) return Optional.of(ViewConfig.MSG_BAD_SIZE_NUM);
        if (size % 2 != 1) return Optional.of(ViewConfig.MSG_ODD_SIZE_NUM);
        if (size > 26) return Optional.of(ViewConfig.MSG_UPPERBOUND_SIZE_NUM);
        if (numProtection < 0) return Optional.of(ViewConfig.MSG_NEG_PROT);
        return Optional.empty();
    }

    private void showError(String msg) {
        Alert a = new Alert(Alert.AlertType.ERROR);
        a.setTitle("Error");
        a.setContentText(msg);
        a.showAndWait();
    }
    private void changePlayerType(int playerNum, boolean isHuman){
        if (playerNum == 1){
            isHumanPlayer1Button.textProperty().setValue("Player 1: " + (isHuman ? "Human" : "Computer"));
        } else {
            isHumanPlayer2Button.textProperty().setValue("Player 2: " + (isHuman ? "Human" : "Computer"));
        }
    }
}
