using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetPrepaidProducts
{
    [DataContract(Name = "PrepaidProductsResponseDashboard")]
    public class PrepaidProductsResponse
    {
        public PrepaidProductsResponse()
        {
            ListOptionalObject = new List<OptionalObject>();
            ListProduct = new List<Prepaid.Product>();
        }

        [DataMember]
        public string DataOrigin { get; set; }
        [DataMember]
        public List<OptionalObject> ListOptionalObject { get; set; }
        [DataMember]
        public List<Prepaid.Product> ListProduct { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }
        [DataMember]
        public string CodeError { get; set; }
    }
}
