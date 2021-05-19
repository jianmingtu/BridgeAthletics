using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Athletes.Models
{
    public class Coach
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Boolean HeadCoach { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //navigation properties
        // Coach must belong to a school
        public int SchoolID { get; set; }

        public virtual School School { get; set; }

        // need to define the default constructor; otherwise, crash
        public Coach() { }

        public Coach(string userId, string email,
            Boolean headCoach, string firstName, string lastName, int schoolID)
        {
            Id = userId;
            Email = email;
            HeadCoach = headCoach;
            FirstName = firstName;
            LastName = lastName;
            SchoolID = schoolID;
        }
    }
}