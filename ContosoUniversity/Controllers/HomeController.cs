using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<CourseTitleGroup> data = from student in db.Students
                                                   join enrollment in db.Enrollments on student.ID equals enrollment.StudentID
                                                   join course in db.Courses on enrollment.CourseID equals course.ID
                                                   group student by course.Title into courseGroup
                                                   select new CourseTitleGroup()
                                                   {
                                                       CourseTitle = courseGroup.Key,
                                                       StudentCount = courseGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}