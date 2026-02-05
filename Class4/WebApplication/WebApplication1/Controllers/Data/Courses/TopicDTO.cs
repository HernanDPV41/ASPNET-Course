namespace WebApplication1.Controllers.Data.Courses
{
    public sealed record TopicDTO(
            Guid Id,
            string Name,
            int TotalHours,
            bool HasFinalTest);
}
