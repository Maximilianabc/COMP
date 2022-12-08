using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace ReelflicsWebsite
{
    public class Global : HttpApplication
    {
        public enum ReelflicsRole { ReelflicsMember, Employee, None }

        public enum SearchType { All, Movie, Person }

        public enum AwardType { Directing, Acting }

        public enum RecordStatus { add, existing, modify, remove }

        public enum EmptyQueryResultMessageType { DBError, DBQueryError, SQLError, Information, NoMessage }

        public static bool isEmptyQueryResult = false;
        public static bool isSqlError = false;

        public static int maxGenreLength = 15;

        public static string bestPictureAwardId = "1";
        public static string contact3311rep = " Please contact 3311rep.";
        public static string directingAward = "Best Director";
        public static string employee = "employee";
        public static string equal = "==";
        public static string noMessage = null;
        public static string notequal = "!=";
        public static string postersDirectory = "~/images/posters/";
        public static string peopleDirectory = "~/images/people/";
        public static string RMSSPassword = "Reelflics1#";
        public static string sqlErrorMessage;
        public static string tempPeoplePhoto = "~/images/people/0-tempPhoto.jpg";
        public static string tempPosterImage = "~/images/posters/0-tempPoster.jpg";

        // Feedback messages.
        public static string chooseUploadFile = "Choose a file to upload!";
        public static string informationNotChanged = "No information has been changed.";
        public static string informationUpdated = "The information has been updated.";
        public static string noActorActressFilmography = "The actor/actress has no additional filmography.";
        public static string noDirectorFilmography = "The director has no additional filmography.";
        public static string noFilmography = "There is no filmography for this person.";
        public static string reviewNotChanged = "The review has not been modified.";
        public static string reviewModified = "The review has been modified.";
        public static string reviewSubmitted = "The review has been submitted.";
        public static string noAcademyAwardsToAssign = "No academy awards can be assigned.";
        public static string noMovieMatches = "No title matches the query.";
        public static string noMoviePersonMatches = "No name matches the query.";
        public static string noWatchHistory = "No movies have been watched.";
        public static string noWatchlist = "There are no movies on the watchlist.";
        public static string noneAssigned = "None assigned.";

        // Internal error messages.
        public static string emptyOrNullErrorMessage = "*** System error: Empty or null error message. " + contact3311rep;
        public static string nullDataTableErrorMessage = "*** System error: Null DataTable ";
        public static string nullSessionStateErrorMessage = "*** System error: Session state value is null for ";
        public static string querystringIsNullOrEmpty = "*** System error: Querystring is null or empty in Page_Load method of ";
        public static string unknownRecordStatus = "*** System error: Unknown record status ";

        // Database error messages.
        public static string dbError = "*** Database error in ";
        public static string dbErrorNoMovie = ": No record was retrieved for the selected movie. Please check your database.";
        public static string dbErrorNoMoviePerson = ": No record was retrieved for the selected person. Please check your database.";

        // Database/query error message.
        public static string dbqueryError = "*** Database/SQL error in ";
        public static string dbqueryErrorNoActors = ": No actors were retrieved for the movie. Please check your query and/or database.";
        public static string dbqueryErrorNoDirectors = ": No directors were retrieved for the movie. Please check your query and/or database.";
        public static string dbqueryErrorNoFilmography = ": No actor or director filmography was retrieved. Please check your query and/or database.";
        public static string dbqueryErrorNoGenres = ": No genres were retrieved. Please check your query and/or database.";
        public static string dbqueryErrorNoEducationLevelReport = ": No education level information were retrieved. Please check your query and/or database.";
        public static string dbqueryErrorNoMembershipReport = ": No membership information were retrieved. Please check your query and/or database.";
        public static string dbqueryErrorNoRecordsRetrieved = ": No record was retrieved. Please check your query and/or database.";

        // Query error messages.
        public static string queryError = "*** SQL error in ";
        public static string queryErrorAttributesNotInCorrectOrder = ": The attributes are not retrieved in the correct order.";
        public static string queryErrorMultipleRecordsRetrieved = ": Your query retrieves more than one record.";
        public static string queryErrorNoRecordsRetrieved = ": Your query does not retrieve any records.";

        // Empty query result messages.
        public static string noNameMatches = "No name matches.";
        public static string noReviews = "There are no reviews for this movie.";
        public static string noTitleMatches = "No movie title matches.";

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}