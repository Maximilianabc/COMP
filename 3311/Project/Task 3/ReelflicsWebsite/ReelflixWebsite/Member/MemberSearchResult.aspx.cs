using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite
{
    public partial class MemberSearchResult : Page
    {
        //************************
        // Uses TODO 03, TODO 04 *
        //************************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e) // Uses TODO 03, TODO 04
        {
            if (!IsPostBack)
            {
                string queryString = Server.UrlDecode(StringExtension.CleanInput(Request.QueryString["queryString"]));
                if (!string.IsNullOrEmpty(queryString))
                {
                    //*********************************************************
                    // Uses TODO 03 to populate a gridView with movie titles. *
                    //*********************************************************
                    if (myHelpers.PopulateGridView("TODO 03",
                                                   gvTitleSearchResult,
                                                   myReelflicsDB.GetMovieSearchResult(queryString),
                                                   new List<string> { "MOVIEID", "TITLE", "RELEASEYEAR" },
                                                   lblErrorMessage,
                                                   lblTitleSearchResultMessage,
                                                   noMovieMatches))
                    {
                        if (!isEmptyQueryResult) { gvTitleSearchResult.Visible = true; }
                        else { lblTitleSearchResultMessage.Visible = true; }
                    }
                    //***************************************************************
                    // Uses TODO 04 to populate a gridView with movie person names. *
                    //***************************************************************
                    if (myHelpers.PopulateGridView("TODO 04",
                                                   gvNameSearchResult,
                                                   myReelflicsDB.GetMoviePersonSearchResult(queryString),
                                                   new List<string> { "PERSONID", "NAME" },
                                                   lblErrorMessage,
                                                   lblNameSearchResultMessage,
                                                   noMoviePersonMatches))
                    {
                        if (!isEmptyQueryResult) { gvNameSearchResult.Visible = true; }
                        else { lblNameSearchResultMessage.Visible = true; }
                    }
                }
                else { Response.Redirect("~/Default.aspx"); }
            }
        }

        protected void GvNameSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview data.
            if (e.Row.Cells.Count == 2)
            {
                // GridView columns: 0-PERSONID, 1-NAME
                int personIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PERSONID", lblErrorMessage); // index 0
                int nameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "NAME", lblErrorMessage);         // index 1

                if (personIdColumn != -1 && nameColumn != -1)
                {
                    e.Row.Cells[personIdColumn].Visible = false; // Hide the personId column.

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[nameColumn].Text = Regex.Replace(e.Row.Cells[nameColumn].Text, " ", "&nbsp;");
                        // Change the person name to a hyperlink.
                        var titleCell = e.Row.Cells[nameColumn];
                        titleCell.Controls.Clear();
                        titleCell.Controls.Add(new HyperLink {
                            NavigateUrl = "~/Shared/MoviePersonInformation.aspx?personId="
                                          + e.Row.Cells[personIdColumn].Text,
                            Text = titleCell.Text
                        });
                    }
                }
            }
        }

        protected void GvTitleSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview data.
            if (e.Row.Cells.Count == 3)
            {
                // GridView columns: 0-MOVIEID, 1-TITLE, 2-RELEASEYEAR
                int movieIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MOVIEID", lblErrorMessage);         // index 0
                int titleColumn = myHelpers.GetGridViewColumnIndexByName(sender, "TITLE", lblErrorMessage);             // index 1
                int releaseYearColumn = myHelpers.GetGridViewColumnIndexByName(sender, "RELEASEYEAR", lblErrorMessage); // index 2

                if (movieIdColumn != -1 && titleColumn != -1 && releaseYearColumn != -1)
                {
                    e.Row.Cells[movieIdColumn].Visible = e.Row.Cells[releaseYearColumn].Visible = false; // Hide the movieId and releaseYear columns.

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Cells[titleColumn].Text = Regex.Replace(e.Row.Cells[titleColumn].Text, " ", "&nbsp;");
                        // Change the movie title to a hyperlink and place the release year in parenthesis after the hyperlink.
                        var titleCell = e.Row.Cells[titleColumn];
                        titleCell.Controls.Clear();
                        titleCell.Controls.Add(new HyperLink {
                            NavigateUrl = "~/Shared/MovieInformation.aspx?movieId="
                                          + e.Row.Cells[movieIdColumn].Text,
                            Text = titleCell.Text
                        });
                        titleCell.Controls.Add(new Literal { Text = "&nbsp;("
                                                                    + e.Row.Cells[releaseYearColumn].Text
                                                                    + ")" });
                    }
                }
            }
        }
    }
}