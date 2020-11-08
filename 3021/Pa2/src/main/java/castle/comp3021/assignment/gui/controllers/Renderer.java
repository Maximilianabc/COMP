package castle.comp3021.assignment.gui.controllers;

import castle.comp3021.assignment.protocol.Piece;
import castle.comp3021.assignment.protocol.Place;
import javafx.scene.canvas.Canvas;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.image.Image;
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
        // TODO
    }

    /**
     * Draw a rectangle to show mouse dragging path
     * The width and height are set to be PIECE_SIZE in {@link castle.comp3021.assignment.gui.ViewConfig}
     * @param gc the graphicsContext of canvas
     * @param x X-coordinate relative to the graphics context to draw the rectangle.
     * @param y Y-coordinate relative to the graphics context to draw the rectangle.
     */
    public static void drawRectangle(@NotNull GraphicsContext gc, double x, double y){
        //TODO
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
        //TODO
    }

    /**
     * Render pieces on the chess board
     * @param canvas given canvas
     * @param board board with pieces
     */
    public static void renderPieces(@NotNull Canvas canvas, @NotNull Piece[][] board) {
        //TODO
    }

}
