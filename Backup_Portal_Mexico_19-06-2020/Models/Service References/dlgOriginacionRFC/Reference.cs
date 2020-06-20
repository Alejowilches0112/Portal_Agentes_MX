﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models.dlgOriginacionRFC {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://mx.com.bayport.ws/exposition/originacion/cliente", ConfigurationName="dlgOriginacionRFC.ServicioClienteType")]
    public interface ServicioClienteType {
        
        // CODEGEN: El parámetro 'response' requiere información adicional de esquema que no se puede capturar con el modo de parámetros. El atributo específico es 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:buscarPorRfc", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ResponseBase))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="response")]
        Models.dlgOriginacionRFC.buscarPorRfcResponse buscarPorRfc(Models.dlgOriginacionRFC.buscarPorRfc request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:buscarPorRfc", ReplyAction="*")]
        System.Threading.Tasks.Task<Models.dlgOriginacionRFC.buscarPorRfcResponse> buscarPorRfcAsync(Models.dlgOriginacionRFC.buscarPorRfc request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/request/originacion/cliente")]
    public partial class RequestRfc : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string rfcField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string rfc {
            get {
                return this.rfcField;
            }
            set {
                this.rfcField = value;
                this.RaisePropertyChanged("rfc");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/types/originacion")]
    public partial class TypeDireccion : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codigoPostalField;
        
        private string entidadField;
        
        private string municipioDelegacionField;
        
        private string coloniaField;
        
        private string otraField;
        
        private string domicilioCalleField;
        
        private string numeroExteriorField;
        
        private string numeroInteriorField;
        
        private string entreCallesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string codigoPostal {
            get {
                return this.codigoPostalField;
            }
            set {
                this.codigoPostalField = value;
                this.RaisePropertyChanged("codigoPostal");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string entidad {
            get {
                return this.entidadField;
            }
            set {
                this.entidadField = value;
                this.RaisePropertyChanged("entidad");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string municipioDelegacion {
            get {
                return this.municipioDelegacionField;
            }
            set {
                this.municipioDelegacionField = value;
                this.RaisePropertyChanged("municipioDelegacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string colonia {
            get {
                return this.coloniaField;
            }
            set {
                this.coloniaField = value;
                this.RaisePropertyChanged("colonia");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string otra {
            get {
                return this.otraField;
            }
            set {
                this.otraField = value;
                this.RaisePropertyChanged("otra");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string domicilioCalle {
            get {
                return this.domicilioCalleField;
            }
            set {
                this.domicilioCalleField = value;
                this.RaisePropertyChanged("domicilioCalle");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string numeroExterior {
            get {
                return this.numeroExteriorField;
            }
            set {
                this.numeroExteriorField = value;
                this.RaisePropertyChanged("numeroExterior");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string numeroInterior {
            get {
                return this.numeroInteriorField;
            }
            set {
                this.numeroInteriorField = value;
                this.RaisePropertyChanged("numeroInterior");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        public string entreCalles {
            get {
                return this.entreCallesField;
            }
            set {
                this.entreCallesField = value;
                this.RaisePropertyChanged("entreCalles");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/types/originacion/cliente")]
    public partial class TypeDomicilio : object, System.ComponentModel.INotifyPropertyChanged {
        
        private TypeDireccion direccionField;
        
        private string paisResidenciaField;
        
        private int tiempoResidenciaField;
        
        private bool tiempoResidenciaFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public TypeDireccion direccion {
            get {
                return this.direccionField;
            }
            set {
                this.direccionField = value;
                this.RaisePropertyChanged("direccion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string paisResidencia {
            get {
                return this.paisResidenciaField;
            }
            set {
                this.paisResidenciaField = value;
                this.RaisePropertyChanged("paisResidencia");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public int tiempoResidencia {
            get {
                return this.tiempoResidenciaField;
            }
            set {
                this.tiempoResidenciaField = value;
                this.RaisePropertyChanged("tiempoResidencia");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool tiempoResidenciaSpecified {
            get {
                return this.tiempoResidenciaFieldSpecified;
            }
            set {
                this.tiempoResidenciaFieldSpecified = value;
                this.RaisePropertyChanged("tiempoResidenciaSpecified");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/types/originacion")]
    public partial class TypeTelefonosPersonal : object, System.ComponentModel.INotifyPropertyChanged {
        
        private long celularField;
        
        private string proveedorField;
        
        private long propioRecadosField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public long celular {
            get {
                return this.celularField;
            }
            set {
                this.celularField = value;
                this.RaisePropertyChanged("celular");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string proveedor {
            get {
                return this.proveedorField;
            }
            set {
                this.proveedorField = value;
                this.RaisePropertyChanged("proveedor");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public long propioRecados {
            get {
                return this.propioRecadosField;
            }
            set {
                this.propioRecadosField = value;
                this.RaisePropertyChanged("propioRecados");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/types/originacion/cliente")]
    public partial class TypeGenerales : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string rfcField;
        
        private string curpField;
        
        private string generoField;
        
        private string nacionalidadField;
        
        private string emailField;
        
        private TypeTelefonosPersonal telefonoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string rfc {
            get {
                return this.rfcField;
            }
            set {
                this.rfcField = value;
                this.RaisePropertyChanged("rfc");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string curp {
            get {
                return this.curpField;
            }
            set {
                this.curpField = value;
                this.RaisePropertyChanged("curp");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string genero {
            get {
                return this.generoField;
            }
            set {
                this.generoField = value;
                this.RaisePropertyChanged("genero");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string nacionalidad {
            get {
                return this.nacionalidadField;
            }
            set {
                this.nacionalidadField = value;
                this.RaisePropertyChanged("nacionalidad");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string email {
            get {
                return this.emailField;
            }
            set {
                this.emailField = value;
                this.RaisePropertyChanged("email");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public TypeTelefonosPersonal telefono {
            get {
                return this.telefonoField;
            }
            set {
                this.telefonoField = value;
                this.RaisePropertyChanged("telefono");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/types/originacion")]
    public partial class TypeNacimiento : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string fechaField;
        
        private string paisField;
        
        private string entidadField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string fecha {
            get {
                return this.fechaField;
            }
            set {
                this.fechaField = value;
                this.RaisePropertyChanged("fecha");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string pais {
            get {
                return this.paisField;
            }
            set {
                this.paisField = value;
                this.RaisePropertyChanged("pais");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string entidad {
            get {
                return this.entidadField;
            }
            set {
                this.entidadField = value;
                this.RaisePropertyChanged("entidad");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/types/originacion")]
    public partial class TypeAttributes2 : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string primeroField;
        
        private string segundoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string primero {
            get {
                return this.primeroField;
            }
            set {
                this.primeroField = value;
                this.RaisePropertyChanged("primero");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string segundo {
            get {
                return this.segundoField;
            }
            set {
                this.segundoField = value;
                this.RaisePropertyChanged("segundo");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/types/originacion/cliente")]
    public partial class TypeIdentificacion : object, System.ComponentModel.INotifyPropertyChanged {
        
        private TypeAttributes2 nombreField;
        
        private TypeAttributes2 apellidoField;
        
        private TypeNacimiento nacimientoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public TypeAttributes2 nombre {
            get {
                return this.nombreField;
            }
            set {
                this.nombreField = value;
                this.RaisePropertyChanged("nombre");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public TypeAttributes2 apellido {
            get {
                return this.apellidoField;
            }
            set {
                this.apellidoField = value;
                this.RaisePropertyChanged("apellido");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public TypeNacimiento nacimiento {
            get {
                return this.nacimientoField;
            }
            set {
                this.nacimientoField = value;
                this.RaisePropertyChanged("nacimiento");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/types/originacion")]
    public partial class TypeGlobalTicket : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string idField;
        
        private string inicioField;
        
        private string terminoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("id");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string inicio {
            get {
                return this.inicioField;
            }
            set {
                this.inicioField = value;
                this.RaisePropertyChanged("inicio");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string termino {
            get {
                return this.terminoField;
            }
            set {
                this.terminoField = value;
                this.RaisePropertyChanged("termino");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/types/originacion")]
    public partial class TypeGlobalError : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codigoField;
        
        private string codigoMensajeField;
        
        private string errorField;
        
        private string exceptionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string codigo {
            get {
                return this.codigoField;
            }
            set {
                this.codigoField = value;
                this.RaisePropertyChanged("codigo");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string codigoMensaje {
            get {
                return this.codigoMensajeField;
            }
            set {
                this.codigoMensajeField = value;
                this.RaisePropertyChanged("codigoMensaje");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string error {
            get {
                return this.errorField;
            }
            set {
                this.errorField = value;
                this.RaisePropertyChanged("error");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string exception {
            get {
                return this.exceptionField;
            }
            set {
                this.exceptionField = value;
                this.RaisePropertyChanged("exception");
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
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResponseGenerico))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ResponseBusqueda))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/response/originacion")]
    public partial class ResponseBase : object, System.ComponentModel.INotifyPropertyChanged {
        
        private TypeGlobalError errorField;
        
        private TypeGlobalTicket ticketField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public TypeGlobalError error {
            get {
                return this.errorField;
            }
            set {
                this.errorField = value;
                this.RaisePropertyChanged("error");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public TypeGlobalTicket ticket {
            get {
                return this.ticketField;
            }
            set {
                this.ticketField = value;
                this.RaisePropertyChanged("ticket");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/response/originacion")]
    public partial class ResponseGenerico : ResponseBase {
        
        private string estatusField;
        
        private string descripcionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string estatus {
            get {
                return this.estatusField;
            }
            set {
                this.estatusField = value;
                this.RaisePropertyChanged("estatus");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string descripcion {
            get {
                return this.descripcionField;
            }
            set {
                this.descripcionField = value;
                this.RaisePropertyChanged("descripcion");
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://mx.com.bayport.ws/messages/response/originacion/cliente")]
    public partial class ResponseBusqueda : ResponseBase {
        
        private TypeIdentificacion identificacionField;
        
        private TypeGenerales generalesField;
        
        private TypeDomicilio domicilioField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public TypeIdentificacion identificacion {
            get {
                return this.identificacionField;
            }
            set {
                this.identificacionField = value;
                this.RaisePropertyChanged("identificacion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public TypeGenerales generales {
            get {
                return this.generalesField;
            }
            set {
                this.generalesField = value;
                this.RaisePropertyChanged("generales");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public TypeDomicilio domicilio {
            get {
                return this.domicilioField;
            }
            set {
                this.domicilioField = value;
                this.RaisePropertyChanged("domicilio");
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="buscarPorRfc", WrapperNamespace="http://mx.com.bayport.ws/exposition/originacion/cliente", IsWrapped=true)]
    public partial class buscarPorRfc {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mx.com.bayport.ws/exposition/originacion/cliente", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Models.dlgOriginacionRFC.RequestRfc request;
        
        public buscarPorRfc() {
        }
        
        public buscarPorRfc(Models.dlgOriginacionRFC.RequestRfc request) {
            this.request = request;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="buscarPorRfcResponse", WrapperNamespace="http://mx.com.bayport.ws/exposition/originacion/cliente", IsWrapped=true)]
    public partial class buscarPorRfcResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://mx.com.bayport.ws/exposition/originacion/cliente", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Models.dlgOriginacionRFC.ResponseBusqueda response;
        
        public buscarPorRfcResponse() {
        }
        
        public buscarPorRfcResponse(Models.dlgOriginacionRFC.ResponseBusqueda response) {
            this.response = response;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServicioClienteTypeChannel : Models.dlgOriginacionRFC.ServicioClienteType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServicioClienteTypeClient : System.ServiceModel.ClientBase<Models.dlgOriginacionRFC.ServicioClienteType>, Models.dlgOriginacionRFC.ServicioClienteType {
        
        public ServicioClienteTypeClient() {
        }
        
        public ServicioClienteTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServicioClienteTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioClienteTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServicioClienteTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Models.dlgOriginacionRFC.buscarPorRfcResponse Models.dlgOriginacionRFC.ServicioClienteType.buscarPorRfc(Models.dlgOriginacionRFC.buscarPorRfc request) {
            return base.Channel.buscarPorRfc(request);
        }
        
        public Models.dlgOriginacionRFC.ResponseBusqueda buscarPorRfc(Models.dlgOriginacionRFC.RequestRfc request) {
            Models.dlgOriginacionRFC.buscarPorRfc inValue = new Models.dlgOriginacionRFC.buscarPorRfc();
            inValue.request = request;
            Models.dlgOriginacionRFC.buscarPorRfcResponse retVal = ((Models.dlgOriginacionRFC.ServicioClienteType)(this)).buscarPorRfc(inValue);
            return retVal.response;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Models.dlgOriginacionRFC.buscarPorRfcResponse> Models.dlgOriginacionRFC.ServicioClienteType.buscarPorRfcAsync(Models.dlgOriginacionRFC.buscarPorRfc request) {
            return base.Channel.buscarPorRfcAsync(request);
        }
        
        public System.Threading.Tasks.Task<Models.dlgOriginacionRFC.buscarPorRfcResponse> buscarPorRfcAsync(Models.dlgOriginacionRFC.RequestRfc request) {
            Models.dlgOriginacionRFC.buscarPorRfc inValue = new Models.dlgOriginacionRFC.buscarPorRfc();
            inValue.request = request;
            return ((Models.dlgOriginacionRFC.ServicioClienteType)(this)).buscarPorRfcAsync(inValue);
        }
    }
}