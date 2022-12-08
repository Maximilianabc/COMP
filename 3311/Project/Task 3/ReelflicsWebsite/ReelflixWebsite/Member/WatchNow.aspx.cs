using ReelflicsWebsite.App_Code;
using System;
using System.Web;
using System.Web.UI;
using static ReelflicsWebsite.Global;


namespace ReelflicsWebsite.Member
{
    public partial class WatchNow : Page
    {
        //***************
        // Uses TODO 36 *
        //***************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly string username = HttpContext.Current.User.Identity.Name;

        protected void Page_Load(object sender, EventArgs e) // Uses TODO 36
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["movieId"]))
                {
                    //******************************************************************
                    // Uses TODO 36 to add a movie to the viewing history of a member. *
                    //******************************************************************
                    if (myReelflicsDB.AddRecordToMemberWatchHistory(Request.QueryString["movieId"],
                                                                    username,
                                                                    DateTime.Now.ToString("dd-MMM-yyyy HH:mm")))
                    { pnlEnjoyMovie.Visible = true; }
                    else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.
                }
                else { pnlEnjoyMovie.Visible = true; ; }
            }
        }
    }
}