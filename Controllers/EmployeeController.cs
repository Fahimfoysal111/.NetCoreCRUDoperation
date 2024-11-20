using EmployeeASPNET.Models;
using EmployeeASPNET.Models.Vm;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeASPNET.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext db;
        private readonly IWebHostEnvironment en;

        public EmployeeController(EmployeeDbContext db, IWebHostEnvironment en)
        {
            this.db = db;
            this.en = en;
        }
        public IActionResult Index()
        {
            var student = db.Employees?.Include(a => a.ProjectAssignments).ThenInclude(s => s.Project).OrderBy(s => s.EmployeeId).ToList();
            return View(student);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Project = new SelectList(db.Projects.ToList(), "ProjectId", "ProjectName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVm studentVm, int[] projectid)
        {

            if (ModelState.IsValid)
            {
                var student = new Employee()
                {
                    EmployeeName = studentVm.EmployeeName,
                    Age = studentVm.Age,
                    DateOfJoining = studentVm.DateOfJoining,
                    IsActive = studentVm.IsActive,
                };
                if (studentVm.imageFile != null)
                {
                    var imgName = DateTime.Now.Ticks.ToString() + Path.GetExtension(studentVm.imageFile.FileName);
                    var imgFile = en.WebRootPath + "/Images/" + imgName;
                    using (var strem = System.IO.File.Create(imgFile))
                    {
                        studentVm.imageFile.CopyTo(strem);
                    }
                    student.image = imgName;
                }
                foreach (var item in projectid)
                {
                    var a = new ProjectAssignment()
                    {
                        Employee = student,
                        EmployeeId = student.EmployeeId,
                        ProjectId = item,
                    };
                    await db.ProjectAssignments.AddAsync(a);
                }
                //await db.Students.AddAsync(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(studentVm);
        }
        public async Task<IActionResult> AddSubject(int? id)
        {
            ViewBag.Project = new SelectList(await db.Projects.ToListAsync(), "ProjectId", "ProjectName", id ?? 0);
            return PartialView("_addSubject");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Project = new SelectList(await db.Projects.ToListAsync(), "ProjectId", "ProjectName");
            if (id != null)
            {
                var student = await db.Employees.FindAsync(id);
                var ad = db.ProjectAssignments.Where(s => s.EmployeeId == student.EmployeeId).ToList();
                var st = new EmployeeVm()
                {
                    EmployeeId = student.EmployeeId,
                    EmployeeName = student.EmployeeName,
                    Age = student.Age,
                    DateOfJoining = student.DateOfJoining,
                    image = student.image,
                    IsActive = student.IsActive
                };
                st.ProjectAssignments = ad;
                return View(st);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeVm studentVm, int[] projectid)
        {

            if (ModelState.IsValid)
            {
                var student = await db.Employees.FindAsync(studentVm.EmployeeId);
                var ad = db.ProjectAssignments.Where(s => s.EmployeeId == student.EmployeeId).ToList();

                student.EmployeeName = studentVm.EmployeeName;
                student.Age = studentVm.Age;
                student.DateOfJoining = studentVm.DateOfJoining;
                student.IsActive = studentVm.IsActive;

                if (studentVm.imageFile != null)
                {
                    var imgName = DateTime.Now.Ticks.ToString() + Path.GetExtension(studentVm.imageFile.FileName);
                    var imgFile = en.WebRootPath + "/Images/" + imgName;
                    using (var strem = System.IO.File.Create(imgFile))
                    {
                        studentVm.imageFile.CopyTo(strem);
                    }
                    student.image = imgName;
                }
                else
                {
                    student.image = student.image;
                }
                db.ProjectAssignments.RemoveRange(ad);
                foreach (var item in projectid)
                {
                    var a = new ProjectAssignment()
                    {
                        Employee = student,
                        EmployeeId = student.EmployeeId,
                        ProjectId = item,
                    };
                    await db.ProjectAssignments.AddAsync(a);
                }
                //await db.Students.AddAsync(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(studentVm);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var student = await db.Employees.FindAsync(id);
                var add = await db.ProjectAssignments.Where(a => a.EmployeeId == student.EmployeeId).ToListAsync();
                if (student != null)
                {
                    db.ProjectAssignments.RemoveRange(add);
                }
                db.Employees.Remove(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
