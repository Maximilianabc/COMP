package castle.comp3021.assignment.player;

import castle.comp3021.assignment.protocol.*;
import org.jetbrains.annotations.NotNull;

import java.util.Arrays;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * The player that makes move according to user input from console.
 */
public class ConsolePlayer extends Player
{
    public ConsolePlayer(String name, Color color)
    {
        super(name, color);
    }

    public ConsolePlayer(String name)
    {
        this(name, Color.GREEN);
    }

    /**
     * Choose a move from available moves.
     * This method will be called by {@link Game} object to get the move that the player wants to make when it is the
     * player's turn.
     * <p>
     * {@link ConsolePlayer} returns a move according to user's input in the console.
     * The console input format should conform the format described in the assignment description.
     * (e.g. {@literal a1->b3} means move the {@link Piece} at {@link Place}(x=0,y=0) to {@link Place}(x=1,y=2))
     * Note that in the {@link Game}.board, the index starts from 0 in both x and y dimension, while in the console
     * display, x dimension index starts from 'a' and y dimension index starts from 1.
     * <p>
     * Hint: be sure to handle invalid input to avoid invalid {@link Move}s.
     * <p>
     * <strong>Attention: Student should make sure the {@link Move} returned is valid.</strong>
     * <p>
     * <strong>Attention: {@link Place} object uses integer as index of x and y-axis, both starting from 0 to
     * facilitate programming.
     * This is VERY different from the coordinate used in console display.</strong>
     *
     * @param game           the current game object
     * @param availableMoves available moves for this player to choose from.
     * @return the chosen move
     */
    @Override
    public @NotNull Move nextMove(Game game, Move[] availableMoves)
    {
        // student implementation
        Scanner sc = new Scanner(System.in);
        String input = sc.nextLine();

        Matcher m = Pattern.compile("^([a-z])(\\d+)->([a-z])(\\d+)$").matcher(input);
        if (m.matches())
        {
            int sx = (int)(m.group(1).toCharArray()[0]) - 97;
            int dx = (int)(m.group(3).toCharArray()[0]) - 97;
            int sy = Integer.parseInt(m.group(2)) - 1;
            int dy = Integer.parseInt(m.group(4)) - 1;
            int size = game.getConfiguration().getSize();

            if (sx > size || sy > size || dx > size || dy > size) // out of boundary
            {
                System.out.println("Invalid move.");
                return new Move(-1, -1, -1, -1);
            }

            Move move = new Move(sx, sy, dx, dy);
            if (!Arrays.asList(availableMoves).contains(move))
            {
                System.out.println("Invalid move.");
                return new Move(-1, -1, -1, -1);
            }
            else
            {
                return move;
            }
        }
        else
        {
            System.out.println("Invalid move.");
            return new Move(-1, -1, -1, -1);
        }
    }
}
