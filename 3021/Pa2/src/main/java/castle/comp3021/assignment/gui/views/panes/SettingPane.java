package castle.comp3021.assignment.gui.views.panes;

import castle.comp3021.assignment.gui.DurationTimer;
import castle.comp3021.assignment.gui.ViewConfig;
import castle.comp3021.assignment.gui.controllers.AudioManager;
import castle.comp3021.assignment.gui.controllers.SceneManager;
import castle.comp3021.assignment.gui.views.BigButton;
import castle.comp3021.assignment.gui.views.BigVBox;
import castle.comp3021.assignment.gui.views.NumberTextField;
import castle.comp3021.assignment.gui.views.SideMenuVBox;
import castle.comp3021.assignment.protocol.Configuration;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextArea;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.VBox;
import org.jetbrains.annotations.NotNull;

import java.util.Optional;

public class SettingPane extends BasePane {
    @NotNull
    private final Label title = new Label("Jeson Mor <Game Setting>");
    @NotNull
    private final Button saveButton = new BigButton("Save");
    @NotNull
    private final Button returnButton = new BigButton("Return");
    @NotNull
    private final Button isHumanPlayer1Button = new BigButton("Player 1: ");
    @NotNull
    private final Button isHumanPlayer2Button = new BigButton("Player 2: ");
    @NotNull
    private final Button toggleSoundButton = new BigButton("Sound FX: Enabled");

    @NotNull
    private final VBox leftContainer = new SideMenuVBox();

    @NotNull
    private final NumberTextField sizeFiled = new NumberTextField(String.valueOf(globalConfiguration.getSize()));

    @NotNull
    private final BorderPane sizeBox = new BorderPane(null, null, sizeFiled, null, new Label("Board size"));

    @NotNull
    private final NumberTextField durationField = new NumberTextField(String.valueOf(DurationTimer.getDefaultEachRound()));
    @NotNull
    private final BorderPane durationBox = new BorderPane(null, null, durationField, null,
            new Label("Max Duration (s)"));

    @NotNull
    private final NumberTextField numMovesProtectionField =
            new NumberTextField(String.valueOf(globalConfiguration.getNumMovesProtection()));
    @NotNull
    private final BorderPane numMovesProtectionBox = new BorderPane(null, null,
            numMovesProtectionField, null, new Label("Steps of protection"));

    @NotNull
    private final VBox centerContainer = new BigVBox();
    @NotNull
    private final TextArea infoText = new TextArea(ViewConfig.getAboutText());


    public SettingPane() {
        connectComponents();
        styleComponents();
        setCallbacks();
    }

    /**
     * Add components to corresponding containers
     */
    @Override
    void connectComponents() {
        leftContainer.getChildren().addAll(title, sizeBox, numMovesProtectionBox, durationBox,
        isHumanPlayer1Button, isHumanPlayer2Button, toggleSoundButton, saveButton, returnButton);
        setLeft(leftContainer);

        centerContainer.getChildren().add(infoText);
        setCenter(centerContainer);
    }

    @Override
    void styleComponents() {
        infoText.getStyleClass().add("text-area");
        infoText.setEditable(false);
        infoText.setWrapText(true);
        infoText.setPrefHeight(ViewConfig.HEIGHT);
    }

