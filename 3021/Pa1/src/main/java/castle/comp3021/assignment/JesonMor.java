package castle.comp3021.assignment;

import castle.comp3021.assignment.piece.Knight;
import castle.comp3021.assignment.protocol.*;
import org.jetbrains.annotations.NotNull;

import java.lang.Math;
import java.util.*;

/**
 * This class extends {@link Game}, implementing the game logic of JesonMor game.
 * Student needs to implement methods in this class to make the game work.
 * Hint: make good use of methods predefined in {@link Game} to get various information to facilitate your work.
 * <p>
 * Several sample tests are provided to test your implementation of each method in the test directory.
 * Please make make sure all tests pass before submitting the assignment.
 */
public class JesonMor extends Game
{
    public JesonMor(Configuration configuration)
    {
        super(configuration);
    }

    /**
     * Start the game
     * Players will take turns according to the order in {@link Configuration#getPlayers()} to make a move until
     * a player wins.
     * <p>
     * In the implementation, student should implement the loop letting two players take turns to move pieces.
     * The order of the players should be consistent to the order in {@link Configuration#getPlayers()}.
     * {@link Player#nextMove(Game, Move[])} should be used to retrieve the player's choice of his next move.
     * After each move, {@link Game#refreshOutput()} should be called to refresh the gameboard printed in the console.
     * <p>
     * When a winner appears, set the local variable {@code winner} so that this method can return the winner.
     *
     * @return the winner
     */
    @Override
    public Player start()
    {
        // reset all things
        Player winner;
        this.numMoves = 0;
        this.board = configuration.getInitialBoard();
        this.currentPlayer = null;
        this.refreshOutput();
        while (true)
        {
            // student implementation starts here
            currentPlayer = configuration.getPlayers()[numMoves % 2];
            Move[] ms = getAvailableMoves(currentPlayer);
            Piece p = null;
            Move m = null;
            if (ms.length > 0)
            {
                m = currentPlayer.nextMove(this, ms);
                if (m.getSource().x() != -1) // Error returned from nextMove
                {
                    p = board[m.getSource().x()][m.getSource().y()];
                    movePiece(m);
                    updateScore(currentPlayer, p, m);
                    refreshOutput();
                }
                else
                {
                    continue;
                }
            }
            winner = getWinner(currentPlayer, p, m);

            // student implementation ends here
            if (winner != null)
            {
                System.out.println();
                System.out.println("Congratulations! ");
                System.out.printf("Winner: %s%s%s\n", winner.getColor(), winner.getName(), Color.DEFAULT);
                return winner;
            }
        }
    }

    /**
     * Get the winner of the game. If there is no winner yet, return null;
     * This method will be called every time after a player makes a move and after
     * {@link JesonMor#updateScore(Player, Piece, Move)} is called, in order to
     * check whether any {@link Player} wins.
     * If this method returns a player (the winner), then the game will exit with the winner.
     * If this method returns null, next player will be asked to make a move.
     *
     * @param lastPlayer the last player who makes a move
     * @param lastMove   the last move made by lastPlayer
     * @param lastPiece  the last piece that is moved by the player
     * @return the winner if it exists, otherwise return null
     */
    @Override
    public Player getWinner(Player lastPlayer, Piece lastPiece, Move lastMove)
    {
        // student implementation
        if (lastPiece == null && lastMove == null) // no moves, can be tie-break or no pieces
        {
            int count0 = 0; // no. of pieces of player1
            int count1 = 0; // no. of pieces of player2
            for (int x = 0; x < configuration.getSize(); x++)
            {
                for (int y = 0; y < configuration.getSize(); y++)
                {
                    if (board[x][y] != null)
                    {
                        if (board[x][y].getPlayer() == getPlayers()[0])
                        {
                            count0++;
                        }
                        else
                        {
                            count1++;
                        }
                    }
                }
            }
            if (count0 > 0 && count1 > 0) // both players still have at least 1 piece --> tie-break
            {
                int p1s = getPlayers()[0].getScore();
                int p2s = getPlayers()[1].getScore();
                return p1s != p2s // scores are different
                       ? (p1s > p2s // player with lower score wins
                          ? getPlayers()[1] : getPlayers()[0])
                       : currentPlayer; // scores equal --> current player wins
            }
            else // either player has no pieces left
            {
                return count0 == 0 ? getPlayers()[1] : getPlayers()[0];
            }
        }
        else // a move was made
        {
            if (this.numMoves > configuration.getNumMovesProtection() // can win by leaving centre
            && (lastPiece instanceof Knight || lastPiece.getLabel() == 'M')) // is Knight (or Mock)
            {
                Place centre = configuration.getCentralPlace();
                if (lastPiece.getPlayer() == lastPlayer
                && lastMove.getSource().x() == centre.x()
                && lastMove.getSource().y() == centre.y()) // moved out from centre
                {
                    return lastPlayer;
                }
            }
        }
        return null;
    }

