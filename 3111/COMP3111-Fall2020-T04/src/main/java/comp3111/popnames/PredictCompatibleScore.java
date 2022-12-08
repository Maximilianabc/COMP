package comp3111.popnames;
import java.lang.Math;

/** 
 * A class for computing the result for task 6.
 * @author Maisy
 */

public class PredictCompatibleScore {
	/**
	 * The enum to represent if the user prefer a younger or older soulmate.
	 */
	enum RelativeAge {
		YOUNGER, OLDER
	}

	/**
	 * The (input) string that represent if the user prefer a younger or older soulmate.
	 */
	private static String[] age = {"Older", "Younger"};

	// Take those input (has been converted into suitable type) as properties 
	// so that I don't have to pass them again and again among function.

	/** The input name of the user. */
	private String user_name;
	/** The input year of birth of the user. */
	private int user_YOB;
	/** The input name of the soulmate. */
	private String soulmate_name;
	/** The input gender of the user. */
	private Gender user_gender; 
	/** The input gender of the soulmate. */
	private Gender soulmate_gender; 
	/** The input preference that the user prefer a younger or older soulmate. */
	private RelativeAge soulmate_age;
	/** The computed score in percentage. In range of [0,100]. */
	private double score = 0;
	/** The error message from the inputValidation() (if any). */
	private String error_message = "";

	/**
	 * The private constructor which is only called by ComputeAndGetResult().
	 */
	private PredictCompatibleScore(String user_name, String user_YOB, String soulmate_name, String user_gender, String soulmate_gender, String soulmate_age){
		// Do some simple operation (like trim) so that input with minor mistake are also accepted.
		inputValidationAndConversion(user_name, user_YOB, soulmate_name, user_gender, soulmate_gender, soulmate_age);
		this.user_gender = Gender.convertFromString(user_gender);
		this.soulmate_gender = Gender.convertFromString(soulmate_gender);
		this.soulmate_age = mapRelativeAge(soulmate_age);
		
	}

	/**
	 * The only public method in this class. Called by Controller.
	 * Get all input value and call the other method to <br>
	 * - Do validation. <br>
	 * - Compute result. <br>
	 * - Phrase the result into a user-friendly output.
	 * 
	 * @return The error message or the result string to be placed on the UI as output.
	 */
	public static String ComputeAndGetResult(String user_name, String user_YOB, String soulmate_name, String user_gender, String soulmate_gender, String soulmate_age){
		PredictCompatibleScore predict = new PredictCompatibleScore(user_name, user_YOB, soulmate_name, user_gender, soulmate_gender, soulmate_age);
		
		if (!predict.error_message.equals("")){ // if has error in input
			return predict.error_message;
		}
		predict.computeScore();
		
		return predict.phraseOutput();
	}

	/**
	 * Map a input string to a corresponding RelativeAge.
	 * @param input The input string (younger / older).
	 * @return The corresponding RelativeAge.
	 */
	private RelativeAge mapRelativeAge(String input){
		if (input.equals(age[0])){
			return RelativeAge.OLDER;
		}
		return RelativeAge.YOUNGER;
	}

	/**
	 * To check if all input are valid (input with minor mistake are also accepted after modified).
	 * Set the related properties that may accepted modified input.
	 * Set the error message (if any).
	 */
	private void inputValidationAndConversion(String user_name, String user_YOB, String soulmate_name, String user_gender, String soulmate_gender, String soulmate_age){
		// name 
		String alter_input = DataValidation.ChangeToValidName(user_name);
		if (alter_input.equals("")){ // invalid anyway
			error_message = "Error for user name: " + DataValidation.invalidName;
			return;
		}
		this.user_name = alter_input;

		alter_input = DataValidation.ChangeToValidName(soulmate_name);
		if (alter_input.equals("")){ // invalid anyway
			error_message = "Error for soulmate name: " + DataValidation.invalidName;
			return;
		}
		this.soulmate_name = alter_input;

		// year
		alter_input = user_YOB.trim();
		if (!DataValidation.isValidYearString(alter_input)){
			error_message = "Error for user YOB: " + DataValidation.invalidYearString;
			return;
		}
		this.user_YOB = Integer.parseInt(alter_input);
		if (!DataValidation.checkYearInRange(this.user_YOB)){
			error_message = "Error for user YOB: " + DataValidation.yearNotInRange;
			return;
		}
	}

	/**
	 * Compute the compatible score using the NK-T6 (modified) Algorithm.
	 */
	private void computeScore(){
		// oRank: user_rank
		int user_rank = DataParser.getRankSameFrequencySameRank(user_YOB, user_name, user_gender);
		// check if the name has rank
		if (user_rank < 0){ // if user_name doesn't has a rank in year user_YOB
			// set the user_rank to 5
			user_rank = 5;
		}
		
		// oYOB: soulmate_YOB
		int soulmate_YOB = user_YOB; //+- 1
		switch (soulmate_age){
			case YOUNGER: // RelativeAge.YOUNGER:
				soulmate_YOB = user_YOB + 1; break;
			case OLDER:
				soulmate_YOB = user_YOB - 1; break;
		}
		// check if soulmate_YOB out of range
		if (!DataValidation.checkYearInRange(soulmate_YOB)){ // if out of range
			// set it as the same as user_YOB
			soulmate_YOB = user_YOB;
		}

		// oRankMate
		int soulmate_rank = DataParser.getRankSameFrequencySameRank(soulmate_YOB, soulmate_name, soulmate_gender);
		// check if the name has rank
		if (soulmate_rank < 0){ // if soulmate_name doesn't has a rank in year soulmate_YOB
			// set the soulmate_rank to 5
			soulmate_rank = 5;
		}

		/* 
		 * diff = abs(oRank – oRankMate)
		 * sum = oRank + oRankMate
		 * oScore = [1 - diff / (sum + 200 + diff)] * 100%
		 * 
		 * Explanation:
		 * => Make it a higher compatible score so that users will be happy :)
		 * # diff / (sum + 200 + diff)
		 * 	 The factor showing the difference between their name rank.
		 * 	 As diff < (sum + 200 + diff),
		 * 	 the factor will be < 1, and should be not close to 1.
		 * # 1 - factor 
		 *   Then this should be not close to zero. 
		 * # The 200 is used to make factor smaller when sum is very small.
		 * # The diff is used to make factor smaller when diff is large.
		 */
		int rank_diff = Math.abs(user_rank - soulmate_rank);
		double diff_percentage = (double)rank_diff / (double)(user_rank + soulmate_rank + 200 + rank_diff);
		score =  Math.abs(1 - diff_percentage) * 100;
		//error_message = String.format("Test user_rank %d, s_rank %d, rank_diff %d, score %f\n", user_rank, soulmate_rank, rank_diff, score);
	}

	/**
	 * Phrase the result into a user-friendly output.
	 * And I really don't know what to put in the output so I add some stupid sentences in it :)
	 * @return The result string.
	 */
	private String phraseOutput(){
		//String score_line = error_message + String.format("%s and %s are having a compatible score of %.1f%%.", user_name, soulmate_name, score);
		String score_line = String.format("%s and %s are having a compatible score of %.1f%%.", user_name, soulmate_name, score);
		
		if (score < 50){
			return "Oops.\n" + score_line + "\nBut don't worry, love can solve everything.";
		}
		if (score < 70){
			return "No Bad!\n" + score_line + "\nWish you with luck.";
		}
		if (score < 90){
			return "Congratulations!\n" + score_line;
		}
		return "Wow, a perfect match!\n" + score_line;
		// return error_message + String.format("The score is %.1f%%.", score);
	}
}
