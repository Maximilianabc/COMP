package comp3111.popnames;

import org.junit.Test;
import static org.junit.Assert.assertEquals;

public class PairNamePredictionTest {

    @Test
    public void testIncorrectGender() {
        String oReport = PairNamePrediction.predictName("Elsa","Male", 1950, "Female","Younger");
        assertEquals(oReport, "Error: name found but gender mismatch in year 1950");
    }

    @Test
    public void testIncorrectName() {
        String oReport = PairNamePrediction.predictName("hello","Male", 1950, "Female","Younger");
        assertEquals(oReport, "Error: name not found in year 1950.");
    }

    @Test
    public void testSoulmateYoBtooLow() {
        String oReport = PairNamePrediction.predictName("Edward","Male", 1880, "Female","Younger");
        assertEquals(oReport, "Error: Soulmate's Year of Birth 1879 is out of range (1880-2019)");
    }

    @Test
    public void testSoulmateYoBtooHigh() {
        String oReport = PairNamePrediction.predictName("Edward","Male", 2019, "Female","Older");
        assertEquals(oReport, "Error: Soulmate's Year of Birth 2020 is out of range (1880-2019)");
    }

    @Test
    public void testMale1(){
        String oReport = PairNamePrediction.predictName("Edward","Male", 1950, "Female","Older");
        assertEquals(oReport, "Application 2\n" +
                "Your Female soulmate name(s) is/are [Carolyn] who ranked 22 in year 1951.");
    }

    @Test
    public void testMale2(){
        String oReport = PairNamePrediction.predictName("Edward","Male", 1950, "Female","Younger");
        assertEquals(oReport, "Application 2\n" +
                "Your Female soulmate name(s) is/are [Betty] who ranked 22 in year 1949.");
    }

    @Test
    public void testMale3(){
        String oReport = PairNamePrediction.predictName("Edward","Male", 1950, "Male","Older");
        assertEquals(oReport, "Application 2\n" +
                "Your Male soulmate name(s) is/are [George] who ranked 22 in year 1951.");
    }

    @Test
    public void testMale4(){
        String oReport = PairNamePrediction.predictName("Edward","Male", 1950, "Male","Younger");
        assertEquals(oReport, "Application 2\n" +
                "Your Male soulmate name(s) is/are [Daniel] who ranked 22 in year 1949.");
    }

    @Test
    public void testOrankOutOfScope(){
        String oReport = PairNamePrediction.predictName("Edward","Female", 1950, "Male","Older");
        assertEquals(oReport, "Application 2\n" +
                "Your Male soulmate name(s) is/are [James] who ranked 1 in year 1951.");
    }
}
