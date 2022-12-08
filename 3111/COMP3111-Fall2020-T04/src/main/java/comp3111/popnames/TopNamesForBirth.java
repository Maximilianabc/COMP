package comp3111.popnames;

import org.apache.commons.csv.CSVParser;
import java.io.IOException;
import java.util.*;
import java.util.Map.Entry;
import java.util.function.Function;
import java.util.stream.Collectors;

/**
 * A class for computing the result for task 1.
 * @author Max
 */

public class TopNamesForBirth {

    static final SortedMap<Integer, List<String>> years = new TreeMap<>();
    // <rank, names of that rank in every year>
    static final SortedMap<Integer, List<String>> ranks = new TreeMap<>();
    // stores max length of each columns
    static final List<Integer> max_lengths = new ArrayList<>();
    /**
     * Generate a brief report in response to the queries on the most popular names registered at birth over a given period.
     * @param t1y1 Lower bound of period, inclusive
     * @param t1y2 Upper bound of period, inclusive
     * @param topN The number of top most frequently registered names to be reported
     * @param gender The gender of names to be reported
     * @return String containing the summary and the result table
     * @throws IOException if {@link CSVParser#getRecords()} failed to read the CSV file
     */
    public static String Compute(String t1y1, String t1y2, String topN, Gender gender)
    throws IOException
    {
        Optional<String> err = validation(t1y1, t1y2, topN);
        if (err.isPresent())
            return err.get();

        int y1 = Integer.parseInt(t1y1);
        int y2 = Integer.parseInt(t1y2);
        int top = Integer.parseInt(topN);

        addRecords(y1, y2, top, gender);
        loadIntoRanks(top);
        String summary = getSummary(y1, y2, getMostFrequent(), gender);
        storeMaxLengths();
        return summary + getResult(top);
    }

    /**
     * Validates input from the application. Params info are the same as {@link #Compute(String, String, String, Gender)}
     * @return Error message, none if no error is found
     */
    public static Optional<String> validation(String t1y1, String t1y2, String topN) {
        if (!DataValidation.isValidYearString(t1y1) || !DataValidation.isValidYearString(t1y2)){
            return Optional.of(DataValidation.invalidYearString);
        }
        if (!DataValidation.isValidIntString(topN)) {
            return Optional.of(DataValidation.invalidIntString);
        }
        int y1 = Integer.parseInt(t1y1);
        int y2 = Integer.parseInt(t1y2);
        if (!DataValidation.checkYearInRange(y1) || !DataValidation.checkYearInRange(y2)) {
            return Optional.of(DataValidation.yearNotInRange);
        }
        if (!DataValidation.checkPeriod(y1, y2)) {
            return Optional.of(DataValidation.invalidPeriod);
        }
        return Optional.empty();
    }

    /**
     * Add records to the SortedMap {@link #years}.
     * Params and throw info are the same as {@link #Compute(String, String, String, Gender)}
     */
    public static void addRecords(int y1, int y2, int top, Gender gender)
    throws IOException
    {
        years.clear();
        for (int i = y1; i <= y2; i++) {
            years.put(i, DataParser.getFileParser(i).getRecords() // get all records of the year
                                   .stream()
                                   .filter(r -> r.get(1).equals(gender.toString())) // filter same gender
                                   .limit(top) // limit the list to first n elements
                                   .map(r -> r.get(0)) // map to another stream containing the names only
                                   .collect(Collectors.toList())); // cast back to list
        }
    }

    /**
     * Load records from {@link #years} to {@link #ranks}
     */
    public static void loadIntoRanks(int top) {
        ranks.clear();
        // init ranks map
        for (int j = 1; j <= top; j++) {
            ranks.put(j, new ArrayList<>());
        }

        for (List<String> names : years.values()) {
            for (int k = 1; k <= top; k++) {
                if (k > names.size())
                    break;
                ranks.get(k).add(names.get(k - 1)); // add the name at rank k to ranks
            }
        }
    }

    /**
     * Get the name that held the top place most often in the period in {@link #years}
     * @return The name and frequency of interest, stored in a key value pair.
     */
    public static Optional<Entry<String, Long>> getMostFrequent() {
        return ranks.get(1).stream()
                    // group the elements by occurrence
                    .collect(Collectors.groupingBy(Function.identity(), Collectors.counting()))
                    .entrySet()
                    .stream()
                    .max(Entry.comparingByValue()); // get the most frequent element and store it in a Map.Entry
    }

    /**
     * Store maximum lengths of each rank column in {@link #max_lengths}
     */
    public static void storeMaxLengths() {
        max_lengths.clear();
        for (List<String> rank : ranks.values()) {
            if (rank.size() == 0) {
                max_lengths.add(0); // add dummy max length if the rank has no entries
                continue;
            }
            // add the length of longest name in rank to max_length
            max_lengths.add(Collections.max(rank, Comparator.comparing(String::length)).length());
        }
    }

    /**
     * Get the summary of the result
     * @param most_frequent The name that held the top place most often
     * Other params info are the same as in {@link #Compute(String, String, String, Gender)}
     * @return The summary string
     */
    public static String getSummary(int y1, int y2, Optional<Entry<String, Long>> most_frequent, Gender gender) {
        String g = gender == Gender.MALE ? "male" : "female";
        return most_frequent.isPresent()
               ? String.format("Over the period %1$d to %2$d, %3$s for %4$s has hold the top spot most often for a total of %5$d times.\n\n",
                                y1, y2, most_frequent.get().getKey(), g, most_frequent.get().getValue())
               : String.format("Over the period %1$d to %2$d, all names for %3$s have hold the top spot for equal times.\n\n",
                                y1, y2, g);
    }

    /**
     * Get the table representing the result
     * @return The table in string with padding
     */
    public static StringBuilder getResult(int top) {
        StringBuilder result = new StringBuilder("Year |");
        for (int l = 1; l <= top; l++) {
            String header = "Top " + l;
            if (header.length() > max_lengths.get(l - 1)) {
                max_lengths.set(l - 1, header.length());
            }
            int repeats = max_lengths.get(l - 1) - header.length();
            result.append(header).append(" ".repeat(repeats)).append(" |"); // pad spaces until it reaches max length of that column
        }
        result.append("\n");

        for (Entry<Integer, List<String>> entry : years.entrySet()) {
            int m = 0;
            result.append(entry.getKey()).append(" |");
            for (String name : entry.getValue()) {
                int repeats = max_lengths.get(m) - name.length();
                result.append(name).append(" ".repeat(repeats)).append(" |");
                m++;
            }
            result.append("\n");
        }
        return result;
    }
}
