using EsemipoSirius.Database;
using EsemipoSirius.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EsemipoSirius.Pages
{
    public class posizioneModel : PageModel
    {
        DBDispositivo db = new DBDispositivo();
        public List<Dispositivo> altLatLong { get; set; }
        [BindProperty(SupportsGet = true)]
        public string NomeDevice { get; set; }


        private readonly ILogger<IndexModel> _logger;

        public posizioneModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            altLatLong = new List<Dispositivo>();
        }
        public void OnGet()
        {
            altLatLong = db.getAtlLatLon(NomeDevice);
        }
    }
}
