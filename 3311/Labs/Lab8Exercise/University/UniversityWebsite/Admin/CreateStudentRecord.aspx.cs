using System;
using System.Web.UI;
using System.Collections.Generic;
using UniversityWebsite.App_Code;
using static UniversityWebsite.Global;


namespace UniversityWebsite.Admin
{
    public partial class CreateStudentRecord : Page
    {
        //************************
        // Uses TODO 02, TODO 04 *
        //************************

        private readonly UniversityDBAccess myUniversityDB = new UniversityDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly DBHelperMethods myDBHelperMethods = new DBHelperMethods();

        /*----- Protected Methods -----*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the default admission year to the current year.
                txtAdmissionYear.Text = DateTime.Now.Year.ToString();

                //*********************************************************
                // Uses TODO 02 to populate the department dropdown list. *
                //*********************************************************
                if (myHelpers.PopulateDropDownList("TODO 02",
                                                   ddlDepartments,
                                                   myUniversityDB.GetDepartments(),
                                                   new List<string> { "DEPARTMENTID", "DEPARTMENTNAME" },
                                                   lblQueryResultMessage,
                                                   lblQueryResultMessage,
                                                   dbqueryErrorNoDepartments,
                                                   EmptyQueryResultMessageType.DBQueryError))
                { pnlStudentRecord.Visible = true; }
            }
        }

        protected void CreateStudent_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Hide previous result message.
                lblQueryResultMessage.Visible = false;

                // Collect and validate the student information.
                string studentId = myHelpers.CleanInput(txtStudentId.Text);
                string email = myHelpers.CleanInput(txtEmail.Text);
                string firstName = myHelpers.CleanInput(txtFirstName.Text);
                string lastName = myHelpers.CleanInput(txtLastName.Text);
                string phoneNo = myHelpers.CleanInput(txtPhoneNo.Text);
                string cga = "null";
                string departmentId = ddlDepartments.SelectedValue;
                string admissionYear = myHelpers.CleanInput(txtAdmissionYear.Text);

                //*******************************************
                // Uses TODO 04 to insert a student record. *
                //*******************************************
                if (myUniversityDB.CreateStudentRecord(studentId,
                                                       firstName,
                                                       lastName,
                                                       email,
                                                       phoneNo,
                                                       cga,
                                                       departmentId,
                                                       admissionYear))
                {
                    myHelpers.DisplayMessage(lblQueryResultMessage, studentRecordCreated);
                    pnlStudentRecord.Visible = false;
                }
                else { myHelpers.DisplayMessage(lblQueryResultMessage, sqlErrorMessage); } // An SQL error occurred.
            }
        }

        protected void CvStudentId_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            if (IsValid)
            { if (!myDBHelperMethods.IsUnique("Student", "studentId", txtStudentId.Text)) { args.IsValid = false; } }
        }

        protected void CvUserEmail_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            if (txtEmail.Text == "admin" || !myDBHelperMethods.IsUnique("Student", "email", txtEmail.Text)) { args.IsValid = false; }
        }
    }
}