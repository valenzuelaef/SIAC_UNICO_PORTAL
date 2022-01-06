﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.Transversal.ProxyService.ConsultaClaves {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ebs/operaciones/consultaclaves", ConfigurationName="ConsultaClaves.ebsConsultaClavesPortType")]
    public interface ebsConsultaClavesPortType {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ebs/operaciones/consultaclaves/desencriptar", ReplyAction="*")]
        Claro.Transversal.ProxyService.ConsultaClaves.desencriptarResponse desencriptar(Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ebs/operaciones/consultaclaves/desencriptar", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.Transversal.ProxyService.ConsultaClaves.desencriptarResponse> desencriptarAsync(Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class desencriptarRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="desencriptarRequest", Namespace="http://claro.com.pe/eai/ebs/operaciones/consultaclaves", Order=0)]
        public Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequestBody Body;
        
        public desencriptarRequest() {
        }
        
        public desencriptarRequest(Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://claro.com.pe/eai/ebs/operaciones/consultaclaves")]
    public partial class desencriptarRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string idTransaccion;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ipAplicacion;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string ipTransicion;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string usrAplicacion;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string idAplicacion;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string codigoAplicacion;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string usuarioAplicacionEncriptado;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string claveEncriptado;
        
        public desencriptarRequestBody() {
        }
        
        public desencriptarRequestBody(string idTransaccion, string ipAplicacion, string ipTransicion, string usrAplicacion, string idAplicacion, string codigoAplicacion, string usuarioAplicacionEncriptado, string claveEncriptado) {
            this.idTransaccion = idTransaccion;
            this.ipAplicacion = ipAplicacion;
            this.ipTransicion = ipTransicion;
            this.usrAplicacion = usrAplicacion;
            this.idAplicacion = idAplicacion;
            this.codigoAplicacion = codigoAplicacion;
            this.usuarioAplicacionEncriptado = usuarioAplicacionEncriptado;
            this.claveEncriptado = claveEncriptado;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class desencriptarResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="desencriptarResponse", Namespace="http://claro.com.pe/eai/ebs/operaciones/consultaclaves", Order=0)]
        public Claro.Transversal.ProxyService.ConsultaClaves.desencriptarResponseBody Body;
        
        public desencriptarResponse() {
        }
        
        public desencriptarResponse(Claro.Transversal.ProxyService.ConsultaClaves.desencriptarResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://claro.com.pe/eai/ebs/operaciones/consultaclaves")]
    public partial class desencriptarResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string idTransaccion;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string codigoResultado;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string mensajeResultado;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string usuarioAplicacion;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string clave;
        
        public desencriptarResponseBody() {
        }
        
        public desencriptarResponseBody(string idTransaccion, string codigoResultado, string mensajeResultado, string usuarioAplicacion, string clave) {
            this.idTransaccion = idTransaccion;
            this.codigoResultado = codigoResultado;
            this.mensajeResultado = mensajeResultado;
            this.usuarioAplicacion = usuarioAplicacion;
            this.clave = clave;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ebsConsultaClavesPortTypeChannel : Claro.Transversal.ProxyService.ConsultaClaves.ebsConsultaClavesPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ebsConsultaClavesPortTypeClient : System.ServiceModel.ClientBase<Claro.Transversal.ProxyService.ConsultaClaves.ebsConsultaClavesPortType>, Claro.Transversal.ProxyService.ConsultaClaves.ebsConsultaClavesPortType {
        
        public ebsConsultaClavesPortTypeClient() {
        }
        
        public ebsConsultaClavesPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ebsConsultaClavesPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ebsConsultaClavesPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ebsConsultaClavesPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.Transversal.ProxyService.ConsultaClaves.desencriptarResponse Claro.Transversal.ProxyService.ConsultaClaves.ebsConsultaClavesPortType.desencriptar(Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequest request) {
            return base.Channel.desencriptar(request);
        }
        
        public string desencriptar(ref string idTransaccion, string ipAplicacion, string ipTransicion, string usrAplicacion, string idAplicacion, string codigoAplicacion, string usuarioAplicacionEncriptado, string claveEncriptado, out string mensajeResultado, out string usuarioAplicacion, out string clave) {
            Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequest inValue = new Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequest();
            inValue.Body = new Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequestBody();
            inValue.Body.idTransaccion = idTransaccion;
            inValue.Body.ipAplicacion = ipAplicacion;
            inValue.Body.ipTransicion = ipTransicion;
            inValue.Body.usrAplicacion = usrAplicacion;
            inValue.Body.idAplicacion = idAplicacion;
            inValue.Body.codigoAplicacion = codigoAplicacion;
            inValue.Body.usuarioAplicacionEncriptado = usuarioAplicacionEncriptado;
            inValue.Body.claveEncriptado = claveEncriptado;
            Claro.Transversal.ProxyService.ConsultaClaves.desencriptarResponse retVal = ((Claro.Transversal.ProxyService.ConsultaClaves.ebsConsultaClavesPortType)(this)).desencriptar(inValue);
            idTransaccion = retVal.Body.idTransaccion;
            mensajeResultado = retVal.Body.mensajeResultado;
            usuarioAplicacion = retVal.Body.usuarioAplicacion;
            clave = retVal.Body.clave;
            return retVal.Body.codigoResultado;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.Transversal.ProxyService.ConsultaClaves.desencriptarResponse> Claro.Transversal.ProxyService.ConsultaClaves.ebsConsultaClavesPortType.desencriptarAsync(Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequest request) {
            return base.Channel.desencriptarAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.Transversal.ProxyService.ConsultaClaves.desencriptarResponse> desencriptarAsync(string idTransaccion, string ipAplicacion, string ipTransicion, string usrAplicacion, string idAplicacion, string codigoAplicacion, string usuarioAplicacionEncriptado, string claveEncriptado) {
            Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequest inValue = new Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequest();
            inValue.Body = new Claro.Transversal.ProxyService.ConsultaClaves.desencriptarRequestBody();
            inValue.Body.idTransaccion = idTransaccion;
            inValue.Body.ipAplicacion = ipAplicacion;
            inValue.Body.ipTransicion = ipTransicion;
            inValue.Body.usrAplicacion = usrAplicacion;
            inValue.Body.idAplicacion = idAplicacion;
            inValue.Body.codigoAplicacion = codigoAplicacion;
            inValue.Body.usuarioAplicacionEncriptado = usuarioAplicacionEncriptado;
            inValue.Body.claveEncriptado = claveEncriptado;
            return ((Claro.Transversal.ProxyService.ConsultaClaves.ebsConsultaClavesPortType)(this)).desencriptarAsync(inValue);
        }
    }
}