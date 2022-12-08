using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += Master_Page_PreLoad;
        }

        protected void Master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ReelflicsRole role = ReelflicsRole.None;

            // Hide member menu items.
            txtSearchReelflics.Visible = false;
            btnSearch.Visible = false;
            liWatchHistory.Visible = false;
            liWatchlist.Visible = false;
            liManageAccount.Visible = false;

            // Hide employee menu items.
            liEmployeeMovieDropDown.Visible = false;
            liAddMovie.Visible = false;
            liModifyMovie.Visible = false;
            liEmployeeMoviePersonDropDown.Visible = false;
            liAddCastDirector.Visible = false;
            liModifyCastDirector.Visible = false;
            liAddCastDirectorAward.Visible = false;
            liEmployeeReportsDropDown.Visible = false;
            liMemberInformationReport.Visible = false;
            liMemberActivityReport.Visible = false;
            liMembershipStatisticsReport.Visible = false;

            string userId = HttpContext.Current.User.Identity.GetUserId();
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if (userId != null)
            {
                if (manager.IsInRole(userId, ReelflicsRole.ReelflicsMember.ToString())) { role = ReelflicsRole.ReelflicsMember; }
                else if (manager.IsInRole(userId, ReelflicsRole.Employee.ToString())) { role = ReelflicsRole.Employee; }
            }

            {
                switch (role)
                {
                    case ReelflicsRole.Employee:
                        // Show employee movie menu items.
                        liEmployeeMovieDropDown.Visible = true;
                        liAddMovie.Visible = true;
                        liModifyMovie.Visible = true;
                        // Show employee movie person menu items.
                        liEmployeeMoviePersonDropDown.Visible = true;
                        liAddCastDirector.Visible = true;
                        liModifyCastDirector.Visible = true;
                        liAddCastDirectorAward.Visible = true;
                        // Show report menu items.
                        liEmployeeReportsDropDown.Visible = true;
                        liMemberInformationReport.Visible = true;
                        liMemberActivityReport.Visible = true;
                        liMembershipStatisticsReport.Visible = true;
                        // Hide navbar search box.
                        txtSearchReelflics.Visible = false;
                        btnSearch.Visible = false;
                        break;
                    case ReelflicsRole.ReelflicsMember:
                        // Show member menu items.
                        txtSearchReelflics.Visible = true;
                        btnSearch.Visible = true;
                        liWatchHistory.Visible = true;
                        liWatchlist.Visible = true;
                        liManageAccount.Visible = true;
                        break;
                    case ReelflicsRole.None:
                        break;
                }
            }
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            var searchText = Server.UrlEncode(txtSearchReelflics.Text); // URL encode in case of special characters
            Response.Redirect("~/Member/MemberSearchResult.aspx?queryString=" + searchText);
        }
    }
}