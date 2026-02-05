using Microsoft.EntityFrameworkCore;

namespace DBExamples.Example1
{
    public class SampleContext
        : DbContext
    {

        public DbSet<SampleClass> SampleClasses => Set<SampleClass>();

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

            modelBuilder.Entity<SampleClass>().HasKey(x => x.IntegerProperty);

        }


        #region Helpers

        private static DbContextOptions GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder().UseNpgsql(connectionString).Options;
        }

        #endregion
    }
}
