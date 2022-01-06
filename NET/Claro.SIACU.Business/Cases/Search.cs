using Claro.SIACU.Entity.Cases.GetCasesById;

namespace Claro.SIACU.Business.Cases
{
    public static class Search
    {
        public static CasesByIdResponse GetCasesByQueueUser(string strIdSession, string strTransaction, string strCaseId, string strReportId, string strComplaintId)
        {
            return new CasesByIdResponse()
            {
                Cases = Claro.SIACU.Data.Cases.Common.GetCasesById(strIdSession, strTransaction, strCaseId, strReportId, strComplaintId)
            }; 
        }
    }
}
