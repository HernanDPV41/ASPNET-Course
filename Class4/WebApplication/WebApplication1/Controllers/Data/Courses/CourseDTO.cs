namespace WebApplication1.Controllers.Data.Courses
{
    public sealed record CourseDTO(
        Guid Id,
        int Year,
        List<TopicDTO> Topics);
}
