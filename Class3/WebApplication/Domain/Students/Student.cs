using Domain.Common;
using Domain.Students.BusinessRules;
using Domain.Students.Errors;

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

        private Student(
            Guid id,
            string names,
            string lastNames,
            IdNumber idNumber,
            int scholarYear)
            : base(id)
        {
            Names = names;
            LastNames = lastNames;
            IdNumber = idNumber;
            ScholarYear = scholarYear;
        }

        public static Result<Student> Create(
            string names,
            string lastNames,
            long idNumber,
            int scholarYear)   
        {

            IdNumber ci = new IdNumber(idNumber);
            var studentAge = ci.GetAge();

            var result = CheckRules(
                new StudentMustBeOlderThan17(studentAge),
                new StudentMustBeYoungerThan65(studentAge));

            if (result.IsFailure)
                return result;

            return Result.Success(
                new Student(
                    Guid.NewGuid(),
                    names,
                    lastNames,
                    ci,
                    scholarYear));
        }

        public Result Update(
            string names,
            string lastNames,
            int scholarYear)
        {
            var result = CheckRules(
                new StudentCannotGoBackAScholarYear(ScholarYear,scholarYear),
                new StudentCannotPassMoreThanOneYearAtTheSameTime(ScholarYear,scholarYear));

            Names = names;
            LastNames = lastNames;
            ScholarYear = scholarYear;

            return Result.Success();
        }
    }
}
