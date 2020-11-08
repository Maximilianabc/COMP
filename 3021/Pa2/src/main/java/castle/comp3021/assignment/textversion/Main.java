package castle.comp3021.assignment.textversion;

import castle.comp3021.assignment.piece.Archer;
import castle.comp3021.assignment.piece.Knight;
import castle.comp3021.assignment.player.ConsolePlayer;
import castle.comp3021.assignment.player.RandomPlayer;
import castle.comp3021.assignment.protocol.Configuration;
import castle.comp3021.assignment.protocol.Game;
import castle.comp3021.assignment.protocol.Player;

public class Main {
    protected static Player whitePlayer;
    protected static Player blackPlayer;
    protected static int size;
    protected static int numMovesProtection;

    static {
        whitePlayer = new ConsolePlayer("White");
        blackPlayer = new RandomPlayer("Black");
        size = 9;
        numMovesProtection = 1;
    }

    /**
     * Create and initialize a game
     *
     * @param size               size of gameboard
     * @param numMovesProtection number of moves with capture protection
     * @return the game object
     */
    public static Game createGame(int size, int numMovesProtection) {
        Configuration configuration =
                new Configuration(size, new Player[]{whitePlayer, blackPlayer}, numMovesProtection);

        for (int i = 0; i < size; i++) {
            if (i % 2 == 0) {
                configuration.addInitialPiece(new Knight(blackPlayer), i, size - 1);
            } else {
                configuration.addInitialPiece(new Archer(blackPlayer), i, size - 1);
            }
        }
        for (int i = 0; i < size; i++) {
            if (i % 2 == 0) {
                configuration.addInitialPiece(new Knight(whitePlayer), i, 0);
            } else {
                configuration.addInitialPiece(new Archer(whitePlayer), i, 0);
            }
        }
        return new JesonMor(configuration);
    }

    public static void main(String[] args) {
        var helper = "two integer arguments are required specifying size of gameboard and number of moves with capturing protection ";

        if (args.length == 0){
            createGame(size, numMovesProtection).start();
        }

        if (args.length != 2) {
            throw new IllegalArgumentException(helper);
        }

        try {
            size = Integer.parseInt(args[0]);
        } catch (NumberFormatException e) {
            throw new IllegalArgumentException("the first argument is not a number");
        }
        try {
            numMovesProtection = Integer.parseInt(args[1]);
        } catch (NumberFormatException e) {
            throw new IllegalArgumentException("the second argument is not a number");
        }
        createGame(size, numMovesProtection).start();
    }
}
