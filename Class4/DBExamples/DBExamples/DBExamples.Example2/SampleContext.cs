using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExamples.Example2
{
    public class SampleContext
        : DbContext
    {

        public DbSet<A> As => Set<A>();

        public DbSet<B> Bs => Set<B>();

        public SampleContext() { }

        public SampleContext(string connectionString)
            : base(GetOptions(connectionString)) { }

        public SampleContext(DbContextOptions<SampleContext> options)
           : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<A>().HasKey(x => x.Id);

            modelBuilder.Entity<B>().HasKey(x => x.Id);

        }


        #region Helpers

        private static DbContextOptions GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder().UseNpgsql(connectionString).Options;
        }

        #endregion
    }
}
