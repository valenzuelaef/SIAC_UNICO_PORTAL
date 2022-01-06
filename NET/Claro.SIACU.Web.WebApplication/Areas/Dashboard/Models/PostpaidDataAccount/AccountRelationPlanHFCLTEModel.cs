using Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHFCLTE;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataAccount
{
    public class AccountRelationPlanHFCLTEModel
    {
        public List<RelationPlanHFCLTE> lstGSM { get; set; }
        public List<RelationPlanHFCLTE> lstHFC { get; set; }
        public List<RelationPlanHFCLTE> lstLTE { get; set; }
        public List<Equiment> lstEquiment { get; set; }
        public string StrAplicacion { get; set; }

        public AccountRelationPlanHFCLTEModel()
        {
            lstGSM = new List<RelationPlanHFCLTE>();
            lstHFC = new List<RelationPlanHFCLTE>();
            lstLTE = new List<RelationPlanHFCLTE>();
            lstEquiment = new List<Equiment>();
        }


    }
}