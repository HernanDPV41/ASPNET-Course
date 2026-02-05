using Domain.Common;
using Domain.Courses;
using Domain.Students.BusinessRules;

namespace Domain.Students
{
    public class Student
        : Entity
    {
        public string Names { get; private set; } = string.Empty;

        public string LastNames { get; private set; } = string.Empty;

        public IdNumber IdNumber { get; private set; } = new IdNumber(0);

        public int ScholarYear { get; private set; }

        public Guid CourseId { get; private set; } = Guid.Empty;

        private List<Evaluation> _evaluations = [];
        public IReadOnlyCollection<Evaluation> Evaluations
        {
            get => _evaluations;
            private set
            {
                _evaluations = new(value);
            }
        }

        private Student()
        {
        }

        private Student(
            Guid id,
            string names,
            string lastNames,
            IdNumber idNumber,
            int scholarYear)
            : base(id)
        {
            Names = names;
            LastNames = lastNames;
            IdNumber = idNumber;
            ScholarYear = scholarYear;
        }

        public static Result<Student> Create(
            string names,
            string lastNames,
            long idNumber,
            int scholarYear)
        {

            IdNumber ci = new IdNumber(idNumber);
            var studentAge = ci.GetAge();

            var result = CheckRules(
                new StudentMustBeOlderThan17(studentAge),
                new StudentMustBeYoungerThan65(studentAge));

            if (result.IsFailure)
                return result;

            return Result.Success(
                new Student(
                    Guid.NewGuid(),
                    names,
                    lastNames,
                    ci,
                    scholarYear));
        }

        public Result Update(
            string names,
            string lastNames,
            int scholarYear,
            IEnumerable<EvaluationRecord> evaluations)
        {
            var result = CheckRules(
                new StudentCannotGoBackAScholarYear(ScholarYear, scholarYear),
                new StudentCannotPassMoreThanOneYearAtTheSameTime(ScholarYear, scholarYear),
                new CannotUpdateNotMatriculatedEvaluation(_evaluations, evaluations));

            if (result.IsFailure)
                return result;

            Names = names;
            LastNames = lastNames;
            ScholarYear = scholarYear;

            // Actualizando evaluaciones
            foreach(var evaluation in _evaluations)
            {
                var newEvaluation = evaluations.FirstOrDefault(x => x.Id == evaluation.Id);
                if (newEvaluation is null)
                    continue;

                var evaluationResult = EvaluationGrade.Create(
                    newEvaluation.Grade);
                
                if (evaluationResult.IsFailure)
                {
                    Result.Merge(result, evaluationResult.ToResult());
                    continue;
                }

                evaluation.Update(
                    evaluationResult.Value!,
                    newEvaluation.AttendanceHours);
            }

            return Result.Success();
        }

        public Result Matriculate(
            Course course)
        {
            CourseId = CourseId;

            _evaluations.Clear();

            foreach(var topic in course.Topics)
            {
                var result = Evaluation.Create(
                    Id,
                    course.Id,
                    topic.Id);

                if (result.IsFailure)
                    return result.ToResult();

                _evaluations.Add(result.Value!);
            }

            return Result.Success();
        }
        
    }
}
