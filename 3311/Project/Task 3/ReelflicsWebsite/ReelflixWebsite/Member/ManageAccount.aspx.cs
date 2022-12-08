using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Member
{
    public partial class ManageAccount : Page
    {
        //************************
        // Uses TODO 33, TODO 34 *
        //************************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        readonly HelperMethods myHelpers = new HelperMethods();
        private readonly string username = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private bool IsMemberAccountRecordChanged(string newFirstName, string newLastName, string newoccupation,
                                                  string newEmail, string newGender, string newBirthdate,
                                                  string newPhoneNumber, string newEducationLevel,
                                                  string newCardholderName, string newCardNumber, string newCardType,
                                                  string newSecurityCode, string newExpiryMonth, string newExpiryYear)
        {
            if (ViewState["oldFirstName"].ToString() == newFirstName
                && ViewState["oldLastName"].ToString() == newLastName
                && ViewState["oldOccupation"].ToString() == newoccupation
                && ViewState["oldEmail"].ToString() == newEmail
                && ViewState["oldGender"].ToString() == newGender
                && ViewState["oldBirthdate"].ToString() == newBirthdate
                && ViewState["oldPhoneNumber"].ToString() == newPhoneNumber
                && ViewState["oldEducationLevel"].ToString() == newEducationLevel
                && ViewState["oldCardholderName"].ToString() == newCardholderName
                && ViewState["oldCardNumber"].ToString() == newCardNumber
                && ViewState["oldCardType"].ToString() == newCardType
                && ViewState["oldSecurityCode"].ToString() == newSecurityCode
                && ViewState["oldExpiryMonth"].ToString() == newExpiryMonth
                && ViewState["oldExpiryYear"].ToString() == newExpiryYear)
            { return false; } // The member account record was not modified.
            else
            { return true; } // The member account record was modified.
        }

        private void PopulateAccountInformation() // Uses TODO 33
        {
            //*************************************************
            // Uses TODO 33 to get a member's account record. *
            //*************************************************
            DataTable dtMemberAccountRecord = myReelflicsDB.GetMemberAccountRecord(username);

            // Show the member's account information if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 33",
                                             dtMemberAccountRecord,
                                             new List<string> { "USERNAME", "PSEUDONYM", "FIRSTNAME", "LASTNAME", "OCCUPATION",
                                                 "EMAIL", "GENDER", "BIRTHDATE", "PHONENUMBER", "EDUCATIONLEVEL", "CARDHOLDERNAME",
                                                 "CARDNUMBER", "CARDTYPE", "SECURITYCODE", "EXPIRYMONTH", "EXPIRYYEAR" },
                                             lblErrorMessage))
            {
                if (dtMemberAccountRecord.Rows.Count == 1) // Only one record should be retrieved.
                {
                    // Assign values to their controls and save modifiable values in ViewState for checking if anything was changed.
                    litUsername.Text = HttpUtility.HtmlDecode(dtMemberAccountRecord.Rows[0]["USERNAME"].ToString());
                    litPseudonym.Text = HttpUtility.HtmlDecode(dtMemberAccountRecord.Rows[0]["PSEUDONYM"].ToString());
                    ViewState["oldFirstName"] = txtFirstName.Text = HttpUtility.HtmlDecode(dtMemberAccountRecord.Rows[0]["FIRSTNAME"].ToString());
                    ViewState["oldLastName"] = txtLastName.Text = HttpUtility.HtmlDecode(dtMemberAccountRecord.Rows[0]["LASTNAME"].ToString());
                    ViewState["oldOccupation"] = txtOccupation.Text = dtMemberAccountRecord.Rows[0]["OCCUPATION"].ToString();
                    ViewState["oldEmail"] = txtEmail.Text = dtMemberAccountRecord.Rows[0]["EMAIL"].ToString();
                    ViewState["oldGender"] = ddlGender.SelectedValue = dtMemberAccountRecord.Rows[0]["GENDER"].ToString();
                    ViewState["oldBirthdate"] = txtBirthdate.Text = ((DateTime)dtMemberAccountRecord.Rows[0]["BIRTHDATE"]).ToString("dd-MMM-yyyy");
                    ViewState["oldPhoneNumber"] = txtPhoneNumber.Text = dtMemberAccountRecord.Rows[0]["PHONENUMBER"].ToString();
                    ViewState["oldEducationLevel"] = ddlEducationLevel.SelectedValue = dtMemberAccountRecord.Rows[0]["EDUCATIONLEVEL"].ToString();
                    ViewState["oldCardholderName"] = txtCardHolderName.Text = HttpUtility.HtmlDecode(dtMemberAccountRecord.Rows[0]["CARDHOLDERNAME"].ToString());
                    ViewState["oldCardNumber"] = txtCardNumber.Text = dtMemberAccountRecord.Rows[0]["CARDNUMBER"].ToString();
                    ViewState["oldCardType"] = ddlCardType.SelectedValue = dtMemberAccountRecord.Rows[0]["CARDTYPE"].ToString();
                    ViewState["oldSecurityCode"] = txtSecurityCode.Text = dtMemberAccountRecord.Rows[0]["SECURITYCODE"].ToString();
                    ViewState["oldExpiryMonth"] = ddlExpiryMonth.SelectedValue = dtMemberAccountRecord.Rows[0]["EXPIRYMONTH"].ToString();
                    ViewState["oldExpiryYear"] = ddlExpiryYear.SelectedValue = dtMemberAccountRecord.Rows[0]["EXPIRYYEAR"].ToString();
                }
                else
                {
                    if (dtMemberAccountRecord.Rows.Count > 1) // Multiple records were retrieved.
                    { myHelpers.DisplayMessage(lblErrorMessage, queryError + "TODO 33" + queryErrorMultipleRecordsRetrieved); }
                    else // No record was retrieved.
                    { myHelpers.DisplayMessage(lblErrorMessage, dbqueryError + "TODO 33" + dbqueryErrorNoRecordsRetrieved); }
                }
            }
        }

        private void PopulateYearDropdownList()
        {
            // Populate the Year DropDownList from current year to plus 10 years.
            for (int year = DateTime.Now.Year; year <= DateTime.Now.Year + 10; year++)
            { ddlExpiryYear.Items.Add(year.ToString()); }
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateYearDropdownList();
                PopulateAccountInformation();
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e) // Uses TODO 34
        {
            Validate();
            if (IsValid && !isSqlError)
            {
                lblUpdateMessage.Visible = false;

                // Determine if the member's account record has changed.
                if (IsMemberAccountRecordChanged(txtFirstName.Text.Trim(), txtLastName.Text.Trim(),
                                                 txtOccupation.Text.Trim(), txtEmail.Text.Trim(),
                                                 ddlGender.SelectedValue, txtBirthdate.Text, txtPhoneNumber.Text,
                                                 ddlEducationLevel.SelectedValue, txtCardHolderName.Text.Trim(),
                                                 txtCardNumber.Text.Trim(), ddlCardType.SelectedValue,
                                                 txtSecurityCode.Text.Trim(), ddlExpiryMonth.SelectedValue,
                                                 ddlExpiryYear.SelectedValue))
                {
                    //****************************************************
                    // Uses TODO 34 to update a member's account record. *
                    // ***************************************************
                    if (myReelflicsDB.UpdateMemberAccountRecord(username, StringExtension.CleanInput(txtFirstName.Text),
                                                                StringExtension.CleanInput(txtLastName.Text),
                                                                StringExtension.CleanInput(txtOccupation.Text),
                                                                txtEmail.Text.Trim(), ddlGender.SelectedValue,
                                                                txtBirthdate.Text, txtPhoneNumber.Text, ddlEducationLevel.SelectedValue,
                                                                StringExtension.CleanInput(txtCardHolderName.Text),
                                                                StringExtension.CleanInput(txtCardNumber.Text),
                                                                ddlCardType.SelectedValue, txtSecurityCode.Text.Trim(),
                                                                ddlExpiryMonth.SelectedValue,
                                                                ddlExpiryYear.SelectedValue))
                    {
                        myHelpers.DisplayMessage(lblUpdateSuccessMessage, informationUpdated);
                        pnlMemberAccountInformation.Visible = false;
                    }
                    else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.
                }
                else { myHelpers.DisplayMessage(lblUpdateMessage, informationNotChanged); } // No information was changed.
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
            args.IsValid = false;
            if (ViewState["oldEmail"] != null)
            {
                if (myDBHelpers.IsEmailValid(ViewState["oldEmail"].ToString(), txtEmail.Text.Trim()))
                { args.IsValid = true; }
                else
                {
                    if (isSqlError)
                    {
                        myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage);
                        cvEmail.Visible = false;
                    }
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
                        cvSecurityCode.ErrorMessage = "Enter exactly 3 digits only.";
                        args.IsValid = false;
                    }
                }
            }
        }
    }
}