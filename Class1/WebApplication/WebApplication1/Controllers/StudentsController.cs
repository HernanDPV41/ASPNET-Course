using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers.Data;
using WebApplication1.Models;
using WebApplication1.Persistence;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController
        : ControllerBase
    {

        private readonly ILogger<StudentsController> _logger;

        private readonly StudentsContext _context;

        public StudentsController(
            ILogger<StudentsController> logger,
            StudentsContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateStudent(
            [FromBody] CreateStudentDTO dto,
            CancellationToken cancellationToken)
        {
            // Validando datos
            if (dto.IdNumber < 10000000000 || dto.IdNumber > 99999999999)
                return BadRequest("Id number must have 11 digits.");
            if(dto.ScholarYear < 1 || dto.ScholarYear > 5)
                return BadRequest("Schollar year must be between 1 and 5, including them.");
            if(string.IsNullOrEmpty(dto.Names))
                return BadRequest("Names property is required.");
            if(string.IsNullOrEmpty(dto.LastNames))
                return BadRequest("Lastnames property is required.");

            // Lógica de negocio
            var student = new Student(
                Guid.NewGuid(),
                dto.Names,
                dto.LastNames,
                dto.IdNumber,
                dto.ScholarYear);

            // Validando reglas de negocio (parte de la lógica de negocio)
            long studentBirthYear = student.IdNumber / 1000000000; // Obteniendo las primeras dos cifras del carnet
            studentBirthYear = studentBirthYear > 50 ? 1900 + studentBirthYear : 2000 + studentBirthYear; // Convirtiendo esas dos cifras al año de nacimiento
            long studentAge = DateTime.Now.Year - studentBirthYear; // calculando edad
            if (studentAge < 17 || studentAge > 65)
                return BadRequest($"Student age must be between 17 and 65.");

            try
            {
                _context.Students.Add(student);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            // Devolviendo resultado exitoso
            return Created("api/students", student.Id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudentById(
            Guid id, 
            CancellationToken cancellationToken)
        {
            // Validando datos

            // Lógica de negocio
            Student? student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (student is null)
                return NotFound($"The student with id {id} could not be found.");

            // Mapeando resultado a DTO
            StudentDTO dto = new StudentDTO(
                student.Id,
                student.Names,
                student.LastNames,
                student.IdNumber,
                student.ScholarYear);

            // Devolviendo resultado exitoso
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> ListStudents(
            [FromQuery]StudentsRequestParameters parameters,
            CancellationToken cancellationToken)
        {
            // Validando datos
            if (parameters.ScholarYear != null && (
                parameters.ScholarYear > 5 ||
                parameters.ScholarYear < 1))
                return BadRequest("Scholar year filter must be between 1 and 5.");

            // Lógica de negocio
            var query =  _context.Students.AsQueryable();
            if (parameters.ScholarYear != null)
                query = query.Where(x => x.ScholarYear == parameters.ScholarYear);

            List<Student> students = await query.ToListAsync();

            // Mapeando resultado a DTO
            List<StudentDTO> studentDTOs = [];
            foreach (var student in students)
                studentDTOs.Add(
                    new StudentDTO(
                        student.Id,
                        student.Names,
                        student.LastNames,
                        student.IdNumber,
                        student.ScholarYear));

            // Devolviendo resultado exitoso
            return Ok(studentDTOs);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(
            Guid id,
            [FromBody] UpdateStudentDTO dto,
            CancellationToken cancellationToken)
        {
            // Validando datos
            if (dto.ScholarYear < 1 || dto.ScholarYear > 5)
                return BadRequest("Schollar year must be between 1 and 5, including them.");
            if (string.IsNullOrEmpty(dto.Names))
                return BadRequest("Names property is required.");
            if (string.IsNullOrEmpty(dto.LastNames))
                return BadRequest("Lastnames property is required.");

            // Lógica de negocio
            var student = _context.Students.FirstOrDefault(x => x.Id == id);
            
            if(student is null)
                return NotFound($"The student with id {id} could not be found.");

            student.Names = dto.Names;
            student.LastNames = dto.LastNames;
            student.ScholarYear = dto.ScholarYear;

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
        public async Task<ActionResult> DeleteStudent(
            Guid id,
            CancellationToken cancellationToken)
        {
            // Validando datos

            // Lógica de negocio
            var student = _context.Students.FirstOrDefault(x => x.Id == id);

            if (student is null)
                return NotFound($"The student with id {id} could not be found.");

            try
            {
                _context.Students.Remove(student);
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
