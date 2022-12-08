package comp3111.popnames;
import static org.junit.Assert.*; 
import org.junit.Test;

public class GenderTest {
    @Test
	public void convertFromStringTest() { 
        Gender gender_from_str = Gender.convertFromString("M");
		assertTrue(gender_from_str == Gender.MALE); 
        gender_from_str = Gender.convertFromString("Male");
        assertTrue(gender_from_str == Gender.MALE); 
        
        gender_from_str = Gender.convertFromString("F");
		assertTrue(gender_from_str == Gender.FEMALE); 
        gender_from_str = Gender.convertFromString("Female");
        assertTrue(gender_from_str == Gender.FEMALE);
        
        Boolean has_error = false;
        try {
            gender_from_str = Gender.convertFromString("abc");
        } catch (IllegalArgumentException e) {
            has_error = true;
        }
        assertTrue(has_error);
    }
    
    @Test
    public void convertToSymbolTest(){
        Gender g = Gender.convertFromString("Male");
        assertTrue(g.convertToSymbol().equals("M"));
        g = Gender.convertFromString("Female");
        assertTrue(g.convertToSymbol().equals("F"));
    }
}
