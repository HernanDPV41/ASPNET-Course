namespace WebApplication1.Controllers.Data
{
    public sealed record StudentDTO(
        Guid Id,
        string Names,
        string LastNames,
        long IdNumber,
        int ScholarYear);
}
