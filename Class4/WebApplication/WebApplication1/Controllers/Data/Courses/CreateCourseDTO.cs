namespace WebApplication1.Controllers.Data.Courses
{
    public sealed record CreateCourseDTO(
        int Year,
        List<TopicDTO> Topics);
}
