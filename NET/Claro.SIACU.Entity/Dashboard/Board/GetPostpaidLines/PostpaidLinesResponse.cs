using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Board.GetPostpaidLines
{
    [DataContract(Name = "PostpaidLinesResponseDashboard")]
    public class PostpaidLinesResponse
    {
        public PostpaidLinesResponse()
        {
            ListOptionalObject = new List<OptionalObject>();
            ListDetailProduct = new List<Postpaid.DetailProductPost>();
        }

        [DataMember]
        public string DataOrigin { get; set; }
        [DataMember]
        public List<OptionalObject> ListOptionalObject { get; set; }
        [DataMember]
        public List<Postpaid.DetailProductPost> ListDetailProduct { get; set; }
        [DataMember]
        public string ErrorMsg { get; set; }
        [DataMember]
        public string CodeError { get; set; }
    }
}
