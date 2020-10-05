package castle.comp3021.assignment;

import castle.comp3021.assignment.mock.MockPlayer;
import castle.comp3021.assignment.piece.Archer;
import castle.comp3021.assignment.piece.Knight;
import castle.comp3021.assignment.player.ConsolePlayer;
import castle.comp3021.assignment.protocol.*;
import castle.comp3021.assignment.util.Compares;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.lang.reflect.*;
import java.util.Arrays;

import static org.junit.jupiter.api.Assertions.*;

/**
 * Put your additional JUnit5 tests for Bonus Task 2 in this class.
 */
public class AdditionalTests
{
    // player1's pieces are in capital letters, while that of player2's are in small letter
    private Configuration config;
    private MockPlayer player1;
    private MockPlayer player2;

    //region place names
    Place Err = new Place(-1, -1);
    Place A1 = new Place(0, 0);
    Place A2 = new Place(0, 1);
    Place A3 = new Place(0, 2);
    Place A4 = new Place(0, 3);
    Place A5 = new Place(0, 4);
    Place A6 = new Place(0, 5);
    Place A7 = new Place(0, 6);
    Place A8 = new Place(0, 7);
    Place A9 = new Place(0, 8);
    Place B1 = new Place(1, 0);
    Place B2 = new Place(1, 1);
    Place B3 = new Place(1, 2);
    Place B4 = new Place(1, 3);
    Place B5 = new Place(1, 4);
    Place B6 = new Place(1, 5);
    Place B7 = new Place(1, 6);
    Place B8 = new Place(1, 7);
    Place B9 = new Place(1, 8);
    Place C1 = new Place(2, 0);
    Place C2 = new Place(2, 1);
    Place C3 = new Place(2, 2);
    Place C4 = new Place(2, 3);
    Place C5 = new Place(2, 4);
    Place C6 = new Place(2, 5);
    Place C7 = new Place(2, 6);
    Place C8 = new Place(2, 7);
    Place C9 = new Place(2, 8);
    Place D1 = new Place(3, 0);
    Place D2 = new Place(3, 1);
    Place D3 = new Place(3, 2);
    Place D4 = new Place(3, 3);
    Place D5 = new Place(3, 4);
    Place D6 = new Place(3, 5);
    Place D7 = new Place(3, 6);
    Place D8 = new Place(3, 7);
    Place D9 = new Place(3, 8);
    Place E1 = new Place(4, 0);
    Place E2 = new Place(4, 1);
    Place E3 = new Place(4, 2);
    Place E4 = new Place(4, 3);
    Place E5 = new Place(4, 4);
    Place E6 = new Place(4, 5);
    Place E7 = new Place(4, 6);
    Place E8 = new Place(4, 7);
    Place E9 = new Place(4, 8);
    Place F1 = new Place(5, 0);
    Place F2 = new Place(5, 1);
    Place F3 = new Place(5, 2);
    Place F4 = new Place(5, 3);
    Place F5 = new Place(5, 4);
    Place F6 = new Place(5, 5);
    Place F7 = new Place(5, 6);
    Place F8 = new Place(5, 7);
    Place F9 = new Place(5, 8);
    Place G1 = new Place(6, 0);
    Place G2 = new Place(6, 1);
    Place G3 = new Place(6, 2);
    Place G4 = new Place(6, 3);
    Place G5 = new Place(6, 4);
    Place G6 = new Place(6, 5);
    Place G7 = new Place(6, 6);
    Place G8 = new Place(6, 7);
    Place G9 = new Place(6, 8);
    Place H1 = new Place(7, 0);
    Place H2 = new Place(7, 1);
    Place H3 = new Place(7, 2);
    Place H4 = new Place(7, 3);
    Place H5 = new Place(7, 4);
    Place H6 = new Place(7, 5);
    Place H7 = new Place(7, 6);
    Place H8 = new Place(7, 7);
    Place H9 = new Place(7, 8);
    Place I1 = new Place(8, 0);
    Place I2 = new Place(8, 1);
    Place I3 = new Place(8, 2);
    Place I4 = new Place(8, 3);
    Place I5 = new Place(8, 4);
    Place I6 = new Place(8, 5);
    Place I7 = new Place(8, 6);
    Place I8 = new Place(8, 7);
    Place I9 = new Place(8, 8);
    //endregion

