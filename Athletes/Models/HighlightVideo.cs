using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Athletes.Models
{
    // Prior to today, the table "School" has a field called HighlightVideos, which contains video links. However, it volates the first rule (or form) of normalization
    // that your table must represent a relation, meanly that this rule applies "Every row/column intersection must contain one and only one value. Therefore,
    // the table "HightlightVideo" is created today to store all video links
    public class HighlightVideo
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int SchoolId { get; set; }
        virtual public School School { get; set; }
    }
}