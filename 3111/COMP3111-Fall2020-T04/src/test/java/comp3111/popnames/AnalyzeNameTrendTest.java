package comp3111.popnames;
import static org.junit.Assert.*; 
import org.junit.Test;
import edu.duke.*;

public class AnalyzeNameTrendTest {
    @Test
    public void invalidInput(){
        String result, error_message;
        //ComputeAndGetResult(String start_year, String end_year, String gender, String top_N)
        
        error_message = "Error for start year: " + DataValidation.invalidYearString;
        result = AnalyzeNameTrend.ComputeAndGetResult("abcd", "1999", "Male", "5");
        assertTrue(result.equals(error_message));

        error_message = "Error for end year: " + DataValidation.invalidYearString;
        result = AnalyzeNameTrend.ComputeAndGetResult("1999", "abcd", "Male", "5");
        assertTrue(result.equals(error_message));

        error_message = "Error for period: " + DataValidation.invalidPeriod;
        result = AnalyzeNameTrend.ComputeAndGetResult("1999", "1899", "Male", "5");
        assertTrue(result.equals(error_message));

        error_message = "Error for period: Please input a period of at least two year.";
        result = AnalyzeNameTrend.ComputeAndGetResult("1999", "1999", "Male", "5");
        assertTrue(result.equals(error_message));

        error_message = "Error for top N: " + DataValidation.invalidIntString;
        result = AnalyzeNameTrend.ComputeAndGetResult("1999", "2000", "Male", "N");
        assertTrue(result.equals(error_message));

        error_message = "Error for top N: Please input a integer > 0.";
        result = AnalyzeNameTrend.ComputeAndGetResult("1999", "2000", "Male", "-5");
        assertTrue(result.equals(error_message));
    }

    @Test
    public void validInput1(){
        String result, expected_result = "";
        FileResource expected_result_txt = new FileResource("test_case_expected_result/task3expected_result1.txt");
        for (String line: expected_result_txt.lines()) { 
            expected_result += (line + "\n");
        }

        //ComputeAndGetResult(String start_year, String end_year, String gender, String top_N)
        
        result = AnalyzeNameTrend.ComputeAndGetResult("1999", "2001", "Female", "10");
        assertTrue(result.equals(expected_result));
    }

    @Test
    public void validInput2(){
        String result, expected_result = "";
        FileResource expected_result_txt = new FileResource("test_case_expected_result/task3expected_result2.txt");
        for (String line: expected_result_txt.lines()) { 
            expected_result += (line + "\n");
        }

        //ComputeAndGetResult(String start_year, String end_year, String gender, String top_N)
        
        result = AnalyzeNameTrend.ComputeAndGetResult("1889", "2000", "Male", "1");
        assertTrue(result.equals(expected_result));
    }

}
