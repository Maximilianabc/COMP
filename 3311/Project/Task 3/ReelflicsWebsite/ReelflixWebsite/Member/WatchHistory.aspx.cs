using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Member
{
    public partial class WatchHistory : System.Web.UI.Page
    {
        //***************
        // Uses TODO 35 *
        //***************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly string username = HttpContext.Current.User.Identity.Name;
        private const string ASCENDING = " ASC";
        private const string DESCENDING = " DESC";

        /***** Private Methods *****/

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

        protected void Page_Load(object sender, EventArgs e) // Uses TODO 35
        {
            if (!IsPostBack)
            {
                //*****************************************************************************
                // Uses TODO 35 to populate a gridview with a member's watch history records. *
                //*****************************************************************************
                DataTable dtWatchHistory = myReelflicsDB.GetMemberWatchHistory(username);
                if (myHelpers.PopulateGridView("TODO 35",
                                               gvWatchHistory,
                                               dtWatchHistory,
                                               new List<string> { "MOVIEID", "WATCHDATE", "TITLE", "RELEASEYEAR", "RUNNINGTIME", "MPAARATING" },
                                               lblErrorMessage,
                                               lblNoWatchHistory,
                                               noWatchHistory))
                {
                    ViewState["dtWatchHistory"] = dtWatchHistory;
                    pnlWatchHistory.Visible = true;
                    if (!isEmptyQueryResult)
                    {
                        gvWatchHistory.Visible = true;
                        CurrentSortDirection = SortDirection.Descending;
                    }
                    else { lblNoWatchHistory.Visible = true; }
                }
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
                        // Change the movie title to a hyperlink.
                        var titleCell = e.Row.Cells[titleColumn];
                        titleCell.Controls.Clear();
                        titleCell.Controls.Add(new HyperLink
                        {
                            NavigateUrl = "~/Shared/MovieInformation.aspx?movieId=" + e.Row.Cells[movieIdColumn].Text,
                            Text = titleCell.Text
                        });

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
    }
}