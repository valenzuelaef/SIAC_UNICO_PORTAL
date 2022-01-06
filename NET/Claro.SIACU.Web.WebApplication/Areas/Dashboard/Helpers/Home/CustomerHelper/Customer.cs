
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Home.CustomerHelper
{
    public class Customer
    {
        public Customer()
        {
            Surnames = "";
            Names = "";
            DocumentType = "";
            DocumentIdentity = "";
            RazonSocial = "";
        }
        public string Names { get; set; }
        public string Surnames { get; set; }
        public string DocumentType { get; set; }
        public string DocumentIdentity { get; set; }
        public string RazonSocial { get; set; }
    }
}