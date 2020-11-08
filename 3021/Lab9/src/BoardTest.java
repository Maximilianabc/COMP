
import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.Test;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Arrays;

import static org.junit.jupiter.api.Assertions.*;

class BoardTest {
    private final char[][] invalidBoard =
            {{'K', 'A', 'K'},
                    {'K', 'A', 'K'}};

    private final char[][] invalidBoard2 =
            {{'K', 'A'},
                    {'K', 'A'}};

    private final char[][] goodBoard =
            {{'K', 'A', 'K'},
                    {'.', '.', '.'},
                    {'K', 'A', 'K'}};

    private static final String FILENAME = "test1.txt";
    private static final String FILENAME2 = "test2.txt";
    private static final String FILENAME3 = "test3.txt";

    private static final Board board = new Board();

    @AfterAll
    public static void cleanupFiles() {
        try {
            Files.deleteIfExists(Paths.get(new File(".").getAbsolutePath() + File.separator + FILENAME));
            Files.deleteIfExists(Paths.get(new File(".").getAbsolutePath() + File.separator + FILENAME2));
            Files.deleteIfExists(Paths.get(new File(".").getAbsolutePath() + File.separator + FILENAME3));

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Test
    public void testSaveBoardSuccess() {
        board.createBoard(FILENAME, goodBoard);
    }

    @Test
    public void testSaveBoardInvalidNotEqual() {
        assertThrows(IllegalArgumentException.class, () -> board.createBoard(FILENAME, invalidBoard));
    }

    @Test
    public void testSaveBoardInvalidSize() {
        assertThrows(IllegalArgumentException.class, () -> board.createBoard(FILENAME, invalidBoard2));
    }

    @Test
    public void testLoadBoardSuccess() throws BadConfigException {
        board.createBoard(FILENAME2, goodBoard);
        var ans = board.loadBoard(FILENAME2, 2, 2);
        assertTrue(Arrays.deepEquals(ans, goodBoard));
    }

    @Test
    public void testLoadBadBoard() {
        board.createBoard(FILENAME3, goodBoard);
        assertThrows(BadConfigException.class, () -> board.loadBoard(FILENAME3, 10, 10));
    }
}