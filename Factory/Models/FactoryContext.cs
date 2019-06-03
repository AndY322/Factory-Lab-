using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SqlServer;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Factory.Models
{
    public class FactoryContext : IdentityDbContext<User>
    {
        public FactoryContext() : base("DefaultConnection")
        { }

        public static FactoryContext Create()
        {
            return new FactoryContext();
        }

        public DbSet<Detail> Details { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Routing> Routings { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<DetailStatus> Statuses { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
    }
}