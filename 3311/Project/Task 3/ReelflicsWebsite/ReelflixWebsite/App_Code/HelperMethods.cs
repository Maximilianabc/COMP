using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using static ReelflicsWebsite.Global;


namespace ReelflicsWebsite.App_Code
{
    public class HelperMethods
    {
        public DataTable AddDataTableColumnWithDefaultValue(DataTable dt, string columnName, string columnValue)
        {
            DataColumn newColumn = new DataColumn(columnName, typeof(string));
            newColumn.DefaultValue = columnValue;
            dt.Columns.Add(newColumn);
            return dt;
        }

        public void DisplayMessage(Label labelControl, string message)
        {
            labelControl.ForeColor = Color.Red;  // Error message color.
            if (!string.IsNullOrEmpty(message))
            {
                if (message.Substring(0, 3) != "***")
                { labelControl.ForeColor = Color.BlanchedAlmond; } // Information message color.
                labelControl.Text = message;
            }
            else // Error message was not set; should not happen!
            { labelControl.Text = emptyOrNullErrorMessage; }
            labelControl.Visible = true;
        }

        public int GetGridViewColumnIndexByName(object sender, string attributeName, Label labelControl)
        {
            DataTable dt = ((DataTable)((GridView)sender).DataSource);
            if (dt != null)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.ToUpper().Trim() == attributeName.ToUpper().Trim()) { return i; }
                }
                DisplayMessage(labelControl, $"*** SQL error: The attribute {attributeName} is missing in the query result.");
            }
            return -1;
        }

        public bool IsQueryResultValid(string TODO, DataTable datatableToCheck, List<string> columnNames, Label labelControl)
        {
            bool result = false;
            if (datatableToCheck != null)
            {
                if (datatableToCheck.Columns != null && datatableToCheck.Columns.Count == columnNames.Count)
                {
                    // Check that the first retrieved column is the first in the list of attributes.
                    if (datatableToCheck.Columns.IndexOf(columnNames[0]) == 0 || columnNames[0] == "ANYNAME")
                    {
                        result = true;

                        // Check if the query result contains the required attributes.
                        foreach (string columnName in columnNames)
                        {
                            if ((!datatableToCheck.Columns.Contains(columnName)) && columnName != "ANYNAME")
                            {
                                DisplayMessage(labelControl, $"*** The SELECT statement of {TODO} does not retrieve the attribute {columnName}.");
                                result = false;
                                break;
                            }
                        }
                    }
                    else { DisplayMessage(labelControl, $"{queryError}{TODO}: The attribute {columnNames[0]} must be the first attribute in the query result."); }
                }
                else { DisplayMessage(labelControl, $"*** The SELECT statement of {TODO} retrieves {datatableToCheck.Columns.Count} attributes while the required number is {columnNames.Count}."); }
            }
            else { DisplayMessage(labelControl, sqlErrorMessage); } // An SQL error occurred.
            return result;
        }

        public bool IsInteger(string number)
        { return int.TryParse(number, out _); }

        public bool IsRecordInDataTable(DataTable dt, string attributeName, string attributeValue)
        {
            // If a record with value attributeValue for attribute attributeName exists in DataTable dt return true; else return false.
            bool result = false;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                { if (row[attributeName].ToString() == attributeValue) { result = true; break; } }
            }
            return result;
        }

        public bool IsValidAndInRange(string number, decimal min, decimal max)
        {
            if (decimal.TryParse(number, out decimal n))
            {
                if (min <= n && n <= max) { return true; }
            }
            return false;
        }

        public int GetColumnIndexByName(GridViewRowEventArgs e, string columnName)
        {
            for (int i = 0; i < e.Row.Controls.Count; i++)
                if (e.Row.Cells[i].Text.ToLower().Trim() == columnName.ToLower().Trim()) { return i; }
            return -1;
        }

        public bool PopulateDropDownList(string TODO, DropDownList ddlDropDownList, DataTable dtDropDownListData, List<string> columnNames,
            Label lblQueryErrorMessage, Label lblEmptyQueryResultMessage, string queryResultMessage, EmptyQueryResultMessageType emptyResultMessageType)
        {
            /* Parameters:
             * 1. TODO - the number of the TODO that populates the dropdown list (format: "TODO 00").
             * 2. ddlDropDownList - the name of the dropdown list control that is to be populated.
             * 3. dtDropDownListData - a DataTable returned by the TODO query that is used to populate the dropdown list.
             * 4. columnNames - the names of the columns in the returned DataTable that is used to populate the dropdown list.
             * 5. lblQueryErrorMessage - the label in which to display a message if there is an error in the query and/or database.
             * 6. lblEmptyQueryResultMessage - the label in which to display a message if the query result is empty.
             * 7. queryResultMessage - the message to display, if any, indicating the result of a query.
             * 8. emptyResultMessageType - the type of message if the result is empty; one of { DBError, DBQueryError, SQLError, Information }. */

            bool populateResult = false;
            // Populate the dropdown list with the dropdown list ids and names if the result is not null.
            if (IsQueryResultValid(TODO, dtDropDownListData, columnNames, lblQueryErrorMessage))
            {
                if (dtDropDownListData.Rows.Count != 0)
                {
                    ddlDropDownList.DataSource = dtDropDownListData;
                    ddlDropDownList.DataValueField = columnNames[0]; // The DataValueField is entry 0 in columnNames.
                    if (columnNames.Count == 1) { ddlDropDownList.DataTextField = columnNames[0]; } // The DataTextField is entry 0 in columnNames.
                    else { ddlDropDownList.DataTextField = columnNames[1]; } // The DataTextField is entry 1 in columnNames.
                    ddlDropDownList.DataBind();
                    ddlDropDownList.Items.Insert(0, "-- Select --");
                    ddlDropDownList.Items.FindByText("-- Select --").Value = "none selected";
                    ddlDropDownList.SelectedIndex = 0;
                    populateResult = true;
                }
                else // The query result is empty; determine what message to show.
                {
                    switch (emptyResultMessageType)
                    {
                        case EmptyQueryResultMessageType.DBError:
                            DisplayMessage(lblQueryErrorMessage, $"*** Database error{queryResultMessage}");
                            break;
                        case EmptyQueryResultMessageType.DBQueryError:
                            DisplayMessage(lblQueryErrorMessage, $"*** Database/SQL error in {TODO}{queryResultMessage}");
                            break;
                        case EmptyQueryResultMessageType.SQLError:
                            DisplayMessage(lblQueryErrorMessage, $"*** SQL error in {TODO}{queryResultMessage}");
                            break;
                        case EmptyQueryResultMessageType.Information:
                            DisplayMessage(lblEmptyQueryResultMessage, queryResultMessage);
                            break;
                        default:
                            DisplayMessage(lblQueryErrorMessage, "*** Internal error in HelperMethods - PopulateDropDownList. Please contact 3311rep.");
                            break;
                    }
                }
            }
            return populateResult;
        }

        public bool PopulateGridView(string TODO, GridView gv, DataTable resultDataTable, List<string> attributeList,
            Label lblSQLQueryErrorMessage, Label lblEmptyQueryResultMessage, string emptyQueryResultMessage)
        {
            /* Parameters:
             * * 1. TODO - the number of the TODO that populates the dropdown list (format: "TODO 00").
             * * 2. gv - the name of the gridview control that is to be populated.
             * * 3. resultDataTable - a DataTable returned by the TODO query that is used to populate the gridview.
             * * 4. attributeList - the names of the columns in the returned DataTable that is used to populate the gridview.
             * * 5. lblQueryErrorMessage - the label in which to display a message if there is an error in the query and/or database.
             * * 6. lblEmptyQueryResultMessage - the label in which to display a message if the query result is empty.
             * * 7. emptyQueryResultMessage - the message to display if the query result is empty. */

            bool result = false;
            isEmptyQueryResult = false;
            if (IsQueryResultValid(TODO, resultDataTable, attributeList, lblSQLQueryErrorMessage))
            {
                gv.DataSource = resultDataTable;
                gv.DataBind();
                if (resultDataTable.Rows.Count != 0)
                {
                    gv.Visible = true;
                }
                else // The query result is empty. 
                {
                    if (emptyQueryResultMessage != null) { DisplayMessage(lblEmptyQueryResultMessage, emptyQueryResultMessage); }
                    isEmptyQueryResult = true;
                }
                result = true;
            }
            return result;
        }

        public bool PopulateListBox(string TODO, ListBox lstListBox, DataTable dtListBoxData, List<string> columnNames,
            Label lblQueryErrorMessage, Label lblEmptyQueryResultMessage, string queryResultMessage, EmptyQueryResultMessageType emptyResultMessageType)
        {
            /* Parameters:
             * 1. TODO - the number of the TODO that populates the dropdown list (format: "TODO 00").
             * 2. lstListBox - the name of the list box control that is to be populated.
             * 3. dtListBoxData - a DataTable returned by the TODO query that is used to populate the list box.
             * 4. columnNames - the names of the columns in the returned DataTable that is used to populate the list box.
             * 5. lblQueryErrorMessage - the label in which to display a message if there is an error in the query and/or database.
             * 6. lblEmptyQueryResultMessage - the label in which to display a message if the query result is empty.
             * 7. queryResultMessage - the message to display, if any, indicating the result of a query.
             * 8. emptyResultMessageType - the type of message if the result is empty; one of { DBError, DBQueryError, SQLError, Information }. */

            bool populateResult = true;
            // Populate the dropdown list with the dropdown list ids and names if the result is not null.
            if (IsQueryResultValid(TODO, dtListBoxData, columnNames, lblQueryErrorMessage))
            {
                if (dtListBoxData.Rows.Count != 0)
                {
                    lstListBox.DataSource = dtListBoxData;
                    lstListBox.DataValueField = columnNames[0]; // The DataValueField is entry 0 in columnNames.
                    if (columnNames.Count == 1) { lstListBox.DataTextField = columnNames[0]; } // The DataTextField is entry 0 in columnNames.
                    else { lstListBox.DataTextField = columnNames[1]; } // The DataTextField is entry 1 in columnNames.
                    lstListBox.DataBind();
                }
                else // The query result is empty; determine what message to show.
                {
                    switch (emptyResultMessageType)
                    {
                        case EmptyQueryResultMessageType.DBError:
                            DisplayMessage(lblQueryErrorMessage, $"*** Database error{queryResultMessage}");
                            break;
                        case EmptyQueryResultMessageType.DBQueryError:
                            DisplayMessage(lblQueryErrorMessage, $"*** Database/SQL error in {TODO}{queryResultMessage}");
                            break;
                        case EmptyQueryResultMessageType.SQLError:
                            DisplayMessage(lblQueryErrorMessage, $"*** SQL error in {TODO}{queryResultMessage}");
                            break;
                        case EmptyQueryResultMessageType.Information:
                            DisplayMessage(lblEmptyQueryResultMessage, queryResultMessage);
                            break;
                        default:
                            DisplayMessage(lblQueryErrorMessage, "*** Internal error in HelperMethods - PopulateDropDownList. Please contact 3311rep.");
                            break;
                    }
                    populateResult = false;
                }
            }
            else // An SQL error occurred.
            { populateResult = false; }
            return populateResult;
        }

        public DataTable RemoveDataTableRecord(DataTable dt, string attributeName, string attributeValue, string condition)
        {
            // If the value of attributename in the row of DataTable dt meets the specified condition for attributeValue, then remove the record.
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (condition == equal)
                {
                    if (dr[attributeName].ToString() == attributeValue) { dr.Delete(); }
                }
                else if (condition == notequal)
                {
                    if (dr[attributeName].ToString() != attributeValue) { dr.Delete(); }
                }
            }
            dt.AcceptChanges();
            return dt;
        }

        public void RenameGridViewColumn(GridViewRowEventArgs e, string fromName, string toName)
        {
            for (int i = 0; i < e.Row.Controls.Count; i++)
            { if (e.Row.Cells[i].Text.ToUpper().Trim() == fromName.ToUpper().Trim()) 
                { e.Row.Cells[i].Text = toName; } }
        }

        public void SortDropdownList(ListControl control, bool isAscending)
        {
            List<ListItem> collection;

            if (isAscending)
                collection = control.Items.Cast<ListItem>()
                    .Select(x => x)
                    .OrderBy(x => x.Text)
                    .ToList();
            else
                collection = control.Items.Cast<ListItem>()
                    .Select(x => x)
                    .OrderByDescending(x => x.Text)
                    .ToList();

            control.Items.Clear();

            foreach (ListItem item in collection)
                control.Items.Add(item);
        }

        public DataTable SortGridview(GridView gv, DataTable dt, string SortExpression, string direction)
        {
            DataTable dtsort = dt;
            DataView dv = new DataView(dtsort) { Sort = SortExpression + direction };
            dt = dv.ToTable();
            gv.DataSource = dt;
            gv.DataBind();
            return dt;
        }
    }
}