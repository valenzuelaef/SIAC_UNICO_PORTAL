
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountCreditLimit
{
    public class AccountCreditLimit
    {
        public string Fecha_Marca_SMS1 { get; set; }
        public string Fecha_Marca_SMS2 { get; set; }
        public string Tiene_Pac { get; set; }
        public decimal Monto_Pac { get; set; }
        public string Numero_Pac { get; set; }
        public string Fecha_Bloq_Programada { get; set; }
        public string Fecha_Bloq_Ejecucion { get; set; }
        public decimal Monto_Consumido { get; set; }
        public decimal Cargo_Fijo { get; set; }
        public decimal Cargo_Adicional { get; set; }
        public decimal Cargo_Prorrat { get; set; }
        public string Fecha_Desbloq_Ejecucion { get; set; }
        public string Account { get; set; }
        public string CustomerId { get; set; }
        public string BusinessName { get; set; }
    }
}