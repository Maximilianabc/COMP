package comp3111.popnames;
import static org.junit.Assert.*; 
import org.junit.Test;

public class PredictCompatibleScoreTest {
    @Test
    public void invalidInput(){
        String result, error_message;
        //ComputeAndGetResult(user_name, user_YOB, soulmate_name, user_gender, 
        //                    soulmate_gender, soulmate_age)
        
        error_message = error_message = "Error for user name: " + DataValidation.invalidName;
        result = PredictCompatibleScore.ComputeAndGetResult("A00","2000","Reid","Female","Male","Older");
        assertTrue(result.equals(error_message));

        error_message = error_message = "Error for soulmate name: " + DataValidation.invalidName;
        result = PredictCompatibleScore.ComputeAndGetResult("Kate","2000","Rei--d","Female","Male","Older");
        assertTrue(result.equals(error_message));

        error_message = error_message = "Error for user YOB: " + DataValidation.invalidYearString;
        result = PredictCompatibleScore.ComputeAndGetResult("Kate  ","2000+","Reid","Female","Male","Younger");
        assertTrue(result.equals(error_message));
        
        error_message = error_message = "Error for user YOB: " + DataValidation.yearNotInRange;
        result = PredictCompatibleScore.ComputeAndGetResult("Kate  ","2999","Reid","Female","Male","Older");
        assertTrue(result.equals(error_message));
    }

    @Test
    public void validInput(){
        String result, expected_result; 

        // both rank 1 => score = 100  
        expected_result = "Wow, a perfect match!\nMary and John are having a compatible score of 100.0%.";
        result = PredictCompatibleScore.ComputeAndGetResult("Mary  ","  1880  ","John","Female","Male","Younger");
        assertTrue(result.equals(expected_result));

        // David: 14, Nancy: 342
        expected_result = "No Bad!\nDavid and Nancy are having a compatible score of 62.9%.\nWish you with luck.";
        result = PredictCompatibleScore.ComputeAndGetResult("David","2008","NANCY","Male","Female","Older");
        assertTrue(result.equals(expected_result));

        // Jacob: 1, Tiffany: 80
        expected_result = "Congratulations!\nJacob and Tiffany are having a compatible score of 78.1%.";
        result = PredictCompatibleScore.ComputeAndGetResult("Jacob","2000","Tiffany","Male","Female","Older");
        assertTrue(result.equals(expected_result));

        // both name unfound, and soulmate YOB out of range
        expected_result = "Wow, a perfect match!\nAaaa and Bbbb are having a compatible score of 100.0%.";
        result = PredictCompatibleScore.ComputeAndGetResult("AAAA","2019","BBBB","Male","Female","Younger");
        assertTrue(result.equals(expected_result));
    }

}
