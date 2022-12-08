package comp3111.popnames;

import org.junit.Test;

//import jdk.jfr.Timestamp;
//error when compiling

import java.util.ArrayList;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertTrue;

public class DataParserTest {
    @Test
    public void testGetRankNotFound() {
        // DataParser a = new DataParser();
        // int i = a.getRank(2016, "XXX", Gender.convertFromString("M"));
        int i = DataParser.getRank(2016, "XXX", Gender.convertFromString("M"));
        assertEquals(i, -2);
    }

    @Test
    public void testGetRankMale() {
        // DataParser a = new DataParser();
        // int i = a.getRank(2016, "Jack", Gender.convertFromString("M"));
        int i = DataParser.getRank(2016, "Jack", Gender.convertFromString("M"));
        assertTrue(i==38);
    }

    @Test
    public void testGetRankFemale() {
        // DataParser a = new DataParser();
        // int i = a.getRank(2016, "Elizabeth", Gender.convertFromString("F"));
        int i = DataParser.getRank(2016, "Elizabeth", Gender.convertFromString("F"));
        assertTrue(i==13);
    }

    @Test
    public void testCountMale() {
        // DataParser a = new DataParser();
        // int i = a.getCount(2016, "Jack", Gender.convertFromString("M"));
        int i = DataParser.getCount(2016, "Jack", Gender.convertFromString("M"));
        assertTrue(i==8418);
    }

    @Test
    public void testCountFemale() {
        // DataParser a = new DataParser();
        // int i = a.getCount(2016, "Elizabeth", Gender.convertFromString("F"));
        int i = DataParser.getCount(2016, "Elizabeth", Gender.convertFromString("F"));
        assertTrue(i==9565);
    }

    @Test
    public void testGetTotalRankMale() {
        // DataParser a = new DataParser();
        // int i = a.getTotalCount(2016, Gender.convertFromString("M"));
        int i = DataParser.getTotalCount(2016, Gender.convertFromString("M"));
        assertTrue(i==1893471);
    }

    @Test
    public void testGetTotalRankFemale() {
        // DataParser a = new DataParser();
        // int i = a.getTotalCount(2016, Gender.convertFromString("F"));
        int i = DataParser.getTotalCount(2016, Gender.convertFromString("F"));
        assertTrue(i==1767902);
    }
    
    @Test
    @SuppressWarnings("unchecked")
    public void sameFrequencySameRankTest(){
        // 2016:
        // 951      Aabha      F     7
        // 952     Aafiya      F     6
        // 952     Akeyla      F     6
        // 953    Aadriti      F     5
        int i = DataParser.getRankSameFrequencySameRank(2016, "Emma" ,Gender.convertFromString("F"));
        assertTrue(i==1);
        i = DataParser.getRankSameFrequencySameRank(2016, "Aafiya" ,Gender.convertFromString("F"));
        assertTrue(i==952);
        i = DataParser.getRankSameFrequencySameRank(2016, "Akeyla" ,Gender.convertFromString("F"));
        assertTrue(i==952);

        ArrayList<String> names = DataParser.getNameSameFrequencySameRank(2016, 1 ,Gender.convertFromString("F"));
        String name = names.get(0);
        assertTrue(name.equals("Emma"));
        names = DataParser.getNameSameFrequencySameRank(2016, 952 ,Gender.convertFromString("F"));
        name = names.get(0);
        assertTrue(name.equals("Aafiya"));
    }

}
