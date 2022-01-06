using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ProductPostPaid")]
    public class Product
    {
        [DataMember]
        public string IdPlan { get; set; }

        [DataMember]
        public string TipoProducto { get; set; }

        [DataMember]
        public string CodigoProducto { get; set; }

        [DataMember]
        public string Producto { get; set; }

        [DataMember]
        public string Cuenta { get; set; }

        [DataMember]
        public string CodigoCliente { get; set; }

        [DataMember]
        public string TipoCliente { get; set; }

        [DataMember]
        public DateTime FechaActivacionCuenta { get; set; }

        [DataMember]
        public int NroServiciosActivos { get; set; }

        [DataMember]
        public int NroServiciosNoActivos { get; set; }

        [DataMember]
        public int id { get; set; }   
    }
}