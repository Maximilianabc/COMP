package comp3111.popnames;

import edu.duke.FileResource;
import org.apache.commons.csv.CSVParser;
import org.apache.commons.csv.CSVRecord;

import java.util.ArrayList;

/**
 *  A class to handle data parsing from CSV.
 * @author ALL
 */

public class DataParser {
    /**
     * Function to get the CSV file.
     */
    public static CSVParser getFileParser(int year) {
        FileResource fr = new FileResource(String.format("dataset/yob%s.csv", year));
        return fr.getCSVParser(false);
    }

    /**
     * Wrapper functions for function that with the same name.
     * Support gender in Gender type other than string.
     * (Maisy)
     */
    public static int getRank(int year, String name, Gender gender){
        return getRank(year, name, gender.convertToSymbol());
    }

    /**
     * Wrapper functions for function that with the same name.
     * Support gender in Gender type other than string.
     * (Maisy)
     */
    public static String getName(int year, int rank, Gender gender){
        return getName(year, rank, gender.convertToSymbol());
    }

    /**
     * Wrapper functions for function that with the same name.
     * Support gender in Gender type other than string.
     * (Maisy)
     */
    public static int getCount(int year, String name, Gender gender){
        return getCount(year, name, gender.convertToSymbol());
    }

    /**
     * Wrapper functions for function that with the same name.
     * Support gender in Gender type other than string.
     * (Maisy)
     */
    public static int getTotalCount(int year, Gender gender){
        return getTotalCount(year, gender.convertToSymbol());
    }

    /**
     * Wrapper functions for function that with the same name.
     * Support gender in Gender type other than string.
     * (Maisy)
     */
    public static ArrayList getRecordOfTopN(int year, Gender gender, int top_N){
        return getRecordOfTopN(year, gender.convertToSymbol(), top_N);
    }

    /**
     * Wrapper functions for function that with the same name.
     * Support gender in Gender type other than string.
     * (Maisy)
     */
    public static int getRankSameFrequencySameRank(int year, String name, Gender gender) {
    	return getRankSameFrequencySameRank(year, name, gender.convertToSymbol());
    }

    /**
     * Wrapper functions for function that with the same name.
     * Support gender in Gender type other than string.
     * (Maisy)
     */
    public static ArrayList getNameSameFrequencySameRank(int year, int given_rank, Gender gender) {
    	return getNameSameFrequencySameRank(year, given_rank, gender.convertToSymbol());
    }

    /**
     * A helper function help to wash away any special character in a name string
     * so that only letters (upper case or lower case) are left.
     * @param string_to_wash The string that needed to have special character (or number) be removed. 
     * @return The washed string which only contain letters.
     * (Maisy)
     */
    public static String RemoveSpecialCharacter(String string_to_wash){
        String regex = "[^a-z,A-Z]";
        return string_to_wash.replaceAll(regex, "");
    }

    /**
     *  Implementation of getCount of a name in a year.
     *  @author Frankie
     *  @param year Year to get  count.
     *  @param name Name to get count.
     *  @param gender Gender of the name.
     *  @return The count of the name or error -1 (Name found, Gender mismatch), -2 (Name not found)
     */
    private static int getCount(int year, String name, String gender) {
        boolean foundName = false;
        for (CSVRecord rec : getFileParser(year)) {
            if (RemoveSpecialCharacter(rec.get(0)).equals(name)) {
                if (rec.get(1).equals(gender)) {
                    return Integer.parseInt(rec.get(2));
                }
                foundName = true;
            }
        }
        if (foundName)
            return -1; //Name found, Gender mismatch
        else
            return -2; //Name not found
    }

    /**
     *  Implementation of geTotalCount of every name in a year.
     *  @author Frankie
     *  @param year Year to get  count.
     *  @param gender Gender of the name.
     *  @return The total count or error -1.
     */
    private static int getTotalCount(int year, String gender) {
        int count = 0;
        for (CSVRecord rec : getFileParser(year)) {
            if (rec.get(1).equals(gender)) {
                count += Integer.parseInt(rec.get(2));
            }
        }
        if (count == 0)
            return -1; //cannot find total count
        else
            return count;
    }

    /**
     *  Implementation of getRank of a name in a year by looping through the csv file.
     *  @author Frankie
     *  @param year Year to get  count.
     *  @param name Name to get count.
     *  @param gender Gender of the name.
     *  @return The rank of the name or error -1 (Name found, Gender mismatch), -2 (Name not found)
     */
    private static int getRank(int year, String name, String gender) {
        boolean foundName = false;
        int rank = 1;
        for (CSVRecord rec : getFileParser(year)) {
            if (rec.get(0).equals(name)) {
                if (rec.get(1).equals(gender)){
                    return rank;
                }
                foundName = true;
            }
            if (rec.get(1).equals(gender))
                rank++;
        }
        if (foundName)
            return -1; //Name found, Gender mismatch
        else
            return -2; //Name not found
    }

    /**
     * Get the rank of the given name in the given year. 
     * Different name with the same frequency to have the same rank. 
     * (Modified from getRank() by Maisy)
     * @param year The given year to search with.
     * @param name The given name to search with.
     * @param gender The gender of the name corresponding to.
     * @return Return the rank if the name appear. Return -1 if the given name is found
     * but in different gender. Return -2 if the given name is not found.
     */
    private static int getRankSameFrequencySameRank(int year, String name, String gender) {
        boolean foundName = false;
        int current_rank = 0, last_frequency = Integer.MAX_VALUE, this_frequency = 0;
        boolean same_gender;
        for (CSVRecord rec : getFileParser(year)) {
            same_gender = rec.get(1).equals(gender);
            
            if (same_gender){ // same gender, count the rank
                this_frequency = Integer.parseInt(rec.get(2));
                if (this_frequency < last_frequency){ // new frequency, new rank
                    current_rank++;
                    last_frequency = this_frequency;
                }
            }
            
            if (RemoveSpecialCharacter(rec.get(0)).equals(name)) {
                // if current has the same name with the target
                if (same_gender){ // same gender too
                    return current_rank;
                }
                foundName = true; // found name but diff gender
            }
        }

        if (foundName)
            return -1; //Name found, Gender mismatch
        else
            return -2; //Name not found
    }

