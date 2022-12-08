using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Member
{
    public partial class Watchlist : Page
    {
        //***************
        // Uses TODO 12 *
        //***************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly string username = HttpContext.Current.User.Identity.Name;

        protected void Page_Load(object sender, EventArgs e) // Uses TODO 12
        {
            if (!IsPostBack)
            {
                //*************************************************************************
                // Uses TODO 12 to populate a gridview with a member's watchlist records. *
                //*************************************************************************
                if (myHelpers.PopulateGridView("TODO 12",
                                               gvWatchlist,
                                               myReelflicsDB.GetWatchlist(username),
                                               new List<string> { "MOVIEID", "TITLE", "RELEASEYEAR", "RUNNINGTIME", "MPAARATING" },
                                               lblErrorMessage,
                                               lblNoWatchlist,
                                               noWatchlist))
                {
                    pnlWatchlist.Visible = true;
                    if (!isEmptyQueryResult) { gvWatchlist.Visible = true; }
                    else { lblNoWatchlist.Visible = true; }
                }
            }
        }

        protected void GvWatchlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview data.
            if (e.Row.Cells.Count == 5)
            {
                // GridView columns: 0-MOVIEID, 1-TITLE, 2-RELEASEYEAR, 3-RUNNINGTIME, 4-MPAARATING
                int movieIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MOVIEID", lblErrorMessage);         // index 0
                int titleColumn = myHelpers.GetGridViewColumnIndexByName(sender, "TITLE", lblErrorMessage);             // index 1
                int releaseYearColumn = myHelpers.GetGridViewColumnIndexByName(sender, "RELEASEYEAR", lblErrorMessage); // index 2
                int runningTimeColumn = myHelpers.GetGridViewColumnIndexByName(sender, "RUNNINGTIME", lblErrorMessage); // index 3
                int MPAARATINGColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MPAARATING", lblErrorMessage);   // index 4

                if (movieIdColumn != -1 && titleColumn != -1 && releaseYearColumn != -1 && runningTimeColumn != -1 && MPAARATINGColumn != -1)
                {
                    e.Row.Cells[movieIdColumn].Visible = false; // Hide the movie id column.
                    e.Row.Cells[releaseYearColumn].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[runningTimeColumn].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[MPAARATINGColumn].HorizontalAlign = HorizontalAlign.Center;
                }

                if (e.Row.RowType == DataControlRowType.Header)
                {
                    myHelpers.RenameGridViewColumn(e, "RELEASEYEAR", "RELEASE YEAR");
                    myHelpers.RenameGridViewColumn(e, "RUNNINGTIME", "RUNNING TIME");
                    myHelpers.RenameGridViewColumn(e, "MPAARATING", "MPAA RATING");
                }

                // Convert the running time from minutes to hours:minutes.
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
                    e.Row.Cells[runningTimeColumn].Text = TimeSpan.FromMinutes(Convert.ToDouble(e.Row.Cells[runningTimeColumn].Text)).ToString(@"h\:mm");
                }
            }
        }
    }
}