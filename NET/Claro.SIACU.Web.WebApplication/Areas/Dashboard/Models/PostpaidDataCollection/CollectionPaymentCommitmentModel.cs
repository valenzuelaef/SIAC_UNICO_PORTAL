using System.Collections.Generic;
using HELPER_COLLECTION = Claro.SIACU.Web.WebApplication.Areas.Dashboard.Helpers.Postpaid.CollectionStatusAccountHelper;

namespace Claro.SIACU.Web.WebApplication.Areas.Dashboard.Models.PostpaidDataCollection
{
    public class CollectionPaymentCommitmentModel
    {
        public List<HELPER_COLLECTION.CollectionPaymentCommitment> listCollectionPaymentCommitment { get; set; }
    }

}