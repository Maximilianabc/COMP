package comp3111.popnames;
import static org.junit.Assert.*; 
import org.junit.Test;
import java.util.Collections;
import java.util.ArrayList;

public class DataRecordTest {
    @Test
    @SuppressWarnings("unchecked")
    public void TestSort(){
        ArrayList<DataRecord> target_record = DataParser.getRecordOfTopN(2000, Gender.FEMALE, 5);
        Collections.sort(target_record);
        assertTrue(target_record.get(0).name.equals("Ashley"));
        assertTrue(target_record.get(1).name.equals("Emily"));
        assertTrue(target_record.get(2).name.equals("Hannah"));
        assertTrue(target_record.get(3).name.equals("Madison"));
        assertTrue(target_record.get(4).name.equals("Sarah"));   
    }
}
