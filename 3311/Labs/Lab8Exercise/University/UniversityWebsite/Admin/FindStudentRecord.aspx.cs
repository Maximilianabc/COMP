using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using UniversityWebsite.App_Code;
using static UniversityWebsite.Global;

namespace UniversityWebsite.Student
{
    public partial class FindStudentRecord : Page
    {
        //***************
        // Uses TODO 01 *
        //***************

        private readonly UniversityDBAccess myUniversityDB = new UniversityDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /*----- Protected Methods -----*/

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnFindStudentRecord_Click(object sender, EventArgs e)
        {
            // Hide previous messages and results.
            lblQueryResultMessage.Visible = lblNoStudentRecordMessage.Visible = pnlStudentRecord.Visible = false;

            string studentId = myHelpers.CleanInput(txtStudentId.Text);

            if (IsValid && myHelpers.IsValidStudentId(studentId, lblQueryResultMessage, lblNoStudentRecordMessage))
            {
                //*************************************************************
                // Uses TODO 01 to populate a gridview with a student record. *
                //*************************************************************
                if (myHelpers.PopulateGridView("TODO 01", gvStudentRecord, myUniversityDB.GetStudentRecord(studentId),
                    new List<string> { "STUDENTID", "FIRSTNAME", "LASTNAME", "EMAIL", "PHONENO", "CGA",
                        "DEPARTMENTID", "ADMISSIONYEAR" }, lblQueryResultMessage, lblQueryResultMessage,
                        dbError + "TODO 01: " + dbErrorNoStudentRecord))
                {
                    if (!isEmptyQueryResult)
                    {
                        if (gvStudentRecord.Rows.Count > 1) // Multiple records were retrieved.
                        {
                            myHelpers.DisplayMessage(lblQueryResultMessage, queryError + "TODO 01"
                              + queryErrorMultipleRecordsRetrieved);
                        }
                        else { pnlStudentRecord.Visible = true; } // Only one record was retrieved.
                    }
                }
            }
        }

        protected void gvStudentRecord_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 8)
            {
                // GridView columns: STUDENTID-0, LASTNAME-1, FIRSTNAME-2, EMAIL-3, PHONENO-4, CGA-5, DEPARTMENTID-6, ADMISSIONYEAR-7
                int studentIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "STUDENTID", lblQueryResultMessage);         // index 0
                int cgaColumn = myHelpers.GetGridViewColumnIndexByName(sender, "CGA", lblQueryResultMessage);                     // index 5
                int admissionYearColumn = myHelpers.GetGridViewColumnIndexByName(sender, "ADMISSIONYEAR", lblQueryResultMessage); // index 7

                if (studentIdColumn != -1 && cgaColumn != -1 && admissionYearColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "STUDENTID", "ID");
                        myHelpers.RenameGridViewColumn(e, "LASTNAME", "LAST NAME");
                        myHelpers.RenameGridViewColumn(e, "FIRSTNAME", "FIRST NAME");
                        myHelpers.RenameGridViewColumn(e, "PHONENO", "PHONE");
                        myHelpers.RenameGridViewColumn(e, "DEPARTMENTID", "DEPT");
                        myHelpers.RenameGridViewColumn(e, "ADMISSIONYEAR", "ADMIT YEAR");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[studentIdColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[cgaColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[admissionYearColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }
    }
}