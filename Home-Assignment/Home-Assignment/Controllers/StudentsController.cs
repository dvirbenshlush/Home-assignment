using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Home_Assignment.Data;
using Home_Assignment.Models;
using Home_Assignment.Dal;

namespace Home_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly MvcStudentContext _context;

        public StudentsController(MvcStudentContext context)
        {
            _context = context;
        }

        // GET: api/Students/5
        [HttpGet("GetStudent")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            else if (!RedisService.isTheDurationTooShort("GetStudent"))
            {
                return Content("This query was called 10 seconds ago");
            }
            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.id)
            {
                return BadRequest();
            }
            _context.Entry(student).State = EntityState.Modified;

            try
            {
                if (student.age <= 18)
                {
                    if (!RedisService.isTheDurationTooShort("UpdateStudent"))
                    {
                        return Content("This query was called in the last 10 seconds");
                    }
                    else
                    {
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    return Content("The age of the student must be between 0-18");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateStudent")]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            if (student.age <= 18)
            {
                _context.Student.Add(student);
                try
                {
                    if (!RedisService.isTheDurationTooShort("GetStudent"))
                    {
                        return Content("This query was called in the last 10 seconds");
                    }
                    else
                    {
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateException)
                {
                    if (StudentExists(student.id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetStudent", new { id = student.id }, student);
            }
            else
            {
                return Content("The student age must be between 0-18");
            }
        }

        // DELETE: api/Students/5
        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.id == id);
        }


    }
}
