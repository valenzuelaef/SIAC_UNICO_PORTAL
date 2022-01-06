using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "DetailProductPostPaid")]
    public class DetailProductPost
    {
        [DataMember]
        public string CoId { get; set; }

        [DataMember]
        public string Telefono { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public DateTime FechaActivacion { get; set; }
    }
}