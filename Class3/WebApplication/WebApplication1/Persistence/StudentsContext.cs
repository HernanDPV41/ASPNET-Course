using Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Persistence
{
    public sealed class StudentsContext
        : DbContext
    {

        public DbSet<Student> Students => Set<Student>();


        public StudentsContext() { }

        public StudentsContext(string connectionString)
            : base(GetOptions(connectionString)) { }

        public StudentsContext(DbContextOptions<StudentsContext> options)
           : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasKey(x => x.Id);
        }


        #region Helpers

        private static DbContextOptions GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder().UseNpgsql(connectionString).Options;
        }

        #endregion
    }
}