    @BeforeEach
    public void setUpGame()
    {
        player1 = new MockPlayer(Color.PURPLE);
        player2 = new MockPlayer(Color.YELLOW);
    }

    @Test
    public void testProtectionCannotCapture()
    {
        config = new Configuration(5, new Player[] { player1, player2 }, 5);

        /*
         * 5 | . . . . . |
         * 4 | . . . . . |
         * 3 | . k x . . |
         * 2 | . . . . . |
         * 1 | K . . . . |
         *     A B C D E
         */
        Knight k1 = new Knight(player1);
        config.addInitialPiece(k1, A1);
        config.addInitialPiece(new Knight(player2), B3);
        Game game = new JesonMor(config);
        Move[] moves = k1.getAvailableMoves(game, A1);
        Move[] expected = new Move[] { new Move(A1, C2) };
        assertTrue(Compares.areContentsEqual(moves, expected));
    }

    @Test
    public void testProtectionCannotWinByLeaveCentral()
    {
        config = new Configuration(5, new Player[] { player1, player2 }, 5);

        /*
         * 5 | . . . . . |
         * 4 | . . . . . |
         * 3 | . k x . . |
         * 2 | . . . . . |
         * 1 | . K . . . |
         *     A B C D E
         */
        Knight k1 = new Knight(player1);
        config.addInitialPiece(k1, B1);
        config.addInitialPiece(new Knight(player2), B3);
        Game game = new JesonMor(config);

        /*
         * 5 | . . . . . |
         * 4 | . . . . . |
         * 3 | . k K . . |
         * 2 | . . . . . |
         * 1 | . . . . . |
         *     A B C D E
         */
        game.movePiece(new Move(B1, C3));
        /*
         * 5 | . . . . . |
         * 4 | . . . . . |
         * 3 | . . K . . |
         * 2 | . . . . . |
         * 1 | k . . . . |
         *     A B C D E
         */
        game.movePiece(new Move(B3, A1));
        /*
         * 5 | . . . . . |
         * 4 | . . . . . |
         * 3 | . . x . . |
         * 2 | . . . . . |
         * 1 | k K . . . |
         *     A B C D E
         */
        game.movePiece(new Move(C3, B1));
        assertEquals(3, game.getNumMoves());
        assertTrue(game.getNumMoves() < game.getConfiguration().getNumMovesProtection());
        assertNull(game.getWinner(player1, k1, new Move(C3, B1)));
    }