    /**
     * Update the score of a player according to the {@link Piece} and corresponding move made by him just now.
     * This method will be called every time after a player makes a move, in order to update the corresponding score
     * of this player.
     * <p>
     * The score of a player is the cumulative score of each move he makes.
     * The score of each move is calculated with the Manhattan distance between the source and destination {@link Place}.
     * <p>
     * Student can use {@link Player#getScore()} to get the current score of a player before updating.
     * {@link Player#setScore(int)} can be used to update the score of a player.
     * <p>
     * <strong>Attention: Student should make NO assumption that the {@link Move} is valid.</strong>
     *
     * @param player the player who just makes a move
     * @param piece  the piece that is just moved
     * @param move   the move that is just made
     */
    public void updateScore(Player player, Piece piece, Move move)
    {
        Piece p = board[move.getDestination().x()][move.getDestination().y()];
        if (p == piece)
        {
            int distance = Math.abs(move.getDestination().x() - move.getSource().x())
            + Math.abs(move.getDestination().y() - move.getSource().y());
            player.setScore(player.getScore() + distance);
        }
    }


    /**
     * Make a move.
     * This method performs moving a {@link Piece} from source to destination {@link Place} according {@link Move} object.
     * Note that after the move, there will be no {@link Piece} in source {@link Place}.
     * <p>
     * Positions of all {@link Piece}s on the gameboard are stored in {@link JesonMor#board} field as a 2-dimension array of
     * {@link Piece} objects.
     * The x and y coordinate of a {@link Place} on the gameboard are used as index in {@link JesonMor#board}.
     * E.g. {@code board[place.x()][place.y()]}.
     * If one {@link Place} does not have a piece on it, it will be null in {@code board[place.x()][place.y()]}.
     * Student may modify elements in {@link JesonMor#board} to implement moving a {@link Piece}.
     * The {@link Move} object can be considered valid on present gameboard.
     *
     * @param move the move to make
     */
    public void movePiece(@NotNull Move move)
    {
        // student implementation
        board[move.getDestination().x()][move.getDestination().y()] = null;
        board[move.getDestination().x()][move.getDestination().y()] = board[move.getSource().x()][move.getSource().y()];
        board[move.getSource().x()][move.getSource().y()] = null;
        numMoves++;
    }

    /**
     * Get all available moves of one player.
     * This method is called when it is the {@link Player}'s turn to make a move.
     * It will iterate all {@link Piece}s belonging to the {@link Player} on board and obtain available moves of
     * each of the {@link Piece}s through method {@link Piece#getAvailableMoves(Game, Place)} of each {@link Piece}.
     * <p>
     * <strong>Attention: Student should make sure all {@link Move}s returned are valid.</strong>
     *
     * @param player the player whose available moves to get
     * @return an array of available moves
     */
    public @NotNull Move[] getAvailableMoves(Player player)
    {
        // student implementation
        List<Move> moves = new ArrayList<>();
        for (int x = 0; x < configuration.getSize(); x++)
        {
            for (int y = 0; y < configuration.getSize(); y++)
            {
                if (board[x][y] == null)
                {
                    continue;
                }
                if (board[x][y].getPlayer() == player)
                {
                    Collections.addAll(moves, board[x][y].getAvailableMoves(this, new Place(x, y)));
                }
            }
        }
        return moves.toArray(new Move[moves.size()]);
    }
}
