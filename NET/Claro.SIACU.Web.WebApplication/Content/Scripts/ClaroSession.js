var Session;
if (typeof window.parent != 'undefined' && window.parent != null && typeof window.parent.Session != 'undefined') {

if (!!window.parent.Session) {
    Session = window.parent.Session;
    }
} else if (typeof window.opener != 'undefined' && window.opener != null && (typeof window.opener.Session != 'undefined' && typeof window.opener.Session != 'unknown')) {

    if (!!window.opener.Session) {
        Session = window.opener.Session;
    }
} else {

    Session = function () { };

    Session.TRANSACCION = "";
    Session.CONTRATOID = "";
    Session.NOMBRES = "";
    Session.APELLIDOS = "";
    Session.CUSTOMER_ID = "";
    Session.CALLE_FAC = "";
    Session.CICLO_FACTURACION = "";
    Session.CUENTA = "";
    Session.DEPARTAMENTO = "";
    Session.DISTRITO = "";
    Session.PROVINCIA = "";
    Session.DEPARTEMENTO_FAC = "";
    Session.DISTRITO_FAC = "";
    Session.DNI_RUC = "";
    Session.MODALIDAD = "";
    Session.NOMBRE_COMPLETO = "";
    Session.NRO_DOC = "";
    Session.OBJID_CONTACTO = "";
    Session.PROVINCIA_FAC = "";
    Session.RAZON_SOCIAL = "";
    Session.TIPO_CLIENTE = "";
    Session.TIPO_DOC = "";
    Session.DIRECCION_DESPACHO = "";
    Session.REPRESENTANTE_LEGAL = "";
    Session.FECACTIVACION = "";
    Session.FECHADESACTIVACION = "";
    Session.TIPOSERVICIO = "";
    Session.ACTDESBOLSA = "";
    Session.EMAIL = "";
    Session.FLAGTFI = "";
    Session.S_NOMBRES = "";
    Session.S_APELLIDOS = "";
    Session.TELEF_REFERENCIA = "";
    Session.CODIGO_PLANO_INST = "";
    Session.DOMICILIO = "";

    Session.btnDynamic = {};
    Session.OPTIONREDIRECT = "";
    Session.ORIGINAPP = "";
    Session.SEARCHTYPE = "";
    Session.IDINSTANT = {};
    Session.DESCRIPTIONINSTANT = {};
    Session.DATACUSTOMER = {};
    Session.CUSTOMERPRODUCT = {};
    Session.DATASERVICE = {};
    Session.HistoricalTriationRFA = {};
    Session.DetalleHistoricalTriationRFA = {};
    Session.MENU = {};
    Session.ORIGINTYPE = {};
    Session.CUSTOMEROLD = {}
    Session.MESSAGEPREPAID = {}
    Session.TELEPHONE = ""
    Session.SHOWPOPUPCUSTOMEROLD = {}
    Session.SHOWDATACUSTOMERPREPAID = {}
    Session.IDSESSION = "";
    Session.TypeMenuInst = "";
    Session.ArchivoIntantanea = "";
    Session.NewArchivoIntantanea = "";
    Session.ListMassiveInstantFail = [];
    Session.ListMassiveInstantCorrect = [];

    /*INI - Sesiones en duro de Redireccionamiento*/
    Session.PAGEACCESS = "";
    Session.CODEPROFILE = "";
    Session.USERQUERY = "0";
    Session.PORTABILITY = "";
    Session.PORTABILITYRESPUESTA = "";
    Session.code = "";
    
    Session.LOGIN = "";
    Session.FULLNAME = "";
    Session.SERVISBAMBAF = "0";
    Session.USERLOGIN = "";
    Session.SERVDTH_MOVIL = "0";
    Session.LOGINM = "";
    Session.BUSQINSTANT = "";
    /*FIN - Sesiones en duro de Redireccionamiento*/

    /*INI - Parametros en duro de Redireccionamiento*/
    Session.USERACCESS = { optionPermissions: '' };
    Session.ACCION = "";
    Session.ACTUALIZADATOS = "";
    Session.ALTURAVENTANA = "460";
    Session.ANCHOVENTANA = "980";
    Session.CAE = "";
    Session.CASOINTERACCIONID = "";
    Session.CBOLOCALIDAD = "";
    Session.CLASEID = "1095";
    Session.CO_ID = "";
    Session.CODCLIENTE = "";
    Session.CODUSR = "";
    Session.CODUSER = Session.USERACCESS.userId;
    Session.CONTACTOID = "";
    Session.CONTRATOID = "";
    Session.CP = "";
    Session.CRITBUSQUEDA = "1";
    Session.CU = "";
    Session.CUSTOMERID = "";
    Session.CUSTUMERID = "";
    Session.DATOSCLIENTE = "";
    Session.DETALLELLAMADAS = "";
    Session.DTH = "";
    Session.ESRETENCION = "";
    Session.ESTADOFORM = "N";
    Session.FLAG_TFI = "";
    Session.FLAGCINTILLO = "";
    Session.FLAGCLIENTEANTIGUO = "";
    Session.FLAGESLINEAINACTIVA = "";
    Session.FLAGRCEC = "";
    Session.FLAGTFI = "";
    Session.ID = "";
    Session.IDCASO = "";
    Session.INVOICENUMBER = "";
    Session.LLAMADODESDEINDEX = "S";
    Session.MIGRACION = "";
    Session.MODE = "";
    Session.NRODOC = "";
    Session.NRODOCUMENTO = "";
    Session.NROSOTEXTERNO = "";
    Session.NROTELEFONO = "";
    Session.OP = "1";
    Session.OPCION = "";
    Session.ORIGEN = "";
    Session.PERFILES = "";
    Session.PSALDO = "";
    Session.PSTRCASOPADREID = "";
    Session.PSTRCODCLIENTE = "";
    Session.PSTRCODCONTRATOID = "";
    Session.PSTRCODCUSTOMERID = "";
    Session.PSTRINTERACCIONID = "";
    Session.PSTRNOTA = "";
    Session.PSTRTELREFER = "R";
    Session.PSTRTIPO = "P";
    Session.PSTRTRANSACCIONORIGEN = "";
    Session.SERVICIO = "";
    Session.SOLOCONSULTA = "";
    Session.STRCBOTIPOBUSQUEDAVAL = "";
    Session.STRTELEFONOBUS = "";
    Session.SUBCLASEID = "109503";
    Session.TEL = "";
    Session.TELEFONOREF = "";
    Session.TIPOAPP = "";
    Session.TIPOBLOQUEO = "";
    Session.TIPOCASOINTERACCION = "2";
    Session.TIPOCONTACTO = "P";
    Session.TIPOCUSTOMERID = "";
    Session.TIPODOC = "";
    Session.TIPOFORM = "";
    Session.TIPOID = "10";
    Session.TIPOLTE = "";
    Session.TIPOSERVI = "";
    Session.TIPOSERVICIO = "";
    Session.TIPOTELEFONO = "2";
    Session.TIPOTX = "";
    Session.TO = "";
    Session.TRANSACCIONDOL = "";
    Session.TXTEXTERNO = "";
    Session.TXTTELEFONO = "";
    Session.PlanRate = "";
    Session.TIPO = "";
    Session.NUMLOTE = "";
    Session.CO = "";
    
    Session.STRIPNUMBERTRI = '';
    
    Session.CU = Session.USERACCESS.userId;
    /*FIN - Parametros en duro de Redireccionamiento*/
    Session.IsPostPaid = false;
    Session.CODERESPONSEPREPAIDSERVICE = "";

    Session.ORIGINTYPESERVICE = "";
    Session.CONTRACTIDSERVICE = "";
    Session.SEGMENTOCLIENTE = "";

    /*INICIO - Sesiones de Producto OLO*/
    Session.POINTSALE = null;
    Session.PLANFREE = 1;
    Session.templateId = "";
    Session.CONSUMPTIONPRINT = null;

    Session.COVID_19 = "";
    Session.BloqSES = "";

    Session.BloqClaro = "";
    Session.MensajeClaro = "";
}

