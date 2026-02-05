using Domain.Common;
using Domain.Students.Errors;

namespace Domain.Students.BusinessRules
{
    public class EvaluationGradeCannotBeLowerThan2(
        int Value)
        : IBusinessRule
    {
        public Result Check()
        {
            if (Value < 2)
                return Result.WithError(
                    EvaluationGradeErrors.GradeLowerThan2);
            return Result.Success();
        }
    }
}
