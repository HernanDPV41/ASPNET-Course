using Domain.Common;
using Domain.Students.Errors;

namespace Domain.Students.BusinessRules
{
    internal class EvaluationCannotBeUpdatedWithEmptyGrade(
        EvaluationGrade Grade)
        : IBusinessRule
    {
        public Result Check()
        {
            if (Grade.IsEmpty())
                return Result.WithError(
                    EvaluationErrors.UpdatedWithEmptyGrade);
            return Result.Success();
        }
    }
}
