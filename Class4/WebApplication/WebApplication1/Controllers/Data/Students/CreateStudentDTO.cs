namespace WebApplication1.Controllers.Data.Students
{
    public sealed record CreateStudentDTO(
        string Names,
        string LastNames,
        long IdNumber,
        int ScholarYear);
}
