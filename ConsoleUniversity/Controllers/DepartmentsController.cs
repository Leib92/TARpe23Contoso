using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly SchoolContext _context;

        public DepartmentsController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var schoolContext = _context.Departments.Include(d => d.Administrator);
            return View(await schoolContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            string query = "SELECT * FROM Departments WHERE DepartmentId = {0}";
            var department = await _context.Departments
                .FromSqlRaw(query, id)
                .Include(d => d.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Budget,StartDate,RowVersion,InstructorID,ForeignStudent,SummonedSkeleton")] Department department)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "FullName", department.InstructorID);
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var departmentToEdit = await _context.Departments
                .Include(i => i.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentToEdit == null) { return NotFound(); }

            ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "FullName", departmentToEdit.InstructorID);
            return View(departmentToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, byte[] rowVersion)
        {
            ModelState.Remove("ForeignStudent");
            ModelState.Remove("RowVersion");
            ModelState.Remove("Courses");

            var departmentToUpdate = await _context.Departments
                .Include(i => i.Administrator)
                .FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (departmentToUpdate == null)
            {
                Department departmentIsDeleted = new Department();
                await TryUpdateModelAsync(departmentIsDeleted);
                ModelState.AddModelError(string.Empty, "Department already removed.");
                ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "FullName", departmentIsDeleted.InstructorID);
                return View(departmentIsDeleted);
            }

            _context.Entry(departmentToUpdate).Property("RowVersion").OriginalValue = rowVersion;
            var tryUpdate = await TryUpdateModelAsync<Department>(departmentToUpdate,
                "",
                s => s.Name,
                s => s.StartDate,
                s => s.Budget,
                s => s.InstructorID,
                s => s.ForeignStudent,
                s => s.SkeletonsSummoned);

            if (tryUpdate)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Department)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();

                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Department already removed.");
                    }
                    else
                    {
                        var databaseValues = (Department)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name) { ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}"); }
                        if (databaseValues.StartDate != clientValues.StartDate) { ModelState.AddModelError("StartDate", $"Current value: {databaseValues.StartDate}"); }
                        if (databaseValues.Budget != clientValues.Budget) { ModelState.AddModelError("Budget", $"Current value: {databaseValues.Budget}"); }
                        if (databaseValues.ForeignStudent != clientValues.ForeignStudent) { ModelState.AddModelError("ForeignStudent", $"Current value: {databaseValues.ForeignStudent}"); }
                        if (databaseValues.SkeletonsSummoned != clientValues.SkeletonsSummoned) { ModelState.AddModelError("SkeletonsSummoned", $"Current value: {databaseValues.SkeletonsSummoned}"); }
                        if (databaseValues.InstructorID != clientValues.InstructorID)
                        {
                            Instructor databaseHasThisInstructor = await _context.Instructors.FirstOrDefaultAsync(i => i.Id == databaseValues.InstructorID);
                            ModelState.AddModelError("Name", $"Current vlaue: {databaseValues.InstructorID}");
                        }
                        ModelState.AddModelError(string.Empty, "WARNING! Changes you are about to save differ from the info in the DB.");
                        departmentToUpdate.RowVersion = databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "Fullname", departmentToUpdate.InstructorID);
            return View(departmentToUpdate);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var departmentToDelete = await _context.Departments
                .Include(i => i.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentToDelete == null) { return NotFound(); }

            ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "FullName", departmentToDelete.InstructorID);
            return View(departmentToDelete);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            // add method to remove InstructorID from existing Department
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BaseOn([Bind("Name,Budget,StartDate,RowVersion,InstructorID,ForeignStudent,SummonedSkeleton")] Department department)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "FullName", department.InstructorID);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BaseOn(int? id)
        {
            if (id == null) { return NotFound(); }

            var departmentToBaseOn = await _context.Departments
                .Include(i => i.Administrator)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartmentID == id);
            if (departmentToBaseOn == null) { return NotFound(); }

            ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "FullName", departmentToBaseOn.InstructorID);
            return View(departmentToBaseOn);
        }
    }
}
