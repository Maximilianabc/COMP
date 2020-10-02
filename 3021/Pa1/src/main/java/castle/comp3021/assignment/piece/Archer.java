package castle.comp3021.assignment.piece;

import castle.comp3021.assignment.protocol.Game;
import castle.comp3021.assignment.protocol.Move;
import castle.comp3021.assignment.protocol.Piece;
import castle.comp3021.assignment.protocol.Place;
import castle.comp3021.assignment.protocol.Player;

import java.util.ArrayList;
import java.util.List;

/**
 * Archer piece that moves similar to cannon in chinese chess.
 * Rules of move of Archer can be found in wikipedia (https://en.wikipedia.org/wiki/Xiangqi#Cannon).
 * <p>
 * <strong>Attention: If you want to implement Archer as the bonus task, you should remove "{@code throw new
 * UnsupportedOperationException();}" in the constructor of this class.</strong>
 *
 * @see <a href='https://en.wikipedia.org/wiki/Xiangqi#Cannon'>Wikipedia</a>
 */
public class Archer extends Piece {
    public Archer(Player player) {
        super(player);
    }

    @Override
    public char getLabel() {
        return 'A';
    }

    /**
     * Returns an array of moves that are valid given the current place of the piece.
     * Given the {@link Game} object and the {@link Place} that current knight piece locates, this method should
     * return ALL VALID {@link Move}s according to the current {@link Place} of this knight piece.
     * All the returned {@link Move} should have source equal to the source parameter.
     * <p>
     * Hint: you should consider corner cases when the {@link Move} is not valid on the gameboard.
     * Several tests are provided and your implementation should pass them.
     * <p>
     * <strong>Attention: Student should make sure all {@link Move}s returned are valid.</strong>
     *
     * @param game   the game object
     * @param source the current place of the piece
     * @return an array of available moves
     */
    @Override
    public Move[] getAvailableMoves(Game game, Place source) {
        // student implementation

        boolean canCapture = game.getConfiguration().getNumMovesProtection() <= game.getNumMoves();
        int boundary = game.getConfiguration().getSize();
        List<Move> possibleMoves = new ArrayList<>();

        boolean hasPotentialPlatform = false;
        for (int x = source.x() + 1; x < boundary; x++) // traverse to +ve x-direction w.r.t. archer
        {
            Piece p = game.getPiece(x, source.y());
            if (p != null)
            {
                if (!hasPotentialPlatform)
                    hasPotentialPlatform = true;
                // found platform and now find another piece behind it w.r.t. archer
                // && it is not own piece && not in protected rounds --> can capture
                else if (canCapture
                        && (p.getPlayer().getName() != game.getCurrentPlayer().getName()))
                {
                    possibleMoves.add(new Move(source, x, source.y()));
                    hasPotentialPlatform = false; // reset
                    break;
                }
            }
            if (!hasPotentialPlatform) // cannot move behind the platform
                possibleMoves.add(new Move(source, x, source.y()));
        }
        // do the same for -ve x-direction
        for (int x = source.x() - 1; x >= 0; x--)
        {
            Piece p = game.getPiece(x, source.y());
            if (p != null)
            {
                if (!hasPotentialPlatform)
                    hasPotentialPlatform = true;
                else if (canCapture
                        && (p.getPlayer().getName() != game.getCurrentPlayer().getName()))
                {
                    possibleMoves.add(new Move(source, x, source.y()));
                    hasPotentialPlatform = false;
                    break;
                }
            }
            if (!hasPotentialPlatform)
                possibleMoves.add(new Move(source, x, source.y()));
        }
        //do the same for +ve y-direction
        for (int y = source.y() + 1; y < boundary; y++)
        {
            Piece p = game.getPiece(source.x(), y);
            if (p != null)
            {
                if (!hasPotentialPlatform)
                    hasPotentialPlatform = true;
                else if (canCapture
                        && (p.getPlayer().getName() != game.getCurrentPlayer().getName()))
                {
                    possibleMoves.add(new Move(source, source.x(), y));
                    hasPotentialPlatform = false;
                    break;
                }
            }
            if (!hasPotentialPlatform)
                possibleMoves.add(new Move(source, source.x(), y));
        }
        //do the same for -ve y-direction
        for (int y = source.y() - 1; y >= 0; y--)
        {
            Piece p = game.getPiece(source.x(), y);
            if (p != null)
            {
                if (!hasPotentialPlatform)
                    hasPotentialPlatform = true;
                else if (canCapture
                        && (p.getPlayer().getName() != game.getCurrentPlayer().getName()))
                {
                    possibleMoves.add(new Move(source, source.x(), y));
                    hasPotentialPlatform = false;
                    break;
                }
            }
            if (!hasPotentialPlatform)
                possibleMoves.add(new Move(source, source.x(), y));
        }
        return possibleMoves.toArray(new Move[possibleMoves.size()]);
    }
}
