using System.Data;
using System.Web;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using UniversityWebsite.Models;
using static UniversityWebsite.Global;

namespace UniversityWebsite.App_Code
{
    public class HelperMethods : Page
    {
        private readonly DBHelperMethods myDBHelperMethods = new DBHelperMethods();

        public string CleanInput(string input)
        {
            // Replace single quote by two quotes and remove leading and trailing spaces.
            return input.Replace("'", "''").Trim();
        }

        public void DisplayMessage(Label labelControl, string message)
        {
            labelControl.ForeColor = Color.Red;  // Error message color.
            if (message != "" && message != null)
            {
                if (message.Substring(0, 3) != "***") // && message.Substring(0, 6) == "Please")
                { labelControl.ForeColor = Color.Blue; } // Information message.
                labelControl.Text = message;
            }
            else // Error message was not set; should not happen!
            { labelControl.Text = emptyOrNullErrorMessage + contact3311rep; }
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
                DisplayMessage(labelControl, "*** SQL error: The attribute " + attributeName + " is missing in the query result.");
            }
            return -1;
        }

        public bool IsQueryResultValid(string TODO, DataTable datatableToCheck, List<string> columnNames, Label labelControl)
        {
            bool isQueryResultValid = false;
            if (datatableToCheck != null)
            {
                if (datatableToCheck.Columns != null && datatableToCheck.Columns.Count == columnNames.Count)
                {
                    // Check that the first retrieved column is the first in the list of attributes.
                    if (datatableToCheck.Columns.IndexOf(columnNames[0]) == 0 || columnNames[0] == "ANYNAME")
                    {
                        isQueryResultValid = true;

                        // Check if the query result contains the required attributes.
                        foreach (string columnName in columnNames)
                        {
                            if ((!datatableToCheck.Columns.Contains(columnName)) && columnName != "ANYNAME")
                            {
                                DisplayMessage(labelControl, "*** The SELECT statement of " + TODO + " does not retrieve the attribute " + columnName + ".");
                                isQueryResultValid = false;
                                break;
                            }
                        }
                    }
                    else { DisplayMessage(labelControl, queryError + TODO + ": The attribute " + columnNames[0] + " is not the first attribute in the query result."); }
                }
                else { DisplayMessage(labelControl, "*** The SELECT statement of " + TODO + " retrieves " + datatableToCheck.Columns.Count + " attributes while the required number is " + columnNames.Count + "."); }
            }
            else { DisplayMessage(labelControl, sqlErrorMessage); } // An SQL error occurred.
            return isQueryResultValid;
        }

