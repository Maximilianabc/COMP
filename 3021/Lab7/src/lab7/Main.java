package lab7;

import javafx.application.Application;
import javafx.scene.Scene;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;

public class Main extends Application {
    public static final int CELL_SIZE = 32;

    /**
     * boardSize: size of board, customized.
     */
    private final int boardSize = 9;

    private static Image lightBoardTile = null;
    private static Image darkBoardTile = null;


    static{
        //TODO
        /**
         * load "assets/images/lightBoard.png" and assign it to {@link Main#lightBoardTile}
         * load "assets/images/darkBoard.png" and assign it to {@link Main#darkBoardTile}
         * There are two methods in readme, you can use either one.
         */
        lightBoardTile = new Image("file:src/assets/images/lightboard.png");
        darkBoardTile = new Image("file:src/assets/images/darkboard.png");
    }
    


    public static void main(String[] args) {
        launch(args);
    }

    /**
     * set the primaryStage title to "Lab 7"
     * set the scene as {@link Main#setboardScene(int)}
     * show the stage
     *
     * @param primaryStage
     */
    @Override
    public void start(Stage primaryStage) {
        //TODO
        primaryStage.setTitle("Lab 7");
        primaryStage.setScene(setboardScene(9));
        primaryStage.show();
    }


    /**
     * Generate the canvas with the height and width both are {@link Main#boardSize} * {@link Main#CELL_SIZE}
     * Draw {@link Main#lightBoardTile} and {@link Main#darkBoardTile} take turns
     *  - odd: lightBoardTile
     *  - even: darkBoardTile
     * Place the canvas inside a VBox before returning it in a scene
     * Similar implementation logic can be used in PA2 gui/controllers/Render# renderChessBoard method.
     *
     * @param boardSize Size of board
     * @return The created scene
     */
    public Scene setboardScene(int boardSize) {
        //TODO
        Canvas canvas = new Canvas(boardSize * CELL_SIZE, boardSize * CELL_SIZE);
        GraphicsContext gc = canvas.getGraphicsContext2D();
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                gc.drawImage(((x + y) % 2 == 0 ? darkBoardTile : lightBoardTile), x * CELL_SIZE, y * CELL_SIZE);
            }
        }
        Pane p = new Pane();
        p.getChildren().add(canvas);
        return new Scene(p);
    }
}
