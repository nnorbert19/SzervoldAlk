using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GAMF.Data;
using GAMF.Models;
using Microsoft.AspNetCore.Mvc;

namespace GAMF.Controllers
{
    public class CreditController : Controller
    {
        private readonly GAMFDbContext _context;
        public CreditController(GAMFDbContext context)
        {
            _context = context;
        }
        public IActionResult CreditControllerRiport()
        {
            List<CreditVM> Creditlist;
            IQueryable<CreditVM> data =
                from Enrollment in _context.Enrollments
                where Enrollment.Grade != Grade.F
                join Student in _context.Students on Enrollment.StudentId equals Student.Id
                join Course in _context.Courses on Enrollment.CourseId equals Course.CourseId
                select new {Student.FirstMidName,Student.LastName,Course.Credits,Enrollment.Grade} into x
                group x by new {x.FirstMidName,x.LastName} into datagroup
                select new CreditVM()
                {
                    FirstMidName = datagroup.Key.FirstMidName,
                    LastName = datagroup.Key.LastName,
                    Credit = datagroup.Sum(i =>i.Credits)
                };
            Creditlist = data.ToList();
            return View(Creditlist);
        }

    }
}
