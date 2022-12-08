package comp3111.popnames;

import java.util.Optional;

/**
 * A class for computing the result for task 4.
 * @author Max
 */

public class NamesRecommendation {

    /**
     * Validates input from the application. Params info are the same as {@link #recommend(String, String, String, String, String)}
     * @return Error message, none if no errors is found
     */
    public static Optional<String> validation(String dName, String mName, String dYOB, String mYOB, String vYear) {
        if (!DataValidation.isValidYearString(dYOB) || !DataValidation.isValidYearString(mYOB) || !DataValidation.isValidYearString(vYear)) {
            return Optional.of(DataValidation.invalidYearString);
        }
        if (!DataValidation.isValidName(dName) || !DataValidation.isValidName(mName)) {
            return Optional.of(DataValidation.invalidName);
        }
        
        int dy, my, vin;
        dy = Integer.parseInt(dYOB);
        my = Integer.parseInt(mYOB);
        vin = Integer.parseInt(vYear);
        if (!DataValidation.checkYearInRange(dy) || !DataValidation.checkYearInRange(my) || !DataValidation.checkYearInRange(vin)) {
            return Optional.of(DataValidation.yearNotInRange);
        }
        return Optional.empty();
    }
    /**
     * Recommend baby boy's and girl's name based on dad's and mom's name and vintage year.
     * @param dad_name Name of baby's dad
     * @param mom_name Name of baby's mom
     * @param dadYOB Year of birth of baby's dad
     * @param momYOB Year of birth of baby's mum
     * @param vintage_year The vintage year
     * @return String containing the recommended names
     */
    public static String recommend(String dad_name, String mom_name, String dadYOB, String momYOB, String vintage_year) {
        if (vintage_year.isEmpty())
            vintage_year = "2019";
        
        Optional<String> err = validation(dad_name, mom_name, dadYOB, momYOB, vintage_year);
        if (err.isPresent())
            return err.get();
        
        int dadRank, momRank;
        int dy, my, vin;
        dy = Integer.parseInt(dadYOB);
        my = Integer.parseInt(momYOB);
        vin = Integer.parseInt(vintage_year);
        
        dadRank = DataParser.getRankSameFrequencySameRank(dy, dad_name, Gender.MALE);
        if (dadRank == -1) {
            return String.format("Error: dad's name found but gender mismatch in year %d", dy);
        } else if (dadRank == -2) {
            dadRank = 1;
        }

        momRank = DataParser.getRankSameFrequencySameRank(my, mom_name, Gender.FEMALE);
        if (momRank == -1) {
            return String.format("Error: mom's name found but gender mismatch in year %d", my);
        } else if (momRank == -2) {
            momRank = 1;
        }

        String boy = DataParser.getName(vin, dadRank, Gender.MALE);
        String girl = DataParser.getName(vin, momRank, Gender.FEMALE);
        return String.format("Recommended boy's name: %1$s\nRecommended girl's name: %2$s", boy, girl);
    }
}
