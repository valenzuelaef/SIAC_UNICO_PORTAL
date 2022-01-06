using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard
{
    [DbTable("TEMPO")]
    [DataContract(Name = "Person")]
    public class Person : IPerson
    {
        [DbColumn("NOMBRES")]
        [DataMember]
        public string NOMBRES { get; set; }

        [DbColumn("APELLIDOS")]
        [DataMember]
        public string APELLIDOS { get; set; }

        [DataMember]
        public string DESCRIPCION { get; set; }

        [DbColumn("TIPO_DOC")]
        [DataMember]
        public string TIPO_DOC { get; set; }

        [DataMember]
        public string TIPO_DOC_ID { get; set; }

        [DbColumn("NRO_DOC")]
        [DataMember]
        public string NRO_DOC { get; set; }

        [DbColumn("ESTADO_CIVIL")]
        [DataMember]
        public string ESTADO_CIVIL { get; set; }

        [DataMember]
        public string ESTADO_CIVIL_ID { get; set; }


        [DataMember]
        public string LUGAR_NACIMIENTO_ID { get; set; }

        [DbColumn("LUGAR_NAC")]
        [DataMember]
        public string LUGAR_NACIMIENTO_DES { get; set; }

        [DbColumn("FECHA_NAC")]
        [DataMember]
        public string FECHA_NAC { get; set; }

        [DbColumn("SEXO")]
        [DataMember]
        public string SEXO { get; set; }

        [DbColumn("EMAIL")]
        [DataMember]
        public string EMAIL { get; set; }

        [DbColumn("TELEF_REFERENCIA")]
        [DataMember]
        public string TELEF_REFERENCIA { get; set; }

        [DbColumn("TELEFONO")]
        [DataMember]
        public string TELEFONO { get; set; }

        [DbColumn("OCUPACION")]
        [DataMember]
        public string OCUPACION { get; set; }

        [DbColumn("CARGO")]
        [DataMember]
        public string CARGO { get; set; }

        [DbColumn("FAX")]
        [DataMember]
        public string FAX { get; set; }


        [DataMember]
        public string FLAG_EMAIL { get; set; }

        [DbColumn("DOMICILIO")]
        [DataMember]
        public string DOMICILIO { get; set; }

        [DbColumn("URBANIZACION")]
        [DataMember]
        public string URBANIZACION { get; set; }

        [DbColumn("DISTRITO")]
        [DataMember]
        public string DISTRITO { get; set; }

        [DbColumn("ZIPCODE")]
        [DataMember]
        public string ZIPCODE { get; set; }

        [DbColumn("CIUDAD")]
        [DataMember]
        public string CIUDAD { get; set; }

        [DbColumn("DEPARTAMENTO")]
        [DataMember]
        public string DEPARTAMENTO { get; set; }


        [DataMember]
        public string PROVINCIA { get; set; }

        [DbColumn("REFERENCIA")]
        [DataMember]
        public string REFERENCIA { get; set; }

        [DataMember]
        public string LONG_NRO_DOC { get; set; }


        [DataMember]
        public string PAIS { get; set; }

        [DataMember]
        public string RAZON_SOCIAL { get; set; }
        [DataMember]
        public string APELLIDO_PAT_TOBE { get; set; }
        [DataMember]
        public string APELLIDO_MAT_TOBE { get; set; }
        [DataMember]
        public string indicadorCambioNumero { get; set; }
         [DataMember]
        public string nacionalidadId { get; set; }
        
    }
}