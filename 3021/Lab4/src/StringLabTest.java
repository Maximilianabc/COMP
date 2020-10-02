import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;
import static org.junit.jupiter.api.Assertions.assertTrue;

class StringLabTest {
    private StringLab tester = new StringLab();

    @Test
    void testReverseString() {
        assertEquals("cba", tester.reverseString("abc"));
        assertEquals("", tester.reverseString(""));
        assertEquals("_1234_", tester.reverseString("_4321_"));
    }

    @Test
    void testCapitalizeAndMakeLowercase() {
        assertEquals("HELLOworld", tester.capitalizeAndMakeLowercase("hElLoWoRlD", 5));
        assertEquals("", tester.capitalizeAndMakeLowercase("", 20));
        assertEquals("foobar", tester.capitalizeAndMakeLowercase("foobar", 0));
        assertEquals("foobar", tester.capitalizeAndMakeLowercase("FOOBAR", -10));
        assertEquals("FOOBAr", tester.capitalizeAndMakeLowercase("fOoBaR", 5));
        assertEquals("FOOBAR", tester.capitalizeAndMakeLowercase("FOoBAR", 20));
    }

    @Test
    void testCountVowels() {
        assertEquals(0, tester.countVowels("hllwrld"));
        assertEquals(0, tester.countVowels(""));
        assertEquals(5, tester.countVowels("aeiouy"));
        assertEquals(2, tester.countVowels("abde"));
    }

    @Test
    void testRemoveLetter() {
        assertEquals("", tester.removeLetter("", 'a'));
        assertEquals("hellwrld", tester.removeLetter("helloworld", 'o'));
    }

    @Test
    void testIsPalindrome() {
        assertTrue(tester.isPalindrome(""));
        assertTrue(tester.isPalindrome("123321"));
        assertTrue(tester.isPalindrome("racecar"));

        assertFalse(tester.isPalindrome("helloworld"));
        assertFalse(tester.isPalindrome("racecar1"));
    }


}