    /**
     *  Implementation of getName of a rank in a year by looping through the csv file.
     *  @author Frankie
     *  @param year Year to get name.
     *  @param rank Rank to get name.
     *  @param gender Gender of the name.
     *  @return The the name string or error null (Rank out of bound)
     */
    private static String getName(int year, int rank, String gender) {
        int currentRank = 0;
        for (CSVRecord rec : getFileParser(year)) {
            // Get its rank if gender matches param
            if (rec.get(1).equals(gender)) {
                // Get the name whose rank matches param
                currentRank++;
                if (currentRank == rank) {
                    return rec.get(0);
                }
            }
        }
        return null; //rank input > rank of gender in year csv
    }

    /**
     * Get the names with the given rank in the given year.
     * Different name with the same frequency to have the same rank. 
     * (Modified from getName() by Maisy)
     * @param year The given year to search with.
     * @param given_rank The given year to search with.
     * @param gender The gender of the name should be corresponding to.
     * @return An ArrayList with the target names. It may continue 0 to 
     * "count of names with the given rank in the given year" items. 
     */
    private static ArrayList getNameSameFrequencySameRank(int year, int given_rank, String gender) {
        int current_rank = 0, last_frequency = Integer.MAX_VALUE, this_frequency = 0;
        ArrayList<String> target_names = new ArrayList<String>();
        for (CSVRecord rec : getFileParser(year)) {
            if (rec.get(1).equals(gender)){ // go to the correct gender
                this_frequency = Integer.parseInt(rec.get(2));
                if (this_frequency < last_frequency){ // new frequency, new rank
                    current_rank++;
                    last_frequency = this_frequency;
                }
                if (current_rank == given_rank){ // if it fits the target rank
                    target_names.add(RemoveSpecialCharacter(rec.get(0))); // add the name to the result
                }
            }
        }
        return target_names;
    }

    /**
     * For task 3. Get a list of records (in form of DataRecord instances) that fit the condition: <br>
     * - In the given year. <br>
     * - Corresponding to given gender. <br>
     * - Is the top N names in that year, where names with the same frequency have the same rank.
     * 
     * @param year The given year to search with.
     * @param gender The given gender to search with.
     * @param top_N The given range of rank to search with.
     * @return An ArrayList of records (in form of DataRecord instances).
     * 
     * (Maisy)
     */
    private static ArrayList getRecordOfTopN(int year, String gender, int top_N){
        ArrayList<DataRecord> target_records = new ArrayList<DataRecord>();
        int current_rank = 1, last_frequency = Integer.MAX_VALUE;

        for (CSVRecord rec : getFileParser(year)) { // get the year csv file
            if (rec.get(1).equals(gender)){ // go to the correct gender section
                if (current_rank <= top_N){ // if still in range of top N
                    // fit all condition, include this record in the result
                    DataRecord this_rec = new DataRecord(rec, current_rank);
                    target_records.add(this_rec);

                    if (this_rec.frequency < last_frequency){
                        // update the current rank if the frequency changed (decreased)
                        current_rank++;
                        last_frequency = this_rec.frequency;
                    }
                } else { // more than top N, no need to read the rest
                    break;
                }
            }
        }
        return target_records;
    }
    /*
    public static int getCount_alternative(int year, String name, Gender gender)
    throws IOException {
        Optional<CSVRecord> record = getFileParser(year).getRecords().stream()
                                                        .filter(r -> r.get(0).equals(name))
                                                        .findFirst();
        if (record.isEmpty()) {
            throw new IllegalArgumentException("Name not found.");
        } else if (!record.get().get(1).equals(gender.toString())) {
            throw new IllegalArgumentException("Gender mismatch");
        } else {
            return Integer.parseInt(record.get().get(2));
        }
    }

    public static int getTotalCount_alternative(int year, Gender gender)
    throws IOException {
        int sum = getFileParser(year).getRecords().stream()
                                     .filter(r -> r.get(1).equals(gender.toString()))
                                     .mapToInt(r -> Integer.parseInt(r.get(2)))
                                     .sum();
        if (sum == 0)
            throw new IllegalArgumentException("Cannot find total count.");
        else
            return sum;
    }

    public static int getRank_alternative(int year, String name, Gender gender)
    throws IOException {
        List<CSVRecord> records = getFileParser(year).getRecords();
        Optional<CSVRecord> rec = records.stream()
                                         .filter(r -> r.get(0).equals(name))
                                         .findFirst();
        if (rec.isEmpty()) {
            throw new IllegalArgumentException("Name not found.");
        } else if (!rec.get().get(1).equals(gender.toString())) {
            throw new IllegalArgumentException("Gender mismatch");
        } else {
            return records.indexOf(rec) + 1;
        }
    }

    public static String getName_alternative(int year, int rank, Gender gender)
    throws IOException {
        try {
            return getFileParser(year).getRecords().stream()
                                      .filter(r -> r.get(1).equals(gender.toString()))
                                      .collect(Collectors.toList())
                                      .get(rank + 1).get(1);
        } catch (IndexOutOfBoundsException e) {
            throw new IllegalArgumentException(String.format("Rank is larger than total number of names in %d.", year));
        }
    }
    */
}
