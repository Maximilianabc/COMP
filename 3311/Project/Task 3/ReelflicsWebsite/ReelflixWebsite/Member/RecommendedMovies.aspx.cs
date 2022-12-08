using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;

namespace ReelflicsWebsite.Member
{
    public partial class RecommendedMovies : Page
    {
        //***************
        // Uses TODO 02 *
        //***************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly string username = HttpContext.Current.User.Identity.Name;

        /***** Private Methods *****/

        private bool PopulateRecommendedMovies() // Uses TODO 02
        {
            bool result = false;
            //*********************************************************************
            // Uses TODO 02 to get the recommended movies for the logged in user. *
            //*********************************************************************
            DataTable dtRecommendedMovies = myReelflicsDB.GetRecommendedMovies(username);

            // Show the recommended movies if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 02",
                                             dtRecommendedMovies,
                                             new List<string> { "MOVIEID", "TITLE", "IMDBRATING" },
                                             lblErrorMessage))
            {
                if (dtRecommendedMovies.Rows.Count != 0)
                {
                    if (myDBHelpers.PopulateMovieDisplay(dtRecommendedMovies, phRecommendedMovies, lblErrorMessage))
                    { result = true; }
                }
                else { pnlSplashscreen.Visible = true; } // No movies were retrieved.
            }
            return result;
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { if (PopulateRecommendedMovies()) { pnlRecommendedMovies.Visible = true; } }
        }
    }
}