using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Athletes.Models
{
    // Prior to today, the table "School" has a field called HighlightVideos, which contains video links. However, it volates the first rule (or form) of normalization
    // that your table must represent a relation, meanly that this rule applies "Every row/column intersection must contain one and only one value. Therefore,
    // the table "HightlightVideo" is created today to store all video links
    public class AthleteHighlightVideo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UrlLink { get; set; }
        public string AthleteId { get; set; }
        virtual public Athlete Athlete { get; set; }
    }
}