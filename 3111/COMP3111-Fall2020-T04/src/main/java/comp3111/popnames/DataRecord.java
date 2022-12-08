package comp3111.popnames;
import org.apache.commons.csv.*;

/**
 * For task 3. An DataRecord represent a row in the csv file (so that I don't have to 
 * understand and get data I need from some string/CSVRecord). It is easy to order 
 * a list of DataRecord instances by the name as well.
 * @author Maisy
 */
public class DataRecord implements Comparable<DataRecord>{
    public String name;
    // public Gender gender;
    public int frequency;
    public int rank;

    /**
     * Create a DataRecord from a CSVRecord.
     * @param rec The CSVRecord.
     * @param rank The rank of the name in the record should be.
     */
    public DataRecord(CSVRecord rec, int rank){
        // rec: name, gender, frequency
        name = DataParser.RemoveSpecialCharacter(rec.get(0));
        // gender = Gender.convertFromString(rec.get(1));
        frequency = Integer.parseInt(rec.get(2));
        this.rank = rank;
    }

    /* public DataRecord(String name, int frequency, int rank){
        this.name = name;
        this.frequency = frequency;
        this.rank = rank;
    } */

    /**
     * For sorting DataRecord using name asc.
     */
    @Override
    public int compareTo(DataRecord another) {
        return this.name.compareTo(another.name);
    }

}
