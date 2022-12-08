using ReelflicsWebsite.App_Code;
using System;
using System.Web;
using System.Web.UI;
using static ReelflicsWebsite.Global;


namespace ReelflicsWebsite.Member
{
    public partial class CreateReview : Page
    {
        //***************
        // Uses TODO 38 *
        //***************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly string username = HttpContext.Current.User.Identity.Name;

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["movieId"]))
                {
                    if (Session["movieTitle"] is string movieTitle)
                    {
                        litHeading.Text += movieTitle + "'</h4>";
                        ddlRating.SelectedIndex = 0;
                        pnlReview.Visible = litHeading.Visible = true;
                    }
                    else
                    {
                        myHelpers.DisplayMessage(lblErrorMessage, nullSessionStateErrorMessage
                                                                  + "'movieTitle' in the Page_Load method of CreateReview.aspx.cs."
                                                                  + contact3311rep);
                    }
                }
                else
                {
                    myHelpers.DisplayMessage(lblErrorMessage, querystringIsNullOrEmpty
                                                              + "CreateReview.aspx.cs."
                                                              + contact3311rep);
                }
                Session["searchType"] = SearchType.All;
            }
        }

        protected void BtnCreateReview_Click(object sender, EventArgs e) // TODO 38
        {
            if (Page.IsValid && !isSqlError)
            {
                //********************************************
                // Uses TODO 38 to add a review for a movie. *
                //********************************************
                if (myReelflicsDB.CreateMemberMovieReviewRecord(Request.QueryString["movieId"],
                                                               username,
                                                               StringExtension.CleanInput(txtTitle.Text),
                                                               ddlRating.SelectedValue.ToString(),
                                                               StringExtension.CleanInput(txtReviewText.Text),
                                                               DateTime.Now.ToString("dd-MMM-yyyy")))
                { Response.Redirect("~/Shared/MovieInformation.aspx?movieId=" + Request.QueryString["movieId"]); }
                else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.
            }
        }
    }
}