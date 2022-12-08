package comp3111.popnames;

/**
 * An enum to represent gender "male" / "female"
 * so that we don't have to use string to represent it all around the program.
 * @author Maisy 
 */

public enum Gender {
    MALE, 
    FEMALE
    ;
    /**
     * String (in lower case) to represent the two gender.
     */
    private static String[] gender_in_string_all_lower = {"MALE", "FEMALE"};
    
    /**
     * Symbol (a upper case letter) to represent the two gender. M for male and F for female.
     */
    private static String[] gender_symbol = {"M", "F"};

    /**
     * Get the corresponding Gender type given the string representing the gender. (Maisy)
     * @param gender_string The string representing the gender. (e.g. Male/male and Female/female)
     * @return The corresponding Gender.
     */
    public static Gender convertFromString(String gender_string){
        gender_string = gender_string.trim().toUpperCase();
        if (gender_string.equals(gender_in_string_all_lower[0]) || gender_string.equals(gender_symbol[0])){
            return MALE;
        }
        if (gender_string.equals(gender_in_string_all_lower[1]) || gender_string.equals(gender_symbol[1])){
            return FEMALE;
        }
        throw new IllegalArgumentException("Gender is only used to represent male or female.");
    }
    
    /**
     * Convert the Gender into a symbol (a upper case letter) to represent the gender. (Maisy)
     * @return The symbol (a upper case letter) to represent the gender.
     */
    public String convertToSymbol(){
        if (this == MALE){
            return gender_symbol[0];
        }
        return gender_symbol[1];
    }

    /**
     * An alternate method for {@link Gender#convertFromString(String)} using switch expression. (Max)
     * @param gender The string representing the gender.
     * @return The corresponding Gender.
     * @throws IllegalArgumentException if the gender string is not valid.
     */
    public static Gender fromString(String gender)
    {
        // return switch (gender.toLowerCase().trim())
        // {
        //     case "male" -> MALE;
        //     case "female" -> FEMALE;
        //     default -> throw new IllegalArgumentException("Gender is only used to represent male or female.");
        // };
        return convertFromString(gender);
    }

    /**
     * Override {@link Enum#toString()} to return gender symbol instead. (Max)
     * @return The gender symbol.
     * @throws IllegalArgumentException if the gender enum value is invalid.
     */
    @Override
    public String toString()
    {
        // return switch (this)
        // {
        //     case MALE -> "M";
        //     case FEMALE -> "F";
        //     default -> throw new IllegalStateException("Unknown value for gender enum.");
        // };
        return convertToSymbol();
    }
}
