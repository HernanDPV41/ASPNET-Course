using Domain.Common;
using Domain.Students.BusinessRules;

namespace Domain.Students
{
    public class EvaluationGrade
        : ValueObject
    {
        public static EvaluationGrade Empty =>
            new EvaluationGrade(0);

        public int Value { get; private set; }

        public bool IsSuspense => Value == 2;

        private EvaluationGrade() { }

        private EvaluationGrade(
            int value)
        {
            Value = value;
        }


        public static Result<EvaluationGrade> Create(
            int value)
        {
            var result = CheckRules(
                new EvaluationGradeCannotBeGreaterThan5(value),
                new EvaluationGradeCannotBeLowerThan2(value));

            if (result.IsFailure)
                return result;

            return Result.Success(
                new EvaluationGrade(value));
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value!;
        }

        public bool IsEmpty()
        {
            return Value == 0;
        }
    }
}
