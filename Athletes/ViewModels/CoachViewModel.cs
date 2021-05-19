using Athletes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Athletes.ViewModels
{
    public class CoachViewModel
    {
        public Coach Coach { get; set; }
        public List<string> HighlightVideos { get; set; }
    }
}