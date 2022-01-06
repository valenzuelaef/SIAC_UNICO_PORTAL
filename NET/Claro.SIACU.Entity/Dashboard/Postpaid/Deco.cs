using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "DecoPostPaid")]
    public class Deco
    {
      
        [DataMember]
        public string tipodth { get; set; }
      
        [DataMember]
        public string id_transaccion { get; set; }
      
        [DataMember]
        public string codigo_material { get; set; }
     
        [DataMember]
        public string codigo_sap { get; set; }
       
        [DataMember]
        public string numero_serie { get; set; }
       
        [DataMember]
        public string macadress { get; set; }
      
        [DataMember]
        public string descripcion_material { get; set; }
      
        [DataMember]
        public string abrev_material { get; set; }
      
        [DataMember]
        public string estado_material { get; set; }
       
        [DataMember]
        public string precio_almacen { get; set; }
       
        [DataMember]
        public string codigo_cuenta { get; set; }
      
        [DataMember]
        public string componente { get; set; }
      
        [DataMember]
        public string centro { get; set; }
      
        [DataMember]
        public string idalm { get; set; }
      
        [DataMember]
        public string almacen { get; set; }
       
        [DataMember]
        public string tipo_equipo { get; set; }
      
        [DataMember]
        public string id_producto { get; set; }
      
        [DataMember]
        public string id_cliente { get; set; }
        
        [DataMember]
        public string modelo { get; set; }
       
        [DataMember]
        public string fecusu { get; set; }
     
        [DataMember]
        public string codusu { get; set; }
        [DataMember]
        public string convertertype { get; set; }

        [DataMember]
        public string servicio_principal { get; set; }

        [DataMember]
        public string headend { get; set; }

        [DataMember]
        public string ephomeexchange { get; set; }

        [DataMember]
        public string numero { get; set; }

        [DataMember]
        public string Tipo { get; set; }

        [DataMember]
        public string sim_card { get; set; }
        [DataMember]
        public string tipo_servicio { get; set; }

        [DataMember]
        public string Estado { get; set; }
    }
}
