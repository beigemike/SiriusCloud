using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EsemipoSirius.Models;
using EsemipoSirius.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EsemipoSirius.Pages
{
    public class IndexModel : PageModel
    {
        ElencoDispositiviDisponibili dbDispositivi = new ElencoDispositiviDisponibili();
        ActivePower dbActivePower = new ActivePower();
        DBDispositiviLuoghi dbDis= new DBDispositiviLuoghi();
        public List<DispositiviLuoghi> ElencoDispositivi { get; set; }
        public List<ActivePowerDevice> dispositivoActivePower { get; set; }
        public List<string> NomiDispositivi { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NomeDevice { get; set; }

        [BindProperty]
        public List<DateTime?> Date { get; set; }
        public List<float?> ActivePower { get; set; }
        public List<string> Plant { get; set; }
        public List<int> NumDisp { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            dispositivoActivePower = new List<ActivePowerDevice>();
            NomiDispositivi = new List<string>();
            ElencoDispositivi = new List<DispositiviLuoghi>();
            Date = new List<DateTime?>();
            ActivePower = new List<float?>();
            Plant = new List<string>();
            NumDisp = new List<int>();
        }

        public void OnGet()
        {
            NomiDispositivi = dbDispositivi.DispositiviDisponibili();
            ElencoDispositivi = dbDis.getNumDispositivi();
            dispositivoActivePower = dbActivePower.getAll(NomeDevice);
            foreach (DispositiviLuoghi a in ElencoDispositivi) 
            {
                Plant.Add(a.Plant);
                NumDisp.Add(a.NumDevice);
            }
            foreach (ActivePowerDevice a in dispositivoActivePower)
            {
                Date.Add(a.Date);
                ActivePower.Add(a.ActivePower);
            }



        }
    }
}

