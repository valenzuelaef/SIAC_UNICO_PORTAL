//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/peticionanotacionws", ConfigurationName="SIACPost.PeticionAnotacion.PeticionAnotacionWSPortType")]
    public interface PeticionAnotacionWSPortType {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación consultar no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ws/postventa/peticionanotacionws/consultar", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarResponse1 consultar(Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ws/postventa/peticionanotacionws/consultar", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarResponse1> consultarAsync(Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest1 request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/postventa/peticionanotacionws/types")]
    public partial class consultarRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditRequestType auditRequestField;
        
        private string customer_idField;
        
        private string co_idField;
        
        private string cstypeField;
        
        private string tickler_numberField;
        
        private parametrosTypeObjetoOpcional[] listaRequestOpcionalField;
        
        /// <remarks/>
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
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string customer_id {
            get {
                return this.customer_idField;
            }
            set {
                this.customer_idField = value;
                this.RaisePropertyChanged("customer_id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string co_id {
            get {
                return this.co_idField;
            }
            set {
                this.co_idField = value;
                this.RaisePropertyChanged("co_id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string cstype {
            get {
                return this.cstypeField;
            }
            set {
                this.cstypeField = value;
                this.RaisePropertyChanged("cstype");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string tickler_number {
            get {
                return this.tickler_numberField;
            }
            set {
                this.tickler_numberField = value;
                this.RaisePropertyChanged("tickler_number");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=5)]
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class auditRequestType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string ipAplicacionField;
        
        private string nombreAplicacionField;
        
        private string usuarioAplicacionField;
        
        /// <remarks/>
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
        
        /// <remarks/>
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
        
        /// <remarks/>
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
        
        /// <remarks/>
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/peticionanotacionws/types")]
    public partial class itmPet_AnotacionType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string cuentaField;
        
        private string codigo_ticklerField;
        
        private string estado_anotacionField;
        
        private string prioridadField;
        
        private string descripcion_cortaField;
        
        private string usuario_seguimientoField;
        
        private string fecha_seguimientoField;
        
        private string accion_seguimientoField;
        
        private string fecha_aperturaField;
        
        private string fecha_cierreField;
        
        private string nro_ticklerField;
        
        private string estadoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string cuenta {
            get {
                return this.cuentaField;
            }
            set {
                this.cuentaField = value;
                this.RaisePropertyChanged("cuenta");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string codigo_tickler {
            get {
                return this.codigo_ticklerField;
            }
            set {
                this.codigo_ticklerField = value;
                this.RaisePropertyChanged("codigo_tickler");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string estado_anotacion {
            get {
                return this.estado_anotacionField;
            }
            set {
                this.estado_anotacionField = value;
                this.RaisePropertyChanged("estado_anotacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string prioridad {
            get {
                return this.prioridadField;
            }
            set {
                this.prioridadField = value;
                this.RaisePropertyChanged("prioridad");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string descripcion_corta {
            get {
                return this.descripcion_cortaField;
            }
            set {
                this.descripcion_cortaField = value;
                this.RaisePropertyChanged("descripcion_corta");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string usuario_seguimiento {
            get {
                return this.usuario_seguimientoField;
            }
            set {
                this.usuario_seguimientoField = value;
                this.RaisePropertyChanged("usuario_seguimiento");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string fecha_seguimiento {
            get {
                return this.fecha_seguimientoField;
            }
            set {
                this.fecha_seguimientoField = value;
                this.RaisePropertyChanged("fecha_seguimiento");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string accion_seguimiento {
            get {
                return this.accion_seguimientoField;
            }
            set {
                this.accion_seguimientoField = value;
                this.RaisePropertyChanged("accion_seguimiento");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string fecha_apertura {
            get {
                return this.fecha_aperturaField;
            }
            set {
                this.fecha_aperturaField = value;
                this.RaisePropertyChanged("fecha_apertura");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string fecha_cierre {
            get {
                return this.fecha_cierreField;
            }
            set {
                this.fecha_cierreField = value;
                this.RaisePropertyChanged("fecha_cierre");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string nro_tickler {
            get {
                return this.nro_ticklerField;
            }
            set {
                this.nro_ticklerField = value;
                this.RaisePropertyChanged("nro_tickler");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = value;
                this.RaisePropertyChanged("estado");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class auditResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string codigoRespuestaField;
        
        private string mensajeRespuestaField;
        
        /// <remarks/>
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
        
        /// <remarks/>
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
        
        /// <remarks/>
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/baseschema")]
    public partial class parametrosTypeObjetoOpcional : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string campoField;
        
        private string valorField;
        
        /// <remarks/>
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
        
        /// <remarks/>
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/postventa/peticionanotacionws/types")]
    public partial class consultarResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditResponseType auditResponseField;
        
        private itmPet_AnotacionType[] listaPet_AnotacionResponseField;
        
        private parametrosTypeObjetoOpcional[] listaResponseOpcionalField;
        
        /// <remarks/>
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
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("itmPet_Anotacion", IsNullable=false)]
        public itmPet_AnotacionType[] listaPet_AnotacionResponse {
            get {
                return this.listaPet_AnotacionResponseField;
            }
            set {
                this.listaPet_AnotacionResponseField = value;
                this.RaisePropertyChanged("listaPet_AnotacionResponse");
            }
        }
        
        /// <remarks/>
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
    public partial class consultarRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/peticionanotacionws/types", Order=0)]
        public Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest consultarRequest;
        
        public consultarRequest1() {
        }
        
        public consultarRequest1(Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest consultarRequest) {
            this.consultarRequest = consultarRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class consultarResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/peticionanotacionws/types", Order=0)]
        public Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarResponse consultarResponse;
        
        public consultarResponse1() {
        }
        
        public consultarResponse1(Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarResponse consultarResponse) {
            this.consultarResponse = consultarResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PeticionAnotacionWSPortTypeChannel : Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.PeticionAnotacionWSPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PeticionAnotacionWSPortTypeClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.PeticionAnotacionWSPortType>, Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.PeticionAnotacionWSPortType {
        
        public PeticionAnotacionWSPortTypeClient() {
        }
        
        public PeticionAnotacionWSPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PeticionAnotacionWSPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PeticionAnotacionWSPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PeticionAnotacionWSPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarResponse1 Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.PeticionAnotacionWSPortType.consultar(Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest1 request) {
            return base.Channel.consultar(request);
        }
        
        public Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarResponse consultar(Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest consultarRequest) {
            Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest1();
            inValue.consultarRequest = consultarRequest;
            Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarResponse1 retVal = ((Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.PeticionAnotacionWSPortType)(this)).consultar(inValue);
            return retVal.consultarResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarResponse1> Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.PeticionAnotacionWSPortType.consultarAsync(Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest1 request) {
            return base.Channel.consultarAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarResponse1> consultarAsync(Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest consultarRequest) {
            Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.consultarRequest1();
            inValue.consultarRequest = consultarRequest;
            return ((Claro.SIACU.ProxyService.SIACPost.PeticionAnotacion.PeticionAnotacionWSPortType)(this)).consultarAsync(inValue);
        }
    }
}
