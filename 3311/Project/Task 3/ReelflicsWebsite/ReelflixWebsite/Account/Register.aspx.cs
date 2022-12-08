using ReelflicsWebsite.App_Code;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ReelflicsWebsite.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using static ReelflicsWebsite.Global;
using System.Web.UI.WebControls;

namespace ReelflicsWebsite.Account
{
    public partial class Register : Page
    {
        readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        readonly HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/

        private void PopulateYearDropdownList()
        {
            // Populate the Year DropDownList from current year to plus 10 years.
            for (int year = DateTime.Now.Year; year <= DateTime.Now.Year + 10; year++)
            { ddlExpiryYear.Items.Add(year.ToString()); }
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { PopulateYearDropdownList(); }
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && !isSqlError)
            {
                string username = StringExtension.CleanInput(txtUsername.Text);
                // Synchronize users in AspNetUsers and Reelflics databases.
                if (myDBHelpers.SynchLoginAndApplicationDatabases(username, ErrorMessage))
                {
                    if (myDBHelpers.InsertReelflicsMember(username,
                                                         StringExtension.CleanInput(txtPseudonym.Text),
                                                         StringExtension.CleanInput(FirstName.Text),
                                                         StringExtension.CleanInput(LastName.Text),
                                                         StringExtension.CleanInput(txtOccupation.Text),
                                                         StringExtension.CleanInput(txtEmail.Text),
                                                         ddlGender.SelectedValue,
                                                         txtBirthdate.Text,
                                                         txtPhoneNumber.Text,
                                                         ddlEducationLevel.SelectedValue,
                                                         StringExtension.CleanInput(txtCardHolderName.Text),
                                                         StringExtension.CleanInput(txtCardNumber.Text),
                                                         ddlCardType.SelectedValue,
                                                         StringExtension.CleanInput(txtSecurityCode.Text),
                                                         ddlExpiryMonth.SelectedValue,
                                                         ddlExpiryYear.SelectedValue))
                    {
                        var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
                        var user = new ApplicationUser() { UserName = username };
                        IdentityResult result = manager.Create(user, RMSSPassword);
                        if (result.Succeeded)
                        {
                            IdentityResult roleResult = manager.AddToRole(user.Id, ReelflicsRole.ReelflicsMember.ToString());
                            if (roleResult.Succeeded)
                            {
                                signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                            }
                            else
                            { myHelpers.DisplayMessage(lblErrorMessage, "*** ASP.NET error: " + roleResult.Errors.FirstOrDefault() + " Please contact 3311rep."); }
                        }
                        else
                        { myHelpers.DisplayMessage(lblErrorMessage, "*** ASP.NET error: " + result.Errors.FirstOrDefault() + " Please contact 3311rep."); }
                    }
                    else // An SQL error occurred trying to insert a registered user.
                    { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); }
                }
                else // An SQL error occurred synchronizing the AspNetUsers and Reelflics databases.
                {
                    string resultMessage = "*** Error in DBHelperMethods - SynchLoginAndApplicationDatabases: " + ErrorMessage.Text;
                    ErrorMessage.Text = null;
                    myHelpers.DisplayMessage(lblErrorMessage, resultMessage);
                }
            }
        }

        protected void CvBirthdate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string checkedDate = StringExtension.DateIsValid(txtBirthdate.Text);
            if (checkedDate == "") { args.IsValid = false; }
            else { txtBirthdate.Text = checkedDate; }
        }

        protected void CvEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Determine if the email already exists in the database.
            if (!myDBHelpers.IsEmailValid("", txtEmail.Text.Trim()))
            {
                args.IsValid = false;
                if (isSqlError)
                {
                    myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage);
                    cvEmail.Visible = false;
                }
            }
        }

        protected void CvExpiryDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (ddlExpiryMonth.SelectedIndex != 0)
            {
                short month = Convert.ToInt16(ddlExpiryMonth.SelectedValue.Trim());
                short year = Convert.ToInt16(ddlExpiryYear.SelectedValue.Trim());
                if ((month < DateTime.Now.Month) && (year <= DateTime.Now.Year))
                { args.IsValid = false; }
            }
        }

        protected void CvPseudonym_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Determine if the pseudonym already exists in the database.
            if (!myDBHelpers.IsPseudonymValid(StringExtension.CleanInput(txtPseudonym.Text)))
            {
                args.IsValid = false;
                if (isSqlError)
                {
                    myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage);
                    cvPseudonym.Visible = false;
                }
            }
        }

        protected void CvSecurityCode_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (revSecurityCode.IsValid && ddlCardType.SelectedValue != "none selected")
            {
                if (ddlCardType.SelectedValue == "American Express")
                {
                    if (txtSecurityCode.Text.Trim().Length != 4)
                    {
                        cvSecurityCode.ErrorMessage = "Enter exactly 4 digits only.";
                        args.IsValid = false;
                    }
                }
                else
                {
                    if (txtSecurityCode.Text.Trim().Length != 3)
                    {
                        cvSecurityCode.ErrorMessage = "Enter exactly 3 digits obly.";
                        args.IsValid = false;
                    }
                }
            }
        }

        protected void CvUsername_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Determine if the username already exists in the database.
            if (!myDBHelpers.IsUsernameValid(StringExtension.CleanInput(txtUsername.Text)))
            {
                args.IsValid = false;
                if (isSqlError)
                {
                    myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage);
                    cvUsername.Visible = false;
                }
            }
        }

    }
}