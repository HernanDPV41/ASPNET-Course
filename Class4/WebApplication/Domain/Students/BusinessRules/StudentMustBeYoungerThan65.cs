using Domain.Common;
using Domain.Students.Errors;

namespace Domain.Students.BusinessRules
{
    internal class StudentMustBeYoungerThan65(
        long Age)
        : IBusinessRule
    {
        public Result Check()
        {
            if (Age > 65)
                return Result.WithError(
                    StudentErrors.OverAge);
            return Result.Success();
        }
    }
}
