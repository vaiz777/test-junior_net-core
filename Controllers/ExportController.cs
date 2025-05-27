using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Rotativa.AspNetCore;
using ExportApp.Models;

namespace ExportApp.Controllers
{
    public class ExportController : Controller
    {
        public IActionResult Index() => View();

        // Export PDF dari view HTML
        public IActionResult ExportToPdf()
        {
            var data = GetDummyData();
            return new ViewAsPdf("PdfView", data)
            {
                FileName = "report.pdf"
            };
        }

        // Export Excel
        public IActionResult ExportToExcel()
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Laporan Siswa");

            worksheet.Cell(1, 1).Value = "Nama";
            worksheet.Cell(1, 2).Value = "Nilai";

            var data = GetDummyData();
            for (int i = 0; i < data.Count; i++)
            {
                worksheet.Cell(i + 2, 1).Value = data[i].Nama;
                worksheet.Cell(i + 2, 2).Value = data[i].Nilai;
            }

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0; // rewind sebelum dikirim ke client

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
        }

        private List<Siswa> GetDummyData() => new()
        {
            new Siswa { Nama = "Andi", Nilai = 85 },
            new Siswa { Nama = "Budi", Nilai = 90 },
            new Siswa { Nama = "Citra", Nilai = 88 }
        };

        // ✅ Hanya satu definisi
        public class Siswa
        {
            public string Nama { get; set; } = string.Empty;
            public int Nilai { get; set; }
        }
    }
}
