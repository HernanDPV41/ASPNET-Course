using Domain.Common;
using Domain.Students.Errors;

namespace Domain.Students.BusinessRules
{
    public class EvaluationGradeCannotBeGreaterThan5(
        int Value)
        : IBusinessRule
    {
        public Result Check()
        {
            if (Value > 5)
                return Result.WithError(
                    EvaluationGradeErrors.GradeGreaterThan5);
            return Result.Success();
        }
    }
}
