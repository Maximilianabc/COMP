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
    public partial class ModifyCastDirector : Page
    {
        //*********************************
        // Uses TODO 04, TODO 22, TODO 26 *
        //*********************************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/

        private bool GetMoviePersonRecord(string personId) // Uses TODO 22
        {
            bool result = false;
            //*********************************************************************
            // Uses TODO 22 to retrieve the record of a cast member or director. *
            //*********************************************************************
            DataTable dtMoviePerson = myReelflicsDB.GetMoviePersonRecord(personId);

            // Show the cast member/director information if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 22",
                                             dtMoviePerson,
                                             new List<string> { "PERSONID", "NAME", "BIOGRAPHY", "GENDER", "BIRTHDATE", "DEATHDATE" },
                                             lblErrorMessage))
            {
                if (dtMoviePerson.Rows.Count == 1) // Only one record should be retrieved.
                {
                    // Create the cast member's/director's image photo filename from his/her person id and name.
                    imgPhoto.ImageUrl = peopleDirectory + personId + StringExtension.CreateFileName(dtMoviePerson.Rows[0]["NAME"].ToString());

                    // Save the values in ViewState for checking if any value was changed.
                    ViewState["oldName"] = txtName.Text = HttpUtility.HtmlDecode(dtMoviePerson.Rows[0]["NAME"].ToString());
                    ViewState["oldBiography"] = txtBiography.Text = HttpUtility.HtmlDecode(dtMoviePerson.Rows[0]["BIOGRAPHY"].ToString());
                    ViewState["oldGender"] = ddlGender.SelectedValue = dtMoviePerson.Rows[0]["GENDER"].ToString();

                    // Set the birthdate, if known.
                    if (!string.IsNullOrEmpty(dtMoviePerson.Rows[0]["BIRTHDATE"].ToString()))
                    {
                        txtBirthdate.Text = ((DateTime)dtMoviePerson.Rows[0]["BIRTHDATE"]).ToString("dd-MMM-yyyy");
                        ViewState["oldBirthdate"] = ((DateTime)dtMoviePerson.Rows[0]["BIRTHDATE"]).ToString("dd-MMM-yyyy");
                    }
                    else { ViewState["oldBirthdate"] = dtMoviePerson.Rows[0]["BIRTHDATE"]; } // Birthdate is not known.

                    // Set the deathdate, if any.
                    ViewState["oldDeathdate"] = dtMoviePerson.Rows[0]["DEATHDATE"];
                    if (!string.IsNullOrEmpty(dtMoviePerson.Rows[0]["DEATHDATE"].ToString()))
                    {
                        txtDeathdate.Text = ((DateTime)dtMoviePerson.Rows[0]["DEATHDATE"]).ToString("dd-MMM-yyyy");
                        ViewState["oldDeathdate"] = ((DateTime)dtMoviePerson.Rows[0]["DEATHDATE"]).ToString("dd-MMM-yyyy");
                    }
                    else { ViewState["oldDeathdate"] = dtMoviePerson.Rows[0]["DEATHDATE"]; } // No death date or not known.
                    result = true;
                }
                else if (dtMoviePerson.Rows.Count == 0) // No record was retrieved.
                { myHelpers.DisplayMessage(lblErrorMessage, dbqueryError + "TODO 22" + dbqueryErrorNoRecordsRetrieved); }
                else // Multiple records were retrieved.
                { myHelpers.DisplayMessage(lblErrorMessage, queryError + "TODO 22" + queryErrorMultipleRecordsRetrieved); }
            }
            return result;
        }

        private bool IsMoviePersonRecordChanged(string newName, string newBiography, string newGender,
            string newBirthdate, string newDeathdate)
        {
            if (ViewState["oldName"].ToString() == newName
                && ViewState["oldBiography"].ToString() == newBiography
                && ViewState["oldGender"].ToString() == newGender
                && ViewState["oldBirthdate"].ToString() == newBirthdate
                && ViewState["oldDeathdate"].ToString() == newDeathdate)
            { return false; } // The movie person record was not modified.
            else
            { return true; } // The movie person record was modified.
        }

        private bool IsPhotoChanged(string personId, string name)
        {
            bool result = false;
            string newFilename = peopleDirectory + personId + StringExtension.CreateFileName(name);
            string oldFilename = peopleDirectory + personId + StringExtension.CreateFileName(ViewState["oldName"].ToString());

            // Replace the old photo with the new one if the photo has changed.
            if ((bool)ViewState["hasNewPhoto"])
            {
                File.Delete(Server.MapPath(oldFilename));
                File.Copy(Server.MapPath(tempPeoplePhoto), Server.MapPath(newFilename));
                result = true;
            }
            // Change the photo's file name if the movie person's name has changed.
            else if (newFilename != oldFilename)
            {
                File.Delete(Server.MapPath(newFilename));
                File.Copy(Server.MapPath(oldFilename), Server.MapPath(newFilename));
                File.Delete(Server.MapPath(oldFilename));
                result = true;
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Show the movie person record if there is a person id in the query string.
                if (!string.IsNullOrEmpty(Request.QueryString["personId"]))
                {
                    if (GetMoviePersonRecord(Request.QueryString["personId"]))
                    {
                        pnlNameSearch.Visible = false;
                        pnlMoviePersonRecord.Visible = true;
                        ViewState["hasNewPhoto"] = false;
                    }
                }
                else // Hide the movie person record if there is no person id in the query string.
                { pnlMoviePersonRecord.Visible = false; }
            }
        }

        protected void BtnUploadPhoto_Click(object sender, EventArgs e)
        {
            Validate("UploadPhoto");
            if (IsValid && fuPhoto.HasFile)
            {
                lblUploadMessage.Text = "Photo file: " + fuPhoto.FileName.ToString();
                fuPhoto.SaveAs(Server.MapPath(tempPeoplePhoto));
                imgPhoto.ImageUrl = tempPeoplePhoto;
                ViewState["hasNewPhoto"] = true;
            }
        }

        protected void BtnSearchName_Click(object sender, EventArgs e) // Uses TODO 04
        {
            lblSearchResultMessage.Visible = false;
            if (!string.IsNullOrEmpty(StringExtension.CleanInput(txtSearchName.Text)))
            {
                //***************************************************************
                // Uses TODO 04 to populate a gridView with movie person names. *
                //***************************************************************
                if (myHelpers.PopulateGridView("TODO 04",
                                               gvSearchResult,
                                               myReelflicsDB.GetMoviePersonSearchResult(StringExtension.CleanInput(txtSearchName.Text)),
                                               new List<string> { "PERSONID", "NAME" },
                                               lblErrorMessage,
                                               lblSearchResultMessage,
                                               noMoviePersonMatches))
                {
                    if (!isEmptyQueryResult)
                    {
                        lblSearchResultMessage.Visible = false;
                        pnlSearchResult.Visible = true;
                    }
                    else { lblSearchResultMessage.Visible = true; }
                }
            }
        }

        protected void BtnModifyCastDirector_Click(object sender, EventArgs e) // Uses TODO 26
        {
            if (Page.IsValid && !isSqlError)
            {
                bool isRecordChanged = false;
                lblModifyCastDirectorMessage.Visible = false;

                // Determine if the movie person's record has changed.
                if (IsMoviePersonRecordChanged(txtName.Text.Trim(),
                                               txtBiography.Text.Trim(),
                                               ddlGender.SelectedValue,
                                               txtBirthdate.Text,
                                               txtDeathdate.Text))
                {
                    isRecordChanged = true;
                    //*************************************************
                    // Uses TODO 26 to update a movie person record. *
                    //*************************************************
                    if (!myReelflicsDB.ModifyMoviePersonRecord(Request.QueryString["personId"],
                                                             StringExtension.CleanInput(txtName.Text),
                                                             StringExtension.CleanInput(txtBiography.Text),
                                                             ddlGender.SelectedValue,
                                                             txtBirthdate.Text,
                                                             txtDeathdate.Text))
                    { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); return; } // An SQL error occurred.
                }

                // Redirect if the photo or movie person record have changed.
                if (IsPhotoChanged(Request.QueryString["personId"], StringExtension.CleanInput(txtName.Text)) || isRecordChanged)
                { Response.Redirect("~/Shared/MoviePersonInformation.aspx?personId=" + Request.QueryString["personId"]); }
                else  // The poaster and movie person record were not changed.
                { myHelpers.DisplayMessage(lblModifyCastDirectorMessage, informationNotChanged); }
            }
        }

        protected void CvBirthdate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtBirthdate.Text.Trim() != "")
            {
                string checkedDate = StringExtension.DateIsValid(txtBirthdate.Text);
                if (checkedDate == "") { args.IsValid = false; }
                else { txtBirthdate.Text = checkedDate; }
            }
        }

        protected void CvDeathdate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (txtDeathdate.Text.Trim() != "")
            {
                string checkedDate = StringExtension.DateIsValid(txtDeathdate.Text);
                if (checkedDate == "") { args.IsValid = false; }
                else { txtDeathdate.Text = checkedDate; }
            }
        }

        protected void GvSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
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

                        // Change the name to a hyperlink.
                        var titleCell = e.Row.Cells[nameColumn];
                        titleCell.Controls.Clear();
                        titleCell.Controls.Add(new HyperLink
                        {
                            NavigateUrl = "~/Employee/ModifyCastDirector.aspx?personId="
                                          + e.Row.Cells[personIdColumn].Text,
                            Text = titleCell.Text
                        });
                    }
                }
            }
        }
    }
}