using EsemipoSirius.Database;
using EsemipoSirius.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EsemipoSirius.Pages
{
    public class TempModel : PageModel
    {
        ElencoDispositiviDisponibili dbDispositivi;
        DBTemperatura dbTemp;
        public List<string> NomiDispositivi { get; set; }
        public List<TemperaturaDisp> temperaturaDisp { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NomeDevice { get; set; }
        
        [BindProperty]
        public List<float?> hubTemp { get; set; }
        public List<float?> TopTemp { get; set; }
        public List<float?> AmbTemp { get; set; }
        public List<float?> OilTemp { get; set; }
        public List<float?> OilPress { get; set; }
        public List<DateTime?> Date { get; set; }
        public TempModel()
        {
            dbTemp = new DBTemperatura();
            dbDispositivi = new ElencoDispositiviDisponibili();
            temperaturaDisp = new List<TemperaturaDisp>();
            NomiDispositivi = new List<string>();
            hubTemp = new List<float?>();
            TopTemp = new List<float?>();
            AmbTemp = new List<float?>();
            OilTemp = new List<float?>();
            OilPress = new List<float?>();
            Date = new List<DateTime?>();
        }
        public void OnGet()
        {
            NomiDispositivi = dbDispositivi.DispositiviDisponibili();
            temperaturaDisp = dbTemp.getAllTemperature(NomeDevice);
            foreach (TemperaturaDisp a in temperaturaDisp)
            {
                hubTemp.Add(a.HubTemp);
                TopTemp.Add(a.TopTemp);
                AmbTemp.Add(a.AmbTemp);
                OilTemp.Add(a.OilTemp);
                OilPress.Add(a.OilPress);
                Date.Add(a.Date);
            }
        }
    }
}
