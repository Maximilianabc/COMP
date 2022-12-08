using ReelflicsWebsite.App_Code;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Employee
{
    public partial class AddCastDirector : Page
    {
        //***************
        // Uses TODO 25 *
        //***************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnAddCastDirector_Click(object sender, EventArgs e) // Uses TODO 25
        {
            if (Page.IsValid && !isSqlError)
            {
                string personId = myDBHelpers.GetNextTableId("MoviePerson", "personId", lblErrorMessage); // Get a new movie person id.

                if (!isSqlError)
                {
                    //**************************************************
                    // Uses TODO 25 to add a new movie person record. *
                    //**************************************************
                    if (myReelflicsDB.AddMoviePersonRecord(personId,
                                                          StringExtension.CleanInput(txtName.Text),
                                                          StringExtension.CleanInput(txtBiography.Text),
                                                          ddlGender.SelectedValue,
                                                          StringExtension.CleanInput(txtBirthdate.Text),
                                                          StringExtension.CleanInput(txtDeathdate.Text)))
                    {
                        // Save the movie person's photo.
                        string photoFilename = peopleDirectory
                                               + personId
                                               + StringExtension.CreateFileName(txtName.Text);
                        File.Delete(Server.MapPath(photoFilename)); // Delete the photo if it already exists.
                        File.Copy(Server.MapPath(tempPeoplePhoto), Server.MapPath(photoFilename));

                        // Display the movie person's information.
                        Response.Redirect("~/Shared/MoviePersonInformation.aspx?personId=" + personId);
                    }
                    else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.
                    pnlAddMoviePerson.Visible = false;
                }
            }
        }

        protected void BtnUploadPhoto_Click(object sender, EventArgs e)
        {
            Validate("UploadPhoto");
            if (IsValid && fuPhoto.HasFile)
            {
                lblUploadMessage.Text = "Photo file: " + fuPhoto.FileName.ToString();
                txtUploadMessage.Text = fuPhoto.FileName.ToString();
                fuPhoto.SaveAs(Server.MapPath(tempPeoplePhoto));
                imgPhoto.ImageUrl = tempPeoplePhoto;
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
    }
}