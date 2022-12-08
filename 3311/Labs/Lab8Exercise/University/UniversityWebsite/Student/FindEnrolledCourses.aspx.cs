using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using UniversityWebsite.App_Code;
using static UniversityWebsite.Global;

namespace UniversityWebsite.Enrollment
{
    public partial class FindEnrolledCourses : Page
    {
        //***************
        // Uses TODO 06 *
        //***************

        private readonly UniversityDBAccess myUniversityDB = new UniversityDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly string currentUserEmail = HttpContext.Current.User.Identity.Name;

        /*----- Private Methods -----*/

        private void GetEnrolledCourses(string email)
        {
            // Hide previous messages and results.
            //pnlEnrolledCourses.Visible = false;

            //***************************************************************************************
            // Uses TODO 06 to populate a gridview with the courses in which a student is enrolled. *
            //***************************************************************************************
            if (myHelpers.PopulateGridView("TODO 06",
                                           gvEnrolledCourses,
                                           myUniversityDB.GetEnrolledCourses(email),
                                           new List<string> { "COURSEID", "COURSENAME", "GRADE", "CREDITS", "INSTRUCTOR" },
                                           lblQueryResultMessage,
                                           lblQueryResultMessage,
                                           noEnrolledCourses))
            {
                if (isEmptyQueryResult) { lblQueryResultMessage.Visible = true; }
                else { pnlEnrolledCourses.Visible = true; }
            }
        }

        /*----- Protected Methods -----*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { GetEnrolledCourses(currentUserEmail); }
        }

        protected void GvEnrolledCourses_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 5)
            {
                // GridView columns: COURSEID-0, COURSENAME-1, GRADE-2, CREDITS-3, INSTRUCTOR-4
                int gradeColumn = myHelpers.GetGridViewColumnIndexByName(sender, "GRADE", lblQueryResultMessage);     // index 2
                int creditsColumn = myHelpers.GetGridViewColumnIndexByName(sender, "CREDITS", lblQueryResultMessage); // index 3

                if (gradeColumn != -1 && creditsColumn != -1)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "COURSEID", "ID");
                        myHelpers.RenameGridViewColumn(e, "COURSENAME", "NAME");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[gradeColumn].HorizontalAlign = HorizontalAlign.Center;
                        e.Row.Cells[creditsColumn].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
            }
        }
    }
}