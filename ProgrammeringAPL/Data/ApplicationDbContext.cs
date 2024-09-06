﻿using BookManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProgrammeringAPL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Book> Books { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
