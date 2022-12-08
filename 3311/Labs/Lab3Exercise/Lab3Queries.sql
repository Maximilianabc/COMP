/* COMP 3311 Lab 3 Exercise: Queries */

clear screen
set feedback off


/* Query 1 */
set heading off
select 'Query 1: Find the student id, first name, last name, email and cga of the students who are' || chr(10) ||
       '         not in the COMP or BUS departments. Order the result from highest to lowest cga.' from dual;
set heading on
-- ***** Construct Query 1 below this line *****
COLUMN STUDENTID HEADING Id;
COLUMN FIRSTNAME HEADING 'First Name';
COLUMN LASTNAME HEADING 'Last Name';
COLUMN EMAIL HEADING Email;
SELECT studentId, firstName, lastName, email, cga FROM Student
WHERE departmentid != 'COMP' AND departmentid != 'BUS'
ORDER BY cga DESC;


/* Query 2 */
set heading off
select '-------------------------------------------------------------------------------------------' from dual;
select 'Query 2: Find the last name and first name of the students whose last name' || chr(10) ||
       '         contains the letter "u" as the 2nd character.' from dual;
set heading on
-- ***** Construct Query 2 below this line *****
COLUMN LASTNAME CLEAR;
COLUMN FIRSTNAME CLEAR;
SELECT lastName, firstName FROM Student
WHERE lastName LIKE '_u%';

/* Query 3 */
set heading off
select '-------------------------------------------------------------------------------------------' from dual;
select 'Query 3: Find the last name and first name of the students whose first name' || chr(10) ||
       '         and last name contains a double letter (e.g., "ee", "ll", "mm", etc.).' from dual;
set heading on
-- ***** Construct Query 3 below this line *****
SELECT lastName, firstName FROM Student
WHERE REGEXP_LIKE(firstName, '([a-z])\1') AND REGEXP_LIKE(lastName, '([a-z])\1');


/* Query 4 */
set heading off
select '-------------------------------------------------------------------------------------------' from dual;
select 'Query 4: Find the last name, first name and cga of the students who have the three' || chr(10) ||
       '         lowest cgas.' from dual;
set heading on
-- ***** Construct Query 4 below this line *****
SELECT lastName, firstName, cga FROM Student
ORDER BY cga
FETCH FIRST 3 ROWS ONLY;

/* Query 5 */
set heading off
select '-------------------------------------------------------------------------------------------' from dual;
select 'Query 5: Find the student id, last name, first name, cga and department name of' || chr(10) ||
       '         the students who are in the COMP or the BUS departments and whose CGA' || chr(10) ||
       '         is in the range 3.0 to 4.0. Order the result by last name ascending.' from dual;
set heading on
-- ***** Construct Query 5 below this line *****
COLUMN STUDENTID CLEAR;
SELECT Student.studentId, Student.lastName, Student.firstName, Student.cga, Department.departmentName FROM Student
JOIN Department 
ON Student.departmentid = Department.departmentid
AND Student.cga >= 3.0 AND Student.cga <= 4.0
ORDER BY student.lastName ASC;