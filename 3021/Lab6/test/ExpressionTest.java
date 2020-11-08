import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import value.IntNumber;

import java.io.IOException;
import java.math.BigInteger;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

public class ExpressionTest {
    private Parser parser = new Parser();

    @Test
    void testParser() throws IOException {
        assertEquals("100013", parser.parse("\t  \t  +100013\t\t ").toString());
        assertEquals("-123", parser.parse("\t  \t-123  \t").toString());
        assertEquals("(+ (- (* (/ 100 -2) 3) 4) 5)", parser.parse("(+ 	(- (* 	(/ 100 -2) 3) 4)     5)").toString());
        assertEquals(new IntNumber("21"), parser.parse("(+ (- (* 1 2) (/ 10 2)) 24)").eval());
    }

    @Test
    void testAdd() throws IOException {
        assertEquals(new IntNumber("100"), parser.parse("(+ 100)").eval());
        assertEquals(new IntNumber("6"), parser.parse("(+ 1 2 3)").eval());
        assertEquals(new IntNumber("-10"), parser.parse("(+ 10 -20)").eval());
    }

    @Test
    void testSub() throws IOException {
        assertEquals(new IntNumber("-11"), parser.parse("(- 11)").eval());
        assertEquals(new IntNumber("78"), parser.parse("(- 100 22)").eval());
        assertEquals(new IntNumber("7"), parser.parse("(- 10 2 -4 5)").eval());
    }

    @Test
    void testMult() throws IOException {
        assertEquals(new IntNumber("10"), parser.parse("(* 10)").eval());
        assertEquals(new IntNumber("12"), parser.parse("(* 3 4)").eval());
        assertEquals(new IntNumber("-60"), parser.parse("(* 3 -4 5)").eval());
    }

    @Test
    void testDiv() throws IOException {
        assertEquals(new IntNumber("3"), parser.parse("(/ 10 3)").eval());
        assertEquals(new IntNumber(BigInteger.ONE), parser.parse("(/ 1)").eval());
        assertEquals(new IntNumber(BigInteger.ZERO), parser.parse("(/ 2)").eval());
        assertEquals(new IntNumber("-5"), parser.parse("(/ 100 -2 10)").eval());
    }

    @Test
    public void testDivByZero() {
        assertThrows(ArithmeticException.class, () -> {
            parser.parse("(/ 100 -2 0 3)").eval();
        });
    }

    @Test
    void testExp() throws IOException {
        assertEquals(new IntNumber("1024"), parser.parse("(^ 2 10)").eval());
        assertEquals(new IntNumber("81"), parser.parse("(^ -3 4)").eval());
    }
}
