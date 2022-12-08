using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using UniversityWebsite.App_Code;
using static UniversityWebsite.Global;

namespace UniversityWebsite.Admin
{
    public partial class ModifyStudentRecord : System.Web.UI.Page
    {
        //*********************************
        // Uses TODO 01, TODO 02, TODO 05 *
        //*********************************

        private readonly UniversityDBAccess myUniversityDB = new UniversityDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly DBHelperMethods myDBHelperMethods = new DBHelperMethods();

        /*----- Private Methods -----*/

        private bool StudentIsChanged(string newFirstName, string newLastName, string newEmail, string newPhoneNo,
                                      string newCga, string newDepartment, string newAdmissionYear)
        {
            // Return true if any information in the student record has changed; else return false.
            bool result = true;

            // Handle cga specially since it is a decimal number (e.g., 3 and 3.0 should be equal).
            string currectCga = ViewState["currentCga"].ToString();
            if (currectCga != "") { currectCga = string.Format("{0:N2}", double.Parse(currectCga)); }
            if (newCga != "") { newCga = string.Format("{0:N2}", double.Parse(newCga)); }

            if (ViewState["currentFirstName"].ToString() == newFirstName && ViewState["currentLastName"].ToString() == newLastName &&
                ViewState["currentEmail"].ToString() == newEmail && ViewState["currentPhoneNo"].ToString() == newPhoneNo && currectCga == newCga &&
                ViewState["currentDepartment"].ToString() == newDepartment && ViewState["currentAdmissionYear"].ToString() == newAdmissionYear)
            {
                txtCga.Text = ViewState["currentCga"].ToString(); // Reset the cga to its current value to remove any edit that did not chnage its value.
                result = false;
            }
            return result;
        }

        /*----- Protected Methods -----*/

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnFindStudent_Click(object sender, EventArgs e)
        {
            // Hide previous messages and results.
            lblQueryResultMessage.Visible = lblNoStudentRecordMessage.Visible = pnlStudentRecord.Visible = false;

            string studentId = myHelpers.CleanInput(txtStudentId.Text);

            if (IsValid && myHelpers.IsValidStudentId(studentId, lblQueryResultMessage, lblNoStudentRecordMessage))
            {
                //*******************************************************************
                // Uses TODO 01 to get a student record identified by a student id. *
                //*******************************************************************
                DataTable dtStudentRecord = myUniversityDB.GetStudentRecord(studentId);

                // Show the student information if the query result is valid.
                if (myHelpers.IsQueryResultValid("TODO 01",
                                                 dtStudentRecord,
                                                 new List<string> { "STUDENTID", "FIRSTNAME", "LASTNAME", "EMAIL", 
                                                     "PHONENO", "CGA", "DEPARTMENTID", "ADMISSIONYEAR" },
                                                 lblQueryResultMessage))
                {
                    if (dtStudentRecord.Rows.Count == 1) // Only one record should be retrieved.
                    {
                        //*********************************************************
                        // Uses TODO 02 to populate the department dropdown list. *
                        //*********************************************************
                        if (myHelpers.PopulateDropDownList("TODO 02", ddlDepartments, myUniversityDB.GetDepartments(),
                                                           new List<string> { "DEPARTMENTID", "DEPARTMENTNAME" },
                                                           lblQueryResultMessage, lblQueryResultMessage,
                                                           dbqueryErrorNoDepartments,
                                                           EmptyQueryResultMessageType.DBQueryError))
                        {   // Populate the webform with the retrieved student information and save it in ViewState for determining if changes were made.
                            ViewState["currentFirstName"] = txtFirstName.Text = dtStudentRecord.Rows[0]["FIRSTNAME"].ToString().Trim();
                            ViewState["currentLastName"] = txtLastName.Text = dtStudentRecord.Rows[0]["LASTNAME"].ToString().Trim();
                            ViewState["currentEmail"] = txtEmail.Text = dtStudentRecord.Rows[0]["EMAIL"].ToString().Trim();
                            ViewState["currentPhoneNo"] = txtPhoneNo.Text = dtStudentRecord.Rows[0]["PHONENO"].ToString();
                            ViewState["currentCga"] = txtCga.Text = dtStudentRecord.Rows[0]["CGA"].ToString();
                            ViewState["currentDepartment"] = ddlDepartments.SelectedValue = dtStudentRecord.Rows[0]["DEPARTMENTID"].ToString();
                            ViewState["currentAdmissionYear"] = txtAdmissionYear.Text = dtStudentRecord.Rows[0]["ADMISSIONYEAR"].ToString();
                            pnlStudentRecord.Visible = true;
                        }
                    }
                    else
                    {
                        if (dtStudentRecord.Rows.Count > 1)
                        {
                            myHelpers.DisplayMessage(lblQueryResultMessage,
                                                     $"{queryError}TODO 01{queryErrorMultipleRecordsRetrieved}");
                        } // Multiple records were retrieved.
                        else {
                            myHelpers.DisplayMessage(lblQueryResultMessage,
                                                     $"{dbqueryError}TODO 01{dbqueryErrorNoRecordsRetrieved}");
                        } // No record was retrieved.
                    }
                }
            }
        }

        protected void CvUserEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string email = myHelpers.CleanInput(txtEmail.Text);
            if (email != ViewState["currentEmail"].ToString())
            {
                if (email == "admin" || !myDBHelperMethods.IsUnique("Student", "email", email)) { args.IsValid = false; } // The email already exists.
            }
        }

        protected void ModifyStudent_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Hide previous messages.
                lblQueryResultMessage.Visible = lblNoStudentRecordMessage.Visible = lblNoInformationChangedMessage.Visible = false;

                // Collect the student information for updating.
                string studentId = txtStudentId.Text;
                string newFirstName = myHelpers.CleanInput(txtFirstName.Text);
                string newLastName = myHelpers.CleanInput(txtLastName.Text);
                string newEmail = myHelpers.CleanInput(txtEmail.Text);
                string newPhoneNo = txtPhoneNo.Text;
                string newCga = txtCga.Text;
                string newDepartmentId = ddlDepartments.SelectedValue.ToString();
                string newAdmissionYear = txtAdmissionYear.Text;

                // Update the student record if it has changed.
                if (StudentIsChanged(newFirstName, newLastName, newEmail, newPhoneNo, newCga, newDepartmentId, newAdmissionYear))
                {
                    // Set no cga input to the null value.
                    if (newCga == "") { newCga = "null"; }

                    //*******************************************
                    // Uses TODO 05 to update a student record. *
                    //*******************************************
                    if (myUniversityDB.ModifyStudentRecord(studentId,
                                                           newFirstName,
                                                           newLastName,
                                                           newEmail,
                                                           newPhoneNo,
                                                           newCga,
                                                           newDepartmentId,
                                                           newAdmissionYear))
                    {
                        myHelpers.DisplayMessage(lblQueryResultMessage, informationUpdated);
                        pnlSearch.Visible = pnlStudentRecord.Visible = false;
                    }
                    else { myHelpers.DisplayMessage(lblQueryResultMessage, sqlErrorMessage); } // An SQL error occurred.
                }
                else { myHelpers.DisplayMessage(lblNoInformationChangedMessage, informationNotChanged); } // Nothing was changed.
            }
        }
    }
}