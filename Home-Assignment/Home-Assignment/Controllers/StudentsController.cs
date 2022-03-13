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
        const int Minimum_age = 0;
        const int Maximum_age = 18;

        public StudentsController(MvcStudentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            return await _context.Student.ToListAsync();
        }


        // GET: api/Students/5
        [HttpGet("GetStudent")]
        public async Task<ActionResult<List<Student>>> GetStudent(string first_name = null, string last_name = null, double age = -1, double gpa = -1, string name_of_school = null, string school_address = null)
        {
            List<Student> studentsResult = new List<Student>();
            //await _context.Student.Where(student => student.first_name == first_name).ForEachAsync(stuntAfterFilter => studentsResult.Add(stuntAfterFilter));
            //await _context.Student.Where(student => student.last_name == last_name).ForEachAsync(stuntAfterFilter => studentsResult.Add(stuntAfterFilter));
            //await _context.Student.Where(student => student.age == age).ForEachAsync(stuntAfterFilter => studentsResult.Add(stuntAfterFilter));
            //await _context.Student.Where(student => student.gpa == gpa).ForEachAsync(stuntAfterFilter => studentsResult.Add(stuntAfterFilter));
            //await _context.Student.Where(student => student.name_of_school == name_of_school).ForEachAsync(stuntAfterFilter => studentsResult.Add(stuntAfterFilter));
            //await _context.Student.Where(student => student.school_address == school_address).ForEachAsync(stuntAfterFilter => studentsResult.Add(stuntAfterFilter));

            studentsResult.AddRange( _context.Student.Where(student => student.first_name == first_name 
            || student.last_name == last_name 
            || student.age == age
            || student.gpa == gpa
            || student.name_of_school == name_of_school
            || student.school_address == school_address)
                .Distinct().ToList());
            //studentsResult.AddRange(_context.Student.Where(student => student.last_name == last_name));
            //studentsResult.AddRange(_context.Student.Where(student => student.age == age));
            //studentsResult.AddRange(_context.Student.Where(student => student.gpa == gpa));
            //studentsResult.AddRange(_context.Student.Where(student => student.name_of_school == name_of_school));
            //studentsResult.AddRange(_context.Student.Where(student => student.school_address == school_address));

            if (studentsResult == null)
            {
                return NotFound();
            }
            else if (!RedisService.isTheDurationTooShort("GetStudent"))
            {
                return Content("This query was called 10 seconds ago");
            }
            return studentsResult;
        }

        // PUT: api/Students/5
        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(Student student)
        {
            if (student.id != student.id)
            {
                return BadRequest();
            }
            _context.Entry(student).State = EntityState.Modified;

            try
            {
                if (student.age <= Maximum_age && student.age >= Minimum_age)
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
                if (!StudentExists(student.id))
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
        [HttpPost("CreateStudent")]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            if (student.age <= Maximum_age && student.age >= Minimum_age)
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
        public async Task<IActionResult> DeleteStudent(long id)
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

        private bool StudentExists(long id)
        {
            return _context.Student.Any(student => student.id == id);
        }


    }
}
