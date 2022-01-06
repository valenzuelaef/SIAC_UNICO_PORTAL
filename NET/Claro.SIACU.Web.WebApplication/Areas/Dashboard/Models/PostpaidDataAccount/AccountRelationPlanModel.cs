
using Claro.Helpers;
using System.Collections.Generic;
using RELATIONPLAN_MODELHELPERS = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.AccountRelationPlanHelper;


namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataAccount
{

    public class AccountRelationPlanModel : IExcel
    {
        [Header(Title = "objCustomer")]
        public RELATIONPLAN_MODELHELPERS.AccountCustomer objCustomer { get; set; }
        [Header(Title = "objAccountBag")]
        public RELATIONPLAN_MODELHELPERS.AccountBag objAccountBag { get; set; }
        [Header(Title = "lstAccountBagDetail")]
        public List<RELATIONPLAN_MODELHELPERS.AccountBagDetail> lstAccountBagDetail { get; set; }
        [Header(Title = "lstAccountRelationPlan")]
        public List<RELATIONPLAN_MODELHELPERS.AccountRelationPlan> lstAccountRelationPlan { get; set; }
        public string Transaction { get; set; }

        public AccountRelationPlanModel()
        {
            objCustomer = new RELATIONPLAN_MODELHELPERS.AccountCustomer();
            objAccountBag = new RELATIONPLAN_MODELHELPERS.AccountBag();
            lstAccountBagDetail = new List<RELATIONPLAN_MODELHELPERS.AccountBagDetail>();
            lstAccountRelationPlan = new List<RELATIONPLAN_MODELHELPERS.AccountRelationPlan>();
        }
    }
}