using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoronaManagementSystem.Models;

namespace CoronaManagementSystem2.Data
{
    public class CoronaManagementSystem2Context : DbContext
    {
        public CoronaManagementSystem2Context (DbContextOptions<CoronaManagementSystem2Context> options)
            : base(options)
        {
        }

        public DbSet<CoronaManagementSystem.Models.Vaccination> Vaccination { get; set; } = default!;

        public DbSet<CoronaManagementSystem.Models.Member>? Member { get; set; }
        public DbSet<CoronaManagementSystem.Models.Vaccinated>? Vaccinated { get; set; }
        public DbSet<CoronaManagementSystem.Models.CovidResultDates>? CovidResultDates { get; set; }
    }
}
