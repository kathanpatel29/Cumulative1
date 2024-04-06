using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Cumulative1.Models;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    public class StudentDataController : ApiController
    {
        // Database context class allows us to connect or access our MySQL Database.
        private schoolDbContext school = new schoolDbContext();

        // This Controller Will access the students table of our blog database.
        /// <summary>
        /// Returns a list of Students in the system
        /// </summary>
        /// <example>GET api/StudentData/ListStudents</example>
        /// <returns>
        /// A list of students (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Student> ListStudents()
        {
            // Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "Select * from students";

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Create an empty list of Students
            List<Student> Students = new List<Student>();

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                // Access Column information by the DB column name as an index
                int StudentId = (int)ResultSet["studentid"];
                string StudentName = ResultSet["studentfname"].ToString() + " " + ResultSet["studentlname"].ToString();
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];
                


                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentName = StudentName;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;
               


                // Add the Student to the List
                Students.Add(NewStudent);
            }
            return Students;
        }


        [HttpGet]
        [Route("api/StudentData/FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            // Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the web server and database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "Select * from students where studentid = " + id;

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                // Access Column information by the DB column name as an index
                int StudentId = (int)ResultSet["studentid"];
                string StudentName = ResultSet["studentfname"].ToString() + " " + ResultSet["studentlname"].ToString();
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime EnrolDate = (DateTime)ResultSet["enroldate"];



                NewStudent.StudentId = StudentId;
                NewStudent.StudentName = StudentName;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;
              


            }
            return NewStudent;
        }

        [HttpGet]
        [Route("api/StudentData/SearchStudents/{searchString}")]
        public IEnumerable<Student> SearchStudents(string searchString)
        {
            // Create instance for the connection
            MySqlConnection Conn = school.AccessDatabase();

            // Open the connection between the web server and the database
            Conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            // QUERY 
            cmd.CommandText = "SELECT * FROM students WHERE studentfname LIKE @searchString OR studentlname LIKE @searchString";
            cmd.Parameters.AddWithValue("@searchString", "%" + searchString + "%");

            // Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Define Students list
            List<Student> Students = new List<Student>();

            // Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                // Populate Students list
                Student student = new Student
                {
                    StudentId = (int)ResultSet["studentid"],
                    StudentFname = ResultSet["studentfname"].ToString(),
                    StudentLname = ResultSet["studentlname"].ToString(),
                    StudentNumber = ResultSet["studentnumber"].ToString(),
                    EnrolDate = (DateTime)ResultSet["enrollmentdate"],
                };
                Students.Add(student);
            }
            // Close the connection between the server and database
            Conn.Close();

            return Students;
        }
    }
}
