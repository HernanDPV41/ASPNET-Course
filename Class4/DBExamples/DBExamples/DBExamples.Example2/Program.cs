using Microsoft.EntityFrameworkCore;

namespace DBExamples.Example2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SampleContext context = new SampleContext(
                "User ID =postgres;" +
                "Password=qwerty;" +
                "Server=localhost;" +
                "Port=5432;" +
                "Database=SampleDB2");

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.As.Add(new A()
            {
                Id = 1,
                B = new B()
                {
                    Id = 1
                }
            });

            context.SaveChanges();

            var a = context.As
                .Where(x => x.Id == 1)
                .Include(x => x.B);

        }
    }
}
