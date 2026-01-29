using Domain.Common;
using Domain.Students.Errors;

namespace Domain.Students.BusinessRules
{
    public class StudentCannotGoBackAScholarYear(
        int currentYear,
        int newYear)
        : IBusinessRule
    {
        public Result Check()
        {
            if (currentYear > newYear)
                return Result.WithError(
                    StudentErrors.UpdateToPreviousYear);
            return Result.Success();
        }
    }
}
