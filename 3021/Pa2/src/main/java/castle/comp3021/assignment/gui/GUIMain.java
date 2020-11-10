package castle.comp3021.assignment.gui;

import castle.comp3021.assignment.gui.controllers.SceneManager;
import castle.comp3021.assignment.gui.views.panes.MainMenuPane;
import castle.comp3021.assignment.textversion.Main;
import javafx.application.Application;
import javafx.stage.Stage;

import java.util.ArrayList;
import java.util.Arrays;


public class GUIMain extends Application {
    @Override
    public void start(final Stage primaryStage) {
        System.out.println("start");
        SceneManager.getInstance().setStage(primaryStage);
        System.out.println("primaryStage created");
        SceneManager.getInstance().showPane(MainMenuPane.class);
    }

    public static void main(String[] args) {
        if (args.length > 0 && args[0].equals("--text")) {
            final var txtArgs = new ArrayList<>(Arrays.asList(args));
            txtArgs.remove(0);

            final var txtArrayArgs = new String[]{};
            Main.main(txtArgs.toArray(txtArrayArgs));

            System.exit(0);
        } else {
            GUIMain.launch(args);
        }
    }
}
