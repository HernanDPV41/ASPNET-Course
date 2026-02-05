using Domain.Common;

namespace Domain.Courses.Errors
{
    public static class CourseErrors
    {

        public static Error YearAlreadyUsed =>
            new Error("Course.YearAlreadyUsed",
                ErrorType.Validation,
                "Cannot create a course of a year that already has a course defined.");

    }
}
