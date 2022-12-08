using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Employee
{
    public partial class MemberInformationReport : System.Web.UI.Page
    {
        //***************
        // Uses TODO 33 *
        //***************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate a dropdown list with members' usernames and names.
                if (myHelpers.PopulateDropDownList("DBHelperMethods - GetReelflicsMembers",
                                                   ddlMemberName,
                                                   myDBHelpers.GetReelflicsMembers(),
                                                   new List<string> { "USERNAME", "NAME" },
                                                   lblErrorMessage,
                                                   lblErrorMessage,
                                                   dbqueryErrorNoRecordsRetrieved,
                                                   EmptyQueryResultMessageType.DBQueryError))
                { pnlMemberName.Visible = true; }
                else { if (isSqlError) { lblErrorMessage.Text += contact3311rep; } }
            }
        }

        protected void DdlMemberName_SelectedIndexChanged(object sender, EventArgs e) // Uses TODO 33
        {
            if (IsValid && !isSqlError)
            {
                //*************************************************
                // Uses TODO 33 to get a member's account record. *
                //*************************************************
                DataTable dtMemberAccountRecord = myReelflicsDB.GetMemberAccountRecord(ddlMemberName.SelectedValue);

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
                        // Assign values to their controls.
                        litUsername.Text = HttpUtility.HtmlDecode(dtMemberAccountRecord.Rows[0]["USERNAME"].ToString());
                        litPseudonym.Text = HttpUtility.HtmlDecode(dtMemberAccountRecord.Rows[0]["PSEUDONYM"].ToString());
                        litFirstName.Text = HttpUtility.HtmlDecode(dtMemberAccountRecord.Rows[0]["FIRSTNAME"].ToString());
                        litLastName.Text = HttpUtility.HtmlDecode(dtMemberAccountRecord.Rows[0]["LASTNAME"].ToString());
                        litOccupation.Text = dtMemberAccountRecord.Rows[0]["OCCUPATION"].ToString();
                        litEmail.Text = dtMemberAccountRecord.Rows[0]["EMAIL"].ToString();
                        litGender.Text = dtMemberAccountRecord.Rows[0]["GENDER"].ToString();
                        litBirthdate.Text = ((DateTime)dtMemberAccountRecord.Rows[0]["BIRTHDATE"]).ToString("dd-MMM-yyyy");
                        litPhoneNumber.Text = dtMemberAccountRecord.Rows[0]["PHONENUMBER"].ToString();
                        litEducationLevel.Text = dtMemberAccountRecord.Rows[0]["EDUCATIONLEVEL"].ToString();
                        litCardHolderName.Text = HttpUtility.HtmlDecode(dtMemberAccountRecord.Rows[0]["CARDHOLDERNAME"].ToString());
                        litCardNumber.Text = dtMemberAccountRecord.Rows[0]["CARDNUMBER"].ToString();
                        litCardType.Text = dtMemberAccountRecord.Rows[0]["CARDTYPE"].ToString();
                        litSecurityCode.Text = dtMemberAccountRecord.Rows[0]["SECURITYCODE"].ToString();
                        litExpiryMonth.Text = dtMemberAccountRecord.Rows[0]["EXPIRYMONTH"].ToString();
                        litExpiryYear.Text = dtMemberAccountRecord.Rows[0]["EXPIRYYEAR"].ToString();
                        pnlMemberAccountInformation.Visible = true;
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
            else { pnlMemberAccountInformation.Visible = false; }
        }
    }
}