package castle.comp3021.assignment.piece;

import castle.comp3021.assignment.protocol.Game;
import castle.comp3021.assignment.protocol.Move;
import castle.comp3021.assignment.protocol.Piece;
import castle.comp3021.assignment.protocol.Place;
import castle.comp3021.assignment.protocol.Player;

import java.util.ArrayList;
import java.util.Comparator;
import java.util.List;
import java.util.function.Predicate;
import java.util.stream.Collectors;

/**
 * Archer piece that moves similar to cannon in chinese chess.
 * Rules of move of Archer can be found in wikipedia (https://en.wikipedia.org/wiki/Xiangqi#Cannon).
 * <p>
 * <strong>Attention: If you want to implement Archer as the bonus task, you should remove "{@code throw new
 * UnsupportedOperationException();}" in the constructor of this class.</strong>
 *
 * @see <a href='https://en.wikipedia.org/wiki/Xiangqi#Cannon'>Wikipedia</a>
 */
public class Archer extends Piece
{
    public Archer(Player player)
    {
        super(player);
    }

    @Override
    public char getLabel()
    {
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
    public Move[] getAvailableMoves(Game game, Place source)
    {
        // student implementation
        int boundary = game.getConfiguration().getSize();
        List<Move> possibleMoves = new ArrayList<>();
        List<Place> occupied = new ArrayList<>();

        for (int i = 0; i < boundary; i++)
        {
            if (i == source.x())
            {
                continue;
            }
            Piece p = game.getPiece(i, source.y());
            if (p != null)
            {
                occupied.add(new Place(i, source.y()));
            }
            possibleMoves.add(new Move(source, i, source.y()));
        }
        for (int j = 0; j < boundary; j++)
        {
            if (j == source.y())
            {
                continue;
            }
            Piece p = game.getPiece(source.x(), j);
            if (p != null)
            {
                occupied.add(new Place(source.x(), j));
            }
            possibleMoves.add(new Move(source, source.x(), j));
        }

        Predicate<Place> left = o -> o.x() < source.x();
        Predicate<Place> right = o -> o.x() > source.x();
        Predicate<Place> up = o -> o.y() > source.y();
        Predicate<Place> down = o -> o.y() < source.y();

        List<Place> ps = occupied.stream().filter(up).sorted(Comparator.comparing(Place::y)).collect(Collectors.toList());
        RemoveInvalidMoves(game, possibleMoves, ps, 0);
        ps = occupied.stream().filter(right).sorted(Comparator.comparing(Place::x)).collect(Collectors.toList());
        RemoveInvalidMoves(game, possibleMoves, ps, 1);
        ps = occupied.stream().filter(down).sorted(Comparator.comparing(Place::y).reversed()).collect(Collectors.toList());
        RemoveInvalidMoves(game, possibleMoves, ps, 2);
        ps = occupied.stream().filter(left).sorted(Comparator.comparing(Place::x).reversed()).collect(Collectors.toList());
        RemoveInvalidMoves(game, possibleMoves, ps, 3);

        return possibleMoves.toArray(new Move[possibleMoves.size()]);
    }

    private void RemoveInvalidMoves(Game game, List<Move> possibleMoves, List<Place> ps, int direction)
    {
        /* platform ('P'), target ('T'), blockage ('B')
         * when there's one piece only in ps, it's blockage, not platform, e.g.
         * | . . . B . A |
         * when there are more than two pieces, the closet is platform, the 2nd closet is target, e.g.
         * | . T . P . A |
         * */
        int numProtect = game.getConfiguration().getNumMovesProtection();
        boolean canCapture = numProtect == 0 ? true : numProtect <= game.getNumMoves();
        long psSize = ps.size();

        Predicate<Move> furtherToBlockage = null;
        Predicate<Move> furtherToTarget = null;
        Predicate<Move> betweenTargetAndPlatform = null;
        Predicate<Move> target = null;

        if (psSize > 0)
        {
            switch (direction)
            {
                // up
                case 0 -> {
                    furtherToBlockage = m -> m.getDestination().y() >= ps.get(0).y();
                    if (psSize > 1) // need to have more than 1 piece inorder to have platform and target
                    {
                        int t = ps.get(1).y();
                        furtherToTarget = m -> m.getDestination().y() > t;
                        betweenTargetAndPlatform = m -> m.getDestination().y() < t && m.getDestination().y() >= ps.get(0).y();
                        target = m -> m.getDestination().y() == t;
                    }
                }
                // right
                case 1 -> {
                    furtherToBlockage = m -> m.getDestination().x() >= ps.get(0).x();
                    if (psSize > 1)
                    {
                        int t = ps.get(1).x();
                        furtherToTarget = m -> m.getDestination().x() > t;
                        betweenTargetAndPlatform = m -> m.getDestination().x() < t && m.getDestination().x() >= ps.get(0).x();
                        target = m -> m.getDestination().x() == t;
                    }
                }
                // down
                case 2 -> {
                    furtherToBlockage = m -> m.getDestination().y() <= ps.get(0).y();
                    if (psSize > 1)
                    {
                        int t = ps.get(1).y();
                        furtherToTarget = m -> m.getDestination().y() < t;
                        betweenTargetAndPlatform = m -> m.getDestination().y() > t && m.getDestination().y() <= ps.get(0).y();
                        target = m -> m.getDestination().y() == t;
                    }
                }
                // left
                case 3 -> {
                    furtherToBlockage = m -> m.getDestination().x() <= ps.get(0).x();
                    if (psSize > 1)
                    {
                        int t = ps.get(1).x();
                        furtherToTarget = m -> m.getDestination().x() < t;
                        betweenTargetAndPlatform = m -> m.getDestination().x() > t && m.getDestination().x() <= ps.get(0).x();
                        target = m -> m.getDestination().x() == t;
                    }
                }
            }
            if (psSize == 1) // blockage only
            { possibleMoves.removeIf(furtherToBlockage); }
            else
            {
                possibleMoves.removeIf(furtherToTarget.or(betweenTargetAndPlatform));
                Piece p = game.getPiece(ps.get(1));
                if (p.getPlayer() == game.getCurrentPlayer() || !canCapture)
                {
                    possibleMoves.removeIf(target); // remove the move to target
                }
            }
        }
    }
}
