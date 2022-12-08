using ReelflicsWebsite.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ReelflicsWebsite.Global;

namespace ReelflicsWebsite
{
    public partial class MovieInformation : Page
    {
        //******************************************************************************************************************
        // Uses TODO 05, TODO 06, TODO 07, TODO 08, TODO 09, TODO 10, TODO 11, TODO 12, TODO 13, TODO 14, TODO 15, TODO 35 *
        //******************************************************************************************************************

        private readonly ReelflicsDBAccess myReelflicsDB = new ReelflicsDBAccess();
        private readonly DBHelperMethods myDBHelpers = new DBHelperMethods();
        private readonly HelperMethods myHelpers = new HelperMethods();
        private readonly string username = HttpContext.Current.User.Identity.Name;
        private readonly string spacer5 = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        private readonly string separator = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style=\"font-size: larger; color: white; margin-top: 12px\">&#8226;</span> &nbsp;&nbsp;&nbsp;&nbsp; ";
        private readonly string hideReviews = "Reviews " + ((char)0x25B2).ToString();
        private readonly string showReviews = "Reviews " + ((char)0x25BC).ToString();
        private const string crossmark = "\u274c";
        private const string checkmark = "\u2713";
        private const string watchNow = "\u25B6 Watch Now";
        private const string continueWatching = "\u25B6 Continue Watching";
        private bool isInViewingHistory = false;
        private bool isMemberReviewed = false;
        private DataTable dtMovieAcademyAwards;
        private DataTable dtDirectors;
        private DataTable dtReviews;
        private DataTable dtCastMembers;

        /***** Private Methods *****/

