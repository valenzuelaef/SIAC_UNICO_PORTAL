using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts
{
    [DataContract(Name = "PostpaidProductsResponseDashboard")]
    public class PostpaidProductsResponse
    {
        public PostpaidProductsResponse()
        {
            ListOptionalObject = new List<OptionalObject>();
            ListProduct = new List<Postpaid.Product>();
        }

        [DataMember]
        public string DataOrigin { get; set; }
        [DataMember]
        public List<OptionalObject> ListOptionalObject { get; set; }
        [DataMember]
        public List<Postpaid.Product> ListProduct { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }
        [DataMember]
        public string CodeError { get; set; }
    }
}
