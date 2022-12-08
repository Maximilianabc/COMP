/* 
Name: LEUNG Ho Man Max
Student ID: 20611398
*/
CLEAR SCREEN;

/*
1. Find the genre name and the frequency of occurrence of the five most frequently occurring genres for only
those movies that members have watched. Order the result by frequency descending. [6 marks]
*/

SELECT genre, COUNT(*) AS Frequency FROM MovieGenre
WHERE EXISTS (SELECT movieId FROM WatchHistory WHERE MovieGenre.movieId = WatchHistory.movieId)
GROUP BY genre
ORDER BY COUNT(*) DESC
FETCH NEXT 5 ROWS ONLY;

/*
2. Find the cast member name, the role he/she played, the title and the release year of the movie in which the
cast member both appeared in and directed the movie, and the movie won the Best Picture Academy award.
Order the result by name ascending. [6 marks]
*/

SELECT "name", "role", title, releaseYear FROM Movie
JOIN CastMemberAppearsIn ON Movie.movieId = CastMemberAppearsIn.MovieId
JOIN MoviePerson ON CastMemberAppearsIn.personId = MoviePerson.personId
WHERE EXISTS (SELECT personId FROM DirectorOf WHERE MoviePerson.personId = DirectorOf.personId) 
AND bestPictureAwardId IS NOT NULL
ORDER BY "name" ASC;

/*
3. Find the title, release year and MPAA rating of the movies that won the Best Picture academy award and
whose director won the Best Director academy award for the same movie, but the movie has not been
watched by any member. Order the result by title ascending. [10 marks]
*/

SELECT DISTINCT title, releaseYear, MPAARating FROM Movie
JOIN AwardWin ON Movie.movieId = AwardWin.movieId
WHERE bestPictureAwardId IS NOT NULL
AND EXISTS (SELECT DISTINCT awardName FROM AcademyAward WHERE AcademyAward.awardName = 'Best Director')
AND NOT EXISTS (SELECT movieId FROM WatchHistory WHERE Movie.movieId = WatchHistory.movieId)
ORDER BY title ASC;

/*
4. Find the title, release year, running time, MPAA rating and the number of times watched for the movies
that have been watched the greatest number of times. Order the result by title ascending. [12 marks]
*/

SELECT title, releaseYear, runningTime, MPAARating, COUNT(*) AS timesWatched FROM Movie
JOIN WatchHistory ON Movie.movieId = WatchHistory.movieId
GROUP BY title, releaseYear, runningTime, MPAARating, WatchHistory.movieId
ORDER BY title ASC;

/*
5. Find the title, MPAA rating, IMDB rating, Reelflics rating, number of times watched by different female
members and number of times watched by different male members for the movies that won the Best Picture
academy award and that have been watched more times by different female members than by different male
members. Order the result first by the number of times watched by different female members descending,
then by the number of times watched by different male members descending, then by title ascending. If a
movie has not been watched, then the number of times watched should be shown as zero not as null or as a
blank. [16 marks]
*/
SELECT * FROM
(
    SELECT 
        Movie.title, 
        MPAARating, 
        IMDBRating, 
        AVG(rating) AS reelflicsRating, 
        SUM(CASE WHEN gender = 'M' THEN 1 ELSE 0 END) AS timesWatchedByMale,
        SUM(CASE WHEN gender = 'F' THEN 1 ELSE 0 END) AS timesWatchedByFemale
    FROM Movie
    JOIN Review ON Review.movieId = Movie.movieId
    JOIN ReelflicsMember ON ReelflicsMember.userName = Review.userName
    WHERE bestPictureAwardId IS NOT NULL
    GROUP BY Movie.title, MPAARating, IMDBRating
)
WHERE timesWatchedByFemale > timesWatchedByMale
ORDER BY timesWatchedByFemale DESC, timesWatchedByMale DESC, title ASC;