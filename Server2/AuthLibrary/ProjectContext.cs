using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AuthLibrary
{
    public class ProjectContext: IdentityDbContext
    {
        public ProjectContext([NotNullAttribute] DbContextOptions options) : base(options) { }

        // TODO: to be implemented in case of skill or course
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<MovieActors>().HasKey(ma => new { ma.MovieId, ma.ActorId });
            builder.Entity<IdentityRole>(r => r.HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "Admin" },
                new IdentityRole { Id = "2", Name = "Student", NormalizedName = "Student" },
                new IdentityRole { Id = "3", Name = "Mentor", NormalizedName = "Mentor" }
            ));
            base.OnModelCreating(builder);
        }
        public DbSet<MODUser> MODUser { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<MentorSkill> MentorSkills { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
    }
}
