﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.SIACPost.Validate4GLTE {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS", ConfigurationName="SIACPost.Validate4GLTE.Validacion4GLTEWSPortType")]
    public interface Validacion4GLTEWSPortType {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación validarLinea no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS/validarLinea", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaResponse1 validarLinea(Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaRequest1 request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación validarPlan no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS/validarPlan", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanResponse1 validarPlan(Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanRequest1 request);
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS/types")]
    public partial class validarLineaRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditRequestType auditRequestField;
        
        private string numeroField;
        
        private parametrosTypeObjetoOpcional[] listaRequestOpcionalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public auditRequestType auditRequest {
            get {
                return this.auditRequestField;
            }
            set {
                this.auditRequestField = value;
                this.RaisePropertyChanged("auditRequest");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string numero {
            get {
                return this.numeroField;
            }
            set {
                this.numeroField = value;
                this.RaisePropertyChanged("numero");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("objetoOpcional", Namespace="http://claro.com.pe/eai/ws/baseschema", IsNullable=false)]
        public parametrosTypeObjetoOpcional[] listaRequestOpcional {
            get {
                return this.listaRequestOpcionalField;
            }
            set {
                this.listaRequestOpcionalField = value;
                this.RaisePropertyChanged("listaRequestOpcional");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class auditRequestType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string ipAplicacionField;
        
        private string nombreAplicacionField;
        
        private string usuarioAplicacionField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
                this.RaisePropertyChanged("idTransaccion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ipAplicacion {
            get {
                return this.ipAplicacionField;
            }
            set {
                this.ipAplicacionField = value;
                this.RaisePropertyChanged("ipAplicacion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string nombreAplicacion {
            get {
                return this.nombreAplicacionField;
            }
            set {
                this.nombreAplicacionField = value;
                this.RaisePropertyChanged("nombreAplicacion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string usuarioAplicacion {
            get {
                return this.usuarioAplicacionField;
            }
            set {
                this.usuarioAplicacionField = value;
                this.RaisePropertyChanged("usuarioAplicacion");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class auditResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string codigoRespuestaField;
        
        private string mensajeRespuestaField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
                this.RaisePropertyChanged("idTransaccion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string codigoRespuesta {
            get {
                return this.codigoRespuestaField;
            }
            set {
                this.codigoRespuestaField = value;
                this.RaisePropertyChanged("codigoRespuesta");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string mensajeRespuesta {
            get {
                return this.mensajeRespuestaField;
            }
            set {
                this.mensajeRespuestaField = value;
                this.RaisePropertyChanged("mensajeRespuesta");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class parametrosTypeObjetoOpcional : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string campoField;
        
        private string valorField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string campo {
            get {
                return this.campoField;
            }
            set {
                this.campoField = value;
                this.RaisePropertyChanged("campo");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string valor {
            get {
                return this.valorField;
            }
            set {
                this.valorField = value;
                this.RaisePropertyChanged("valor");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS/types")]
    public partial class validarLineaResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditResponseType auditResponseField;
        
        private bool flagField;
        
        private parametrosTypeObjetoOpcional[] listaResponseOpcionalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public auditResponseType auditResponse {
            get {
                return this.auditResponseField;
            }
            set {
                this.auditResponseField = value;
                this.RaisePropertyChanged("auditResponse");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public bool flag {
            get {
                return this.flagField;
            }
            set {
                this.flagField = value;
                this.RaisePropertyChanged("flag");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("objetoOpcional", Namespace="http://claro.com.pe/eai/ws/baseschema", IsNullable=false)]
        public parametrosTypeObjetoOpcional[] listaResponseOpcional {
            get {
                return this.listaResponseOpcionalField;
            }
            set {
                this.listaResponseOpcionalField = value;
                this.RaisePropertyChanged("listaResponseOpcional");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class validarLineaRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS/types", Order=0)]
        public Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaRequest validarLineaRequest;
        
        public validarLineaRequest1() {
        }
        
        public validarLineaRequest1(Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaRequest validarLineaRequest) {
            this.validarLineaRequest = validarLineaRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class validarLineaResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS/types", Order=0)]
        public Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaResponse validarLineaResponse;
        
        public validarLineaResponse1() {
        }
        
        public validarLineaResponse1(Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaResponse validarLineaResponse) {
            this.validarLineaResponse = validarLineaResponse;
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS/types")]
    public partial class validarPlanRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditRequestType auditRequestField;
        
        private string tmcodeField;
        
        private parametrosTypeObjetoOpcional[] listaRequestOpcionalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public auditRequestType auditRequest {
            get {
                return this.auditRequestField;
            }
            set {
                this.auditRequestField = value;
                this.RaisePropertyChanged("auditRequest");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string tmcode {
            get {
                return this.tmcodeField;
            }
            set {
                this.tmcodeField = value;
                this.RaisePropertyChanged("tmcode");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("objetoOpcional", Namespace="http://claro.com.pe/eai/ws/baseschema", IsNullable=false)]
        public parametrosTypeObjetoOpcional[] listaRequestOpcional {
            get {
                return this.listaRequestOpcionalField;
            }
            set {
                this.listaRequestOpcionalField = value;
                this.RaisePropertyChanged("listaRequestOpcional");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS/types")]
    public partial class validarPlanResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditResponseType auditResponseField;
        
        private bool flagField;
        
        private parametrosTypeObjetoOpcional[] listaResponseOpcionalField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public auditResponseType auditResponse {
            get {
                return this.auditResponseField;
            }
            set {
                this.auditResponseField = value;
                this.RaisePropertyChanged("auditResponse");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public bool flag {
            get {
                return this.flagField;
            }
            set {
                this.flagField = value;
                this.RaisePropertyChanged("flag");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=2)]
        [System.Xml.Serialization.XmlArrayItemAttribute("objetoOpcional", Namespace="http://claro.com.pe/eai/ws/baseschema", IsNullable=false)]
        public parametrosTypeObjetoOpcional[] listaResponseOpcional {
            get {
                return this.listaResponseOpcionalField;
            }
            set {
                this.listaResponseOpcionalField = value;
                this.RaisePropertyChanged("listaResponseOpcional");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class validarPlanRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS/types", Order=0)]
        public Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanRequest validarPlanRequest;
        
        public validarPlanRequest1() {
        }
        
        public validarPlanRequest1(Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanRequest validarPlanRequest) {
            this.validarPlanRequest = validarPlanRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class validarPlanResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/ventas/validacion4GLTEWS/types", Order=0)]
        public Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanResponse validarPlanResponse;
        
        public validarPlanResponse1() {
        }
        
        public validarPlanResponse1(Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanResponse validarPlanResponse) {
            this.validarPlanResponse = validarPlanResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Validacion4GLTEWSPortTypeChannel : Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.Validacion4GLTEWSPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Validacion4GLTEWSPortTypeClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.Validacion4GLTEWSPortType>, Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.Validacion4GLTEWSPortType {
        
        public Validacion4GLTEWSPortTypeClient() {
        }
        
        public Validacion4GLTEWSPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Validacion4GLTEWSPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Validacion4GLTEWSPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Validacion4GLTEWSPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaResponse1 Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.Validacion4GLTEWSPortType.validarLinea(Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaRequest1 request) {
            return base.Channel.validarLinea(request);
        }
        
        public Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaResponse validarLinea(Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaRequest validarLineaRequest) {
            Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaRequest1();
            inValue.validarLineaRequest = validarLineaRequest;
            Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarLineaResponse1 retVal = ((Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.Validacion4GLTEWSPortType)(this)).validarLinea(inValue);
            return retVal.validarLineaResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanResponse1 Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.Validacion4GLTEWSPortType.validarPlan(Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanRequest1 request) {
            return base.Channel.validarPlan(request);
        }
        
        public Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanResponse validarPlan(Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanRequest validarPlanRequest) {
            Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanRequest1();
            inValue.validarPlanRequest = validarPlanRequest;
            Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.validarPlanResponse1 retVal = ((Claro.SIACU.ProxyService.SIACPost.Validate4GLTE.Validacion4GLTEWSPortType)(this)).validarPlan(inValue);
            return retVal.validarPlanResponse;
        }
    }
}
