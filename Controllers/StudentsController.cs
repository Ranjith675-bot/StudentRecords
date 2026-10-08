using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRecordsApi.Data;
using StudentRecordsApi.Models;

namespace StudentRecordsApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // ============================================
        // 1. ADD STUDENT
        // POST: api/students
        // ============================================

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            // Check duplicate roll number
            bool rollNumberExists = await _context.Students
                .AnyAsync(s => s.RollNumber == student.RollNumber);

            if (rollNumberExists)
            {
                return Conflict(new
                {
                    message = "Roll number already exists."
                });
            }

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return Ok(student);
        }


        // ============================================
        // 2. GET ALL STUDENTS
        // GET: api/students
        // ============================================

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _context.Students
                .Select(s => new
                {
                    s.Id,
                    s.RollNumber,
                    s.Name,
                    s.Course,
                    s.Mark1,
                    s.Mark2,
                    s.Mark3,

                    Total = s.Mark1 + s.Mark2 + s.Mark3,

                    Average = (s.Mark1 + s.Mark2 + s.Mark3) / 3.0,

                    Result =
                        ((s.Mark1 + s.Mark2 + s.Mark3) / 3.0 >= 40)
                        ? "Pass"
                        : "Fail"
                })
                .ToListAsync();

            return Ok(students);
        }


        // ============================================
        // 3. GET STUDENT BY ID
        // GET: api/students/1
        // ============================================

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _context.Students
                .Where(s => s.Id == id)
                .Select(s => new
                {
                    s.Id,
                    s.RollNumber,
                    s.Name,
                    s.Course,
                    s.Mark1,
                    s.Mark2,
                    s.Mark3,

                    Total = s.Mark1 + s.Mark2 + s.Mark3,

                    Average = (s.Mark1 + s.Mark2 + s.Mark3) / 3.0,

                    Result =
                        ((s.Mark1 + s.Mark2 + s.Mark3) / 3.0 >= 40)
                        ? "Pass"
                        : "Fail"
                })
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound(new
                {
                    message = "Student not found."
                });
            }

            return Ok(student);
        }


        // ============================================
        // 4. UPDATE STUDENT
        // PUT: api/students/1
        // ============================================

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(
            int id,
            Student updatedStudent)
        {
            var student = await _context.Students
                .FindAsync(id);

            if (student == null)
            {
                return NotFound(new
                {
                    message = "Student not found."
                });
            }

            // Check duplicate roll number
            bool rollNumberExists = await _context.Students
                .AnyAsync(s =>
                    s.RollNumber == updatedStudent.RollNumber &&
                    s.Id != id);

            if (rollNumberExists)
            {
                return Conflict(new
                {
                    message = "Roll number already exists."
                });
            }

            student.RollNumber = updatedStudent.RollNumber;
            student.Name = updatedStudent.Name;
            student.Course = updatedStudent.Course;
            student.Mark1 = updatedStudent.Mark1;
            student.Mark2 = updatedStudent.Mark2;
            student.Mark3 = updatedStudent.Mark3;

            await _context.SaveChangesAsync();

            return Ok(student);
        }


        // ============================================
        // 5. DELETE STUDENT
        // DELETE: api/students/1
        // ============================================

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students
                .FindAsync(id);

            if (student == null)
            {
                return NotFound(new
                {
                    message = "Student not found."
                });
            }

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Student deleted successfully."
            });
        }
    }
}