var vTelefoniaMovil = "M";
function SetDataRedirect(Session) {
   
    Session.SUREDIRECT = Session.ORIGINAPP == "POSTPAGO" ? "POSTPAID" : Session.ORIGINAPP == "PREPAGO" ? "PREPAID" : Session.ORIGINTYPE;
    Session.TIPOAPP = "SGA";
    Session.INSTANTANEAID = Session.IDINSTANT;
    Session.TELEFONO = (Session.TELEPHONE != null && (Session.ORIGINTYPE == 'POSTPAID' || Session.ORIGINTYPE == 'DTH') ? Session.TELEPHONE : (Session.TELEF_REFERENCIA != '' ? Session.TELEF_REFERENCIA : Session.DATACUSTOMER.PhoneReference));
    if (Session.ORIGINTYPE == 'PREPAID') Session.TELEFONO = Session.TELEPHONE;
    Session.TELEFONOSAR = Session.TELEPHONE;
    Session.PSTRCUSTOMERID = Session.DATACUSTOMER.CustomerID;
    Session.PSTRCUENTA = Session.DATACUSTOMER.Account;
    Session.CONTACTCODE = Session.DATACUSTOMER.ContactCode;
    if (Session.DATACUSTOMER.Segment2 == null) {
        Session.DATACUSTOMER.Segment2 = Session.DATACUSTOMER.Segment;
    }


      
    switch (Session.SEARCHTYPE) {
        case '1': Session.TIPOBUSQ = "3";
            break;
        case '2': Session.TIPOBUSQ = "4";
            break;
        case '3': Session.TIPOBUSQ = "2";
            break;
        case '4': Session.TIPOBUSQ = "7";
            break;
        default: Session.TIPOBUSQ = "";
    }
    if (Session.ORIGINTYPE == "POSTPAID") {
        Session.CUENTA = Session.DATACUSTOMER.Account;
        Session.CONTRATOID = Session.DATACUSTOMER.ContractID;
        Session.CUSTOMERID = Session.DATACUSTOMER.CustomerID;
        Session.OBJID = Session.DATACUSTOMER.IdContactObject;
        if (Session.DATACUSTOMER.StateAgreement == "Activo") {
            Session.FLAGCLIENTEANTIGUO = "";
        } else {
            Session.FLAGCLIENTEANTIGUO = "NCSIACPOS";
            Session.TIPOAPP = "NOCLIENTE";

        }
    } else if (Session.ORIGINTYPE == "PREPAID") {
        Session.CUENTA = Session.DATACUSTOMER.SiteId;
        Session.OBJID = Session.DATACUSTOMER.CustomerCode;
        if (Session.DATACUSTOMER.State == "Activo") {
            Session.FLAGCLIENTEANTIGUO = "";
        } else {
            Session.FLAGCLIENTEANTIGUO = "X";
        }

    } else if (Session.ORIGINTYPE == "HFC") {
        Session.CO_ID = Session.DATACUSTOMER.ContractID;
    } 

    Session.ACCESSBLACKLIST = "1";
   };

