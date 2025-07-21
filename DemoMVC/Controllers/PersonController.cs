using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models;
using DemoMVC.Models.Process;
using OfficeOpenXml;
using X.PagedList.Extensions;

namespace DemoMVC.Controllers
{
    public class PersonController : Controller
    {
        // private readonly AutoGenerateId autoGenerateId;
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index(int? page, int? PageSize)
        {
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="3", Text="3"},
                new SelectListItem() { Value="5", Text="5"},
                new SelectListItem() { Value="10", Text="10"},
                new SelectListItem() { Value="15", Text="15"},
                new SelectListItem() { Value="25", Text="25"},
                new SelectListItem() { Value="50", Text="50"},
            };
            int pagesize = (PageSize ?? 3);
            ViewBag.psize = pagesize;
            var model = _context.Persons.ToList().ToPagedList(page ?? 1, 5);
            return View(model);
            // var model = await _context.Person.ToListAsync();
            // return View(model);
            // return View(await _context.Persons.ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            AutoGenerateId autoGenerateId = new AutoGenerateId();

            // Lấy ra bản ghi mới nhất của Person
            var person = _context.Persons.OrderByDescending(p => p.PersonId).FirstOrDefault();
            // Nếu person == null thi gan PersonId = P0000
            var personId = person == null ? "P0000" : person.PersonId;
            // Gọi tới phương thức sinh id tự động
            var newPersonId = autoGenerateId.GenerateId(personId);
            var newPerson = new Person
            {
                PersonId = newPersonId,
                FullName = string.Empty,
                Email = string.Empty,
                Address = string.Empty
            };
            return View(newPerson);
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("PersonID,FullName,Email,Address")] Person person)
        {
            if (string.IsNullOrEmpty(person.PersonId))
            {
                // Gán lại mã mới nếu bị null (phòng trường hợp người dùng không nhập được)
                var last = await _context.Persons
                    .OrderByDescending(p => p.PersonId)
                    .Select(p => p.PersonId)
                    .FirstOrDefaultAsync();

                var lastId = last ?? "P0000";
                var autoGen = new AutoGenerateId();
                person.PersonId = autoGen.GenerateId(lastId);
            }

            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Email,Address")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
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
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Uploads/Excels

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    // Đảm bảo thư mục tồn tại
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "Excels");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                    //rename file when upload to server
                    var fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + fileExtension;
                    var filePath = Path.Combine(folderPath, fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        //read data from excel file fill DataTable
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //using for loop to read data from dt
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //create new Person object
                            var ps = new Person()
                            {

                                FullName = string.Empty,
                                Email = string.Empty,
                                Address = string.Empty
                            };

                            //set value to attributes
                            ps.PersonId = dt.Rows[i]["PersonId"].ToString();
                            ps.FullName = dt.Rows[i]["FullName"].ToString();
                            ps.Email = dt.Rows[i]["Email"].ToString();
                            ps.Address = dt.Rows[i]["Address"].ToString();
                           
                            //add object to context
                            _context.Add(ps);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }

        public IActionResult Download()
        {
            //Name the file when downloading
            var fileName = "YourFileName" + ".xlsx";
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                //add some text to cell A1
                worksheet.Cells["A1"].Value = "PersonId";
                worksheet.Cells["B1"].Value = "FullName";
                worksheet.Cells["C1"].Value = "Email";
                worksheet.Cells["D1"].Value = "Address";
                //get all Person
                var personList = _context.Persons.ToList();
                //fill data to worksheet
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                //download file
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        private bool PersonExists(string id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }
    }
}
