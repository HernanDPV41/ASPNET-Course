using Domain.Courses;
using Domain.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers.Data.Courses;
using WebApplication1.Controllers.Data.Students;
using WebApplication1.Helpers.Extensions;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController
        : ControllerBase
    {

        private readonly ILogger<StudentsController> _logger;

        private readonly StudentsContext _context;

        public CourseController(
            ILogger<StudentsController> logger,
            StudentsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCourse(
            [FromBody] CreateCourseDTO dto,
            CancellationToken cancellationToken)
        {
            // Validando datos


            // Lógica de negocio

            bool isYearAlreadyUsed = _context.Courses.Any(x => x.Year == dto.Year);

            var result = Course.Create(
                dto.Year,
                isYearAlreadyUsed,
                dto.Topics.Select(x =>
                new TopicRecord(
                    Guid.Empty,
                    x.Name,
                    x.TotalHours,
                    x.HasFinalTest))
                .ToList());

            if (result.IsFailure)
                return this.FromResult(result);

            try
            {
                _context.Courses.Add(result.Value!);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            // Devolviendo resultado exitoso
            return Created("api/courses", result.Value!.Id);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourseById(
            Guid id,
            CancellationToken cancellationToken)
        {
            // Validando datos

            // Lógica de negocio
            Course? course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (course is null)
                return NotFound($"The course with id {id} could not be found.");

            // Mapeando resultado a DTO
            CourseDTO dto = new CourseDTO(
                course.Id,
                course.Year,
                course.Topics.Select(x =>
                new TopicDTO(
                    x.Id,
                    x.Name,
                    x.TotalHours,
                    x.HasFinalTest))
                .ToList());

            // Devolviendo resultado exitoso
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> ListCourses(
            [FromQuery] CourseRequestParamenters parameters,
            CancellationToken cancellationToken)
        {
            // Validando datos

            // Lógica de negocio
            var query = _context.Courses.AsQueryable();
            if (parameters.Year != null)
                query = query.Where(x => x.Year == parameters.Year);

            List<Course> courses = await query.ToListAsync();

            // Mapeando resultado a DTO
            List<CourseDTO> coursesDTOs = [];
            foreach (var course in courses)
                coursesDTOs.Add(
                    new CourseDTO(
                        course.Id,
                        course.Year,
                        course.Topics.Select(x =>
                        new TopicDTO(
                            x.Id,
                            x.Name,
                            x.TotalHours,
                            x.HasFinalTest))
                        .ToList()));

            // Devolviendo resultado exitoso
            return Ok(coursesDTOs);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(
            Guid id,
            [FromBody] UpdateCourseDTO dto,
            CancellationToken cancellationToken)
        {
            // Validando datos
            
            // Lógica de negocio
            var course = _context.Courses.FirstOrDefault(x => x.Id == id);

            if (course is null)
                return NotFound($"The course with id {id} could not be found.");

            bool isYearAlreadyUsed = _context.Courses.Any(x => x.Year == dto.Year);

            var result = course.Update(
                    dto.Year,
                    isYearAlreadyUsed,
                    dto.Topics.Select(x =>
                new TopicRecord(
                    Guid.Empty,
                    x.Name,
                    x.TotalHours,
                    x.HasFinalTest))
                .ToList());

            if (result.IsFailure)
                return this.FromResult(result);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            // Devolviendo resultado exitoso
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(
            Guid id,
            CancellationToken cancellationToken)
        {
            // Validando datos

            // Lógica de negocio
            var course = _context.Courses.FirstOrDefault(x => x.Id == id);

            if (course is null)
                return NotFound($"The course with id {id} could not be found.");

            try
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            // Devolviendo resultado exitoso
            return NoContent();
        }
    }
}
