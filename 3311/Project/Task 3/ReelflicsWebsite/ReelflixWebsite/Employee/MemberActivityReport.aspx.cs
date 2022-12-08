using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Employee
{
    public partial class MemberActivityReport : Page
    {
        //*********************************
        // Uses TODO 12, TODO 35, TODO 42 *
        //*********************************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private DataTable dtGenreCounts;
        private DataTable dtWatchHistory;
        private DataTable dtWatchlist;
        private readonly string hideReport = "Hide Report " + ((char)0x25B2).ToString();
        private readonly string showReport = "Show Report " + ((char)0x25BC).ToString();
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        /***** Private Methods *****/

        private bool PopulateGenreViewingReport(string username) // Uses TODO 42
        {
            bool result = false;

            // Define a DataTable to contain the genre viewing data.
            DataTable dtGenreViewingData = new DataTable();

            if (ViewState["dtGenreCounts"] == null)
            {
                //*******************************************************************************************************************
                // Uses TODO 42 to get the genres of the movies viewed by a member identified by his/her username and their counts. *
                //*******************************************************************************************************************
                dtGenreCounts = myReelflicsDB.GetGenreViewingReport(username);

                // Save the DataTable in ViewState to avoid having to retrived it again.
                ViewState["dtGenreCounts"] = dtGenreCounts;
            }
            else { dtGenreCounts = ViewState["dtGenreCounts"] as DataTable; }

            // Show the genre counts for the member if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 42",
                                             dtGenreCounts,
                                             new List<string> { "GENRE", "ANYNAME" },
                                             lblErrorMessage))
            {
                if (dtGenreCounts.Rows.Count != 0)
                {
                    int numGenres = dtGenreCounts.Rows.Count;
                    int numGenreRows = 5; // The number of rows in the report.
                    int numGenreCountColumns = (int)Math.Ceiling((decimal)numGenres / numGenreRows); // The number of (genre, count) columns in the report.

                    // Add twice numGenreCountColumns report columns (one for genre and one for counts).
                    for (int i = 0; i < numGenreCountColumns; i++)
                    {
                        dtGenreViewingData.Columns.Add();
                        dtGenreViewingData.Columns.Add();
                    }

                    // Get the name of the column containing the genre counts.
                    string genreCountColumn = dtGenreCounts.Columns[1].ToString();

                    // Populate the genres and counts.
                    for (int i = 0; i < numGenreRows; i++)
                    {
                        DataRow dr = dtGenreViewingData.NewRow();

                        int k = 0; // Ranges across the (genre, count) columns in a row.

                        // Place the (genre, count) rows in dtGenreCounts into (genre, count) columns in dtGenreViewingData.
                        for (int j = 0; j < numGenres; j += numGenreRows)
                        {
                            if (i + j < numGenres)
                            {
                                dr[k] = dtGenreCounts.Rows[i + j]["GENRE"].ToString();
                                dr[k + 1] = dtGenreCounts.Rows[i + j][genreCountColumn].ToString();
                            }
                            else // Pad additonal (genre, count) columns with spaces, if needed.
                            {
                                dr[k] = "";
                                dr[k + 1] = "";
                            }
                            k += 2;
                        }
                        dtGenreViewingData.Rows.Add(dr);
                    }

                    // The genre viewing statistics DataTable was successfully constructed.
                    result = true;
                    gvGenreViewingStatisticsReport.DataSource = dtGenreViewingData;
                    gvGenreViewingStatisticsReport.DataBind();
                }
                else  // No genre count records were retrieved.
                {
                    myHelpers.DisplayMessage(lblErrorMessage, dbqueryError
                                                  + "TODO 42"
                                                  + dbqueryErrorNoRecordsRetrieved);
                    return result;
                }
            }
            else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.

            return result;
        }

        private bool PopulateWatchHistoryReport(string username) // Uses TODO 35
        {
            bool result = false;
            lblNoWatchHistory.Visible = false;

            if (ViewState["dtWatchHistory"] == null)
            {
                //*****************************************************************************
                // Uses TODO 35 to populate a gridview with a member's watch history records. *
                //*****************************************************************************
                dtWatchHistory = myReelflicsDB.GetMemberWatchHistory(username);

                // Save the DataTable in ViewState to avoid having to retrieve it again.
                ViewState["dtWatchHistory"] = dtWatchHistory;
            }
            else { dtWatchHistory = ViewState["dtWatchHistory"] as DataTable; }

            if (myHelpers.PopulateGridView("TODO 35",
                                           gvWatchHistory,
                                           dtWatchHistory,
                                           new List<string> { "MOVIEID", "WATCHDATE", "TITLE", "RELEASEYEAR", "RUNNINGTIME", "MPAARATING" },
                                           lblErrorMessage,
                                           lblNoWatchHistory,
                                           noWatchHistory))
            {
                result = pnlWatchHistory.Visible = true;
                if (!isEmptyQueryResult)
                {
                    gvWatchHistory.Visible = true;
                    CurrentSortDirection = SortDirection.Descending;
                }
                else { lblNoWatchHistory.Visible = true; }
            }
            return result;
        }

        private bool PopulateWatchlistReport(string username) // Uses TODO 12
        {
            bool result = false;
            lblNoWatchlist.Visible = false;

            if (ViewState["dtWatchlist"] == null)
            {
                //*************************************************************************
                // Uses TODO 12 to populate a gridview with a member's watchlist records. *
                //*************************************************************************
                dtWatchlist = myReelflicsDB.GetWatchlist(username);

                // Save the DataTable in ViewState to avoid having to retrieve it again.
                ViewState["dtWatchlist"] = dtWatchlist;
            }
            else { dtWatchlist = ViewState["dtWatchlist"] as DataTable; }

            if (myHelpers.PopulateGridView("TODO 12",
                                           gvWatchlist,
                                           dtWatchlist,
                                           new List<string> { "MOVIEID", "TITLE", "RELEASEYEAR", "RUNNINGTIME", "MPAARATING" },
                                           lblErrorMessage,
                                           lblNoWatchlist,
                                           noWatchlist))
            {
                result = pnlWatchlist.Visible = true;

                if (!isEmptyQueryResult) { gvWatchlist.Visible = true; }
                else { lblNoWatchlist.Visible = true; }
            }
            return result;
        }

        private SortDirection CurrentSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                { ViewState["sortDirection"] = SortDirection.Ascending; }
                return (SortDirection)ViewState["sortDirection"];
            }
            set
            { ViewState["sortDirection"] = value; }
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate a dropdown list with members' usernames and names.
                if (myHelpers.PopulateDropDownList("DBHelperMethods - GetReelflicsMembers",
                                                   ddlMemberName,
                                                   myDBHelpers.GetReelflicsMembers(),
                                                   new List<string> { "USERNAME", "NAME" },
                                                   lblErrorMessage,
                                                   lblErrorMessage,
                                                   dbqueryErrorNoRecordsRetrieved,
                                                   EmptyQueryResultMessageType.DBQueryError))
                { pnlMemberName.Visible = true; }
                else { if (isSqlError) { lblErrorMessage.Text += contact3311rep; } }
            }
        }

        protected void BtnGenreViewingStatisticsReport_Click(object sender, EventArgs e)
        {
            if (btnGenreViewingStatisticsReport.Text == showReport) // If the report is hidden, then show it.
            {
                if (PopulateGenreViewingReport(ddlMemberName.SelectedValue))
                {
                    pnlGenreViewingStatisticsReport.Visible = true;
                    btnGenreViewingStatisticsReport.Text = hideReport;
                }
            }
            else // Hide the report.
            {
                pnlGenreViewingStatisticsReport.Visible = false;
                btnGenreViewingStatisticsReport.Text = showReport;
            }
        }

        protected void BtnWatchHistoryReport_Click(object sender, EventArgs e)
        {
            if (btnWatchHistoryReport.Text == showReport) // If the report is hidden, then show it.
            {
                if (PopulateWatchHistoryReport(ddlMemberName.SelectedValue))
                {
                    pnlWatchHistory.Visible = true;
                    btnWatchHistoryReport.Text = hideReport;
                }
            }
            else // Hide the report.
            {
                pnlWatchHistory.Visible = false;
                btnWatchHistoryReport.Text = showReport;
            }
        }

        protected void BtnWatchlistReport_Click(object sender, EventArgs e)
        {
            if (btnWatchlistReport.Text == showReport) // If the report is hidden, then show it.
            {
                if (PopulateWatchlistReport(ddlMemberName.SelectedValue))
                {
                    pnlWatchlist.Visible = true;
                    btnWatchlistReport.Text = hideReport;
                }
            }
            else // Hide the report.
            {
                pnlWatchlist.Visible = false;
                btnWatchlistReport.Text = showReport;
            }
        }

        protected void DdlMemberName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsValid && !isSqlError)
            {
                pnlActivityReport.Visible = true;
                pnlWatchHistory.Visible = pnlGenreViewingStatisticsReport.Visible = pnlWatchlist.Visible = false;
                btnWatchHistoryReport.Text = showReport;
                btnGenreViewingStatisticsReport.Text = showReport;
                btnWatchlistReport.Text = showReport;

                // Reset the DataTables in ViewState.
                ViewState["dtWatchHistory"] = null;
                ViewState["dtGenreCounts"] = null;
                ViewState["dtWatchlist"] = null;
            }
        }

        protected void GvGenreViewingStatisticsReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 1; i < e.Row.Cells.Count; i += 2)
            {
                e.Row.Cells[i].Width = 100;
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Left;
            }
        }

        protected void GvWatchHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWatchHistory.PageIndex = e.NewPageIndex;
            gvWatchHistory.DataSource = ViewState["dtWatchHistory"];
            gvWatchHistory.DataBind();
        }

        protected void GvWatchHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview data.
            if (e.Row.Cells.Count == 6)
            {
                // GridView columns: 0-MOVIEID, 1-TITLE, 2-WATCHDATE 3-RELEASEYEAR 4-RUNNINGTIME 5-MPAARATING
                int movieIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MOVIEID", lblErrorMessage);         // index 0
                int watchDateColumn = myHelpers.GetGridViewColumnIndexByName(sender, "WATCHDATE", lblErrorMessage);     // index 1
                int titleColumn = myHelpers.GetGridViewColumnIndexByName(sender, "TITLE", lblErrorMessage);             // index 2
                int releaseYearColumn = myHelpers.GetGridViewColumnIndexByName(sender, "RELEASEYEAR", lblErrorMessage); // index 3
                int runningTimeColumn = myHelpers.GetGridViewColumnIndexByName(sender, "RUNNINGTIME", lblErrorMessage); // index 4
                int MPAARATINGColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MPAARATING", lblErrorMessage);   // index 5

                if (movieIdColumn != -1 && watchDateColumn != -1 && titleColumn != -1 && releaseYearColumn != -1 && runningTimeColumn != -1 && MPAARATINGColumn != -1)
                {
                    e.Row.Cells[movieIdColumn].Visible = false; // Hide the movie id column.
                    e.Row.Cells[releaseYearColumn].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[runningTimeColumn].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[MPAARATINGColumn].HorizontalAlign = HorizontalAlign.Center;

                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        ((LinkButton)e.Row.Cells[watchDateColumn].Controls[0]).Text = "DATE";
                        ((LinkButton)e.Row.Cells[releaseYearColumn].Controls[0]).Text = "RELEASE YEAR";
                        ((LinkButton)e.Row.Cells[runningTimeColumn].Controls[0]).Text = "RUNNING TIME";
                        ((LinkButton)e.Row.Cells[MPAARATINGColumn].Controls[0]).Text = "MPAA RATING";
                    }
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Format the watchDate column.
                        e.Row.Cells[watchDateColumn].Text = DateTime.Parse(e.Row.Cells[watchDateColumn].Text).ToString("d MMMM yyyy HH:mm");

                        // Convert the running time from minutes to hours:minutes.
                        e.Row.Cells[runningTimeColumn].Text = TimeSpan.FromMinutes(Convert.ToDouble(e.Row.Cells[runningTimeColumn].Text)).ToString(@"h\:mm");
                    }
                }
            }
        }

        protected void GvWatchHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            string columnToSort = e.SortExpression; // Get the column name.
            gvWatchHistory.PageIndex = 0;

            if (CurrentSortDirection == SortDirection.Ascending)
            {
                CurrentSortDirection = SortDirection.Descending;
                ViewState["dtWatchHistory"] = myHelpers.SortGridview(gvWatchHistory,
                                                                       (DataTable)ViewState["dtWatchHistory"],
                                                                       columnToSort,
                                                                       DESCENDING);
            }
            else
            {
                CurrentSortDirection = SortDirection.Ascending;
                ViewState["dtWatchHistory"] = myHelpers.SortGridview(gvWatchHistory,
                                                                       (DataTable)ViewState["dtWatchHistory"],
                                                                       columnToSort,
                                                                       ASCENDING);
            }
        }

        protected void GvWatchlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWatchlist.PageIndex = e.NewPageIndex;
            gvWatchlist.DataSource = ViewState["dtWatchlist"];
            gvWatchlist.DataBind();
        }

        protected void GvWatchlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview data.
            if (e.Row.Cells.Count == 5)
            {
                // GridView columns: 0-MOVIEID, 1-TITLE, 2-RELEASEYEAR, 3-RUNNINGTIME, 4-MPAARATING
                int movieIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MOVIEID", lblErrorMessage);         // index 0
                int releaseYearColumn = myHelpers.GetGridViewColumnIndexByName(sender, "RELEASEYEAR", lblErrorMessage); // index 2
                int runningTimeColumn = myHelpers.GetGridViewColumnIndexByName(sender, "RUNNINGTIME", lblErrorMessage); // index 3
                int MPAARATINGColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MPAARATING", lblErrorMessage);   // index 4

                if (movieIdColumn != -1 && releaseYearColumn != -1 && runningTimeColumn != -1 && MPAARATINGColumn != -1)
                {
                    e.Row.Cells[movieIdColumn].Visible = false; // Hide the movie id column.
                    e.Row.Cells[releaseYearColumn].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[runningTimeColumn].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[MPAARATINGColumn].HorizontalAlign = HorizontalAlign.Center;

                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        myHelpers.RenameGridViewColumn(e, "RELEASEYEAR", "RELEASE YEAR");
                        myHelpers.RenameGridViewColumn(e, "RUNNINGTIME", "RUNNING TIME");
                        myHelpers.RenameGridViewColumn(e, "MPAARATING", "MPAA RATING");
                    }

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Convert the running time from minutes to hours:minutes.
                        e.Row.Cells[runningTimeColumn].Text = TimeSpan.FromMinutes(Convert.ToDouble(e.Row.Cells[runningTimeColumn].Text)).ToString(@"h\:mm");
                    }
                }
            }
        }
    }
}