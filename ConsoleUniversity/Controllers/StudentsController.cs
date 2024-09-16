using ConstosoUniversity.Data;
using ConstosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConstosoUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }


        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,EnrollmentDate")] Student student) 
        {
            if (ModelState.IsValid) 
            { 
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(student);
        }


        public async Task<IActionResult> Delete(int? id) 
        {
            if (id == null) 
            {
                return NotFound();
            }

            var student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);

            if (student == null) 
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) 
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToEdit = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);
            if (studentToEdit == null)
            {
                return NotFound();
            }
            return View(studentToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,LastName,FirstName,EnrollmentDate")] Student modifiedStudent)
        {
            if (ModelState.IsValid)
            {
                if (modifiedStudent.ID == null)
                {
                    return BadRequest();
                }
                _context.Students.Update(modifiedStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(modifiedStudent);
        }

        [HttpGet]
        public async Task<IActionResult> Clone([Bind("ID,LastName,FirstName,EnrollmentDate")] Student selectedStudent, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var existingStudent = Details(id);
            return View(existingStudent);
        }
    }
}
