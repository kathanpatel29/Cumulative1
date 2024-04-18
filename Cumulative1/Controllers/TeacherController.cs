using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cumulative1.Models;
using Mysqlx.Datatypes;

namespace Cumulative1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        /// <summary>
        /// To find teacher by(name, hiredate, salary) - creating a search interface
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns> /Teacher/Search</ returns>

        //GET : /Teacher/Search
        public ActionResult Search(string searchString)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.SearchTeachers(searchString);
            return View("Search", Teachers);
        }


        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherName, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            // Check for missing information
            if (string.IsNullOrEmpty(TeacherFname) || string.IsNullOrEmpty(TeacherLname) || string.IsNullOrEmpty(EmployeeNumber))
            {
                ModelState.AddModelError("", "Please fill out all required fields.");
                return View("New");
            }
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherName);
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherName = TeacherName;
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }


        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();

        }

        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the Teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher /Update/5</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }
        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }
            /// <summary>
            /// Receives a POST request containing information about an existing Teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated Teacher.
            /// </summary>
            /// <param name="id">Id of the Teacher to update</param>
            /// <param name="TeacherFname">The updated first name of the Teacher</param>
            /// <param name="TeacherLname">The updated last name of the Teacher</param>
            /// <param name="EmployeeNumber">The updated employee number of the Teacher.</param>
            /// <param name="HireDate">The updated hiring date of the Teacher.</param>
            ///  /// <param name="Salary">The updated salary of the Teacher.</param>
            /// <returns>A dynamic webpage which provides the current information of the Teacher.</returns>
            /// <example>
            /// POST : /Teacher/Update/10
            /// FORM DATA / POST DATA / REQUEST BODY 
            /// {
            ///	"TeacherFname":"Kathan",
            ///	"TeacherLname":"Patel",
            ///	"EmployeeNumber":"T999!",
            ///	"HireDate":"christine@test.ca"
            /// }
            /// </example>
            [HttpPost]
            public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
            {
                Teacher TeacherInfo = new Teacher();
                TeacherInfo.TeacherFname = TeacherFname;
                TeacherInfo.TeacherLname = TeacherLname;
                TeacherInfo.EmployeeNumber = EmployeeNumber;
                TeacherInfo.HireDate = HireDate;
                TeacherInfo.Salary = Salary;

                TeacherDataController controller = new TeacherDataController();
                controller.UpdateTeacher(id, TeacherInfo);

                return RedirectToAction("Show/" + id);
            }
        }

    }