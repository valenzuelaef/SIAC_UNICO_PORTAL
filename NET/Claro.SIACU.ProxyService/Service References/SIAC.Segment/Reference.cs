﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Claro.SIACU.ProxyService.SIAC.Segment
{


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", ConfigurationName="SIAC.Segment.ebsConsultaSegmentoPortType")]
    public interface ebsConsultaSegmentoPortType {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de contenedor (ConsultaSegmentoRequest) del mensaje consultaSegmentoRequest no coincide con el valor predeterminado (consultaSegmento)
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente/consultaSegmento" +
            "", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmentoResponse consultaSegmento(Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmentoRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de contenedor (ConsultXTramRequest) del mensaje consultarSegmxTramaRequest no coincide con el valor predeterminado (consultarSegmxTrama)
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente/consultarSegment" +
            "oPorTrama", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIAC.Segment.consultarSegmxTramaResponse consultarSegmxTrama(Claro.SIACU.ProxyService.SIAC.Segment.consultarSegmxTramaRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de contenedor (consultaSegmHistoricoRequest) del mensaje consultaSegmHistoricoRequest no coincide con el valor predeterminado (consultaSegmHistorico)
        [System.ServiceModel.OperationContractAttribute(Action="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente/consultaSegmHist" +
            "orico", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmHistoricoResponse consultaSegmHistorico(Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmHistoricoRequest request);
    }
    
    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1532.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente")]
    public partial class AuditType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string usrAppField;
        
        private string ipAplicacionField;
        
        private string aplicacionField;
        
        private string idTransaccionField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string usrApp {
            get {
                return this.usrAppField;
            }
            set {
                this.usrAppField = value;
                this.RaisePropertyChanged("usrApp");
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
        public string aplicacion {
            get {
                return this.aplicacionField;
            }
            set {
                this.aplicacionField = value;
                this.RaisePropertyChanged("aplicacion");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string idTransaccion {
            get {
                return this.idTransaccionField;
            }
            set {
                this.idTransaccionField = value;
                this.RaisePropertyChanged("idTransaccion");
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente")]
    public partial class ListaConsultaSegmHistorico : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string fecInicioField;
        
        private string nroDocumentoField;
        
        private string segmentoField;
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string fecInicio {
            get {
                return this.fecInicioField;
            }
            set {
                this.fecInicioField = value;
                this.RaisePropertyChanged("fecInicio");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string nroDocumento {
            get {
                return this.nroDocumentoField;
            }
            set {
                this.nroDocumentoField = value;
                this.RaisePropertyChanged("nroDocumento");
            }
        }
        
        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string segmento {
            get {
                return this.segmentoField;
            }
            set {
                this.segmentoField = value;
                this.RaisePropertyChanged("segmento");
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
    [System.ServiceModel.MessageContractAttribute(WrapperName="ConsultaSegmentoRequest", WrapperNamespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", IsWrapped=true)]
    public partial class consultaSegmentoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=0)]
        public Claro.SIACU.ProxyService.SIAC.Segment.AuditType audit;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=1)]
        public string tipoDocumento;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=2)]
        public string nroDocumento;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=3)]
        public string longitudDocumento;
        
        public consultaSegmentoRequest() {
        }
        
        public consultaSegmentoRequest(Claro.SIACU.ProxyService.SIAC.Segment.AuditType audit, string tipoDocumento, string nroDocumento, string longitudDocumento) {
            this.audit = audit;
            this.tipoDocumento = tipoDocumento;
            this.nroDocumento = nroDocumento;
            this.longitudDocumento = longitudDocumento;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ConsultaSegmentoResponse", WrapperNamespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", IsWrapped=true)]
    public partial class consultaSegmentoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=0)]
        public string idTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=1)]
        public string codigoRespuesta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=2)]
        public string mensajeRespuesta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=3)]
        public string segmento;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=4)]
        public string nombreCliente;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=5)]
        public string mensaje1;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=6)]
        public string mensaje2;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=7)]
        public string mensaje3;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=8)]
        public string mensaje4;
        
        public consultaSegmentoResponse() {
        }
        
        public consultaSegmentoResponse(string idTransaccion, string codigoRespuesta, string mensajeRespuesta, string segmento, string nombreCliente, string mensaje1, string mensaje2, string mensaje3, string mensaje4) {
            this.idTransaccion = idTransaccion;
            this.codigoRespuesta = codigoRespuesta;
            this.mensajeRespuesta = mensajeRespuesta;
            this.segmento = segmento;
            this.nombreCliente = nombreCliente;
            this.mensaje1 = mensaje1;
            this.mensaje2 = mensaje2;
            this.mensaje3 = mensaje3;
            this.mensaje4 = mensaje4;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ConsultXTramRequest", WrapperNamespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", IsWrapped=true)]
    public partial class consultarSegmxTramaRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=0)]
        public string tramaConsulta;
        
        public consultarSegmxTramaRequest() {
        }
        
        public consultarSegmxTramaRequest(string tramaConsulta) {
            this.tramaConsulta = tramaConsulta;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ConsultXTramResponse", WrapperNamespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", IsWrapped=true)]
    public partial class consultarSegmxTramaResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=0)]
        public string tramaRespuesta;
        
        public consultarSegmxTramaResponse() {
        }
        
        public consultarSegmxTramaResponse(string tramaRespuesta) {
            this.tramaRespuesta = tramaRespuesta;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="consultaSegmHistoricoRequest", WrapperNamespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", IsWrapped=true)]
    public partial class consultaSegmHistoricoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=0)]
        public Claro.SIACU.ProxyService.SIAC.Segment.AuditType audit;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=1)]
        public string tipoDocumento;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=2)]
        public string nroDocumento;
        
        public consultaSegmHistoricoRequest() {
        }
        
        public consultaSegmHistoricoRequest(Claro.SIACU.ProxyService.SIAC.Segment.AuditType audit, string tipoDocumento, string nroDocumento) {
            this.audit = audit;
            this.tipoDocumento = tipoDocumento;
            this.nroDocumento = nroDocumento;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="consultaSegmHistoricoResponse", WrapperNamespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", IsWrapped=true)]
    public partial class consultaSegmHistoricoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=0)]
        public string idTransaccion;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=1)]
        public string codigoRespuesta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=2)]
        public string mensajeRespuesta;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://claro.com.pe/eai/ebs/ws/postventa/ConsultaSegmentoCliente", Order=3)]
        [System.Xml.Serialization.XmlElementAttribute("listaConsultaSegmHistorico")]
        public Claro.SIACU.ProxyService.SIAC.Segment.ListaConsultaSegmHistorico[] listaConsultaSegmHistorico;
        
        public consultaSegmHistoricoResponse() {
        }
        
        public consultaSegmHistoricoResponse(string idTransaccion, string codigoRespuesta, string mensajeRespuesta, Claro.SIACU.ProxyService.SIAC.Segment.ListaConsultaSegmHistorico[] listaConsultaSegmHistorico) {
            this.idTransaccion = idTransaccion;
            this.codigoRespuesta = codigoRespuesta;
            this.mensajeRespuesta = mensajeRespuesta;
            this.listaConsultaSegmHistorico = listaConsultaSegmHistorico;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ebsConsultaSegmentoPortTypeChannel : Claro.SIACU.ProxyService.SIAC.Segment.ebsConsultaSegmentoPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ebsConsultaSegmentoPortTypeClient : System.ServiceModel.ClientBase<Claro.SIACU.ProxyService.SIAC.Segment.ebsConsultaSegmentoPortType>, Claro.SIACU.ProxyService.SIAC.Segment.ebsConsultaSegmentoPortType {
        
        public ebsConsultaSegmentoPortTypeClient() {
        }
        
        public ebsConsultaSegmentoPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ebsConsultaSegmentoPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ebsConsultaSegmentoPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ebsConsultaSegmentoPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmentoResponse Claro.SIACU.ProxyService.SIAC.Segment.ebsConsultaSegmentoPortType.consultaSegmento(Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmentoRequest request) {
            return base.Channel.consultaSegmento(request);
        }
        
        public string consultaSegmento(Claro.SIACU.ProxyService.SIAC.Segment.AuditType audit, string tipoDocumento, string nroDocumento, string longitudDocumento, out string codigoRespuesta, out string mensajeRespuesta, out string segmento, out string nombreCliente, out string mensaje1, out string mensaje2, out string mensaje3, out string mensaje4) {
            Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmentoRequest inValue = new Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmentoRequest();
            inValue.audit = audit;
            inValue.tipoDocumento = tipoDocumento;
            inValue.nroDocumento = nroDocumento;
            inValue.longitudDocumento = longitudDocumento;
            Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmentoResponse retVal = ((Claro.SIACU.ProxyService.SIAC.Segment.ebsConsultaSegmentoPortType)(this)).consultaSegmento(inValue);
            codigoRespuesta = retVal.codigoRespuesta;
            mensajeRespuesta = retVal.mensajeRespuesta;
            segmento = retVal.segmento;
            nombreCliente = retVal.nombreCliente;
            mensaje1 = retVal.mensaje1;
            mensaje2 = retVal.mensaje2;
            mensaje3 = retVal.mensaje3;
            mensaje4 = retVal.mensaje4;
            return retVal.idTransaccion;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIAC.Segment.consultarSegmxTramaResponse Claro.SIACU.ProxyService.SIAC.Segment.ebsConsultaSegmentoPortType.consultarSegmxTrama(Claro.SIACU.ProxyService.SIAC.Segment.consultarSegmxTramaRequest request) {
            return base.Channel.consultarSegmxTrama(request);
        }
        
        public string consultarSegmxTrama(string tramaConsulta) {
            Claro.SIACU.ProxyService.SIAC.Segment.consultarSegmxTramaRequest inValue = new Claro.SIACU.ProxyService.SIAC.Segment.consultarSegmxTramaRequest();
            inValue.tramaConsulta = tramaConsulta;
            Claro.SIACU.ProxyService.SIAC.Segment.consultarSegmxTramaResponse retVal = ((Claro.SIACU.ProxyService.SIAC.Segment.ebsConsultaSegmentoPortType)(this)).consultarSegmxTrama(inValue);
            return retVal.tramaRespuesta;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmHistoricoResponse Claro.SIACU.ProxyService.SIAC.Segment.ebsConsultaSegmentoPortType.consultaSegmHistorico(Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmHistoricoRequest request) {
            return base.Channel.consultaSegmHistorico(request);
        }
        
        public string consultaSegmHistorico(Claro.SIACU.ProxyService.SIAC.Segment.AuditType audit, string tipoDocumento, string nroDocumento, out string codigoRespuesta, out string mensajeRespuesta, out Claro.SIACU.ProxyService.SIAC.Segment.ListaConsultaSegmHistorico[] listaConsultaSegmHistorico) {
            Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmHistoricoRequest inValue = new Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmHistoricoRequest();
            inValue.audit = audit;
            inValue.tipoDocumento = tipoDocumento;
            inValue.nroDocumento = nroDocumento;
            Claro.SIACU.ProxyService.SIAC.Segment.consultaSegmHistoricoResponse retVal = ((Claro.SIACU.ProxyService.SIAC.Segment.ebsConsultaSegmentoPortType)(this)).consultaSegmHistorico(inValue);
            codigoRespuesta = retVal.codigoRespuesta;
            mensajeRespuesta = retVal.mensajeRespuesta;
            listaConsultaSegmHistorico = retVal.listaConsultaSegmHistorico;
            return retVal.idTransaccion;
        }
    }
}