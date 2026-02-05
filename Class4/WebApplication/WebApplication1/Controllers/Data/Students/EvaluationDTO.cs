namespace WebApplication1.Controllers.Data.Students
{
    public sealed record EvaluationDTO(
         Guid Id,
         Guid StudentId,
         Guid CourseId,
         Guid TopicId,
         int Grade,
         int AttendanceHours);
}
