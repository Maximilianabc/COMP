package comp3111.popnames;
import static org.junit.Assert.*; 
import org.junit.Test;

public class DataValidationTest {
	@Test
	public void checkYearInRangeTestTrue() { 
		assertTrue(DataValidation.checkYearInRange(1880)); 
		assertTrue(DataValidation.checkYearInRange(2019)); 
		assertTrue(DataValidation.checkYearInRange(1881)); 
		assertTrue(DataValidation.checkYearInRange(2018)); 
		assertTrue(DataValidation.checkYearInRange(2000));
	}
	
	@Test
	public void checkYearInRangeTestFalse() {
		assertFalse(DataValidation.checkYearInRange(1879)); 
		assertFalse(DataValidation.checkYearInRange(2020)); 
		assertFalse(DataValidation.checkYearInRange(2777));
	}
	@Test
	public void checkPeriodTestTrue() {
		assertTrue(DataValidation.checkPeriod(1880, 2019));
		assertTrue(DataValidation.checkPeriod(1997, 2000)); 
	}
	
	@Test
	public void checkPeriodTestFalse() {
		assertFalse(DataValidation.checkPeriod(1877, 2019));
		assertFalse(DataValidation.checkPeriod(2019, 1899)); 
	}
	
	@Test
	public void checkChoicenOneTestTrue() {
		assertTrue(DataValidation.checkChoicenOne(true, false)); 
		assertTrue(DataValidation.checkChoicenOne(false, true));
	}
	
	@Test
	public void checkChoicenOneTestFalse() {
		assertFalse(DataValidation.checkChoicenOne(true, true));
		assertFalse(DataValidation.checkChoicenOne(false, false)); 
	}

	@Test
	public void isValidNameTest(){
		// false cases
		// null string
		assertFalse(DataValidation.isValidName(""));
		// shorter than 2 characters
		assertFalse(DataValidation.isValidName("W"));
		// has symbols
		assertFalse(DataValidation.isValidName("Ww&&"));
		// Not one upper case follow by lower case
		assertFalse(DataValidation.isValidName("WwWW"));

		// true cases
		assertTrue(DataValidation.isValidName("Amy"));
		assertTrue(DataValidation.isValidName("Bob"));
	}

	@Test
	public void isValidYearStringTest(){
		// false cases
		// null string
		assertFalse(DataValidation.isValidYearString(""));
		// wrong length
		assertFalse(DataValidation.isValidYearString("19999"));
		// not only number
		assertFalse(DataValidation.isValidYearString("199A"));

		// true cases
		assertTrue(DataValidation.isValidYearString("1999"));
		assertTrue(DataValidation.isValidYearString("2000"));
	}

	@Test
	public void ChangeToValidNameTest(){
		String ori_string, altered_name;
		
		ori_string = "";
		altered_name = DataValidation.ChangeToValidName(ori_string);
		assertTrue(altered_name.equals(""));

		ori_string = "BOb    ";
		altered_name = DataValidation.ChangeToValidName(ori_string);
		assertTrue(altered_name.equals("Bob"));

		ori_string = "Amy";
		altered_name = DataValidation.ChangeToValidName(ori_string);
		assertTrue(altered_name.equals("Amy"));

	}
}
