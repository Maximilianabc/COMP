package comp3111.popnames;

import org.junit.Test;

import static org.junit.Assert.*;

public class NamesRecommendationTest extends TopNamesForBirth
{
    @Test
    public void testInput() {
        assertEquals(DataValidation.invalidName, NamesRecommendation.validation("peter", "Mary", "1960", "1960", "1980").get());
        assertEquals(DataValidation.invalidName, NamesRecommendation.validation("Peter", "mary", "1960", "1960", "1980").get());
        assertEquals(DataValidation.invalidYearString, NamesRecommendation.validation("Peter", "Mary", "abc", "1960", "1980").get());
        assertEquals(DataValidation.invalidYearString, NamesRecommendation.validation("Peter", "Mary", "1960", "abc", "1980").get());
        assertEquals(DataValidation.invalidYearString, NamesRecommendation.validation("Peter", "Mary", "1960", "1960", "abc").get());
        assertEquals(DataValidation.yearNotInRange, NamesRecommendation.validation("Peter", "Mary", "1879", "1960", "1980").get());
        assertEquals(DataValidation.yearNotInRange, NamesRecommendation.validation("Peter", "Mary", "1960", "1879", "1980").get());
        assertEquals(DataValidation.yearNotInRange, NamesRecommendation.validation("Peter", "Mary", "1960", "1960", "1879").get());
        assertTrue(NamesRecommendation.validation("Peter", "Mary", "1960", "1960", "1980").isEmpty());
    }

    @Test
    public void testRecommend() {
        assertEquals("Recommended boy's name: Jeremy\nRecommended girl's name: Raven", NamesRecommendation.recommend("Lawrence", "Irene", "1964", "1960", "1998"));
        assertEquals("Recommended boy's name: Connor\nRecommended girl's name: Teagan", NamesRecommendation.recommend("Lawrence", "Irene", "1964", "1960", ""));
    }
}