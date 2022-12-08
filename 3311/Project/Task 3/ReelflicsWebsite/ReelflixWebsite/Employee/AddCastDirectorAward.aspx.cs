using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Employee
{
    public partial class AddCastDirectorAward : Page
    {
        //************************************************************
        // Uses TODO 04, TODO 16, TODO 22, TODO 23, TODO 24, TODO 27 *
        //************************************************************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/

        private bool GetFilmographyRecords(string personId) // Uses TODO 23, TODO 24
        {
            bool result = false;
            bool hasCastMemberFilmography = false;
            bool hasDirectorFilmography = false;
            //************************************************************************************
            // Uses TODO 23 to populate a GridView with an actor's/actress' flimography records. *
            //************************************************************************************
            if (myHelpers.PopulateGridView("TODO 23",
                                           gvActorActressFilmography,
                                           myReelflicsDB.GetCastMemberFilmography(personId),
                                           new List<string> { "MOVIEID", "TITLE", "RELEASEYEAR", "ROLE", "AWARDNAME" },
                                           lblErrorMessage,
                                           lblErrorMessage,
                                           null))
            {
                // Save the actor filmography in ViewState for later use.
                ViewState["dtActorActressFilmography"] = gvActorActressFilmography.DataSource as DataTable;
                if (ViewState["gender"].ToString() == "F") { litActorActressHeading.Text = "Actress"; }
                if (!isEmptyQueryResult) { result = hasCastMemberFilmography = pnlActorActressFilmography.Visible = true; }
            }
            else { pnlNameSearch.Visible = false; return result; } // An SQL error occurred.

            //*****************************************************************************
            // Uses TODO 24 to populate a GridView with a director's filmography records. *
            //*****************************************************************************
            if (myHelpers.PopulateGridView("TODO 24",
                                           gvDirectorFilmography,
                                           myReelflicsDB.GetDirectorFilmography(personId),
                                           new List<string> { "MOVIEID", "TITLE", "RELEASEYEAR", "AWARDNAME" },
                                           lblErrorMessage,
                                           lblErrorMessage,
                                           null))
            {
                // Save the director filmography in ViewState for later use.
                ViewState["dtDirectorFilmography"] = gvDirectorFilmography.DataSource as DataTable;
                if (!isEmptyQueryResult) { result = hasDirectorFilmography = pnlDirectorFilmography.Visible = true; }
            }
            else { result = pnlNameSearch.Visible = false; return result; } // An SQL error occurred.

            // Show no filmography message, if necessary.
            if (!hasCastMemberFilmography && !hasDirectorFilmography)
            { myHelpers.DisplayMessage(lblNoFilmography, noFilmography); }
            return result;
        }

        private bool GetMoviePersonRecord(string personId) // Uses TODO 22
        {
            bool result = false;
            //********************************************************************
            // Uses TODO 22 to retrieve the record of a cast member or director. *
            //********************************************************************
            DataTable dtMoviePerson = myReelflicsDB.GetMoviePersonRecord(personId);

            // Determine if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 22",
                                             dtMoviePerson,
                                             new List<string> { "PERSONID", "NAME", "BIOGRAPHY", "GENDER", "BIRTHDATE", "DEATHDATE" },
                                             lblErrorMessage))
            {
                if (dtMoviePerson.Rows.Count == 1) // Only one record should be retrieved.
                {
                    // Create the cast member/director's image photo filename from his/her person id and name.
                    imgPhoto.ImageUrl = peopleDirectory
                                        + personId
                                        + StringExtension.CreateFileName(dtMoviePerson.Rows[0]["NAME"].ToString());
                    litName.Text = HttpUtility.HtmlDecode(dtMoviePerson.Rows[0]["NAME"].ToString()) + " Filmography";
                    ViewState["gender"] = dtMoviePerson.Rows[0]["GENDER"].ToString();
                    result = true;
                }
                else if (dtMoviePerson.Rows.Count == 0) // No record was retrieved.
                { myHelpers.DisplayMessage(lblErrorMessage, dbqueryError + "TODO 22" + dbqueryErrorNoRecordsRetrieved); }
                else // Multiple records were retrieved.
                { myHelpers.DisplayMessage(lblErrorMessage, queryError + "TODO 22" + queryErrorMultipleRecordsRetrieved); }
            }
            return result;
        }

        private DataTable PopulateAwardDropDownList(string movieId) // Uses TODO 16
        {
            DataTable dtActorActressFilmography = ViewState["dtActorActressFilmography"] as DataTable;
            DataTable dtDirectorFilmography = ViewState["dtDirectorFilmography"] as DataTable;

            if (dtActorActressFilmography != null && dtDirectorFilmography != null) // These DataTables should never be null!
            {
                DataTable dtActorInMovie = dtActorActressFilmography;
                DataTable dtDirectorOfMovie = dtDirectorFilmography;

                // Select the cast member/director filmography only for a specified movie.
                DataRow[] drActorActressMovie = dtActorActressFilmography.Select("MOVIEID=" + movieId);
                DataRow[] drDirectorMovie = dtDirectorFilmography.Select("MOVIEID=" + movieId);

                if (drActorActressMovie.Length > 0) // Get the actor/actress filmography, if any.
                { dtActorInMovie = drActorActressMovie.CopyToDataTable(); }
                else { dtActorInMovie.Rows.Clear(); } // There is no actor/actress filmography.

                if (drDirectorMovie.Length > 0) // Get the director filmography, if any.
                { dtDirectorOfMovie = drDirectorMovie.CopyToDataTable(); }
                else { dtDirectorOfMovie.Rows.Clear(); } // There is no director filmography.

                //************************************************************************
                // Uses TODO 16 to retrieve all the acting and directing academy awards. *
                //************************************************************************
                DataTable dtAcademyAwards = myReelflicsDB.GetAcademyAwards();

                if (myHelpers.IsQueryResultValid("TODO 16",
                                                 dtAcademyAwards,
                                                 new List<string> { "AWARDID", "AWARDNAME" },
                                                 lblErrorMessage))
                {
                    // Determine what acting awards to keep in the award dropdown list.
                    // If the person has no acting role in this movie or has already won an acting award for this movie, remove all acting awards.
                    if (dtActorInMovie.Rows.Count == 0 || !string.IsNullOrEmpty(dtActorInMovie.Rows[0]["AWARDNAME"].ToString()))
                    {
                        dtAcademyAwards = myHelpers.RemoveDataTableRecord(dtAcademyAwards, "awardName", "Best Actor", "==");
                        dtAcademyAwards = myHelpers.RemoveDataTableRecord(dtAcademyAwards, "awardName", "Best Supporting Actor", "==");
                        dtAcademyAwards = myHelpers.RemoveDataTableRecord(dtAcademyAwards, "awardName", "Best Actress", "==");
                        dtAcademyAwards = myHelpers.RemoveDataTableRecord(dtAcademyAwards, "awardName", "Best Supporting Actress", "==");
                    }
                    else if (string.IsNullOrEmpty(dtActorInMovie.Rows[0]["AWARDNAME"].ToString())) // The person has a role in this movie and has not won an acting award.
                    {
                        if (ViewState["gender"].ToString() == "M") // If the person is male, then remove Best Actress and Best Supporting Actress awards.
                        {
                            dtAcademyAwards = myHelpers.RemoveDataTableRecord(dtAcademyAwards, "awardName", "Best Actress", "==");
                            dtAcademyAwards = myHelpers.RemoveDataTableRecord(dtAcademyAwards, "awardName", "Best Supporting Actress", "==");
                        }
                        else // (gender = "F") If the person is female, then remove Best Actor and Best Supporting Actor awards.
                        {
                            dtAcademyAwards = myHelpers.RemoveDataTableRecord(dtAcademyAwards, "awardName", "Best Actor", "==");
                            dtAcademyAwards = myHelpers.RemoveDataTableRecord(dtAcademyAwards, "awardName", "Best Supporting Actor", "==");
                        }
                    }

                    // Determine whether to keep the directing award in the award dropdown list.
                    // If the person has no directing role in this movie or has already won a directing award for this movie, remove the directing award.
                    if (dtDirectorOfMovie.Rows.Count == 0 || !string.IsNullOrEmpty(dtDirectorOfMovie.Rows[0]["AWARDNAME"].ToString()))
                    { dtAcademyAwards = myHelpers.RemoveDataTableRecord(dtAcademyAwards, "awardName", "Best Director", "=="); }
                    return dtAcademyAwards;
                }
            }
            else
            {
                myHelpers.DisplayMessage(lblErrorMessage, nullDataTableErrorMessage
                                                          + "dtActorActressFilmography and/or dtDirectorFilmography in AddCastDirectorAward/PopulateAwardsDropDownList."
                                                          + contact3311rep);
            }
            return null;
        }

        private bool PopulateMovieDropDownList()
        {
            bool result = false;
            // Get the movies for the movie dropdown list from the actor/actress//director's filmography records.
            DataTable dtActorActressMovies = ViewState["dtActorActressFilmography"] as DataTable;
            DataTable dtDirectorMovies = ViewState["dtDirectorFilmography"] as DataTable;

            if (dtActorActressMovies != null && dtDirectorMovies != null)  // These DataTable should never be null!
            {
                // Remove movies from the actor's/actress' filmography that already have an acting award
                // as no more acting award can be assigned for this movie.
                dtActorActressMovies = myHelpers.RemoveDataTableRecord(dtActorActressMovies, "AWARDNAME", "", "!=");

                // Remove movies from the director's filmography that already have a directing award
                // as no more directing award can be assigned to the director for this movie.
                dtDirectorMovies = myHelpers.RemoveDataTableRecord(dtDirectorMovies, "AWARDNAME", "", "!=");

                // Prepare the actor/actress and director DataTables for merging by keeping only the movie id and title.
                dtActorActressMovies = new DataView(dtActorActressMovies).ToTable("Selected", false, "MOVIEID", "TITLE");
                dtActorActressMovies.PrimaryKey = new DataColumn[] { dtActorActressMovies.Columns["MOVIEID"] };
                dtDirectorMovies = new DataView(dtDirectorMovies).ToTable("Selected", false, "MOVIEID", "TITLE");
                dtDirectorMovies.PrimaryKey = new DataColumn[] { dtDirectorMovies.Columns["MOVIEID"] };

                // Merge the actor/actress movies and director movies DataTables into the actor/actress movies DataTable.
                dtActorActressMovies.Merge(dtDirectorMovies);

                // Determine if there are any movies remaining for which an award can be assigned to the actor/actress/director.
                if (dtActorActressMovies.Rows.Count != 0)
                {
                    if (myHelpers.PopulateDropDownList("AddCastDirectorAward/PopulateMovieDropDownList",
                                                       ddlMovie,
                                                       dtActorActressMovies,
                                                       new List<string> { "MOVIEID", "TITLE" },
                                                       lblErrorMessage,
                                                       lblErrorMessage,
                                                       dbqueryErrorNoFilmography,
                                                       EmptyQueryResultMessageType.DBQueryError))
                    { result = pnlAddAward.Visible = pnlSelectAward.Visible = true; }
                }
                else // No more awards can be assigned to the actor/actress/director.
                {
                    myHelpers.DisplayMessage(lblAddAwardMessage, noAcademyAwardsToAssign);
                    pnlAddAward.Visible = true;
                }
            }
            else
            {
                myHelpers.DisplayMessage(lblErrorMessage, nullDataTableErrorMessage
                                                          + "dtActorActressMovies or dtDirectorMovies in AddCastDirectorAward/PopulateMovieDropDownList.");
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["personId"]))
                {
                    // Search is successful.
                    pnlNameSearch.Visible = false;
                    if (GetMoviePersonRecord(Request.QueryString["personId"]))
                    {
                        pnlMoviePersonInformation.Visible = pnlFilmography.Visible = true;
                        if (GetFilmographyRecords(Request.QueryString["personId"]))
                        { if (PopulateMovieDropDownList()) { pnlAddAward.Visible = true; } }
                    }
                }
            }
        }

        protected void BtnAddCastDirectorAward_Click(object sender, EventArgs e) // Uses TODO 27
        {
            rfvDDLMovie.Visible = true;
            Page.Validate("DDLMovie");
            Page.Validate("DDLAward");

            if (IsValid && !isSqlError)
            {
                //***********************************************************************************
                // Uses TODO 27 to add an academy award for an actor/actress/director for a movie. *
                //***********************************************************************************
                if (myReelflicsDB.AddAcademyAwardForMoviePersonForMovie(ddlMovie.SelectedValue,
                                                                       Request.QueryString["personId"],
                                                                       ddlAward.SelectedValue))
                { Response.Redirect("~/Shared/MoviePersonInformation.aspx?personId=" + Request.QueryString["personId"]); }
                else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.
            }
        }

        protected void BtnNameSearch_Click(object sender, EventArgs e) // Uses TODO 04
        {
            if (!string.IsNullOrEmpty(StringExtension.CleanInput(txtNameSearch.Text)))
            {
                //***********************************************************************
                // Uses TODO 04 to populate a GridView with the names of movie persons. *
                //***********************************************************************
                if (myHelpers.PopulateGridView("TODO 04",
                                               gvNameSearchResult,
                                               myReelflicsDB.GetMoviePersonSearchResult(StringExtension.CleanInput(txtNameSearch.Text)),
                                               new List<string> { "PERSONID", "NAME" },
                                               lblErrorMessage,
                                               lblNameSearchResultMessage,
                                               noMoviePersonMatches))
                {
                    if (!isEmptyQueryResult)
                    {
                        pnlNameSearch.Visible = lblNameSearchResultMessage.Visible = false;
                        pnlNameSearchResult.Visible = true;
                    }
                    else { lblNameSearchResultMessage.Visible = true; }
                }
            }
        }

        protected void DdlAward_SelectedIndexChanged(object sender, EventArgs e)
        {
            Page.Validate("DDLMovie");
            Page.Validate("DDLAward");
        }

        protected void DdlMovie_SelectedIndexChanged(object sender, EventArgs e) // Uses TODO 16
        {
            Page.Validate("DDLMovie");
            if (IsValid && ddlMovie.SelectedIndex != 0)
            {
                DataTable dtAcademyAwards = PopulateAwardDropDownList(ddlMovie.SelectedValue);
                if (dtAcademyAwards != null) // An SQL error occurred if the DataTable is null.
                {
                    // Uses the result of TODO 16 to populate the award dropdown list.
                    if (myHelpers.PopulateDropDownList("TODO 16",
                                                       ddlAward,
                                                       dtAcademyAwards,
                                                       new List<string> { "AWARDID", "AWARDNAME" },
                                                       lblErrorMessage,
                                                       lblSelectAwardMessage,
                                                       noAcademyAwardsToAssign,
                                                       EmptyQueryResultMessageType.Information))
                    { ddlAward.Visible = true; lblSelectAwardMessage.Visible = false; }
                }
            }
            else
            {
                ddlAward.Visible = false;
                lblSelectAwardMessage.Visible = true;
            }
        }

        protected void GvActorActressFilmography_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview data.
            if (e.Row.Cells.Count == 5)
            {
                // GridView columns: 0-MOVIEID, 1-TITLE, 2-ROLE, 3-RELEASEYEAR, 4-AWARDNAME
                int movieIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MOVIEID", lblErrorMessage);     // index 0
                int titleColumn = myHelpers.GetGridViewColumnIndexByName(sender, "TITLE", lblErrorMessage);         // index 1
                int roleColumn = myHelpers.GetGridViewColumnIndexByName(sender, "ROLE", lblErrorMessage);           // index 2
                int awardNameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "AWARDNAME", lblErrorMessage); // index 4

                if (movieIdColumn != -1 && titleColumn != -1 && roleColumn != -1 && awardNameColumn != -1)
                {
                    e.Row.Cells[movieIdColumn].Visible = false; // Hide the movieId column.

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Format the role column.
                        e.Row.Cells[roleColumn].Text = "<span style=\"font-weight: lighter; font-size: smaller\">("
                                                       + e.Row.Cells[roleColumn].Text
                                                       + ")</span>";
                        // Format acting academy award, if any
                        if (e.Row.Cells[awardNameColumn].Text.ToString() != "&nbsp;")
                        {
                            e.Row.Cells[awardNameColumn].Text = "<span style =\"font-size: smaller; color: goldenrod\">("
                                                                + e.Row.Cells[awardNameColumn].Text.ToString()
                                                                + ")</span>";
                        }
                    }
                }
            }
        }

        protected void GvDirectorFilmography_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview data.
            if (e.Row.Cells.Count == 4)
            {
                // GridView columns: 0-MOVIEID, 1-TITLE, 2-RELEASEYEAR, 3-AWARDNAME
                int movieIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "MOVIEID", lblErrorMessage);     // index 0
                int titleColumn = myHelpers.GetGridViewColumnIndexByName(sender, "TITLE", lblErrorMessage);         // index 1
                int awardNameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "AWARDNAME", lblErrorMessage); // index 3

                if (movieIdColumn != -1 && titleColumn != -1 && awardNameColumn != -1)
                {
                    e.Row.Cells[movieIdColumn].Visible = false; // Hide the movieId  column.

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Format directing academy award, if any
                        if (e.Row.Cells[awardNameColumn].Text.ToString() != "&nbsp;")
                        {
                            e.Row.Cells[awardNameColumn].Text = "<span style =\"font-size: smaller; color: goldenrod\">("
                                                                + e.Row.Cells[awardNameColumn].Text.ToString()
                                                                + ")</span>";
                        }
                    }
                }
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
                        titleCell.Controls.Add(new HyperLink
                        {
                            NavigateUrl = "~/Employee/AddCastDirectorAward.aspx?personId="
                                          + e.Row.Cells[personIdColumn].Text,
                            Text = titleCell.Text
                        });
                    }
                }
            }
        }
    }
}
