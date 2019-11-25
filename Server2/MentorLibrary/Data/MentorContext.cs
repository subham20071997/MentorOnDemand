using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MentorLibrary.Data
{
    public class MentorContext : IdentityDbContext
    {
        public MentorContext([NotNullAttribute] DbContextOptions options) : base(options)
        {

        }

        public DbSet<Technology> Technologies { get; set; }
        public DbSet<MODUser> MODUser { get; set; }
        public DbSet<MentorSkill> MentorSkills { get; set; }
    }
}
