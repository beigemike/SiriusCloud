using EsemipoSirius.Database;
using EsemipoSirius.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EsemipoSirius.Pages
{
    public class CosPhiModel : PageModel
    {
        DBCosPhi dbcosphi = new DBCosPhi();
        public ElencoDispositiviDisponibili dispositiviDisponibili { get; set; }
        public List<EfficienzaDispositivo> efficienzaDispositivo { get; set; }

        public List<string> NomiDispositivi { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NomeDevice { get; set; }

        [BindProperty(SupportsGet = true)]
        public CosPhi mediaCosPhi { get; set; }     
        
        [BindProperty(SupportsGet = true)]
        public float Mancante { get; set; }

        public CosPhiModel()
        {
            Mancante = 1;
            dispositiviDisponibili = new ElencoDispositiviDisponibili();
            efficienzaDispositivo = new List<EfficienzaDispositivo>();
        }

        public void OnGet()
        {
            NomiDispositivi = dispositiviDisponibili.DispositiviDisponibili();
            mediaCosPhi = dbcosphi.getMediaCosPhi(NomeDevice);
            Mancante = 1 - mediaCosPhi.ValoreCosPhi;
            efficienzaDispositivo = dbcosphi.getEfficienzaCosPhi(NomeDevice);
        }
    }
}
