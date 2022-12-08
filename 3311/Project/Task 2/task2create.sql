/* 
Name: LEUNG Ho Man Max
Student ID: 20611398
*/
CLEAR SCREEN;

DROP TABLE Review;
DROP TABLE WatchHistory;
DROP TABLE Watchlist;
DROP TABLE AwardWin;
DROP TABLE CastMemberAppearsIn;
DROP TABLE DirectorOf;
DROP TABLE MovieGenre;
DROP TABLE MoviePerson;
DROP TABLE AcademyAward;
DROP TABLE Movie;
DROP TABLE ReelflicsMember;

CREATE TABLE ReelflicsMember
(
    username CHAR(10) PRIMARY KEY CHECK(REGEXP_LIKE(username, '[a-z]{6,10}')), 
    pseudonym VARCHAR2(20) UNIQUE NOT NULL, 
    firstName VARCHAR2(15) NOT NULL, 
    lastName VARCHAR2(20) NOT NULL,
    occupation VARCHAR2(25) NOT NULL, 
    email VARCHAR2(25) UNIQUE NOT NULL, 
    gender CHAR(1) NOT NULL CHECK(gender IN ('M', 'F')), 
    birthdate DATE NOT NULL, 
    phoneNumber CHAR(8) NOT NULL CHECK(REGEXP_LIKE(phoneNumber, '\d{8}')),
    educationLevel VARCHAR2(13) NOT NULL CHECK(educationLevel IN ('none', 'primary', 'secondary', 'tertiary', 'post tertiary')), 
    cardholderName VARCHAR2(35) NOT NULL, 
    cardNumber VARCHAR2(16) NOT NULL CHECK(REGEXP_LIKE(cardNumber, '\d{15,16}')), 
    cardType VARCHAR2(16) NOT NULL CHECK(cardType IN ('American Express', 'MasterCard', 'UnionPay', 'Visa')),
    securityCode VARCHAR2(4) NOT NULL CHECK(REGEXP_LIKE(securityCode, '\d{3,4}')), 
    expiryMonth CHAR(2) NOT NULL CHECK(REGEXP_LIKE(expiryMonth, '\d{2}')), 
    expiryYear CHAR(4) NOT NULL CHECK(REGEXP_LIKE(expiryYear, '\d{4}'))
);

CREATE TABLE Movie
(
    movieId SMALLINT PRIMARY KEY, 
    title VARCHAR2(70), 
    synopsis VARCHAR2(300), 
    releaseYear CHAR(4), 
    runningTime SMALLINT,
    MPAARating VARCHAR2(10) DEFAULT 'Not Rated' CHECK(MPAARating IN ('Approved', 'G', 'Passed', 'PG', 'PG-13', 'Not Rated', 'R')), 
    IMDBRating DECIMAL(3,1) CHECK(IMDBRating >= 1 AND IMDBRating <= 10), 
    bestPictureAwardId SMALLINT
);

CREATE TABLE AcademyAward
(
    awardId SMALLINT PRIMARY KEY, 
    awardName VARCHAR2(25)
);

CREATE TABLE MoviePerson
(
    personId SMALLINT PRIMARY KEY, 
    "name" VARCHAR2(35), 
    biography VARCHAR2(500), 
    gender CHAR(1), 
    birthdate DATE, 
    deathdate DATE
);

CREATE TABLE MovieGenre
(
    movieId SMALLINT REFERENCES Movie(movieId) ON DELETE CASCADE, 
    genre VARCHAR2(15),
    PRIMARY KEY (movieId, genre)
);

CREATE TABLE DirectorOf 
(
    movieId SMALLINT REFERENCES Movie(movieId) ON DELETE CASCADE,
    personId SMALLINT REFERENCES MoviePerson(personId) ON DELETE CASCADE,
    PRIMARY KEY (movieId, personId)
);

CREATE TABLE CastMemberAppearsIn
(
    movieId SMALLINT REFERENCES Movie(movieId) ON DELETE CASCADE, 
    personId SMALLINT REFERENCES MoviePerson(personId) ON DELETE CASCADE, 
    "role" VARCHAR2(100),
    PRIMARY KEY (movieId, personId)
);

CREATE TABLE AwardWin
(
    movieId SMALLINT REFERENCES Movie(movieId) ON DELETE CASCADE, 
    personId SMALLINT REFERENCES MoviePerson(personId) ON DELETE CASCADE, 
    awardId SMALLINT REFERENCES AcademyAward(awardId) ON DELETE CASCADE,
    PRIMARY KEY (movieId, personId, awardId)
);

CREATE TABLE Watchlist
(
    movieId SMALLINT REFERENCES Movie(movieId), 
    username CHAR(10) REFERENCES ReelflicsMember(username) ON DELETE CASCADE,
    PRIMARY KEY (movieId, username)
);

CREATE TABLE WatchHistory
(
    movieId SMALLINT REFERENCES Movie(movieId) ON DELETE CASCADE,
    username CHAR(10) REFERENCES ReelflicsMember(username) ON DELETE CASCADE, 
    watchDate DATE,
    PRIMARY KEY (movieId, username, watchDate)
);

CREATE TABLE Review
(
    movieId SMALLINT REFERENCES Movie(movieId) ON DELETE CASCADE, 
    username CHAR(10) REFERENCES ReelflicsMember(username) ON DELETE CASCADE, 
    title VARCHAR2(50), 
    rating SMALLINT CHECK (rating >= 1 AND rating <= 10), 
    reviewText VARCHAR2(500), 
    reviewDate DATE,
    PRIMARY KEY(movieId, username)
    /* cannot be done without using udf */
    /* CONSTRANT CK_REVIEW_ON_WATCHED_ONLY CHECK(movieId IN (SELECT movieId FROM WatchHistory WHERE Review.username = WatchHistory.username)) */
);