        public bool IsValidStudentId(string studentId, Label sqlError, Label noRecord)
        {
            bool result = true;
            decimal queryResult = myDBHelperMethods.RecordExists("Student", "studentId", studentId);

            if (queryResult == 0) // No student with the specified student id.
            {
                result = false;
                DisplayMessage(noRecord, nonexistentStudent);
            }
            else if (queryResult == -1) // An SQL error occurred.
            {
                result = false;
                DisplayMessage(sqlError, sqlErrorMessage);
            }
            return result;
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

            bool populateResult = true;

            // Populate the dropdown list with the dropdown list ids and names if the result is not null.
            if (IsQueryResultValid(TODO, dtDropDownListData, columnNames, lblQueryErrorMessage))
            {
                if (dtDropDownListData.Rows.Count != 0)
                {
                    ddlDropDownList.DataSource = dtDropDownListData;
                    ddlDropDownList.DataValueField = columnNames[0]; // The DataValueField is entry 0 in columnNames.
                    ddlDropDownList.DataTextField = columnNames[1];  // The DataTextField is entry 1 in columnNames.
                    ddlDropDownList.DataBind();
                    ddlDropDownList.Items.Insert(0, "-- Select --");
                    ddlDropDownList.Items.FindByText("-- Select --").Value = "none selected";
                    ddlDropDownList.SelectedIndex = 0;
                }
                else // The query result is empty; determine what message to show.
                {
                    switch (emptyResultMessageType)
                    {
                        case EmptyQueryResultMessageType.DBError:
                            DisplayMessage(lblQueryErrorMessage, "*** Database error" + queryResultMessage);
                            break;
                        case EmptyQueryResultMessageType.DBQueryError:
                            DisplayMessage(lblQueryErrorMessage, "*** Database/SQL error in " + TODO + queryResultMessage);
                            break;
                        case EmptyQueryResultMessageType.SQLError:
                            DisplayMessage(lblQueryErrorMessage, "*** SQL error in " + TODO + queryResultMessage);
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
            else { populateResult = false; } // An SQL error occurred.
            return populateResult;
        }

        public bool PopulateGridView(string TODO, GridView gv, DataTable resultDataTable, List<string> attributeList,
            Label lblQueryErrorMessage, Label lblEmptyQueryResultMessage, string emptyQueryResultMessage)
        {
            /* Parameters:
             * * 1. TODO - the number of the TODO that populates the dropdown list (format: "TODO 00").
             * * 2. gv - the name of the gridview control that is to be populated.
             * * 3. resultDataTable - a DataTable returned by the TODO query that is used to populate the gridview.
             * * 4. attributeList - the names of the columns in the returned DataTable that is used to populate the gridview.
             * * 5. lblQueryErrorMessage - the label in which to display a message if there is an error in the query and/or database.
             * * 6. lblEmptyQueryResultMessage - the label in which to display a message if the query result is empty.
             * * 7. emptyQueryResultMessage - the message to display if the query result is empty. */

            // Return false if an SQL error occurred.
            bool result = true;
            isEmptyQueryResult = false;
            if (IsQueryResultValid(TODO, resultDataTable, attributeList, lblQueryErrorMessage))
            {
                if (resultDataTable.Rows.Count != 0)
                {
                    gv.DataSource = resultDataTable;
                    gv.DataBind();
                    gv.Visible = true;
                }
                else // The query result is empty. 
                {
                    isEmptyQueryResult = true;
                    DisplayMessage(lblEmptyQueryResultMessage, emptyQueryResultMessage);
                }
            }
            else { result = false; }
            return result;
        }

        public void RenameGridViewColumn(GridViewRowEventArgs e, string fromName, string toName)
        {
            for (int i = 0; i < e.Row.Controls.Count; i++)
            { if (e.Row.Cells[i].Text.ToUpper().Trim() == fromName.ToUpper().Trim()) { e.Row.Cells[i].Text = toName; } }
        }

        public bool SynchLoginAndApplicationDatabases(string email, Literal literalControl)
        {
            bool synchResult = true;
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            // Get the role of the user.
            UniversityRole role = GetUserRole(email);
            ApplicationUser user = manager.FindByName(email);

            switch (role)
            {
                case UniversityRole.None:
                    // The user is not an admin or a student. If the user is in AspNetUsers, then delete him/her from AspNetUsers.
                    if (user != null) { manager.Delete(user); }
                    break;
                case UniversityRole.Student:
                    // If the user is in UniversityDB, but not in AspNetUsers, add the user to AspNetUsers in his/her specified role.
                    if (user == null)
                    {
                        user = new ApplicationUser() { UserName = email };
                        IdentityResult result = manager.Create(user, UniversityPassword);
                        if (result.Succeeded)
                        {
                            IdentityResult roleResult = manager.AddToRole(user.Id, role.ToString());
                            if (!roleResult.Succeeded)
                            {
                                manager.Delete(user);
                                literalControl.Text = "Cannot create role " + role.ToString() + " for user with email '" + email + "'." + contact3311rep;
                                synchResult = false;
                            }
                        }
                        else
                        {
                            literalControl.Text = ((string[])result.Errors)[0] + contact3311rep;
                            synchResult = false;
                        }
                    }
                    break;
                default:
                    // No action need for Admin role.
                    break;
            }
            return synchResult;
        }

        public UniversityRole GetUserRole(string email)
        {
            // If the user is neither an admin nor a student, return the None role.
            UniversityRole resultRole = UniversityRole.None;

            // If the username is admin, return the Admin role.
            if (email == "admin") { resultRole = UniversityRole.Admin; }

            // Else if the user is a student, return the Student role.
            else if (myDBHelperMethods.RecordExists(UniversityRole.Student.ToString(), "email", email) == 1)
            { resultRole = UniversityRole.Student; }

            return resultRole;
        }
    }
}