package castle.comp3021.assignment.piece;

import castle.comp3021.assignment.protocol.*;

public class CriticalRegionRule implements Rule {


    /**
     * Validate whether the proposed  move will violate the critical region rule
     * I.e., there are no more than {@link Configuration#getCriticalRegionCapacity()} in the critical region.
     * Determine whether the move is in critical region, using {@link this#isInCriticalRegion(Game, Place)}
     * @param game the current game object
     * @param move the move to be validated
     * @return whether the given move is valid or not
     */
    @Override
    public boolean validate(Game game, Move move) {
        Configuration c = game.getConfiguration();
        int size = c.getSize();
        int critic = 0;
        if (isInCriticalRegion(game, move.getDestination())) {
            critic++;
        } else {
            return true;
        }
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {
                Piece p = game.getBoard()[i][j];
                if (p instanceof Knight && p.getPlayer().equals(game.getCurrentPlayer())) {
                    if (isInCriticalRegion(game, new Place(i, j))) {
                        critic++;
                    }
                }
            }
        }
        return critic <= c.getCriticalRegionCapacity();
    }

    /**
     * Check whether the given move is in critical region
     * Critical region is {@link Configuration#getCriticalRegionSize()} of rows, centered around center place
     * Example:
     *      In a 5 * 5 board, which center place lies in the 3rd row
     *      Suppose critical region size = 3, then for row 1-5, the critical region include row 2-4.
     * @param game the current game object
     * @param place the move to be validated
     * @return whether the given move is in critical region
     */
    private boolean isInCriticalRegion(Game game, Place place) {
        int regionSize = game.getConfiguration().getCriticalRegionSize();
        int cx = game.getConfiguration().getCentralPlace().x();
        return place.y() <= cx + (regionSize - 1) / 2 && place.y() >= cx - (regionSize - 1) / 2;
    }

    @Override
    public String getDescription() {
        return "critical region is full";
    }
}
