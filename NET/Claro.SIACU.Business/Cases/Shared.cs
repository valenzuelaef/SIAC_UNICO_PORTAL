using Claro.SIACU.Entity.Cases.GetCaseNotes;
using Claro.SIACU.Entity.Cases.GetSubCaseNotes;
using System;

namespace Claro.SIACU.Business.Cases
{
    public static class Shared
    {
        public static CaseNotesResponse GetCaseNotes(string strIdSession, string strTransaction, string strCaseId)
        {
            return new CaseNotesResponse()
            {
                CaseNotes = Claro.SIACU.Data.Cases.Common.GetCaseNotes(strIdSession,
                                                                       strTransaction,
                                                                       strCaseId)
            };
        }

        public static SubCaseNotesResponse GetSubCaseNotes(string strIdSession, string strTransaction, string strCaseId)
        {
            throw new NotImplementedException();
        }
    }
}