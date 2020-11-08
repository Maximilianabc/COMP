import java.io.*;
import java.util.Scanner;

public class Board {

    /**
     * This method saves a rectangular block of text (representing a board in PA1) based on the given dimensions
     * to file with the given filename.
     * Before saving the board, print out the number of rows on the first line, the number of columns on the second line,
     * and the actual block of text starting from the third line.
     *
     * Throw IllegalArgumentException if:
     * - The number of rows doesn't equal to the number of columns, with message
     *   "Number of rows should equal to the number of columns!"
     * - The number of rows/columns is smaller than 3, with message
     *   "Number of rows/column should be greater than or equal to 3."
     *
     * Example input board:
     *                     {{'K', 'A', 'K'},
     *                     {'.', '.', '.'},
     *                     {'K', 'A', 'K'}}
     * Output Example:
     * 3
     * 3
     * KAK
     * ...
     * KAK
     *
     * @param filename The filename to save the block of text. Assume it includes the .txt extension.
     * @param board      The grid of chars to save
     * @throws IllegalArgumentException if number of rows/column is invalid
     */
    public void createBoard(String filename, char[][] board) throws IllegalArgumentException {
        if (board.length != board[0].length)
            throw new IllegalArgumentException("Number of rows should equal to the number of columns!");
        if (board.length < 3 || board[0].length < 3)
            throw new IllegalArgumentException("Number of rows/column should be greater than or equal to 3.");
        try (FileOutputStream fos = new FileOutputStream(filename))
        {
            for (int i = 0; i < board.length; i++)
            {
                for (int j = 0; j < board[i].length; j++)
                {
                    fos.write(board[i][j] == '\0' ? '.' : board[i][j]);
                    fos.write(' ');
                }
                fos.write('\n');
            }
        }
        catch (Exception e)
        {
            e.printStackTrace();
        }
    }

    /**
     * Loads a board into a 2d char array. Note that the format of the text file will be the same
     * format as we saved it: first row  has an integer representing how many rows, second row has an integer
     * representing how many columns, and the rest of the rows is the actual block of text.
     *
     * Throw BadConfigException if:
     * - The number of rows < minRows or The number of columns < minCols
     *
     * Example input string
     * 3
     * 3
     * KAK
     * ...
     * KAK
     *
     * Expected return array:
     *                     {{'K', 'A', 'K'},
     *                     {'.', '.', '.'},
     *                     {'K', 'A', 'K'}}
     *
     * @param filename The filename of the board
     * @param minRows  the minimum number of rows this board must have. If violated, throw BadboardException
     * @param minCols  the minimum number of cols this board must have. If violated, throw BadboardException
     * @return The 2d char representing the board.
     * @throws BadConfigException if the minRows or minCols constraints are violated.
     */
    public char[][] loadBoard(String filename, int minRows, int minCols) throws BadConfigException {
        char[][] board = new char[minRows][];
        for (int i = 0; i < minCols; i++)
        {
            board[i] = new char[minCols];
        }
        try (FileInputStream fis = new FileInputStream(filename))
        {

        }
        catch (Exception e)
        {
            e.printStackTrace();
            return null;
        }
        return board;
    }
}
