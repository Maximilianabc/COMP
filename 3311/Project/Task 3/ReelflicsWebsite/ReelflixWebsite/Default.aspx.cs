using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using ReelflicsWebsite.App_Code;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite
{
    public partial class Default : Page
    {
        //***************
        // Uses TODO 01 *
        //***************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();

        /***** Private Methods *****/

        private bool PopulateMostWatchedMovies() // Uses TODO 01
        {
            bool result = false;
            //***********************************************
            // Uses TODO 01 to get the most watched movies. *
            //***********************************************
            DataTable dtMostWatchedMovies = myReelflicsDB.GetMostWatchedMovies();

            // Show the most watched movies if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 01",
                                             dtMostWatchedMovies,
                                             new List<string> { "MOVIEID", "TITLE", "IMDBRATING" },
                                             lblErrorMessage))
            {
                if (dtMostWatchedMovies.Rows.Count != 0)
                {
                    if (myDBHelpers.PopulateMovieDisplay(dtMostWatchedMovies, phMostWatchedMovies, lblErrorMessage))
                    { result = true; }
                }
                else { pnlSplashscreen.Visible = true; }
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.IsInRole(ReelflicsRole.ReelflicsMember.ToString()))
                { Response.Redirect("~/Member/RecommendedMovies.aspx"); }
                else if (User.IsInRole(ReelflicsRole.Employee.ToString()))
                { pnlSplashscreen.Visible = true; }
                else
                {
                    if (PopulateMostWatchedMovies()) { pnlMostWatchedMovies.Visible = true; }
                }
            }
            if (isSqlError) { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); }
        }
    }
}