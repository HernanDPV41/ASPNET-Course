using Domain.Common;

namespace Domain.Students.Errors
{
    internal static class EvaluationErrors
    {
        public static Error UpdatedWithEmptyGrade
            => new Error("Evaluation.UpdatedWithEmptyGrade",
                ErrorType.Validation,
                "An evaluation cannot be updated with an empty grade.");
    }
}
