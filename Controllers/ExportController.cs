using ClosedXML.Excel;
using ExportApp.Data;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System.IO;

namespace ExportApp.Controllers
{
    public class ExportController : Controller
    {
        private readonly SiswaRepository _repo;

        public ExportController(IConfiguration config)
        {
            _repo = new SiswaRepository(config);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ExportToPdf()
        {
            var data = _repo.GetRanking();
            return new ViewAsPdf("PdfView", data)
            {
                FileName = "report.pdf"
            };
        }

        public IActionResult ExportToExcel()
        {
            var data = _repo.GetRanking();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Ranking Siswa");

            // Header
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Nama";
            worksheet.Cell(1, 3).Value = "Nilai";
            worksheet.Cell(1, 4).Value = "Peringkat";

            // Data
            for (int i = 0; i < data.Count; i++)
            {
                var s = data[i];
                worksheet.Cell(i + 2, 1).Value = s.Id;
                worksheet.Cell(i + 2, 2).Value = s.Nama;
                worksheet.Cell(i + 2, 3).Value = s.Nilai;
                worksheet.Cell(i + 2, 4).Value = s.RankNilai;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(), 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                "report.xlsx");
        }
    }
}
