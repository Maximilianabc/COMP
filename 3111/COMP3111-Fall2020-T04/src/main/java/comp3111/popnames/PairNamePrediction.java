package comp3111.popnames;

import java.util.ArrayList;

/**
 * A class for computing name popularity within a period for task 5.
 * @author Frankie
 */

public class PairNamePrediction {

    /**
     * A public method called by Controller to predict the name of soulmate.
     * It is assumed that input is validated at controller class.
     * @param iName The name of the user
     * @param iGender The gender of the user
     * @param iYOB The Year of Birth of the user
     * @param iMateGender The genfer of the soulmate
     * @param iPreference The prefered age of the soulmaet (younger or older)
     * @return Error message or the soulmate name for the UI.
     */

    public static String predictName(String iName, String iGender, int iYOB, String iMateGender, String iPreference){
        int oYOB = iPreference.equals("Younger") ? iYOB-1: iYOB+1;
        if(oYOB < 1880 || oYOB > 2019) {
            return String.format("Error: Soulmate's Year of Birth %d is out of range (1880-2019)", oYOB);
        }
        String oReport = "Application 2\n";
        int oRank = 0;
        oRank = DataParser.getRankSameFrequencySameRank(iYOB, iName, Gender.convertFromString(iGender));
        if (oRank == -1){
            return String.format("Error: name found but gender mismatch in year %d", iYOB);
        } else if (oRank == -2){
            return String.format("Error: name not found in year %d.", iYOB);
        }
        ArrayList oName = DataParser.getNameSameFrequencySameRank(oYOB, oRank, Gender.convertFromString(iMateGender));
        if (oName.isEmpty()){
            oName = DataParser.getNameSameFrequencySameRank(oYOB, 1, Gender.convertFromString(iMateGender));
            oRank = 1; //magic number
        }
        oReport += String.format("Your %s soulmate name(s) is/are %s who ranked %d in year %d.", iMateGender, oName.toString(), oRank, oYOB);
        return oReport;
    }
}
