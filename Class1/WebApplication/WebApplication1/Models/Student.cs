namespace WebApplication1.Models
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Names { get; set; } = string.Empty;

        public string LastNames { get; set; } = string.Empty;

        public long IdNumber {  get; set; } 

        public int ScholarYear { get; set; }

        public Student()
        {
        }

        public Student(
            Guid id,
            string names,
            string lastNames,
            long idNumber,
            int scholarYear)
        {
            Id = id;
            Names = names;
            LastNames = lastNames;
            IdNumber = idNumber;
            ScholarYear = scholarYear;
        }

    }
}
