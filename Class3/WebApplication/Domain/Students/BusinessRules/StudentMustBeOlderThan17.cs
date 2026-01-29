using Domain.Common;
using Domain.Students.Errors;

namespace Domain.Students.BusinessRules
{
    public class StudentMustBeOlderThan17(
        long Age)
        : IBusinessRule
    {
        public Result Check()
        {
            if (Age < 17)
                return Result.WithError(
                    StudentErrors.UnderAge);
            return Result.Success();
        }
    }


}
