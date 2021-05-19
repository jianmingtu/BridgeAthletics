using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Athletes.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Athletes.DAL
{
    public class AthletesContext : DbContext
    {

        public AthletesContext() : base("DefaultConnection")
        {
        }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<HighlightVideo> HighlightVideos { get; set; }
        public DbSet<AthleteHighlightVideo> AthleteHighlightVideos { get; set; }
       

        public DbSet<SchoolName> SchoolNames { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}