    @Test
    public void testWinByCapturingAllPieces()
    {
        config = new Configuration(5, new Player[] { player1, player2 });

        /*
         * 5 | k . k . . |
         * 4 | . . . . . |
         * 3 | . . x . . |
         * 2 | . . . . . |
         * 1 | K . K . . |
         *     A B C D E
         */
        config.addInitialPiece(new Knight(player1), A1);
        config.addInitialPiece(new Knight(player1), C1);
        config.addInitialPiece(new Knight(player2), A5);
        config.addInitialPiece(new Knight(player2), C5);
        Move[] p1moves = new Move[]
        {
            new Move(A1, B3),
            new Move(C1, B3)
        };
        Move[] p2moves = new Move[]
        {
            new Move(A5, B3),
            new Move(C5, B3)
        };
        player1.setNextMoves(p1moves);
        player2.setNextMoves(p2moves);

        Game game = new JesonMor(config);
        Player winner = game.start();

        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Piece p = game.getPiece(i, j);
                if (p != null && p.getPlayer() == player1)
                    count++;
            }
        }
        assertEquals(0, count); // player1 has no pieces left
        assertEquals(player2, winner);
    }

    @Test
    public void testKnightCapture()
    {
        config = new Configuration(5, new Player[] { player1, player2 });

        /*
         * 5 | . . . . . |
         * 4 | . . . . . |
         * 3 | . K x . . |
         * 2 | . . k . . |
         * 1 | K . . . . |
         *     A B C D E
         */
        Knight k1 = new Knight(player1);
        config.addInitialPiece(k1, A1);
        config.addInitialPiece(new Knight(player1), B3);
        config.addInitialPiece(new Knight(player2), C2);
        Game game = new JesonMor(config);
        Move cap = new Move(A1, C2);
        Move[] moves = k1.getAvailableMoves(game, A1);
        Move[] expected = new Move[] { cap };
        assertTrue(Compares.areContentsEqual(expected, moves));

        /*
         * 5 | . . . . . |
         * 4 | . . . . . |
         * 3 | . K x . . |
         * 2 | . . K . . |
         * 1 | . . . . . |
         *     A B C D E
         */
        game.movePiece(cap);
        assertEquals(k1, game.getPiece(2, 1));
    }

    @Test
    public void testKnightBlock()
    {
        config = new Configuration(7, new Player[] { player1, player2 });

        /*
         * 7 | . . . . . . . |
         * 6 | . . . . . . . |
         * 5 | . . . . . . . |
         * 4 | . . K x . . . |
         * 3 | . K k K . . . |
         * 2 | . . K . . . . |
         * 1 | . . . . . . . |
         *     A B C D E F G
         */
        Knight k5 = new Knight(player2);
        config.addInitialPiece(new Knight(player1), B3);
        config.addInitialPiece(new Knight(player1), C4);
        config.addInitialPiece(new Knight(player1), D3);
        config.addInitialPiece(new Knight(player1), C2);
        config.addInitialPiece(k5, C3);
        Game game = new JesonMor(config);
        Move[] moves = k5.getAvailableMoves(game, C3);
        assertEquals(0, moves.length);
    }

    @Test
    public void testArcherCapture()
    {
        config = new Configuration(5, new Player[] { player1, player2 });

        /*
         * 5 | . k . . . |
         * 4 | . . . . . |
         * 3 | . K x . . |
         * 2 | . . . . . |
         * 1 | . A . . . |
         *     A B C D E
         */
        Archer a = new Archer(player1);
        config.addInitialPiece(new Knight(player1), B3);
        config.addInitialPiece(new Knight(player2), B5);
        config.addInitialPiece(a, B1);
        Game game = new JesonMor(config);
        Move cap = new Move(B1, B5);
        assertTrue(Arrays.asList(a.getAvailableMoves(game, B1)).contains(cap));

        /*
         * 5 | . A . . . |
         * 4 | . . . . . |
         * 3 | . K x . . |
         * 2 | . . . . . |
         * 1 | . . . . . |
         *     A B C D E
         */
        game.movePiece(cap);
        assertEquals(a, game.getPiece(B5));
    }

    @Test
    public void testArcherMoves()
    {
        config = new Configuration(9, new Player[] { player1, player2 });

        /*
         * 9 | . . . . . . . . . |
         * 8 | . . . . . . . . . |
         * 7 | . . . . . . . . . |
         * 6 | . . . . . . . . . |
         * 5 | . . . . x . . . . |
         * 4 | . . . A . . . . . |
         * 3 | . . . . . . . . . |
         * 2 | . . . . . . . . . |
         * 1 | . . . . . . . . . |
         *     A B C D E F G H I
         */
        Archer a1 = new Archer(player1);
        config.addInitialPiece(a1, D4);
        Game game = new JesonMor(config);
        Move[] moves = new Move[]
        {
            new Move(D4, D1),
            new Move(D4, D2),
            new Move(D4, D3),
            new Move(D4, D5),
            new Move(D4, D6),
            new Move(D4, D7),
            new Move(D4, D8),
            new Move(D4, D9),
            new Move(D4, A4),
            new Move(D4, B4),
            new Move(D4, C4),
            new Move(D4, E4),
            new Move(D4, F4),
            new Move(D4, G4),
            new Move(D4, H4),
            new Move(D4, I4)
        };
        assertTrue(Compares.areContentsEqual(moves, a1.getAvailableMoves(game, D4)));

        /*
         * 9 | . . . . . . . . . |
         * 8 | . . . . . . . . . |
         * 7 | . . . . . . . . . |
         * 6 | . . . . . . . . . |
         * 5 | . . . . x . . . . |
         * 4 | . . A A . . . . . |
         * 3 | . . . . . . . . . |
         * 2 | . . . . . . . . . |
         * 1 | . . . . . . . . . |
         *     A B C D E F G H I
         */
        Archer a2 = new Archer(player1);
        config.addInitialPiece(a2, C4); // blocking left side of a1
        game = new JesonMor(config);
        moves = new Move[]
        {
            new Move(D4, D1),
            new Move(D4, D2),
            new Move(D4, D3),
            new Move(D4, D5),
            new Move(D4, D6),
            new Move(D4, D7),
            new Move(D4, D8),
            new Move(D4, D9),
            new Move(D4, E4),
            new Move(D4, F4),
            new Move(D4, G4),
            new Move(D4, H4),
            new Move(D4, I4)
        };
        assertTrue(Compares.areContentsEqual(moves, a1.getAvailableMoves(game, D4)));

        /*
         * 9 | . . . . . . . . . |
         * 8 | . . . . . . . . . |
         * 7 | . . . . . . . . . |
         * 6 | . . . . . . . . . |
         * 5 | . . . . x . . . . |
         * 4 | A . A A . . . . . |
         * 3 | . . . . . . . . . |
         * 2 | . . . . . . . . . |
         * 1 | . . . . . . . . . |
         *     A B C D E F G H I
         */
        Archer a3 = new Archer(player1);
        config.addInitialPiece(a3, A4); // own piece cannot capture
        game = new JesonMor(config);
        assertTrue(Compares.areContentsEqual(moves, a1.getAvailableMoves(game, D4)));

        /*
         * 9 | . . . . . . . . . |
         * 8 | . . . . . . . . . |
         * 7 | . . . . . . . . . |
         * 6 | . . . . . . . . . |
         * 5 | . . . . x . . . . |
         * 4 | A a A A . . . . . |
         * 3 | . . . . . . . . . |
         * 2 | . . . . . . . . . |
         * 1 | . . . . . . . . . |
         *     A B C D E F G H I
         */
        Archer a4 = new Archer(player2);
        config.addInitialPiece(a4, B4); // enemy piece can capture
        game = new JesonMor(config);
        moves = new Move[]
        {
            new Move(D4, D1),
            new Move(D4, D2),
            new Move(D4, D3),
            new Move(D4, D5),
            new Move(D4, D6),
            new Move(D4, D7),
            new Move(D4, D8),
            new Move(D4, D9),
            new Move(D4, B4), // enemy piece
            new Move(D4, E4),
            new Move(D4, F4),
            new Move(D4, G4),
            new Move(D4, H4),
            new Move(D4, I4)
        };
        assertTrue(Compares.areContentsEqual(moves, a1.getAvailableMoves(game, D4)));

        /*
         * 9 | . . . . . . . . . |
         * 8 | . . . . . . . . . |
         * 7 | . . . . . . . . . |
         * 6 | . . . . . . . . . |
         * 5 | . . . a x . . . . |
         * 4 | A a A A a . . . . |
         * 3 | . . . a . . . . . |
         * 2 | . . . . . . . . . |
         * 1 | . . . . . . . . . |
         *     A B C D E F G H I
         */
        Archer a5 = new Archer(player2);
        Archer a6 = new Archer(player2);
        Archer a7 = new Archer(player2);
        //blocking up, down, right side of a1
        config.addInitialPiece(a5, D5);
        config.addInitialPiece(a6, E4);
        config.addInitialPiece(a7, D3);
        game = new JesonMor(config);
        moves = new Move[]
        {
            new Move(D4, B4)
        };
        assertTrue(Compares.areContentsEqual(moves, a1.getAvailableMoves(game, D4)));

        /*
         * 9 | . . . a . . . . . |
         * 8 | . . . . . . . . . |
         * 7 | . . . . . . . . . |
         * 6 | . . . . . . . . . |
         * 5 | . . . a x . . . . |
         * 4 | A a A A a . . a . |
         * 3 | . . . a . . . . . |
         * 2 | . . . . . . . . . |
         * 1 | . . . a . . . . . |
         *     A B C D E F G H I
         */
        Archer a8 = new Archer(player2);
        Archer a9 = new Archer(player2);
        Archer a10 = new Archer(player2);
        config.addInitialPiece(a8, D9);
        config.addInitialPiece(a9, H4);
        config.addInitialPiece(a10, D1);
        game = new JesonMor(config);
        moves = new Move[]
        {
            new Move(D4, B4),
            new Move(D4, H4),
            new Move(D4, D1),
            new Move(D4, D9)
        };
        assertTrue(Compares.areContentsEqual(moves, a1.getAvailableMoves(game, D4)));

        /*
         * 9 | . . . a . . . . . |
         * 8 | . . . . . . . . . |
         * 7 | . . . . . . . . . |
         * 6 | . . . A . . . . . |
         * 5 | . . . a x . . . . |
         * 4 | A a A A a A . a . |
         * 3 | . . . a . . . . . |
         * 2 | . . . A . . . . . |
         * 1 | . . . a . . . . . |
         *     A B C D E F G H I
         */
        Archer a11 = new Archer(player1);
        Archer a12 = new Archer(player1);
        Archer a13 = new Archer(player1);
        config.addInitialPiece(a11, D6);
        config.addInitialPiece(a12, F4);
        config.addInitialPiece(a13, D2);
        game = new JesonMor(config);

        // a1
        moves = new Move[]
        {
            new Move(D4, B4)
        };
        assertTrue(Compares.areContentsEqual(moves, a1.getAvailableMoves(game, D4)));

        // a2
        moves = new Move[]
        {
            new Move(C4, C1),
            new Move(C4, C2),
            new Move(C4, C3),
            new Move(C4, C5),
            new Move(C4, C6),
            new Move(C4, C7),
            new Move(C4, C8),
            new Move(C4, C9),
            new Move(C4, E4)
        };
        assertTrue(Compares.areContentsEqual(moves, a2.getAvailableMoves(game, C4)));

        // a3
        moves = new Move[]
        {
            new Move(A4, A1),
            new Move(A4, A2),
            new Move(A4, A3),
            new Move(A4, A5),
            new Move(A4, A6),
            new Move(A4, A7),
            new Move(A4, A8),
            new Move(A4, A9)
        };
        assertTrue(Compares.areContentsEqual(moves, a3.getAvailableMoves(game, A4)));

        // a4
        SwitchPlayer(game, 2);
        moves = new Move[]
        {
            new Move(B4, B1),
            new Move(B4, B2),
            new Move(B4, B3),
            new Move(B4, B5),
            new Move(B4, B6),
            new Move(B4, B7),
            new Move(B4, B8),
            new Move(B4, B9),
            new Move(B4, D4)
        };
        assertTrue(Compares.areContentsEqual(moves, a4.getAvailableMoves(game, B4)));

        // a5
        moves = new Move[]
        {
            new Move(D5, A5),
            new Move(D5, B5),
            new Move(D5, C5),
            new Move(D5, E5),
            new Move(D5, F5),
            new Move(D5, G5),
            new Move(D5, H5),
            new Move(D5, I5)
        };
        assertTrue(Compares.areContentsEqual(moves, a5.getAvailableMoves(game, D5)));

        // a6
        moves = new Move[]
        {
            new Move(E4, E1),
            new Move(E4, E2),
            new Move(E4, E3),
            new Move(E4, E5),
            new Move(E4, E6),
            new Move(E4, E7),
            new Move(E4, E8),
            new Move(E4, E9),
            new Move(E4, C4)
        };
        assertTrue(Compares.areContentsEqual(moves, a6.getAvailableMoves(game, E4)));

        // a7
        moves = new Move[]
        {
            new Move(D3, A3),
            new Move(D3, B3),
            new Move(D3, C3),
            new Move(D3, E3),
            new Move(D3, F3),
            new Move(D3, G3),
            new Move(D3, H3),
            new Move(D3, I3)
        };
        assertTrue(Compares.areContentsEqual(moves, a7.getAvailableMoves(game, D3)));

        // a8
        moves = new Move[]
        {
            new Move(D9, A9),
            new Move(D9, B9),
            new Move(D9, C9),
            new Move(D9, E9),
            new Move(D9, F9),
            new Move(D9, G9),
            new Move(D9, H9),
            new Move(D9, I9),
            new Move(D9, D8),
            new Move(D9, D7)
        };
        assertTrue(Compares.areContentsEqual(moves, a8.getAvailableMoves(game, D9)));

        // a9
        moves = new Move[]
        {
            new Move(H4, H1),
            new Move(H4, H2),
            new Move(H4, H3),
            new Move(H4, H5),
            new Move(H4, H6),
            new Move(H4, H7),
            new Move(H4, H8),
            new Move(H4, H9),
            new Move(H4, G4),
            new Move(H4, I4),
        };
        assertTrue(Compares.areContentsEqual(moves, a9.getAvailableMoves(game, H4)));

        // a10
        moves = new Move[]
        {
            new Move(D1, A1),
            new Move(D1, B1),
            new Move(D1, C1),
            new Move(D1, E1),
            new Move(D1, F1),
            new Move(D1, G1),
            new Move(D1, H1),
            new Move(D1, I1)
        };
        assertTrue(Compares.areContentsEqual(moves, a10.getAvailableMoves(game, D1)));

        // a11
        SwitchPlayer(game, 1);
        moves = new Move[]
        {
            new Move(D6, A6),
            new Move(D6, B6),
            new Move(D6, C6),
            new Move(D6, E6),
            new Move(D6, F6),
            new Move(D6, G6),
            new Move(D6, H6),
            new Move(D6, I6),
            new Move(D6, D7),
            new Move(D6, D8)
        };
        assertTrue(Compares.areContentsEqual(moves, a11.getAvailableMoves(game, D6)));

        // a12
        moves = new Move[]
        {
            new Move(F4, F1),
            new Move(F4, F2),
            new Move(F4, F3),
            new Move(F4, F5),
            new Move(F4, F6),
            new Move(F4, F7),
            new Move(F4, F8),
            new Move(F4, F9),
            new Move(F4, G4)
        };
        assertTrue(Compares.areContentsEqual(moves, a12.getAvailableMoves(game, F4)));

        // a13
        moves = new Move[]
        {
            new Move(D2, A2),
            new Move(D2, B2),
            new Move(D2, C2),
            new Move(D2, E2),
            new Move(D2, F2),
            new Move(D2, G2),
            new Move(D2, H2),
            new Move(D2, I2)
        };
        assertTrue(Compares.areContentsEqual(moves, a13.getAvailableMoves(game, D2)));

    }

    @Test
    public void testTieBreakSameScoreCurrentPlayerWins()
    {
        config = new Configuration(5, new Player[] { player1, player2 });

        /*
         * 5 | K A K A K |
         * 4 | k a k a k |
         * 3 | . . x . . |
         * 2 | . . . . . |
         * 1 | . . . . . |
         *     A B C D E
         */
        config.addInitialPiece(new Knight(player2), A4);
        config.addInitialPiece(new Knight(player2), C4);
        config.addInitialPiece(new Knight(player2), E4);
        config.addInitialPiece(new Knight(player1), A5);
        config.addInitialPiece(new Knight(player1), C5);
        config.addInitialPiece(new Knight(player1), E5);
        config.addInitialPiece(new Archer(player2), B4);
        config.addInitialPiece(new Archer(player2), D4);
        config.addInitialPiece(new Archer(player1), B5);
        config.addInitialPiece(new Archer(player1), D5);
        player1.setScore(123);
        player2.setScore(123);

        Game game = new JesonMor(config);
        Player winner = game.start();
        assertEquals(player1, game.getCurrentPlayer());
        assertEquals(player1, winner);
    }

    @Test
    public void testInput()
    {
        String[] invalid = new String[]
        {
            "abcdef\r\n", // random string
            "ab->cd\r\n", // no numbers
            "05->13\r\n", // no letters
            "A5->B3\r\n", // capital
            "Ba5->b3C\r\n", // add sth before and at the end
            "a5->b7\r\n", // out of boundary
            "a5->a6\r\n" // not a valid move
        };

        ConsolePlayer player3 = new ConsolePlayer("Test", Color.PURPLE);
        config = new Configuration(5, new Player[] { player3, player2 });
        config.addInitialPiece(new Knight(player3), A5);
        Game game = new JesonMor(config);


        for (String s : invalid)
        {
            System.setIn(new ByteArrayInputStream(s.getBytes()));
            ByteArrayOutputStream out = new ByteArrayOutputStream();
            System.setOut(new PrintStream(out));

            Move m = player3.nextMove(game, game.getAvailableMoves(player3));
            assertEquals(new Move(Err, Err), m);
            assertEquals("Invalid move.\r\n", out.toString());
        }

        // what if a larger board
        String[] valid = new String[]
        {
            "c23->b25\r\n",
            "c23->d25\r\n",
            "c23->b21\r\n",
            "c23->d21\r\n",
            "c23->a24\r\n",
            "c23->e24\r\n",
            "c23->a22\r\n",
            "c23->e22\r\n",
        };

        player3 = new ConsolePlayer("Test", Color.PURPLE);
        config = new Configuration(25, new Player[] { player3, player2 });
        config.addInitialPiece(new Knight(player3), 2, 22);
        game = new JesonMor(config);

        for (String s : valid)
        {
            System.setIn(new ByteArrayInputStream(s.getBytes()));
            Move m = player3.nextMove(game, game.getAvailableMoves(player3));
            assertNotEquals(new Move(Err, Err), m);
        }

        System.setIn(System.in);
        System.setOut(System.out);
    }

    @Test
    public void testMain()
    {
        // arg length < 2
        assertThrows(IllegalArgumentException.class, () -> Main.main(new String[] {"3"}),
                    "two integer arguments are required specifying size of gameboard and number of moves with capturing protection ");
        // arg[0] is not int
        assertThrows(IllegalArgumentException.class, () -> Main.main(new String[] {"abc", "5"}),
                    "the first argument is not a number");
        // arg[1] is not int
        assertThrows(IllegalArgumentException.class, () -> Main.main(new String[] {"5", "abc"}),
                    "the second argument is not a number");
    }

    private void SwitchPlayer(Game game, int player)
    {
        try
        {
            Field curPlayer = Game.class.getDeclaredField("currentPlayer");
            curPlayer.setAccessible(true);
            curPlayer.set(game, player == 2 ? player2 : player1);
        }
        catch (NoSuchFieldException | IllegalAccessException e)
        {
            e.printStackTrace();
        }
    }
}
