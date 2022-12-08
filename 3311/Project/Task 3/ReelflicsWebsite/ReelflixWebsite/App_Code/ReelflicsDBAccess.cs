using Oracle.DataAccess.Client;
using System.Data;

namespace ReelflicsWebsite.App_Code
{
    /// <summary>
    /// Student name: LEUNG Ho Man Max
    /// Student number: 20611398
    /// 
    ///                     ***** IMPORTANT *****
    /// This is an individual task. By submitting this file you certify that
    /// this code is the result of YOUR INDIVIDUAL EFFORT and that it has not
    /// been developed in collaoration with or copied from any other person.
    /// If this is not the case, then you must identify below, by name, all
    /// the persons with whom you collaborated or from whom you copied code.
    /// 
    /// Collaborators: 
    /// </summary>

    public class ReelflicsDBAccess
    {
        //******************************** IMPORTANT NOTE ********************************
        // For the web pages to display a query result correctly, and possibly to not    *
        // generate errors, the attributes should be retrieved in the order specified,   *
        // if any, in a TODO and the attribute names in a query result table must be     *
        // EXACTLY the same as that in the database tables.                              *
        //                                                                               *
        //   REMINDER: DO NOT place single quotes around numeric data type parameters.   *
        //                                                                               *
        //          Report problems with the website code to 3311rep@cse.ust.hk.         *
        //********************************************************************************

        private readonly OracleDBAccess myOracleDBAccess = new OracleDBAccess();
        private DataTable queryResult;
        private decimal aggregateQueryResult;
        private bool updateResult;
        private string sql;
        //********************************************************************************

        #region General SQL Statements

        public DataTable GetMostWatchedMovies() // TODO 01
        {
            //********************************************************************************
            // TODO 01: Construct the SELECT statement to retrieve the movie id, title and   *
            //          IMDB rating of the most watched movies by Reelflics members. Count   *
            //          each time a member watched a movie. Limit the number of results to   *
            //          at most twelve. Order the result first by the number of times each   *
            //          movie was watched descending, then by title ascending.               *
            //********************************************************************************

            /* PLACEHOLDER QUERY - Replace this query with one that meets the to TODO requirements. */
            sql = $"select movieId, title, IMDBRating, count(*) from Movie group by movieId, title, IMDBRating order by count(*) desc, title asc fetch first 12 rows only;";
            return queryResult = myOracleDBAccess.GetData("TODO 01", sql);
        }

        public DataTable GetRecommendedMovies(string username) // TODO 02
        {
            //********************************************************************************
            // TODO 02: Construct the SELECT statement to retrieve the movie id, title and   *
            //          IMDB rating of the movies recommended for a member. The recommended  *
            //          movies should be those that have genres that include both of the two *
            //          most frequently occurring genres of the distinct movies in a         *
            //          member's watch history. Moreover, the recommended movies should      *
            //          not appear in the member's watch history or watchlist.               *
            //          Order the result by title ascending.                                 *
            //********************************************************************************

            /* PLACEHOLDER QUERY - Replace this query with one that meets the to TODO requirements. */
            sql = $"select Movie.movieId, title, IMDBRating from Movie";
            return queryResult = myOracleDBAccess.GetData("TODO 02", sql);
        }

        public DataTable GetMovieSearchResult(string searchString) // TODO 03
        {
            //********************************************************************************
            // TODO 03: Construct the SELECT statement to retrieve the movie id, title and   *
            //          release year of those movies where there is a match for the search   *
            //          string anywhere in the title or where the search string matches      *
            //          exactly one of the movie's genres. Search string matching should be  *
            //          case insensitive. Order the result by movie title ascending.         *
            //********************************************************************************
            sql = $"select Movie.movieId, title, releaseYear from Movie join MovieGenre on Movie.movieId = MovieGenre.movieId where regexp_like(title, '{searchString}', 'i') or regexp_like(genre, '{searchString}', 'i') order by title asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 03", sql);
        }

