package castle.comp3021.assignment.protocol;

import castle.comp3021.assignment.piece.Knight;

import java.util.*;
import java.util.stream.Collectors;
import java.util.stream.IntStream;

public class MakeMoveByBehavior {
    private final Behavior behavior;
    private final Game game;
    private final Move[] availableMoves;

    public MakeMoveByBehavior(Game game, Move[] availableMoves, Behavior behavior){
        this.game = game;
        this.availableMoves = availableMoves;
        this.behavior = behavior;
    }

    /**
     * Return next move according to different strategies made by each piece.
     * You can add helper method if needed, as long as this method returns a next move.
     * - {@link Behavior#RANDOM}: return a random move from {@link this#availableMoves}
     * - {@link Behavior#GREEDY}: prefer the moves towards central place, the closer, the better
     * - {@link Behavior#CAPTURING}: prefer the moves that captures the enemies, killing the more, the better.
     *                               when there are many pieces that can captures, randomly select one of them
     * - {@link Behavior#BLOCKING}: prefer the moves that block enemy's {@link Knight}.
     *                              See how to block a knight here: https://en.wikipedia.org/wiki/Xiangqi (see `Horse`)
     *
     * @return a selected move adopting strategy specified by {@link this#behavior}
     */
    public Move getNextMove(){
        if (availableMoves.length <= 0)
            return null;
        return switch (behavior) {
            case GREEDY -> Arrays.stream(availableMoves)
                                 .filter(m -> m.getSource().equals(game.getCentralPlace())
                                    && game.getPiece(m.getSource()) instanceof Knight)
                                 .findFirst()
                                 .orElse(availableMoves[
                                 IntStream.range(0, availableMoves.length)
                                          .mapToObj(i -> new AbstractMap.SimpleEntry<>(i,
                                          Math.abs(availableMoves[i].getDestination().x() - game.getCentralPlace().x())
                                          + Math.abs(availableMoves[i].getDestination().y() - game.getCentralPlace().y())))
                                          .min(Comparator.comparingInt(AbstractMap.SimpleEntry::getValue))
                                          .get()
                                          .getKey()]);
            case BLOCKING -> {
                int flatBoardSize = (int)Math.pow(game.getConfiguration().getSize(), 2);
                int boardSize = game.getConfiguration().getSize();
                List<Integer> is = IntStream.range(0, flatBoardSize)
                                            .mapToObj(i -> new AbstractMap.SimpleEntry(i,
                                            Arrays.stream(game.board).flatMap(Arrays::stream)
                                                  .collect(Collectors.toList()).get(i)))
                                            .filter(p -> p.getValue() instanceof Knight
                                                && !(((Piece)p.getValue()).getPlayer().equals(game.getCurrentPlayer())))
                                            .map(e -> Arrays.asList((int)e.getKey() + 1,
                                                                    (int)e.getKey() - 1,
                                                                    (int)e.getKey() + boardSize,
                                                                    (int)e.getKey() - boardSize))
                                            .flatMap(List::stream)
                                            .filter(i -> i >= 0 && i < flatBoardSize)
                                            .collect(Collectors.toList());
                Move[] blockingMoves = Arrays.stream(availableMoves)
                                              .filter(m ->
                                                    is.contains(m.getDestination().x() * boardSize + m.getDestination().y()))
                                              .toArray(Move[]::new);
                yield blockingMoves.length == 0 ? availableMoves[new Random().nextInt(availableMoves.length)]
                                                 : blockingMoves[new Random().nextInt(blockingMoves.length)];
            }
            case CAPTURING -> {
                Move[] caps = Arrays.stream(availableMoves)
                                          .filter(m -> game.getPiece(m.getDestination()) != null)
                                          .toArray(Move[]::new);
                yield caps.length == 0 ? availableMoves[new Random().nextInt(availableMoves.length)]
                                       : caps[new Random().nextInt(caps.length)];
            }
            default -> availableMoves[new Random().nextInt(availableMoves.length)];
        };
    }
}

