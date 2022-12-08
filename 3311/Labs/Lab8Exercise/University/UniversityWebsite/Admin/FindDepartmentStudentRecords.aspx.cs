using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using UniversityWebsite.App_Code;
using static UniversityWebsite.Global;

namespace UniversityWebsite.Student
{
    public partial class FindDepartmentStudentRecords : Page
    {
        //************************
        // Uses TODO 02, TODO 03 *
        //************************

        private readonly UniversityDBAccess myUniversityDB = new UniversityDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /*----- Protected Methods -----*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                { pnlSearchCriteria.Visible = true; }
            }
        }

        protected void DdlDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hide previous messages and results.
            lblQueryResultMessage.Visible = lblNoStudentsEnrolledMessage.Visible = pnlSearchResult.Visible = false;

            Page.Validate();

            if (IsValid)
            {
                // Get the department id from the dropdown list.
                string departmentId = ddlDepartments.SelectedItem.Value;

                //*****************************************************************************************************************
                // Uses TODO 03 to populate a gridview with the student records for a department identified by its department id. *
                //*****************************************************************************************************************
                if (myHelpers.PopulateGridView("TODO 03",
                                               gvFindStudentRecordsResult,
                                               myUniversityDB.GetDepartmentStudentRecords(departmentId),
                                               new List<string> { "STUDENTID", "LASTNAME", "FIRSTNAME", "EMAIL", "CGA" },
                                               lblQueryResultMessage,
                                               lblNoStudentsEnrolledMessage,
                                               noEnrolledStudents))
                {
                    if (isEmptyQueryResult) { lblNoStudentsEnrolledMessage.Visible = true; }
                    else { pnlSearchResult.Visible = true; }
                }
            }
        }

        protected void GvFindStudentRecordsResult_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 5)
            {
                // GridView columns: STUDENTID-0, LASTNAME-1, FIRSTNAME-2, EMAIL-3, CGA-4
                int studentIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "STUDENTID", lblQueryResultMessage);  // index 0
                int cgaColumn = myHelpers.GetGridViewColumnIndexByName(sender, "CGA", lblQueryResultMessage);              // index 4

                if (studentIdColumn != -1 && cgaColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "STUDENTID", "ID");
                        myHelpers.RenameGridViewColumn(e, "LASTNAME", "LAST NAME");
                        myHelpers.RenameGridViewColumn(e, "FIRSTNAME", "FIRST NAME");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[studentIdColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[cgaColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }
    }
}