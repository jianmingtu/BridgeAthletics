using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Athletes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Athletes.DAL
{
    public class AthletesInitializer : DropCreateDatabaseIfModelChanges<AthletesContext>
    {
        protected override void Seed(AthletesContext context)
        {
            var athletes = new List<Athlete>
            {
                new Athlete{Id="1", Email = "test1@test.com", FirstName="Test",LastName="Athlete", Position="Setter", DateOfBirth= Convert.ToDateTime(DateTime.Now.ToShortDateString()) , Age=21, Gender="Female",  Height="6' 5\"",SpikeTouch="9' 7\"", ImgUrl=""},
                new Athlete{Id="2", Email = "test2@test.com", FirstName="Meredith",LastName="Alonso", Position="Libero" ,DateOfBirth= Convert.ToDateTime(DateTime.Now.ToShortDateString()) , Age=21, Gender="Female", Height="6' 5\"",SpikeTouch="9' 7\"", ImgUrl="" },
                new Athlete{Id="3", Email = "test3@test.com", FirstName="Arturo",LastName="Anand", Position="Opposite" ,DateOfBirth= DateTime.Now.Date , Age=21, Gender="Male", Height="6' 5\"",SpikeTouch="9' 7\"", ImgUrl="" },
                new Athlete{Id="4", Email = "test4@test.com", FirstName="Gytis",LastName="Barzdukas", Position="Middle" ,DateOfBirth= DateTime.Now.Date , Age=21, Gender="Female", Height="6' 5\"",SpikeTouch="9' 7\"", ImgUrl="" },
                new Athlete{Id="5", Email = "test5@test.com", FirstName="Yan",LastName="Li", Position="Opposite" ,DateOfBirth= DateTime.Now.Date , Age=21, Gender="Female", Height="6' 5\"",SpikeTouch="9' 7\"", ImgUrl="" },
                new Athlete{Id="6", Email = "test6@test.com", FirstName="Peggy",LastName="Justice", Position="Left Side" ,DateOfBirth= DateTime.Now.Date , Age=21, Gender="Female", Height="6' 5\"",SpikeTouch="9' 7\"", ImgUrl="" },
                new Athlete{Id="7", Email = "test7@test.com", FirstName="Laura",LastName="Norman", Position="Libero" ,DateOfBirth= DateTime.Now.Date , Age=21, Gender="Male", Height="6' 5\"",SpikeTouch="9' 7\"", ImgUrl="" },
                new Athlete{Id="8", Email = "test8@test.com", FirstName="Nino",LastName="Olivetto", Position="Setter" ,DateOfBirth= DateTime.Now.Date , Age=21, Gender="Male", Height="6' 5\"",SpikeTouch="9' 7\"", ImgUrl="" }
            };
            athletes.ForEach(a => context.Athletes.Add(a));
            context.SaveChanges();

            // Athlete HighlightVideo Seeder 
            var athleteHighlightVideos = new List<AthleteHighlightVideo>
            {
                // parameter order - Id, video link, AthleteId 
                new AthleteHighlightVideo{ Id = 1, UrlLink="https://www.youtube.com/embed/iZOnzyXoxuc", AthleteId="1" },
                new AthleteHighlightVideo{ Id = 2, UrlLink="https://www.youtube.com/embed/rc_pi5fcxzM", AthleteId="1" },
                new AthleteHighlightVideo{ Id = 3, UrlLink="https://www.youtube.com/embed/HO7rxJLgLac", AthleteId="1" },
                new AthleteHighlightVideo{ Id = 4, UrlLink="https://www.youtube.com/embed/iZOnzyXoxuc", AthleteId="2" },
                new AthleteHighlightVideo{ Id = 5, UrlLink="https://www.youtube.com/embed/rc_pi5fcxzM", AthleteId="2" },
                new AthleteHighlightVideo{ Id = 6, UrlLink="https://www.youtube.com/embed/HO7rxJLgLac", AthleteId="2" },
            };

            athleteHighlightVideos.ForEach(s => context.AthleteHighlightVideos.Add(s));
            context.SaveChanges();

            // School Seeder 
            // ***** IMPORTANT:  school seeder first and then coach 
            var schools = new List<School>
            {
                // parameter order - name, rosterPicture, highlightVideos, coachPhilosophy
               
                new School{
                            ID = 2,
                            Name = "Trinity Western University",
                            // HighlightVideos = new List<string>{ "https://www.youtube.com/embed/iPVwlGmWGqw", "https://www.youtube.com/embed/iPVwlGmWGqw" },
                            CoachPhilosophy = "Bring all TWUs to succeed",
                            MensCoach = "10001-0",
                            WomensCoach = "",
                    },
                new School{
                            ID = 3,
                            Name = "University of Fraser Valley",
                            // HighlightVideos = new List<string>{ "https://www.youtube.com/embed/iPVwlGmWGqw", "https://www.youtube.com/embed/iPVwlGmWGqw" },
                            CoachPhilosophy = "Bring all UFV to succeed",
                            MensCoach = "",
                            WomensCoach = "10002-100",
                    },

            };
            schools.ForEach(s => context.Schools.Add(s));
            context.SaveChanges();


            // Coach Seeder 
            var coaches = new List<Coach>
            {
                // parameter order - userId, headCoach, firstName, lastName, schoolId

                // TWU
                // https://gospartans.ca/staff-directory/mens-volleyball-department/8
                new Coach( "10001-0", "ben.josephson@twu.ca", true, "Ben", "Josephson", 2),
                new Coach( "10001-1", "ben.ball@mytwu.ca", false, "Ben", "Ball", 2),
                // https://gospartans.ca/staff-directory/womens-volleyball-department/7
                new Coach( "10001-100", "ryan.hofer@twu.ca", true, "Ryan", "Hofer", 2),
                new Coach( "10001-101", "", false, "Duncan", "Harrison", 2),

                // UFV
                // https://gocascades.ca/sports/mens-volleyball/roster
                new Coach( "10002-0", "nathan.bennett@ufv.ca", true, "NATHAN", "BENNETT", 2),
                new Coach( "10002-1", "", false, "ALEX", "HARVALIAS", 2),       
                // https://gocascades.ca/sports/womens-volleyball/roster
                new Coach( "10002-100", "janelle.rozema@ufv.ca", true, "JANELLE", "ROZEMA", 2),
                new Coach( "10002-101", "", true, "JENN", "COOK", 2),


            };
            coaches.ForEach(s => context.Coaches.Add(s));
            context.SaveChanges();

            // HighlightVideo Seeder 
            var hightlightVideos = new List<HighlightVideo>
            {
                // parameter order - Id, video link, schoolId ( UBC is 1 )
                new HighlightVideo{ Id = 1, Link="https://youtu.be/oEbXOYR9Lg0", SchoolId=1 },
                new HighlightVideo{ Id = 2, Link="https://www.youtube.com/watch?v=QWggS46t6_8", SchoolId=1 }
            };

            hightlightVideos.ForEach(s => context.HighlightVideos.Add(s));
            context.SaveChanges();

            var schoolNames = new List<SchoolName>
            {
            new SchoolName {ID = 1 ,Name="University of British Columbia"},
            new SchoolName {ID = 2 ,Name="Trinity Western University"},
            new SchoolName {ID = 3 ,Name="University of Fraser Valley"},
            new SchoolName {ID = 4 ,Name="Thompson Rivers University"},
            new SchoolName {ID = 5 ,Name="University of British Columbia - Okanagan"},
            new SchoolName {ID = 6 ,Name="Vancouver Island University"},
            new SchoolName {ID = 7 ,Name="Douglas College"},
            new SchoolName {ID = 8 ,Name="Capilano University"},
            new SchoolName {ID = 9 ,Name="Camosun College"},
            new SchoolName {ID = 10,Name="College of the Rockies"},
            new SchoolName {ID = 11,Name="University of Alberta"},
            new SchoolName {ID = 12,Name="University of Calgary"},
            new SchoolName {ID = 13,Name="Grand Macewan University"},
            new SchoolName {ID = 14,Name="Mount Royal University"},
            new SchoolName {ID = 15,Name="Concordia University of Edmonton"},
            new SchoolName {ID = 16,Name="Grand Prarie Regional College"},
            new SchoolName {ID = 17,Name="King's University College"},
            new SchoolName {ID = 18,Name="Lakeland College"},
            new SchoolName {ID = 19,Name="Northern Alberta Insitute of Technology"},
            new SchoolName {ID = 20,Name="The King's University"},
            new SchoolName {ID = 21,Name="Ambrose University"},
            new SchoolName {ID = 22,Name="Lethbridge University"},
            new SchoolName {ID = 23,Name="Medicine Hat College"},
            new SchoolName {ID = 24,Name="Olds College"},
            new SchoolName {ID = 25,Name="Red Deer College"},
            new SchoolName {ID = 26,Name="Southern Alberta Institute of Technology"},
            new SchoolName {ID = 27,Name="University of Saskatchewan"},
            new SchoolName {ID = 28,Name="University of Regina"},
            new SchoolName {ID = 29,Name="Briercrest College"},
            new SchoolName {ID = 30,Name="University of Manitoba"},
            new SchoolName {ID = 31,Name="University of Winnipeg"},
            new SchoolName {ID = 32,Name="Brandon University"},
            new SchoolName {ID = 33,Name="University of Toronto"},
            new SchoolName {ID = 34,Name="Queen's University"},
            new SchoolName {ID = 35,Name="Ryerson University"},
            new SchoolName {ID = 36,Name="Nipissing University"},
            new SchoolName {ID = 37,Name="York University"},
            new SchoolName {ID = 38,Name="Royal Military College"},
            new SchoolName {ID = 39,Name="Trent University"},
            new SchoolName {ID = 40,Name="McMaster University"},
            new SchoolName {ID = 41,Name="Guelph University"},
            new SchoolName {ID = 42,Name="Windsor University"},
            new SchoolName {ID = 43,Name="Brock University"},
            new SchoolName {ID = 44,Name="Western University"},
            new SchoolName {ID = 45,Name="Waterloo University"},
            new SchoolName {ID = 46,Name="Durham College"},
            new SchoolName {ID = 47,Name="Georgian College"},
            new SchoolName {ID = 48,Name="Canadore College"},
            new SchoolName {ID = 49,Name="Fleming College"},
            new SchoolName {ID = 50,Name="Algonquin College"},
            new SchoolName {ID = 51,Name="George Brown College"},
            new SchoolName {ID = 52,Name="Loyalist College"},
            new SchoolName {ID = 53,Name="Centennial College"},
            new SchoolName {ID = 54,Name="Seneca College"},
            new SchoolName {ID = 55,Name="LA Cite College/La Cite Collegiale"},
            new SchoolName {ID = 56,Name="Humber College"},
            new SchoolName {ID = 57,Name="Conestoga College"},
            new SchoolName {ID = 58,Name="Niagara College"},
            new SchoolName {ID = 59,Name="Redeemer College"},
            new SchoolName {ID = 60,Name="Fanshawe College"},
            new SchoolName {ID = 61,Name="St. Claire College"},
            new SchoolName {ID = 62,Name="Mohawk College"},
            new SchoolName {ID = 63,Name="Sheridan College"},
            new SchoolName {ID = 64,Name="Boreal College"},
            new SchoolName {ID = 65,Name="Cambrian College"},
            new SchoolName {ID = 66,Name="University of Montreal"},
            new SchoolName {ID = 67,Name="Université of Québec at Montréal"},
            new SchoolName {ID = 68,Name="Laval University"},
            new SchoolName {ID = 69 ,Name="University of Sherbrooke"},
            new SchoolName {ID = 70 ,Name="McGill University"},
            new SchoolName {ID = 71 ,Name="University of Ottawa"},
            new SchoolName {ID = 72 ,Name="Cégep de l'Outaouais"},
            new SchoolName {ID = 73 ,Name="Cégep Limoilou"},
            new SchoolName {ID = 74 ,Name="Cégep Saint-Jean-sur-Richelieu"},
            new SchoolName {ID = 75 ,Name="Cégep de Saint-Jérôme"},
            new SchoolName {ID = 76 ,Name="Cégep Garneau"},
            new SchoolName {ID = 77 ,Name="Cégep Édouard-Montpetit"},
            new SchoolName {ID = 78,Name="Cégep de Sherbrooke"},
            new SchoolName {ID = 79,Name="Cégep André-Laurendeau"},
            new SchoolName {ID = 80,Name="Collège de Bois-de-Boulogne"},
            new SchoolName {ID = 81,Name="Collège Lionel-Groulx"},
            new SchoolName {ID = 82, Name="Cégep de Jonquière"}
            };
            schoolNames.ForEach(s => context.SchoolNames.Add(s));
            context.SaveChanges();
        }
    }

}