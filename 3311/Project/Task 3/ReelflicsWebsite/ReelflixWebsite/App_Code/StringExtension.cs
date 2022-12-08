using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ReelflicsWebsite.App_Code
{
    public static class StringExtension
    {
        public static string Age(this DateTime birthday)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - birthday.Year;
            if (now < birthday.AddYears(age))
                age--;
            return age.ToString();
        }

        public static string AgeAtDeath(this DateTime birthday, DateTime deathday)
        {
            int age = deathday.Year - birthday.Year;
            if (deathday < birthday.AddYears(age))
                age--;
            return age.ToString();
        }

        public static string CleanInput(this string text)
        {
            // Replace single quote by two quotes and remove leading and trailing spaces.
            return text.Replace("'", "''").Trim();
        }

        public static string CreateFileName(this string text)
        {
            return "-" + Truncate(
                ReplaceSpaceWithDash(RemoveDiacritics(StripPunctuation(text))),
                50).Replace("--", "-") + ".jpg";
        }

        public static string DataTableToCommaSeparatedText(DataTable dtInput, string columnName)
        {
            string result = "";
            for (int i = 0; i < dtInput.Rows.Count; i++)
            {
                result += dtInput.Rows[i][columnName].ToString();
                if (i < dtInput.Rows.Count - 1) { result += ", "; }
            }
            return result;
        }

        public static string DateIsValid(string date)
        {
            if (date.Trim() != "")
            {
                if (DateTime.TryParse(date, out DateTime resultDate)) { return resultDate.ToString("dd-MMM-yyyy"); }
            }
            return "";
        }

        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return text; }
            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public static string ReplaceSpaceWithDash(this string text)
        {
            return text.Replace(" ", "-").Trim();
        }

        public static string StripPunctuation(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)) { return text; }
            var sb = new StringBuilder();
            foreach (char c in text)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }

        public static string Truncate(this string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Length <= maxLength ? text : text.Substring(0, maxLength);
        }
    }
}