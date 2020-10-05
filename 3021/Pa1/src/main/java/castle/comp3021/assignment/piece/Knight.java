package castle.comp3021.assignment.piece;

import castle.comp3021.assignment.protocol.Game;
import castle.comp3021.assignment.protocol.Move;
import castle.comp3021.assignment.protocol.Piece;
import castle.comp3021.assignment.protocol.Place;
import castle.comp3021.assignment.protocol.Player;

import java.util.ArrayList;
import java.util.List;

/**
 * Knight piece that moves similar to knight in chess.
 * Rules of move of Knight can be found in wikipedia (https://en.wikipedia.org/wiki/Knight_(chess)).
 *
 * @see <a href='https://en.wikipedia.org/wiki/Knight_(chess)'>Wikipedia</a>
 */
public class Knight extends Piece
{
    public Knight(Player player)
    {
        super(player);
    }

    @Override
    public char getLabel()
    {
        return 'K';
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
    public Move[] getAvailableMoves(Game game, Place source)
    {
        // student implementation
        int boundary = game.getConfiguration().getSize() - 1;
        List<Move> possibleMoves = new ArrayList<>()
        {
            {
                add(new Move(source, source.x() + 1, source.y() + 2));
                add(new Move(source, source.x() + 2, source.y() + 1));
                add(new Move(source, source.x() + 2, source.y() - 1));
                add(new Move(source, source.x() + 1, source.y() - 2));
                add(new Move(source, source.x() - 1, source.y() - 2));
                add(new Move(source, source.x() - 2, source.y() - 1));
                add(new Move(source, source.x() - 2, source.y() + 1));
                add(new Move(source, source.x() - 1, source.y() + 2));
            }
        };
        possibleMoves.removeIf(m ->
        m.getDestination().x() < 0 || m.getDestination().y() < 0
        || m.getDestination().x() > boundary || m.getDestination().y() > boundary); // remove moves with -ve coordinates

        // check blockage
        if (source.x() + 1 < boundary && game.getPiece(source.x() + 1, source.y()) != null) // right-blocked
        {
            possibleMoves.removeIf(m -> m.getDestination().x() == source.x() + 2);
        }
        if (source.x() - 1 > 0 && game.getPiece(source.x() - 1, source.y()) != null) // left-blocked
        {
            possibleMoves.removeIf(m -> m.getDestination().x() == source.x() - 2);
        }
        if (source.y() + 1 < boundary && game.getPiece(source.x(), source.y() + 1) != null) // up-blocked
        {
            possibleMoves.removeIf(m -> m.getDestination().y() == source.y() + 2);
        }
        if (source.y() - 1 > 0 && game.getPiece(source.x(), source.y() - 1) != null) // down-blocked
        {
            possibleMoves.removeIf(m -> m.getDestination().y() == source.y() - 2);
        }

        // check occupied
        boolean canCapture = game.getConfiguration().getNumMovesProtection() <= game.getNumMoves();
        // != null --> occupied
        // Cannot capture --> cannot move there no matter whose piece it is
        // Name same as current player --> own piece --> cannot move there
        possibleMoves.removeIf(m -> game.getPiece(m.getDestination()) != null
        && (!canCapture || game.getPiece(m.getDestination()).getPlayer() == game.getCurrentPlayer()));

        return possibleMoves.toArray(new Move[possibleMoves.size()]);
    }
}
