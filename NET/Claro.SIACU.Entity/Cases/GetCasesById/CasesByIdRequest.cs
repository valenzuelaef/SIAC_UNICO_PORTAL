namespace Claro.SIACU.Entity.Cases.GetCasesById
{
    public class CasesByIdRequest : Claro.Entity.Request
    {
        public string CaseId{ get; set; }
        public string ReportId { get; set; }
        public string ComplaintId { get; set; }
    }
}