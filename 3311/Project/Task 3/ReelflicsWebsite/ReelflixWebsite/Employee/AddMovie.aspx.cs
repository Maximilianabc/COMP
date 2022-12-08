using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Employee
{
    public partial class AddMovie : Page
    {
        //************************************************************
        // Uses TODO 04, TODO 17, TODO 19, TODO 20, TODO 28, TODO 30 *
        //************************************************************

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
                    {
                        row["STATUS"] = RecordStatus.existing;
                        break;
                    }
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

        private DataTable GetMovieGenres(string movieId)
        {
            // Create a DataTable with default status 'add' to hold the genres selected in the listbox or added in the textbox.
            DataTable dt = new DataTable();
            dt.Columns.Add("MOVIEID").DefaultValue = movieId;
            dt.Columns.Add("GENRE");
            dt.Columns.Add("STATUS").DefaultValue = RecordStatus.add.ToString();

            // First, assign genres added in the textbox to a string array.
            string[] textboxGenres = txtGenres.Text.Split(',');

            // Second, check if a genre added in the textbox already exists in the listbox.
            // If yes, select it in the listbox and set its value to null in the string array.
            for (int i = 0; i < textboxGenres.Length; i++)
            {
                for (int j = 0; j < lbxGenres.Items.Count; j++)
                {
                    if (textboxGenres[i].Trim() == lbxGenres.Items[j].Text)
                    {
                        lbxGenres.Items[j].Selected = true;
                        textboxGenres[i] = null;
                        break;
                    }
                }
            }

            // Third, add the genres selected in the listbox to the DataTable.
            for (int i = 0; i < lbxGenres.Items.Count; i++)
            {
                if (lbxGenres.Items[i].Selected)
                {
                    DataRow dr = dt.NewRow();
                    dr["GENRE"] = lbxGenres.Items[i].Text;
                    dt.Rows.Add(dr);
                }
            }

            // Fourth, add any remaining genres added in the textbox to the DataTable.
            for (int i = 0; i < textboxGenres.Length; i++)
            {
                if (textboxGenres[i] != null && textboxGenres[i].Trim() != "")
                {
                    DataRow dr = dt.NewRow();
                    dr["GENRE"] = textboxGenres[i].Trim();
                    dt.Rows.Add(dr);
                }
            }
            return dt;
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

        private bool PopulateGenresListBox() // Uses TODO 19
        {
            bool result = false;
            //************************************************************************************************************
            // Uses TODO 19 to populate the genre listbox with the distinct movie genres of the movies in the database. *
            //************************************************************************************************************
            if (myHelpers.PopulateListBox("TODO 19",
                                          lbxGenres,
                                          myReelflicsDB.GetGenres(),
                                          new List<string> { "GENRE" },
                                          lblErrorMessage,
                                          lblErrorMessage,
                                          queryErrorNoRecordsRetrieved,
                                          EmptyQueryResultMessageType.DBQueryError))
            { result = true; }
            return result;
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PopulateGenresListBox())
                {
                    // Create a DataTable to store the directors.
                    DataTable dtDirectors = new DataTable();
                    dtDirectors.Columns.Add("PERSONID");
                    dtDirectors.Columns.Add("NAME");
                    dtDirectors.Columns.Add("STATUS");
                    ViewState["dtDirectors"] = dtDirectors;

                    // Create a DataTable to store the cast members.
                    DataTable dtCast = new DataTable();
                    dtCast.Columns.Add("PERSONID");
                    dtCast.Columns.Add("NAME");
                    dtCast.Columns.Add("ROLE");
                    dtCast.Columns.Add("STATUS");
                    ViewState["dtCast"] = dtCast;
                }
            }
        }

        protected void BtnAddMovie_Click(object sender, EventArgs e) // Uses TODO 17, TODO 20, TODO 28, TODO 30
        {
            Validate("RoleNames");
            if (IsValid && !isSqlError)
            {
                string movieId = myDBHelpers.GetNextTableId("Movie", "movieId", lblErrorMessage); // Get a new movie id.

                if (!isSqlError)
                {
                    //*********************************************************************
                    // Uses TODO 17 to add a new movie record, TODO 20 to add its genres, *
                    // TODO 28 to add its cast and TODO 30 to add its directors.          *
                    //*********************************************************************
                    if (myDBHelpers.AddMovieInformation(movieId,
                                                         StringExtension.CleanInput(txtTitle.Text),
                                                         StringExtension.CleanInput(txtSynopsis.Text),
                                                         txtReleaseYear.Text,
                                                         txtRunningTime.Text,
                                                         ddlMPAARating.SelectedValue,
                                                         txtIMDBRating.Text,
                                                         rblIsBestPicture.SelectedValue == "Y" ? bestPictureAwardId : "null",
                                                         GetMovieGenres(movieId),
                                                         ViewState["dtDirectors"] as DataTable,
                                                         ViewState["dtCast"] as DataTable,
                                                         lblErrorMessage))
                    {
                        // Save the movie's poster.
                        string posterFilename = postersDirectory
                                                + movieId
                                                + StringExtension.CreateFileName(txtTitle.Text);
                        File.Delete(Server.MapPath(posterFilename)); // Delete the poster if it already exists.
                        File.Copy(Server.MapPath(tempPosterImage), Server.MapPath(posterFilename));

                        // Display the movie's information. 
                        Response.Redirect("~/Shared/MovieInformation.aspx?movieId=" + movieId);
                    }
                    else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.
                }
            }
        }

        protected void BtnCastSearch_Click(object sender, EventArgs e) // Uses TODO 04
        {
            lblCastSearchResultMessage.Visible = ddlCastSearchResult.Visible = false;
            Validate("RoleNames");
            if (IsValid)
            {
                if (!string.IsNullOrEmpty(StringExtension.CleanInput(txtSearchCast.Text)))
                {
                    //**********************************************************************
                    // Uses TODO 04 to populate the cast gridview with movie person names. *
                    //**********************************************************************
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
            lblDirectorSearchResultMessage.Visible = ddlDirectorsSearchResult.Visible = false;
            Validate("RoleNames");
            if (IsValid)
            {
                if (!string.IsNullOrEmpty(StringExtension.CleanInput(txtSearchDirector.Text)))
                {
                    //***************************************************************************
                    // Uses TODO 04 to populate the directors gridview with movie person names. *
                    //***************************************************************************
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

        protected void BtnUploadPoster_Click(object sender, EventArgs e)
        {
            Validate("UploadPoster");
            if (IsValid && fuPoster.HasFile)
            {
                lblUploadMessage.Text = "Poster file: " + fuPoster.FileName.ToString();
                txtUploadMessage.Text = fuPoster.FileName.ToString();
                fuPoster.SaveAs(Server.MapPath(tempPosterImage));
                imgPoster.ImageUrl = tempPosterImage;
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
                Validate("TextBoxMovieGenres");
            }
        }

        protected void CvTextBoxMovieGenres_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string[] textBoxGenres = txtGenres.Text.Split(',');
            for (int i = 0; i < textBoxGenres.Length; i++)
            {
                if (textBoxGenres[i].Length > maxGenreLength)
                {
                    cvTextBoxMovieGenres.Text = "The genre '"
                                                + textBoxGenres[i]
                                                + "' exceeds the maximum length of 15 characters.";
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

        protected void TxtRole_TextChanged(object sender, EventArgs e)
        {
            DataTable dtCast = ViewState["dtCast"] as DataTable;

            // Get the GridView row containing the TextBox that was changed.
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

            // Change the role value in the DataTable.
            TextBox txtRole = (TextBox)sender;
            dtCast.Rows[gvRow.RowIndex]["ROLE"] = txtRole.Text;

            // Change the status to 'modify' in the DataTable only if it is 'exisiting'.
            // If status is 'add' or 'modify', no change to the status is needed.
            if (dtCast.Rows[gvRow.RowIndex]["STATUS"].ToString() == RecordStatus.existing.ToString())
            { dtCast.Rows[gvRow.RowIndex]["STATUS"] = RecordStatus.modify; }

            // Save the modified cast DataTable in ViewState and rebind the GridView.
            ViewState["dtCast"] = dtCast;
            gvCast.DataSource = dtCast;
            gvCast.DataBind();
        }

        protected void TxtGenres_TextChanged(object sender, EventArgs e)
        {
            Validate("TextBoxMovieGenres");
        }
    }
}