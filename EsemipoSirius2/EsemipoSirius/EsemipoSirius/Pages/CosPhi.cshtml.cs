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

        [BindProperty(SupportsGet = true)]
        public float media { get; set; }     
        
        [BindProperty(SupportsGet = true)]
        public float Mancante { get; set; }

        public CosPhiModel()
        {
            media = 0;
            Mancante = 1;
        }

        public void OnGet()
        {
            NomiDispositivi = dispositiviDisponibili.DispositiviDisponibili();
            media = dbcosphi.getMediaCosPhi(NomeDevice);
            if (float.IsNaN(media) || float.IsInfinity(media))
            {
                media = 0;
            }
            Mancante = 1 - media;
        }
    }
}
