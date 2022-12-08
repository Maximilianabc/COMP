using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Oracle.DataAccess.Client;
using ReelflicsWebsite.Models;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite.App_Code
{
    public class DBHelperMethods : Page
    {
        private readonly OracleDBAccess myOracleDB = new OracleDBAccess();
        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private string errorMsg;
        private string sql;

        /***** Private Methods *****/

        private string GetReelflicsRating(string movieId, Label labelControl) // Uses TODO 08
        {
            string result = null;
            //***************************************************************
            // Uses TODO 08 to get the members' average rating for a movie. *
            //***************************************************************
            decimal rating = myReelflicsDB.GetReelflicsMovieRating(movieId);
            if (rating != -1)
            {
                if (rating != 0) // The movie has been reviewed, so show the rating.
                {
                    result = Math.Round(rating, 1).ToString();
                }
                else // The movie has not been reviewed.
                {
                    result = "<br />Not yet rated";
                }
            }
            else { myHelpers.DisplayMessage(labelControl, sqlErrorMessage); } // An SQL error occurred.
            return result;
        }

        private ReelflicsRole GetReelflicsUserRole(string username)
        {
            // Return the None role if the username is not found in the following.
            ReelflicsRole resultRole = ReelflicsRole.None;
            // If the user is a member, return the ReelflicsMember role.
            if (IsUserInReelflicsRole(username, ReelflicsRole.ReelflicsMember.ToString()) == 1) { resultRole = ReelflicsRole.ReelflicsMember; }
            // Else if the username is 'employee', return the Employee role.
            else if (username == employee) { resultRole = ReelflicsRole.Employee; }
            return resultRole;
        }

        private decimal IsUserInReelflicsRole(string username, string tableName)
        {
            sql = $"select count(*) from {tableName} where username='{username}'";
            return myOracleDB.GetAggregateValue("DBHelperMethods - IsUserInReelflicsRole", sql);
        }

        private bool ProcessMovieGenreChanges(string movieId,
                                              DataTable dtMovieGenres,
                                              Label labelControl,
                                              OracleTransaction trans) // Uses TODO 20, TODO 21
        {
            foreach (DataRow row in dtMovieGenres.Rows)
            {
                RecordStatus status = (RecordStatus)Enum.Parse(typeof(RecordStatus), row["STATUS"].ToString());
                switch (status)
                {
                    case RecordStatus.add:
                        //******************************************
                        // Uses TODO 20 to add a genre to a movie. *
                        //******************************************
                        if (!myReelflicsDB.AddMovieGenre(movieId, row["GENRE"].ToString(), trans)) { return false; }
                        break;
                    case RecordStatus.remove:
                        //************************************************
                        // Uses TODO 21 to remove a genre from a movie. *
                        //************************************************
                        if (!myReelflicsDB.RemoveMovieGenre(movieId, row["GENRE"].ToString(), trans)) { return false; }
                        break;
                    default:
                        myHelpers.DisplayMessage(labelControl, unknownRecordStatus
                                                               + status.ToString()
                                                               + " in DBHelperMethods - ProcessMovieGenreChanges"
                                                               + contact3311rep);
                        return false;
                }
            }
            return true;
        }

        private bool ProcessDirectorChanges(string movieId,
                                            DataTable dtDirectors,
                                            Label labelControl,
                                            OracleTransaction trans) // Uses TODO 30, TODO 31
        {
            foreach (DataRow row in dtDirectors.Rows)
            {
                RecordStatus status = (RecordStatus)Enum.Parse(typeof(RecordStatus), row["STATUS"].ToString());
                switch (status)
                {
                    case RecordStatus.add:
                        //**********************************************
                        // Uses TODO 30 to add a director for a movie. *
                        //**********************************************
                        if (!myReelflicsDB.AddMovieDirector(movieId, row["PERSONID"].ToString(), trans)) { return false; }
                        break;
                    case RecordStatus.remove:
                        //*************************************************
                        // Uses TODO 31 to remove a director for a movie. *
                        //*************************************************
                        if (!myReelflicsDB.RemoveMovieDirector(movieId, row["PERSONID"].ToString(), trans)) { return false; }
                        break;
                    default:
                        myHelpers.DisplayMessage(labelControl, unknownRecordStatus
                                                               + status.ToString()
                                                               + " in DBHelperMethods - ProcessDirectorChanges"
                                                               + contact3311rep);
                        return false;
                }
            }
            return true;
        }

        private bool ProcessCastChanges(string movieId,
                                        DataTable dtCast,
                                        Label labelControl,
                                        OracleTransaction trans) // Uses TODO 28, TODO 29, TODO 32
        {
            foreach (DataRow row in dtCast.Rows)
            {
                RecordStatus status = (RecordStatus)Enum.Parse(typeof(RecordStatus), row["STATUS"].ToString());
                switch (status)
                {
                    case RecordStatus.add:
                        //************************************************
                        // Uses TODO 28 to add a cast member to a movie. *
                        //************************************************
                        if (!myReelflicsDB.AddMovieCastMember(movieId, row["PERSONID"].ToString(),
                                                       StringExtension.CleanInput(row["ROLE"].ToString()), trans)) { return false; }
                        break;
                    case RecordStatus.remove:
                        //***************************************************
                        // Uses TODO 29 to remove a cast member from movie. *
                        //***************************************************
                        if (!myReelflicsDB.RemoveMovieCastMember(movieId, row["PERSONID"].ToString(), trans)) { return false; }
                        break;
                    case RecordStatus.modify:
                        //**********************************************************
                        // Uses TODO 32 to change a cast member's role in a movie. *
                        //**********************************************************
                        if (!myReelflicsDB.ChangeMovieCastMemberRole(movieId, row["PERSONID"].ToString(),
                                                              StringExtension.CleanInput(row["ROLE"].ToString()), trans)) { return false; }
                        break;
                    default:
                        myHelpers.DisplayMessage(labelControl, unknownRecordStatus
                                                               + status.ToString()
                                                               + " in DBHelperMethods - ProcessCastChanges"
                                                               + contact3311rep);
                        return false;
                }
            }
            return true;
        }

        /***** Public Methods *****/

        public bool AddMovieInformation(string movieId,
                                        string title,
                                        string synopsis,
                                        string releaseYear,
                                        string runningTime,
                                        string MPAARating,
                                        string IMDBRating,
                                        string bestPictureAwardId,
                                        DataTable dtMovieGenres,
                                        DataTable dtDirectors,
                                        DataTable dtCast,
                                        Label labelControl) // Uses TODO 17
        {
            // Create an Oracle Transaction.
            OracleTransaction trans = myOracleDB.BeginTransaction("transaction begin for DBHelperMethods - AddMovieInformation");
            if (trans == null) { return false; } // Creating the transaction failed.

            //**************************************
            // Uses TODO 17 to add a movie record. *
            //**************************************
            if (!myReelflicsDB.AddMovieRecord(movieId, title, synopsis, releaseYear, runningTime, MPAARating, IMDBRating, bestPictureAwardId, trans))
            { return false; }

            // Uses TODO 20 to add or remove genres of a movie.
            if (!ProcessMovieGenreChanges(movieId, dtMovieGenres, labelControl, trans))
            {
                // Delete the movie record if an SQL error occurs.
                errorMsg = sqlErrorMessage;
                if (!DeleteMovie(movieId)) { sqlErrorMessage += Environment.NewLine + errorMsg; }
                return false;
            }

            // Uses TODO 30 to add or remove directors of a movie.
            if (!ProcessDirectorChanges(movieId, dtDirectors, labelControl, trans))
            {
                // Delete the movie record if an SQL error occurs.
                errorMsg = sqlErrorMessage;
                if (!DeleteMovie(movieId)) { sqlErrorMessage += Environment.NewLine + errorMsg; }
                return false;
            }

            // Uses TODO 28 to add, remove or update the role of cast members of a movie.
            if (!ProcessCastChanges(movieId, dtCast, labelControl, trans))
            {
                // Delete the movie record if an SQL error occurs.
                errorMsg = sqlErrorMessage;
                if (!DeleteMovie(movieId)) { sqlErrorMessage += Environment.NewLine + errorMsg; }
                return false;
            }

            myOracleDB.CommitTransaction("transaction commit for DBHelperMethods - AddMovieInformation", trans);
            return true;
        }

        public bool DeleteMovie(string movieId)
        {
            sql = $"delete from Movie where movieId={movieId}";
            return myOracleDB.SetData("DBHelperMethods - DeleteMovie", sql);
        }

        public DataTable GetReelflicsMembers()
        {
            sql = $"select username, lastName || ', ' || firstName as name from ReelflicsMember order by name";
            return myOracleDB.GetData("DBHelperMethods - GetReelflicsMembers", sql);
        }

        public string GetNextTableId(string tableName, string idName, Label labelControl)
        {
            string id = null;
            sql = $"select max({idName}) from {tableName}";
            decimal nextId = myOracleDB.GetAggregateValue("DBHelperMethods - GetNextTableId", sql);
            if (nextId != -1) { id = (nextId + 1).ToString(); }
            else { myHelpers.DisplayMessage(labelControl, sqlErrorMessage + contact3311rep); } //An SQL error occurred. 
            return id;
        }

        public bool InsertReelflicsMember(string username,
                                         string pseudonym,
                                         string firstName,
                                         string lastName,
                                         string occupation,
                                         string email,
                                         string gender,
                                         string birthdate,
                                         string phoneNumber,
                                         string educationLevel,
                                         string cardholderName,
                                         string cardNumber,
                                         string cardType,
                                         string securityCode,
                                         string expiryMonth,
                                         string expiryYear)
        {
            sql = $"insert into ReelflicsMember values ('{username}', '{pseudonym}', '{firstName}' ,'{lastName}', '{occupation}', " +
                $"'{email}', '{gender}', '{birthdate}', '{phoneNumber}', '{educationLevel}', '{cardholderName}', '{cardNumber}', " +
                $"'{cardType}', '{securityCode}', '{expiryMonth}', '{expiryYear}')";
            bool result = myOracleDB.SetData("DBHelperMethods - InsertReelflicsMember", sql);
            if (result == false) { sqlErrorMessage += contact3311rep; }
            return result;
        }

        public bool IsEmailValid(string previousEmail, string newEmail)
        {
            if (previousEmail != newEmail)
            {
                sql = $"select count(*) from ReelflicsMember where email='{newEmail}'";
                decimal queryResult = myOracleDB.GetAggregateValue("DBHelperMethods - IsEmailValid", sql);
                if (queryResult == -1) { sqlErrorMessage += contact3311rep; return false; } // An SQL error occurred.
                else if (queryResult != 0) { return false; }
            }
            return true;
        }

        public bool IsPseudonymValid(string pseudonym)
        {
            // Determine if the pseudonym is that of an existing Reelflics member. 
            sql = $"select count(*) from ReelflicsMember where pseudonym='{pseudonym}'";
            decimal userQueryResult = myOracleDB.GetAggregateValue("DBHelperMethods - IsPseudonymValid", sql);
            if (userQueryResult == -1) { sqlErrorMessage += contact3311rep; return false; } // an SQL error occurred.
            else if (userQueryResult != 0) { return false; }
            return true;
        }

        public bool IsUsernameValid(string username)
        {
            // Determine if the username is that of an existing Reelflics member. 
            sql = $"select count(*) from ReelflicsMember where username='{username}'";
            decimal userQueryResult = myOracleDB.GetAggregateValue("DBHelperMethods - IsUsernameValid", sql);
            if (userQueryResult == -1) { sqlErrorMessage += contact3311rep; return false; } // an SQL error occurred.
            else if (userQueryResult != 0 || username == employee) { return false; }

            // If a 0 result was returned or the username is not 'employee' => the username does not exist in the database.
            return true;
        }

        public bool IsUserReelflicsMember(string username)
        {
            if (IsUserInReelflicsRole(username, ReelflicsRole.ReelflicsMember.ToString()) == 1) { return true; }
            return false;
        }

        public bool ModifyMovieInformation(bool isMovieInformationChanged,
                                           string movieId,
                                           string title,
                                           string synopsis,
                                           string releaseYear,
                                           string runningTime,
                                           string MPAARating,
                                           string IMDBRating,
                                           string bestPictureAwardId,
                                           DataTable dtMovieGenreChanges,
                                           DataTable dtDirectorChanges,
                                           DataTable dtCastChanges,
                                           Label labelControl) // Uses TODO 18
        {
            // Create an Oracle transaction.
            OracleTransaction trans = myOracleDB.BeginTransaction("transaction begin for DBHelperMethods - ModifyMovieInformation");
            if (trans == null) { return false; } // Creating the transaction failed.

            if (isMovieInformationChanged) // Update the movie record if it has changed.
            {
                //*****************************************
                // Uses TODO 18 to update a movie record. *
                //*****************************************
                if (!myReelflicsDB.ModifyMovieRecord(movieId,
                                                    title,
                                                    synopsis,
                                                    releaseYear,
                                                    runningTime,
                                                    MPAARating,
                                                    IMDBRating,
                                                    bestPictureAwardId,
                                                    trans))
                { return false; }
            }

            // Uses TODO 20, TODO 21.
            if (!ProcessMovieGenreChanges(movieId, dtMovieGenreChanges, labelControl, trans)) { return false; }

            // Uses TODO 30, TODO 31.
            if (!ProcessDirectorChanges(movieId, dtDirectorChanges, labelControl, trans)) { return false; }

            // Uses TODO 28, TODO 29, TODO 32.
            if (!ProcessCastChanges(movieId, dtCastChanges, labelControl, trans)) { return false; }

            myOracleDB.CommitTransaction("transaction commit for DBHelperMethods - ModifyMovieInformation", trans);
            return true;
        }

        public bool PopulateMovieDisplay(DataTable dtMovies, PlaceHolder placeholderControl, Label labelControl)
        {
            int movieOffset = 0; // Used to select the information of a specific movie in dtMovies.
            int maxMoviesInRow = 6; // The maximum number of movies to display in a row.
            int movieLoopMax = maxMoviesInRow; // Controls how many movies to place in a row; normally equal to maxMoviesinRow except possibly for the last row.
            int remainingMovies = dtMovies.Rows.Count; // The number of movies remaining to display.
            string movieDisplayPage = "~/Shared/MovieInformation.aspx?movieId="; // The page on which the movie information is displayed.

            do
            {
                if (remainingMovies < maxMoviesInRow) { movieLoopMax = remainingMovies; }

                // Add poster images row.
                Literal litStartImageRowCss = new Literal { Text = "<div class=\"row\" style=\"text-align: center\">" };
                placeholderControl.Controls.Add(litStartImageRowCss);
                for (int i = 0; i < movieLoopMax; i++)
                {
                    Literal litStartColumnCss = new Literal { Text = "<div class=\"col-xs-2\">" };
                    placeholderControl.Controls.Add(litStartColumnCss);
                    HyperLink hlPoster = new HyperLink
                    {
                        NavigateUrl = movieDisplayPage + dtMovies.Rows[movieOffset + i]["MOVIEID"].ToString()
                    };
                    if (!User.IsInRole(ReelflicsRole.ReelflicsMember.ToString())) { hlPoster.Enabled = false; }
                    Image imgPoster = new Image
                    {
                        ImageUrl = postersDirectory
                                   + dtMovies.Rows[movieOffset + i]["MOVIEID"].ToString()
                                   + StringExtension.CreateFileName(dtMovies.Rows[movieOffset + i]["TITLE"].ToString()),
                        CssClass = "img-responsive center-block",
                        BorderColor = System.Drawing.Color.White,
                        BorderWidth = 2
                    };
                    hlPoster.Controls.Add(imgPoster);
                    placeholderControl.Controls.Add(hlPoster);
                    Literal litEndRowCSS = new Literal { Text = "</div>" };
                    placeholderControl.Controls.Add(litEndRowCSS);
                }
                Literal litEndImageRowCss = new Literal { Text = "</div>" };
                placeholderControl.Controls.Add(litEndImageRowCss);

                // Add title row.
                Literal litStartTitleRowCss = new Literal { Text = "<div class=\"row\" style=\"text-align: center; margin-top: 5px\">" };
                placeholderControl.Controls.Add(litStartTitleRowCss);
                for (int i = 0; i < movieLoopMax; i++)
                {
                    Literal litStartColumnCss = new Literal { Text = "<div class=\"col-xs-2\">" };
                    placeholderControl.Controls.Add(litStartColumnCss);
                    HyperLink hlTitle = new HyperLink
                    {
                        Text = dtMovies.Rows[movieOffset + i]["TITLE"].ToString(),
                        NavigateUrl = movieDisplayPage + dtMovies.Rows[movieOffset + i]["MOVIEID"].ToString()
                    };
                    if (!User.IsInRole(ReelflicsRole.ReelflicsMember.ToString())) { hlTitle.Enabled = false; hlTitle.Style.Add("text-decoration", "none"); }
                    placeholderControl.Controls.Add(hlTitle);
                    Literal litEndRowCSS = new Literal { Text = "</div>" };
                    placeholderControl.Controls.Add(litEndRowCSS);
                }
                Literal litEndTitleRowCss = new Literal { Text = "</div>" };
                placeholderControl.Controls.Add(litEndTitleRowCss);

                // Add IMDB rating row.
                Literal litStartIMDBRatingRowCss = new Literal { Text = "<div class=\"row\" style=\"text-align: center\">" };
                placeholderControl.Controls.Add(litStartIMDBRatingRowCss);
                for (int i = 0; i < movieLoopMax; i++)
                {
                    Literal litStartColumnCss = new Literal { Text = "<div class=\"col-xs-2\">" };
                    placeholderControl.Controls.Add(litStartColumnCss);
                    Literal litIMDBRating = new Literal
                    {
                        Text = "IMDB rating <span style=\"color: crimson\">"
                               + dtMovies.Rows[movieOffset + i]["IMDBRating"].ToString()
                               + "</span>"
                    };
                    placeholderControl.Controls.Add(litIMDBRating);
                    Literal litEndRowCSS = new Literal { Text = "</div>" };
                    placeholderControl.Controls.Add(litEndRowCSS);
                }
                Literal litEndIMDBRatingRowCss = new Literal { Text = "</div>" };
                placeholderControl.Controls.Add(litEndIMDBRatingRowCss);

                // Add Reelflics rating row.
                Literal litStartReelflicsRatingRowCss = new Literal { Text = "<div class=\"row\" style=\"text-align: center\">" };
                placeholderControl.Controls.Add(litStartReelflicsRatingRowCss);
                for (int i = 0; i < movieLoopMax; i++)
                {
                    Literal litStartColumnCss = new Literal { Text = "<div class=\"col-xs-2\">" };
                    placeholderControl.Controls.Add(litStartColumnCss);
                    Literal litReelflicsRating = new Literal
                    {
                        Text = "Reelflics rating <span style=\"color: crimson\">"
                               + GetReelflicsRating(dtMovies.Rows[movieOffset + i]["MOVIEID"].ToString(), labelControl)
                               + "</span>"
                    };
                    placeholderControl.Controls.Add(litReelflicsRating);
                    Literal litEndRowCSS = new Literal { Text = "</div>" };
                    placeholderControl.Controls.Add(litEndRowCSS);
                    if (string.IsNullOrEmpty(litReelflicsRating.Text)) { return false; }
                }
                Literal litEndReelflicsRatingRowCss = new Literal { Text = "</div>" };
                placeholderControl.Controls.Add(litEndReelflicsRatingRowCss);

                Literal litRowBreak = new Literal { Text = "<br /><br />" };
                placeholderControl.Controls.Add(litRowBreak);

                movieOffset += movieLoopMax;
                remainingMovies -= movieLoopMax;
            } while (remainingMovies > 0);
            return true;
        }

        public bool SynchLoginAndApplicationDatabases(string username, Literal literalControl)
        {
            bool synchResult = true;
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            // Get the role of the user.
            ReelflicsRole role = GetReelflicsUserRole(username);
            ApplicationUser user = manager.FindByName(username);

            switch (role)
            {
                case ReelflicsRole.None:
                    // If the user is not in ReelflicsDB, but is in AspNetUsers, then delete him/her from AspNetUsers.
                    if (user != null) { manager.Delete(user); }
                    break;
                case ReelflicsRole.ReelflicsMember:
                    // If the user is not in AspNetUsers, create the user and add the user to the ReelflicsMember role.
                    if (user == null)
                    {
                        user = new ApplicationUser() { UserName = username };
                        IdentityResult result = manager.Create(user, RMSSPassword);
                        if (result.Succeeded)
                        {
                            IdentityResult roleResult = manager.AddToRole(user.Id, role.ToString());
                            if (!roleResult.Succeeded)
                            {
                                manager.Delete(user);
                                literalControl.Text = $"Failed to create role {role} for user {username}.{contact3311rep}";
                                synchResult = false;
                            }
                        }
                        else
                        {
                            literalControl.Text = $"Failed to create user {username}.{contact3311rep}";
                            synchResult = false;
                        }
                    }
                    else // The user is in ReelflicsDB and in AspNetUsers.
                    {
                        // If the user is not in the ReelflicsMember role, add the user in this role.
                        if (!manager.IsInRole(user.Id, ReelflicsRole.ReelflicsMember.ToString()))
                        {
                            // Add the user to the ReelflicsMember role.
                            IdentityResult roleResult = manager.AddToRole(user.Id, ReelflicsRole.ReelflicsMember.ToString());
                            if (!roleResult.Succeeded)
                            {
                                literalControl.Text = $"Failed to add user to {ReelflicsRole.ReelflicsMember} role.{contact3311rep}";
                                synchResult = false;
                            }
                        }
                    }
                    break;
                case ReelflicsRole.Employee:
                    // No action needed for Employee role.
                    break;
                default:
                    // Should never get here.
                    literalControl.Text = $"User {username} is not in any role.{contact3311rep}";
                    synchResult = false;
                    break;
            }
            return synchResult;
        }
    }
}