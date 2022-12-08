using System.Data;

namespace UniversityWebsite.App_Code
{
    /// <summary>
    /// Student name: LEUNG Ho Man Max
    /// Student id: 20611398
    /// </summary>

    public class UniversityDBAccess
    {
        //******************************** IMPORTANT NOTE ********************************
        // For the web pages to display a query result correctly, and possibly to not    *
        // generate errors, the attributes should be retrieved in the order specified,   *
        // if any, in a TODO and the attribute names in a query result table must be     *
        // EXACTLY the same as that in the database tables.                              *
        //                                                                               *
        //     REMINDER: DO NOT place single quotes around numeric valued parameters.    *
        //                                                                               *
        //          Report problems with the website code to 3311rep@cse.ust.hk.         *
        //********************************************************************************

        private readonly OracleDBAccess myOracleDBAccess = new OracleDBAccess();
        private DataTable queryResult;
        private bool updateResult;
        private string sql;

        #region SQL statements for admin

        public DataTable GetStudentRecord(string studentId)
        {
            //********************************************************************************
            // TODO 01: Construct the SELECT statement to find the record of a student       *
            //          identified by his/her student id.                                    *
            //********************************************************************************
            sql = $"select * " +
                $"from Student " +
                $"where studentId='{studentId}'";
            return queryResult = myOracleDBAccess.GetData("TODO 01", sql);
        }

        public DataTable GetDepartments()
        {
            //********************************************************************************
            // TODO 02: Construct the SELECT statement to retrieve the department id and     *
            //          name of all departments.                                             *
            //********************************************************************************
            sql = $"select departmentId, departmentName from Department";
            return queryResult = myOracleDBAccess.GetData("TODO 02", sql);
        }

        public DataTable GetDepartmentStudentRecords(string departmentId)
        {
            //********************************************************************************
            // TODO 03: Construct the SELECT statement to find the student id, last name,    *
            //          first name, email and cga of the students in the department          *
            //          identified by a department id.                                       *
            //          Order the result by last name ascending.                             *
            //********************************************************************************
            sql = $"select studentId, lastName, firstName, email, cga from Student where departmentId = '{departmentId}'";
            return queryResult = myOracleDBAccess.GetData("TODO 03", sql);
        }

        public bool CreateStudentRecord(string studentId, string firstName, string lastName, string email,
            string phoneNo, string cga, string departmentId, string admissionYear)
        {
            //********************************************************************************
            // TODO 04: Construct the INSERT statement to add a new student record.          *
            // NOTE: Be careful how the cga value is inserted (see IMPORTANT NOTE above).    *
            //********************************************************************************
            sql = $"insert into Student values ('{studentId}', '{firstName}', '{lastName}', '{email}', '{phoneNo}', {cga}, '{departmentId}', '{admissionYear}')";
            return updateResult = myOracleDBAccess.SetData("TODO 04", sql);
        }

        public bool ModifyStudentRecord(string studentId, string firstName, string lastName, string email,
            string phoneNo, string cga, string departmentId, string admissionYear)
        {
            //********************************************************************************
            // TODO 05: Construct the UPDATE statement to update all updateable attributes   *
            //          of a student record identified by its student id.                    *
            // NOTE: Be careful how the cga value is updated (see IMPORTANT NOTE above).     *
            //********************************************************************************
            sql = $"update Student set studentId = '{studentId}', firstName = '{firstName}', lastName = '{lastName}', email = '{email}', phoneNo = '{phoneNo}', cga = {cga}, departmentId = '{departmentId}', admissionYear = '{admissionYear}'";
            return updateResult = myOracleDBAccess.SetData("TODO 05", sql);
        }

        #endregion SQL statements for admin

        #region SQL statements for students

        public DataTable GetEnrolledCourses(string email)
        {
            //*********************************************************************************
            // TODO 06: Construct the SELECT statement to find the course id, name, grade,    *
            //          credits and instructor of the courses in which a student, identified  *
            //          by his/her email, is enrolled.                                        *
            //          Order the result by course id ascending.                              *
            //*********************************************************************************
            sql = $"select Course.courseId, courseName, grade, credits, instructor from Course join EnrollsIn on Course.courseId = EnrollsIn.courseId where studentId = (select studentId from Student where email = '{email}')";
            return queryResult = myOracleDBAccess.GetData("TODO 06", sql);
        }

        public DataTable GetCoursesAvailableToEnrollIn(string email)
        {
            //********************************************************************************
            // TODO 07: Construct the SELECT statement to find the course id, name, credits  *
            //          and instructor of the courses in which a student, identified by      *
            //          his/her email, is not enrolled.                                      *
            //          Order the result by course id ascending.                             *
            //********************************************************************************
            sql = $"select distinct Course.courseId, courseName, credits, instructor from Course join EnrollsIn on Course.courseId = EnrollsIn.courseId where studentId != (select studentId from Student where email = '{email}');";
            return queryResult = myOracleDBAccess.GetData("TODO 07", sql);
        }

        public DataTable GetStudentId(string email)
        {
            //********************************************************************************
            // TODO 08: Construct the SELECT statement to find the student id of a student   *
            //          identified by his/her email.                                         *
            //********************************************************************************
            sql = $"select studentId from Student where email = '{email}'";
            return queryResult = myOracleDBAccess.GetData("TODO 08", sql);
        }

        public bool EnrollInCourses(string studentId, string courseId)
        {
            //********************************************************************************
            // TODO 09: Construct the INSERT statement to enroll a student in a course.      *
            //********************************************************************************
            sql = $"insert into EnrollsIn values ('{studentId}', '{courseId}')";
            return updateResult = myOracleDBAccess.SetData("TODO 09", sql);
        }

        #endregion SQL statements for students
    }
}