package comp3111.popnames;

import junit.framework.AssertionFailedError;
import org.junit.Test;

import java.io.IOException;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.stream.Stream;

import static org.junit.Assert.*;

public class TopNamesForBirthTest extends TopNamesForBirth
{
    @Test
    public void testInput(){
        assertEquals(DataValidation.invalidYearString, validation("abcd", "abcd", "123").get());
        assertEquals(DataValidation.invalidIntString, validation("1880", "1881", "abcd").get());
        assertEquals(DataValidation.yearNotInRange, validation("1879", "2010", "10").get());
        assertEquals(DataValidation.invalidPeriod, validation("2010", "2009", "20").get());
        assertTrue(validation("1900", "1950", "10").isEmpty());
    }

    @Test
    public void testCompute()
    throws IOException
    {
        assertFalse(Compute("1918", "1945", "10", Gender.FEMALE).isEmpty());
    }

    @Test
    public void testBreakdownMethods() {
        final int TOP = 20;
        try {
            addRecords(1900, 1909, TOP, Gender.MALE);
            assertEquals(10, years.keySet().size());
            for (List<String> year : years.values()) {
                assertEquals(20, year.size());
            }
        } catch (IOException e) {
            throw new AssertionFailedError(e.getMessage());
        }

        loadIntoRanks(TOP);
        assertEquals(TOP, ranks.keySet().size());
        for (List<String> rank : ranks.values()) {
            assertEquals(10, rank.size());
        }

        Optional<Map.Entry<String, Long>> f = getMostFrequent();
        assertTrue(f.isPresent());
        assertEquals("John", f.get().getKey());
        assertEquals(10, (long)f.get().getValue());

        storeMaxLengths();
        assertEquals(TOP, max_lengths.size());
        assertArrayEquals(Stream.of(4, 7, 5, 6, 7, 7, 7, 5, 6, 6, 6, 6, 6, 6, 6, 6, 8, 8, 8, 7).toArray(), max_lengths.toArray());

        String s = getSummary(1900, 1909, f, Gender.MALE);
        assertEquals("Over the period 1900 to 1909, John for male has hold the top spot most often for a total of 10 times.\n\n", s);
    }
}