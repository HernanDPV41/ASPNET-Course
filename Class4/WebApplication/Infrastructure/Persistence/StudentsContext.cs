using Domain.Courses;
using Domain.Students;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Persistence
{
    public sealed class StudentsContext
        : DbContext
    {

        public DbSet<Student> Students => Set<Student>();

        public DbSet<Course> Courses => Set<Course>();

        public StudentsContext() { }

        public StudentsContext(string connectionString)
            : base(GetOptions(connectionString)) { }

        public StudentsContext(DbContextOptions<StudentsContext> options)
           : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseNpgsql());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasKey(x => x.Id);
            modelBuilder.Entity<Student>().OwnsOne(x => x.IdNumber);
            modelBuilder.Entity<Student>()
                .Navigation(x => x.Evaluations)
                .AutoInclude();

            modelBuilder.Entity<Evaluation>().HasKey(x => x.Id);
            modelBuilder.Entity<Evaluation>()
                .OwnsOne(x => x.Grade)
                .Ignore(x => x.IsSuspense);


            modelBuilder.Entity<Course>().HasKey(x => x.Id);
            modelBuilder.Entity<Course>()
                .Navigation(x => x.Topics)
                .AutoInclude();

            modelBuilder.Entity<Topic>().HasKey(x => x.Id);

        }


        #region Helpers

        private static DbContextOptions GetOptions(string connectionString)
        {
            return new DbContextOptionsBuilder().UseNpgsql(connectionString).Options;
        }

        #endregion
    }
}
