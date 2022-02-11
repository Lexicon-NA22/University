#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data.Data;
using University.Entities;
using University.Web.Filters;

namespace University.Web.Controllers
{
    [ModelNotNull]
    [NullRefferenseExceptionFilter]
    public class StudentsController : Controller
    {
        private readonly UniversityContext db;
        private readonly IMapper mapper;
        private readonly Faker faker;

        public StudentsController(UniversityContext context, IMapper mapper)
        {
            db = context;
            this.mapper = mapper;
            faker = new Faker("sv");
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
           // var m = db.Student.Where(s => EF.Property<DateTime>(s, "Edited") >= DateTime.Now.AddDays(-1));

            var model = mapper.ProjectTo<StudentIndexViewModel>(db.Student)
                              .OrderByDescending(s => s.Id)
                              .Take(10);

            return View(await model.ToListAsync());
        }

        // GET: Students/Details/5
        [RequiredParam("id")]
        //[ModelNotNull]
        public async Task<IActionResult> Details(int? id)
        {
            var student = await mapper.ProjectTo<StudentDetailsViewModel>(db.Student)
                                      .FirstOrDefaultAsync(s => s.Id == id);

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            var model = new StudentCreateViewModel();
            return View(model);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelIsValid]
        public async Task<IActionResult> Create(StudentCreateViewModel viewModel)
        {
           
                //var student = new Student(faker.Internet.Avatar(), viewModel.Email, new Name(viewModel.FirstName, viewModel.LastName))
                //{
                //    Adress = new Adress
                //    {
                //        City = viewModel.City,
                //        Street = viewModel.Street,
                //        ZipCode = viewModel.ZipCode
                //    }
                //};

                var student = mapper.Map<Student>(viewModel);
                student.Avatar = faker.Internet.Avatar();

                db.Add(student);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
          //  return View(viewModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await mapper.ProjectTo<StudentEditViewModel>(db.Student)
                                .FirstOrDefaultAsync(s =>s.Id == id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  var student = await db.Student.Include(s => s.Adress)
                                                .FirstOrDefaultAsync(s => s.Id == id);

                  mapper.Map(viewModel, student);

                   // db.Entry(student).Property("Edited").CurrentValue = DateTime.Now;

                    db.Update(student);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(viewModel.Id))
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
            return View(viewModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await db.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await db.Student.FindAsync(id);
            db.Student.Remove(student);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return db.Student.Any(e => e.Id == id);
        }
    }
}
