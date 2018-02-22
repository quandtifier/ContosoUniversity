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
            /*
             * LINQ statement groups the students by enrollment date, calculates the numnber of students in
             * each group and stores the results in a collection EnrollmentDateGroup
             */
            // commented out to show raw sql query
            //IQueryable<EnrollmentDateGroup> data = from student in db.Students
            //                                       group student by student.EnrollmentDate into dateGroup
            //                                       select new EnrollmentDateGroup()
            //                                       {
            //                                           EnrollmentDate = dateGroup.Key,
            //                                           StudentCount = dateGroup.Count()
            //                                       };

            // SQL version of the LINQ code
            // differs from tutorial because inheritance was never implemented
            string query = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                + "FROM Student "
                + "GROUP BY EnrollmentDate";
            IEnumerable<EnrollmentDateGroup> data = db.Database.SqlQuery<EnrollmentDateGroup>(query);
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