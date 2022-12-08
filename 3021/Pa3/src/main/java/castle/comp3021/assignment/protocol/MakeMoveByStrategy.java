package castle.comp3021.assignment.protocol;

import castle.comp3021.assignment.piece.Knight;

import java.util.*;

public class MakeMoveByStrategy {
    private final Strategy strategy;
    private final Game game;
    private final Move[] availableMoves;

    public MakeMoveByStrategy(Game game, Move[] availableMoves, Strategy strategy){
        this.game = game;
        this.availableMoves = availableMoves;
        this.strategy = strategy;
    }

    /**
     * Return next move according to different strategies made by {@link castle.comp3021.assignment.player.ComputerPlayer}
     * You can add helper method if needed, as long as this method returns a next move.
     * - {@link Strategy#RANDOM}: select a random move from the proposed moves by all pieces
     * - {@link Strategy#SMART}: come up with some strategy to select a next move from the proposed moves by all pieces
     *
     * @return a next move
     */
    public Move getNextMove(){
        return switch (strategy) {
            case SMART -> {
                Optional<Move> win = Arrays.stream(availableMoves)
                                           .filter(m -> m.getSource().equals(game.getCentralPlace())
                                           && game.getPiece(m.getSource()) instanceof Knight)
                                           .findFirst();
                if (win.isPresent())
                    yield win.get();
                List<Move> legitBlockingMoves = new ArrayList<>();
                List<Move> legitCapturingMoves = new ArrayList<>();
                List<Move> greedy = new ArrayList<>();

                for(Move m : availableMoves) {
                    Place pl = m.getDestination();
                    Player p = game.getCurrentPlayer();
                    switch (game.getPiece(m.getSource()).behavior) {
                        case BLOCKING:
                            int x = pl.x();
                            int y = pl.y();
                            Piece e = game.getPiece(x - 1, y);
                            Piece s = game.getPiece(x, y - 1);
                            Piece w = game.getPiece(x + 1, y);
                            Piece n = game.getPiece(x, y + 1);
                            if (e instanceof Knight && !e.getPlayer().equals(p) ||
                                s instanceof Knight && !s.getPlayer().equals(p) ||
                                w instanceof Knight && !w.getPlayer().equals(p) ||
                                n instanceof Knight && !n.getPlayer().equals(p))
                                legitBlockingMoves.add(m);
                            break;
                        case CAPTURING:
                            Piece ps = game.getPiece(pl);
                            if (ps != null && !ps.getPlayer().equals(p))
                                legitCapturingMoves.add(m);
                        case GREEDY:
                            greedy.add(m);
                        default:
                            break;
                    }
                }
                if (legitCapturingMoves.size() > 0) {
                    yield legitCapturingMoves.get(new Random().nextInt(legitCapturingMoves.size()));
                }
                if (legitBlockingMoves.size() > 0) {
                    yield legitBlockingMoves.get(new Random().nextInt(legitBlockingMoves.size()));
                }
                yield greedy.stream()
                            .min(Comparator.comparingInt(m ->
                                Math.abs(m.getDestination().x() - game.getCentralPlace().x())
                                + Math.abs(m.getDestination().y() - game.getCentralPlace().y())))
                            .get();
            }
            default -> availableMoves[new Random().nextInt(availableMoves.length)];
        };
    }
}