        public DataTable GetMoviePersonSearchResult(string searchString) // TODO 04
        {
            //********************************************************************************
            // TODO 04: Construct the SELECT statement to retrieve the movie person id and   *
            //          name of the cast members or directors where there is a match for the *
            //          search string anywhere in the name. Search string matching should be *
            //          case insensitive. Order the result by name ascending.                *
            //********************************************************************************
            sql = $"select personId, name from MoviePerson where regexp_like(name, '{searchString}', 'i') order by name asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 04", sql);
        }

        #endregion General SQL Statements

        #region Movie SQL Statements

        public DataTable GetMovieRecord(string movieId) // TODO 05
        {
            //********************************************************************************
            // TODO 05: Construct the SELECT statement to retrieve the record of a movie     *
            //          identified by its movie id.                                          *
            //********************************************************************************
            sql = $"select * from Movie where movieId = {movieId}";
            return queryResult = myOracleDBAccess.GetData("TODO 05", sql);
        }

        public DataTable GetMovieGenres(string movieId) // TODO 06
        {
            //********************************************************************************
            // TODO 06: Construct the SELECT statement to retrieve the movie id and genres   *
            //          of the movie identified by its movie id. Order the result by genre   *
            //          ascending.                                                           *
            //********************************************************************************
            sql = $"select Movie.movieId, genre from Movie join MovieGenre on Movie.movieId = MovieGenre.movieId where Movie.movieId = {movieId} order by genre asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 06", sql);
        }

        public DataTable GetMovieReviews(string movieId) // TODO 07
        {
            //********************************************************************************
            // TODO 07: Construct the SELECT statement to retrieve the movie id, username,   *
            //          title, rating, review text, review date and pseudonym of the member  *
            //          who submitted the review for all the reviews of a movie identified   *
            //          by its movie id. Order the result by review date descending.         *
            //********************************************************************************
            sql = $"select movieId, Review.username, title, rating, reviewText, reviewDate, pseudonym from Review join ReelflicsMember on Review.username = ReelflicsMember.username where movieId = {movieId} order by reviewDate desc;";
            return queryResult = myOracleDBAccess.GetData("TODO 07", sql);
        }

        public decimal GetReelflicsMovieRating(string movieId) // TODO 08
        {
            //********************************************************************************
            // TODO 08: Construct the SELECT statement to retrieve the average Reelflics     *
            //          rating of a movie identified by its movie id. Round the result to    *
            //          one decimal place.                                                   *
            //********************************************************************************
            sql = $"select round(avg(rating), 1) from Review where movieId = {movieId};";
            return aggregateQueryResult = myOracleDBAccess.GetAggregateValue("TODO 08", sql);
        }

        public DataTable GetMovieAcademyAwards(string movieId) // TODO 09
        {
            //********************************************************************************
            // TODO 09: Construct the SELECT statement to retrieve the person id and award   *
            //          name of all the academy awards won by the actors and directors for a *
            //          movie identified by its movie id.                                    *
            //********************************************************************************
            sql = $"select MoviePerson.personId, awardName from MoviePerson join AwardWin on MoviePerson.personId = AwardWin.personId join AcademyAward on AcademyAward.awardId = AwardWin.awardId where AwardWin.movieId = {movieId};";
            return queryResult = myOracleDBAccess.GetData("TODO 09", sql);

        }

        public DataTable GetMovieDirectors(string movieId) // TODO 10
        {
            //********************************************************************************
            // TODO 10: Construct the SELECT statement to retrieve the person id and name of *
            //          the directors of a movie identified by its movie id.                 *
            //          Order the result by name ascending.                                  *
            //********************************************************************************
            sql = $"select DirectorOf.personId, name from DirectorOf join MoviePerson on DirectorOf.personId = MoviePerson.personId where DirectorOf.movieId = {movieId} order by name asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 10", sql);
        }

        public DataTable GetMovieCastMembers(string movieId) // TODO 11
        {
            //********************************************************************************
            // TODO 11: Construct the SELECT statement to retrieve the person id, name and   *
            //          role of the cast members of a movie identified by its movie id.      *
            //          Order the result by name ascending.                                  *
            //********************************************************************************
            sql = $"select CastMemberAppearsIn.personId, name, role from CastMemberAppearsIn join MoviePerson on CastMemberAppearsIn.personId = MoviePerson.personId where CastMemberAppearsIn.movieId = {movieId} order by name asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 11", sql);
        }

        public DataTable GetWatchlist(string username) // TODO 12
        {
            //********************************************************************************
            // TODO 12: Construct the SELECT statement to retrieve the movie id, title,      *
            //          release year, running time and MPAA rating of the movies on the      *
            //          watchlist of a member identified by his/her username.                *
            //          Order the result by title ascending.                                 *
            //********************************************************************************
            sql = $"select Movie.movieId, title, releaseYear, runningTime, MPAARating from Movie join Watchlist on Movie.movieId = Watchlist.movieId where username = '{username}' order by title asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 12", sql);
        }

        public bool AddToWatchlist(string movieId, string username) // TODO 13
        {
            //********************************************************************************
            // TODO 13: Construct the INSERT statement to add a movie, identified by its     *
            //          movie id, to the watchlist of a member identified by his/her         *
            //          username.                                                            *
            //********************************************************************************
            sql = $"insert into Watchlist values({movieId}, '{username}')";
            return updateResult = myOracleDBAccess.SetData("TODO 13", sql);
        }

        public bool RemoveFromWatchlist(string movieId, string username) // TODO 14
        {
            //********************************************************************************
            // TODO 14: Construct the DELETE statement to remove a movie, identified by its  *
            //          movie id, from the watchlist of a member identified by his/her       *
            //          username.                                                            *
            //********************************************************************************
            sql = $"delete from Watchlist where movieId = {movieId} and username = '{username}'";
            return updateResult = myOracleDBAccess.SetData("TODO 14", sql);
        }

        public DataTable GetMostRecentWatchDate(string movieId, string username) // TODO 15
        {
            //********************************************************************************
            // TODO 15: Construct the SELECT statement to retrieve, for a movie identified   *
            //          by its movie id, its most recent watch date from the watch history   *
            //          of a member, identified by his/her username.                         *
            //********************************************************************************
            sql = $"select watchDate from WatchHistory where movieId = {movieId} and username = '{username}' order by watchDate desc fetch next 1 row only;";
            return queryResult = myOracleDBAccess.GetData("TODO 15", sql);
        }

        public DataTable GetAcademyAwards() // TODO 16
        {
            //********************************************************************************
            // TODO 16: Construct the SELECT statement to retrieve all the acting and        *
            //          directing academy award records. Order the result by award id        *
            //          ascending.                                                           *
            //********************************************************************************
            sql = $"select * from AcademyAward order by awardId asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 16", sql);
        }

        public bool AddMovieRecord(string movieId, string title, string synopsis, string releaseYear,
            string runningTime, string MPAARating, string IMDBRating, string bestPictureAwardId, OracleTransaction trans) // TODO 17
        {
            //********************************************************************************
            // TODO 17: Construct the INSERT statement to add a movie record.                *
            //********************************************************************************
            sql = $"insert into Movie values ({movieId}, '{title}', '{synopsis}', '{releaseYear}', {runningTime}, '{MPAARating}', {IMDBRating}, {bestPictureAwardId});";
            return updateResult = myOracleDBAccess.SetData("TODO 17", sql, trans);
        }

        public bool ModifyMovieRecord(string movieId, string title, string synopsis, string releaseYear,
            string runningTime, string MPAARating, string IMDBRating, string bestPictureAwardId, OracleTransaction trans) // TODO 18
        {
            //********************************************************************************
            // TODO 18: Construct the UPDATE statement to update the record of a movie       *
            //          identified by its movie id.                                          *
            //********************************************************************************
            sql = $"update Movie set title = '{title}', synopsis = '{synopsis}', releaseYear = '{releaseYear}', runningTime = {runningTime}, MPAARating = '{MPAARating}', IMDBRating = {IMDBRating}, bestPictureAwardId = {bestPictureAwardId} where movieId = {movieId};";
            return updateResult = myOracleDBAccess.SetData("TODO 18", sql, trans);
        }

        public DataTable GetGenres() // TODO 19
        {
            //********************************************************************************
            // TODO 19: Construct the SELECT statement to retrieve the distinct genres of    *
            //          the movies currently in the database. Order the result by genre      *
            //          ascending.                                                           *
            //********************************************************************************
            sql = $"select distinct genre from MovieGenre order by genre asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 19", sql);
        }

        public bool AddMovieGenre(string movieId, string genre, OracleTransaction trans) // TODO 20
        {
            //********************************************************************************
            // TODO 20: Construct the INSERT statement to add a genre for a movie identified *
            //          by its movie id.                                                     *
            //********************************************************************************
            sql = $"insert into MovieGenre values ({movieId}, '{genre}')";
            return updateResult = myOracleDBAccess.SetData("TODO 20", sql, trans);
        }

        public bool RemoveMovieGenre(string movieId, string genre, OracleTransaction trans) // TODO 21
        {
            //********************************************************************************
            // TODO 21: Construct the DELETE statement to remove a genre for a movie         *
            //          identified by its movie id.                                          *
            //********************************************************************************
            sql = $"delete from MovieGenre where movieId = {movieId} and genre = '{genre}'";
            return updateResult = myOracleDBAccess.SetData("TODO 21", sql, trans);
        }

        #endregion Movie SQL Statements

        #region Movie Person SQL Statements

        public DataTable GetMoviePersonRecord(string personId) // TODO 22
        {
            //********************************************************************************
            // TODO 22: Construct the SELECT statement to retrieve the movie person record   *
            //          of the cast member or director identified by his/her person id.      *
            //********************************************************************************
            sql = $"select * from MoviePerson where personId = {personId};";
            return queryResult = myOracleDBAccess.GetData("TODO 22", sql);
        }

        public DataTable GetCastMemberFilmography(string personId) // TODO 23
        {
            //********************************************************************************
            // TODO 23: Construct the SELECT statement to retrieve the movie id, title,      *
            //          release year, role the cast member played in the movie and academy   *
            //          award received for the role, if any, for all the movies in which a   *
            //          cast member, identified by his/her person id, appeared. Order the    *
            //          result first by release year descending, then by title ascending.    *
            //********************************************************************************
            sql = $"select Movie.movieId, title, role, releaseYear, awardName from Movie join CastMemberAppearsIn on Movie.movieId = CastMemberAppearsIn.movieId left join AwardWin on CastMemberAppearsIn.personId = AwardWin.personId join AcademyAward on AcademyAward.awardId = AwardWin.awardId where CastMemberAppearsIn.personId = {personId} order by releaseYear desc, title asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 23", sql);
        }

        public DataTable GetDirectorFilmography(string personId) // TODO 24
        {
            //********************************************************************************
            // TODO 24: Construct the SELECT statement to retrieve the movie id, title,      *
            //          release year and directing academy award received for the movie, if  *
            //          any, for all the movies directed by a director identified by his/her *
            //          person id. Order the result first by release year descending, then   *
            //          by title ascending.                                                  *
            //********************************************************************************
            sql = $"select Movie.movieId, title, releaseYear, awardName from Movie join DirectorOf on Movie.movieId = DirectorOf.movieId left join AwardWin on DirectorOf.personId = AwardWin.personId join AcademyAward on AcademyAward.awardId = AwardWin.awardId where DirectorOf.personId = {personId} order by releaseYear desc, title asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 24", sql);
        }

        public bool AddMoviePersonRecord(string personId, string name, string biography,
            string gender, string birthdate, string deathdate) // TODO 25
        {
            //********************************************************************************
            // TODO 25: Construct the INSERT statement to add a movie person record.         *
            //********************************************************************************
            sql = $"insert into MoviePerson values ({personId}, '{name}', '{biography}', '{gender}', '{birthdate}', '{deathdate}')";
            return updateResult = myOracleDBAccess.SetData("TODO 25", sql);
        }

        public bool ModifyMoviePersonRecord(string personId, string name, string biography,
            string gender, string birthdate, string deathdate) // TODO 26
        {
            //********************************************************************************
            // TODO 26: Construct the UPDATE statement to update a movie person record       *
            //          identified by a person id.                                           *
            //********************************************************************************
            sql = $"update MoviePerson set name = '{name}', biography = '{biography}', gender = '{gender}', birthdate = '{birthdate}', deathdate = '{deathdate}' where personId = {personId}";
            return updateResult = myOracleDBAccess.SetData("TODO 26", sql);
        }

        public bool AddAcademyAwardForMoviePersonForMovie(string movieId, string personId, string awardId) // TODO 27
        {
            //********************************************************************************
            // TODO 27: Construct the INSERT statement to add, for a movie identified by its *
            //          movie id and a cast member/director identified by his/her person id, *
            //          an academy award identified by its award id.                         *
            //********************************************************************************
            sql = $"insert into AwardWin values ({movieId}, {personId}, {awardId});";
            return updateResult = myOracleDBAccess.SetData("TODO 27", sql);
        }

        public bool AddMovieCastMember(string movieId, string personId, string role, OracleTransaction trans) // TODO 28
        {
            //********************************************************************************
            // TODO 28: Construct the INSERT statement to add a cast member, identified by   *
            //          his/her person id, playing a specified role (or roles) to a movie    *
            //          identified by its movie id.                                          *
            //********************************************************************************
            sql = $"insert into CastMemberAppearsIn values ({movieId}, {personId}, '{role}');";
            return updateResult = myOracleDBAccess.SetData("TODO 28", sql, trans);
        }

        public bool RemoveMovieCastMember(string movieId, string personId, OracleTransaction trans) // TODO 29
        {
            //********************************************************************************
            // TODO 29: Construct the DELETE statement to remove a cast member, identified   *
            //          by his/her person id, from a movie identified by its movie id.       *
            //********************************************************************************
            sql = $"delete from CastMemberAppearsIn where movieId = {movieId} and personId = {personId};";
            return updateResult = myOracleDBAccess.SetData("TODO 29", sql, trans);
        }

        public bool AddMovieDirector(string movieId, string personId, OracleTransaction trans) // TODO 30
        {
            //********************************************************************************
            // TODO 30: Construct the INSERT statement to add a director, identified by      *
            //          his/her person id, for a movie identified by its movie id.           *
            //********************************************************************************
            sql = $"insert into DirectorOf values ({movieId}, {personId});";
            return updateResult = myOracleDBAccess.SetData("TODO 30", sql, trans);
        }

        public bool RemoveMovieDirector(string movieId, string personId, OracleTransaction trans) // TODO 31
        {
            //********************************************************************************
            // TODO 31: Construct the DELETE statement to remove a director, identified by   *
            //          his/her person id, for a movie identified by its movie id.           *
            //********************************************************************************
            sql = $"delete from DirectorOf where movieId = {movieId} and personId = {personId});";
            return updateResult = myOracleDBAccess.SetData("TODO 31", sql, trans);
        }

        public bool ChangeMovieCastMemberRole(string movieId, string personId, string role, OracleTransaction trans) // TODO 32
        {
            //********************************************************************************
            // TODO 32: Construct the UPDATE statement to update the role played by a cast   *
            //          member, identified by his/her person id, who appeared in a movie     *
            //          identified by its movie id.                                          *
            //********************************************************************************
            sql = $"update CastMemberAppearsIn set role = '{role}' where movieId = {movieId} and personId = {personId}";
            return updateResult = myOracleDBAccess.SetData("TODO 32", sql, trans);
        }

        #endregion Movie Person SQL Statements

        #region ReelflicsMember SQL Statements

        public DataTable GetMemberAccountRecord(string username) // TODO 33
        {
            //********************************************************************************
            // TODO 33: Construct the SELECT statement to retrieve the record of a Reelflics *
            //          member identified by his/her username.                               *
            //********************************************************************************
            sql = $"select * from ReelflicsMember where username = '{username}';";
            return queryResult = myOracleDBAccess.GetData("TODO 33", sql);
        }

        public bool UpdateMemberAccountRecord(string username, string firstName, string lastName, string occupation,
                                              string email, string gender, string birthdate, string phoneNumber,
                                              string educationLevel, string cardHolderName, string cardNumber,
                                              string cardType, string securityCode, string expiryMonth,
                                              string expiryYear) // TODO 34
        {
            //********************************************************************************
            // TODO 34: Construct the UPDATE statement to update the record of a Reelflics   *
            //          member identified by his/her username.                               *
            //********************************************************************************
            sql = $"update ReelflicsMember set firstName = '{firstName}', lastName = '{lastName}', occupation = '{occupation}', email = '{email}', gender = '{gender}', birthdate = '{birthdate}', phoneNumber = '{phoneNumber}', educationLevel = '{educationLevel}', cardHolderName = '{cardHolderName}', cardNumber = '{cardNumber}', cardType = '{cardType}', securityCode = '{securityCode}', expiryMonth = '{expiryMonth}', expiryYear = '{expiryYear}' where username = '{username}';";
            return updateResult = myOracleDBAccess.SetData("TODO 34", sql);
        }

        public DataTable GetMemberWatchHistory(string username) // TODO 35
        {
            //********************************************************************************
            // TODO 35: Construct the SELECT statement to retrieve the movie id, watch date, *
            //          title, release year, running time and MPAA rating of the watch       *
            //          history of a member identified by his/her username.                  *
            //          Order the result by watch date descending.                           *
            //********************************************************************************
            sql = $"select WatchHistory.movieId, watchDate, title, releaseYear, runningTime, MPAARating from WatchHistory join Movie on WatchHistory.movieId = Movie.movieId where username = '{username}' order by watchDate desc;";
            return queryResult = myOracleDBAccess.GetData("TODO 35", sql);
        }

        public bool AddRecordToMemberWatchHistory(string movieId, string username, string watchDate) // TODO 36
        {
            //********************************************************************************
            // TODO 36: Construct the INSERT statement to add a movie, identified by its     *
            //          movie id, to the watch history of a member identified by his/her     *
            //          username.                                                            *
            // Note: the watch date must be formatted as 'dd-mon-yyyy hh24:mi' for insertion.*
            //********************************************************************************
            sql = $"insert into WatchHistory values ({movieId}, '{username}', '{watchDate}')";
            return updateResult = myOracleDBAccess.SetData("TODO 36", sql);
        }

        public DataTable GetMemberMovieReviewRecord(string movieId, string username) // TODO 37
        {
            //********************************************************************************
            // TODO 37: Construct the SELECT statement to retrieve the title, rating and     *
            //          review text of a review for a movie, identified by its movie id,     *
            //          submitted by a member identified by his/her username.                *
            //********************************************************************************
            sql = $"select title, rating, reviewText from Review where movieId = {movieId} and username = {username};";
            return queryResult = myOracleDBAccess.GetData("TODO 37", sql);
        }

        public bool CreateMemberMovieReviewRecord(string movieId, string username, string title,
            string rating, string reviewText, string reviewdate) // TODO 38
        {
            //********************************************************************************
            // TODO 38: Construct the INSERT statement to add a review for a movie,          *
            //          identified by its movie id, submitted by a member identified by      *
            //          his/her username.                                                    *
            //********************************************************************************
            sql = $"insert into Review values ({movieId}, '{username}', '{title}', {rating}, '{reviewText}', '{reviewdate}')";
            return updateResult = myOracleDBAccess.SetData("TODO 38", sql);
        }

        public bool ModifyMemberMovieReviewRecord(string movieId, string username, string title,
            string rating, string reviewText) // TODO 39
        {
            //********************************************************************************
            // TODO 39: Construct the UPDATE statement to change a review for a movie,       *
            //          identified by its movie id, submitted by a member identified by      *
            //          his/her username.                                                    *
            //********************************************************************************
            sql = $"update Review set title = '{title}', rating = '{rating}', reviewText = '{reviewText}' where movieId = {movieId} and username = '{username}';";
            return updateResult = myOracleDBAccess.SetData("TODO 39", sql);
        }

        #endregion ReelflicsMember SQL Statements

        #region Report SQL Statements

        public DataTable GetMembershipStatisticsReport() // TODO 40
        {
            //********************************************************************************
            // TODO 40: Construct the SELECT statement to retrieve the total number of       *
            //          members, number of male members and number of female members.        *
            //********************************************************************************
            sql = $"select count(*), sum(case gender when 'M' then 1 else 0 end), sum(case gender when 'F' then 1 else 0 end) from ReelflicsMember;";
            return queryResult = myOracleDBAccess.GetData("TODO 40", sql);
        }

        public DataTable GetEducationLevelReport() // TODO 41
        {
            //********************************************************************************
            // TODO 41: Construct the SELECT statement to retrieve, for each education       *
            //          level, the number of members that have that education level.         *
            //          Order the result by the number at each level ascending.              * 
            //********************************************************************************
            sql = $"select count(*) from ReelflicsMember group by educationLevel, count(*) order by count(*) asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 41", sql);
        }

        public DataTable GetGenreViewingReport(string username) // TODO 42
        {
            //********************************************************************************
            // TODO 42: Construct the SELECT statement to retrieve, for each genre in the    *
            //          database, the number of times a member, identified by his/her        *
            //          username, has watched movies that have that genre. Every genre of a  *
            //          movie should be counted in the result, each movie should be counted  *
            //          only once even if watched multiple times by a member and the result  *
            //          for a genre should be zero if the member has not watched a movie     *
            //          that has that genre. Order the result by genre ascending.            * 
            //********************************************************************************
            sql = $"select count(distinct genre) from MovieGenre join WatchHistory on MovieGenre.movieId = WatchHistory.movieId where username = {username} group by genre, count(distinct genre) order by genre asc;";
            return queryResult = myOracleDBAccess.GetData("TODO 42", sql);
        }
        #endregion Report SQL Statements
    }
}