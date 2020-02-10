using StartUp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace StartUp.Data
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        // public DbSet<ModelName> TableName { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
    }
}