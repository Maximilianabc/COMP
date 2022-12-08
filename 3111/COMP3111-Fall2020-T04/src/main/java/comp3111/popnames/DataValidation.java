package comp3111.popnames;
import java.util.regex.Pattern;  

/**
 *  A class for doing input validation. Use for
 * - Check if some String / value is valid
 * - Get default error message.
 * - Convert a "semi-valid" name string to valid string.
 * 
 * @author Maisy
 */

public class DataValidation {
	// Default error message.
	// For example, you can use it like :
	// if (!DataValidation.checkYearInRange(2000)){
	//      return DataValidation.yearNotInRange;
	// }
	
	/**
	 * Default error message for checkYearInRange().
	 */
	public static String yearNotInRange = "Please input a year in the range of 1880-2019.";
	
	/**
	 * Default error message for checkPeriod().
	 */
	public static String invalidPeriod = "Please input a valid period. Both of the starting year and" + 
			   							 " ending year need to be in range of 1880-2019. And the ending" +
			   							 " year should not be earlier than the starting year.";

	/**
	 * Default error message for checkChoicenOne().
	 */
	public static String moreThanOneChoice = "Please only select only on option.";
	
	/**
	 * Default error message for isValidName().
	 */
	public static String invalidName = "Please enter a valid name. e.g. Amy, Bob (Capital for the first letter)";
	
	/**
	 * Default error message for isValidYearString().
	 */
	public static String invalidYearString = "Please enter a valid year.";
	
	/**
	 * Default error message for isValidIntString().
	 */
	public static String invalidIntString = "Please enter a non-negative integer.";
	
	/**
	 * Check if a year is in range of 1880-2019.
	 * @param year The year to be checked.
	 * 
	 * @return Return true if the year is in range. <br>
	 * Return false otherwise.
	 */
	public static boolean checkYearInRange(int year) {
		if (1880 <= year && year <= 2019) {
			return true;
		}
		return false;
	}
	
	/**
	 * Check if a given period is true.
	 * @param start_year The given starting year of the period.
	 * @param end_year The given ending year of the period.
	 * 
	 * @return Return true if the period is valid. <br>
	 * Return false otherwise.
	 */
	public static boolean checkPeriod(int start_year, int end_year) {
		if (checkYearInRange(start_year) && checkYearInRange(end_year)){
			if (start_year <= end_year) {
				return true;
			}
		}
		return false;
	}
	
	/**
	 * Check if only one of the two choices is selected. May not needed if use ToggleGroup in the UI.
	 * @param choice1 One of the two choices. True means selected. False means unselected.
	 * @param choice2 One of the two choices. 
	 * 
	 * @return Return true if only one of the two choices is selected. <br>
	 * Return false otherwise.
	 */
	public static boolean checkChoicenOne(boolean choice1, boolean choice2) {
		if ((choice1 && !choice2) || (!choice1 && choice2)) {
			return true;
		}
		return false;
	}
	
	/**
	 * Check if a given string is a valid name. <br>
	 * Valid is defined as  <br>
	 * - Not null. <br>
	 * - Should at least be two characters long. <br>
	 * - Only contain letters (A-Z and a-z).  <br>
	 * - Must start with one upper case and follow by all lower case. <br>
	 * 
	 * @param name The given name to be checked.
	 * 
	 * @return If the given string is a valid name, return true. <br>
	 * Otherwise, return false. 
	 */
	public static boolean isValidName(String name) {
		return Pattern.matches("^[A-Z][a-z]+$", name);
	}
	
	/**
	 * Check if a given string is a valid year. <br>
	 * Valid is defined as  <br>
	 * - Not null. <br>
	 * - Should be exactly 4 digits long. <br>
	 * - Only contain number (0-9).  <br>
	 * 
	 * @param year_in_string The given year in String type.
	 * 
	 * @return If the given string is a valid year string, return true. <br>
	 * Otherwise, return false.
	 */
	public static boolean isValidYearString(String year_in_string) {
		return Pattern.matches("^[0-9]{4}$", year_in_string);
	}

	/**
	 * Check if a given string is a valid integer. <br>
	 * Valid is defined as  <br>
	 * - Not null. <br>
	 * - Only contain number (0-9) and (optional) a negative sign "-" in the front.  <br>
	 * 
	 * @param int_in_string The given integer in String type.
	 * 
	 * @return If the given string is a valid integer string, return true. <br>
	 * Otherwise, return false.
	 */
	public static boolean isValidIntString(String int_in_string) {
		return Pattern.matches("-?[0-9]+$", int_in_string);
	}
	
	/**
	 * Check if a given string is a valid name.  <br>
	 * Valid is defined as  <br>
	 * - Not null. <br>
	 * - Should at least be two characters long. <br>
	 * - Only contain letters (A-Z and a-z).  <br>
	 * - Must start with one upper case and follow by all lower case. <br>
	 * 
	 * @param name The given name to be checked.
	 * 
	 * @return If the given string is valid return the it.  <br>
	 *  <br>
	 * If it is not valid but contain only character (no matter lower / upper case) and space,
	 * change it to the format "start with one upper case and follow by all lower case"
	 * and return the changed string. <br>
	 *  <br> 
	 * Otherwise, the name is invalid anyway, return empty string "".
	 */
	public static String ChangeToValidName(String name) {
		if (isValidName(name)){
			return name;
		}
		name = name.trim();
		if (Pattern.matches("^[A-Z,a-z]{2,}$", name)){
			name = name.toLowerCase();
			name = name.substring(0, 1).toUpperCase() + name.substring(1);
			return name;
		}
		return ""; // invalid name
	}
}
