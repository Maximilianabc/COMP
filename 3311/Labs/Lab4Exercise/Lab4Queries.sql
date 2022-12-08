/* COMP 3311 Lab 4 Exercise: Queries */

clear screen
set feedback off
set pagesize 30

/* Query 1 */
set heading off
select 'Query 1: Find the minimum, maximum, average and total number of computers over all departments.' from dual;
set heading on
-- ***** Construct Query 1 below this line ***** 


/* Query 2 */
set heading off
select '--------------------------------------------------------------------------------------------------' from dual;
select 'Query 2: Find the first name, last name, student id and cga of the students from the BUS' || chr(10) ||
       '         department with the highest cga.' from dual;
-- ***** Construct Query 2 below this line ***** 


/* Query 3 */
select '--------------------------------------------------------------------------------------------------' from dual;
select 'Query 3: Find, for each course, the course id and the average cga of the students enrolled in the' || chr(10) ||
       '         course. Order the result by average cga descending.' from dual;
set heading on
-- ***** Construct Query 3 below this line ***** 


/* Query 4 */
set heading off
select '--------------------------------------------------------------------------------------------------' from dual;
select 'Query 4: Find, for each course, the course id, student last and first name, department id and cga' || chr(10) ||
       '         of the students who have the highest cga in the course. Order the result by course id' || chr(10) ||
       '         ascending.' from dual;
set heading on
-- ***** Construct Query 4 below this line ***** 


/* Query 5 */
set heading off
select '--------------------------------------------------------------------------------------------------' from dual;
select 'Query 5: Find, for each student enrolled in the COMP department, the first name, last name and' || chr(10) ||
       '         the number of courses in which the student is enrolled. Order the result first by the' || chr(10) ||
       '         number of courses descending and second by last name ascending.' from dual;
set heading on
-- ***** Construct Query 5 below this line ***** 

