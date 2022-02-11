using Microsoft.AspNetCore.Mvc;
using University.Entities;

namespace University.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UniversityContext db;

        public CoursesController(UniversityContext context)
        {
            db = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var courses = db.Course.Select(c => new {c.Title, Start = EF.Property<DateTime>(c, "PeriodStart")}).ToList();

            var all = db.Course.TemporalAll();

            var ch = db.Course.TemporalAll()
                       .Where(c => c.Title == "Seamless systemic website")
                       .Select(c => new
                       {
                           c.Title,
                           Added = EF.Property<DateTime>(c, "PeriodStart").ToString()
                       })
                       .OrderBy(c => c.Added);

            return View(await db.Course.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View(new CreateCourseViewModel(""));
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var course = new Course(viewModel.Title);
                db.Add(course);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(course);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await db.Course
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await db.Course.FindAsync(id);
            db.Course.Remove(course);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return db.Course.Any(e => e.Id == id);
        }
    }
}
