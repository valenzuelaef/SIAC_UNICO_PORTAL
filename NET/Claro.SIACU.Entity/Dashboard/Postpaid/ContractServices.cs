using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ContractServicesPostPaid")]
    public class ContractServices
    {
        [DataMember]
        public string COD_GRUPO { get; set; }
        [Data.DbColumn("de_grp")]
        [DataMember]
        public string DES_GRUPO { get; set; }
        [Data.DbColumn("no_grp")]
        [DataMember]
        public string POS_GRUPO { get; set; }
        [Data.DbColumn("co_ser")]
        [DataMember]
        public string COD_SERV { get; set; }
        [Data.DbColumn("de_ser")]
        [DataMember]
        public string DES_SERV { get; set; }
        [Data.DbColumn("no_ser")]
        [DataMember]
        public string POS_SERV { get; set; }
        [Data.DbColumn("co_excl")]
        [DataMember]
        public string COD_EXCLUYENTE { get; set; }
        [Data.DbColumn("de_excl")]
        [DataMember]
        public string DES_EXCLUYENTE { get; set; }
        [Data.DbColumn("estado")]
        [DataMember]
        public string ESTADO { get; set; }
        [Data.DbColumn("valido_desde")]
        [DataMember]
        public string FECHA_VALIDEZ { get; set; }
        [Data.DbColumn("suscrip")]
        [DataMember]
        public string MONTO_CARGO_SUS { get; set; }
        [Data.DbColumn("cargofijo")]
        [DataMember]
        public string MONTO_CARGO_FIJO { get; set; }
        [Data.DbColumn("cuota")]
        [DataMember]
        public string CUOTA_MODIF { get; set; }
        [DataMember]
        public string MONTO_FINAL { get; set; }
        [Data.DbColumn("periodos")]
        [DataMember]
        public string PERIODOS_VALIDOS { get; set; }
        [Data.DbColumn("bloq_act")]
        [DataMember]
        public string BLOQUEO_ACT { get; set; }
        [Data.DbColumn("bloq_des")]
        [DataMember]
        public string BLOQUEO_DESACT { get; set; }
        [Data.DbColumn("spcode")]
        [DataMember]
        public string SPCODE { get; set; }
        [Data.DbColumn("sncode")]
        [DataMember]
        public string SNCODE { get; set; }
        
        [DataMember(Name = "daPo")]
        public string DAPO { get; set; }
        
        [DataMember(Name = "servicio")]
        public string SERVICIO { get; set; }
     
        [DataMember(Name = "itemCargo")]
        public string CARGO { get; set; }
       
        [DataMember(Name = "tipoServicio")]
        public string TIPOSERVICIO { get; set; }
      
        [DataMember(Name = "categoriaServicio")]
        public string CATSERVICIO { get; set; }

    }
}
