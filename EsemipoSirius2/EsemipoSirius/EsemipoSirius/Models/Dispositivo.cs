namespace EsemipoSirius.Models
{
    public class Dispositivo
    {
        public int IdDevice { get; set; }
        public string? Device { get; set; }
        public float? altitudine { get; set; }
        public float? latitudine { get; set; }
        public float? longitudine { get; set; }
        public float? year { get; set; }
        public string? up { get; set; }
        public string? notes { get; set; }
        public float? nominalPower { get; set; }
        public string? techAvailability { get; set; }
        public string? contrAvailability { get; set; }
        public string? municipality { get; set; }
        public int? IdType_FK { get; set; }
        public int? IdLinea_FK { get; set; }
        public int? IdSubsystem_FK { get; set; }
        public int? IdModel_FK { get; set; }
        public int? IdVendor_FK { get; set; }
        public int? IdSEE_FK { get; set; }
        public int? IdEcName_FK { get; set; }
        public int? IdPlant_FK { get; set; }
    }
}
