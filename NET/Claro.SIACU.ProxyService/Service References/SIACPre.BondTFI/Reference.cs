//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.SIACPre.BondTFI
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin", ConfigurationName="SIACPre.BondTFI.ebsOperacionesINPortType")]
    public interface ebsOperacionesINPortType {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación consultarLineaIN no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/esb/services/postventa/operacionesin/consultarLineaIN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINResponse1 consultarLineaIN(Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/esb/services/postventa/operacionesin/consultarLineaIN", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINResponse1> consultarLineaINAsync(Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest1 request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que la operación ejecutarOperacionIN no es RPC ni está encapsulada en un documento.
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/esb/services/postventa/operacionesin/ejecutarOperacionIN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINResponse1 ejecutarOperacionIN(Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest1 request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/esb/services/postventa/operacionesin/ejecutarOperacionIN", ReplyAction="*")]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINResponse1> ejecutarOperacionINAsync(Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest1 request);
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class consultarLineaINRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string nombreAplicacionField;
        
        private string ipAplicacionField;
        
        private string msisdnField;
        
        private string inField;
        
        private string[] listaParametrosRequestField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string msisdn {
            get {
                return this.msisdnField;
            }
            set {
                this.msisdnField = value;
                this.RaisePropertyChanged("msisdn");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string @in {
            get {
                return this.inField;
            }
            set {
                this.inField = value;
                this.RaisePropertyChanged("in");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=5)]
        [System.Xml.Serialization.XmlArrayItemAttribute("parametro", IsNullable=false)]
        public string[] listaParametrosRequest {
            get {
                return this.listaParametrosRequestField;
            }
            set {
                this.listaParametrosRequestField = value;
                this.RaisePropertyChanged("listaParametrosRequest");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class ParametrosObjectType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string parametroField;
        
        private string valorField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string parametro {
            get {
                return this.parametroField;
            }
            set {
                this.parametroField = value;
                this.RaisePropertyChanged("parametro");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class consultarLineaINResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string codigoRespuestaField;
        
        private string mensajeRespuestaField;
        
        private string customeridField;
        
        private ParametrosObjectType[] listaParametrosResponseField;
        
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
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string customerid {
            get {
                return this.customeridField;
            }
            set {
                this.customeridField = value;
                this.RaisePropertyChanged("customerid");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=4)]
        [System.Xml.Serialization.XmlArrayItemAttribute("parametrosObject", IsNullable=false)]
        public ParametrosObjectType[] listaParametrosResponse {
            get {
                return this.listaParametrosResponseField;
            }
            set {
                this.listaParametrosResponseField = value;
                this.RaisePropertyChanged("listaParametrosResponse");
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
    public partial class consultarLineaINRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin", Order=0)]
        public Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest consultarLineaINRequest;
        
        public consultarLineaINRequest1() {
        }
        
        public consultarLineaINRequest1(Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest consultarLineaINRequest) {
            this.consultarLineaINRequest = consultarLineaINRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class consultarLineaINResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin", Order=0)]
        public Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINResponse consultarLineaINResponse;
        
        public consultarLineaINResponse1() {
        }
        
        public consultarLineaINResponse1(Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINResponse consultarLineaINResponse) {
            this.consultarLineaINResponse = consultarLineaINResponse;
        }
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class ejecutarOperacionINRequest : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string nombreAplicacionField;
        
        private string ipAplicacionField;
        
        private string msisdnField;
        
        private string operacionField;
        
        private string inField;
        
        private ParametrosObjectType[] listaParametrosRequestField;
        
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
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string msisdn {
            get {
                return this.msisdnField;
            }
            set {
                this.msisdnField = value;
                this.RaisePropertyChanged("msisdn");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string operacion {
            get {
                return this.operacionField;
            }
            set {
                this.operacionField = value;
                this.RaisePropertyChanged("operacion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string @in {
            get {
                return this.inField;
            }
            set {
                this.inField = value;
                this.RaisePropertyChanged("in");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=6)]
        [System.Xml.Serialization.XmlArrayItemAttribute("parametrosObject", IsNullable=false)]
        public ParametrosObjectType[] listaParametrosRequest {
            get {
                return this.listaParametrosRequestField;
            }
            set {
                this.listaParametrosRequestField = value;
                this.RaisePropertyChanged("listaParametrosRequest");
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin")]
    public partial class ejecutarOperacionINResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idTransaccionField;
        
        private string codigoRespuestaField;
        
        private string mensajeRespuestaField;
        
        private ParametrosObjectType[] listaParametrosResponseField;
        
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
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=3)]
        [System.Xml.Serialization.XmlArrayItemAttribute("parametrosObject", IsNullable=false)]
        public ParametrosObjectType[] listaParametrosResponse {
            get {
                return this.listaParametrosResponseField;
            }
            set {
                this.listaParametrosResponseField = value;
                this.RaisePropertyChanged("listaParametrosResponse");
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
    public partial class ejecutarOperacionINRequest1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin", Order=0)]
        public Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest ejecutarOperacionINRequest;
        
        public ejecutarOperacionINRequest1() {
        }
        
        public ejecutarOperacionINRequest1(Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest ejecutarOperacionINRequest) {
            this.ejecutarOperacionINRequest = ejecutarOperacionINRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ejecutarOperacionINResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/esb/services/postventa/operacionesin", Order=0)]
        public Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINResponse ejecutarOperacionINResponse;
        
        public ejecutarOperacionINResponse1() {
        }
        
        public ejecutarOperacionINResponse1(Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINResponse ejecutarOperacionINResponse) {
            this.ejecutarOperacionINResponse = ejecutarOperacionINResponse;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ebsOperacionesINPortTypeChannel : Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ebsOperacionesINPortTypeClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType>, Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType {
        
        public ebsOperacionesINPortTypeClient() {
        }
        
        public ebsOperacionesINPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ebsOperacionesINPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ebsOperacionesINPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ebsOperacionesINPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINResponse1 Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType.consultarLineaIN(Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest1 request) {
            return base.Channel.consultarLineaIN(request);
        }
        
        public Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINResponse consultarLineaIN(Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest consultarLineaINRequest) {
            Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest1 inValue = new Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest1();
            inValue.consultarLineaINRequest = consultarLineaINRequest;
            Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINResponse1 retVal = ((Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType)(this)).consultarLineaIN(inValue);
            return retVal.consultarLineaINResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINResponse1> Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType.consultarLineaINAsync(Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest1 request) {
            return base.Channel.consultarLineaINAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINResponse1> consultarLineaINAsync(Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest consultarLineaINRequest) {
            Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest1 inValue = new Claro.SIACU.ProxyService.SIACPre.BondTFI.consultarLineaINRequest1();
            inValue.consultarLineaINRequest = consultarLineaINRequest;
            return ((Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType)(this)).consultarLineaINAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINResponse1 Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType.ejecutarOperacionIN(Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest1 request) {
            return base.Channel.ejecutarOperacionIN(request);
        }
        
        public Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINResponse ejecutarOperacionIN(Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest ejecutarOperacionINRequest) {
            Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest1 inValue = new Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest1();
            inValue.ejecutarOperacionINRequest = ejecutarOperacionINRequest;
            Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINResponse1 retVal = ((Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType)(this)).ejecutarOperacionIN(inValue);
            return retVal.ejecutarOperacionINResponse;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINResponse1> Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType.ejecutarOperacionINAsync(Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest1 request) {
            return base.Channel.ejecutarOperacionINAsync(request);
        }
        
        public System.Threading.Tasks.Task<Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINResponse1> ejecutarOperacionINAsync(Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest ejecutarOperacionINRequest) {
            Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest1 inValue = new Claro.SIACU.ProxyService.SIACPre.BondTFI.ejecutarOperacionINRequest1();
            inValue.ejecutarOperacionINRequest = ejecutarOperacionINRequest;
            return ((Claro.SIACU.ProxyService.SIACPre.BondTFI.ebsOperacionesINPortType)(this)).ejecutarOperacionINAsync(inValue);
        }
    }
}
