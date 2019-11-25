using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace StudentLibrary.Data
{
    public class StudentContext:IdentityDbContext
    {
        public StudentContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }

        public DbSet<MODUser> MODUser { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<MentorSkill> MentorSkills { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
    }
}
