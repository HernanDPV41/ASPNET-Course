using Domain.Common;
using Domain.Students.Errors;

namespace Domain.Students.BusinessRules
{
    public class StudentCannotPassMoreThanOneYearAtTheSameTime(
        int currentYear,
        int newYear)
        : IBusinessRule
    {
        public Result Check()
        {
            if (currentYear + 1 < newYear)
                return Result.WithError(
                    StudentErrors.UpdateMoreThanOneYear);
            return Result.Success();
        }
    }
}
