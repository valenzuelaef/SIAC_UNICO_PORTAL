using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetContractServices.Response
{
    public class ListServiciosAsociados
    {
        public string productOfferindId { get; set; }
        public string productOfferingType { get; set; }
        public string commercialDescription { get; set; }
        public string status { get; set; }
        public string effectiveDate { get; set; }
        public string calcSub { get; set; }
        public string origAcc { get; set; }
        public string reason { get; set; }

    }
}
