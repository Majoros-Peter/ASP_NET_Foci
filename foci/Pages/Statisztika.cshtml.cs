using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using foci.Models;

namespace foci.Pages
{
    public class StatisztikaModel : PageModel
    {
        private readonly foci.Models.FociDBContext _context;

        public StatisztikaModel(foci.Models.FociDBContext context)
        {
            _context = context;
        }

        public IList<Meccs> Meccs { get;set; } = default!;
        public List<Csapat> Stat { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Meccs = await _context.Meccsek.ToListAsync();

            Stat = Meccs.Select(G => G.HazaiNev)
                .Distinct()
                .Select(G => new Csapat()
                {
                    CsapatNev = G,
                    JatszottMerkozesekSzama = Meccs.Count(x => G == x.HazaiNev || G == x.VendegNev),
                    Nyert = Meccs.Count(x => x.GyoztesCsapatNeve() == G),
                    Dontetlen = Meccs.Count(x => x.GyoztesCsapatNeve() == ""),
                    Vesztett = Meccs.Count(x => x.VesztesCsapatNeve() == G),
                    LottGolok = Meccs.Where(x => x.HazaiNev == G).Sum(x => x.HazaiVeg) + Meccs.Where(x => x.VendegNev == G).Sum(x => x.VendegVeg),
                    KapottGolok = Meccs.Where(x => x.HazaiNev == G).Sum(x => x.VendegVeg) + Meccs.Where(x => x.VendegNev == G).Sum(x => x.HazaiVeg)
                })
                .Union(
                Meccs.Select(G => G.VendegNev)
                .Distinct()
                .Select(G => new Csapat()
                {
                    CsapatNev = G,
                    JatszottMerkozesekSzama = Meccs.Count(x => G == x.HazaiNev || G == x.VendegNev),
                    Nyert = Meccs.Count(x => x.GyoztesCsapatNeve() == G),
                    Dontetlen = Meccs.Count(x => x.GyoztesCsapatNeve() == ""),
                    Vesztett = Meccs.Count(x => x.VesztesCsapatNeve() == G),
                    LottGolok = Meccs.Where(x => x.HazaiNev == G).Sum(x => x.HazaiVeg) + Meccs.Where(x => x.VendegNev == G).Sum(x => x.VendegVeg),
                    KapottGolok = Meccs.Where(x => x.HazaiNev == G).Sum(x => x.VendegVeg) + Meccs.Where(x => x.VendegNev == G).Sum(x => x.HazaiVeg)
                }))
                .ToList();

            Stat = Stat.OrderBy(G => G.Pontszam)
                       .Select((G, i) => { G.Helyezes = i + 1; return G; })
                       .ToList();
        }
    }
}
