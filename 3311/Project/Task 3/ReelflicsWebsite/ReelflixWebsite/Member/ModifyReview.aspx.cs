using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.Member
{
    public partial class ModifyReview : Page
    {
        //************************
        // Uses TODO 37, TODO 39 *
        //************************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly string username = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private bool GetReviewRecord(string movieId) //Uses TODO 37
        {
            bool result = false;
            //***********************************************
            // Uses TODO 37 to get a member's movie revieW. *
            //***********************************************
            DataTable dtReview = myReelflicsDB.GetMemberMovieReviewRecord(movieId, username);

            if (myHelpers.IsQueryResultValid("TODO 37",
                                             dtReview,
                                             new List<string> { "TITLE", "RATING", "REVIEWTEXT" },
                                             lblErrorMessage))
            {
                if (dtReview.Rows.Count == 1) // Only one record should be retrieved.
                {
                    // Save the original review values in ViewState for determining whether the review was changed.
                    ViewState["oldTitle"] = txtTitle.Text = dtReview.Rows[0]["TITLE"].ToString();
                    ViewState["oldRating"] = ddlRating.SelectedValue = dtReview.Rows[0]["RATING"].ToString();
                    ViewState["oldReviewText"] = txtReviewText.Text = dtReview.Rows[0]["REVIEWTEXT"].ToString();
                    result = true;
                }
                else
                {
                    if (dtReview.Rows.Count > 1) // Multiple records were retrieved.
                    {
                        myHelpers.DisplayMessage(lblErrorMessage, queryError
                                                                  + "TODO 37"
                                                                  + queryErrorMultipleRecordsRetrieved);
                    }
                    else // No record was retrieved.
                    {
                        myHelpers.DisplayMessage(lblErrorMessage, dbqueryError
                                                                  + "TODO 37"
                                                                  + dbqueryErrorNoRecordsRetrieved);
                    }
                }
            }
            return result;
        }

        private bool IsReviewChanged(string newTitle, string newRating, string newReviewText)
        {
            if (ViewState["oldTitle"].ToString() == newTitle
                && ViewState["oldRating"].ToString() == newRating
                && ViewState["oldReviewText"].ToString() == newReviewText)
            { return false; } // The review was not modified.
            else
            { return true; } // The review was modified.
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["movieId"]))
                {
                    if (Session["movieTitle"] is string movieTitle)
                    {
                        litMovieTitle.Text += movieTitle + "'</h4>";
                        if (GetReviewRecord(Request.QueryString["movieId"]))
                        { pnlReview.Visible = litMovieTitle.Visible = true; }
                    }
                    else
                    {
                        myHelpers.DisplayMessage(lblErrorMessage, nullSessionStateErrorMessage
                                                                  + "'movieTitle' in Page_load of ModifyReview.aspx.cs."
                                                                  + contact3311rep);
                    }
                }
                else
                {
                    myHelpers.DisplayMessage(lblErrorMessage, querystringIsNullOrEmpty
                                                              + "ModifyReview.aspx.cs"
                                                              + contact3311rep);
                }
                Session["searchType"] = SearchType.All;
            }
        }

        protected void BtnModifyReview_Click(object sender, EventArgs e) // Uses TODO 39
        {
            if (Page.IsValid && !isSqlError)
            {
                if (IsReviewChanged(txtTitle.Text.Trim(), ddlRating.SelectedValue, txtReviewText.Text.Trim()))
                {
                    //*******************************************
                    // Uses TODO 39 to change a movie's review. *
                    //*******************************************
                    if (myReelflicsDB.ModifyMemberMovieReviewRecord(Request.QueryString["movieId"],
                                                                   username,
                                                                   StringExtension.CleanInput(txtTitle.Text),
                                                                   ddlRating.SelectedValue,
                                                                   StringExtension.CleanInput(txtReviewText.Text)))
                    { Response.Redirect("~/Shared/MovieInformation.aspx?movieId=" + Request.QueryString["movieId"]); }
                    else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.
                }
                else { myHelpers.DisplayMessage(lblModifyMessage, reviewNotChanged); } // The review was not changed.
            }
        }
    }
}