using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Athletes.Models
{
	public class Athlete
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public int Age { get; set; }
		public string Gender { get; set; }
		public string Height { get; set; } 
		public string SpikeTouch { get; set; }
		public string Position { get; set; }
		public string ImgUrl { get; set; }

		public Athlete(string id, string email, string firstName, string lastName, string position, DateTime dateOfBirth, string gender, string height, string spikeTouch, string imgurl)
		{
			Id = id;
			Email = email;
			FirstName =  firstName;
			LastName = lastName;
			Position = position;
			DateOfBirth = dateOfBirth;
			Age = dateOfBirth.Year;
			Gender = gender;
			Height = height;
			SpikeTouch = spikeTouch;
			ImgUrl = imgurl;
		}

		public Athlete() { }
	}
}
