using Claro.SIACU.Entity.Dashboard;
using Claro.SIACU.Entity.Dashboard.Board.GetCustomerInfo;
using Claro.SIACU.Entity.Dashboard.Board.GetCustomerName;
using Claro.SIACU.Entity.File.GetFileDefaultImpersonation;
using Claro.SIACU.Entity.Dashboard.Board.GetFileInvoice;
using Claro.SIACU.Entity.Dashboard.Board.GetInvoiceFtp;
using Claro.SIACU.Entity.Dashboard.Board.GetPostpaidLines;
using Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts;
using Claro.SIACU.Entity.Dashboard.Board.GetPrepaidProducts;
using Claro.SIACU.Entity.Dashboard.Board.GetReceiptNumber;
using Claro.SIACU.Entity.Dashboard.Board.GetTypeMIME;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer;
using System;
using System.Linq;
using System.Collections.Generic;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.ProxyService;
using SECURITY = Claro.SIACU.ProxyService.SecurityService;
using Claro.SIACU.Data.Configuration;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetTypeProduct.Common;
using Claro.SIACU.Entity.Coliving.getConsultaLineaCuenta;
using Claro.SIACU.Business.Coliving;
using Claro.SIACU.Entity.Dashboard.Postpaid;
namespace Claro.SIACU.Business.Dashboard
{
    public static class Dashboard
    {
        /// <summary>
        /// Método para obtener la información del cliente.
        /// </summary>
        /// <param name="objCustomerInfoRequest">Contiene tipo y valor de búsqueda.</param>
        /// <returns>Devuelve objeto CustomerInfoResponse con la información del cliente</returns>
        public static CustomerInfoResponse GetCustomerInfo(CustomerInfoRequest objCustomerInfoRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<CustomerInfoResponse>(objCustomerInfoRequest.Audit.Session, objCustomerInfoRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetCustomerInfo(objCustomerInfoRequest.Audit, objCustomerInfoRequest.SearchType, objCustomerInfoRequest.SearchValue); });
        }

        /// <summary>
        /// Método para obtener el listado de productos postpago.
        /// </summary>
        /// <param name="objPostpaidProductsRequest">Contiene tipo y número de documento de identidad.</param>
        /// <returns>Devuelve objeto PostpaidProductsResponse con el listado e información de los productos postpago.</returns>
        public static PostpaidProductsResponse GetPostpaidProducts(PostpaidProductsRequest objPostpaidProductsRequest)
        {



            PostpaidProductsResponse objPostpaidProductsResponse = Claro.Web.Logging.ExecuteMethod<PostpaidProductsResponse>(objPostpaidProductsRequest.Audit.Session, objPostpaidProductsRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPostpaidProducts(objPostpaidProductsRequest.Audit.Session, objPostpaidProductsRequest.Audit.UserName, objPostpaidProductsRequest.Audit.IPAddress, objPostpaidProductsRequest.Audit.ApplicationName, objPostpaidProductsRequest.Audit.Transaction, objPostpaidProductsRequest.DocumentType, objPostpaidProductsRequest.DocumentIdentity); });

