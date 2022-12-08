/* COMP 3311: Task 2 - Reelflics Movie Streaming Management System: Schema Check */

clear screen

DELETE FROM Review;
DELETE FROM WatchHistory;
DELETE FROM Watchlist;
DELETE FROM AwardWin;
DELETE FROM CastMemberAppearsIn;
DELETE FROM DirectorOf;
DELETE FROM MovieGenre;
DELETE FROM MoviePerson;
DELETE FROM AcademyAward;
DELETE FROM Movie;
DELETE FROM ReelflicsMember;

/********** AcademyAward relation **********/
insert into AcademyAward values (1,'Best Picture');
insert into AcademyAward values (2,'Best Director');

/********** Movie relation **********/
insert into Movie values (1000,'movie title','movie synopsis','2022',120,'PG',10,1);
insert into Movie values (1001,'movie title2','movie synopsis2','2022',120,'PG',10,1);

/********** MoviePerson relation **********/
insert into MoviePerson values (999,'name','biography','M','01-JAN-2000',null);
insert into MoviePerson values (998,'name2','biography','M','01-JAN-2000',null);

/********** AwardWin relation **********/
insert into AwardWin values (1000,999,1);
insert into AwardWin values (1000,998,2);

/********** CastMemberAppearsIn relation  **********/
insert into CastMemberAppearsIn values (1000,999,'role');
insert into CastMemberAppearsIn values (1000,998,'role');

/********** DirectorOf relation **********/
insert into DirectorOf values (1000,999);
insert into DirectorOf values (1000,998);

/********** MovieGenre relation **********/
insert into MovieGenre values (1000,'drama');
insert into MovieGenre values (1001,'drama');

/********** ReelflicsMember relation **********/
insert into ReelflicsMember values ('testuser','pseudonym','first','last','student','testuser@nomail.com','F','01-JAN-2000','00000000','none','Cardholder','000000000000000','Visa','000','01','2025');
insert into ReelflicsMember values ('testuser2','pseudonym2','first','last','student','testuser2@nomail.com','F','01-JAN-2000','00000001','none','Cardholder1','000000000000001','Visa','000','01','2025');

/********** Review relation **********/
insert into Review values (1001,'testuser','review title',5,'review text','01-JAN-2021');
insert into Review values (1001,'testuser2','review title',4,'review text','01-JAN-2021');

/********** WatchHistory relation **********/
insert into WatchHistory values (1001,'testuser','01-JAN-2021');

/********** Watchlist relation **********/
insert into Watchlist values (1000,'testuser');