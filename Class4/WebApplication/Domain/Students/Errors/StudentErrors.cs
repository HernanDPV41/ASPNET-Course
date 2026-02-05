using Domain.Common;

namespace Domain.Students.Errors
{
    public static class StudentErrors
    {
        public static Error UpdateToPreviousYear
            => new Error(
                "Sudent.UpdateToPreviousYear",
                ErrorType.Conflict,
                "Students cannot go " +
                    "back to a previous scholar year.");

        public static Error UpdateMoreThanOneYear
            => new Error(
                "Sudent.UpdateMoreThanOneYear",
                ErrorType.Conflict,
                "Students cannot increase " +
                    "more than one scholar year at the same time.");

        public static Error UnderAge
            => new Error(
                "Student.UnderAge",
                ErrorType.Validation,
                "Student age cannot be lesser than 17.");

        public static Error OverAge
            => new Error(
                "Student.OverAge",
                ErrorType.Validation,
                "Student age cannot be greater than 65.");

        public static Error UpdateNotMatriculatedEvaluation
            => new Error(
                "Student.UpdateNotMatriculatedEvaluation",
                ErrorType.Conflict,
                "Cannot update an evaluation that the student is not currently matriculated.");

    }
}
