using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rengo.Data;
using Rengo.Models;
using Rengo.Models.ModelView;

namespace Rengo.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var departments = _context.Departments.Where(t=>t.Id!=1).Include(d => d.SubDepartments).ToList();
            return View(departments);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Departments department, IFormFile logo)
        {
 
            if (ModelState.IsValid)
            {
                if (_context.Departments.Any(t => t.Name == department.Name))
                {
                    ModelState.AddModelError(string.Empty, "A department with this name already exists.");

                    ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
                    var viewModel = new Department(); // Incorrect type

                    return View(viewModel);
                }
                if (logo != null && logo.Length > 0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/logos", logo.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await logo.CopyToAsync(stream);
                    }
                    department.LogoPath = $"/logos/{logo.FileName}";
                }
                // Create a new Department object
                Department newObj = new Department()
                {
                    Name = department.Name,
                    LogoPath = department.LogoPath,
                    ParentDepartmentId = department.ParentDepartmentId
                };

                // If ParentDepartmentId is not null, assign the ParentDepartment
                if (department.ParentDepartmentId != null)
                {
                    newObj.ParentDepartment = await _context.Departments
                        .FirstOrDefaultAsync(t => t.Id == department.ParentDepartmentId);
                }

                _context.Add(newObj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Console.WriteLine(errors);
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            return View(department);
        }


        public IActionResult Details(int id)
        {
            var department = _context.Departments
                .Include(d => d.SubDepartments)
                .Include(d => d.ParentDepartment)
                .FirstOrDefault(d => d.Id == id);

            if (department == null)
            {
                return NotFound();
            }

            var parentDepartments = new List<Department>();
            var current = department;
            while (current.ParentDepartment != null)
            {
                current = _context.Departments
                    .Include(d => d.ParentDepartment)
                    .FirstOrDefault(d => d.Id == current.ParentDepartmentId);

                if (current != null)
                {
                    parentDepartments.Add(current);
                }
            }

            parentDepartments.Add(department);
            parentDepartments.Reverse();

            var allSubDepartments = _context.Departments
                .Where(d => d.ParentDepartmentId == id)
                .OrderBy(t => t.Id)
                .ToList();

            return View(new DepartmentDetailsViewModel
            {
                Department = department,
                ParentDepartments = parentDepartments,
                SubDepartments = allSubDepartments
            });
        }



    }

}
