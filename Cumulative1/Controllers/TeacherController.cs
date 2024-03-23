using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cumulative1.Models;

namespace Cumulative1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Author/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        //GET : /Author/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.findTeacher(id);


            return View(NewTeacher);
        }

        /// <summary>
        /// To find teacher by(name, hiredate, salary) - creating a search interface
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns> /Teacher/Search</ returns>
        public ActionResult Search(string searchString) 
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.SearchTeachers(searchString);
            return View("Search", Teachers);
        }

    }
}