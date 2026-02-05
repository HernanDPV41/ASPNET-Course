using Domain.Common;
using Domain.Students.BusinessRules;

namespace Domain.Students
{
    public class Evaluation
        : Entity
    {
        public Guid StudentId { get; private set; }

        public Guid CourseId { get; private set; }

        public Guid TopicId { get; private set; }

        public EvaluationGrade Grade { get; private set; } = EvaluationGrade.Empty;

        public int AttendanceHours { get; private set; } = 0;

        private Evaluation() { }

        private Evaluation(
            Guid studentId,
            Guid courseId, 
            Guid topicId)
        {
            StudentId = studentId;
            CourseId = courseId;
            TopicId = topicId;
        }

        public static Result<Evaluation> Create(
            Guid studentId,
            Guid courseId,
            Guid topicId)
        {
            return Result.Success(
                new Evaluation(
                    studentId,
                    courseId,
                    topicId));
        }

        public Result Update(
            EvaluationGrade grade,
            int attendanceHours)
        {
            var result = CheckRules(
                new EvaluationCannotBeUpdatedWithEmptyGrade(grade));

            if (result.IsFailure)
                return result;

            Grade = grade;
            AttendanceHours = attendanceHours;

            return Result.Success();
        }

    }

    public record EvaluationRecord(
        Guid Id,
        Guid StudentId,
        Guid CourseId,
        Guid TopicId,
        int Grade,
        int AttendanceHours);
    
}