        private bool GetCastRecords(string movieId) // Uses TODO 11
        {
            bool result = false;
            //******************************************************************************
            // Uses TODO 11 to get the person id, name and role of a movie's cast members. *
            //******************************************************************************
            DataTable dtCastMembers = myReelflicsDB.GetMovieCastMembers(movieId);

            // Return the cast member names if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 11",
                                             dtCastMembers,
                                             new List<string> { "PERSONID", "NAME", "ROLE" },
                                             lblErrorMessage))
            {
                // Save the actor records in ViewState to avoid retrieving them again when the page reloads.
                ViewState["dtCastMembers"] = dtCastMembers;
                PopulateCastMembers();
                result = true;
            }
            return result;
        }

        private bool GetDirectorRecords(string movieId) // Uses TODO 10
        {
            bool result = false;
            //*********************************************************************
            // Uses TODO 10 to get the person id and name of a movie's directors. *
            //*********************************************************************
            DataTable dtDirectors = myReelflicsDB.GetMovieDirectors(movieId);

            // Return the director names if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 10",
                                             dtDirectors,
                                             new List<string> { "PERSONID", "NAME" },
                                             lblErrorMessage))
            {
                // Save the director records in ViewState to avoid retrieving them again when the page reloads.
                ViewState["dtDirectors"] = dtDirectors;
                PopulateDirectors();
                result = true;
            }
            return result;
        }

        private bool GetMemberWatchlist(string movieId) // Uses TODO 12
        {
            bool result = false;
            //*****************************************************************************
            // Uses TODO 12 to get the movie ids of the movies on the member's watchlist. *
            //*****************************************************************************
            DataTable dtWatchlist = myReelflicsDB.GetWatchlist(username);

            // Show the member's watchlist if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 12",
                                             dtWatchlist,
                                             new List<string> { "MOVIEID", "TITLE", "RELEASEYEAR", "RUNNINGTIME", "MPAARATING" },
                                             lblErrorMessage))
            {
                pnlWatchlist.Visible = true;
                // Set the default watchlist button (i.e., the movie is not on the watchlist).
                btnWatchlist.Text = crossmark; // The button is a crossmark (X) symbol.
                btnWatchlist.ForeColor = System.Drawing.Color.Red;

                // Determine if the current movie is on the member's watchlist.
                for (int i = 0; i < dtWatchlist.Rows.Count; i++)
                {
                    if (dtWatchlist.Rows[i]["MOVIEID"].ToString() == movieId)
                    {
                        btnWatchlist.Text = checkmark; // The button is a checkmark (√) symbol.
                        btnWatchlist.ForeColor = System.Drawing.Color.Green;
                    }
                }
                result = true;
            }
            return result;
        }

        private bool GetMostRecentWatchDate(string movieId) // Uses TODO 15
        {
            bool result = false;
            //***********************************************************************
            // Uses TODO 15 to get the most recent watch date, if any, for a movie. *
            //***********************************************************************
            DataTable dtMostRecentWatchDate = myReelflicsDB.GetMostRecentWatchDate(movieId, username);

            // Show the most recent watch date if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 15",
                                             dtMostRecentWatchDate,
                                             new List<string> { "ANYNAME" },
                                             lblErrorMessage))
            {
                if (dtMostRecentWatchDate.Rows.Count == 1)
                {
                    btnWatchNow.Text = watchNow;
                    if (!string.IsNullOrEmpty(dtMostRecentWatchDate.Rows[0][0].ToString()))
                    {
                        litLastWatched.Text += ((DateTime)dtMostRecentWatchDate.Rows[0][0]).ToString("d MMMM yyyy HH:mm");
                        litLastWatched.Visible = true;
                        if (DateTime.Now < ((DateTime)dtMostRecentWatchDate.Rows[0][0]).AddMinutes(double.Parse(ViewState["runningTime"].ToString())))
                        { btnWatchNow.Text = continueWatching; }
                    }
                    result = true;
                }
                else if (dtMostRecentWatchDate.Rows.Count == 0) // No record was retrieved => SQL rerror.
                { btnWatchNow.Text = watchNow; result = true; }
                else // Multiple records were retrieved => SQL error.
                { myHelpers.DisplayMessage(lblErrorMessage, queryError + "TODO 15" + queryErrorMultipleRecordsRetrieved); }
            }
            return result;
        }

        private bool GetMovieAcademyAwards(string movieId) // Uses TODO 09
        {
            bool result = false;
            //*********************************************************************
            // Uses TODO 09 to get a movie's acting and directing academy awards. *
            //*********************************************************************
            dtMovieAcademyAwards = myReelflicsDB.GetMovieAcademyAwards(movieId);

            if (myHelpers.IsQueryResultValid("TODO 09",
                                             dtMovieAcademyAwards,
                                             new List<string> { "PERSONID", "AWARDNAME" },
                                             lblErrorMessage))
            {
                // Save the academy award records in ViewState to avoid retrieving them again when the page reloads.
                ViewState["dtAcademyAwards"] = dtMovieAcademyAwards;
                result = true;
            }
            return result;
        }

        private bool GetMovieGenres(string movieId) // Uses TODO 06
        {
            bool result = false;
            //****************************************
            // Uses TODO 06 to get a movie's genres. *
            //****************************************
            DataTable dtGenres = myReelflicsDB.GetMovieGenres(movieId);

            // Show the movie's genre information if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 06",
                                             dtGenres,
                                             new List<string> { "MOVIEID", "GENRE" },
                                             lblErrorMessage))
            {
                if (dtGenres.Rows.Count != 0)
                {
                    // Concatenate the genres into a single string.
                    litGenre.Text = separator + StringExtension.DataTableToCommaSeparatedText(dtGenres, "GENRE");
                    result = true;
                }
                else { myHelpers.DisplayMessage(lblErrorMessage, dbqueryError + "TODO 06" + dbqueryErrorNoRecordsRetrieved); }
            }
            return result;
        }

        private void GetMovieRecord(string movieId) // Uses TODO 05
        {
            //****************************************
            // Uses TODO 05 to get a movie's record. *
            //****************************************
            DataTable dtMovie = myReelflicsDB.GetMovieRecord(movieId);

            // Show the movie information if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 05",
                                             dtMovie,
                                             new List<string> { "MOVIEID", "TITLE", "SYNOPSIS", "RUNNINGTIME", "RELEASEYEAR", "MPAARATING", "IMDBRATING", "BESTPICTUREAWARDID" },
                                             lblErrorMessage))
            {
                if (dtMovie.Rows.Count == 1) // Only one record should be retrieved.
                {
                    pnlMovieInformation.Visible = true;
                    // Create the movie's image poster filename from the title.
                    imgPoster.ImageUrl += movieId + StringExtension.CreateFileName(dtMovie.Rows[0]["TITLE"].ToString());
                    Session["movieTitle"] = litTitle.Text = HttpUtility.HtmlDecode(dtMovie.Rows[0]["TITLE"].ToString());
                    litReleaseYear.Text = dtMovie.Rows[0]["RELEASEYEAR"].ToString();
                    litMPAARating.Text = separator + dtMovie.Rows[0]["MPAARATING"].ToString();
                    ViewState["runningTime"] = dtMovie.Rows[0]["RUNNINGTIME"].ToString();
                    litRunningTime.Text = separator + TimeSpan.FromMinutes(Convert.ToDouble(dtMovie.Rows[0]["RUNNINGTIME"])).ToString(@"h\:mm");
                    litIMDBRating.Text += spacer5 + "<span style=\"color: crimson\">" + dtMovie.Rows[0]["IMDBRATING"].ToString() + "</span>";
                    litSynopsis.Text = HttpUtility.HtmlDecode(dtMovie.Rows[0]["SYNOPSIS"].ToString());
                    if (!string.IsNullOrEmpty(dtMovie.Rows[0]["BESTPICTUREAWARDID"].ToString())) { imgBestPicture.Visible = litBestPicture.Visible = true; }

                    // Abort if an SQL error occurs in any of the following methods.
                    if (!GetMovieGenres(movieId)) { return; } // Uses TODO 06
                    if (!GetMovieAcademyAwards(movieId)) { return; } // Uses TODO 09
                    if (!GetDirectorRecords(movieId)) { return; } // Uses TODO 10
                    if (!GetCastRecords(movieId)) { return; } // Uses TODO 11

                    if (myDBHelpers.IsUserReelflicsMember(username))
                    {
                        if (!GetReviewRecords(movieId)) { return; } // Uses TODO 07
                        if (!GetViewingHistory(movieId)) { return; } // Uses TODO 35
                        if (!GetReelflicsRating(movieId)) { return; } // Uses TODO 08
                        if (!GetMemberWatchlist(movieId)) { return; } // Uses TODO 12
                        if (!GetMostRecentWatchDate(movieId)) { return; } // Uses TODO 15
                        pnlWatchNow.Visible = true;
                    }
                }
                else
                {
                    if (dtMovie.Rows.Count > 1) // Multiple records were retrieved.
                    { myHelpers.DisplayMessage(lblErrorMessage, queryError + "TODO 05" + queryErrorMultipleRecordsRetrieved); }
                    else // No record was retrieved.
                    { myHelpers.DisplayMessage(lblErrorMessage, dbqueryError + "TODO 05" + dbqueryErrorNoRecordsRetrieved); }
                }
            }
        }

        private bool GetReelflicsRating(string movieId) // Uses TODO 08
        {
            bool result = false;
            //***************************************************************
            // Uses TODO 08 to get the members' average rating for a movie. *
            //***************************************************************
            decimal rating = myReelflicsDB.GetReelflicsMovieRating(movieId);
            if (rating != -1)
            {
                litReelflicsRating.Text = separator + "Reelflics Rating" + spacer5;
                if (rating != 0) // The movie has been reviewed, so show the rating and the reviews button.
                {
                    litReelflicsRating.Text += "<span style=\"color: crimson\">" + Math.Round(rating, 1) + "</span>" + spacer5;
                    btnReviews.Text = showReviews;
                    btnReviews.Visible = true;
                    if (isInViewingHistory) // Member has watched the movie.
                    {
                        if (isMemberReviewed) // Member has reviewed the movie, so show "Edit your review" hyperlink..
                        {
                            hlCreateEditReview.NavigateUrl = "~/Member/ModifyReview.aspx?movieId=" + movieId;
                            hlCreateEditReview.Text = "Edit your review";
                            hlCreateEditReview.Visible = true;
                        }
                        else // Member has not reviewed the movie, so show "Review this movie" hyperlink.
                        {
                            hlCreateEditReview.NavigateUrl = "~/Member/CreateReview.aspx?movieId=" + movieId;
                            hlCreateEditReview.Text = "Review this movie";
                            hlCreateEditReview.Visible = true;
                        }
                    }
                }
                else // The movie has not been reviewed.
                {
                    if (isInViewingHistory) // If the member has watched the movie, show "Be the first to review this movie" hyperlink.
                    {
                        hlCreateEditReview.NavigateUrl = "~/Member/CreateReview.aspx?movieId=" + movieId;
                        hlCreateEditReview.Text = "Be the first to review this movie";
                        hlCreateEditReview.Visible = true;
                    }
                    else { litReelflicsRating.Text += "<span style=\"color: crimson\">Not yet reviewed</span>"; }
                }
                litReelflicsRating.Visible = true;
                result = true;
            }
            else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.
            return result;
        }

        private bool GetReviewRecords(string movieId) // Uses TODO 07
        {
            bool result = false;
            //************************************************
            // Uses TODO 07 to get a movie's review records. *
            //************************************************
            dtReviews = myReelflicsDB.GetMovieReviews(movieId);

            // Show the review information if the query result is valid.
            if (myHelpers.IsQueryResultValid("TODO 07",
                                             dtReviews,
                                             new List<string> { "MOVIEID", "USERNAME", "TITLE", "RATING", "REVIEWTEXT", "REVIEWDATE", "PSEUDONYM" },
                                             lblErrorMessage))
            {
                // Determine if the logged in member has submitted a review for this movie.
                foreach (DataRow row in dtReviews.Rows)
                {
                    if (row["USERNAME"].ToString().Trim() == username) { isMemberReviewed = true; break; }
                }

                // Save the review records in ViewState to avoid retrieving them again when the page reloads.
                ViewState["dtReviews"] = dtReviews;
                result = true;
            }
            return result;
        }

        private bool GetViewingHistory(string movieId) // Uses TODO 35
        {
            bool result = false;
            //*************************************************************************************
            // Uses TODO 35 to get the movie id and watch date of the movies watched by a member. *
            //*************************************************************************************
            DataTable dtViewingHistory = myReelflicsDB.GetMemberWatchHistory(username);

            if (myHelpers.IsQueryResultValid("TODO 35",
                                             dtViewingHistory,
                                             new List<string> { "MOVIEID", "TITLE", "WATCHDATE", "RELEASEYEAR", "RUNNINGTIME", "MPAARATING" },
                                             lblErrorMessage))
            {
                if (dtViewingHistory.Rows.Count != 0)
                {
                    // Determine if the member has watched the movie.
                    foreach (DataRow row in dtViewingHistory.Rows)
                    {
                        if (row["MOVIEID"].ToString().Trim() == movieId) { isInViewingHistory = true; break; }
                    }
                }
                result = true;
            }
            return result;
        }

        private string PopulateAcademyAwards(string personId, AwardType awardType)
        {
            string result = "";
            dtMovieAcademyAwards = ViewState["dtAcademyAwards"] as DataTable;
            if (dtMovieAcademyAwards != null)
            {
                for (int i = 0; i < dtMovieAcademyAwards.Rows.Count; i++)
                {
                    if (dtMovieAcademyAwards.Rows[i]["PERSONID"].ToString() == personId)
                    {
                        switch (awardType)
                        {
                            case AwardType.Directing: // Determine if the person won a directing award.
                                if (dtMovieAcademyAwards.Rows[i]["AWARDNAME"].ToString() == directingAward)
                                {
                                    result += "<span style =\"font-size: smaller; color: goldenrod\">&nbsp;("
                                              + dtMovieAcademyAwards.Rows[i]["AWARDNAME"].ToString().Replace(" ", "&nbsp;")
                                              + ")</span>";
                                }
                                break;
                            case AwardType.Acting: // Determine if the person won an acting award.
                                if (dtMovieAcademyAwards.Rows[i]["AWARDNAME"].ToString() != directingAward)
                                {
                                    result += "<span style =\"font-size: smaller; color: goldenrod\">&nbsp;("
                                              + dtMovieAcademyAwards.Rows[i]["AWARDNAME"].ToString().Replace(" ", "&nbsp;")
                                              + ")</span>";
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                myHelpers.DisplayMessage(lblErrorMessage, nullDataTableErrorMessage
                                                          + "dtAcademmyAwards in PopulateAcademyAwards()."
                                                          + contact3311rep);
            }
            return result;
        }

        private void PopulateDirectors()
        {
            // Get the director records from ViewState. 
            dtDirectors = ViewState["dtDirectors"] as DataTable;
            if (dtDirectors != null)
            {
                // Concatenate the director names into a single string for display.
                for (int i = 0; i < dtDirectors.Rows.Count; i++)
                {
                    pnlDirectors.Visible = true;
                    HyperLink hlDirectors = new HyperLink
                    {
                        Text = HttpUtility.HtmlDecode(dtDirectors.Rows[i]["NAME"].ToString()).Replace(" ", "&nbsp;"),
                        NavigateUrl = "~/Shared/MoviePersonInformation.aspx?personId=" + dtDirectors.Rows[i]["PERSONID"].ToString()
                    };
                    phDirectors.Controls.Add(hlDirectors);

                    // Get the directing award, if any.
                    Literal litDirectingAward = new Literal
                    {
                        Text = PopulateAcademyAwards(dtDirectors.Rows[i]["PERSONID"].ToString(),
                                                                                           AwardType.Directing)
                    };
                    phDirectors.Controls.Add(litDirectingAward);

                    if (i < dtDirectors.Rows.Count - 1)
                    {
                        Literal litSeparator = new Literal { Text = separator };
                        phDirectors.Controls.Add(litSeparator);
                    }
                }
            }
            else { myHelpers.DisplayMessage(lblErrorMessage, nullDataTableErrorMessage + "dtDirectors in PopulateDirectors()." + contact3311rep); }
        }

        private bool PopulateReviews()
        {
            bool result = false;
            // Get the review records from ViewState.
            dtReviews = ViewState["dtReviews"] as DataTable;

            if (dtReviews != null)
            {
                for (int i = 0; i < dtReviews.Rows.Count; i++)
                {
                    // Construct the review for display.
                    string review = "<div class=\"col-xs-12\"><p style=\"color: crimson\"><span style=\"color: gold\">&#9733;</span>&nbsp;"
                        + dtReviews.Rows[i]["RATING"].ToString() + "/10</p></div>";
                    review += "<div class=\"col-xs-12\"><span style=\"font-size: larger; color: white\">"
                              + dtReviews.Rows[i]["TITLE"].ToString()
                              + "</span></div>";
                    review += "<div class=\"col-xs-12\"><span style=\"font-size: x-small\">"
                              + dtReviews.Rows[i]["PSEUDONYM"].ToString()
                              + "&nbsp;&nbsp;&nbsp;"
                              + ((DateTime)dtReviews.Rows[i]["REVIEWDATE"]).ToString("d MMMMM yyyy")
                              + "</span></div>";
                    review += "<div class=\"col-xs-12\"><p>"
                              + dtReviews.Rows[i]["REVIEWTEXT"].ToString();

                    // Determine if this is the last review.
                    if (i != dtReviews.Rows.Count - 1) { review += "<hr /></p></div>"; }
                    else { review += "</p></div>"; } // This is the last review.

                    Literal litReview = new Literal { Text = HttpUtility.HtmlDecode(review) };
                    phReviews.Controls.Add(litReview);
                }
                result = true;
            }
            else
            {
                myHelpers.DisplayMessage(lblErrorMessage, nullDataTableErrorMessage
                                                          + "dtReviews in PopulateReviews()."
                                                          + contact3311rep);
            }
            return result;
        }

        private void PopulateCastMembers()
        {
            // Get the cast member records from ViewState;
            dtCastMembers = ViewState["dtCastMembers"] as DataTable;
            if (dtCastMembers != null)
            {
                // Concatenate the actor names into a single string for display.
                for (int i = 0; i < dtCastMembers.Rows.Count; i++)
                {
                    pnlCast.Visible = true;
                    HyperLink hlCast = new HyperLink
                    {
                        Text = HttpUtility.HtmlDecode(dtCastMembers.Rows[i]["NAME"].ToString()).Replace(" ", "&nbsp;"),
                        NavigateUrl = "~/Shared/MoviePersonInformation.aspx?personId=" + dtCastMembers.Rows[i]["PERSONID"].ToString()
                    };
                    phCast.Controls.Add(hlCast);

                    // Add the role.
                    Literal litRole = new Literal
                    {
                        Text = "<span style=\"color: ghostwhite; font-weight: lighter; font-size: smaller\"> ("
                                + dtCastMembers.Rows[i]["ROLE"].ToString().Replace(" ", "&nbsp;") + ")</span>"
                    };
                    phCast.Controls.Add(litRole);

                    // Add the acting award, if any.
                    Literal litActingAward = new Literal { Text = PopulateAcademyAwards(dtCastMembers.Rows[i]["PERSONID"].ToString(), AwardType.Acting) };
                    phCast.Controls.Add(litActingAward);

                    if (i < dtCastMembers.Rows.Count - 1)
                    {
                        Literal litSeparator = new Literal { Text = separator };
                        phCast.Controls.Add(litSeparator);
                    }
                }
            }
            else
            {
                myHelpers.DisplayMessage(lblErrorMessage, nullDataTableErrorMessage
                                                          + "dtCastMembers in PopulateCastMembers()."
                                                          + contact3311rep);
            }
        }

        /***** Protected Methods *****/

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["movieId"]))
                { GetMovieRecord(Request.QueryString["movieId"]); }
                else { Response.Redirect("~/Default.aspx"); }
            }
            else
            {
                PopulateDirectors();
                PopulateCastMembers();
            }
        }

        protected void BtnReviews_Click(object sender, EventArgs e)
        {
            if (btnReviews.Text == showReviews) // If reviews are hidden (with down pointing triangle on Reviews button), then show them.
            {
                if (PopulateReviews())
                {
                    pnlReviews.Visible = true;
                    btnReviews.Text = hideReviews; // Place up pointing triangle on Reviews button.
                }
            }
            else // Hide reviews (with down pointing triangle on Reviews button).
            {
                pnlReviews.Visible = false;
                btnReviews.Text = showReviews; // Place down pointing triangle on Reviews button.
            }
        }

        protected void BtnWatchlist_Click(object sender, EventArgs e) // Uses TODO 13, TODO 14
        {
            if (btnWatchlist.Text == crossmark)
            {
                //*******************************************************
                // Uses TODO 13 to add a movie to a member's watchlist. *
                //*******************************************************
                if (myReelflicsDB.AddToWatchlist(Request.QueryString["movieId"], username))
                {
                    btnWatchlist.Text = checkmark;
                    btnWatchlist.ForeColor = System.Drawing.Color.Green;
                }
                else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.
            }
            else
            {
                //************************************************************
                // Uses TODO 14 to remove a movie from a member's watchlist. *
                //************************************************************
                if (myReelflicsDB.RemoveFromWatchlist(Request.QueryString["movieId"], username))
                {
                    btnWatchlist.Text = crossmark;
                    btnWatchlist.ForeColor = System.Drawing.Color.Red;
                }
                else { myHelpers.DisplayMessage(lblErrorMessage, sqlErrorMessage); } // An SQL error occurred.
            }

            if (btnReviews.Text == hideReviews)
            {
                if (!PopulateReviews()) { return; }
            }
        }

        protected void BtnWatchNow_Click(object sender, EventArgs e)
        {
            if (btnWatchNow.Text == watchNow)
            { Response.Redirect("~/Member/WatchNow.aspx?movieId=" + Request.QueryString["movieId"]); }
            else { Response.Redirect("~/Member/WatchNow.aspx"); }
        }
    }
}