namespace WebApplication1.Controllers.Data.Students
{
    public sealed record UpdateStudentDTO(
        string Names,
        string LastNames,
        int ScholarYear,
        List<EvaluationDTO> Evaluations);
}
