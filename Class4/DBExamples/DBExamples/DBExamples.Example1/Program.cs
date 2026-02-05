using Microsoft.EntityFrameworkCore;

namespace DBExamples.Example1
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
                "Database=SampleDB");

            context.Database.EnsureCreated();

            //var sample = new SampleClass()
            //{
            //    BoolProperty = true,
            //    DateTimeProperty = DateTime.UtcNow,
            //    DoubleProperty = 3.14,
            //    IntegerProperty = 1,
            //    TextProperty = String.Empty,
            //};

            //context.SampleClasses.Add(sample);
            //context.SaveChanges();

            //var sample = context.SampleClasses
            //    .AsNoTracking()
            //    .FirstOrDefault(x => x.IntegerProperty == 1);

            //if (sample is null)
            //    throw new Exception("Sample not found.");

            //sample.TextProperty = "Nuevo valor";

            //context.SampleClasses.Update(sample);

            //context.SaveChanges();

            //var samples = context.SampleClasses
            //    .Where(x => x.BoolProperty == true);

            var sample = context.SampleClasses
                .AsNoTracking()
                .FirstOrDefault(x => x.IntegerProperty == 1);

            if (sample is null)
                throw new Exception("Sample not found.");

            context.SampleClasses.Remove(sample);

            context.SaveChanges();




        }
    }
}
