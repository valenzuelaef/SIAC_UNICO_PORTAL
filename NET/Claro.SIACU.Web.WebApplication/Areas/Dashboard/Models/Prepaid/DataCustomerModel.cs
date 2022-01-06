
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Prepaid
{
    public class DataCustomerModel
    {
        public DataCustomerModel() { }

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string TypeDocument { get; set; }
        public string NumberDocument { get; set; }
        public string TelephoneCustomer { get; set; }
        public string State { get; set; }
        public string Segment { get; set; }
        public string VoucherElectronical { get; set; }
        public bool Membership { get; set; }
       
        public string Modality { get; set; }
        public string Sex { get; set; }
        public string DateBirth { get; set; }
        public string PlaceBirth { get; set; }
        public string TelephoneReference { get; set; }
        public string StateCivil { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string EmailConfirmation { get; set; }
        public string Ocupation { get; set; }
        public string Position { get; set; }
        public string Role { get; set; }
        public string Registered { get; set; }
        public string ReasonRegistry { get; set; }
        public string Urbanization { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Department { get; set; }
        public string Reference { get; set; }
        public string BannerMessage { get; set; }
        public DataServiceModel oDataService { get; set; }
        public string ResponseService { get; set; }
        public string SiteId { get; set; }
        public string CustomerCode { get; set; }

        
        public string ContactCode { get; set; }
        public string Segment2 { get; set; }
        public string ZipCode { get; set; }
        public string StateContact { get; set; }
        public string FlagEmail { get; set; }
        public int FlagRegister { get; set; }
        public string SiteCode { get; set; }
        public string Account { get; set; }
        public string User { get; set; }
        public string FullName { get; set; }

        public string ContractID { get; set; }
        public string DNIRUC { get; set; }
        public string Province { get; set; }
        public string BusinessName { get; set; }
        public string LegalAgent { get; set; }
        public string CustomerType { get; set; }

        public string Transaction { get; set; }
        public string PermissionInteraction { get; set; }
        public string ContingencyClarify { get; set; }
        public string Application { get; set; }
        public string BlackList { get; set; }

        public string ProductType { get; set; } 

        public bool IsAppMiclaro { get; set; }
        public string AppMiclaroVersion { get; set; }
        public string AppMiClaroLastDate { get; set; }
    }
}