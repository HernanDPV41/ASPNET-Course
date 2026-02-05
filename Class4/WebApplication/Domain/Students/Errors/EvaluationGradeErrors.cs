using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Students.Errors
{
    public static class EvaluationGradeErrors
    {
        public static Error GradeGreaterThan5
            => new Error(
                "A grade cannot be greater than 5",
                ErrorType.Validation,
                "Grade.GreaterThan5");

        public static Error GradeLowerThan2
            => new Error(
                "A grade cannot be lower than 2",
                ErrorType.Validation,
                "Grade.LowerThan2");

    }
}
