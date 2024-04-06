using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cumulative1.Models
{
    public class Student : Controller
    {
        // GET: Student
        // GET: Teacher
        public int TeacherId;
        public string TeacherName;
        public string TeacherFname;
        public string TeacherLname;
        public string EmployeeNumber;
        public DateTime HireDate;
        public decimal Salary;
        public ActionResult Index()
        {
            return View();
        }
    }
}