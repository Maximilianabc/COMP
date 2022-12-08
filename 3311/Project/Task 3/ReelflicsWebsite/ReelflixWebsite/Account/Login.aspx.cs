using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.UI;
using ReelflicsWebsite.App_Code;
using static ReelflicsWebsite.Global;
using System.Web.Security;

namespace ReelflicsWebsite.Account
{
    public partial class Login : Page
    {
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!string.IsNullOrEmpty(returnUrl))
            { RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl; }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Synchronize users in AspNetUsers and Fanclub databases.
                if (myDBHelpers.SynchLoginAndApplicationDatabases(UserName.Text, FailureText))
                {
                    // Validate the user password.
                    var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                    // This doesn't count login failures towards account lockout.
                    // To enable password failures to trigger lockout, change to shouldLockout: true.
                    var result = signinManager.PasswordSignIn(UserName.Text, RMSSPassword, false, shouldLockout: false);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            isSqlError = false;
                            sqlErrorMessage = "";
                            IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                            break;
                        case SignInStatus.LockedOut:
                            Response.Redirect("/Account/Lockout");
                            break;
                        case SignInStatus.RequiresVerification:
                            Response.Redirect(string.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                            Request.QueryString["ReturnUrl"], false), true);
                            break;
                        case SignInStatus.Failure:
                        default:
                            FailureText.Text = "Invalid username.";
                            ErrorMessage.Visible = true;
                            break;
                    }
                }
                else { ErrorMessage.Visible = true; }
            }
        }
    }
}