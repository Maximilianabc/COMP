using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace UniversityWebsite
{
    public class Global : HttpApplication
    {
        public enum UniversityRole { Admin, Student, None }

        public enum EmptyQueryResultMessageType { DBError, DBQueryError, SQLError, Information }

        public static string contact3311rep = " Please contact 3311rep.";
        public static string UniversityPassword = "University1#";
        public static bool isEmptyQueryResult;
        public static bool isSqlError = false;
        public static string sqlErrorMessage;

        // Feedback messages.
        public static string enrollmentSuccess = "You have successfully enrolled in ";
        public static string informationNotChanged = "You have not changed any information.";
        public static string informationUpdated = "The student's information has been updated.";
        public static string nonexistentStudent = "There is no student with this student id.";
        public static string studentRecordCreated = "The student record has been created.";
        public static string selectCourse = "Please select one or more courses to enroll in.";

        // Internal error messages.
        public static string emptyOrNullErrorMessage = "*** System error: empty or null error message.";

        // Database error messages.
        public static string dbError = "*** Database error in ";
        public static string dbErrorNoStudentRecord = "There is no record for this student."; // Should never get this message.

        // Database/query error message.
        public static string dbqueryError = "*** Database/SQL error in ";
        public static string dbqueryErrorNoDepartments = ": No departments were retrieved. Please check your query and/or database.";
        public static string dbqueryErrorNoRecordsRetrieved = ": No record for this student was retrieved. Please check your query and/or database.";
        public static string dbqueryErrorNoStudentIdRetrieved = ": No student id was retrieved. Please check your query and/or database.";

        // Query error messages.
        public static string queryError = "*** SQL error in ";
        public static string queryErrorMultipleStudentIdsRetrieved = ": Your query retrieves more than one student id.";
        public static string queryErrorMultipleRecordsRetrieved = ": Your query retrieves more than one record.";

        // Empty query result messages.
        public static string noAvailableCourses = "There are no available courses for you to enroll in.";
        public static string noEnrolledCourses = "You are not enrolled in any courses.";
        public static string noEnrolledStudents = "There are no students enrolled in this department.";

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}