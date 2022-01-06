using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.GetFeeEquipment
{
    [DataContract(Name = "FeeEquipmentResponsePostPaid")]
    public class FeeEquipmentResponse
    {
        [DataMember]
        public List<FeeEquipment> ListFeeEquipmentccount { get; set; }
        [DataMember]
        public string strsumImportePendiente { get; set; }
        [DataMember]
        public string strSumImporteOriginal { get; set; }
    }
}
