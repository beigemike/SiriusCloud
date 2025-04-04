using EsemipoSirius.Database;
using EsemipoSirius.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EsemipoSirius.Pages
{
    public class nominalPowerModel : PageModel
    {

        DBDispositivo db = new DBDispositivo();
        public List<Dispositivo> elencoDispDaPw { get; set; }
        
        private readonly ILogger<IndexModel> _logger;

        [BindProperty(SupportsGet = true)]
        public float valoreNominalPower { get; set; }

        public nominalPowerModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            elencoDispDaPw = new List<Dispositivo>();
        }

        public void OnGet()
        {
            elencoDispDaPw = db.GetDispositiviDaNominalPower(valoreNominalPower);
        }
    }
}
