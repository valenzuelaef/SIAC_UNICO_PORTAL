
namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid
{
    public class DataCustomerModel
    {
        public string Application { get; set; }
        public string ValueSearch { get; set; }
        public string OriginType { get; set; }
        public bool ChangeApplication { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string AddressNote { get; set; }
        public string CustomerID { get; set; }
        public string ContractID { get; set; }
        public string PaymentMethod { get; set; }
        public string Account { get; set; }
        public string Reference { get; set; }
        public string District { get; set; }
        public string Departament { get; set; }
        public string InvoiceCode { get; set; }
        public string Email { get; set; }
        public string PhoneReference { get; set; }
        public string BusinessName { get; set; }
        public string Tradename { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeId { get; set; }
        public string Assessor { get; set; }
        public string CustomerType { get; set; }
        public string CustomerTypeCode { get; set; }
        public string Province { get; set; }
        public string LegalAgent { get; set; }
        public string Segment2 { get; set; }
        public string PostalCode { get; set; }
        public string Position { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string CivilStatus { get; set; }
        public string CustomerContact { get; set; }
        public string Fax { get; set; }
        public string PhoneContact { get; set; }
        public string Country { get; set; }
        public string InvoiceCountry { get; set; }
        public string InvoiceAddress { get; set; }
        public string InvoiceUrbanization { get; set; }
        public string InvoiceDistrict { get; set; }
        public string InvoiceProvince { get; set; }
        public string InvoiceDepartament { get; set; }
        public string LegalCountry { get; set; }
        public string LegalAddress { get; set; }
        public string LegalUrbanization { get; set; }
        public string LegalDistrict { get; set; }
        public string LegalProvince { get; set; }
        public string LegalCode { get; set; }
        public string LegalDepartament { get; set; }
        public string InvoiceUbigeo { get; set; }
        public string InstallUbigeo { get; set; }
        public string BillingCycle { get; set; }
        public string OfficeAddress { get; set; }
        public string ActivationDate { get; set; }
        public bool Membership { get; set; }
        public string PlaneCode { get; set; }
        public string HubCode { get; set; }
        public string BannerMessage { get; set; }
        public string ProductType { get; set; }
        public string ProductTypeText { get; set; }
        public string PlaneCodeBilling { get; set; }
        public string PlaneCodeInstallation { get; set; }
        public string CodeCenterPopulate { get; set; }
        public string DescriptionCenterPopulate { get; set; }
        public string ContactCode { get; set; }
        public string SiteCode { get; set; }
        public string Modality { get; set; }
        public string Urbanization { get; set; }
        public string Sex { get; set; }
        public string CivilStatusID { get; set; }
        public string DNIRUC { get; set; }
        public string InvoicePostal { get; set; }
        public string LegalPostal { get; set; }
        public string CodCustomerType { get; set; }
        public string BirthPlaceID { get; set; }
        public bool IsLTE { get; set; }
        public string IdContactObject { get; set; }
        public string ContactFlag { get; set; }
        public string CodePlanTariff { get; set; }
        public string StateAgreement { get; set; }
        public string ContactCustomerCode { get; set; }
        public string ContactNumberReference1 { get; set; }
        public string ContactNumberReference2 { get; set; }
        public string ContactCntCode { get; set; }
        public string StatusCodeFullClaro { get; set; }
        public string StatusCodeFullClaroDesc { get; set; }
        public string csIdPub { get; set; }
        public string coIdPub { get; set; }
        public string flagConvivencia { get; set; }
        public bool IsAppMiclaro { get; set; }
        public string AppMiclaroVersion { get; set; }
        public string AppMiClaroLastDate { get; set; }
        public Itm itm { get; set; }
        public string origen { get; set; }
        public string apellido_pat_tobe { get; set; }
        public string apellido_mat_tobe { get; set; }
        public string indicadorCambioNumero { get; set; }

        

        public Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.Postpaid.DataAccountModel objPostDataAccount { get; set; }
        public DataCustomerModel()
        {
            objPostDataAccount = new DataAccountModel();
        }
        public string DirReferenciaFac { get; set; }


        //INICIATIVA 616
        public string telefonoTOBE { get; set; }        
    }
}