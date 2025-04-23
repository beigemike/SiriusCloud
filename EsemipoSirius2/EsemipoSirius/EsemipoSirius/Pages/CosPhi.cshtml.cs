using EsemipoSirius.Database;
using EsemipoSirius.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EsemipoSirius.Pages
{
    public class CosPhiModel : PageModel
    {
        ElencoDispositiviDisponibili dispositiviDisponibili = new ElencoDispositiviDisponibili();
        DBCosPhi dbcosphi = new DBCosPhi();

        public List<string> NomiDispositivi { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NomeDevice { get; set; }

        public float? media { get; set; }

        public void OnGet()
        {
            NomiDispositivi = dispositiviDisponibili.DispositiviDisponibili();
            media = dbcosphi.getMediaCosPhi(NomeDevice);

        }
    }
}
