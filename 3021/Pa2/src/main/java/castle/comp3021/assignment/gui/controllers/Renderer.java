package castle.comp3021.assignment.gui.controllers;

import castle.comp3021.assignment.gui.ViewConfig;
import castle.comp3021.assignment.protocol.Piece;
import castle.comp3021.assignment.protocol.Place;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
import javafx.scene.paint.Color;
import org.jetbrains.annotations.NotNull;


/**
 * This class render images
 *  - All image resources can be found in main/resources/assets/images folder.
 *  - The size of piece is defined in gui/ViewConfig
 * Helper class for render operations on a {@link Canvas}.
 * Hint:
 * Necessary functions:
 * - Render chess pieces with different kinds and colors
 * - Render chess board
 *     - There are two kinds of chess board image: lightBoard.png and darkBoard.png.
 *     - They should take turn to appear
 * - Highlight the selected board (can be implemented with rectangle)
 * - Highlight the path when mouse moves (can be implemented with oval with a small radius
 */
public class Renderer {
    /**
     * An image of a cell, with support for rotated images.
     */
    public static class CellImage {

        /**
         * Image of the cell.
         */
        @NotNull
        final Image image;
        /**
         * @param image    Image of the cell.
         */
        public CellImage(@NotNull Image image) {
            this.image = image;
        }
    }

    /**
     * Draws a rotated image onto a {@link GraphicsContext}.
     * The radius = 12
     * Color = rgb(255, 255, 220)
     * @param gc    Target Graphics Context.
     * @param x     X-coordinate relative to the graphics context to draw the oval.
     * @param y     Y-coordinate relative to the graphics context to draw the oval.
     */
    public static void drawOval(@NotNull GraphicsContext gc, double x, double y) {
        gc.setFill(Color.rgb(255, 255, 220));
        gc.fillOval(x, y, 12, 12);
    }

    /**
     * Draw a rectangle to show mouse dragging path
     * The width and height are set to be PIECE_SIZE in {@link castle.comp3021.assignment.gui.ViewConfig}
     * @param gc the graphicsContext of canvas
     * @param x X-coordinate relative to the graphics context to draw the rectangle.
     * @param y Y-coordinate relative to the graphics context to draw the rectangle.
     */
    public static void drawRectangle(@NotNull GraphicsContext gc, double x, double y) {
        gc.setFill(Color.rgb(255, 255, 220));
        gc.fillRect(x, y, ViewConfig.PIECE_SIZE, ViewConfig.PIECE_SIZE);
    }

    /**
     * Render chess board
     *     - There are two kinds of chess board image: lightBoard.png and darkBoard.png.
     *     - They should take turn to appear
     * @param canvas given canvas
     * @param boardSize the size of board
     * @param centerPlace the central place
     */
    public static void renderChessBoard(@NotNull Canvas canvas, int boardSize, Place centerPlace){
        CellImage darkBoardTile = new CellImage(new Image("file:" + ResourceLoader.getResource("assets/images/darkBoard.png")));
        CellImage lightBoardTile = new CellImage(new Image("file:" + ResourceLoader.getResource("assets/images/lightBoard.png")));
        GraphicsContext gc = canvas.getGraphicsContext2D();
        for (int x = 0; x < boardSize; x++) {
            for (int y = 0; y < boardSize; y++) {
                gc.drawImage(((x + y) % 2 == 0 ? darkBoardTile.image : lightBoardTile.image),
                x * ViewConfig.PIECE_SIZE, y * ViewConfig.PIECE_SIZE);
                if (x == centerPlace.x() && y == centerPlace.y()) {
                    gc.drawImage(new Image("file:" + ResourceLoader.getResource("assets/images/center.png")),
                    x * ViewConfig.PIECE_SIZE, y * ViewConfig.PIECE_SIZE);
                }
            }
        }
    }

    /**
     * Render pieces on the chess board
     * @param canvas given canvas
     * @param board board with pieces
     */
    public static void renderPieces(@NotNull Canvas canvas, @NotNull Piece[][] board) {
        Image blackK = new Image("file:" + ResourceLoader.getResource("assets/images/blackK.png"));
        Image blackA = new Image("file:" + ResourceLoader.getResource("assets/images/blackA.png"));
        Image whiteK = new Image("file:" + ResourceLoader.getResource("assets/images/whiteK.png"));
        Image whiteA = new Image("file:" + ResourceLoader.getResource("assets/images/whiteA.png"));

        for (int x = 0; x < board.length; x++) {
            for (int y = 0; y < board[x].length; y++) {
                if (board[x][y] == null) continue;
                canvas.getGraphicsContext2D().drawImage(
                board[x][y].getLabel() == 'K'
                ? (board[x][y].getPlayer().getName().equals("Black") ? blackK : whiteK)
                : (board[x][y].getPlayer().getName().equals("Black") ? blackA : whiteA),
                x * ViewConfig.PIECE_SIZE, y * ViewConfig.PIECE_SIZE);
            }
        }
    }
}
