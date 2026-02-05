namespace WebApplication1.Controllers.Data.Students
{
    public sealed record StudentDTO(
        Guid Id,
        Guid CourseId,
        string Names,
        string LastNames,
        long IdNumber,
        int ScholarYear,
        List<EvaluationDTO> Evaluations);
}
