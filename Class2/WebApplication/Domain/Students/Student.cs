using Domain.Common;

namespace Domain.Students
{
    public class Student
        : Entity
    {
        public string Names { get; private set; } = string.Empty;

        public string LastNames { get; private set; } = string.Empty;

        public IdNumber IdNumber { get; private set; } = new IdNumber(0);

        public int ScholarYear { get; private set; }

        private Student()
        {
        }

        public Student(
            Guid id,
            string names,
            string lastNames,
            long idNumber,
            int scholarYear)
            : base(id)
        {

            IdNumber ci = new IdNumber(idNumber);
            var studentAge = ci.GetAge();

            if (studentAge < 17 || studentAge > 65)
                throw new Exception($"Student age must be between 17 and 65.");

            Names = names;
            LastNames = lastNames;
            IdNumber = ci;
            ScholarYear = scholarYear;
        }

        public void Update(
            string names,
            string lastNames,
            int scholarYear)
        {
            Names = names;
            LastNames = lastNames;
            ScholarYear = scholarYear;
        }
    }
}
