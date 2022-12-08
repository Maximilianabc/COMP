using static ReelflicsWebsite.Global;
using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ReelflicsWebsite
{
    public partial class MoviePersonInformation : Page
    {
        //*********************************
        // Uses TODO 22, TODO 23, TODO 24 *
        //*********************************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/

        private void GetFilmographyRecords(string personId) // Uses TODO 23, TODO 24
        {
            //***************************************************************************
            // Uses TODO 23 to populate a gridview with an actor's flimography records. *
            //***************************************************************************
            if (myHelpers.PopulateGridView("TODO 23",
                                           gvActorActressFilmography,
                                           myReelflicsDB.GetCastMemberFilmography(personId),
                                           new List<string> { "MOVIEID", "TITLE", "RELEASEYEAR", "ROLE", "AWARDNAME" },
                                           lblErrorMessage,
                                           lblErrorMessage,
                                           null))
            {
                if (!isEmptyQueryResult) { pnlFilmography.Visible = pnlActorActressFilmography.Visible = true; }
            }
            else { return; } // An SQL error occurred.

            //*****************************************************************************
            // Uses TODO 24 to populate a gridview with a director's filmography records. *
            //*****************************************************************************
            if (myHelpers.PopulateGridView("TODO 24",
                                           gvDirectorFilmography,
                                           myReelflicsDB.GetDirectorFilmography(personId),
                                           new List<string> { "MOVIEID", "TITLE", "RELEASEYEAR", "AWARDNAME" },
                                           lblErrorMessage,
                                           lblErrorMessage,
                                           null))
            {
                if (!isEmptyQueryResult) { pnlFilmography.Visible = pnlDirectorFilmography.Visible = true; }
            }
        }

        private bool GetMoviePersonRecord(string personId) // Uses TODO 22
        {
            bool result = false;
            //************************************************************************************************
            // Uses TODO 22 to retrieve the record of an actor or director identified by his/her person id. *
            //************************************************************************************************
            DataTable dtMoviePerson = myReelflicsDB.GetMoviePersonRecord(personId);

            // Show the actor/director information if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 22",
                                             dtMoviePerson,
                                             new List<string> { "PERSONID", "NAME", "BIOGRAPHY", "GENDER", "BIRTHDATE", "DEATHDATE" },
                                             lblErrorMessage))
            {
                if (dtMoviePerson.Rows.Count == 1) // Only one record should be retrieved.
                {
                    // Create the actor/director's image photo filename from his/her name.
                    imgPhoto.ImageUrl += personId + StringExtension.CreateFileName(dtMoviePerson.Rows[0]["NAME"].ToString());
                    litName.Text = HttpUtility.HtmlDecode(dtMoviePerson.Rows[0]["NAME"].ToString());
                    litBiography.Text = HttpUtility.HtmlDecode(dtMoviePerson.Rows[0]["BIOGRAPHY"].ToString());
                    litGender.Text += dtMoviePerson.Rows[0]["GENDER"].ToString() == "M" ? "Male" : "Female";
                    if (dtMoviePerson.Rows[0]["GENDER"].ToString() == "F") { litActorActressHeading.Text = "Actress"; }

                    // Set the birthday, if known.
                    if (!string.IsNullOrEmpty(dtMoviePerson.Rows[0]["BIRTHDATE"].ToString()))
                    {
                        litBirthdate.Text += ((DateTime)dtMoviePerson.Rows[0]["BIRTHDATE"]).ToString("d MMMMMM yyyy");
                        // Check if the person has died.
                        if (string.IsNullOrEmpty(dtMoviePerson.Rows[0]["DEATHDATE"].ToString()))
                        { litBirthdate.Text += " (age " + StringExtension.Age((DateTime)dtMoviePerson.Rows[0]["BIRTHDATE"]) + " years)"; ; }
                    }
                    else { litBirthdate.Text += "unknown"; }

                    // Set the deathdate, if any.
                    if (!string.IsNullOrEmpty(dtMoviePerson.Rows[0]["DEATHDATE"].ToString()))
                    {
                        litDeathdate.Text += ((DateTime)dtMoviePerson.Rows[0]["DEATHDATE"]).ToString("d MMMMMM yyyy");
                        // Check if the birthdate is known.
                        if (!string.IsNullOrEmpty(dtMoviePerson.Rows[0]["BIRTHDATE"].ToString()))
                        {
                            litDeathdate.Text += " (age " + StringExtension.AgeAtDeath((DateTime)dtMoviePerson.Rows[0]["BIRTHDATE"],
                                                                                     (DateTime)dtMoviePerson.Rows[0]["DEATHDATE"]) + " years)";
                        }
                        litDeathdate.Visible = true;
                    }
                    result = true;
                }
                else if (dtMoviePerson.Rows.Count == 0) // No record was retrieved.
                { myHelpers.DisplayMessage(lblErrorMessage, dbqueryError + "TODO 22" + dbqueryErrorNoRecordsRetrieved); }
                else // Multiple records were retrieved.
                { myHelpers.DisplayMessage(lblErrorMessage, queryError + "TODO 22" + queryErrorMultipleRecordsRetrieved); }
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (!string.IsNullOrEmpty(Request.QueryString["personId"]))
                {
                    {
                        if (!GetMoviePersonRecord(Request.QueryString["personId"])) { return; }
                        pnlMoviePersonInformation.Visible = true;
                        GetFilmographyRecords(Request.QueryString["personId"]);
                    }
                }
                else { Response.Redirect("~/Default.aspx"); }
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
                        // Change the movie title to a hyperlink.
                        var titleCell = e.Row.Cells[titleColumn];
                        titleCell.Controls.Clear();
                        titleCell.Controls.Add(new HyperLink
                        {
                            NavigateUrl = "~/Shared/MovieInformation.aspx?movieId="
                                          + e.Row.Cells[movieIdColumn].Text,
                            Text = titleCell.Text
                        });
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
                    e.Row.Cells[movieIdColumn].Visible = false; // Hide the movieId column.

                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Change the movie title to a hyperlink.
                        var titleCell = e.Row.Cells[titleColumn];
                        titleCell.Controls.Clear();
                        titleCell.Controls.Add(new HyperLink
                        {
                            NavigateUrl = "~/Shared/MovieInformation.aspx?movieId="
                                          + e.Row.Cells[movieIdColumn].Text,
                            Text = titleCell.Text
                        });
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
    }
}