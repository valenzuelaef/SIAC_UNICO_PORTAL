//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.SIACPost.HLRUDB
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws", ConfigurationName="SIACPost.HLRUDB.UDBWSPortTypeSOAP11")]
    public interface UDBWSPortTypeSOAP11 {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación consultar no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ConnectorUdb/ws/Consulta", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1 consultar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ConnectorUdb/ws/Consulta", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1> consultarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación ejecutar no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ConnectorUdb/ws/Ejecucion", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1 ejecutar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ConnectorUdb/ws/Ejecucion", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1> ejecutarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 request);
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/Consulta")]
    public partial class consultarRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private parametrosAuditRequest auditRequestField;
        
        private accionType accionRequestField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public parametrosAuditRequest auditRequest {
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
        public accionType accionRequest {
            get {
                return this.accionRequestField;
            }
            set {
                this.accionRequestField = value;
                this.RaisePropertyChanged("accionRequest");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/types")]
    public partial class parametrosAuditRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/Ejecucion")]
    public partial class ejecutarResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private parametrosAuditResponse auditResponseField;
        
        private accionType accionResponseField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public parametrosAuditResponse auditResponse {
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
        public accionType accionResponse {
            get {
                return this.accionResponseField;
            }
            set {
                this.accionResponseField = value;
                this.RaisePropertyChanged("accionResponse");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/types")]
    public partial class parametrosAuditResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string codRespuestaField;
        
        private string msjRespuestaField;
        
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
        public string codRespuesta {
            get {
                return this.codRespuestaField;
            }
            set {
                this.codRespuestaField = value;
                this.RaisePropertyChanged("codRespuesta");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string msjRespuesta {
            get {
                return this.msjRespuestaField;
            }
            set {
                this.msjRespuestaField = value;
                this.RaisePropertyChanged("msjRespuesta");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/types")]
    public partial class accionType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idAccionField;
        
        private listaParametros[] listaParametrosField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string idAccion {
            get {
                return this.idAccionField;
            }
            set {
                this.idAccionField = value;
                this.RaisePropertyChanged("idAccion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("listaParametros", Order=1)]
        public listaParametros[] listaParametros {
            get {
                return this.listaParametrosField;
            }
            set {
                this.listaParametrosField = value;
                this.RaisePropertyChanged("listaParametros");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/types")]
    public partial class listaParametros : object, System.ComponentModel.INotifyPropertyChanged {
        
        private listaParametrosParametro[] parametroField;
        
        private subListaParametros[] subListaParametrosField;
        
        private string nombreListaField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("parametro", Order=0)]
        public listaParametrosParametro[] parametro {
            get {
                return this.parametroField;
            }
            set {
                this.parametroField = value;
                this.RaisePropertyChanged("parametro");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("subListaParametros", Order=1)]
        public subListaParametros[] subListaParametros {
            get {
                return this.subListaParametrosField;
            }
            set {
                this.subListaParametrosField = value;
                this.RaisePropertyChanged("subListaParametros");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombreLista {
            get {
                return this.nombreListaField;
            }
            set {
                this.nombreListaField = value;
                this.RaisePropertyChanged("nombreLista");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/types")]
    public partial class listaParametrosParametro : object, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/types")]
    public partial class subListaParametros : object, System.ComponentModel.INotifyPropertyChanged {
        
        private subListaParametrosParametro[] parametroField;
        
        private string nombreListaField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute("parametro", Order=0)]
        public subListaParametrosParametro[] parametro {
            get {
                return this.parametroField;
            }
            set {
                this.parametroField = value;
                this.RaisePropertyChanged("parametro");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nombreLista {
            get {
                return this.nombreListaField;
            }
            set {
                this.nombreListaField = value;
                this.RaisePropertyChanged("nombreLista");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/types")]
    public partial class subListaParametrosParametro : object, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/Ejecucion")]
    public partial class ejecutarRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private parametrosAuditRequest auditRequestField;
        
        private accionType accionRequestField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public parametrosAuditRequest auditRequest {
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
        public accionType accionRequest {
            get {
                return this.accionRequestField;
            }
            set {
                this.accionRequestField = value;
                this.RaisePropertyChanged("accionRequest");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/Consulta")]
    public partial class consultarResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private parametrosAuditResponse auditResponseField;
        
        private accionType accionResponseField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public parametrosAuditResponse auditResponse {
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
        public accionType accionResponse {
            get {
                return this.accionResponseField;
            }
            set {
                this.accionResponseField = value;
                this.RaisePropertyChanged("accionResponse");
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
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/Consulta", Order=0)]
        public Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest consultarRequest;
        
        public consultarRequest1() {
        }
        
        public consultarRequest1(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest consultarRequest) {
            this.consultarRequest = consultarRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class consultarResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/Consulta", Order=0)]
        public Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse consultarResponse;
        
        public consultarResponse1() {
        }
        
        public consultarResponse1(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse consultarResponse) {
            this.consultarResponse = consultarResponse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ejecutarRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/Ejecucion", Order=0)]
        public Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest ejecutarRequest;
        
        public ejecutarRequest1() {
        }
        
        public ejecutarRequest1(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest ejecutarRequest) {
            this.ejecutarRequest = ejecutarRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ejecutarResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws/Ejecucion", Order=0)]
        public Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse ejecutarResponse;
        
        public ejecutarResponse1() {
        }
        
        public ejecutarResponse1(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse ejecutarResponse) {
            this.ejecutarResponse = ejecutarResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface UDBWSPortTypeSOAP11Channel : Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UDBWSPortTypeSOAP11Client : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11>, Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11 {
        
        public UDBWSPortTypeSOAP11Client() {
        }
        
        public UDBWSPortTypeSOAP11Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UDBWSPortTypeSOAP11Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UDBWSPortTypeSOAP11Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UDBWSPortTypeSOAP11Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1 Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11.consultar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 request) {
            return base.Channel.consultar(request);
        }
        
        public Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse consultar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest consultarRequest) {
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1();
            inValue.consultarRequest = consultarRequest;
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1 retVal = ((Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11)(this)).consultar(inValue);
            return retVal.consultarResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1> Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11.consultarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 request) {
            return base.Channel.consultarAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1> consultarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest consultarRequest) {
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1();
            inValue.consultarRequest = consultarRequest;
            return ((Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11)(this)).consultarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1 Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11.ejecutar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 request) {
            return base.Channel.ejecutar(request);
        }
        
        public Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse ejecutar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest ejecutarRequest) {
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1();
            inValue.ejecutarRequest = ejecutarRequest;
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1 retVal = ((Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11)(this)).ejecutar(inValue);
            return retVal.ejecutarResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1> Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11.ejecutarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 request) {
            return base.Channel.ejecutarAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1> ejecutarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest ejecutarRequest) {
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1();
            inValue.ejecutarRequest = ejecutarRequest;
            return ((Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP11)(this)).ejecutarAsync(inValue);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ConnectorUdb/ws", ConfigurationName="SIACPost.HLRUDB.UDBWSPortTypeSOAP12")]
    public interface UDBWSPortTypeSOAP12 {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación consultar no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ConnectorUdb/ws/Consulta", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1 consultar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ConnectorUdb/ws/Consulta", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1> consultarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación ejecutar no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ConnectorUdb/ws/Ejecucion", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1 ejecutar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ConnectorUdb/ws/Ejecucion", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1> ejecutarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface UDBWSPortTypeSOAP12Channel : Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UDBWSPortTypeSOAP12Client : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12>, Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12 {
        
        public UDBWSPortTypeSOAP12Client() {
        }
        
        public UDBWSPortTypeSOAP12Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UDBWSPortTypeSOAP12Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UDBWSPortTypeSOAP12Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UDBWSPortTypeSOAP12Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1 Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12.consultar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 request) {
            return base.Channel.consultar(request);
        }
        
        public Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse consultar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest consultarRequest) {
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1();
            inValue.consultarRequest = consultarRequest;
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1 retVal = ((Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12)(this)).consultar(inValue);
            return retVal.consultarResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1> Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12.consultarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 request) {
            return base.Channel.consultarAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarResponse1> consultarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest consultarRequest) {
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.HLRUDB.consultarRequest1();
            inValue.consultarRequest = consultarRequest;
            return ((Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12)(this)).consultarAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1 Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12.ejecutar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 request) {
            return base.Channel.ejecutar(request);
        }
        
        public Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse ejecutar(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest ejecutarRequest) {
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1();
            inValue.ejecutarRequest = ejecutarRequest;
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1 retVal = ((Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12)(this)).ejecutar(inValue);
            return retVal.ejecutarResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1> Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12.ejecutarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 request) {
            return base.Channel.ejecutarAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarResponse1> ejecutarAsync(Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest ejecutarRequest) {
            Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1 inValue = new Claro.SIACU.ProxyService.SIACPost.HLRUDB.ejecutarRequest1();
            inValue.ejecutarRequest = ejecutarRequest;
            return ((Claro.SIACU.ProxyService.SIACPost.HLRUDB.UDBWSPortTypeSOAP12)(this)).ejecutarAsync(inValue);
        }
    }
}
