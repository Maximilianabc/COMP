namespace UniversityWebsite.App_Code
{
    public class DBHelperMethods
    {
        private readonly OracleDBAccess myOracleDBAccess = new OracleDBAccess();
        private string sql;

        public bool IsUnique(string tableName, string attributeName, string attributeValue)
        {
            sql = "select count(*) from " + tableName + " where " + attributeName + "='" + attributeValue + "'";
            if (myOracleDBAccess.GetAggregateValue("Method IsUnique", sql) == 0) { return true; }
            else { return false; }
        }

        public decimal RecordExists(string tableName, string attributeName, string value)
        {
            sql = "select count(*) from " + tableName + " where " + attributeName + "='" + value + "'";
            return myOracleDBAccess.GetAggregateValue("Method IsUserInRole", sql);
        }
    }
}