public class StringLab {

    /**
     * @param str The input string
     * @return The reversed string
     */
    public String reverseString(String str) {
        return new StringBuilder().append(str).reverse().toString();
    }

    /**
     * Makes all characters before the index value uppercase, makes all characters on the index and afterwards
     * lowercase. See test cases for a better understanding.
     *
     * @param str   The input string
     * @param index All character positions smaller than index must be uppercase. All character positions greater
     *              than index must be lowercase.
     * @return The new string
     */
    public String capitalizeAndMakeLowercase(String str, int index) {
        StringBuilder sb = new StringBuilder().append(str);
        for (int i = 0; i < sb.length(); i++)
        {
            sb.setCharAt(i, i < index
                    ? Character.toUpperCase(sb.charAt(i))
                    : Character.toLowerCase(sb.charAt(i)));
        }
        return sb.toString();
    }

    /**
     * Counts the number of vowels in a string.
     *
     * @param str The input string
     * @return The number of vowels
     */
    public long countVowels(String str) {
        long count = 0;
        for (int i = 0; i < str.length(); i++)
        {
            char c = str.charAt(i);
            if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' ||
                c == 'A' ||c == 'E' ||c == 'I' ||c == 'O' ||c  == 'U')
            {
                count++;
            }
        }
        return count;
    }

    /**
     * Removes a certain letter from a string
     *
     * @param str The input string
     * @param a   The letter to remove
     * @return The input string without the specified letter
     */
    public String removeLetter(String str, char a) {
        return str.replace(Character.toString(a), "");
    }

    /**
     * Checks if a string is a palindrome
     *
     * @param str The string to check
     * @return Whether or not the string is a palindrome
     */
    public boolean isPalindrome(String str) {
        return new StringBuilder().append(str).reverse().toString().contentEquals(str);
    }
}
