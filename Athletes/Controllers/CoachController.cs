using Athletes.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Athletes.Models;
using Athletes.ViewModels;

namespace Athletes.Controllers
{
    public class CoachController : Controller
    {
        private AthletesContext db = new AthletesContext();

        // GET: Coach
        public ActionResult Index()
        {
            // eager loading
            List<Coach> coaches = db.Coaches.Include(c => c.School).ToList();
            var coachViewModels = new List<CoachViewModel>();
            
            foreach(var coach in coaches)
            { 
                var hightlightVideos = db.HighlightVideos.Where(video => video.SchoolId == coach.SchoolID).Select(video => video.Link).ToList();
                coachViewModels.Add(new CoachViewModel
                {
                    Coach = coach,
                    HighlightVideos = hightlightVideos
                });
            }

            return View(coachViewModels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}