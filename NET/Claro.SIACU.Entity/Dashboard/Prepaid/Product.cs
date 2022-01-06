using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "ProductPrePaid")]
    public class Product
    {
        [DataMember]
        public string TipoProducto { get; set; }

        [DataMember]
        public string Telefono { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public DateTime FechaActivacion { get; set; }

        [DataMember]
        public string ObjId { get; set; }

        [DataMember]
        public int Id { get; set; } 
    }
}