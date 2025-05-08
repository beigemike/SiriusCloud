using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EsemipoSirius.Models;
using EsemipoSirius.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EsemipoSirius.Pages
{
    public class IndexModel : PageModel
    {
        ActivePower dbActivePower = new ActivePower();
        ElencoDispositiviDisponibili dbDispositivi = new ElencoDispositiviDisponibili();

        public List<ActivePowerDevice> dispositivoActivePower { get; set; }
        public List<string> NomiDispositivi { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NomeDevice { get; set; }

        [BindProperty]
        public List<DateTime?> Date { get; set; }
        public List<float?> ActivePower { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            dispositivoActivePower = new List<ActivePowerDevice>();
            NomiDispositivi = new List<string>();

            Date = new List<DateTime?>();
            ActivePower = new List<float?>();
        }

        public void OnGet()
        {
            NomiDispositivi = dbDispositivi.DispositiviDisponibili();
            dispositivoActivePower = dbActivePower.getAll(NomeDevice);

            foreach (ActivePowerDevice a in dispositivoActivePower)
            {
                Date.Add(a.Date);
                ActivePower.Add(a.ActivePower);
            }

        }
    }
}

