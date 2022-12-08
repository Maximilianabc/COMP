package castle.comp3021.assignment.action;

import castle.comp3021.assignment.player.ComputerPlayer;
import castle.comp3021.assignment.protocol.*;
import castle.comp3021.assignment.protocol.exception.ActionException;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Resume a paused piece.
 * <p>
 * The piece must belong to {@link ComputerPlayer}.
 * The piece must not be terminated.
 */
public class ResumePieceAction extends Action {

    /**
     * @param game the current {@link Game} object
     * @param args the arguments input by users in the console
     */
    public ResumePieceAction(Game game, String[] args) {
        super(game, args);
    }

    /**
     * Resume the piece according to {@link this#args}
     * Expected {@link this#args}: "a1"
     * Hint:
     * Consider corner cases (e.g., invalid {@link this#args})
     * Throw {@link ActionException} when exception happens.
     * <p>
     * Related meethods:
     * - {@link Piece#resume()}
     */
    @Override
    public void perform() throws ActionException {
        Matcher m = Pattern.compile("^([a-z])(\\d{1,2})$").matcher(args[0]);
        if (m.find()) {
            int x = m.group(1).toCharArray()[0] - 97;
            int y = Integer.parseInt(m.group(2)) - 1;

            int size = game.getConfiguration().getSize();
            if (y >= size || x >= size || y < 0 || x < 0) {
                throw new ActionException("Invalid args.");
            }
            Piece p = game.getPiece(x, y);
            if (p != null) {
                if (p.getPlayer() instanceof ComputerPlayer) {
                    p.resume();
                } else {
                    throw new ActionException("The piece does not belong to computer player.");
                }
            } else {
                throw new ActionException("Piece does not exist at the specified place");
            }
        } else {
            throw new ActionException("Invalid args.");
        }
    }

    @Override
    public String toString() {
        return "Action[Resume piece]";
    }
}
