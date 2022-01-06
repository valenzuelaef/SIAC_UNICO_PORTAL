using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DataContract(Name = "DataSalePELPrepaid")]
    public class DataSalePEL
    {

        [Data.DbColumn("TELEFONO")]
        [DataMember]
        public string Telefono { get; set; }
        [Data.DbColumn("OVENV_DESCRIPCION")]
        [DataMember]
        public string PuntoVenta { get; set; }
        [Data.DbColumn("CONTV_VENDEDOR")]
        [DataMember]
        public string Vendedor { get; set; }
        [Data.DbColumn("CAMPANA_DESC")]
        [DataMember]
        public string Campaña { get; set; }
        [Data.DbColumn("CONTD_FECHA_CONTRATO")]
        [DataMember]
        public string FechaCompra { get; set; }
        [Data.DbColumn("IMEI19")]
        [DataMember]
        public string IMEI { get; set; }
        [Data.DbColumn("COD_EQUIPO")]
        [DataMember]
        public string CodigoArticulo { get; set; }
        [Data.DbColumn("DES_EQUIPO")]
        [DataMember]
        public string MarcaModelo { get; set; }
        [DataMember]
        public string DescripcionVenta { get; set; }
        [Data.DbColumn("SERIE_EQUIPO")]
        [DataMember]
        public string SerieEquipo { get; set; }
    }
}
