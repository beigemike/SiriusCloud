using EsemipoSirius.Database;
using EsemipoSirius.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EsemipoSirius.Pages
{
    public class SpeedModel : PageModel
    {
        ElencoDispositiviDisponibili dispositiviDisponibili = new ElencoDispositiviDisponibili();
        DBRotorGeneratorWind dbSpeed = new DBRotorGeneratorWind();

        public List<RotorGeneratorWind> rotorGeneratorWind = new List<RotorGeneratorWind>(); 
        public List<string> NomiDispositivi { get; set; }

        [BindProperty(SupportsGet = true)]
        public string NomeDevice { get; set; }

        [BindProperty]
        public List<float?> rpmRotor { get; set; }
        public List<float?> WindSpeed { get; set; }

        public List<RotorSpeed> datiRotor = new List<RotorSpeed>();
        public List<generatorSpeed> datigenerator = new List<generatorSpeed>();



        public SpeedModel()
        {
            rpmRotor = new List<float?>();
            WindSpeed = new List<float?>();
        }

        public void OnGet()
        {
            NomiDispositivi = dispositiviDisponibili.DispositiviDisponibili();

            rotorGeneratorWind = dbSpeed.getRotorWindGenSpeed(NomeDevice);

            foreach(RotorGeneratorWind a in rotorGeneratorWind)
            {
                RotorSpeed coordinateRot = new RotorSpeed();
                generatorSpeed coordinateGen = new generatorSpeed();
                coordinateRot.x = a.RotorSpeed;
                coordinateRot.y = a.WindSpeed;
                coordinateGen.x = a.GeneratorSpeed;
                coordinateGen.y = a.WindSpeed;
                datiRotor.Add(coordinateRot);
                datigenerator.Add(coordinateGen);
            }

        }
    }
}
