using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Employee
{
    public partial class MembershipStatisticsReport : Page
    {
        //************************
        // Uses TODO 40, TODO 41 *
        //************************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/

        private void GetEducationLevelStatistics() // Uses TODO 41
        {
            //********************************************************************
            // Uses TODO 41 to populate the education level statistics gridview. *
            //********************************************************************
            if (myHelpers.PopulateGridView("TODO 41",
                                           gvEducationLevelReport,
                                           myReelflicsDB.GetEducationLevelReport(),
                                           new List<string> { "EDUCATIONLEVEL", "ANYNAME" },
                                           lblErrorMessage,
                                           lblErrorMessage,
                                           $"{dbqueryError}TODO 41{dbqueryErrorNoEducationLevelReport}"))
            {
                if (!isEmptyQueryResult) { pnlEducationLevelReport.Visible = true; }
            }
        }

        private bool GetMembershipStatistics() // Uses TODO 40
        {
            //****************************************************************
            // Uses TODO 40 to populate the membership statistics gridview . *
            //****************************************************************
            if (myHelpers.PopulateGridView("TODO 40",
                                           gvMembershipReport,
                                           myReelflicsDB.GetMembershipStatisticsReport(),
                                           new List<string> { "ANYNAME", "ANYNAME", "ANYNAME" },
                                           lblErrorMessage,
                                           lblErrorMessage,
                                           $"{dbqueryError}TODO 40{dbqueryErrorNoMembershipReport}"))
            {
                if (!isEmptyQueryResult) { return pnlMembershipReport.Visible = true; }
                else { return false; }
            }
            else { return false; }
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (GetMembershipStatistics()) { GetEducationLevelStatistics(); }
            }
        }

        protected void GvEducationLevelReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview headers and data.
            if (e.Row.Cells.Count == 2)
            {
                // GridView columns: EDUCATIONLEVEL-0, COUNT-1
                int educationLevelColumn = myHelpers.GetGridViewColumnIndexByName(sender, "EDUCATIONLEVEL", lblErrorMessage); // index 0
                int countColumn = 1; // index 1

                if (educationLevelColumn != -1)
                {
                    e.Row.Cells[educationLevelColumn].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[countColumn].HorizontalAlign = HorizontalAlign.Center;

                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Cells[educationLevelColumn].Text = "EDUCATION LEVEL";
                        e.Row.Cells[countColumn].Text = "COUNT";
                    }
                }
            }
        }

        protected void GvMembershipReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview headers and data.
            if (e.Row.Cells.Count == 3)
            {
                // GridView columns: MEMBERS-0, MALE-1, FEMALE-2
                int membersColumn = 0; // index 0
                int maleColumn = 1;    // index 1
                int femaleColumn = 2;  // index 2

                e.Row.Cells[membersColumn].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[maleColumn].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[femaleColumn].HorizontalAlign = HorizontalAlign.Center;

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[membersColumn].Text = "MEMBERS";
                    e.Row.Cells[maleColumn].Text = "MALE";
                    e.Row.Cells[femaleColumn].Text = "FEMALE";
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells[membersColumn].Text == "&nbsp;") { e.Row.Cells[membersColumn].Text = "-"; }
                    if (e.Row.Cells[maleColumn].Text == "&nbsp;") { e.Row.Cells[maleColumn].Text = "-"; }
                    if (e.Row.Cells[femaleColumn].Text == "&nbsp;") { e.Row.Cells[femaleColumn].Text = "-"; }
                }
            }
        }
    }
}