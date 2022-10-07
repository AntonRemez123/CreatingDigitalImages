using ClosedXML.Excel;
using CreatingDigitalImages.Models;
using CreatingDigitalImages.ViewModels;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CreatingDigitalImages.Controllers
{
    public class HomeController : Controller
    {

        private AppDbContext db;

        public HomeController(AppDbContext context)
        {
            db = context;
        }


        
        public IActionResult Index(string name)
        {

            var items = db.CDI.ToList();
            if (name != null)
            {
                 items = db.CDI.Where(p => p.Parent.Equals(name)).ToList();
            }
            else
            {
                 items = db.CDI.Where(p => p.Id.Equals(1)).ToList();
            }

            var dvm = new DirectoryViewModel
            {
                directory = items,
                parent = name
            };
            return View(dvm);

        }

        public IActionResult UploadData(IFormFile file)
        {
            try
            {

                if (file == null)
                    throw new Exception("Нет файла");
                var fileextension = Path.GetExtension(file.FileName);
                var filename = Guid.NewGuid().ToString() + fileextension;
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", filename);
                using (FileStream fs = System.IO.File.Create(filepath))
                {
                    file.CopyTo(fs);
                }
                int rowno = 1;
                XLWorkbook workbook = XLWorkbook.OpenFromTemplate(filepath);
                var sheets = workbook.Worksheets.First();
                var rows = sheets.Rows().ToList();
                foreach (var row in rows)
                {
                    if (rowno != 1)
                    {
                        var test = row.Cell(1).Value.ToString();
                        if (string.IsNullOrWhiteSpace(test) || string.IsNullOrEmpty(test))
                        {
                            break;
                        }
                        CDI directory;
                        directory = db.CDI.Where(s => s.Name == row.Cell(1).Value.ToString()).FirstOrDefault();
                        if (directory == null)
                        {
                            directory = new CDI();
                        }
                        directory.Name = row.Cell(1).Value.ToString();
                        directory.Parent = row.Cell(2).Value.ToString();
                        if (directory.Id.ToString() == Guid.Empty.ToString())
                            db.CDI.Add(directory);
                        else
                            db.CDI.Update(directory);
                    }
                    else
                    {
                        rowno = 2;
                    }
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                return RedirectToAction("Index");
            }
        }

        public IActionResult ExportData()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Users");
            var currentRow = 1;

            worksheet.Cell(currentRow, 1).Value = "Name";
            worksheet.Cell(currentRow, 2).Value = "Parent";

            foreach (var user in db.CDI)
            {
                currentRow++;

                worksheet.Cell(currentRow, 1).Value = user.Name;
                worksheet.Cell(currentRow, 2).Value = user.Parent;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
        }

    }
}
