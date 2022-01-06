using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Response
{
    public class response
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject(myJsonResponse); 
        public class DetalleError
        {
            public object errorCode { get; set; }
            public object errorDescription { get; set; }

        }

        public class ResponseStatus
        {
            public int status { get; set; }
            public string codigoRespuesta { get; set; }
            public string descripcionRespuesta { get; set; }
            public object ubicacionError { get; set; }
            public DateTime fecha { get; set; }
            public object origen { get; set; }
            public List<DetalleError> detalleError { get; set; }

        }

        public class RecursoFisico
        {
            public object iccid { get; set; }

        }

        public class RecursoLogico
        {
            public string numeroLinea { get; set; }

        }

        public class CaracteristicaProducto
        {
            public string tecnologia { get; set; }
            public string tipoServicio { get; set; }

        }

        public class Producto
        {
            public List<RecursoFisico> recursoFisico { get; set; }
            public List<RecursoLogico> recursoLogico { get; set; }
            public CaracteristicaProducto caracteristicaProducto { get; set; }

        }

        public class OfertaProducto
        {
            public Producto producto { get; set; }

        }

        public class IdentificacionPersona
        {
            public object numeroDocumento { get; set; }
            public object tipoDocumento { get; set; }
            public object genero { get; set; }
            public object fechaNacimiento { get; set; }
            public object telefonoContacto { get; set; }
            public object email { get; set; }
            public string nombreCompleto { get; set; }
            public string nombre { get; set; }
            public string apellidoCompleto { get; set; }

        }

        public class Propiedad
        {
            public object tipoPredio { get; set; }
            public object nroDepartamento { get; set; }

        }

        public class DireccionUrbana
        {
            public object referenciaDireccion { get; set; }
            public object tipoVia { get; set; }
            public object distrito { get; set; }
            public object departamento { get; set; }
            public string provincia { get; set; }
            public string codigoPostal { get; set; }
            public object nombreCalle { get; set; }
            public string nroCuadra { get; set; }
            public List<Propiedad> _propiedad { get; set; }

        }

        public class Cliente
        {
            public IdentificacionPersona identificacionPersona { get; set; }
            public List<DireccionUrbana> direccionUrbana { get; set; }

        }

        public class CuentaCliente
        {
            public Cliente cliente { get; set; }
            public string idCliente { get; set; }
            public string idPublicoCliente { get; set; }
            public string nroCuenta { get; set; }

        }

        public class CuentaFacturacion
        {
            public CuentaCliente cuentaCliente { get; set; }
      


        }
        public class CaracteristicaAdicional
        {
            public string descripcion { get; set; } //indCambioNum
            public string valor { get; set; }//0

        }

        public class Contrato
        {
            public string idContrato { get; set; }
            public string fechaActivacion { get; set; }
            public string idPublicoContrato { get; set; }
            public string estadoContrato { get; set; }
            public List<OfertaProducto> ofertaProducto { get; set; }
            public CuentaFacturacion cuentaFacturacion { get; set; }
            public List<CaracteristicaAdicional> caracteristicaAdicional { get; set; }
        }

        public class ResponseData
        {
            public List<Contrato> contrato { get; set; }

        }


        public ResponseStatus responseStatus { get; set; }
        public ResponseData responseData { get; set; }




    }
}
