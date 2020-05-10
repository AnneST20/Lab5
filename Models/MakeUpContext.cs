using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Models
{
    public class MakeUpContext : DbContext
    {
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Firm> Firms { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<_Color> Colors { get; set; }
        public virtual DbSet<Cosmetic> Cosmetics { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductColor> ProductColors { get; set; }

        public MakeUpContext(DbContextOptions<MakeUpContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
