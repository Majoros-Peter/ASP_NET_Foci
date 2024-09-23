using System.IO;
using foci.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace foci.Pages
{
    public class AdatokFeltolteseFajlbolModel : PageModel
    {
        private readonly IWebHostEnvironment _env;
        private readonly FociDBContext _context;

        public AdatokFeltolteseFajlbolModel(IWebHostEnvironment env, FociDBContext context)
        {
            _env = env;
            _context = context;
        }

        [BindProperty]
        public IFormFile Feltoltes { get; set; }
        public char Separator { get; set; } = ';';
        public bool Header { get; set; } = true;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string uploadDirPath = Path.Combine(_env.ContentRootPath, "uploads");
            string uploadFilePath = Path.Combine(uploadDirPath, Feltoltes.FileName);
            using(FileStream stream = new(uploadFilePath, FileMode.Create))
            {
                await Feltoltes.CopyToAsync(stream);
            }

            using(StreamReader sr = new(uploadFilePath))
            {
                if(Header)
                    sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] elemek = line.Split(Separator);
                    Meccs meccs = new()
                    {
                        Fordulo = int.Parse(elemek[0]),
                        HazaiFelido = int.Parse(elemek[1]),
                        VendegFelido = int.Parse(elemek[2]),
                        HazaiVeg = int.Parse(elemek[3]),
                        VendegVeg = int.Parse(elemek[4]),
                        HazaiNev = elemek[5],
                        VendegNev = elemek[6]
                    };
                    if (!_context.Meccsek.ToList().Any(meccs.UgyazanE))
                        _context.Add(meccs);
                }
                sr.Close();
            }
            _context.SaveChanges();
            System.IO.File.Delete(uploadFilePath);
            return Page();
        }
    }
}
