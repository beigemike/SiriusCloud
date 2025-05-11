using EsemipoSirius.Database;
using EsemipoSirius.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EsemipoSirius.Pages
{
    public class WindModel : PageModel
    {
        ElencoDispositiviDisponibili dbDispositivi;
        dbWindDir dbDir;
        public List<string> NomiDispositivi { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NomeDevice { get; set; }
        
        [BindProperty]
        public List<WindDir> datiDir { get; set; }
        public List<WindDir> MediadatiDir { get; set; }
        public WindModel()
        {
            dbDispositivi = new ElencoDispositiviDisponibili();
            NomiDispositivi = new List<string>();
            dbDir = new dbWindDir();
            datiDir = new List<WindDir>();
        }
        public void OnGet()
        {
            NomiDispositivi = dbDispositivi.DispositiviDisponibili();
            datiDir = dbDir.getWindDir(NomeDevice);
            MediadatiDir = dbDir.getMediaDir(NomeDevice);
        }
    }
}
