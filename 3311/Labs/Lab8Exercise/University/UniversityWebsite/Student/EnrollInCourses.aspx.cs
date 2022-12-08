using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using UniversityWebsite.App_Code;
using static UniversityWebsite.Global;

namespace UniversityWebsite.Enrollment
{
    public partial class EnrollInCourses : Page
    {
        //*********************************
        // Uses TODO 07, TODO 08, TODO 09 *
        //*********************************

        private readonly UniversityDBAccess myUniversityDB = new UniversityDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly string currentUserEmail = HttpContext.Current.User.Identity.Name;

        /*----- Private Methods -----*/

        private void FindAvailableCourses(string email)
        {
            // Hide previous messages and results.
            pnlAvailableCourses.Visible = btnSubmit.Visible = lblQueryResultMessage.Visible = lblNoAvailableCoursesMessage.Visible = false;

            //**************************************************************************************
            // Uses TODO 07 to populate a gridview with the courses in which a student can enroll. *
            //**************************************************************************************
            if (myHelpers.PopulateGridView("TODO 07",
                                           gvAvailableCourses,
                                           myUniversityDB.GetCoursesAvailableToEnrollIn(email),
                                           new List<string> { "COURSEID", "COURSENAME", "CREDITS", "INSTRUCTOR" },
                                           lblQueryResultMessage,
                                           lblNoAvailableCoursesMessage,
                                           noAvailableCourses))
            {
                pnlAvailableCourses.Visible = true;
                if (isEmptyQueryResult) { lblNoAvailableCoursesMessage.Visible = true; }
                else { gvAvailableCourses.Visible = btnSubmit.Visible = true; }
            }
        }

        /*----- Protected Methods -----*/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { FindAvailableCourses(currentUserEmail); }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Hide previous messages.
            lblQueryResultMessage.Visible = false;

            if (IsValid)
            {
                //***************************************************************
                // Uses TODO 08 to get the student id of the logged in student. *
                //***************************************************************
                DataTable dtStudentId = myUniversityDB.GetStudentId(currentUserEmail);

                // Display the query result if it is valid.
                if (myHelpers.IsQueryResultValid("TODO 08",
                                                 dtStudentId,
                                                 new List<string> { "STUDENTID" },
                                                 lblQueryResultMessage))
                {
                    if (dtStudentId.Rows.Count == 1)
                    {
                        string studentId = dtStudentId.Rows[0]["STUDENTID"].ToString();
                        int coursesEnrolled = 0;

                        // Search each row of the GridView to determine if any courses were selected.
                        foreach (GridViewRow row in gvAvailableCourses.Rows)
                        {
                            if (row.RowType == DataControlRowType.DataRow)
                            {
                                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                                if (chkRow != null && chkRow.Checked)
                                {
                                    // Get the course id of the selected course.
                                    string courseId = myHelpers.CleanInput(row.Cells[1].Text);

                                    //************************************************
                                    // Uses TODO 09 to enroll a student in a course. *
                                    //************************************************
                                    if (!myUniversityDB.EnrollInCourses(studentId, courseId))
                                    {
                                        myHelpers.DisplayMessage(lblQueryResultMessage, sqlErrorMessage);
                                        return;
                                    }
                                    coursesEnrolled += 1;
                                }
                            }
                        }

                        // Display a message indicating enrollment result.
                        if (coursesEnrolled != 0)
                        {
                            string enrollmentSuccessfulMessage = enrollmentSuccess + coursesEnrolled.ToString();
                            if (coursesEnrolled == 1) { enrollmentSuccessfulMessage += " course."; }
                            else { enrollmentSuccessfulMessage += " courses."; }
                            myHelpers.DisplayMessage(lblQueryResultMessage, enrollmentSuccessfulMessage);
                            pnlAvailableCourses.Visible = false;
                        }
                        else { myHelpers.DisplayMessage(lblSubmitMessage, selectCourse); }
                    }
                }
                else
                {
                    if (dtStudentId.Rows.Count > 1)
                    {
                        myHelpers.DisplayMessage(lblQueryResultMessage,
                                                 $"{queryError}TODO 08{queryErrorMultipleStudentIdsRetrieved}");
                    } // Multiple student ids retrieved.
                    else
                    {
                        myHelpers.DisplayMessage(lblQueryResultMessage,
                                                 $"{dbqueryError}TODO 08{dbqueryErrorNoStudentIdRetrieved}");
                    } // No student id was retrieved.
                }
            }
        }

        protected void GvAvailableCourses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Controls.Count == 5)
            {
                // GridView columns: CHECKBOX-0, COURSEID-1, COURSENAME-2, CREDITS-3, INSTRUCTOR-4 (offset by plus 1 due to checkbox column)
                int creditsColumn = myHelpers.GetGridViewColumnIndexByName(sender, "CREDITS", lblQueryResultMessage) + 1; // index 3

                if (creditsColumn != 0)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "COURSEID", "ID");
                        myHelpers.RenameGridViewColumn(e, "COURSENAME", "NAME");
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow) { e.Row.Cells[creditsColumn].HorizontalAlign = HorizontalAlign.Center; }
                }
            }
        }
    }
}