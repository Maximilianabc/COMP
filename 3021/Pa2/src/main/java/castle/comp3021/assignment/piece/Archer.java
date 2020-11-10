package castle.comp3021.assignment.piece;

import castle.comp3021.assignment.gui.controllers.Renderer;
import castle.comp3021.assignment.gui.controllers.ResourceLoader;
import castle.comp3021.assignment.protocol.*;
import org.jetbrains.annotations.NotNull;

import java.util.ArrayList;

/**
 * Archer piece that moves similar to cannon in chinese chess.
 * Rules of move of Archer can be found in wikipedia (https://en.wikipedia.org/wiki/Xiangqi#Cannon).
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

    @Override
    public Move[] getAvailableMoves(Game game, Place source) {
        var moves = new ArrayList<Move>();
        for (int x = 0; x < game.getConfiguration().getSize(); x++) {
            if (x != source.x()) {
                moves.add(new Move(source, x, source.y()));
            }
        }
        for (int y = 0; y < game.getConfiguration().getSize(); y++) {
            if (y != source.y()) {
                moves.add(new Move(source, source.x(), y));
            }
        }

        return moves.stream()
                .filter(move -> validateMove(game, move))
                .toArray(Move[]::new);
    }

    private boolean validateMove(Game game, Move move) {
        var rules = new Rule[]{
                new OutOfBoundaryRule(),
                new OccupiedRule(),
                new VacantRule(),
                new NilMoveRule(),
                new FirstNMovesProtectionRule(game.getConfiguration().getNumMovesProtection()),
                new ArcherMoveRule(),
        };
        for (var rule :
                rules) {
            if (!rule.validate(game, move)) {
                return false;
            }
        }
        return true;
    }

    /**
     * Render archer pieces to the corresponding images implemented in {@link ResourceLoader getImage}
     * Hint: consider different color of archer pieces.
     * Throw IllegalStateException exception if failed to load image.
     * @return {@link Renderer.CellImage}
     */
    public Renderer.@NotNull CellImage getImageRep() {
        // TODO

        return null;
    }




}