    /**
     * Add handlers to buttons, textFields.
     * Hint:
     *  - Text of {@link SettingPane#isHumanPlayer1Button}, {@link SettingPane#isHumanPlayer2Button},
     *            {@link SettingPane#toggleSoundButton} should be changed accordingly
     *  - You may use:
     *      - {@link Configuration#isFirstPlayerHuman()},
     *      - {@link Configuration#isSecondPlayerHuman()},
     *      - {@link Configuration#setFirstPlayerHuman(boolean)}
     *      - {@link Configuration#isSecondPlayerHuman()},
     *      - {@link AudioManager#setEnabled(boolean)},
     *      - {@link AudioManager#isEnabled()},
     */
    @Override
    void setCallbacks() {
        sizeFiled.textProperty().addListener((o, ov, nv) -> {
            try {
                Optional<String> msg = validate(Integer.parseInt(nv), 1, 30);
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
                Optional<String> msg = validate(3, Integer.parseInt(nv), 30);
                if (msg.isEmpty()) {
                    numMovesProtectionField.textProperty().setValue(nv);
                } else {
                    showError(String.valueOf(msg));
                }
            } catch (NumberFormatException e) {
                showError("Number of protected moves has to be an integer.");
            }
        });
        durationField.textProperty().addListener((o, ov, nv) -> {
            try {
                Optional<String> msg = validate(3, 1, Integer.parseInt(nv));
                if (msg.isEmpty()) {
                    numMovesProtectionField.textProperty().setValue(nv);
                } else {
                    showError(String.valueOf(msg));
                }
            } catch (NumberFormatException e) {
                showError("Duration has to be an integer.");
            }
        });
        isHumanPlayer1Button.setOnAction((e) -> changePlayerType(1, !isHumanPlayer1Button.textProperty().getValue().contains("Human")));
        isHumanPlayer2Button.setOnAction((e) -> changePlayerType(2, !isHumanPlayer2Button.textProperty().getValue().contains("Human")));
        toggleSoundButton.setOnAction((e) -> {
            String ov = toggleSoundButton.textProperty().getValue();
            toggleSoundButton.textProperty().setValue("SoundFX: " + (ov.contains("Enable") ? "Disabled" : "Enabled"));
        });
        saveButton.setOnAction((e) -> returnToMainMenu(true));
        returnButton.setOnAction((e) -> returnToMainMenu(false));
    }

    /**
     * Fill in the default values for all editable fields.
     */
    private void fillValues() {
        sizeFiled.setText("9");
        numMovesProtectionField.setText("1");
        durationField.setText("30");
        isHumanPlayer1Button.textProperty().setValue("Player 1: Computer");
        isHumanPlayer2Button.textProperty().setValue("Player 2: Computer");
        toggleSoundButton.setText("SoundFX: Enabled");
    }

    /**
     * Switches back to the {@link MainMenuPane}.
     *
     * @param writeBack Whether to save the values present in the text fields to their respective classes.
     */
    private void returnToMainMenu(final boolean writeBack) {
        if (writeBack) {
            globalConfiguration.setSize(Integer.parseInt(sizeFiled.textProperty().getValue()));
            globalConfiguration.setNumMovesProtection(Integer.parseInt(numMovesProtectionField.textProperty().getValue()));
            DurationTimer.setDefaultEachRound(Integer.parseInt(durationField.textProperty().getValue()));
            globalConfiguration.setFirstPlayerHuman(isHumanPlayer1Button.textProperty().getValue().contains("Human"));
            globalConfiguration.setSecondPlayerHuman(isHumanPlayer2Button.textProperty().getValue().contains("Human"));
            AudioManager.getInstance().setEnabled(toggleSoundButton.textProperty().getValue().contains("Enabled"));
        }
        SceneManager.getInstance().showPane(MainMenuPane.class);
    }

    /**
     * Validate the text fields
     * The useful msgs are predefined in {@link ViewConfig#MSG_BAD_SIZE_NUM}, etc.
     * @param size number in {@link SettingPane#sizeFiled}
     * @param numProtection number in {@link SettingPane#numMovesProtectionField}
     * @param duration number in {@link SettingPane#durationField}
     * @return If validation failed, {@link Optional} containing the reason message; An empty {@link Optional}
     *      * otherwise.
     */
    public static Optional<String> validate(int size, int numProtection, int duration) {
        if (size < 3) return Optional.of(ViewConfig.MSG_BAD_SIZE_NUM);
        if (size % 2 != 1) return Optional.of(ViewConfig.MSG_ODD_SIZE_NUM);
        if (size > 26) return Optional.of(ViewConfig.MSG_UPPERBOUND_SIZE_NUM);
        if (numProtection < 0) return Optional.of(ViewConfig.MSG_NEG_PROT);
        if (duration < 0) return Optional.of(ViewConfig.MSG_NEG_DURATION);
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
