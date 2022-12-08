using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Employee
{
    public partial class ModifyMovie : Page
    {
        //*********************************************************************************************************************************************
        // Uses TODO 03, TODO 04, TODO 05, TODO 06, TODO 10, TODO 11, TODO 18, TODO 19, TODO 20, TODO 21, TODO 28, TODO 29, TODO 30, TODO 31, TODO 32 *
        //*********************************************************************************************************************************************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/

        private DataTable AddCastDirector(DataTable dt, string personId, string name)
        {
            // Add cast/director if not already in the DataTable; change status to 'existing' if status is 'remove'.
            if (myHelpers.IsRecordInDataTable(dt, "PERSONID", personId))
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["PERSONID"].ToString() == personId
                        && row["STATUS"].ToString() == RecordStatus.remove.ToString())
                    { row["STATUS"] = RecordStatus.existing; break; }
                }
            }
            else // Add the selected movie person to the list of cast members/directors.
            {
                DataRow dr = dt.NewRow();
                dr["PERSONID"] = personId;
                dr["NAME"] = name;
                dr["STATUS"] = RecordStatus.add;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private DataTable GetCastChanges(DataTable dt)
        {
            DataTable dtCastChanges = dt.Copy();
            // Remove rows where status is 'existing' as no change is needed.
            for (int i = dtCastChanges.Rows.Count - 1; i >= 0; i--)
            {
                if (dtCastChanges.Rows[i]["STATUS"].ToString() == RecordStatus.existing.ToString())
                {
                    dtCastChanges.Rows[i].Delete();
                    dtCastChanges.AcceptChanges();
                }
            }
            return dtCastChanges;
        }

        private bool GetCastRecords(string movieId) // Uses TODO 11
        {
            bool result = false;
            //******************************************************************************
            // Uses TODO 11 to get the person id, name and role of a movie's cast members. *
            //******************************************************************************
            DataTable dtCast = myReelflicsDB.GetMovieCastMembers(movieId);

            // Return the cast member records if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 11",
                                             dtCast,
                                             new List<string> { "PERSONID", "NAME", "ROLE" },
                                             lblErrorMessage))
            {
                // Add a column to the DataTable with default value 'existing'.
                dtCast = myHelpers.AddDataTableColumnWithDefaultValue(dtCast, "STATUS", RecordStatus.existing.ToString());
                if (dtCast.Rows.Count != 0)
                {
                    gvCast.DataSource = dtCast;
                    gvCast.DataBind();
                    gvCast.Visible = true;
                }
                else { myHelpers.DisplayMessage(lblNoCast, noneAssigned); } // No cast members are assigned.
                ViewState["dtCast"] = dtCast;
                result = true;
            }
            return result;
        }

        private DataTable GetDirectorChanges(DataTable dt)
        {
            DataTable dtDirectorChanges = dt.Copy();
            // Remove rows where status is 'existing' as no change is needed.
            for (int i = dtDirectorChanges.Rows.Count - 1; i >= 0; i--)
            {
                if (dtDirectorChanges.Rows[i]["STATUS"].ToString() == RecordStatus.existing.ToString())
                {
                    dtDirectorChanges.Rows[i].Delete();
                    dtDirectorChanges.AcceptChanges();
                }
            }
            return dtDirectorChanges;
        }

        private bool GetDirectorRecords(string movieId) // Uses TODO 10
        {
            bool result = false;
            //*********************************************************************
            // Uses TODO 10 to get the person id and name of a movie's directors. *
            //*********************************************************************
            DataTable dtDirectors = myReelflicsDB.GetMovieDirectors(movieId);

            // Return the director records if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 10",
                                             dtDirectors,
                                             new List<string> { "PERSONID", "NAME" },
                                             lblErrorMessage))
            {
                // Add a column to the DataTable with default value 'existing'.
                dtDirectors = myHelpers.AddDataTableColumnWithDefaultValue(dtDirectors, "STATUS", RecordStatus.existing.ToString());
                if (dtDirectors.Rows.Count != 0)
                {
                    gvDirectors.DataSource = dtDirectors;
                    gvDirectors.DataBind();
                    gvDirectors.Visible = true;
                }
                else { myHelpers.DisplayMessage(lblNoDirectors, noneAssigned); } // No directors are assigned.
                ViewState["dtDirectors"] = dtDirectors;
                result = true;
            }
            return result;
        }

        private DataTable GetMovieGenreChanges(DataTable dt)
        {
            DataTable dtMovieGenreChanges = dt.Copy();

            // Add a status column with status 'remove' for the movie's original genres.
            dtMovieGenreChanges = myHelpers.AddDataTableColumnWithDefaultValue(dtMovieGenreChanges, "STATUS", RecordStatus.remove.ToString());

            // First, get any new genres added in the textbox as a string array.
            string[] textboxGenres = txtGenres.Text.Split(',');

            // Second, check if any of the new genres already exist in the listbox.
            for (int i = 0; i < textboxGenres.Length; i++)
            {
                for (int j = 0; j < lbxGenres.Items.Count; j++)
                {
                    if (textboxGenres[i].Trim() == lbxGenres.Items[j].Text)
                    {
                        // When there is a match, select the new genre in the ListBox and set it to null in the string array.
                        lbxGenres.Items[j].Selected = true;
                        textboxGenres[i] = null;
                        break;
                    }
                }
            }

            // Third, for each genre that is selected in the listbox, compare it with the movie's original genres.
            // 1. If there is a match, then delete the genre from the movie genre changes DataTable as no action is needed (i.e., it is still selected).
            // 2. If there is no match, then add the genre as a genre to be added for the movie (i.e., it is an additional genre for the movie).
            // 3. Any unselected original genres will have status 'remove' and so will be deleted.

            for (int i = 0; i < lbxGenres.Items.Count; i++) // Loop through the listbox genres.
            {
                if (lbxGenres.Items[i].Selected) // Process only the genres that are selected.
                {
                    bool isExistingGenre = false;
                    for (int j = dtMovieGenreChanges.Rows.Count - 1; j >= 0; j--)
                    {
                        if (lbxGenres.Items[i].Text == dtMovieGenreChanges.Rows[j]["GENRE"].ToString()) // The selected genre is one of the movie's original genres.
                        {
                            dtMovieGenreChanges.Rows[j].Delete(); // So, remove it from the movie genres change DataTable.
                            dtMovieGenreChanges.AcceptChanges();
                            isExistingGenre = true;
                        }
                    }
                    if (!isExistingGenre) // If the selected genre is a new genre, add it to the movie genres change DataTable with status 'add'.
                    {
                        DataRow dr = dtMovieGenreChanges.NewRow();
                        dr["MOVIEID"] = Request.QueryString["movieId"];
                        dr["GENRE"] = lbxGenres.Items[i].Text;
                        dr["STATUS"] = RecordStatus.add;
                        dtMovieGenreChanges.Rows.Add(dr);
                    }
                }
            }

            // Fourth, add any remaining genres in the string array to the movie genres change DataTable with status 'add'.
            for (int i = 0; i < textboxGenres.Length; i++)
            {
                if (textboxGenres[i] != null && textboxGenres[i].Trim() != "")
                {
                    DataRow dr = dtMovieGenreChanges.NewRow();
                    dr["MOVIEID"] = Request.QueryString["movieId"];
                    dr["GENRE"] = textboxGenres[i].Trim();
                    dr["STATUS"] = RecordStatus.add;
                    dtMovieGenreChanges.Rows.Add(dr);
                }
            }
            return dtMovieGenreChanges;
        }

        private bool GetMovieRecord(string movieId) // Uses TODO 05
        {
            bool result = false;
            //****************************************
            // Uses TODO 05 to get a movie's record. *
            //****************************************
            DataTable dtMovie = myReelflicsDB.GetMovieRecord(movieId);

            // Show the movie information if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 05",
                                             dtMovie,
                                             new List<string> { "MOVIEID", "TITLE", "SYNOPSIS", "RUNNINGTIME", "RELEASEYEAR", "MPAARATING", "IMDBRATING", "BESTPICTUREAWARDID" },
                                             lblErrorMessage))
            {
                if (dtMovie.Rows.Count == 1) // Only one record should be retrieved.
                {
                    // Create the movie's image poster filename from the title.
                    imgPoster.ImageUrl += movieId + StringExtension.CreateFileName(dtMovie.Rows[0]["TITLE"].ToString());
                    // Assign values to their controls and save all values in ViewState for checking if anything was changed.
                    ViewState["oldTitle"] = txtTitle.Text = HttpUtility.HtmlDecode(dtMovie.Rows[0]["TITLE"].ToString());
                    ViewState["oldSynopsis"] = txtSynopsis.Text = HttpUtility.HtmlDecode(dtMovie.Rows[0]["SYNOPSIS"].ToString());
                    ViewState["oldReleaseYear"] = txtReleaseYear.Text = dtMovie.Rows[0]["RELEASEYEAR"].ToString();
                    ViewState["oldRunningTime"] = txtRunningTime.Text = dtMovie.Rows[0]["RUNNINGTIME"].ToString();
                    ViewState["oldMPAARating"] = ddlMPAARating.SelectedValue = dtMovie.Rows[0]["MPAARATING"].ToString();
                    ViewState["oldIMDBRating"] = txtIMDBRating.Text = dtMovie.Rows[0]["IMDBRATING"].ToString();
                    ViewState["oldBestPictureAwardId"] = rblIsBestPicture.SelectedValue = dtMovie.Rows[0]["BESTPICTUREAWARDID"].ToString() == bestPictureAwardId ? "Y" : "N";
                    result = true;
                }
                else
                {
                    if (dtMovie.Rows.Count > 1) // Multiple records were retrieved.
                    { myHelpers.DisplayMessage(lblErrorMessage, queryError + "TODO 05" + queryErrorMultipleRecordsRetrieved); }
                    else // No record was retrieved.
                    { myHelpers.DisplayMessage(lblErrorMessage, dbqueryError + "TODO 05" + dbqueryErrorNoRecordsRetrieved); }
                }
            }
            return result;
        }

        private bool IsAnyAssigned(DataTable dt)
        {
            // Returns true if there is at least one row in the DataTable; else returns false.
            bool result = false;
            foreach (DataRow row in dt.Rows)
            {
                if (row["STATUS"].ToString() == RecordStatus.existing.ToString()
                    || row["STATUS"].ToString() == RecordStatus.add.ToString())
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private bool IsMovieRecordChanged(string newTitle, string newSynopsis, string newReleaseYear, string newRunningTime, string newMPAARating,
            string newIMDBRating, string newBestPictureAwardId)
        {
            if (ViewState["oldTitle"].ToString() == newTitle
                && ViewState["oldSynopsis"].ToString() == newSynopsis
                && ViewState["oldReleaseYear"].ToString() == newReleaseYear
                && ViewState["oldRunningTime"].ToString() == newRunningTime
                && ViewState["oldMPAARating"].ToString() == newMPAARating
                && ViewState["oldIMDBRating"].ToString() == newIMDBRating
                && ViewState["oldBestPictureAwardId"].ToString() == newBestPictureAwardId)
            { return false; } // The movie record was not modified.
            else
            { return true; } // The movie record was modified.
        }

        private bool IsPosterChanged(string movieId, string title)
        {
            bool result = false;
            string newFilename = postersDirectory + movieId + StringExtension.CreateFileName(title);
            string oldFilename = postersDirectory + movieId + StringExtension.CreateFileName(ViewState["oldTitle"].ToString());

            // Replace the old photo with the new one if the photo has changed.
            if ((bool)ViewState["hasNewPoster"])
            {
                File.Delete(Server.MapPath(oldFilename));
                File.Copy(Server.MapPath(tempPosterImage), Server.MapPath(newFilename));
                result = true;
            }
            // Change the posters's file name if the movie's title has changed.
            else if (newFilename != oldFilename)
            {
                File.Delete(Server.MapPath(newFilename));
                File.Copy(Server.MapPath(oldFilename), Server.MapPath(newFilename));
                File.Delete(Server.MapPath(oldFilename));
                result = true;
            }
            return result;
        }

        private bool PopulateGenresListBox() // Uses TODO 19
        {
            bool result = false;
            //***********************************************************************************************************
            // Uses TODO 19 to populate the genre listbox with the distinct movie genres of the movies in the database. *
            //***********************************************************************************************************
            if (myHelpers.PopulateListBox("TODO 19",
                                          lbxGenres,
                                          myReelflicsDB.GetGenres(),
                                          new List<string> { "GENRE" },
                                          lblErrorMessage,
                                          lblErrorMessage,
                                          queryErrorNoRecordsRetrieved,
                                          EmptyQueryResultMessageType.DBQueryError))
            {
                if (SetMovieGenresListBox(Request.QueryString["movieId"])) { result = true; }
            }
            return result;
        }

        private bool SetMovieGenresListBox(string movieId) // USES TODO 06
        {
            bool result = false;
            //****************************************
            // Uses TODO 06 to get a movie's genres. *
            //****************************************
            DataTable dtMovieGenres = myReelflicsDB.GetMovieGenres(movieId);

            // Set the movie's genres if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 06",
                                             dtMovieGenres,
                                             new List<string> { "MOVIEID", "GENRE" },
                                             lblErrorMessage))
            {
                if (dtMovieGenres.Rows.Count != 0)
                {
                    // Set the selected genres.
                    foreach (DataRow row in dtMovieGenres.Rows)
                    {
                        for (int i = 0; i < lbxGenres.Items.Count; i++)
                        {
                            if (lbxGenres.Items[i].Text == row["GENRE"].ToString())
                            { lbxGenres.Items[i].Selected = true; }
                        }
                    }
                    // Save the movie's original genres in ViewState for later use.
                    ViewState["dtMovieGenres"] = dtMovieGenres;
                    result = true;
                }
                else
                {
                    myHelpers.DisplayMessage(lblErrorMessage, dbqueryError
                                                              + "TODO 06"
                                                              + dbqueryErrorNoRecordsRetrieved);
                }
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Show the movie information panel if there is a movie id in the query string.
                if (!string.IsNullOrEmpty(Request.QueryString["movieId"]))
                {
                    pnlMovieInformation.Visible = true;
                    if (!GetMovieRecord(Request.QueryString["movieId"])) { return; } // Uses TODO 05
                    pnlPoster.Visible = fuPoster.Visible = true;
                    if (!PopulateGenresListBox()) { return; } // Uses TODO 19
                    pnlCastDirectorsInformation.Visible = pnlDirectors.Visible = true;
                    if (!GetDirectorRecords(Request.QueryString["movieId"])) { return; } // Uses TODO 10
                    pnlDirectorsSearch.Visible = pnlCast.Visible = true;
                    if (!GetCastRecords(Request.QueryString["movieId"])) { return; } // Uses TODO 11
                    pnlCastSearch.Visible = pnlBtnModify.Visible = true;
                    ViewState["hasNewPoster"] = false;
                }
                // Show the movie search panel if there is no movie id in the query string.
                else
                { pnlMovieSearch.Visible = true; }
            }
        }

        protected void BtnCastSearch_Click(object sender, EventArgs e) // Uses TODO 04
        {
            lblModifyMovieMessage.Visible = lblCastSearchResultMessage.Visible = ddlCastSearchResult.Visible = false;
            Validate("RoleNames");
            if (IsValid)
            {
                if (!string.IsNullOrEmpty(StringExtension.CleanInput(txtSearchCast.Text)))
                {
                    //**********************************************************************************
                    // Uses TODO 04 to populate the cast member dropdown list with movie person names. *
                    //**********************************************************************************
                    if (myHelpers.PopulateDropDownList("TODO 04",
                                                       ddlCastSearchResult,
                                                       myReelflicsDB.GetMoviePersonSearchResult(StringExtension.CleanInput(txtSearchCast.Text)),
                                                       new List<string> { "PERSONID", "NAME" },
                                                       lblErrorMessage,
                                                       lblCastSearchResultMessage,
                                                       noMoviePersonMatches,
                                                       EmptyQueryResultMessageType.Information))
                    {
                        ddlCastSearchResult.Items[0].Text = "-- Select cast member --";
                        ddlCastSearchResult.Visible = true;
                    }
                }
            }
        }

        protected void BtnDirectorSearch_Click(object sender, EventArgs e) // Uses TODO 04
        {
            lblModifyMovieMessage.Visible = lblDirectorSearchResultMessage.Visible = ddlDirectorsSearchResult.Visible = false;
            Validate("RoleNames");
            if (IsValid)
            {
                if (!string.IsNullOrEmpty(StringExtension.CleanInput(txtSearchDirector.Text)))
                {
                    //********************************************************************************
                    // Uses TODO 04 to populate the directors dropdown list with movie person names. *
                    //********************************************************************************
                    if (myHelpers.PopulateDropDownList("TODO 04",
                                                       ddlDirectorsSearchResult,
                                                       myReelflicsDB.GetMoviePersonSearchResult(StringExtension.CleanInput(txtSearchDirector.Text)),
                                                       new List<string> { "PERSONID", "NAME" },
                                                       lblErrorMessage,
                                                       lblDirectorSearchResultMessage,
                                                       noMoviePersonMatches,
                                                       EmptyQueryResultMessageType.Information))
                    {
                        ddlDirectorsSearchResult.Items[0].Text = "-- Select director --";
                        ddlDirectorsSearchResult.Visible = true;
                    }
                }
            }
        }

        protected void BtnModifyMovie_Click(object sender, EventArgs e) // Uses TODO 18, TODO 20, TODO 21, TODO 28, TODO 29, TODO 30, TODO 31, TODO 32
        {
            Validate("RoleNames");
            if (IsValid && !isSqlError)
            {
                bool isRecordChanged = false;
                lblModifyMovieMessage.Visible = false;

                DataTable dtMovieGenreChanges = GetMovieGenreChanges(ViewState["dtMovieGenres"] as DataTable);
                DataTable dtDirectorChanges = GetDirectorChanges(ViewState["dtDirectors"] as DataTable);
                DataTable dtCastChanges = GetCastChanges(ViewState["dtCast"] as DataTable);

                // Determine if the movie's record, genres, directors or cast have changed.
                bool isMovieInformationChanged = IsMovieRecordChanged(txtTitle.Text.Trim(),
                                                                      txtSynopsis.Text.Trim(),
                                                                      txtReleaseYear.Text,
                                                                      txtRunningTime.Text.Trim(),
                                                                      ddlMPAARating.SelectedValue,
                                                                      txtIMDBRating.Text.Trim(),
                                                                      rblIsBestPicture.SelectedValue);
                if (isMovieInformationChanged
                    || dtMovieGenreChanges.Rows.Count != 0
                    || dtDirectorChanges.Rows.Count != 0
                    || dtCastChanges.Rows.Count != 0)
                {
                    isRecordChanged = true;
                    //******************************************************************************
                    // Uses TODO 18, TODO 20, TODO 21, TODO 28, TODO 29, TODO 30, TODO 31, TODO 32 *
                    // *****************************************************************************
                    if (!myDBHelpers.ModifyMovieInformation(isMovieInformationChanged,
                                                            Request.QueryString["movieId"],
                                                            StringExtension.CleanInput(txtTitle.Text),
                                                            StringExtension.CleanInput(txtSynopsis.Text),
                                                            txtReleaseYear.Text,
                                                            txtRunningTime.Text.Trim(),
                                                            ddlMPAARating.SelectedValue,
                                                            txtIMDBRating.Text.Trim(),
                                                            rblIsBestPicture.SelectedValue == "Y" ? bestPictureAwardId : "null",
                                                            dtMovieGenreChanges,
                                                            dtDirectorChanges,
                                                            dtCastChanges,
                                                            lblErrorMessage))
                    { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); return; } // An SQL error occurred.
                }

                // Redirect if the poster or movie record have changed.
                if (IsPosterChanged(Request.QueryString["movieId"], StringExtension.CleanInput(txtTitle.Text)) || isRecordChanged)
                { Response.Redirect("~/Shared/MovieInformation.aspx?movieId=" + Request.QueryString["movieId"]); }
                else // The poster and movie record were not changed.
                { myHelpers.DisplayMessage(lblModifyMovieMessage, informationNotChanged); }
            }
        }

        protected void BtnMovieSearch_Click(object sender, EventArgs e) // Uses TODO 03
        {
            lblMovieSearchResultMessage.Visible = false;
            if (!string.IsNullOrEmpty(StringExtension.CleanInput(txtMovieSearch.Text)))
            {
                //**********************************************************
                // Uses TODO 03 to populate a GridView with movie titles . *
                //**********************************************************
                if (myHelpers.PopulateGridView("TODO 03",
                                               gvMovieSearchResult,
                                               myReelflicsDB.GetMovieSearchResult(StringExtension.CleanInput(txtMovieSearch.Text)),
                                               new List<string> { "MOVIEID", "TITLE", "RELEASEYEAR" },
                                               lblErrorMessage,
                                               lblMovieSearchResultMessage,
                                               noMovieMatches))
                {
                    if (!isEmptyQueryResult)
                    {
                        lblMovieSearchResultMessage.Visible = false;
                        pnlMovieSearchResult.Visible = true;
                    }
                    else { lblMovieSearchResultMessage.Visible = true; }
                }
            }
        }

        protected void BtnUploadPoster_Click(object sender, EventArgs e)
        {
            Validate("UploadPoster");
            if (IsValid && fuPoster.HasFile)
            {
                lblUploadMessage.Text = "Poster file: " + fuPoster.FileName.ToString();
                txtUploadMessage.Text = fuPoster.FileName.ToString();
                fuPoster.SaveAs(Server.MapPath(tempPosterImage));
                imgPoster.ImageUrl = tempPosterImage;
                ViewState["hasNewPoster"] = true;
            }
        }

        protected void CvIsDirectorAssigned_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!IsAnyAssigned(ViewState["dtDirectors"] as DataTable))
            { args.IsValid = gvDirectors.Visible = false; }
        }

        protected void CvMovieGenres_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (lbxGenres.SelectedIndex == -1 && string.IsNullOrEmpty(txtGenres.Text))
            {
                args.IsValid = false;
                Validate("NewMovieGenres");
            }
        }

        protected void CvNewMovieGenres_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string[] textboxGenres = txtGenres.Text.Split(',');
            for (int i = 0; i < textboxGenres.Length; i++)
            {
                if (textboxGenres[i].Length > maxGenreLength)
                {
                    cvNewMovieGenres.Text = "The genre '"
                                            + textboxGenres[i]
                                            + "' exceeds the allowed maximum length of 15 characters.";
                    args.IsValid = false;
                }
            }
        }

        protected void DdlCastSearchResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate("RoleNames");
            if (IsValid && ddlCastSearchResult.SelectedIndex != 0)
            {
                DataTable dtCast = ViewState["dtCast"] as DataTable;

                // Add the selected movie person to the list of cast members.
                dtCast = AddCastDirector(dtCast, ddlCastSearchResult.SelectedValue, ddlCastSearchResult.SelectedItem.Text);

                //Save the modified cast DataTable in ViewState and rebind the GridView.
                ViewState["dtCast"] = dtCast;
                gvCast.DataSource = dtCast;
                gvCast.DataBind();
                gvCast.Visible = true;
                lblNoCast.Visible = false;
            }
            ddlCastSearchResult.SelectedIndex = 0;
        }

        protected void DdlDirectorsSearchResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate("RoleNames");
            if (IsValid && ddlDirectorsSearchResult.SelectedIndex != 0)
            {
                DataTable dtDirectors = ViewState["dtDirectors"] as DataTable;

                // Add the selected movie person to the list of directors.
                dtDirectors = AddCastDirector(dtDirectors, ddlDirectorsSearchResult.SelectedValue, ddlDirectorsSearchResult.SelectedItem.Text);

                //Save the modified directors DataTable in ViewState and rebind the GridView.
                ViewState["dtDirectors"] = dtDirectors;
                gvDirectors.DataSource = dtDirectors;
                gvDirectors.DataBind();
                gvDirectors.Visible = true;
                lblNoDirectors.Visible = false;
            }
            ddlDirectorsSearchResult.SelectedIndex = 0;
        }

        protected void GvCast_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview data.
            if (e.Row.Cells.Count == 5)
            {
                // GridView columns: 0-SELECT, 1-PERSONID, 2-NAME, 3-ROLE, 4-STATUS
                int personIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PERSONID", lblErrorMessage) + 1; // index 1
                int nameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "NAME", lblErrorMessage) + 1;         // index 2
                int roleColumn = myHelpers.GetGridViewColumnIndexByName(sender, "ROLE", lblErrorMessage) + 1;         // index 3
                int statusColumn = myHelpers.GetGridViewColumnIndexByName(sender, "STATUS", lblErrorMessage) + 1;     // index 4

                if (personIdColumn != 0 && nameColumn != 0 && roleColumn != 0 && statusColumn != 0)
                {
                    e.Row.Cells[personIdColumn].Visible = e.Row.Cells[statusColumn].Visible = false; // Hide the personId and status columns.
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Hide removed cast members.
                        if (e.Row.Cells[statusColumn].Text == RecordStatus.remove.ToString()) { e.Row.Visible = false; }
                    }
                }
            }
        }

        protected void GvCast_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblModifyMovieMessage.Visible = false;
            DataTable dtCast = ViewState["dtCast"] as DataTable;

            // Change the status to 'remove' if it is 'existing'.
            if (dtCast.Rows[e.RowIndex]["STATUS"].ToString() == RecordStatus.existing.ToString())
            { dtCast.Rows[e.RowIndex]["STATUS"] = RecordStatus.remove; }

            // Remove the row if its status is 'add'.
            if (dtCast.Rows[e.RowIndex]["STATUS"].ToString() == RecordStatus.add.ToString())
            { myHelpers.RemoveDataTableRecord(dtCast, "PERSONID", dtCast.Rows[e.RowIndex]["PERSONID"].ToString(), equal); }

            // Save the modified cast DataTable in ViewState and rebind the GridView.
            ViewState["dtCast"] = dtCast;
            gvCast.DataSource = dtCast;
            gvCast.DataBind();

            // Show the 'none assigned' message if there are no cast members in the cast DataTable. 
            if (!IsAnyAssigned(dtCast)) { myHelpers.DisplayMessage(lblNoCast, noneAssigned); gvCast.Visible = false; }

        }

        protected void GvDirectors_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Format the gridview data.
            if (e.Row.Cells.Count == 4)
            {
                // GridView columns: 0-SELECT, 1-PERSONID, 2-NAME, 3-STATUS
                int personIdColumn = myHelpers.GetGridViewColumnIndexByName(sender, "PERSONID", lblErrorMessage) + 1; // index 1
                int nameColumn = myHelpers.GetGridViewColumnIndexByName(sender, "NAME", lblErrorMessage) + 1;         // index 2
                int statusColumn = myHelpers.GetGridViewColumnIndexByName(sender, "STATUS", lblErrorMessage) + 1;     // index 3

                if (personIdColumn != 0 && nameColumn != 0 && statusColumn != 0)
                {
                    e.Row.Cells[personIdColumn].Visible = e.Row.Cells[statusColumn].Visible = false; // Hide the personId and status columns.
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        // Hide removed directors.
                        if (e.Row.Cells[statusColumn].Text == RecordStatus.remove.ToString()) { e.Row.Visible = false; }
                    }
                }
            }
        }

        protected void GvDirectors_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblModifyMovieMessage.Visible = false;
            DataTable dtDirectors = ViewState["dtDirectors"] as DataTable;

            // Change the status to 'remove' if it is 'existing'.
            if (dtDirectors.Rows[e.RowIndex]["STATUS"].ToString() == RecordStatus.existing.ToString())
            { dtDirectors.Rows[e.RowIndex]["STATUS"] = RecordStatus.remove; }

            // Remove the row if its status is 'add'.
            if (dtDirectors.Rows[e.RowIndex]["STATUS"].ToString() == RecordStatus.add.ToString())
            { myHelpers.RemoveDataTableRecord(dtDirectors, "PERSONID", dtDirectors.Rows[e.RowIndex]["PERSONID"].ToString(), equal); }

            // Save the modified directors DataTable in ViewState and rebind the GridView.
            ViewState["dtDirectors"] = dtDirectors;
            gvDirectors.DataSource = dtDirectors;
            gvDirectors.DataBind();

            // Show the 'none assigned' message if there are no directors in the directors DataTable. 
            if (!IsAnyAssigned(dtDirectors)) { myHelpers.DisplayMessage(lblNoDirectors, noneAssigned); gvDirectors.Visible = false; }
        }

        protected void GvMovieSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        // Change the movie title to a hyperlink.
                        var titleCell = e.Row.Cells[titleColumn];
                        titleCell.Controls.Clear();
                        titleCell.Controls.Add(new HyperLink
                        {
                            NavigateUrl = "~/Employee/ModifyMovie.aspx?movieId="
                                          + e.Row.Cells[movieIdColumn].Text
                                          + "&title="
                                          + Server.HtmlEncode(e.Row.Cells[titleColumn].Text),
                            Text = titleCell.Text
                        });
                    }
                }
            }
        }

        protected void TxtRole_TextChanged(object sender, EventArgs e)
        {
            DataTable dtCast = ViewState["dtCast"] as DataTable;

            // Get the GridView row containing the TextBox that was changed.
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            // Change the status to 'modify' in the DataTable only if it is 'exisiting'.
            // If status is 'add' or 'modify', no change to the status is needed.
            TextBox txtRole = (TextBox)sender;
            dtCast.Rows[gvRow.RowIndex]["ROLE"] = txtRole.Text;
            if (dtCast.Rows[gvRow.RowIndex]["STATUS"].ToString() == RecordStatus.existing.ToString())
            { dtCast.Rows[gvRow.RowIndex]["STATUS"] = RecordStatus.modify; }

            // Save the modified cast DataTable in ViewState and rebind the GridView.
            ViewState["dtCast"] = dtCast;
            gvCast.DataSource = dtCast;
            gvCast.DataBind();
        }
    }
}