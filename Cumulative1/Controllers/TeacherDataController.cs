using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using Cumulative1.Models;
using MySql.Data.MySqlClient;
using System.Web.Http.Cors;

namespace Cumulative1.Controllers
{
    public class TeacherDataController : ApiController
    {
        //Database context class allows us to connect or access our MySQL Database.
        private schoolDbContext school = new schoolDbContext();

        //This Controller Will access the teachers table of our blog database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherName = ResultSet["teacherfname"].ToString() + " " + ResultSet["teacherlname"].ToString();
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];


                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherName = TeacherName;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;


                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }
            return Teachers;
        }


        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid = "+id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherName = ResultSet["teacherfname"].ToString() + " " + ResultSet["teacherlname"].ToString();
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = (decimal)ResultSet["salary"];


                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherName = TeacherName;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;


            }
            return NewTeacher;
        }

        [HttpGet]
        [Route("api/TeacherData/SearchTeachers/{searchString}")]
        public IEnumerable<Teacher> SearchTeachers(string searchString)
        {
            //Create instance for the connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and the database
            Conn.Open();

            // //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //QUERY 
            cmd.CommandText = "SELECT * FROM Teachers WHERE teacherfname LIKE @searchString OR teacherlname LIKE @searchString";
            cmd.Parameters.AddWithValue("@searchString", "%" + searchString + "%");

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Define Teachers list
            List<Teacher> Teachers = new List<Teacher>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                // Populate Teachers list
                Teacher teacher = new Teacher
                {
                    TeacherId = (int)ResultSet["teacherid"],
                    TeacherFname = ResultSet["teacherfname"].ToString(),
                    TeacherLname = ResultSet["teacherlname"].ToString(),
                    EmployeeNumber = ResultSet["employeenumber"].ToString(),
                    HireDate = (DateTime)ResultSet["hiredate"],
                    Salary = (decimal)ResultSet["salary"]
                };
                Teachers.Add(teacher);
            }
            //Close the connection between the server and database
            Conn.Close();

            return Teachers;
        }

        /// <summary>
        /// Adds Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) VALUES (@TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            Conn.Close();



        }
        /// <summary>
        /// Deletes an Teacher from the connected MySQL Database if the ID of that teacher exists. Does NOT maintain relational integrity.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <example>POST /api/TeacherData/DeleteTeacher/3</example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers WHERE teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher teacherInfo)
        {
            // Create an instance of a connection
            MySqlConnection conn = school.AccessDatabase();

            // Open the connection between the web server and database
            conn.Open();

            // Establish a new command (query) for our database
            MySqlCommand cmd = conn.CreateCommand();

            // SQL QUERY
            cmd.CommandText = "UPDATE teachers SET TeacherFname = @TeacherFname, TeacherLname = @TeacherLname, EmployeeNumber = @EmployeeNumber, HireDate = @HireDate, Salary = @Salary WHERE TeacherId = @TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", teacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", teacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", teacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", teacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@Salary", teacherInfo.Salary);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }
}