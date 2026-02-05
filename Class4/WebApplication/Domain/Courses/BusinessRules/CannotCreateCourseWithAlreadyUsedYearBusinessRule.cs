using Domain.Common;
using Domain.Courses.Errors;

namespace Domain.Courses.BusinessRules
{
    internal class CannotCreateCourseWithAlreadyUsedYearBusinessRule(
        bool YearIsAlreadyUsed)
        : IBusinessRule
    {
        public Result Check()
        {
            if (YearIsAlreadyUsed)
                return Result.WithError(
                    CourseErrors.YearAlreadyUsed);
            return Result.Success();
        }
    }
}
