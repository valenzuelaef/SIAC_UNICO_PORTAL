//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.SIACU.Post.Products
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/productospostpagoclientews", ConfigurationName="SIACU.Post.Products.ProductosPostpagoClienteWSPortType")]
    public interface ProductosPostpagoClienteWSPortType {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación consultarProductosPostpago no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ws/postventa/productospostpagoclientews/consultarProducto" +
            "sPostpago", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACU.Post.Products.consultarProductosPostpagoResponse1 consultarProductosPostpago(Claro.SIACU.ProxyService.SIACU.Post.Products.consultarProductosPostpagoRequest1 request);
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/postventa/productospostpagoclientews/types")]
    public partial class ConsultarProductosPostpagoRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private auditRequestType auditRequestField;
        
        private string tipoDocIdentidadField;
        
        private string docIdentidadField;
        
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
        public string tipoDocIdentidad {
            get {
                return this.tipoDocIdentidadField;
            }
            set {
                this.tipoDocIdentidadField = value;
                this.RaisePropertyChanged("tipoDocIdentidad");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string docIdentidad {
            get {
                return this.docIdentidadField;
            }
            set {
                this.docIdentidadField = value;
                this.RaisePropertyChanged("docIdentidad");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=3)]
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/productospostpagoclientews/types")]
    public partial class servicioType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codigoProductoField;
        
        private string tipoProductoField;
        
        private string productoField;
        
        private string idPlanField;
        
        private string cuentaField;
        
        private string codigoClienteField;
        
        private string tipoClienteField;
        
        private System.DateTime fechaActivacionCuentaField;
        
        private int nroServiciosActivosField;
        
        private int nroServiciosNoActivosField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string producto {
            get {
                return this.productoField;
            }
            set {
                this.productoField = value;
                this.RaisePropertyChanged("producto");
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
        public string cuenta {
            get {
                return this.cuentaField;
            }
            set {
                this.cuentaField = value;
                this.RaisePropertyChanged("cuenta");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string codigoCliente {
            get {
                return this.codigoClienteField;
            }
            set {
                this.codigoClienteField = value;
                this.RaisePropertyChanged("codigoCliente");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string tipoCliente {
            get {
                return this.tipoClienteField;
            }
            set {
                this.tipoClienteField = value;
                this.RaisePropertyChanged("tipoCliente");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public System.DateTime fechaActivacionCuenta {
            get {
                return this.fechaActivacionCuentaField;
            }
            set {
                this.fechaActivacionCuentaField = value;
                this.RaisePropertyChanged("fechaActivacionCuenta");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public int nroServiciosActivos {
            get {
                return this.nroServiciosActivosField;
            }
            set {
                this.nroServiciosActivosField = value;
                this.RaisePropertyChanged("nroServiciosActivos");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public int nroServiciosNoActivos {
            get {
                return this.nroServiciosNoActivosField;
            }
            set {
                this.nroServiciosNoActivosField = value;
                this.RaisePropertyChanged("nroServiciosNoActivos");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/productospostpagoclientews/types")]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ws/postventa/productospostpagoclientews/types")]
    public partial class ConsultarProductosPostpagoResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
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
    public partial class consultarProductosPostpagoRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/productospostpagoclientews/types", Order=0)]
        public Claro.SIACU.ProxyService.SIACU.Post.Products.ConsultarProductosPostpagoRequest ConsultarProductosPostpagoRequest;
        
        public consultarProductosPostpagoRequest1() {
        }
        
        public consultarProductosPostpagoRequest1(Claro.SIACU.ProxyService.SIACU.Post.Products.ConsultarProductosPostpagoRequest ConsultarProductosPostpagoRequest) {
            this.ConsultarProductosPostpagoRequest = ConsultarProductosPostpagoRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class consultarProductosPostpagoResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ws/postventa/productospostpagoclientews/types", Order=0)]
        public Claro.SIACU.ProxyService.SIACU.Post.Products.ConsultarProductosPostpagoResponse ConsultarProductosPostpagoResponse;
        
        public consultarProductosPostpagoResponse1() {
        }
        
        public consultarProductosPostpagoResponse1(Claro.SIACU.ProxyService.SIACU.Post.Products.ConsultarProductosPostpagoResponse ConsultarProductosPostpagoResponse) {
            this.ConsultarProductosPostpagoResponse = ConsultarProductosPostpagoResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ProductosPostpagoClienteWSPortTypeChannel : Claro.SIACU.ProxyService.SIACU.Post.Products.ProductosPostpagoClienteWSPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProductosPostpagoClienteWSPortTypeClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.SIACU.Post.Products.ProductosPostpagoClienteWSPortType>, Claro.SIACU.ProxyService.SIACU.Post.Products.ProductosPostpagoClienteWSPortType {
        
        public ProductosPostpagoClienteWSPortTypeClient() {
        }
        
        public ProductosPostpagoClienteWSPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProductosPostpagoClienteWSPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductosPostpagoClienteWSPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProductosPostpagoClienteWSPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACU.Post.Products.consultarProductosPostpagoResponse1 Claro.SIACU.ProxyService.SIACU.Post.Products.ProductosPostpagoClienteWSPortType.consultarProductosPostpago(Claro.SIACU.ProxyService.SIACU.Post.Products.consultarProductosPostpagoRequest1 request) {
            return base.Channel.consultarProductosPostpago(request);
        }
        
        public Claro.SIACU.ProxyService.SIACU.Post.Products.ConsultarProductosPostpagoResponse consultarProductosPostpago(Claro.SIACU.ProxyService.SIACU.Post.Products.ConsultarProductosPostpagoRequest ConsultarProductosPostpagoRequest) {
            Claro.SIACU.ProxyService.SIACU.Post.Products.consultarProductosPostpagoRequest1 inValue = new Claro.SIACU.ProxyService.SIACU.Post.Products.consultarProductosPostpagoRequest1();
            inValue.ConsultarProductosPostpagoRequest = ConsultarProductosPostpagoRequest;
            Claro.SIACU.ProxyService.SIACU.Post.Products.consultarProductosPostpagoResponse1 retVal = ((Claro.SIACU.ProxyService.SIACU.Post.Products.ProductosPostpagoClienteWSPortType)(this)).consultarProductosPostpago(inValue);
            return retVal.ConsultarProductosPostpagoResponse;
        }
    }
}
