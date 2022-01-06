using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "BagDetailPostPaid")]
     public class BagDetail
     {
         [Data.DbColumn("BOLSA")]
         [DataMember]
         public string TIPO_BOLSA { get; set; }

         [Data.DbColumn("CARGO")]
         [DataMember]
         public string CARGO_FIJO { get; set; }

         [Data.DbColumn("ACTIVACION")]
         [DataMember]
         public string FECHA_ACTIVACION { get; set; }

         [Data.DbColumn("NUM_LINEAS")]
         [DataMember] 
         public string CANTIDAD_LINEAS { get; set; }

         [Data.DbColumn("MINUTOS1")]
         [DataMember]
         public string MIN_SOL { get; set; }

         [DataMember]
         public string CUENTA { get; set; }

         [DataMember]
         public string CICLO { get; set; }

         [DataMember]
         public string NOMBRE { get; set; }

         [DataMember]
         public string BOLSA { get; set; }

         [DataMember]
         public string ACTIVACION { get; set; }

         [Data.DbColumn("DESACTIVA")]
         [DataMember]
         public string DESACTIVA { get; set; }

         [DataMember]
         public string CARGO { get; set; }

        [Data.DbColumn("MINUTOS1")]
         [DataMember]
         public string MINUTOS1 { get; set; }

         [DataMember]
         public string MINUTOS2 { get; set; }

         [DataMember]
         public string RANGO1 { get; set; }

         [DataMember]
         public string RANGO2 { get; set; }

         [DataMember]
         public string MULTI1 { get; set; }

         [DataMember]
         public string MULTI2 { get; set; }


         [DataMember]
         public int FLAG_DESACTIVA { get; set; }
        



     }
}
