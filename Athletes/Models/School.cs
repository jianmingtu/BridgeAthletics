using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Athletes.Models
{
    public class School
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string CoachPhilosophy { get; set; }

        public string MensCoach { get; set; }   // the coach can edit the School Mens Profile
        public string WomensCoach { get; set; } // the coach can edit the School Womens Profile
    }
}