            List<Claro.SIACU.Entity.ListItem> listItem;
            string strTelefoniaMovil = "";
            string str3PlayAlambrico = "";
            string str2PlayAlambrico = "";
            string str1PlayAlambrico = "";
            string str3PlayInalambrico = "";
            string str2PlayInalambrico = "";
            string str1PlayInalambrico = "";
            string strTelefoniaFijaInalambrica = "";
            string strTinternetMovil = "";
            string strTvSatelital = "";
            string strTelefoniaPublicaInalambrica = "";
            try
            {
                strTelefoniaMovil = KEY.AppSettings("TELEFONIA_MOVIL");
                str3PlayAlambrico = KEY.AppSettings("3PLAY_ALAMBRICO");
                str2PlayAlambrico = KEY.AppSettings("2PLAY_ALAMBRICO");
                str1PlayAlambrico = KEY.AppSettings("1PLAY_ALAMBRICO");
                str3PlayInalambrico = KEY.AppSettings("3PLAY_INALAMBRICO");
                str2PlayInalambrico = KEY.AppSettings("2PLAY_INALAMBRICO");
                str1PlayInalambrico = KEY.AppSettings("1PLAY_INALAMBRICO");
                strTelefoniaFijaInalambrica = KEY.AppSettings("TELEFONIA_FIJA_INALAMBRICA");
                strTinternetMovil = KEY.AppSettings("TINTERNET_MOVIL");
                strTvSatelital = KEY.AppSettings("TV_SATELITAL");
                strTelefoniaPublicaInalambrica = KEY.AppSettings("TELEFONIA_PUBLICA_INALAMBRICA");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPostpaidProductsRequest.Audit.Session, objPostpaidProductsRequest.Audit.Transaction, ex.Message);
            }

            var a = strTelefoniaMovil.ToString().Split('|');
            Claro.SIACU.Entity.ListItem item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem = new List<Entity.ListItem>();
            listItem.Add(item);
            a = str3PlayAlambrico.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem.Add(item);
            a = str2PlayAlambrico.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem.Add(item);
            a = str1PlayAlambrico.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem.Add(item);
            a = str3PlayInalambrico.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem.Add(item);
            a = str2PlayInalambrico.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem.Add(item);
            a = str1PlayInalambrico.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem.Add(item);
            a = strTelefoniaFijaInalambrica.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem.Add(item);
            a = strTinternetMovil.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem.Add(item);
            a = strTvSatelital.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem.Add(item);
            a = strTelefoniaPublicaInalambrica.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            item.number = int.Parse(a[0]);
            item.Description = a[2];
            listItem.Add(item);

            List<Claro.SIACU.Entity.Dashboard.Postpaid.Product> listProduct = new List<Entity.Dashboard.Postpaid.Product>();

            foreach (var element in objPostpaidProductsResponse.ListProduct)
            {
                bool val = false;
                foreach (var item1 in listItem)
                {
                    if (element.Producto != null && element.Producto.ToUpper().Contains(item1.Description))
                    {
                        Claro.SIACU.Entity.Dashboard.Postpaid.Product product = element;
                        product.id = item1.number;
                        listProduct.Add(product);
                        val = true;
                        break;
                    }
                }
                if (!val)
                {
                    Claro.SIACU.Entity.Dashboard.Postpaid.Product product = element;
                    product.id = Claro.Constants.NumberTwenty;
                    listProduct.Add(product);
                }
            }

            objPostpaidProductsResponse.ListProduct = listProduct;
            return objPostpaidProductsResponse;
        }

        /// <summary>
        /// Método para obtener el listado de líneas postpago.
        /// </summary>
        /// <param name="GetPostpaidLinesRequest">Contiene código y tipo de producto, id de customer, id de plan, origen y estado.</param>
        /// <returns>Devuelve objeto PostpaidLinesResponse con el listado e información de líneas postpago.</returns>
        public static PostpaidLinesResponse GetPostpaidLines(PostpaidLinesRequest GetPostpaidLinesRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<PostpaidLinesResponse>(GetPostpaidLinesRequest.Audit.Session, GetPostpaidLinesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPostpaidLines(GetPostpaidLinesRequest.Audit.Session, GetPostpaidLinesRequest.Audit.UserName, GetPostpaidLinesRequest.Audit.IPAddress, GetPostpaidLinesRequest.Audit.ApplicationName, GetPostpaidLinesRequest.Audit.Transaction, GetPostpaidLinesRequest.CustomerId, GetPostpaidLinesRequest.State, GetPostpaidLinesRequest.CodeProduct, GetPostpaidLinesRequest.Origin, GetPostpaidLinesRequest.IdPlan, GetPostpaidLinesRequest.ProductType); });
        }

        /// <summary>
        /// Método para obtener el listado de productos prepago.
        /// </summary>
        /// <param name="GetPrepaidProductsRequest">Contiene tipo y número de documento de identidad.</param>
        /// <returns>Devuelve objeto PrepaidProductsResponse con el listado e información de los productos prepago.</returns>
        public static PrepaidProductsResponse GetPrepaidProducts(PrepaidProductsRequest GetPrepaidProductsRequest)
        {
            PrepaidProductsResponse objPrepaidProductsResponse = Claro.Web.Logging.ExecuteMethod<PrepaidProductsResponse>(GetPrepaidProductsRequest.Audit.Session, GetPrepaidProductsRequest.Audit.Transaction, () => { return Data.Dashboard.Prepaid.GetPrepaidProducts(GetPrepaidProductsRequest.Audit.Session, GetPrepaidProductsRequest.Audit.UserName, GetPrepaidProductsRequest.Audit.IPAddress, GetPrepaidProductsRequest.Audit.ApplicationName, GetPrepaidProductsRequest.Audit.Transaction, GetPrepaidProductsRequest.DocumentIdentity, GetPrepaidProductsRequest.DocumentType, GetPrepaidProductsRequest.State); });

            List<Claro.SIACU.Entity.ListItem> listItem;

            string strTelefoniaMovilPre = "";
            string strTelefoniaFijaInalambricaPre = "";
            try
            {
                strTelefoniaMovilPre = KEY.AppSettings("TELEFONIA_MOVIL_PRE");
                strTelefoniaFijaInalambricaPre = KEY.AppSettings("TELEFONIA_FIJA_INALAMBRICA_PRE");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(GetPrepaidProductsRequest.Audit.Session, GetPrepaidProductsRequest.Audit.Transaction, ex.Message);
            }
            var a = strTelefoniaMovilPre.Split('|');
            Claro.SIACU.Entity.ListItem item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem = new List<Entity.ListItem>();
            listItem.Add(item);
            a = strTelefoniaFijaInalambricaPre.Split('|');
            item = new Claro.SIACU.Entity.ListItem() { number = int.Parse(a[0]), Description = a[2] };
            listItem.Add(item);

            List<Claro.SIACU.Entity.Dashboard.Prepaid.Product> listProduct = new List<Entity.Dashboard.Prepaid.Product>();

            foreach (var element in objPrepaidProductsResponse.ListProduct)
            {
                bool val = false;
                foreach (var item1 in listItem)
                {
                    if (element.TipoProducto.ToUpper().Contains(item1.Description))
                    {
                        Claro.SIACU.Entity.Dashboard.Prepaid.Product product = element;
                        product.Id = item1.number;
                        listProduct.Add(product);
                        val = true;
                        break;
                    }
                }
                if (!val)
                {
                    Claro.SIACU.Entity.Dashboard.Prepaid.Product product = element;
                    product.Id = Claro.Constants.NumberTen;
                    listProduct.Add(product);
                }
            }

            return objPrepaidProductsResponse;
        }
        public static string ValidationMsjPrepaid(string strCodeResponse, Entity.Dashboard.Prepaid.Service oService, Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerRequest oCustomerRequest)
        {
            string msj = string.Empty;
            Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneResponse oResponse = Claro.SIACU.Business.Dashboard.Prepaid.GetValidateTelephone(new Entity.Dashboard.Prepaid.GetValidateTelephone.ValidateTelephoneRequest()
            {
                Audit = oCustomerRequest.Audit,
                CodigoResponse = strCodeResponse,
                ProviderID = oService.ProviderID,
                TFI = oService.EsTFI,
                Telephone = oCustomerRequest.ValueSearch,
                CustomerCode = ""
            });
            msj = oResponse.Cadena.Split('|')[0];
            return msj;
        }
        private static List<Option> ReturnListOption(List<SECURITY.Option> lstOption)
        {
            List<Option> lstResult = null;
            if (lstOption != null)
            {
                lstResult = new List<Option>();
                foreach (SECURITY.Option item in lstOption)
                {
                    Option it = new Option()
                    {
                        id = item.id,
                        code = item.code,
                        isDefault = item.isDefault,
                        isExternal = item.isExternal,
                        items = ReturnListOption(item.items),
                        parentId = item.parentId,
                        name = item.name,
                        optionType = item.optionType,
                        order = item.order,
                        url = item.url
                    };
                    lstResult.Add(it);
                }
            }

            return lstResult;
        }
        private static Claro.SIACU.Entity.Dashboard.OptionsResponse ConvertOptions(SECURITY.OptionsResponse options)
        {
            Claro.SIACU.Entity.Dashboard.OptionsResponse result = null;
            if (options != null)
            {
                result = new OptionsResponse();
                if (options.menu != null)
                {
                    result.menu = ReturnListOption(options.menu);
                }
                if (options.options != null)
                {
                    result.options = ReturnListOption(options.options);
                }
                if (options.toolbar != null)
                {
                    result.toolbar = ReturnListOption(options.toolbar);
                }

            }

            return result;

        }
        public static string getTypeSearch(string strTypeSearch)
        {
            switch (strTypeSearch)
            {

                case Claro.Constants.NumberTwoString:
                    strTypeSearch = Claro.Constants.NumberFourString; break;

                case Claro.Constants.NumberFourString:
                    strTypeSearch = Claro.Constants.NumberTwoString; break;

            }
            return strTypeSearch;
        }
        public static string IsTobeOrAsis(string strTypeSearch, string strValueSearch, Claro.Entity.AuditRequest Audit, ref ConsultaLineaResponse response)
        {
            ConsultaLineaRequest consultaLineaRequest = new ConsultaLineaRequest()
            {
                Audit = Audit,
                Type = getTypeSearch(strTypeSearch),
                Value = strValueSearch
            };
            response = SearchCustomer.ConsultarLineaCuenta(consultaLineaRequest);
            if (response.ResponseDescription.Equals(Claro.SIACU.Constants.Yes, StringComparison.InvariantCultureIgnoreCase)) return Claro.SIACU.Constants.TOBE;
            return Claro.SIACU.Constants.ASIS;
        }
        /// <summary>
        /// Método para obtener datos del cliente.
        /// </summary>
        /// <param name="objCustomerRequest">Contiene tipo y valor de la búsqueda.</param>
        /// <returns>Devuelve objeto CustomerResponse con la información del cliente.</returns>
        public static Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse GetCustomer(Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerRequest objCustomerRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse objCustomerResponse = new Entity.Dashboard.Board.GetCustomer.CustomerResponse();

            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat outOptional = null;

            string strNameApplication = "";
            string strApplication = "";
            string strNumeroCuenta = "";
            string strTelefono = "";
            string strOutTelefono = "";
            string strplataforma = Claro.SIACU.Constants.ASIS;
            string strNumRecibo = "";
            string strICCID = "";
            string strCustomerID = "";
            string strTecnologia = string.Empty; //INICIATIVA-616


            string strTypeSearch = objCustomerRequest.TypeSearch;
            string strValueSearch = objCustomerRequest.ValueSearch;
            string strLineaPrepago = objCustomerRequest.ValueSearch;
            string strTypeSearchResult = "";
            string strFlagResultado = "";
            string strMensajeResultado = "";

            string strTipoGeneralMOVIL = "";
            string strTipoGeneralHFC = "";
            string strTipoGeneralLTE = "";
            string strTipoGeneralTFI = "";
            string strTipoGeneralDTH = "";
            string strTipoGeneralTPI = "";

            string strTipoGeneralFIJA = string.Empty;
            string CODE_PRODUCT_FIXE = string.Empty;
            string OPTIONS_PRODUCT_FIXE = string.Empty;

            string strNombreAplicacion = "";
            //mg13
            string strTipoGeneralInternet = "";



            try
            {
                strTipoGeneralMOVIL = KEY.AppSettings("strTipoGeneralMOVIL");
                strTipoGeneralHFC = KEY.AppSettings("strTipoGeneralHFC");
                strTipoGeneralLTE = KEY.AppSettings("strTipoGeneralLTE");
                strTipoGeneralTFI = KEY.AppSettings("strTipoGeneralTFI");
                strTipoGeneralDTH = KEY.AppSettings("strTipoGeneralDTH");
                strTipoGeneralTPI = KEY.AppSettings("strTipoGeneralTPI");

                strTipoGeneralInternet = KEY.AppSettings("strTipoGeneralInternet");
                strNombreAplicacion = KEY.AppSettings("NombreAplicacion");
                strTipoGeneralFIJA = KEY.AppSettings("strTipoGeneralFIJA");
                CODE_PRODUCT_FIXE = strTipoGeneralFIJA.Split('|')[1].ToString();
                OPTIONS_PRODUCT_FIXE = strTipoGeneralFIJA.Split('|')[2].ToString();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, ex.Message);
            }
            if (strTypeSearch.Equals(Claro.Constants.NumberOneString) || strTypeSearch.Equals(Claro.Constants.NumberTwoString) || strTypeSearch.Equals(Claro.Constants.NumberThreeString) || strTypeSearch.Equals(Claro.Constants.NumberFourString))
            {


                try
                {
                    ConsultaLineaResponse response = null;
                    strplataforma = IsTobeOrAsis(strTypeSearch, strValueSearch, objCustomerRequest.Audit, ref response);
                    if (strplataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase) && response.itm != null)
                    {
                        objCustomerResponse.itm = response.itm;
                        strTecnologia = response.itm.origenRegistro; //INICIATIVA-616
                    }

                    if (strTypeSearch.Equals(Claro.Constants.NumberOneString) && response.ResponseValue.Equals("1") && response.itm.estado.Equals("d"))
                    {
                        objCustomerRequest.IsPrepaid = true;
                    }


                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
                    throw;
                }
            }



            if (objCustomerRequest.ApplicationType.Equals("") || objCustomerRequest.ApplicationType.Equals(Claro.SIACU.Constants.PostpaidMajuscule))
            {

                strTypeSearchResult = (objCustomerRequest.IsPrepaid ? "" : GetApplicationType(objCustomerRequest.Audit, ref strTypeSearch, ref strValueSearch, ref strFlagResultado, ref strMensajeResultado, ref strplataforma, out outOptional));
                objCustomerRequest.TypeSearch = strTypeSearch;
                //INICIATIVA-616
                if (strplataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase) && objCustomerResponse.itm.origenRegistro.Equals(KEY.AppSettings("strOrigenRegistroTOBE")) && objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberOneString))
                {
                    //CAMBIO NUEVO SERVICIO
                    strValueSearch = KEY.AppSettings("strprefijoTelefoniaTOBE") + GetNumberActiveTOBEFIJA(objCustomerRequest.Audit, objCustomerResponse.itm.identificacion);
                }
                //INICIATIVA-616

                objCustomerRequest.ValueSearch = strValueSearch;
            }
            else
            {
                if (objCustomerRequest.ApplicationType.Equals(Claro.SIACU.Constants.HFC))
                {
                    //INICIATIVA-616
                    if (strplataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase) && objCustomerResponse.itm.origenRegistro.Equals(KEY.AppSettings("strOrigenRegistroTOBE")) && objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberThreeString))
                    {
                        //strValueSearch = KEY.AppSettings("strprefijoTelefoniaTOBE") + objCustomerResponse.itm.identificacion;
                        strValueSearch = KEY.AppSettings("strprefijoTelefoniaTOBE") + GetNumberActiveTOBEFIJA(objCustomerRequest.Audit, objCustomerResponse.itm.identificacion);
                    }
                    //INICIATIVA-616
                    strTypeSearchResult = strTipoGeneralHFC;
                }
                else if (objCustomerRequest.ApplicationType.Equals(Claro.SIACU.Constants.LTE))
                {
                    strTypeSearchResult = strTipoGeneralLTE;
                }
            }

            Entity.Dashboard.Prepaid.Customer oCustomerPre = new Entity.Dashboard.Prepaid.Customer();
            Entity.Dashboard.Postpaid.Customer objCustomerPost = new Entity.Dashboard.Postpaid.Customer();
            Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("TypeSearch: {0} , ValueSearch:{1}, strTypeSearchResult:{2}", objCustomerRequest.TypeSearch, objCustomerRequest.ValueSearch, strTypeSearchResult));
            if (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberOneString))
            {   
                strTelefono = strValueSearch;                
                
                //mg13
                if ((strTypeSearchResult.Equals(strTipoGeneralMOVIL)) || (strTypeSearchResult.Equals(strTipoGeneralTFI)) || (strTypeSearchResult.Equals(strTipoGeneralDTH)) || (strTypeSearchResult.Equals(strTipoGeneralTPI)))
                {
                    strNameApplication = strNombreAplicacion;
                    strApplication = Claro.SIACU.Constants.PostpaidMajuscule;
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));

                    objCustomerResponse.ApplicationType = strApplication;
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                           new CustomerRequest()
                           {
                               TypeSearch = objCustomerRequest.TypeSearch,
                               AccountCustomer = strNumeroCuenta,
                               NumCellphone = strTelefono,
                               Application = strApplication,
                               ApplicationName = strNameApplication,
                               ProductType = strTypeSearchResult,
                               Audit = objCustomerRequest.Audit,
                               Plataform = strplataforma,
                               outOptional = outOptional
                           }).ObjCustomer;

                        if (objCustomerPost != null)
                        {
                            objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                            objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Postpaid;
                            try
                            {
                                if (strTypeSearchResult.Equals(strTipoGeneralMOVIL))
                                {
                                    oCustomerPre = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.Customer>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Prepaid.GetPreviousCustomer(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, strTelefono, Claro.Constants.FormatFourNine + objCustomerPost.CUSTOMER_ID, "", Claro.Constants.NumberOneString); });
                                }
                                else
                                {
                                    oCustomerPre = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Prepaid.Customer>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Prepaid.GetPreviousCustomer(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, Claro.Constants.FormatTripleZero + strTelefono, Claro.Constants.FormatFourNine + objCustomerPost.CUSTOMER_ID, "", Claro.Constants.NumberOneString); });
                                }
                            }
                            catch
                            {
                                oCustomerPre = null;
                            }

                            if (oCustomerPre != null)
                            {
                                objCustomerPost.OBJID_CONTACTO = oCustomerPre.OBJID_CONTACTO;
                            }
                            bool resultPostPago = false;
                            bool resultPrepaId = false;
                            if (objCustomerRequest.UserId != 0)
                            {
                                objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Prepaid.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                                if (objCustomerResponse.objOptions != null) resultPrepaId = true;
                            }



                            Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("ESTADO_LINEA: {0} , NotEvalState: {1}, FlagSearchType: {2}", objCustomerPost.ESTADO_LINEA, objCustomerRequest.NotEvalState.ToString(), objCustomerRequest.FlagSearchType.ToString()));
                            if ((objCustomerPost.ESTADO_LINEA == "Desactivo" && objCustomerRequest.NotEvalState && resultPrepaId) || (objCustomerPost.ESTADO_LINEA == "Desactivo" && objCustomerRequest.FlagSearchType && resultPrepaId))
                            {

                                Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest objGetPreviousCustomerRequest = new Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest
                                {
                                    Telephone = strTelefono.Trim(),
                                    Account = "",
                                    ContactId = "",
                                    FlagRegistry = Claro.Constants.NumberOneString,
                                    ApplicationName = strNameApplication,
                                    Audit = objCustomerRequest.Audit
                                };
                                Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse objPreviousCustomerResponse = Prepaid.GetPreviousCustomer(objGetPreviousCustomerRequest);
                                oCustomerPre = objPreviousCustomerResponse.objCustomer;
                                if (oCustomerPre != null)
                                {
                                    oCustomerPre.ApplicationType = Claro.SIACU.ApplicationType.Prepaid;
                                    Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("Parametros de Entrada ValidationMsjPrepaid -- CodeResponse: {0} ,ProviderId:{1} , EsTFI:{2},ValueSearch:{3} ", objPreviousCustomerResponse.CodeResponse, !string.IsNullOrEmpty(oCustomerPre.oService.ProviderID) ? oCustomerPre.oService.ProviderID.ToString() : "", oCustomerPre.oService.EsTFI.ToString(), objCustomerRequest.ValueSearch.ToString()));
                                    objCustomerResponse.MsjValidation = ValidationMsjPrepaid((objPreviousCustomerResponse.CodeResponse), oCustomerPre.oService, objCustomerRequest);

                                }
                                try
                                {
                                    oCustomerPre.oService.EstadoLinea = Helper.GetValueXML(!string.IsNullOrEmpty(oCustomerPre.oService.StatusLinea) ? oCustomerPre.oService.StatusLinea.Trim() : "", Claro.SIACU.Constants.ListStatusLine);
                                }
                                catch (Exception ex)
                                {
                                    oCustomerPre.oService.EstadoLinea = "";
                                    Claro.Web.Logging.Error(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("Error al obtener estado de Linea Prepago Detalle: {0}", ex.Message));
                                }

                                Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("EstadoLinea: {0} ,  oCustomerPre.oService: {1}, oCustomerPre.oService.EstadoLinea: {2}", objCustomerPost.ESTADO_LINEA, oCustomerPre.oService == null ? "true" : "false", oCustomerPre.oService != null && !string.IsNullOrWhiteSpace(oCustomerPre.oService.EstadoLinea) ? oCustomerPre.oService.EstadoLinea.ToString() : "is null"));
                                if (oCustomerPre != null && oCustomerPre.oService != null && !string.IsNullOrWhiteSpace(oCustomerPre.oService.EstadoLinea) && oCustomerPre.oService.EstadoLinea != "Desactivo")
                                {
                                    Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("EstadoLinea: {0} ,  ", oCustomerPre.oService.StatusLinea));
                                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Prepaid.ToString().ToUpper();
                                    objCustomerResponse.InterfaceCustomer = oCustomerPre;
                                    objCustomerResponse.CodeResponse = (string.IsNullOrEmpty(objPreviousCustomerResponse.CodeResponse) ? "" : objPreviousCustomerResponse.CodeResponse);
                                    Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("EstadoLinea: {0} ,MsjValidation:{1}  ", oCustomerPre.oService.EstadoLinea, objCustomerResponse.MsjValidation));
                                }
                                else
                                {
                                    if (strTypeSearchResult.Equals(strTipoGeneralDTH))
                                    {
                                        objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Dth.ToString().ToUpper();
                                    }
                                    else
                                    {
                                        objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                                    }
                                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(objCustomerResponse.ApplicationType.ToString(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                                    if (objCustomerResponse.objOptions != null)
                                    {
                                        objCustomerResponse.MsjValidation = string.Empty;
                                        objCustomerResponse.InterfaceCustomer = objCustomerPost;
                                    }


                                }

                            }
                            else
                            {
                                if (strTypeSearchResult.Equals(strTipoGeneralDTH))
                                {
                                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Dth.ToString().ToUpper();
                                }
                                else
                                {
                                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                                }
                                objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(objCustomerResponse.ApplicationType.ToString(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                                if (objCustomerResponse.objOptions != null)
                                    objCustomerResponse.InterfaceCustomer = objCustomerPost;

                            }



                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                        }
                    }
                    else if (!objCustomerRequest.IsPostPaid)
                    {

                        objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Prepaid.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                        objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Prepaid.ToString().ToUpper();
                        objCustomerResponse.CodeResponse = string.Empty;
                        if (objCustomerResponse.objOptions != null)
                        {
                            Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest objGetPreviousCustomerRequest = new Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest
                            {
                                Telephone = strTelefono.Trim(),
                                Account = "",
                                ContactId = "",
                                FlagRegistry = Claro.Constants.NumberOneString,
                                ApplicationName = strNameApplication,
                                Audit = objCustomerRequest.Audit
                            };
                            Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse objPreviousCustomerResponse = Prepaid.GetPreviousCustomer(objGetPreviousCustomerRequest);
                            oCustomerPre = objPreviousCustomerResponse.objCustomer;
                            if (oCustomerPre != null)
                            {
                                Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("Parametros de Entrada ValidationMsjPrepaid -- CodeResponse: {0} ,ProviderId:{1} , EsTFI:{2},ValueSearch:{3} ", objPreviousCustomerResponse.CodeResponse, !string.IsNullOrEmpty(oCustomerPre.oService.ProviderID) ? oCustomerPre.oService.ProviderID.ToString() : "", oCustomerPre.oService.EsTFI.ToString(), objCustomerRequest.ValueSearch.ToString()));
                                objCustomerResponse.MsjValidation = ValidationMsjPrepaid((objPreviousCustomerResponse.CodeResponse), oCustomerPre.oService, objCustomerRequest);
                                oCustomerPre.ApplicationType = Claro.SIACU.ApplicationType.Prepaid;
                                Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("Parametros de Entrada ValidationMsjPrepaid -- CodeResponse: {0} ,ProviderId:{1} , EsTFI:{2},ValueSearch:{3} ", objPreviousCustomerResponse.CodeResponse, !string.IsNullOrEmpty(oCustomerPre.oService.ProviderID) ? oCustomerPre.oService.ProviderID.ToString() : "", oCustomerPre.oService.EsTFI.ToString(), objCustomerRequest.ValueSearch.ToString()));
                                objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Prepaid.ToString().ToUpper();
                            }
                            else
                            {
                                objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                            }
                            objCustomerResponse.InterfaceCustomer = oCustomerPre;
                            objCustomerResponse.CodeResponse = (string.IsNullOrEmpty(objPreviousCustomerResponse.CodeResponse) ? "" : objPreviousCustomerResponse.CodeResponse);
                        }
                    }



                }

                else if (strTypeSearchResult.Equals(strTipoGeneralHFC))
                {
                    string strResponseCode = "";
                    string strMessage = "";

                    strNameApplication = strNombreAplicacion;
                    strApplication = Claro.SIACU.Constants.HFC;
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Hfc.ToString().ToUpper();
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Hfc.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,
                             Plataform = strplataforma,
                             outOptional = outOptional
                         }).ObjCustomer;
                        if (objCustomerPost != null)
                        {
                            objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                            objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Hfc;
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Hfc.ToString().ToUpper();
                            objCustomerResponse.ListIndicatorIGV = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.IndicatorIGV>>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetIGV(objCustomerRequest.Audit, out strResponseCode, out strMessage); });
                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                        }
                        objCustomerResponse.InterfaceCustomer = objCustomerPost;
                    }

                }
                else if (strTypeSearchResult.Equals(strTipoGeneralLTE))
                {
                    string strResponseCode = "";
                    string strMessage = "";
                    strNameApplication = strNombreAplicacion;
                    strApplication = Claro.SIACU.Constants.LTE;
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Lte.ToString().ToUpper();
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Lte.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,
                             Plataform = strplataforma,
                             outOptional = outOptional
                         }).ObjCustomer;
                        if (objCustomerPost != null)
                        {
                            objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                            objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Lte;
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Lte.ToString().ToUpper();
                            objCustomerResponse.ListIndicatorIGV = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.IndicatorIGV>>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetIGV(objCustomerRequest.Audit, out strResponseCode, out strMessage); });
                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                        }
                        objCustomerResponse.InterfaceCustomer = objCustomerPost;
                    }

                }
                else if (strTypeSearchResult.Equals(strTipoGeneralInternet)) //IFI
                {
                    string strResponseCode = "";
                    string strMessage = "";
                    strNameApplication = strNombreAplicacion;
                    strApplication = Claro.SIACU.Constants.PostpaidMajuscule;
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Ifi.ToString().ToUpper();
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Ifi.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,
                             Plataform = strplataforma,
                             outOptional = outOptional
                         }).ObjCustomer;
                        if (objCustomerPost != null)
                        {
                            objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                            objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Ifi;
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Ifi.ToString().ToUpper();
                            objCustomerResponse.ListIndicatorIGV = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.IndicatorIGV>>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetIGV(objCustomerRequest.Audit, out strResponseCode, out strMessage); });
                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                        }
                        objCustomerResponse.InterfaceCustomer = objCustomerPost;
                    }
                }
                else if (strTypeSearchResult.Equals(CODE_PRODUCT_FIXE.ToUpper()))
                {
                    string CODIGO_PRODUCTO = CODE_PRODUCT_FIXE.ToUpper();
                    string OPTIONS_PRODUCTO = OPTIONS_PRODUCT_FIXE.ToUpper();
                    string strResponseCode = string.Empty;
                    string strMessage = string.Empty;

                    strNameApplication = strNombreAplicacion;
                    strApplication = CODIGO_PRODUCTO.ToUpper();
                    objCustomerResponse.ApplicationType = CODIGO_PRODUCTO.ToUpper();
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(OPTIONS_PRODUCTO.ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,
                             Plataform = strplataforma,
                             outOptional = outOptional
                         }).ObjCustomer;
                        if (objCustomerPost != null)
                        {
                            objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                            objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Fixe;
                            objCustomerResponse.ApplicationType = CODIGO_PRODUCTO.ToUpper();
                            objCustomerResponse.ListIndicatorIGV = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.IndicatorIGV>>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetIGV(objCustomerRequest.Audit, out strResponseCode, out strMessage); });
                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                        }

                        objCustomerResponse.InterfaceCustomer = objCustomerPost;
                    }

                }
                else
                {
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Prepaid.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Prepaid.ToString().ToUpper();
                    objCustomerResponse.CodeResponse = string.Empty;
                    if (objCustomerResponse.objOptions != null)
                    {
                        Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest objGetPreviousCustomerRequest = new Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest
                        {
                            Telephone = strLineaPrepago.Trim(),
                            Account = "",
                            ContactId = "",
                            FlagRegistry = Claro.Constants.NumberOneString,
                            ApplicationName = strNameApplication,
                            Audit = objCustomerRequest.Audit
                        };
                        Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse objPreviousCustomerResponse = Prepaid.GetPreviousCustomer(objGetPreviousCustomerRequest);
                        oCustomerPre = objPreviousCustomerResponse.objCustomer;
                        if (oCustomerPre != null)
                        {
                            Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("Parametros de Entrada ValidationMsjPrepaid -- CodeResponse: {0} ,ProviderId:{1} , EsTFI:{2},ValueSearch:{3} ", objPreviousCustomerResponse.CodeResponse, !string.IsNullOrEmpty(oCustomerPre.oService.ProviderID) ? oCustomerPre.oService.ProviderID.ToString() : "", oCustomerPre.oService.EsTFI.ToString(), objCustomerRequest.ValueSearch.ToString()));
                            objCustomerResponse.MsjValidation = ValidationMsjPrepaid((objPreviousCustomerResponse.CodeResponse), oCustomerPre.oService, objCustomerRequest);
                            oCustomerPre.ApplicationType = Claro.SIACU.ApplicationType.Prepaid;
                            Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("Parametros de Entrada ValidationMsjPrepaid -- CodeResponse: {0} ,ProviderId:{1} , EsTFI:{2},ValueSearch:{3} ", objPreviousCustomerResponse.CodeResponse, !string.IsNullOrEmpty(oCustomerPre.oService.ProviderID) ? oCustomerPre.oService.ProviderID.ToString() : "", oCustomerPre.oService.EsTFI.ToString(), objCustomerRequest.ValueSearch.ToString()));
                            objCustomerResponse.MsjValidation = ValidationMsjPrepaid((objPreviousCustomerResponse.CodeResponse), oCustomerPre.oService, objCustomerRequest);
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Prepaid.ToString().ToUpper();
                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                        }
                        objCustomerResponse.InterfaceCustomer = oCustomerPre;
                        objCustomerResponse.CodeResponse = (string.IsNullOrEmpty(objPreviousCustomerResponse.CodeResponse) ? "" : objPreviousCustomerResponse.CodeResponse);
                    }

                }

            }
            else if (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberThreeString)) //tipo de filtro contrato
            {
                strTelefono = strValueSearch;

                if (strTypeSearchResult.Equals(strTipoGeneralHFC))
                {
                    string strResponseCode = "";
                    string strMessage = "";
                    strNameApplication = Claro.SIACU.Constants.HFC;
                    strApplication = Claro.SIACU.Constants.HFC;
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Hfc.ToString().ToUpper();
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Hfc.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,
                             Plataform = strplataforma,
                             outOptional = outOptional
                         }).ObjCustomer;

                        if (objCustomerPost != null)
                        {
                            objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                            objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Hfc;
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Hfc.ToString().ToUpper();
                            objCustomerResponse.ListIndicatorIGV = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.IndicatorIGV>>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetIGV(objCustomerRequest.Audit, out strResponseCode, out strMessage); });
                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                        }
                        objCustomerResponse.InterfaceCustomer = objCustomerPost;
                    }

                }
                else if (strTypeSearchResult.Equals(strTipoGeneralLTE))
                {
                    string strResponseCode = "";
                    string strMessage = "";
                    strNameApplication = Claro.SIACU.Constants.LTE;
                    strApplication = Claro.SIACU.Constants.LTE;
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Lte.ToString().ToUpper();
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Lte.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,
                             Plataform = strplataforma,
                             outOptional = outOptional
                         }).ObjCustomer;

                        if (objCustomerPost != null)
                        {
                            objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                            objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Lte;
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Lte.ToString().ToUpper();
                            objCustomerResponse.ListIndicatorIGV = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.IndicatorIGV>>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetIGV(objCustomerRequest.Audit, out strResponseCode, out strMessage); });
                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                        }
                        objCustomerResponse.InterfaceCustomer = objCustomerPost;
                    }

                }
                else if (strTypeSearchResult.Equals(CODE_PRODUCT_FIXE.ToUpper()))
                {
                    string CODIGO_PRODUCTO = CODE_PRODUCT_FIXE.ToUpper();
                    string OPTIONS_PRODUCTO = OPTIONS_PRODUCT_FIXE.ToUpper();
                    string strResponseCode = string.Empty;
                    string strMessage = string.Empty;

                    strNameApplication = strNombreAplicacion;
                    strApplication = CODIGO_PRODUCTO.ToUpper();
                    objCustomerResponse.ApplicationType = CODIGO_PRODUCTO.ToUpper();
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(OPTIONS_PRODUCTO.ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,

                             outOptional = outOptional
                         }).ObjCustomer;
                        if (objCustomerPost != null)
                        {
                            objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                            objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Hfc;
                            objCustomerResponse.ApplicationType = CODIGO_PRODUCTO.ToUpper();
                            objCustomerResponse.ListIndicatorIGV = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.IndicatorIGV>>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetIGV(objCustomerRequest.Audit, out strResponseCode, out strMessage); });
                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                        }

                        objCustomerResponse.InterfaceCustomer = objCustomerPost;
                    }

                }
                else
                {
                    objCustomerRequest.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                    objCustomerResponse.ApplicationType = objCustomerRequest.ApplicationType;
                    objCustomerResponse.InterfaceCustomer = objCustomerPost;
                }

            }
            else if (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberEightString)) //NUMERO RECIBO
            {
                Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, "Iniciando busqueda por Nro Recibo");
                List<Parameter> lstParameter = null;
                strTelefono = "";
                strNumRecibo = strValueSearch;
                Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("el nro de recibo buscado es : {0}", strNumRecibo));
                string valor_c = string.Empty;
                string subNumRecibo = strNumRecibo.Substring(0, 4);
                Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("el nro de recibo (con solo los primeros 4 digistos)  buscado es : {0}", subNumRecibo));
                Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, "Iniciando metodo GetListParameters (capa Business) ");
                lstParameter = Claro.Web.Logging.ExecuteMethod<List<Parameter>>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetListParameters(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, KEY.AppSettings("strKeyRecibo_Prefijo_Cbios"));
                });
                if (lstParameter != null)
                {
                    Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, "lstParameter != null");
                    valor_c = (from u in lstParameter
                               where u.DESCRIPCION.Equals(subNumRecibo)
                               select u.VALOR_C).First();
                    Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("validando en tabla parametrizada valor_c  : {0}", valor_c));

                    Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, "Validando si es ASIS,MIGRADO o TOBE PURO");
                    if (valor_c.Equals(Claro.Constants.NumberSevenString))
                    {
                        strNumeroCuenta = Claro.Web.Logging.ExecuteMethod<string>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () =>
                        {
                            return Data.Dashboard.Postpaid.GetAccountTelephoneCustomer(
                                objCustomerRequest.Audit.Session,
                                objCustomerRequest.Audit.Transaction,
                                strNumRecibo, out strOutTelefono, out strplataforma);
                        });
                        //pivot
                        try
                        {
                            ConsultaLineaResponse response = null;
                            strplataforma = IsTobeOrAsis(Claro.Constants.NumberFourString, strOutTelefono, objCustomerRequest.Audit, ref response);
                            if (strplataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase) && response.itm != null)
                            {

                                Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("es TOBE MIGRADO"));
                                strNumeroCuenta = response.itm.origenRegistro != null && response.itm.origenRegistro.Equals(KEY.AppSettings("MIGRACION")) ? response.itm.custCode : string.Empty;
                                objCustomerResponse.itm = response.itm;
                                strTecnologia = response.itm.origenRegistro; //INICIATIVA-616
                            }
                            strOutTelefono = string.Empty;
                    }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    else
                    {
                        Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("es TOBE PURO"));
                        Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.request()
                        {

                            obtenerLineaPorIccidNroReciboRequest = new Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.obtenerLineaPorIccidNroReciboRequest()
                            {
                                customerId = string.Empty,
                                nroRecibo = strNumRecibo,
                                iccid = string.Empty


                            }

                        };
                        strNumeroCuenta = Claro.Web.Logging.ExecuteMethod<string>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () =>
                        {
                            return Data.Dashboard.Postpaid.GetAccountTelephoneCustomerTobe(objCustomerRequest.Audit,
                                request, out strOutTelefono, out strplataforma);
                        });

                        //pivot
                        try
                        {
                            ConsultaLineaResponse response = null;
                            strplataforma = IsTobeOrAsis(Claro.Constants.NumberTwoString, strNumeroCuenta, objCustomerRequest.Audit, ref response);
                            if (strplataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase) && response.itm != null)
                            {

                                Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("es TOBE PURO"));
                                objCustomerResponse.itm = response.itm;
                                strTecnologia = response.itm.origenRegistro; //INICIATIVA-616
                    }
                            strOutTelefono = string.Empty;
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                }
                }


                strNameApplication = strNombreAplicacion;

                if (!string.IsNullOrEmpty(strNumeroCuenta))
                {
                    strApplication = Claro.SIACU.Constants.PostpaidMajuscule;
                    if (!string.IsNullOrEmpty(strOutTelefono))
                    {
                        strNumeroCuenta = "";
                        strTelefono = strOutTelefono;
                    }
                }

                if (((string.IsNullOrEmpty(strNumeroCuenta)) && (!string.IsNullOrEmpty(strTelefono))) || ((!string.IsNullOrEmpty(strNumeroCuenta)) && (string.IsNullOrEmpty(strTelefono))))
                {
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,
                             Plataform = strplataforma,
                             outOptional = outOptional,
                             strTecnologia = strTecnologia
                         }).ObjCustomer;
                    }

                }
                else
                {
                    objCustomerPost.Application = "";
                }
                if (objCustomerPost != null && (objCustomerPost.Application.Equals(Claro.SIACU.Constants.PostpaidMajuscule)))
                {
                    objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                    objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Postpaid;
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                    objCustomerRequest.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                }
                else
                {
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                }
                objCustomerResponse.InterfaceCustomer = objCustomerPost;
            }
            else if (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberNineString)) //iccid
            {
                strICCID = strValueSearch;
                strTelefono = "";
                strNumeroCuenta = Claro.Web.Logging.ExecuteMethod<string>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetAccountTelephoneCustomer(
                     objCustomerRequest.Audit.Session,
                     objCustomerRequest.Audit.Transaction,
                    "", "", strICCID, out strOutTelefono, out strplataforma);
                });
                if (string.IsNullOrEmpty(strplataforma))
                {

                    Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.request()
                    {

                        obtenerLineaPorIccidNroReciboRequest = new Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.obtenerLineaPorIccidNroReciboRequest()
                        {
                            customerId = string.Empty,
                            nroRecibo = string.Empty,
                            iccid = strICCID,


                        }

                    };
                    strNumeroCuenta = Claro.Web.Logging.ExecuteMethod<string>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetAccountTelephoneCustomerTobe(objCustomerRequest.Audit,
                        request, out strOutTelefono, out strplataforma);
                    });
                }
                else
                {
                    //pivot
                    try
                    {
                        ConsultaLineaResponse response = null;
                        strplataforma = IsTobeOrAsis(strTypeSearch, strValueSearch, objCustomerRequest.Audit, ref response);
                        if (strplataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase) && response.itm != null)
                        {
                            objCustomerResponse.itm = response.itm;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


                strNameApplication = strNombreAplicacion;

                if (!string.IsNullOrEmpty(strOutTelefono))
                {
                    strNumeroCuenta = "";
                    strTelefono = strOutTelefono;
                    strApplication = Claro.SIACU.Constants.PostpaidMajuscule;
                    strTypeSearch = "1";
                    strValueSearch = strTelefono;
                    strTypeSearchResult = GetApplicationType(objCustomerRequest.Audit, ref strTypeSearch, ref strValueSearch, ref strFlagResultado, ref strMensajeResultado, ref strplataforma, out outOptional);
                }

                if (((string.IsNullOrEmpty(strNumeroCuenta)) && (!string.IsNullOrEmpty(strTelefono))) || ((!string.IsNullOrEmpty(strNumeroCuenta)) && (string.IsNullOrEmpty(strTelefono))))
                {
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                          new CustomerRequest()
                          {
                              TypeSearch = objCustomerRequest.TypeSearch,
                              AccountCustomer = strNumeroCuenta,
                              NumCellphone = strTelefono,
                              Application = strApplication,
                              ApplicationName = strNameApplication,
                              ProductType = strTypeSearchResult,
                              Audit = objCustomerRequest.Audit,
                              Plataform = strplataforma,
                              outOptional = outOptional
                          }).ObjCustomer;
                    }

                }
                else
                {
                    objCustomerPost.Application = "";
                }

                if (objCustomerPost != null && (objCustomerPost.Application.Equals(Claro.SIACU.Constants.PostpaidMajuscule)))
                {
                    objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                    objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Postpaid;
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                    objCustomerRequest.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                }
                else
                {
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                }
                objCustomerResponse.InterfaceCustomer = objCustomerPost;
            }
            else if (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberFourString)) //CUSTOMERID
            {
                strCustomerID = strValueSearch;
                strTelefono = "";
                strNumeroCuenta = Claro.Web.Logging.ExecuteMethod<string>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetAccountTelephoneCustomer(
                     objCustomerRequest.Audit.Session,
                     objCustomerRequest.Audit.Transaction,
                    strCustomerID, "", "", out strOutTelefono, out strplataforma);
                });

                Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("Busqueda por CustomerId strCustomerID: {0}, strOutTelefono:{1}, strplataforma:{2}", strCustomerID, strOutTelefono, strplataforma));

                if (string.IsNullOrEmpty(strplataforma))
                {

                    Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.request()
                    {

                        obtenerLineaPorIccidNroReciboRequest = new Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.obtenerLineaPorIccidNroReciboRequest()
                        {
                            customerId = strCustomerID,
                            nroRecibo = string.Empty,
                            iccid = string.Empty,


                        }

                    };
                    strNumeroCuenta = Claro.Web.Logging.ExecuteMethod<string>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetAccountTelephoneCustomerTobe(objCustomerRequest.Audit,
                         request, out strOutTelefono, out strplataforma);
                    });

                    Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("Busqueda por CustomerId strCustomerID: {0}, strOutTelefono:{1}, strplataforma:{2}, strNumeroCuenta:{3}", strCustomerID, strOutTelefono, strplataforma, strNumeroCuenta));
                }
                else
                {
                    //pivot
                    try
                    {
                        ConsultaLineaResponse response = null;
                        strplataforma = IsTobeOrAsis(strTypeSearch, strValueSearch, objCustomerRequest.Audit, ref response);

                        Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("Busqueda por IsTobeOrAsis strplataforma:{0}", strplataforma));

                        if (strplataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.request requestCuenta = new Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.request()
                            {

                                obtenerLineaPorIccidNroReciboRequest = new Entity.Dashboard.Postpaid.Legacy.GetAccountTelephoneCustomer.Request.obtenerLineaPorIccidNroReciboRequest()
                                {
                                    customerId = strCustomerID,
                                    nroRecibo = string.Empty,
                                    iccid = string.Empty,
                                }

                            };
                            strNumeroCuenta = Claro.Web.Logging.ExecuteMethod<string>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () =>
                            {
                                return Data.Dashboard.Postpaid.GetAccountTelephoneCustomerTobe(objCustomerRequest.Audit,
                                 requestCuenta, out strOutTelefono, out strplataforma);
                            });

                            Claro.Web.Logging.Info(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, string.Format("Busqueda por GetAccountTelephoneCustomerTobe strCustomerID: {0}, strOutTelefono:{1}, strplataforma:{2}, strNumeroCuenta:{3}", strCustomerID, strOutTelefono, strplataforma, strNumeroCuenta));
                        }

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


                strNameApplication = strNombreAplicacion;
                strApplication = Claro.SIACU.Constants.PostpaidMajuscule;
                objCustomerRequest.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();

                if (!string.IsNullOrEmpty(strOutTelefono))
                {
                    strNumeroCuenta = "";
                    strTelefono = strOutTelefono;
                }

                if (((string.IsNullOrEmpty(strNumeroCuenta)) && (!string.IsNullOrEmpty(strTelefono))) || ((!string.IsNullOrEmpty(strNumeroCuenta)) && (string.IsNullOrEmpty(strTelefono))))
                {
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,
                             Plataform = strplataforma,
                             outOptional = outOptional,
                             strTecnologia = strTecnologia //INICIATIVA-616
                         }).ObjCustomer;
                    }

                }
                if (objCustomerPost != null && !objCustomerPost.Application.ToString().Equals("NoPrecisado"))
                {
                    objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string strApplicationType;
                    string strType = GetPostpaidProductsType(
                        new Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts.PostpaidProductsRequest()
                        {
                            Audit = objCustomerRequest.Audit,
                            DocumentType = "DNI",
                            DocumentIdentity = objCustomerPost.NRO_DOC
                        }
                        , objCustomerPost, out strApplicationType);

                    sb.Append(objCustomerPost.TipoProducto);
                    sb.Append(strType);
                    objCustomerPost.TipoProducto = sb.ToString();
                    objCustomerResponse.ApplicationType = string.IsNullOrEmpty(strApplicationType) ? objCustomerRequest.ApplicationType : strApplicationType;
                    objCustomerResponse.InterfaceCustomer = objCustomerPost;
                }
                else
                {
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                }
            }
            else if (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberTwoString))
            {
                strTelefono = "";
                strNumeroCuenta = strValueSearch;

                strNameApplication = strNombreAplicacion;
                strApplication = Claro.SIACU.Constants.PostpaidMajuscule;
                objCustomerRequest.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                if (((string.IsNullOrEmpty(strNumeroCuenta)) && (!string.IsNullOrEmpty(strTelefono))) || ((!string.IsNullOrEmpty(strNumeroCuenta)) && (string.IsNullOrEmpty(strTelefono))))
                {
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                    objCustomerResponse.CodeResponse = string.Empty;
                    objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                    if (objCustomerResponse.objOptions != null)
                    {
                        objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,
                             Plataform = strplataforma,
                             outOptional = outOptional,
                             strTecnologia = strTecnologia //INICIATIVA-616
                         }).ObjCustomer;
                    }

                }
                if (objCustomerPost != null)
                {
                    objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    string strApplicationType;
                    string strType = GetPostpaidProductsType(
                        new Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts.PostpaidProductsRequest()
                        {
                            Audit = objCustomerRequest.Audit,
                            DocumentType = "DNI",
                            DocumentIdentity = objCustomerPost.NRO_DOC
                        }
                        , objCustomerPost, out strApplicationType);

                    sb.Append(objCustomerPost.TipoProducto);
                    sb.Append(strType);
                    objCustomerPost.TipoProducto = sb.ToString();
                    objCustomerResponse.ApplicationType = string.IsNullOrEmpty(strApplicationType) ? objCustomerRequest.ApplicationType : strApplicationType;
                    objCustomerResponse.InterfaceCustomer = objCustomerPost;
                }
                else
                {
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                }


            }
            else if (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberTenString))
            {
                strNameApplication = Claro.SIACU.Constants.HFC;
                strApplication = Claro.SIACU.Constants.HFC;
                strTelefono = strValueSearch;
                objCustomerRequest.ApplicationType = Claro.SIACU.ApplicationType.Hfc.ToString().ToUpper();
                objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Hfc.ToString().ToUpper();
                objCustomerResponse.CodeResponse = string.Empty;
                objCustomerResponse.objOptions = ConvertOptions(Data.Dashboard.Postpaid.GetOptions(Claro.SIACU.ApplicationType.Hfc.ToString().ToUpper(), objCustomerRequest.UserId.ToString(), objCustomerRequest.Audit));
                if (objCustomerResponse.objOptions != null)
                {
                    objCustomerPost = Postpaid.GetDataCustomer(
                         new CustomerRequest()
                         {
                             TypeSearch = objCustomerRequest.TypeSearch,
                             AccountCustomer = strNumeroCuenta,
                             NumCellphone = strTelefono,
                             Application = strApplication,
                             ApplicationName = strNameApplication,
                             ProductType = strTypeSearchResult,
                             Audit = objCustomerRequest.Audit,
                             Plataform = strplataforma,
                             outOptional = outOptional
                         }).ObjCustomer;
                }


                if (objCustomerPost != null)
                {
                    objCustomerPost.indicadorCambioNumero = outOptional != null ? outOptional.OutCaractAdi != null ? outOptional.OutCaractAdi.descripcion == "indCambioNum" ? outOptional.OutCaractAdi.valor : string.Empty : string.Empty : string.Empty;
                    objCustomerResponse.ApplicationType = objCustomerRequest.ApplicationType;
                    objCustomerResponse.InterfaceCustomer = objCustomerPost;
                }
                else
                {
                    objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                }


            }
            else
            {
                objCustomerRequest.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();

                objCustomerResponse.ApplicationType = objCustomerRequest.ApplicationType;
                objCustomerResponse.InterfaceCustomer = objCustomerPost;

            }
            return objCustomerResponse;
        }





        private static string GetPostpaidProductsType(Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts.PostpaidProductsRequest objPostpaidProductsRequest, Entity.Dashboard.Postpaid.Customer objCustomerPost, out string strApplicationType)
        {
            string ApplicationType = "";
            strApplicationType = "";
            Claro.SIACU.Entity.Dashboard.Board.GetPostpaidProducts.PostpaidProductsResponse objPostpaidProductsResponse;

            try
            {

                objPostpaidProductsResponse = Business.Dashboard.Dashboard.GetPostpaidProducts(objPostpaidProductsRequest);
                foreach (var item in objPostpaidProductsResponse.ListProduct)
                {

                    if (item.Cuenta.Equals(objCustomerPost.CUENTA))
                    {
                        if (item.Producto.ToUpper().Contains("1 PLAY INALAMBRICO") || item.Producto.ToUpper().Contains("2 PLAY INALAMBRICO") || item.Producto.ToUpper().Contains("3 PLAY INALAMBRICO") ||
                            item.Producto.ToUpper().Contains("PLAN LTE 1PLAY") || item.Producto.ToUpper().Contains("PLAN LTE 2PLAY") || item.Producto.ToUpper().Contains("PLAN LTE 3PLAY") ||
                            item.Producto.ToUpper().Contains("PLAN 1 PLAY FIJO -TELEFONO") || item.Producto.ToUpper().Contains("PLAN 2 PLAY FIJO -TELEFONO") || item.Producto.ToUpper().Contains("PLAN 3 PLAY FIJO -TELEFONO")
                           )
                        {
                            ApplicationType = " - LTE";
                            strApplicationType = "LTE";
                            break;
                        }
                        else if (item.Producto.ToUpper().Contains("1 PLAY ALAMBRICO") || item.Producto.ToUpper().Contains("2 PLAY ALAMBRICO") || item.Producto.ToUpper().Contains("3 PLAY ALAMBRICO") ||
                                 item.Producto.ToUpper().Contains("PLAN HFC 1PLAY") || item.Producto.ToUpper().Contains("PLAN HFC 2PLAY") || item.Producto.ToUpper().Contains("PLAN HFC 3PLAY") ||
                                 item.Producto.ToUpper().Contains("PLAN 1 PLAY INTERNET -TELEFONO") || item.Producto.ToUpper().Contains("PLAN 2 PLAY INTERNET -TELEFONO") || item.Producto.ToUpper().Contains("PLAN 3 PLAY INTERNET -TELEFONO") ||

                             item.Producto.ToUpper().Contains("HFC_3Play_One".ToUpper()) || item.Producto.ToUpper().Contains("HFC_1PlayInt_One".ToUpper()) || item.Producto.ToUpper().Contains("HFC_1PlayTv_One".ToUpper()) ||
                            item.Producto.ToUpper().Contains("HFC_2PlayIntTlf_One".ToUpper()) || item.Producto.ToUpper().Contains("HFC_2PlayIntTv_One".ToUpper()) || item.Producto.ToUpper().Contains("HFC_2PlayTlfTv_One".ToUpper()) ||
                            item.Producto.ToUpper().Contains("HFC_1PlayTlf_One".ToUpper())
)
                        {
                            ApplicationType = " - HFC";
                            strApplicationType = "HFC";
                            break;
                        }

                        else if (item.Producto.ToUpper().Contains("FTTH_3Play_One".ToUpper()) || item.Producto.ToUpper().Contains("FTTH_2PlayIntTv_One".ToUpper()) || item.Producto.ToUpper().Contains("FTTH_2PlayIntTlf_One".ToUpper()) ||
                            item.Producto.ToUpper().Contains("FTTH_2PlayTlfTv_One".ToUpper()) || item.Producto.ToUpper().Contains("FTTH_1PlayInt_One".ToUpper()) || item.Producto.ToUpper().Contains("FTTH_1PlayTlf_One".ToUpper()) ||
                            item.Producto.ToUpper().Contains("FTTH_1PlayTv_One".ToUpper()))
                        {
                            ApplicationType = " - FTTH";
                            strApplicationType = "FTTH";
                            break;
                        }


                        else if (item.Producto.ToUpper().Contains("TV SATELITAL"))
                        {
                            ApplicationType = " - DTH";
                            strApplicationType = "DTH";
                            break;
                        }
                        else if (item.Producto.ToUpper().Contains("FIJA"))
                        {
                            ApplicationType = " - TFI";
                            strApplicationType = "";
                            break;
                        }
                        else if (item.Producto.ToUpper().Contains("PUBLICA"))
                        {
                            ApplicationType = " - TPI";
                            strApplicationType = "";
                            break;
                        }
                        else if (item.Producto.ToUpper().Contains("MOVIL"))
                        {
                            ApplicationType = " - Telefonía Móvil";
                            strApplicationType = "";
                            break;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPostpaidProductsRequest.Audit.Session, objPostpaidProductsRequest.Audit.Transaction, ex.Message);
            }

            return ApplicationType;
        }



        /// <summary>
        /// Método para obtener la sesión de redirección utilizando el ValidarCredencialesWS.
        /// </summary>
        /// <param name="objRedirectSessionRequest">Contiene información necesaria para obtener las sesiones.</param>
        /// <returns>Devuelve objeto RedirectSessionResponse con la información de sesiones para realizar redirección externa.</returns>
        public static Claro.SIACU.Entity.Dashboard.Board.GetRedirectSession.RedirectSessionResponse GetRedirectSession(Claro.SIACU.Entity.Dashboard.Board.GetRedirectSession.RedirectSessionRequest objRedirectSessionRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.GetRedirectSession.RedirectSessionResponse objRedirectSessionResponse = null;
            string strErrorMsg = "", strCodError = "";
            try
            {
                objRedirectSessionResponse = new Claro.SIACU.Entity.Dashboard.Board.GetRedirectSession.RedirectSessionResponse()
                {
                    listRedirect = Claro.Web.Logging.ExecuteMethod<List<Redirect>>(objRedirectSessionRequest.Audit.Session, objRedirectSessionRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetRedirectSession(objRedirectSessionRequest.Audit, objRedirectSessionRequest.Application, objRedirectSessionRequest.Option, out  strErrorMsg, out strCodError); }),
                    ErrorMsg = strErrorMsg,
                    CodeError = strCodError
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRedirectSessionRequest.Audit.Session, objRedirectSessionRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            return objRedirectSessionResponse;
        }

        /// <summary>
        /// Método para insertar la información necesaria para realizar redirección utilizando el ValidarCredencialesWS.
        /// </summary>
        /// <param name="objInsertRedirectComRequest">Contiene información necesaria para redireccionar.</param>
        /// <returns>Devuelve objeto InsertRedirectComResponse con información para realizar la redirección externa.</returns>
        public static Claro.SIACU.Entity.Dashboard.Board.InsertRedirectCommunication.InsertRedirectComResponse InsertRedirectCommunication(Claro.SIACU.Entity.Dashboard.Board.InsertRedirectCommunication.InsertRedirectComRequest objInsertRedirectComRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.InsertRedirectCommunication.InsertRedirectComResponse objInsertRedirectComResponse = null;
            string strSequence = "", strUrl = "";
            try
            {
                objInsertRedirectComResponse = new Claro.SIACU.Entity.Dashboard.Board.InsertRedirectCommunication.InsertRedirectComResponse()
                {
                    ResultRegCommunication = Claro.Web.Logging.ExecuteMethod<string>(objInsertRedirectComRequest.Audit.Session, objInsertRedirectComRequest.Audit.Transaction, () => { return Data.Dashboard.Common.InsertRedirectCommunication(objInsertRedirectComRequest.Audit, objInsertRedirectComRequest.AppDest, objInsertRedirectComRequest.Option, objInsertRedirectComRequest.IpClient, objInsertRedirectComRequest.IpServer, objInsertRedirectComRequest.JsonParameters, out strSequence, out strUrl); }),
                    Sequence = strSequence,
                    Url = strUrl
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInsertRedirectComRequest.Audit.Session, objInsertRedirectComRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            return objInsertRedirectComResponse;
        }

        /// <summary>
        /// Método para obtener la información necesaria enviada por la redirección utilizando el ValidarCredencialesWS.
        /// </summary>
        /// <param name="objValidateRedirectRequest">Contiene código de secuencia.</param>
        /// <returns>Devuelve objeto ValidateRedirectComResponse con la información necesaria enviada por la redirección externa.</returns>
        public static Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication.ValidateRedirectComResponse ValidateRedirectCommunication(Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication.ValidateRedirectComRequest objValidateRedirectRequest)
        {
            Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication.ValidateRedirectComResponse objValidateRedirectResponse = null;
            string strUrlDest = "", strAvailability = "", strErrorMsg = "", strJsonParameters = "";
            try
            {
                objValidateRedirectResponse = new Claro.SIACU.Entity.Dashboard.Board.ValidateRedirectCommunication.ValidateRedirectComResponse()
                {
                    ResultValCommunication = Claro.Web.Logging.ExecuteMethod<Boolean>(objValidateRedirectRequest.Audit.Session, objValidateRedirectRequest.Audit.Transaction, () => { return Data.Dashboard.Common.ValidateRedirectCommunication(objValidateRedirectRequest.Audit, objValidateRedirectRequest.Sequence, out strErrorMsg, objValidateRedirectRequest.Server, out strUrlDest, out strAvailability, out strJsonParameters); }),
                    Availability = strAvailability,
                    UrlDest = strUrlDest,
                    ErrorMsg = strErrorMsg,
                    JsonParameters = strJsonParameters
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objValidateRedirectRequest.Audit.Session, objValidateRedirectRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            return objValidateRedirectResponse;
        }

        /// <summary>
        /// Método para obtener las instantáneas.
        /// </summary>
        /// <param name="objInstantsRequest">Contiene tipo de origen, tipo de teléfono y valor de búsqueda.</param>
        /// <returns>Devuelve objeto InstantsResponse con la información de instantáneas.</returns>
        public static Claro.SIACU.Entity.Dashboard.Board.GetInstants.InstantsResponse GetInstants(Claro.SIACU.Entity.Dashboard.Board.GetInstants.InstantsRequest objInstantsRequest)
        {
            List<Instant> listInstant = null;
            string strPostpaid = "", strHFC = "", strPrepaid = "";
            try
            {
                strPostpaid = KEY.AppSettings("strPostpaid");
                strHFC = KEY.AppSettings("strHFC");
                strPrepaid = KEY.AppSettings("strPrepaid");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objInstantsRequest.Audit.Session, objInstantsRequest.Audit.Transaction, ex.Message);
            }

            if (objInstantsRequest.OriginType.Equals(strPostpaid))
            {
                listInstant = Claro.Web.Logging.ExecuteMethod<List<Instant>>(objInstantsRequest.Audit.Session, objInstantsRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetListInstant(
                        objInstantsRequest.Audit.Session,
                        objInstantsRequest.Audit.Transaction,
                        objInstantsRequest.DataSearch, objInstantsRequest.TypePhone);
                });

            }
            else if (objInstantsRequest.OriginType.Equals(strHFC))
            {
                listInstant = Claro.Web.Logging.ExecuteMethod<List<Instant>>(objInstantsRequest.Audit.Session, objInstantsRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetListInstantByContract(
                     objInstantsRequest.Audit.Session,
                     objInstantsRequest.Audit.Transaction,
                    objInstantsRequest.DataSearch, objInstantsRequest.TypePhone);
                });

            }
            else if (objInstantsRequest.OriginType.Equals(strPrepaid))
            {
                listInstant = Claro.Web.Logging.ExecuteMethod<List<Instant>>(objInstantsRequest.Audit.Session, objInstantsRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Prepaid.GetListInstant(
                     objInstantsRequest.Audit.Session,
                     objInstantsRequest.Audit.Transaction,
                    objInstantsRequest.DataSearch, objInstantsRequest.TypePhone);
                });

            }

            Claro.SIACU.Entity.Dashboard.Board.GetInstants.InstantsResponse objInstantsResponse = new Entity.Dashboard.Board.GetInstants.InstantsResponse()
            {
                ListInstant = listInstant
            };
            return objInstantsResponse;
        }

        /// <summary>
        /// Método para obtener el nombre del cliente.
        /// </summary>
        /// <param name="objCustomerNameRequest">Contiene tipo y valor de la búsqueda.</param>
        /// <returns>Devuelve objeto CustomerNameResponse con la información de clientes.</returns>
        public static CustomerNameResponse GetCustomerName(CustomerNameRequest objCustomerNameRequest)
        {
            CustomerNameResponse objCustomers = null;

            objCustomers = Claro.Web.Logging.ExecuteMethod<CustomerNameResponse>(objCustomerNameRequest.Audit.Session, objCustomerNameRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetCustomerName(objCustomerNameRequest.Audit, objCustomerNameRequest.SearchType, objCustomerNameRequest.SearchValue); });

            return objCustomers;
        }

        //INICIATIVA 616 -- PARA VERIFICAR SI EL ULTIMO NUMERO ACTIVO
        public static String GetNumberActiveTOBEFIJA(Claro.Entity.AuditRequest objCustomerRequestAudit, string strtelefono)
        {
            string strNumberActive = string.Empty;
            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat oOptional = null;
            List<ProductType> listDataTypeProduct = null;
            string idContrato = null;
            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.OfertaProducto ofertaProducto = null;
            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.CaracteristicaAdicional CaracteristicaAdicional = null;
            ofertaProducto = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.OfertaProducto()
            {
                producto = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.Producto()
                {
                    recursoLogico = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.RecursoLogico()
                    {
                        numeroLinea = strtelefono
                    }
                }
            };
            CaracteristicaAdicional = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.CaracteristicaAdicional()
            {
                descripcion = KEY.AppSettings("keyTipoBusqueda").ToString(),
                valor = KEY.AppSettings("keyContractSearchWithoutHistory").ToString()
            };
            
            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request()
            {
                contrato = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.Contrato()
                {
                    idContrato = idContrato,
                    ofertaProducto = ofertaProducto,
                    caracteristicaAdicional = CaracteristicaAdicional
                }
            };

            listDataTypeProduct = Claro.Web.Logging.ExecuteMethod<List<ProductType>>(objCustomerRequestAudit.Session, objCustomerRequestAudit.Transaction, () => { return Data.Dashboard.Common.GetTypeProductDatTobe(objCustomerRequestAudit, request, string.Empty, ref strNumberActive, out  oOptional); });

            return strNumberActive;

        }
        //INICIATIVA 616 -- PARA VERIFICAR SI EL ULTIMO NUMERO ACTIVO



        public static List<ProductType> GetTypeProductPivotTobe(string strContrato, string strtelefono, Claro.Entity.AuditRequest objCustomerRequestAudit, string TypeSearch, ref string strAux, out  Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat outOptional)
        {
            Claro.Web.Logging.Info("ONE-FIJA", "ONE-FIJA", "TypeSearch : " + TypeSearch);
            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat oOptional = null;
            string xstrAux = string.Empty;
            List<ProductType> listDataTypeProduct = null;
            if (KEY.AppSettings("flagGetProductType").ToString().Equals(Claro.Constants.NumberZeroString))
            {
                Entity.Dashboard.Postpaid.Legacy.GetTypeProduct.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetTypeProduct.Request.request()
                {

                    obtenerTipoProductoRequest = new Entity.Dashboard.Postpaid.Legacy.GetTypeProduct.Request.obtenerTipoProductoRequest()
                    {
                        coId = strContrato,
                        nroLinea = strtelefono
                    }
                };

                //roll13
                //listDataTypeProduct = Claro.Web.Logging.ExecuteMethod<List<ProductType>>(objCustomerRequestAudit.Session, objCustomerRequestAudit.Transaction, () => { return Data.Dashboard.Common.GetTypeProductTobe(objCustomerRequestAudit, request, ref xstrAux); });
                //INICIATIVA 616
                listDataTypeProduct = new List<ProductType>();
                listDataTypeProduct.Add(new Claro.SIACU.Entity.Dashboard.ProductType()
                {
                    CODIGO_PRODUCTO = "05",
                    TIPO_PRODUCTO = "3 PLAY"
                });
                //INICIATIVA 616
            }
            else
            {
                string idContrato = strContrato;
                Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.OfertaProducto ofertaProducto = null;
                Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.CaracteristicaAdicional CaracteristicaAdicional = null;
                ofertaProducto = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.OfertaProducto()
                {
                    producto = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.Producto()
                    {
                        recursoLogico = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.RecursoLogico()
                        {
                            numeroLinea = strtelefono
                        }
                    }
                };
                CaracteristicaAdicional = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.CaracteristicaAdicional()
                {
                    descripcion = KEY.AppSettings("keyTipoBusqueda").ToString(),
                    valor = KEY.AppSettings("keyContractSearchWithoutHistory").ToString()
                };
                if (TypeSearch.Equals(Claro.Constants.NumberOneString)) idContrato = null;
                if (TypeSearch.Equals(Claro.Constants.NumberThreeString)) { ofertaProducto = null; CaracteristicaAdicional = null; }

                Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request()
                {
                    contrato = new Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Request.request.Contrato()
                    {
                        idContrato = idContrato,
                        ofertaProducto = ofertaProducto,
                        caracteristicaAdicional = CaracteristicaAdicional
                    }
                };

                listDataTypeProduct = Claro.Web.Logging.ExecuteMethod<List<ProductType>>(objCustomerRequestAudit.Session, objCustomerRequestAudit.Transaction, () => { return Data.Dashboard.Common.GetTypeProductDatTobe(objCustomerRequestAudit, request, TypeSearch, ref xstrAux, out  oOptional); });

                if (listDataTypeProduct == null || listDataTypeProduct.Count == 0) //Si no devuelve informacion, se validara nuevamente con otro tipo de Search
                {
                    if (!TypeSearch.Equals(Claro.Constants.NumberThreeString))
                    {
                        CaracteristicaAdicional.valor = KEY.AppSettings("keyContractSearchWithHistory").ToString();
                        request.contrato.caracteristicaAdicional = CaracteristicaAdicional;
                listDataTypeProduct = Claro.Web.Logging.ExecuteMethod<List<ProductType>>(objCustomerRequestAudit.Session, objCustomerRequestAudit.Transaction, () => { return Data.Dashboard.Common.GetTypeProductDatTobe(objCustomerRequestAudit, request, TypeSearch, ref xstrAux, out  oOptional); });
                    }
                }

            }
            strAux = xstrAux;
            outOptional = oOptional;
            return listDataTypeProduct;
        }
        /// <summary>
        /// Método para obtener el tipo de aplicación.
        /// </summary>
        /// <param name="strIdSession">Id de sessión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="strTypeSearch">Tipo de búsqueda</param>
        /// <param name="strValueSearch">Valor de búsqueda</param>
        /// <param name="StrFlagQuery">Flag de consulta</param>
        /// <param name="StrMesageText">Texto del mensaje</param>
        /// <returns>Devuelve cadena con información del tipo de aplicación.</returns>
        public static string GetApplicationType(Claro.Entity.AuditRequest objCustomerRequestAudit, ref string strTypeSearch, ref string strValueSearch, ref string StrFlagQuery, ref string StrMesageText, ref string plataforma, out  Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat outOptional)
        {
            List<ProductType> listDataTypeProduct = null;
            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat oOptional = null;

            string strTypeSearchResult = "";
            string strtelefono = "";
            string strContrato = "";
            string strAux = "";

            switch (strTypeSearch)
            {
                case Claro.Constants.NumberOneString: strtelefono = strValueSearch; break;
                case Claro.Constants.NumberThreeString: strContrato = strValueSearch; break;
                default: strTypeSearchResult = Claro.Constants.NumberZeroString; break;
            }

            if (!strTypeSearchResult.Equals(Claro.Constants.NumberZeroString))
            {
                string strTipoProductoLTE = "";
                string strTipoGeneralLTE = "";
                string strTipoProductoMovil = "";
                string strTipoGeneralMOVIL = "";
                string strTipoProductoHFC = "";
                string strTipoGeneralHFC = "";
                string strTipoProductoTFI = "";
                string strTipoGeneralTFI = "";
                string strTipoProductoDTH = "";
                string strTipoGeneralDTH = "";
                string strTipoProductoBam = "";
                string strTipoProductoTPI = "";
                string strTipoGeneralTPI = "";

                string strTipoGeneralFIJA = string.Empty;
                string CODE_PRODUCT_FIXE = string.Empty;
                string OPTIONS_PRODUCT_FIXE = string.Empty;

                //mg13
                string strTipoGeneralInternet = "";
                string strTipoProductoInternet = "";


                try
                {
                    strTipoProductoLTE = KEY.AppSettings("strTipoProductoLTE");
                    strTipoGeneralLTE = KEY.AppSettings("strTipoGeneralLTE");
                    strTipoProductoMovil = KEY.AppSettings("strTipoProductoMovil");
                    strTipoGeneralMOVIL = KEY.AppSettings("strTipoGeneralMOVIL");
                    strTipoProductoBam = KEY.AppSettings("strTipoProductoBam");
                    strTipoProductoHFC = KEY.AppSettings("strTipoProductoHFC");
                    strTipoGeneralHFC = KEY.AppSettings("strTipoGeneralHFC");
                    strTipoProductoTFI = KEY.AppSettings("strTipoProductoTFI");
                    strTipoGeneralTFI = KEY.AppSettings("strTipoGeneralTFI");
                    strTipoProductoDTH = KEY.AppSettings("strTipoProductoDTH");
                    strTipoGeneralDTH = KEY.AppSettings("strTipoGeneralDTH");
                    strTipoProductoTPI = KEY.AppSettings("strTipoProductoTPI");
                    strTipoGeneralTPI = KEY.AppSettings("strTipoGeneralTPI");

                    //mg13
                    strTipoGeneralInternet = KEY.AppSettings("strTipoGeneralInternet");
                    strTipoProductoInternet = KEY.AppSettings("strTipoProductoInternet");

                    strTipoGeneralFIJA = KEY.AppSettings("strTipoGeneralFIJA");
                    CODE_PRODUCT_FIXE = strTipoGeneralFIJA.Split('|')[1].ToString();
                    OPTIONS_PRODUCT_FIXE = strTipoGeneralFIJA.Split('|')[2].ToString();


                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objCustomerRequestAudit.Session, objCustomerRequestAudit.Transaction, ex.Message);
                }

                if (plataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
                {
                    //mg13
                    listDataTypeProduct = GetTypeProductPivotTobe(strContrato, strtelefono, objCustomerRequestAudit, strTypeSearch, ref strAux, out oOptional);
                    if (KEY.AppSettings("flagGetProductType").ToString().Equals(Claro.Constants.NumberOneString)) strValueSearch = strAux;

                }
                else
                {
                    listDataTypeProduct = Claro.Web.Logging.ExecuteMethod<List<ProductType>>(objCustomerRequestAudit.Session, objCustomerRequestAudit.Transaction, () => { return Data.Dashboard.Common.GetTypeProduct(objCustomerRequestAudit.Session, objCustomerRequestAudit.Transaction, strtelefono, strContrato, ref strAux); });

                }
                if (listDataTypeProduct != null && listDataTypeProduct.Count > 0)
                {
                    foreach (ProductType productType in listDataTypeProduct)
                    {
                        Claro.Web.Logging.Info(objCustomerRequestAudit.Session, objCustomerRequestAudit.Transaction, string.Format("ProductType Codigo_Producto: {0}, Tipo_Producto:{1}", productType.CODIGO_PRODUCTO, productType.TIPO_PRODUCTO));
                        if (!string.IsNullOrEmpty(productType.TIPO_PRODUCTO))
                        {
                            if (productType.TIPO_PRODUCTO.Equals(strTipoProductoLTE))
                            {
                                strTypeSearchResult = strTipoGeneralLTE;

                                if (strTypeSearch.Equals(Claro.Constants.NumberOneString) && (!strAux.Equals(Claro.Constants.NumberZeroString)))
                                {
                                    strTypeSearch = Claro.Constants.NumberThreeString;
                                    strValueSearch = strAux;
                                }

                                break;
                            }
                            else if (productType.TIPO_PRODUCTO.Equals(strTipoProductoMovil) || productType.TIPO_PRODUCTO.Equals(strTipoProductoBam))
                            {
                                strTypeSearchResult = strTipoGeneralMOVIL;

                                if (strTypeSearch.Equals(Claro.Constants.NumberThreeString) && (!strAux.Equals(Claro.Constants.NumberZeroString)))
                                {
                                    strTypeSearch = Claro.Constants.NumberOneString;
                                    strValueSearch = strAux;
                                }
                                break;
                            }
                            else if (productType.TIPO_PRODUCTO.Equals(strTipoProductoHFC))
                            {
                                strTypeSearchResult = strTipoGeneralHFC;

                                //INICIATIVA 616
                                if (plataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    if (strTypeSearch.Equals(Claro.Constants.NumberThreeString) && (!strAux.Equals(Claro.Constants.NumberZeroString)))
                                    {
                                        strTypeSearch = Claro.Constants.NumberOneString;
                                        strValueSearch = strAux;
                                    }
                                }                                
                                else
                                {
                                    if (strTypeSearch.Equals(Claro.Constants.NumberOneString) && (!strAux.Equals(Claro.Constants.NumberZeroString)))
                                    {
                                        strTypeSearch = Claro.Constants.NumberThreeString;
                                        strValueSearch = strAux;
                                    }
                                }
                                //INICIATIVA 616

                                break;
                            }
                            else if (productType.TIPO_PRODUCTO.Equals(strTipoProductoTFI))
                            {
                                strTypeSearchResult = strTipoGeneralTFI;

                                if (strTypeSearch.Equals(Claro.Constants.NumberThreeString) && (!strAux.Equals(Claro.Constants.NumberZeroString)))
                                {
                                    strTypeSearch = Claro.Constants.NumberOneString;
                                    strValueSearch = strAux;
                                }
                            }
                            else if (productType.TIPO_PRODUCTO.Equals(strTipoProductoDTH))
                            {
                                strTypeSearchResult = strTipoGeneralDTH;

                                if (strTypeSearch.Equals(Claro.Constants.NumberThreeString) && (!strAux.Equals(Claro.Constants.NumberZeroString)))
                                {
                                    strTypeSearch = Claro.Constants.NumberOneString;
                                    strValueSearch = strAux;
                                }
                            }
                            else if (productType.TIPO_PRODUCTO.Equals(strTipoProductoTPI))
                            {
                                strTypeSearchResult = strTipoGeneralTPI;

                                if (strTypeSearch.Equals(Claro.Constants.NumberThreeString) && (!strAux.Equals(Claro.Constants.NumberZeroString)))
                                {
                                    strTypeSearch = Claro.Constants.NumberOneString;
                                    strValueSearch = strAux;
                                }
                            }//mg13
                            else if (productType.TIPO_PRODUCTO.Equals(strTipoProductoInternet))
                            {
                                strTypeSearchResult = strTipoGeneralInternet;

                                if (strTypeSearch.Equals(Claro.Constants.NumberThreeString) && (!strAux.Equals(Claro.Constants.NumberZeroString)))
                                {
                                    strTypeSearch = Claro.Constants.NumberOneString;
                                    strValueSearch = strAux;
                                }
                            }
                            else if (productType.TIPO_PRODUCTO.Equals(CODE_PRODUCT_FIXE))
                            {
                                strTypeSearchResult = CODE_PRODUCT_FIXE;
                                if (strTypeSearch.Equals(Claro.Constants.NumberOneString) && (!strAux.Equals(Claro.Constants.NumberZeroString)))
                                {
                                    strTypeSearch = Claro.Constants.NumberThreeString;
                                    strValueSearch = strAux;
                                }
                            }

                            else
                            {
                                strTypeSearchResult = Claro.Constants.NumberZeroString;
                            }
                        }
                        else
                        {
                            strTypeSearchResult = Claro.Constants.NumberZeroString;
                        }
                    }
                }

            }
            outOptional = oOptional;
            return strTypeSearchResult;
        }

        /// <summary>
        /// Método para obtener el mensaje del banner.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="dteDate">Fecha</param>
        /// <param name="strState">Estado</param>
        /// <param name="strTelephoneType">Tipo de teléfono</param>
        /// <param name="strApplication">Nombre de aplicación</param>
        /// <returns>Devuelve cadena con mensaje del banner.</returns>
        public static string GetBanner(string strIdSession, string strTransactionID, DateTime dteDate, string strState, string strTelephoneType, string strApplication)
        {
            List<Banner> lstBanner = null;
            string strBannerMessage = "";
            string strMessage = "";

            try
            {
                if (string.Equals(strApplication, Claro.SIACU.Constants.PrepaidMajuscule, StringComparison.OrdinalIgnoreCase))
                {
                    lstBanner = Claro.Web.Logging.ExecuteMethod<List<Banner>>(strIdSession, strTransactionID, () => { return Data.Dashboard.Prepaid.GetBanner(strIdSession, strTransactionID, dteDate, strTelephoneType); });
                }
                else
                {
                    lstBanner = Claro.Web.Logging.ExecuteMethod<List<Banner>>(strIdSession, strTransactionID, () => { return Data.Dashboard.Postpaid.GetBanner(strIdSession, strTransactionID, dteDate, strState, strTelephoneType); });
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, ex.Message);
            }

            if (lstBanner != null)
            {
                foreach (Banner item in lstBanner)
                {
                    strMessage = item.MENSAJE;

                    if ((item.ORDEN_PRIORIDAD == Claro.Constants.NumberOne)) strMessage = string.Format("<strong style=\'FONT-WEIGHT: bold; COLOR: #000080;\'>{0}</strong>", strMessage);

                    if (string.IsNullOrEmpty(strBannerMessage))
                    {
                        strBannerMessage = strMessage;
                    }
                    else
                    {
                        strBannerMessage = string.Format("{0}{1}", " --- ", strMessage);
                    }
                }
            }
            return strBannerMessage;
        }

        /// <summary>
        /// Método para obtener el archivo de un servidor FTP.
        /// </summary>
        /// <param name="oInvoiceFtpRequest">Contiene nombre y ruta del archivo.</param>
        /// <returns>Devuelve objeto InvoiceFtpResponse con información del archivo.</returns>
        public static InvoiceFtpResponse GetInvoiceFtp(InvoiceFtpRequest oInvoiceFtpRequest)
        {

            InvoiceFtpResponse oInvoiceFtpResponse = Claro.Web.Logging.ExecuteMethod<InvoiceFtpResponse>(1, () => { return (new Data.Dashboard.Common()).GetInvoiceFtp(oInvoiceFtpRequest.Audit.Session, oInvoiceFtpRequest.Audit.Transaction, oInvoiceFtpRequest.filePath, oInvoiceFtpRequest.fileName); });
            return oInvoiceFtpResponse;
        }

        /// <summary>
        /// Método para obtener el archivo por ruta compartida.
        /// </summary>
        /// <param name="getFileInvoiceRequest">Contiene la ruta del archivo.</param>
        /// <returns>Devuelve objeto FileInvoiceResponse con información del archivo.</returns>
        public static FileInvoiceResponse GetFileInvoice(FileInvoiceRequest getFileInvoiceRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<FileInvoiceResponse>(getFileInvoiceRequest.Audit.Session, getFileInvoiceRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetFileInvoice(getFileInvoiceRequest.Audit.Session, getFileInvoiceRequest.Audit.Transaction, getFileInvoiceRequest.Path); });
        }

        /// <summary>
        /// Método para obtener el archivo por suplantación predeterminada.
        /// </summary>
        /// <param name="getFileDefaultImpersonation">Contiene la ruta del archivo.</param>
        /// <returns>Devuelve objeto FileDefaultImpersonationResponse con información del archivo.</returns>
        public static FileDefaultImpersonationResponse GetFileDefaultImpersonation(FileDefaultImpersonationRequest getFileDefaultImpersonation)
        {
            return Claro.Web.Logging.ExecuteMethod<FileDefaultImpersonationResponse>(1, () => { return (new Data.Dashboard.Common()).GetFileDefaultImpersonation(getFileDefaultImpersonation.Audit.Session, getFileDefaultImpersonation.Audit.Transaction, getFileDefaultImpersonation.Path); });
        }
        /// <summary>
        /// Método para obtener archivo pdf en formato arrary byteTOBE.
        /// </summary>
        /// <param name="getGetTypeMIMERequest">Contiene la extensión del archivo.</param>
        /// <returns>Devuelve objeto TypeMIMEResponse con información de la extensión MIME.</returns>
        public static Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse GetFileAjusteV3(FileDefaultImpersonationRequest getFileDefaultImpersonation)
        {
            Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse objFileDefaultImpersonationResponse = new Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse()
            {
                ObjGlobalDocument = Claro.Web.Logging.ExecuteMethod<Entity.File.GlobalDocument>(1, () => { return Data.Dashboard.Postpaid.GetFileAjusteV3(getFileDefaultImpersonation.Audit.Session, getFileDefaultImpersonation.Audit.Transaction, getFileDefaultImpersonation.Path, getFileDefaultImpersonation.strNumeroRecibo, getFileDefaultImpersonation.DateRegister, getFileDefaultImpersonation.strIdOnBase); })
            };
            return objFileDefaultImpersonationResponse;

        }
        /// <summary>
        /// Método para obtener archivo pdf en formato arrary byte.
        /// </summary>
        /// <param name="getGetTypeMIMERequest">Contiene la extensión del archivo.</param>
        /// <returns>Devuelve objeto TypeMIMEResponse con información de la extensión MIME.</returns>
        public static Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse GetFileAjuste(FileDefaultImpersonationRequest getFileDefaultImpersonation)
        {
            Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse objFileDefaultImpersonationResponse = new Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse()
            {
                ObjGlobalDocument = Claro.Web.Logging.ExecuteMethod<Entity.File.GlobalDocument>(1, () => { return Data.Dashboard.Postpaid.GetFileAjuste(getFileDefaultImpersonation.Audit.Session, getFileDefaultImpersonation.Audit.Transaction, getFileDefaultImpersonation.Path, getFileDefaultImpersonation.DateRegister); })
            };
            return objFileDefaultImpersonationResponse;

        }

        /// <summary>
        /// Método para obtener la extensión MIME del archivo.
        /// </summary>
        /// <param name="getGetTypeMIMERequest">Contiene la extensión del archivo.</param>
        /// <returns>Devuelve objeto TypeMIMEResponse con información de la extensión MIME.</returns>
        public static TypeMIMEResponse GetTypeMIME(TypeMIMERequest getGetTypeMIMERequest)
        {
            return Claro.Web.Logging.ExecuteMethod<TypeMIMEResponse>(getGetTypeMIMERequest.Audit.Session, getGetTypeMIMERequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetTypeMIME(getGetTypeMIMERequest.Audit.Session, getGetTypeMIMERequest.Audit.Transaction, getGetTypeMIMERequest.Extension); });
        }

        /// <summary>
        /// Método para obtener el número de recibo.
        /// </summary>
        /// <param name="objReceiptNumberRequest">Contiene número de recibo y fecha de emisión.</param>
        /// <returns>Devuelve objeto ReceiptNumberResponse con información del recibo.</returns>
        public static ReceiptNumberResponse GetReceiptNumber(ReceiptNumberRequest objReceiptNumberRequest)
        {
            string strInvoiceNumber = objReceiptNumberRequest.InvoiceNumber.Trim();
            string strEmissionDate = objReceiptNumberRequest.EmissionDate.Trim();
            string strNroReciboTemp = "";
            int intLongitud = Claro.Constants.NumberZero;
            string strParteNro = "";
            string sFechaEmisionCorta = "";
            string sFechaEmision = strEmissionDate.Trim();
            string strFechaCorte = Claro.SIACU.Constants.Receipt8;
            string strFechaCorte1 = Claro.SIACU.Constants.Receipt9;
            string lSalida = "";
            string lSalida7 = "";
            string lSalida8 = "";
            string lSalida9 = "";

            try
            {
                sFechaEmisionCorta = (sFechaEmision.Length > 8 ? sFechaEmision.Substring(6, 4) + sFechaEmision.Substring(3, 2) + sFechaEmision.Substring(0, 2) : sFechaEmision);
                strNroReciboTemp = strInvoiceNumber.Substring(0, strInvoiceNumber.Length - 6);
                intLongitud = strNroReciboTemp.Length;

                if (intLongitud >= 7)
                {
                    lSalida7 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(intLongitud - 7);
                    lSalida8 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(intLongitud - 8);
                    lSalida9 = Claro.SIACU.Constants.T001_ + strNroReciboTemp.Substring(0, 10);
                }
                else
                {
                    for (int i = 1; i <= 7 - intLongitud; i++) strParteNro = Claro.Constants.NumberZeroString + strParteNro;
                    lSalida7 = Claro.SIACU.Constants.T001_ + strParteNro + strNroReciboTemp;
                    for (int i = 1; i <= 8 - intLongitud; i++) strParteNro = Claro.Constants.NumberZeroString + strParteNro;
                    lSalida8 = Claro.SIACU.Constants.T001_ + strParteNro + strNroReciboTemp;
                }

                if (string.CompareOrdinal(sFechaEmisionCorta, strFechaCorte) < 0)
                {
                    lSalida = lSalida7;
                }
                else
                {
                    lSalida = ((string.CompareOrdinal(sFechaEmisionCorta, strFechaCorte1) < 0) ? lSalida8 : lSalida9);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objReceiptNumberRequest.Audit.Session, objReceiptNumberRequest.Audit.Transaction, ex.Message);
            }

            ReceiptNumberResponse objReceiptNumberResponse = new ReceiptNumberResponse()
            {
                ReceiptNumber = lSalida
            };

            return objReceiptNumberResponse;
        }


        /// <summary>
        /// Método para obtener datos del cliente.
        /// </summary>
        /// <param name="objCustomerRequest">Contiene tipo y valor de la búsqueda.</param>
        /// <returns>Devuelve objeto CustomerResponse con la información del cliente.</returns>
        public static Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse GetCustomerInstantMasive(Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerRequest objCustomerRequest)
        {
            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat outOptional = null;

            Claro.SIACU.Entity.Dashboard.Board.GetCustomer.CustomerResponse objCustomerResponse = new Entity.Dashboard.Board.GetCustomer.CustomerResponse();
            string strplataforma = "";
            string strNameApplication = "";
            string strApplication = "";
            string strNumeroCuenta = "";
            string strTelefono = "";


            string strTypeSearch = objCustomerRequest.TypeSearch;
            string strValueSearch = objCustomerRequest.ValueSearch;
            string strTypeSearchResult = "";
            string strFlagResultado = "";
            string strMensajeResultado = "";

            string strTipoGeneralMOVIL = "";
            string strTipoGeneralHFC = "";
            string strTipoGeneralLTE = "";
            string strTipoGeneralTFI = "";
            string strTipoGeneralDTH = "";
            string strNombreAplicacion = "";

            try
            {
                strTipoGeneralMOVIL = KEY.AppSettings("strTipoGeneralMOVIL");
                strTipoGeneralHFC = KEY.AppSettings("strTipoGeneralHFC");
                strTipoGeneralLTE = KEY.AppSettings("strTipoGeneralLTE");
                strTipoGeneralTFI = KEY.AppSettings("strTipoGeneralTFI");
                strTipoGeneralDTH = KEY.AppSettings("strTipoGeneralDTH");
                strNombreAplicacion = KEY.AppSettings("NombreAplicacion");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, ex.Message);
            }

            if (objCustomerRequest.ApplicationType.Equals(""))
            {
                strTypeSearchResult = GetApplicationType(objCustomerRequest.Audit, ref strTypeSearch, ref strValueSearch, ref strFlagResultado, ref strMensajeResultado, ref strplataforma, out outOptional);
                objCustomerRequest.TypeSearch = strTypeSearch;
                objCustomerRequest.ValueSearch = strValueSearch;
                if (strTypeSearchResult == null)
                {
                    strTypeSearchResult = "";
                }
            }
            else
            {
                if (objCustomerRequest.ApplicationType.Equals(Claro.SIACU.Constants.PostpaidMajuscule))
                {
                    strTypeSearchResult = strTipoGeneralMOVIL;
                }
                else if (objCustomerRequest.ApplicationType.Equals(Claro.SIACU.Constants.HFC))
                {
                    strTypeSearchResult = strTipoGeneralHFC;
                }
                else if (objCustomerRequest.ApplicationType.Equals(Claro.SIACU.Constants.LTE))
                {
                    strTypeSearchResult = strTipoGeneralLTE;
                }

            }

            Entity.Dashboard.Prepaid.Customer oCustomerPre = new Entity.Dashboard.Prepaid.Customer();
            Entity.Dashboard.Postpaid.Customer objCustomerPost = new Entity.Dashboard.Postpaid.Customer();

            if (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberOneString))
            {
                strTelefono = strValueSearch;

                if ((strTypeSearchResult.Equals(strTipoGeneralMOVIL)) || (strTypeSearchResult.Equals(strTipoGeneralTFI)) || (strTypeSearchResult.Equals(strTipoGeneralDTH)))
                {
                    strNameApplication = strNombreAplicacion;
                    strApplication = Claro.SIACU.Constants.PostpaidMajuscule;
                    objCustomerPost = Postpaid.GetOnlyDataCustomer(
                       new CustomerRequest()
                       {
                           TypeSearch = objCustomerRequest.TypeSearch,
                           AccountCustomer = strNumeroCuenta,
                           NumCellphone = strTelefono,
                           Application = strApplication,
                           ApplicationName = strNameApplication,
                           ProductType = strTypeSearchResult,
                           Audit = objCustomerRequest.Audit
                       }).ObjCustomer;

                    if (objCustomerPost != null)
                    {
                        objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Postpaid;

                        if (strTypeSearchResult.Equals(strTipoGeneralDTH))
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Dth.ToString().ToUpper();
                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Postpaid.ToString().ToUpper();
                        }
                    }
                    else
                    {
                        objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                    }

                    objCustomerResponse.InterfaceCustomer = objCustomerPost;
                }
                else
                {
                    if ((strTelefono.Length == 9))
                    {
                        Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest objGetPreviousCustomerRequest = new Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerRequest
                        {
                            Telephone = strTelefono.Trim(),
                            Account = "",
                            ContactId = "",
                            FlagRegistry = Claro.Constants.NumberOneString,
                            ApplicationName = strNameApplication,
                            Audit = objCustomerRequest.Audit
                        };
                        Entity.Dashboard.Prepaid.GetPreviousCustomer.PreviousCustomerResponse objPreviousCustomerResponse = Prepaid.GetOnlyDataCustomer(objGetPreviousCustomerRequest);
                        oCustomerPre = objPreviousCustomerResponse.objCustomer;
                        if (oCustomerPre != null)
                        {
                            oCustomerPre.ApplicationType = Claro.SIACU.ApplicationType.Prepaid;
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Prepaid.ToString().ToUpper();
                        }
                        else
                        {
                            objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                        }
                        objCustomerResponse.InterfaceCustomer = oCustomerPre;
                        objCustomerResponse.CodeResponse = (string.IsNullOrEmpty(objPreviousCustomerResponse.CodeResponse) ? "" : objPreviousCustomerResponse.CodeResponse);
                    }
                }
            }
            else if (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberThreeString)) //tipo de filtro contrato
            {
                strTelefono = strValueSearch;

                if (strTypeSearchResult.Equals(strTipoGeneralHFC) || strTypeSearchResult.Equals(strTipoGeneralLTE) || string.IsNullOrEmpty(strTypeSearchResult))
                {
                    strNameApplication = Claro.SIACU.Constants.HFC;
                    strApplication = Claro.SIACU.Constants.HFC;

                    objCustomerPost = Postpaid.GetOnlyDataCustomer(
                     new CustomerRequest()
                     {
                         TypeSearch = objCustomerRequest.TypeSearch,
                         AccountCustomer = strNumeroCuenta,
                         NumCellphone = strTelefono,
                         Application = strApplication,
                         ApplicationName = strNameApplication,
                         ProductType = strTypeSearchResult,
                         Audit = objCustomerRequest.Audit,
                     }).ObjCustomer;

                    if (objCustomerPost != null)
                    {
                        objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Hfc;
                        objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Hfc.ToString().ToUpper();
                    }
                    else
                    {
                        objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                    }
                    objCustomerResponse.InterfaceCustomer = objCustomerPost;
                }
                else if (strTypeSearchResult.Equals(strTipoGeneralLTE))
                {
                    strNameApplication = Claro.SIACU.Constants.LTE;
                    strApplication = Claro.SIACU.Constants.LTE;

                    objCustomerPost = Postpaid.GetDataCustomer(
                     new CustomerRequest()
                     {
                         TypeSearch = objCustomerRequest.TypeSearch,
                         AccountCustomer = strNumeroCuenta,
                         NumCellphone = strTelefono,
                         Application = strApplication,
                         ApplicationName = strNameApplication,
                         ProductType = strTypeSearchResult,
                         Audit = objCustomerRequest.Audit
                     }).ObjCustomer;
                    if (objCustomerPost != null)
                    {
                        objCustomerPost.ApplicationType = Claro.SIACU.ApplicationType.Lte;
                        objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.Lte.ToString().ToUpper();
                    }
                    else
                    {
                        objCustomerResponse.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();
                    }
                    objCustomerResponse.InterfaceCustomer = objCustomerPost;
                }
                else
                {
                    objCustomerRequest.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();

                    objCustomerResponse.ApplicationType = objCustomerRequest.ApplicationType;
                    objCustomerResponse.InterfaceCustomer = objCustomerPost;
                }

            }

            else
            {
                objCustomerRequest.ApplicationType = Claro.SIACU.ApplicationType.NoPrecisado.ToString().ToUpper();

                objCustomerResponse.ApplicationType = objCustomerRequest.ApplicationType;
                objCustomerResponse.InterfaceCustomer = objCustomerPost;

            }
            return objCustomerResponse;
        }

        public static Entity.Dashboard.Board.CheckingUser.CheckingUserResponse CheckingUser(Entity.Dashboard.Board.CheckingUser.CheckingUserRequest objCheckingUserRequest)
        {

            Entity.Dashboard.Board.CheckingUser.CheckingUserResponse objCheckingUserResponse;

            objCheckingUserResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Board.CheckingUser.CheckingUserResponse>(objCheckingUserRequest.Audit.Session, objCheckingUserRequest.Audit.Transaction, () =>
            {
                return Data.Dashboard.Common.CheckingUser(objCheckingUserRequest.Audit.Transaction, objCheckingUserRequest.IpAplicacion, objCheckingUserRequest.Audit.ApplicationName, objCheckingUserRequest.Usuario, objCheckingUserRequest.AppCod);
            });

            return objCheckingUserResponse;
        }

        public static Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserResponse ReadOptionsByUser(Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserRequest objReadOptionsByUserRequest)
        {

            Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserResponse objReadOptionsByUserResponse = new Entity.Dashboard.Board.ReadOptionsByUser.ReadOptionsByUserResponse();
            objReadOptionsByUserResponse.ListOption = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Prepaid.PaginaOption>>(objReadOptionsByUserRequest.Audit.Session, objReadOptionsByUserRequest.Audit.Transaction, () =>
            {
                return Data.Dashboard.Common.ReadOptionsByUser(objReadOptionsByUserRequest.IdSession, objReadOptionsByUserRequest.Transaction, objReadOptionsByUserRequest.IdAplication, objReadOptionsByUserRequest.IdUser);
            });

            return objReadOptionsByUserResponse;
        }
        public static Entity.Dashboard.Board.GetEmployeByUser.EmployeeResponse GetEmployeByUser(Entity.Dashboard.Board.GetEmployeByUser.EmployeeRequest objEmployeeRequest)
        {

            Entity.Dashboard.Board.GetEmployeByUser.EmployeeResponse objEmployeeResponse = new Entity.Dashboard.Board.GetEmployeByUser.EmployeeResponse();
            objEmployeeResponse.lstEmployee = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Employee>>(objEmployeeRequest.Audit.Session, objEmployeeRequest.Audit.Transaction, () =>
            {
                return Data.Dashboard.Common.GetEmployeByUser(objEmployeeRequest.Audit.Session, objEmployeeRequest.Audit.Transaction, objEmployeeRequest.UserName);
            });

            return objEmployeeResponse;
        }

        public static Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse GetFlagAjuste(Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjsuteRequest objFlagAjsuteRequest)
        {

            Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse objFlagAjusteResponse;
            objFlagAjusteResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetFlagAjuste.FlagAjusteResponse>(objFlagAjsuteRequest.Audit.Session, objFlagAjsuteRequest.Audit.Transaction, () =>
            {
                return Data.Dashboard.Postpaid.GetFlagAjuste(objFlagAjsuteRequest);
            });

            return objFlagAjusteResponse;
        }

        public static string GetParameterByCode(string strIdSession, string strTransaction, int Code)
        {


            string value = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransaction, () =>
            {
                return Data.Dashboard.Postpaid.GetParameterTerminalTPI(strIdSession, strTransaction, Code);
            });

            return value;
        }
    }
}