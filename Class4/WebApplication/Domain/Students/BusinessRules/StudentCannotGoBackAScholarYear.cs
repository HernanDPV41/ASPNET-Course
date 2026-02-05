using Domain.Common;
using Domain.Students.Errors;

namespace Domain.Students.BusinessRules
{
    public class StudentCannotGoBackAScholarYear(
        int CurrentYear,
        int NewYear)
        : IBusinessRule
    {
        public Result Check()
        {
            if (CurrentYear > NewYear)
                return Result.WithError(
                    StudentErrors.UpdateToPreviousYear);
            return Result.Success();
        }
    }
}
