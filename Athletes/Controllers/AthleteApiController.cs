using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Athletes.DAL;
using Microsoft.AspNet.Identity;
using Athletes.Models;
using Microsoft.AspNet.Identity;

namespace Athletes.Controllers
{
    // Data structure of an athlete's Highlight Video
    public class HighlightVideo
    {
        public int Id { get; set; }
        public string UrlLink { get; set; }
    }

    // Data structure of an athlete data being sent to client sides
    public class AthleteVM
    {
        public Athlete Athlete { get; set; }
        public List<HighlightVideo> HighlightVideos { get; set; }
    }

    [EnableCors(origins: "*", headers:"*", methods: "*")]
    public class AthleteApiController : ApiController
    {
        private AthletesContext db = new AthletesContext();
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        //Get all athletes from the database

        public IHttpActionResult Get()
        {
            try
            {
                var athletes = db.Athletes.ToList();
                if (!athletes.Any())
                {
                    return NotFound();
                }
                return Json(athletes);
            }
            catch
            {
                return BadRequest();
            }
        }


        public IHttpActionResult GetAthleteById(string id)
        {
            // Get the current signed-in athlete data
            var userEmail = User.Identity.Name;
            var athlete = db.Athletes.FirstOrDefault(t => t.Email == userEmail);

            id = athlete.Id;
            var athleteHighlighVideos = db.AthleteHighlightVideos
                                            .Where(t => t.AthleteId == id)
                                            .Select(t => new HighlightVideo { Id = t.Id, UrlLink = t.UrlLink })
                                            .ToList();
            if (athlete == null)
                return NotFound();

            // return an athlete attributes and hightlight videos
            var athleteVM = new AthleteVM { Athlete = athlete, HighlightVideos = athleteHighlighVideos };
            return Ok(athleteVM);
        }

        // Get data from client sides for adding a video link or editing an existing video link
        [HttpPost]
        public IHttpActionResult AddVideo([FromBody] HighlightVideo video)
        {
            // TODO: Ideally the next IF-ELSE should split into a multiple posts on one controller. One for POST and the other for PUT.
            var existingVideo = db.AthleteHighlightVideos.Where(v => v.Id == video.Id)
                                                                .FirstOrDefault<AthleteHighlightVideo>();
            if (existingVideo != null)
            {
                // Update the existing video
                existingVideo.UrlLink = video.UrlLink;
            }
            else
            {
                if (video.Id == -1)
                {
                    // Add video
                    var userId = User.Identity.GetUserId();
                    var newVideo = new AthleteHighlightVideo { UrlLink = video.UrlLink, AthleteId = userId };
                    db.AthleteHighlightVideos.Add(newVideo);
                }
                else
                {
                    return NotFound();
                }
            }

            db.SaveChanges();
            return Ok();
        }

        // Delete an existing video link
        [HttpDelete]
        public IHttpActionResult Delete([FromBody] HighlightVideo video)
        {
            var existingVideo = db.AthleteHighlightVideos.Where(v => v.Id == video.Id)
                                                                .FirstOrDefault<AthleteHighlightVideo>();
            if (existingVideo == null)
            {
                return NotFound();
            }

            db.AthleteHighlightVideos.Remove(existingVideo);
            db.SaveChanges();
            return Ok();
        }


        // Update the athlete information after the athlete has registered or later if there is a need to update anything.
    public IHttpActionResult Put([FromBody]Athlete athlete)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            //getting the data for the athlete who wants to edit the information from the database and swapping it with the updated values.
                var existingAthlete = db.Athletes.Where(a => a.Id == athlete.Id).FirstOrDefault<Athlete>();

                if (existingAthlete != null)
                {
                    existingAthlete.FirstName = athlete.FirstName;
                    existingAthlete.LastName = athlete.LastName;
                    existingAthlete.DateOfBirth = athlete.DateOfBirth;
                    existingAthlete.Age = athlete.DateOfBirth.Year;
                    existingAthlete.Position = athlete.Position;
                    existingAthlete.Height = athlete.Height;
                    existingAthlete.SpikeTouch = athlete.SpikeTouch;
                    existingAthlete.Gender = athlete.Gender;
                   
                db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            return Ok();
        }


    }
}



//https://www.c-sharpcorner.com/article/create-simple-web-api-in-asp-net-mvc/