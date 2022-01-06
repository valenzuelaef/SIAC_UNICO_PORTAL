using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetUsageThresholdsCounter
{

    public class GetUsageThresholdsCounter
    {
        public string responseCode { get; set; }
        public string TypePlan { get; set; }
        public string ConsumptionLimit { get; set; }
        public string Degradation { get; set; }
        public string Thetering { get; set; }
    }


}
