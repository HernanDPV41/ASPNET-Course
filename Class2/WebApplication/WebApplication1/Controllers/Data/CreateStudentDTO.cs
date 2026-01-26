namespace WebApplication1.Controllers.Data
{
    public sealed record CreateStudentDTO(
        string Names,
        string LastNames,
        long IdNumber,
        int ScholarYear);
}
