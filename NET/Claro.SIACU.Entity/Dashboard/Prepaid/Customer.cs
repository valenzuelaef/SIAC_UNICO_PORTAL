using Claro.Data;
using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Prepaid
{
    [DbTable("TEMPO")]
    [DataContract(Name = "CustomerPrePaid")]
    public class Customer : Person, ICustomer
    {
        [DbColumn("SEGMENTO")]
        [DataMember]
        public string SEGMENTO { get; set; }

        [DbColumn("ESTADO_CONTACTO")]
        [DataMember]
        public string ESTADO_CONTACTO { get; set; }

        [DataMember]
        public bool AFILIACION { get; set; }

        [DbColumn("MODALIDAD")]
        [DataMember]
        public string MODALIDAD { get; set; }

        [DbColumn("ROL_CONTACTO")]
        [DataMember]
        public string ROL_CONTACTO { get; set; }

        [DbColumn("MOTIVO_REGISTRO")]
        [DataMember]
        public string MOTIVOREGISTRO { get; set; }

        [DbColumn("OBJID_CONTACTO")]
        [DataMember]
        public string OBJID_CONTACTO { get; set; }

        [DataMember]
        public string ESTADO_CONTRATO { get; set; }

        [DataMember]
        public string FECHAACTIVACION { get; set; }

        [DataMember]
        public string EMAIL_CONFIRMACION { get; set; }

        [DbColumn("FECHA_ACT")]
        [DataMember]
        public DateTime FECHA_ACT { get; set; }

        [DataMember]
        public string BANNER_MESSAGE { get; set; }

        [DataMember]
        public Service oService { get; set; }

        [DataMember]
        public string TipoProducto { get; set; }

        [DbColumn("CUENTA")]
        [DataMember]
        public string CUENTA { get; set; }

        [DataMember]
        public Claro.SIACU.ApplicationType ApplicationType { get; set; }

        [DbColumn("FLAG_REGISTRADO")]
        [DataMember]
        public int FLAG_REGISTRADO { get; set; }

        [DataMember]
        public string USUARIO { get; set; }

        [DataMember]
        public string INTERACCION { get; set; }

        [DataMember]
        public string CONTINGENCIACLARIFY { get; set; }

        [DataMember]
        public string BLACKLIST { get; set; }
        [DataMember]
        public string Transaction { get; set; }

        [DataMember]
        public string TipoProductoTelefono { get; set; }

        [DataMember]
        public string OBJID_SITE { get; set; }

        [DataMember]
        public string ESTADO_SITE { get; set; }

        [DataMember]
        public string PUNTO_VENTA { get; set; }

        [DataMember]
        public string CANT_REG { get; set; }

        [DataMember]
        public string FUNCION { get; set; }

        [DataMember]
        public string LUGAR_NAC { get; set; }



    }
}