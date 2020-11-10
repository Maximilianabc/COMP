package castle.comp3021.assignment.protocol;

import org.jetbrains.annotations.NotNull;

import java.util.Objects;

public class MoveRecord implements Cloneable {
    private Player player;
    private Move move;

    public MoveRecord(@NotNull Player player, @NotNull Move move) {
        this.player = player;
        this.move = move;
    }

    public Player getPlayer() {
        return player;
    }

    public Move getMove() {
        return move;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        MoveRecord that = (MoveRecord) o;
        return player.equals(that.player) &&
                move.equals(that.move);
    }

    @Override
    public int hashCode() {
        return Objects.hash(player, move);
    }


    /**
     * Convert MoveRecord instance to string
     * - Format example:
     * player:White; move:(3,0)->(3,1)
     * player:Black; move:(3,8)->(3,2)
     * player:White; move:(4,0)->(5,2)
     * player:Black; move:(1,8)->(1,1)
     *
     * @return String of move records
     */
    @Override
    public String toString() {
        return String.format("player:%1$s; move:%2$s->%3$s\n", player.getName(), move.getSource().toString(), move.getDestination().toString());
    }

    @Override
    public MoveRecord clone() throws CloneNotSupportedException {
        var cloned = (MoveRecord) super.clone();
        cloned.player = this.player.clone();
        cloned.move = this.move.clone();
        return cloned;
    }
}
