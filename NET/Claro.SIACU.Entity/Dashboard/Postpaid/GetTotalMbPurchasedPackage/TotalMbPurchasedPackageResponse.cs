using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage
{
    [DataContract(Name = "TotalMbPurchasedPackageResponsePospaid")]
    public class TotalMbPurchasedPackageResponse
    {
        [DataMember]
        public List<Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS { get; set; }
        [DataMember]
        public string TotalMBCicle { get; set; }
        [DataMember]
        public string  TotalRows { get; set; }
        [DataMember]
        public string strMensaje { get; set; }
        [DataMember]
        public bool IsVisibleMensaje { get; set; }
        [DataMember]
        public string strMensajeAlert { get; set; }
        [DataMember]
        public string FlagOne { get; set; }

        
    }
}
