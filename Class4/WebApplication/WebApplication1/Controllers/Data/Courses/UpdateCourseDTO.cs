namespace WebApplication1.Controllers.Data.Courses
{
    public sealed record UpdateCourseDTO(
       int Year,
       List<TopicDTO> Topics);
}
