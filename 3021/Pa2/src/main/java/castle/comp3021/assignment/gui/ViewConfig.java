package castle.comp3021.assignment.gui;

import castle.comp3021.assignment.gui.controllers.ResourceLoader;
import org.jetbrains.annotations.NotNull;

public class ViewConfig {

    private ViewConfig() {
    }


    /**
     * The preset width and height of the pane in pixel
     */
    public static final int WIDTH = 800;
    public static final int HEIGHT = 600;

    /**
     * The preset size of piece in pixel
     */
    public static final int PIECE_SIZE = 32;

    public static final String CSS_STYLES_PATH = ResourceLoader.getResource("assets/styles/styles.css");

    /**
     * @return About text of this game.
     */
    @NotNull
    public static String getAboutText() {
        return
                "\t\tAbout Jeson Mor" +
                        " Pieces:\n" +
                        "  - Knight: move 1 horizontal + 2 vertical or 2 horizontal + 1 vertical\n" +
                        "  - Archer: move straightforward\n" +
                        "n" +
                        "Player:\n" +
                        "- Computer / Human\n" +
                        "- Each enemy's piece across one piece\n"
                        + "\n" +
                        "Size of Board:\n" +
                        "- Odd, >= 3, <= 26\n" +
                        "\n" +
                        "Number of protection moves:\n" +
                        "- Positive number\n" +
                        "- Within this number of rounds, no one can win.\n" +
                        "\n" +
                        "Duration of each round:\n" +
                        "- Each player should make decision within 30 seconds, otherwise, lose the game\n";
    }

    /**
     * Necessary warning statements
     */
    @NotNull
    public static final String MSG_BAD_SIZE_NUM = "Size of game board must be at least 3";
    @NotNull
    public static final String MSG_ODD_SIZE_NUM = "Size of game board must be an odd number";
    @NotNull
    public static final String MSG_UPPERBOUND_SIZE_NUM = "Size of game board is at most 26";
    @NotNull
    public static final String MSG_NEG_PROT = "Steps of protection should be greater than or equal to 0.";
    @NotNull
    public static final String MSG_NEG_DURATION = "Duration of each round should be greater than 0.";
}

