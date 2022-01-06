namespace Claro.SIACU.Business.Cases
{
    public static class WipBin
    {
        public static Claro.SIACU.Entity.Cases.GetWipBinListByUser.WipBinListByUserResponse GetWipBinListByUser(string strIdSession, string strTransaction, string strUser)
        {
            return new Entity.Cases.GetWipBinListByUser.WipBinListByUserResponse()
            {
                ListWipBin = Claro.SIACU.Data.Cases.Common.GetWipBinListByUser(strIdSession, strTransaction, strUser) 
            };
        }

        public static Claro.SIACU.Entity.Cases.GetCasesByWipBin.CasesByWipBinResponse GetCasesByWipBin(string strIdSession, string strTransaction, string strUser, string strWipBin)
        {
            return new Claro.SIACU.Entity.Cases.GetCasesByWipBin.CasesByWipBinResponse()
            {
                Cases = Claro.SIACU.Data.Cases.Common.GetCasesByWipBin(strIdSession, strTransaction, strUser, strWipBin)
            };
        }
    }
}