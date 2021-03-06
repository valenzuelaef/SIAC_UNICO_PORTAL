//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.SIACU.Post.Lines
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/detalleproductopostpagoclientews", ConfigurationName="SIACU.Post.Lines.DetalleProductoPostpagoClienteWSPortType")]
    public interface DetalleProductoPostpagoClienteWSPortType {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación consultarDetalleProductoPostpago no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ws/postventa/detalleproductopostpagoclientews/consultarDe" +
            "talleProductoPostpago", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACU.Post.Lines.consultarDetalleProductoPostpagoResponse1 consultarDetalleProductoPostpago(Claro.SIACU.ProxyService.SIACU.Post.Lines.consultarDetalleProductoPostpagoRequest1 request);
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/postventa/detalleproductopostpagoclientews/types")]
    public partial class ConsultarDetalleProductoPostpagoRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditRequestType auditRequestField;
        
        private string customerIdField;
        
        private string codigoProductoField;
        
        private string idPlanField;
        
        private string estadoField;
        
        private string origenDatosField;
        
        private string tipoProductoField;
        
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
        public string customerId {
            get {
                return this.customerIdField;
            }
            set {
                this.customerIdField = value;
                this.RaisePropertyChanged("customerId");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string codigoProducto {
            get {
                return this.codigoProductoField;
            }
            set {
                this.codigoProductoField = value;
                this.RaisePropertyChanged("codigoProducto");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string idPlan {
            get {
                return this.idPlanField;
            }
            set {
                this.idPlanField = value;
                this.RaisePropertyChanged("idPlan");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = value;
                this.RaisePropertyChanged("estado");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string origenDatos {
            get {
                return this.origenDatosField;
            }
            set {
                this.origenDatosField = value;
                this.RaisePropertyChanged("origenDatos");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string tipoProducto {
            get {
                return this.tipoProductoField;
            }
            set {
                this.tipoProductoField = value;
                this.RaisePropertyChanged("tipoProducto");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=7)]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/detalleproductopostpagoclientews/types")]
    public partial class servicioType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string coIdField;
        
        private string telefonoField;
        
        private string estadoField;
        
        private System.DateTime fechaActivacionField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string coId {
            get {
                return this.coIdField;
            }
            set {
                this.coIdField = value;
                this.RaisePropertyChanged("coId");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string telefono {
            get {
                return this.telefonoField;
            }
            set {
                this.telefonoField = value;
                this.RaisePropertyChanged("telefono");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string estado {
            get {
                return this.estadoField;
            }
            set {
                this.estadoField = value;
                this.RaisePropertyChanged("estado");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public System.DateTime fechaActivacion {
            get {
                return this.fechaActivacionField;
            }
            set {
                this.fechaActivacionField = value;
                this.RaisePropertyChanged("fechaActivacion");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/detalleproductopostpagoclientews/types")]
    public partial class datosPostpagoResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string origenDatosField;
        
        private servicioType[] listaServiciosField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string origenDatos {
            get {
                return this.origenDatosField;
            }
            set {
                this.origenDatosField = value;
                this.RaisePropertyChanged("origenDatos");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("servicio", IsNullable=false)]
        public servicioType[] listaServicios {
            get {
                return this.listaServiciosField;
            }
            set {
                this.listaServiciosField = value;
                this.RaisePropertyChanged("listaServicios");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/postventa/detalleproductopostpagoclientews/types")]
    public partial class ConsultarDetalleProductoPostpagoResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditResponseType auditResponseField;
        
        private datosPostpagoResponseType datosPostpagoResponseField;
        
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
        public datosPostpagoResponseType datosPostpagoResponse {
            get {
                return this.datosPostpagoResponseField;
            }
            set {
                this.datosPostpagoResponseField = value;
                this.RaisePropertyChanged("datosPostpagoResponse");
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
    public partial class consultarDetalleProductoPostpagoRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/detalleproductopostpagoclientews/types", Order=0)]
        public Claro.SIACU.ProxyService.SIACU.Post.Lines.ConsultarDetalleProductoPostpagoRequest ConsultarDetalleProductoPostpagoRequest;
        
        public consultarDetalleProductoPostpagoRequest1() {
        }
        
        public consultarDetalleProductoPostpagoRequest1(Claro.SIACU.ProxyService.SIACU.Post.Lines.ConsultarDetalleProductoPostpagoRequest ConsultarDetalleProductoPostpagoRequest) {
            this.ConsultarDetalleProductoPostpagoRequest = ConsultarDetalleProductoPostpagoRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class consultarDetalleProductoPostpagoResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/detalleproductopostpagoclientews/types", Order=0)]
        public Claro.SIACU.ProxyService.SIACU.Post.Lines.ConsultarDetalleProductoPostpagoResponse ConsultarDetalleProductoPostpagoResponse;
        
        public consultarDetalleProductoPostpagoResponse1() {
        }
        
        public consultarDetalleProductoPostpagoResponse1(Claro.SIACU.ProxyService.SIACU.Post.Lines.ConsultarDetalleProductoPostpagoResponse ConsultarDetalleProductoPostpagoResponse) {
            this.ConsultarDetalleProductoPostpagoResponse = ConsultarDetalleProductoPostpagoResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DetalleProductoPostpagoClienteWSPortTypeChannel : Claro.SIACU.ProxyService.SIACU.Post.Lines.DetalleProductoPostpagoClienteWSPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DetalleProductoPostpagoClienteWSPortTypeClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.SIACU.Post.Lines.DetalleProductoPostpagoClienteWSPortType>, Claro.SIACU.ProxyService.SIACU.Post.Lines.DetalleProductoPostpagoClienteWSPortType {
        
        public DetalleProductoPostpagoClienteWSPortTypeClient() {
        }
        
        public DetalleProductoPostpagoClienteWSPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DetalleProductoPostpagoClienteWSPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DetalleProductoPostpagoClienteWSPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DetalleProductoPostpagoClienteWSPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACU.Post.Lines.consultarDetalleProductoPostpagoResponse1 Claro.SIACU.ProxyService.SIACU.Post.Lines.DetalleProductoPostpagoClienteWSPortType.consultarDetalleProductoPostpago(Claro.SIACU.ProxyService.SIACU.Post.Lines.consultarDetalleProductoPostpagoRequest1 request) {
            return base.Channel.consultarDetalleProductoPostpago(request);
        }
        
        public Claro.SIACU.ProxyService.SIACU.Post.Lines.ConsultarDetalleProductoPostpagoResponse consultarDetalleProductoPostpago(Claro.SIACU.ProxyService.SIACU.Post.Lines.ConsultarDetalleProductoPostpagoRequest ConsultarDetalleProductoPostpagoRequest) {
            Claro.SIACU.ProxyService.SIACU.Post.Lines.consultarDetalleProductoPostpagoRequest1 inValue = new Claro.SIACU.ProxyService.SIACU.Post.Lines.consultarDetalleProductoPostpagoRequest1();
            inValue.ConsultarDetalleProductoPostpagoRequest = ConsultarDetalleProductoPostpagoRequest;
            Claro.SIACU.ProxyService.SIACU.Post.Lines.consultarDetalleProductoPostpagoResponse1 retVal = ((Claro.SIACU.ProxyService.SIACU.Post.Lines.DetalleProductoPostpagoClienteWSPortType)(this)).consultarDetalleProductoPostpago(inValue);
            return retVal.ConsultarDetalleProductoPostpagoResponse;
        }
    }
}
