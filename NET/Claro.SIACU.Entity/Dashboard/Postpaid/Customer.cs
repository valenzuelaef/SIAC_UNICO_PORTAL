using Claro.SIACU.Entity.Coliving.getConsultaLineaCuenta;
using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "CustomerPostPaid")]
    public class Customer : Person, ICustomer
    {
        [DataMember]
        public string csIdPub { get; set; }
        [DataMember]
        public string coIdPub { get; set; }
        [DataMember]
        public string NOMBRE_COMERCIAL { get; set; }

        [DataMember]
        public string COD_TIPO_CLIENTE { get; set; }

        [DataMember]
        public string TIPO_CLIENTE { get; set; }

        [DataMember]
        public string DNI_RUC { get; set; }

        [DataMember]
        public string CUENTA { get; set; }

        [DataMember]
        public string MODALIDAD { get; set; }

        [DataMember]
        public string CUSTOMER_ID { get; set; }

        [DataMember]
        public string PAIS_FAC { get; set; }

        [DataMember]
        public string DOMICILIO_FAC { get; set; }

        [DataMember]
        public string URBANIZACION_FAC { get; set; }

        [DataMember]
        public string DISTRITO_FAC { get; set; }

        [DataMember]
        public string PROVINCIA_FAC { get; set; }

        [DataMember]
        public string POSTAL_FAC { get; set; }

        [DataMember]
        public string DEPARTEMENTO_FAC { get; set; }

        [DataMember]
        public string SEGMENTO { get; set; }

        [DataMember]
        public string SEGMENTO2 { get; set; }

        [DataMember]
        public string REPRESENTANTE_LEGAL { get; set; }

        [DataMember]
        public string PAIS_LEGAL { get; set; }

        [DataMember]
        public string DOMICILIO_LEGAL { get; set; }

        [DataMember]
        public string URBANIZACION_LEGAL { get; set; }

        [DataMember]
        public string DISTRITO_LEGAL { get; set; }

        [DataMember]
        public string PROVINCIA_LEGAL { get; set; }

        [DataMember]
        public string POSTAL_LEGAL { get; set; }

        [DataMember]
        public string DEPARTEMENTO_LEGAL { get; set; }

        [DataMember]
        public DateTime FECHA_ACT { get; set; }

        [DataMember]
        public string CICLO_FACTURACION { get; set; }

        [DataMember]
        public string DIRECCION_DESPACHO { get; set; }

        [DataMember]
        public string DIRECCION { get; set; }

        [DataMember]
        public string NOTA_DIRECCION { get; set; }

        [DataMember]
        public string CONTACTO_CLIENTE { get; set; }

        [DataMember]
        public string TELEFONO_CONTACTO { get; set; }

        [DataMember]
        public string CONTRATO_ID { get; set; }

        [DataMember]
        public string FORMA_PAGO { get; set; }

        [DataMember]
        public string ASESOR { get; set; }

        [DataMember]
        public bool AFILIACION { get; set; }

        [DataMember]
        public string COD_PLANO { get; set; }

        [DataMember]
        public string COD_HUB { get; set; }

        [DataMember]
        public string BANNER_MESSAGE { get; set; }

        [DataMember]
        public Account oCUENTA { get; set; }


        [DataMember]
        public string NOMBRE_COMPLETO { get; set; }

        [DataMember]
        public string TIPO_PRODUCTO { get; set; }

        [DataMember]
        public string CODIGO_PLANO_FACT { get; set; }

        [DataMember]
        public string CODIGO_PLANO_INST { get; set; }

        [DataMember]
        public string COD_CENTRO_POBLADO { get; set; }

        [DataMember]
        public string DES_CENTRO_POBLADO { get; set; }

        [DataMember]
        public string UBIGEO_FACT { get; set; }
        [DataMember]
        public string UBIGEO_INST { get; set; }

        [DataMember]
        public string OBJID_CONTACTO { get; set; }

        [DataMember]
        public string Application { get; set; }

        [DataMember]
        public string ValueSearch { get; set; }

        [DataMember]
        public string TipoProducto { get; set; }
        [DataMember]
        public bool EsServicioLTE { get; set; }
        [DataMember]
        public string COD_PLAN_TARIFARIO { get; set; }
        [DataMember]
        public Claro.SIACU.ApplicationType ApplicationType { get; set; }
        [DataMember]
        public string ESTADO_LINEA { get; set; }
        [DataMember]
        public string ContactCustomerCode { get; set; }
        [DataMember]
        public string ContactNumberReference1 { get; set; }
        [DataMember]
        public string ContactNumberReference2 { get; set; }
        [DataMember]
        public string ContactCntCode { get; set; }
        [DataMember]
        public string StatusFullClaroC { get; set; }
        [DataMember]
        public string StatusFullClaroCodeC { get; set; }

        [DataMember]
        public string OBJID_SITE { get; set; }

        [DataMember]
        public string ROL_CONTACTO { get; set; }

        [DataMember]
        public string ESTADO_CONTACTO { get; set; }

        [DataMember]
        public string ESTADO_CONTRATO { get; set; }

        [DataMember]
        public string ESTADO_SITE { get; set; }

        [DataMember]
        public string S_NOMBRES { get; set; }

        [DataMember]
        public string S_APELLIDOS { get; set; }

        [DataMember]
        public string PUNTO_VENTA { get; set; }

        [DataMember]
        public int FLAG_REGISTRADO { get; set; }

        [DataMember]
        public string CANT_REG { get; set; }

        [DataMember]
        public string MOTIVOREGISTRO { get; set; }

        [DataMember]
        public string FUNCION { get; set; }

        [DataMember]
        public string PLAN { get; set; }

        [DataMember]
        public int NACIONALIDAD { get; set; }

        [DataMember]
        public string CUSTOMER_ID_P { get; set; }
        [DataMember]
        public string flagConvivencia { get; set; }
        [DataMember]
        public Itm itm { get; set; }

        [DataMember]
        public string DIRECCIONREFERENCIALLEGAL { get; set; }
        [DataMember]
        public string  origen { get; set; }
               
        //INICIATIVA 616
        [DataMember]
        public string telefonoTOBE { get; set; }

        [DataMember]
        public string nombreCalle { get; set; }

        [DataMember]
        public string tipoVia { get; set; }

        [DataMember]
        public string numeroCuadra { get; set; }

        [DataMember]
        public string tipoPredio { get; set; }
    }
}