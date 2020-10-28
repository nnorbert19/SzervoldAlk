using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GAMF.Data;
using GAMF.Models;
using Microsoft.AspNetCore.Mvc;

namespace GAMF.Controllers
{
    public class RiportController : Controller
    {
        private readonly GAMFDbContext _context;
        public RiportController(GAMFDbContext context)
        {
            _context = context;
        }
        public IActionResult EnrollmentDateRiport()
        {
            List<EnrollmentDateVM> enrollmentDateList;

            IQueryable<EnrollmentDateVM> data =
                from student in _context.Students
                group student by student.EnrollmentDate into dateGroup
                select new EnrollmentDateVM()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            enrollmentDateList = data.ToList();
            return View(enrollmentDateList);
        }
    }
}
