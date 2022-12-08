package comp3111.popnames;

/**
 * A class for computing name popularity within a period for task 2.
 * @author Frankie
 */

public class NamePopularity {

    /**
     * A public method called by Controller to getSummary of the name's popularity in a period
     * It is assumed that input is validated at controller class.
     * @param name The name to be checked
     * @param gender The gender of the name
     * @param y1 The lower bound of the period
     * @param y2 The upper bound of the period
     * @return Error message or the summary and table string for the UI.
     */

    public static String getSummary(String name, String gender, int y1, int y2) {
        String oReport = "Report 2\n";
        int[] rank = new int[y2-y1+1];
        int[] count = new int[y2-y1+1];
        double[] percentage = new double[y2-y1+1];
        int highest = 0;
        int highestRank = DataParser.getRankSameFrequencySameRank(y1, name, Gender.convertFromString(gender));

        for(int y = 0; y < y2-y1+1; y++){
            rank[y] = DataParser.getRankSameFrequencySameRank(y1+y, name, Gender.convertFromString(gender));
            if (rank[y] == -1){
                return String.format("Error: name found but gender mismatch in year %d", y1+y);
            } else if (rank[y] == -2){
                return String.format("Error: name not found in year %d.", y1+y);
            } else{
                if (rank[y] <= highestRank){
                    highestRank = rank[y];
                    highest = y;
                }
            }

            count[y] = DataParser.getCount(y1+y, name, Gender.convertFromString(gender));
            if (count[y] == -1){
                return String.format("Error: name found but gender mismatch in year %d", y1+y);
            } else if (count[y] == -2){
                return String.format("Error: name not found in year %d.", y1+y);
            }

            int totalCount = DataParser.getTotalCount(y1+y, Gender.convertFromString(gender));
            if (totalCount == -1){
                return String.format("Error: totalCount not found in year %d", y1+y);
            }
            percentage[y] = count[y]*100.0/totalCount;
        }

        oReport += String.format("In the year of %d, ",y2);
        oReport += String.format("the number of birth with name %s is %d, ", name, count[y2-y1]);
        oReport += String.format("which represents %g percent of total %s births in %d. ", percentage[y2-y1], gender, y2);
        oReport += String.format("The year when the name %s was most popular is %d. ", name, y1+highest);
        oReport += String.format("In that year, the number of births is %d, ", count[highest]);
        oReport += String.format("which represents %g percent of the total %s birth in %d.\n\n", percentage[highest], gender, y1+highest);
        oReport += "Year\t\t|\t\tRank\t|\t\tCount\t\t|\t\tPercentage\n";
        for(int y = y2-y1; y >= 0; y--){
            oReport += String.format("%d\t\t|\t\t%d\t\t|\t\t%d\t\t|\t\t%g\n", y1+y, rank[y], count[y], percentage[y]);
        }

        return oReport;
    }
}