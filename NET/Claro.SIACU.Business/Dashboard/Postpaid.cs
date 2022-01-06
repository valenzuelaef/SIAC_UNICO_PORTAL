using Claro.SIACU.Entity;
using Claro.SIACU.Entity.Dashboard.Postpaid;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetDataInvoiceAndDetailTobe;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetHistoryDeliveryToBe.Response;
using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetListHistoryIMSI.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using KEY = Claro.ConfigurationManager;
using POSTPAID = Claro.SIACU.Entity.Dashboard.Postpaid;
using System.Data;
using System.Globalization;
using Claro.SIACU.Entity.Dashboard.Postpaid.CancelInvoice;
using Claro.SIACU.Entity.Dashboard.Postpaid.EvaluateAmount;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetUserProfile;
using System.Collections;
using Claro.SIACU.Entity.Dashboard.Postpaid.GetInvoceDetails;


namespace Claro.SIACU.Business.Dashboard
{
    public static class Postpaid
    {
        /// <summary>
        /// Método para obtener los datos del servicio por Id de cliente.
        /// </summary>
        /// <param name="objServiceRequest">Contiene el Id de cliente principalmente.</param>
        /// <returns>Devuelve objeto ServiceResponse con información del servicio.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetServiceByCustomerCode(POSTPAID.GetService.ServiceRequest objServiceRequest)
        {
            List<Claro.SIACU.Entity.Dashboard.ProductType> listDataTypeProduct = null;
            POSTPAID.GetService.ServiceResponse objServiceResponse = new POSTPAID.GetService.ServiceResponse();
            POSTPAID.GetService.ServiceResponse objServiceResponseTOBE = new POSTPAID.GetService.ServiceResponse();
            POSTPAID.GetService.ServiceResponse objServiceResponseASIS = new POSTPAID.GetService.ServiceResponse();
            string strAux = "";

            if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
            {
                if (objServiceRequest.flagConvivencia.Equals(Claro.Constants.NumberOneString))
                {
                    Claro.Web.Logging.Info(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, "Obteniendo la lista TOBE");
                    objServiceResponseTOBE.ListService = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetServiceByCustomerCodeTobe(objServiceRequest.Audit, objServiceRequest.csIdPub); });
                    Claro.Web.Logging.Info(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, "Obteniendo la lista ASIS");
                    objServiceResponseASIS.ListService = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetServiceByCustomerCode(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.CustomerID); });

                    if (objServiceResponseASIS.ListService != null && objServiceResponseASIS.ListService.Count > 0)
                    {
                    foreach (POSTPAID.Service item in objServiceResponseASIS.ListService)
                    {
                        if (item.TIPO_PRODUCTO.Equals(Claro.SIACU.Constants.Post))
                        {
                            listDataTypeProduct = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.ProductType>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () =>
                            {
                                return Data.Dashboard.Common.GetTypeProduct(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, item.NRO_CELULAR, item.CONTRATO_ID, ref strAux);
                            });

                            if (listDataTypeProduct != null && listDataTypeProduct.Count > 0)
                            {
                                foreach (Claro.SIACU.Entity.Dashboard.ProductType productType in listDataTypeProduct)
                                {
                                    if (!string.IsNullOrEmpty(productType.TIPO_PRODUCTO))
                                    {
                                        if (productType.TIPO_PRODUCTO.Equals(ConfigurationManager.AppSettings("strTipoProductoInternet")))
                                        {
                                            item.TIPO_PRODUCTO = ConfigurationManager.AppSettings("strTipoGeneralInternet");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    }
                    Claro.Web.Logging.Info(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, "Inicializando la lista Response");
                    objServiceResponse.ListService = new List<POSTPAID.Service>();
                    Claro.Web.Logging.Info(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, "Agregando a la lista Response la lista TOBE");
                    objServiceResponse.ListService.AddRange(objServiceResponseTOBE.ListService);

                    if (objServiceResponseASIS.ListService != null && objServiceResponseASIS.ListService.Count > 0)
                    {
                    Claro.Web.Logging.Info(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, "Agregando a la lista Response la lista ASIS");
                    objServiceResponse.ListService.AddRange(objServiceResponseASIS.ListService);
                }
                }
                else
                {
                objServiceResponse.ListService = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetServiceByCustomerCodeTobe(objServiceRequest.Audit, objServiceRequest.csIdPub); });
                }
            }
            else
            {
                objServiceResponse.ListService = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetServiceByCustomerCode(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.CustomerID); });

                foreach (POSTPAID.Service item in objServiceResponse.ListService)
                {
                    if (item.TIPO_PRODUCTO.Equals(Claro.SIACU.Constants.Post))
                    {
                        listDataTypeProduct = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.ProductType>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () =>
                        {
                            return Data.Dashboard.Common.GetTypeProduct(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, item.NRO_CELULAR, item.CONTRATO_ID, ref strAux);
                        });

                        if (listDataTypeProduct != null && listDataTypeProduct.Count > 0)
                        {
                            foreach (Claro.SIACU.Entity.Dashboard.ProductType productType in listDataTypeProduct)
                            {
                                if (!string.IsNullOrEmpty(productType.TIPO_PRODUCTO))
                                {
                                    if (productType.TIPO_PRODUCTO.Equals(ConfigurationManager.AppSettings("strTipoProductoInternet")))
                                    {
                                        item.TIPO_PRODUCTO = ConfigurationManager.AppSettings("strTipoGeneralInternet");
                                    }
                                }
                            }
                        }
                    }
                }
            }





            return objServiceResponse;
        }


        /// <summary>
        /// Método para obtener los datos del cliente.
        /// </summary>
        /// <param name="objCustomerRequest">Contiene tipo de búsqueda, cuenta de cliente, número de celular y aplicación.</param>
        /// <returns>Devuelve objeto CustomerResponse con información del cliente.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse GetDataCustomer(POSTPAID.GetCustomer.CustomerRequest objCustomerRequest)
        {

            string CODE_PRODUCT_FIXE = KEY.AppSettings("strTipoGeneralFIJA").Split('|')[1].ToString();
            string OPTIONS_PRODUCT_FIXE = KEY.AppSettings("strTipoGeneralFIJA").Split('|')[2].ToString();


            POSTPAID.GetCustomer.CustomerResponse objCustomerResponse = new POSTPAID.GetCustomer.CustomerResponse();
            StringBuilder sb = new StringBuilder();
            if (objCustomerRequest.Application.Equals(Claro.SIACU.Constants.PostpaidMajuscule) || objCustomerRequest.Application.Equals(Claro.SIACU.Constants.DTH))
            {
                Claro.Web.Logging.Info("ONE-FIJA", "ONE-FIJA", "GetDataCustomer POST");
                objCustomerResponse.ObjCustomer = GetDataCustomerPost(objCustomerRequest);
                if (objCustomerResponse.ObjCustomer != null)
                {
                    objCustomerResponse.ObjCustomer.Application = objCustomerRequest.Application;
                    objCustomerResponse.ObjCustomer.ValueSearch = objCustomerRequest.NumCellphone;
                }
            }
            else if (objCustomerRequest.Application.Equals(Claro.SIACU.Constants.HFC) || objCustomerRequest.Application.Equals(Claro.SIACU.Constants.LTE) || objCustomerRequest.Application.Equals(CODE_PRODUCT_FIXE))
            {
                Claro.Web.Logging.Info("ONE-FIJA", "ONE-FIJA", "GetDataCustomer HFC");
                objCustomerResponse.ObjCustomer = GetDataCustomerHFC(objCustomerRequest);
                if (objCustomerResponse.ObjCustomer != null)
                {
                    objCustomerResponse.ObjCustomer.Application = objCustomerRequest.Application;
                    objCustomerResponse.ObjCustomer.EsServicioLTE = objCustomerRequest.Application.Equals(Claro.SIACU.Constants.LTE);
                }
            }
            if (objCustomerRequest.ProductType.Equals(Claro.SIACU.Constants.Mobile))
            {
                objCustomerRequest.ProductType = Claro.SIACU.Constants.MobilePhone;
            }
            else if (objCustomerRequest.ProductType.Equals(Claro.Constants.NumberZeroString))
            {
                objCustomerRequest.ProductType = "";
            }
            if (objCustomerResponse.ObjCustomer != null)
            {
                objCustomerResponse.ObjCustomer.TipoProducto = sb.Append(!objCustomerRequest.ProductType.Equals("") ? Claro.SIACU.Constants.PostpaidSpanish_ : Claro.SIACU.Constants.PostpaidSpanish).Append(objCustomerRequest.ProductType).ToString();
            }
            return objCustomerResponse;
        }

        /// <summary>
        /// Método para obtener la dirección del cliente.
        /// </summary>
        /// <param name="objOfficeAddressRequest">Contiene tipo y número de documento de identidad</param>
        /// <returns>Devuelve objeto OfficeAddressResponse con información de la dirección del cliente.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetOfficeAddress.OfficeAddressResponse GetAddressOfficce(POSTPAID.GetOfficeAddress.OfficeAddressRequest objOfficeAddressRequest)
        {
            POSTPAID.GetOfficeAddress.OfficeAddressResponse objOfficeAddressResponse = new POSTPAID.GetOfficeAddress.OfficeAddressResponse();

            objOfficeAddressResponse.ObjOfficeAddress = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.OfficeAddress>(objOfficeAddressRequest.Audit.Session, objOfficeAddressRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetAddressOfficce(objOfficeAddressRequest.Audit.Session, objOfficeAddressRequest.Audit.Transaction, objOfficeAddressRequest.DocumentType, objOfficeAddressRequest.DocumentNumber); });


            if (objOfficeAddressResponse.ObjOfficeAddress == null)
            {
                objOfficeAddressResponse.ObjOfficeAddress = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.OfficeAddress>(objOfficeAddressRequest.Audit.Session, objOfficeAddressRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetAddressOfficeRfc(objOfficeAddressRequest.DocumentNumber); });
            }

            return objOfficeAddressResponse;
        }

        /// <summary>
        /// Método para obtener la información de la factura.
        /// </summary>
        /// <param name="objReceiptRequest">Contiene código de cliente</param>
        /// <returns>Devuelve objeto ReceiptResponse con información de la factura.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetReceipt.ReceiptResponse GetDataInvoice(POSTPAID.GetReceipt.ReceiptRequest objReceiptRequest, out bool isEnvioMail)
        {
            string strValidaCorreo = "";
            bool xisEnvioMail = false;
            DataInvoiceAndDetailTobe dataInvoiceAndDetailTobe = null;
            POSTPAID.GetReceipt.ReceiptResponse objReceiptResponse = new POSTPAID.GetReceipt.ReceiptResponse();
            bool esMigrado = false;
            POSTPAID.GetReceipt.ReceiptResponse pivotObjectResponse = new POSTPAID.GetReceipt.ReceiptResponse();

            try
            {

                if (objReceiptRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE))
                {

                    dataInvoiceAndDetailTobe = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetDataInvoiceAndDetailTobe.DataInvoiceAndDetailTobe>(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDataInvoiceAndDetailTobe(objReceiptRequest.Audit, objReceiptRequest.csIdPub, objReceiptRequest.InvoiceNumber); });

                    Claro.Web.Logging.Info(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, "Llenamos el objeto objReceiptResponse");
                    objReceiptResponse.ObjReceipt = (dataInvoiceAndDetailTobe != null) ? dataInvoiceAndDetailTobe.receipt : null;
                    Claro.Web.Logging.Info(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, "Llenamos el objeto pivotObjectResponse");
                    pivotObjectResponse.ObjReceipt = (dataInvoiceAndDetailTobe != null) ? dataInvoiceAndDetailTobe.receipt : null;

                    if (objReceiptResponse.ObjReceipt != null)
                    {
                        if (objReceiptResponse.ObjReceipt.NUMERO_RECIBO.Contains("IN"))
                        {
                            Claro.Web.Logging.Info(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, "Factura tiene IN");
                            objReceiptResponse.ObjReceipt = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Receipt>(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDataInvoice(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptRequest.CustomerCode); });
                            esMigrado = true;
                            if (objReceiptResponse.ObjReceipt == null)
                            {
                                Claro.Web.Logging.Info(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, "Valida nulidad");
                                objReceiptResponse.ObjReceipt = pivotObjectResponse.ObjReceipt;
                                Claro.Web.Logging.Info(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, "Devolvemos valor anterior.");
                                esMigrado = false;
                            }
                        }
                    }

                }
                if (objReceiptRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS))
                {
                    objReceiptResponse.ObjReceipt = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Receipt>(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDataInvoice(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptRequest.CustomerCode); });
                }


                if (objReceiptResponse.ObjReceipt != null)
                {
                    Claro.Web.Logging.Info(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, "Objeto es diferente de null");

                    if (!String.IsNullOrEmpty(objReceiptRequest.InvoiceNumber)) objReceiptResponse.ObjReceipt.NUMERO_RECIBO = objReceiptRequest.InvoiceNumber;

                    if (objReceiptRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS))
                    {
                        objReceiptResponse.ObjReceipt.RECIBO_DETALLADO = Claro.Web.Logging.ExecuteMethod<DetailReceipt>(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailInvoice(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptResponse.ObjReceipt.NUMERO_RECIBO); });
                    }

                    if (objReceiptRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE))
                    {
                        if (esMigrado)
                        {
                            objReceiptResponse.ObjReceipt.RECIBO_DETALLADO = Claro.Web.Logging.ExecuteMethod<DetailReceipt>(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailInvoice(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptResponse.ObjReceipt.NUMERO_RECIBO); });
                        }
                        else
                        {
                            objReceiptResponse.ObjReceipt.RECIBO_DETALLADO = objReceiptResponse.ObjReceipt.RECIBO_DETALLADO;
                        }
                    }


                    if (objReceiptResponse.ObjReceipt.NUMERO_RECIBO.Length > 0)
                    {

                        string NRECIBO1 = objReceiptResponse.ObjReceipt.NUMERO_RECIBO.Substring(0, 4);

                        string NRECIBO2 = objReceiptResponse.ObjReceipt.NUMERO_RECIBO.Substring(4, objReceiptResponse.ObjReceipt.NUMERO_RECIBO.Length - 4);
                        string NRECIBO = NRECIBO1 + "-" + NRECIBO2;
                        Claro.Web.Logging.Error(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, "Recibo: " + NRECIBO);

                        if (objReceiptRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE))
                        {
                            if (esMigrado)
                            {
                                objReceiptResponse.ObjReceipt.INVOICENUMBER = Helper.GetNumberReceipt(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptResponse.ObjReceipt.NUMERO_RECIBO, objReceiptResponse.ObjReceipt.FECHA_EMISION);
                            }
                            else
                            {
                                objReceiptResponse.ObjReceipt.INVOICENUMBER = NRECIBO;
                            }
                        }
                        else
                        {
                            objReceiptResponse.ObjReceipt.INVOICENUMBER = Helper.GetNumberReceipt(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptResponse.ObjReceipt.NUMERO_RECIBO, objReceiptResponse.ObjReceipt.FECHA_EMISION);
                        }
                        objReceiptResponse.ObjReceipt.FECHA_VENCIMIENTO = objReceiptResponse.ObjReceipt.FECHA_VENCIMIENTO.Substring(0, 10);
                    }

                    esMigrado = false;

                    if (objReceiptRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS) && (objReceiptRequest.bmIdPub.Equals(Claro.SIACU.Constants.NullString) || string.IsNullOrEmpty(objReceiptRequest.bmIdPub)))
                {
                    strValidaCorreo = Claro.Web.Logging.ExecuteMethod<string>(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.ValidateMail(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, objReceiptRequest.CustomerCode); });
                    xisEnvioMail = strValidaCorreo.ToUpper(CultureInfo.InvariantCulture).Equals(Claro.Constants.LetterA);
                    Claro.Web.Logging.Info(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, "Envío ASIS");
                }
                else
                {
                    if (objReceiptRequest.bmIdPub.Equals(Claro.SIACU.Constants.MREC, StringComparison.InvariantCultureIgnoreCase) || objReceiptRequest.bmIdPub.Equals(Claro.SIACU.Constants.PMREC, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Claro.Web.Logging.Info(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, "Envío correo TOBE true");
                        xisEnvioMail = true;
                    }
                    else
                    {
                        Claro.Web.Logging.Info(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, "Envío correo TOBE false");
                        xisEnvioMail = false;
                    }

                }
                }


            }
            catch (Exception ex)
            {
                xisEnvioMail = false;
                Claro.Web.Logging.Error(objReceiptRequest.Audit.Session, objReceiptRequest.Audit.Transaction, ex.Message);
            }
            isEnvioMail = xisEnvioMail;
            return objReceiptResponse;
        }

        /// <summary>
        /// Método para obtener la información de la línea o servicio.
        /// </summary>
        /// <param name="objServiceRequest">Contiene id de contrato, teléfono, tipo de cliente, tipo y número de documento de identidad.</param>
        /// <returns>Devuelve objeto ServiceResponse con información de la línea.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetDataServiceLine(POSTPAID.GetService.ServiceRequest objServiceRequest)
        {
            //roll 13

            Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut.outTypeProductDat outOptional = null;
            string plataforma = objServiceRequest.plataformaAT;
            POSTPAID.GetService.ServiceResponse objServiceResponse = new POSTPAID.GetService.ServiceResponse();
            string Contract = objServiceRequest.ContractID;
            string TypeSherach = "3";
            string msj = "";
            string flag = "";
            string typeProduct = Claro.SIACU.Business.Dashboard.Dashboard.GetApplicationType(objServiceRequest.Audit, ref TypeSherach, ref Contract, ref flag, ref msj, ref plataforma, out outOptional);

            string CODE_PRODUCT_FIXE = KEY.AppSettings("strTipoGeneralFIJA").Split('|')[1].ToString();
            string OPTIONS_PRODUCT_FIXE = KEY.AppSettings("strTipoGeneralFIJA").Split('|')[2].ToString();

            if (objServiceRequest.Application.Equals(Claro.SIACU.Constants.PostpaidMajuscule) || objServiceRequest.Application.Equals(Claro.SIACU.Constants.DTH))
            {
                objServiceResponse.ObjService = GetDataServiceLinePost(objServiceRequest);
            }
            else if (objServiceRequest.Application.Equals(Claro.SIACU.Constants.HFC) || objServiceRequest.Application.Equals(CODE_PRODUCT_FIXE))
            {
                //INICIATIVA 616
                objServiceResponse.ObjService = GetDataServiceLineHFC(objServiceRequest);
                //objServiceResponse.ObjService = GetDataServiceLineHFC(objServiceRequest.Audit.Session, objServiceRequest.ContractID, objServiceRequest.Audit.Transaction, objServiceRequest.IpApplication, objServiceRequest.ApplicationName, objServiceRequest.UserApplication);

            }
            else if (objServiceRequest.Application.Equals(Claro.SIACU.Constants.LTE))
            {
                objServiceResponse.ObjService = GetDataServiceLineLTE(objServiceRequest.Audit.Session, objServiceRequest.ContractID, objServiceRequest.Audit.Transaction, objServiceRequest.IpApplication, objServiceRequest.ApplicationName, objServiceRequest.UserApplication);
            }
            if (objServiceResponse.ObjService != null)
            {
                objServiceResponse.ObjService.Application = objServiceRequest.Application;
                if (typeProduct.Equals(Claro.SIACU.Constants.Mobile))
                {
                    typeProduct = Claro.SIACU.Constants.MobilePhone;
                }
                else if (typeProduct.Equals(Claro.Constants.NumberZeroString))
                {
                    typeProduct = "";
                }
                StringBuilder sb = new StringBuilder();
                typeProduct = sb.Append(!typeProduct.Equals("") ? Claro.SIACU.Constants.PostpaidSpanish_ : Claro.SIACU.Constants.PostpaidSpanish).Append(typeProduct).ToString();
                objServiceResponse.ObjService.TypeProductText = typeProduct;
            }
            return objServiceResponse;
        }

        /// <summary>
        /// Método para obtener el número de teléfono por código de contrato.
        /// </summary>
        /// <param name="objServiceRequest">Contiene id de contrato</param>
        /// <returns>Devuelve objeto ServiceResponse con información del servicio.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetTelephoneByContractCode(POSTPAID.GetService.ServiceRequest objServiceRequest)
        {
            POSTPAID.GetService.ServiceResponse objServiceResponse = null;
            if (objServiceRequest.Application.Equals(Claro.SIACU.Constants.LTE))
            {
                objServiceResponse = new POSTPAID.GetService.ServiceResponse()
                {
                    ListService = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTelephoneByContractCodeLTE(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.IpApplication, objServiceRequest.ApplicationName, objServiceRequest.UserApplication, objServiceRequest.ContractID); })
                };
            }
            else if (objServiceRequest.Application.Equals(Claro.SIACU.Constants.HFC))
            {
                if(objServiceRequest.plataformaAT == "TOBE")
                {
                    objServiceResponse = new POSTPAID.GetService.ServiceResponse()
                    {
                        ListService = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTelephoneByContractCodeHFCTOBE(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.IpApplication, objServiceRequest.ApplicationName, objServiceRequest.UserApplication, objServiceRequest.ContractID); })
                    };
                }
                else
                {
                    objServiceResponse = new POSTPAID.GetService.ServiceResponse()
                    {
                        ListService = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTelephoneByContractCodeHFC(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.IpApplication, objServiceRequest.ApplicationName, objServiceRequest.UserApplication, objServiceRequest.ContractID); })
                    };
                }

                
            }
            return objServiceResponse;
        }

        /// <summary>
        /// Método para obtener las líneas desactivas por código de contrato.
        /// </summary>
        /// <param name="objServiceRequest">Contiene id de contrato.</param>
        /// <returns>Devuelve objeto ServiceResponse con información del servicio.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetLineDisableByContractCode(POSTPAID.GetService.ServiceRequest objServiceRequest)
        {
            string CODE_PRODUCT_FIXE = KEY.AppSettings("strTipoGeneralFIJA").Split('|')[1].ToString();
            string OPTIONS_PRODUCT_FIXE = KEY.AppSettings("strTipoGeneralFIJA").Split('|')[2].ToString();

            POSTPAID.GetService.ServiceResponse objServiceResponse = null;
            if (objServiceRequest.Application.Equals(Claro.SIACU.Constants.LTE))
            {
                objServiceResponse = new POSTPAID.GetService.ServiceResponse()
                {
                    ListService = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetLineByContractCodeLTE(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.IpApplication, objServiceRequest.ApplicationName, objServiceRequest.UserApplication, objServiceRequest.ContractID); })

                };
            }
            else if (objServiceRequest.Application.Equals(Claro.SIACU.Constants.HFC) || objServiceRequest.Application.Equals(CODE_PRODUCT_FIXE))
            {
                objServiceResponse = new POSTPAID.GetService.ServiceResponse()
                {
                    ListService = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetLineByContractCodeHFC(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.IpApplication, objServiceRequest.ApplicationName, objServiceRequest.UserApplication, objServiceRequest.ContractID); })

                };
            }
            return objServiceResponse;
        }

        /// <summary>
        /// Método para obtener la dirección de instalación del cliente.
        /// </summary>
        /// <param name="objCustomerRequest">Contiene número de celular.</param>
        /// <returns>Devuelve objeto CustomerResponse con información del cliente. </returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse GetInstallationAddress(POSTPAID.GetCustomer.CustomerRequest objCustomerRequest)
        {
            POSTPAID.GetCustomer.CustomerResponse objCustomerResponse = new POSTPAID.GetCustomer.CustomerResponse()
            {
                ObjCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Customer>(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetInstallationAddress(objCustomerRequest.Audit.Session, objCustomerRequest.Audit.Transaction, objCustomerRequest.NumCellphone); })

            };
            objCustomerResponse.ObjCustomer.PAIS = KEY.AppSettings("strCountry");
            return objCustomerResponse;
        }

        /// <summary>
        /// Método para obtener el histórico de la línea o servicio.
        /// </summary>
        /// <param name="objServiceRequest">Contiene el id de contrato</param>
        /// <returns>Devuelve objeto ServiceResponse con información histórica de la línea.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetHistoryServiceLine(POSTPAID.GetService.ServiceRequest objServiceRequest)
        {
            POSTPAID.GetService.ServiceResponse objServiceResponse = new POSTPAID.GetService.ServiceResponse();

            List<Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Common.listaOpcional> listaOpcional = new List<Entity.Dashboard.Postpaid.Legacy.GetHistoryServiceLine.Common.listaOpcional>();

            // Estructura para la lista opcional.
            listaOpcional.Add(new POSTPAID.Legacy.GetHistoryServiceLine.Common.listaOpcional()
            {
                clave = KEY.AppSettings("KeyFlagCbio"),
                valor = objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE) ? Claro.Constants.NumberOneString : Claro.Constants.NumberZeroString
            });

            listaOpcional.Add(new POSTPAID.Legacy.GetHistoryServiceLine.Common.listaOpcional()
            {
                clave = KEY.AppSettings("KeyFlagMigrado"),
                valor = objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE) ? objServiceRequest.flagConvivencia : Claro.Constants.NumberZeroString
            });


            objServiceResponse.ListService = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetHistoryServiceLineTobe(objServiceRequest.Audit, objServiceRequest.coId, listaOpcional); });

            return objServiceResponse;
        }

        /// <summary>
        /// Método para obtener los días activos y desactivos del contrato.
        /// </summary>
        /// <param name="objActiveDaysRequest">Contiene el id de contrato.</param>
        /// <returns>Devuelve objeto ActiveDaysResponse con información de días activos y desactivos.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetActiveDays.ActiveDaysResponse GetActiveDisableDays(POSTPAID.GetActiveDays.ActiveDaysRequest objActiveDaysRequest)
        {
            POSTPAID.GetActiveDays.ActiveDaysResponse objActiveDaysResponse = new POSTPAID.GetActiveDays.ActiveDaysResponse();
            int intDaysActive = Claro.Constants.NumberZero;
            int intDaysDisable = Claro.Constants.NumberZero;
            int intresul = Claro.Constants.NumberZero;
            string strMessageError = "";
            string strMessage = "";
            Claro.Web.Logging.ExecuteMethod<bool>(objActiveDaysRequest.Audit.Session, objActiveDaysRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetActiveDisableDays(objActiveDaysRequest.Audit.Session, objActiveDaysRequest.Audit.Transaction, objActiveDaysRequest.ContratoID, out intDaysActive, out intDaysDisable, out intresul, out strMessageError); });

            objActiveDaysResponse.ActiveDays = intDaysActive;
            objActiveDaysResponse.DisableDays = intDaysDisable;
            objActiveDaysResponse.Result = intresul;
            objActiveDaysResponse.MessageError = strMessageError;
            try
            {
                strMessage = Claro.Web.Logging.ExecuteMethod<string>(objActiveDaysRequest.Audit.Session, objActiveDaysRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetParameter(objActiveDaysRequest.Audit.Session, objActiveDaysRequest.Audit.Transaction, KEY.AppSettings("gParamMsjeDiasA")); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objActiveDaysRequest.Audit.Session, objActiveDaysRequest.Audit.Transaction, ex.Message);
            }
            objActiveDaysResponse.Message = strMessage;
            return objActiveDaysResponse;
        }

        /// <summary>
        /// Método para obtener el historial de SIMs del servicio.
        /// </summary>
        /// <param name="objHistorySIMRequest">Contiene id de contrato y número de teléfono.</param>
        /// <returns>Devuelve objeto HistorySIMResponse con información de SIMs históricos.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetHistorySIM.HistorySIMResponse GetHistorySIM(POSTPAID.GetHistorySIM.HistorySIMRequest objHistorySIMRequest)
        {
            string strResponseCode;
            string strResponse;


            POSTPAID.GetHistorySIM.HistorySIMResponse objHistorySIMResponse = new POSTPAID.GetHistorySIM.HistorySIMResponse()
            {
                ListHistorySIM = Claro.Web.Logging.ExecuteMethod<List<HistorySIM>>(objHistorySIMRequest.Audit.Session, objHistorySIMRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetHistorySIMTobe(objHistorySIMRequest.Audit, objHistorySIMRequest.ContractID, objHistorySIMRequest.Telephone, objHistorySIMRequest.strPlataforma, objHistorySIMRequest.strFechaMigracion, objHistorySIMRequest.flagconvivencia, out strResponseCode, out strResponse);
                })

            };

            return objHistorySIMResponse;
        }

        /// <summary>
        /// Método para obtener el historial de equipos del cliente.
        /// </summary>
        /// <param name="objDecoRequest">Contiene id de contrato e id de cliente.</param>
        /// <returns>Devuelve objeto DecoResponse con información de equipos del cliente.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoryEquipments.DecoResponse GetHistoryEquipments(POSTPAID.GetHistoryEquipments.DecoRequest objDecoRequest)
        {
            string strTextoRetornoRegistrosConsultaEquipos = "";
            string strTextoNoRetornoRegistrosConsultaEquipos = "";
            string strMsgTituloConEquipoLinea = "";
            try
            {
                strTextoRetornoRegistrosConsultaEquipos = KEY.AppSettings("strTextoRetornoRegistrosConsultaEquipos");
                strTextoNoRetornoRegistrosConsultaEquipos = KEY.AppSettings("strTextoNoRetornoRegistrosConsultaEquipos");
                strMsgTituloConEquipoLinea = KEY.AppSettings("strMsgTituloConEquipoLinea");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDecoRequest.Audit.Session, objDecoRequest.Audit.Transaction, ex.Message);
            }

            POSTPAID.GetHistoryEquipments.DecoResponse objDecoResponse = new POSTPAID.GetHistoryEquipments.DecoResponse();
            if (objDecoRequest.Application.Equals(Claro.SIACU.Constants.LTE))
            {
                objDecoResponse.ListDeco = Claro.Web.Logging.ExecuteMethod<List<Deco>>(objDecoRequest.Audit.Session, objDecoRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetServiceLineLTE(objDecoRequest.Audit.Session, objDecoRequest.Audit.Transaction, objDecoRequest.ContractID, objDecoRequest.CustomerID); });

            }
            else
            {
                objDecoResponse.ListDeco = Claro.Web.Logging.ExecuteMethod<List<Deco>>(objDecoRequest.Audit.Session, objDecoRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetServiceLineDTH(objDecoRequest.Audit.Session, objDecoRequest.Audit.Transaction, objDecoRequest.ContractID, objDecoRequest.CustomerID); });

            }
            if (objDecoResponse.ListDeco != null && objDecoResponse.ListDeco.Count > 0)
            {
                objDecoResponse.Descripcion = strTextoRetornoRegistrosConsultaEquipos;
            }
            else
            {
                objDecoResponse.Descripcion = strTextoNoRetornoRegistrosConsultaEquipos;
            }
            objDecoResponse.Title = strMsgTituloConEquipoLinea;
            return objDecoResponse;
        }

        /// <summary>
        /// Método para consultar IMR.
        /// </summary>
        /// <param name="objIMRRequest">Contiene id de cliente, cuenta, ciclo de facturación, tipo de producto, tipo de cliente, segmento e id de contrato.</param>
        /// <returns>Devuelve objeto IMRResponse con información IMR.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult.IMRResponse GetIMRConsult(POSTPAID.GetIMRConsult.IMRRequest objIMRRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult.IMRResponse objIMRResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetIMRConsult.IMRResponse();
            string strIMRTotalAmount, ExpirationDate, strIMRAmount = "";
            bool isImr;

            isImr = Claro.Web.Logging.ExecuteMethod<bool>(objIMRRequest.Audit.Session, objIMRRequest.Audit.Transaction, () =>
            {
                return Data.Dashboard.Postpaid.GetIMRConsult(objIMRRequest.Audit, objIMRRequest.Msisdn, objIMRRequest.CustomerId, objIMRRequest.Account,
                    objIMRRequest.BillingCycle, objIMRRequest.ProductType, objIMRRequest.CustomerType,
                    objIMRRequest.Segment, objIMRRequest.ContractId,
                    out strIMRTotalAmount, out strIMRAmount, out ExpirationDate);
            });



            objIMRResponse.IMRAmount = isImr ? Claro.SIACU.Constants.SymbolSun + strIMRAmount : Claro.Constants.FormatSpace;
            return objIMRResponse;
        }

        /// <summary>
        /// Método para obtener los datos del contacto.
        /// </summary>
        /// <param name="objContactRequest">Contiene id de cliente, código de cliente, código solin y código CSCTN.</param>
        /// <returns>Devuelce objeto ContactResponse con información del contacto.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetContact.ContactResponse GetContact(Claro.SIACU.Entity.Dashboard.Postpaid.GetContact.ContactRequest objContactRequest)
        {
            string strConstParameter = "";
            POSTPAID.GetContact.ContactResponse objContactResponse = null;
            List<POSTPAID.AdditionalFields> lstAdditional = null;

            try
            {
                strConstParameter = ConfigurationManager.AppSettings("const_ParametroEmailsDisponibles");
                objContactResponse = new POSTPAID.GetContact.ContactResponse()
                {
                    AvailableEmails = Data.Dashboard.Postpaid.AvailableEmails(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction, int.Parse(strConstParameter)),
                    DocumentTypeDNI = KEY.AppSettings("constCodTipoDocumentoDNI"),
                    DocumentTypeRUC = KEY.AppSettings("constCodTipoDocumentoRUC"),
                    DocumentTypePAS = KEY.AppSettings("constCodTipoDocumentoPAS"),
                    DocumentTypeCEX = KEY.AppSettings("constCodTipoDocumentoCEX"),
                    DocumentTypeCIP = KEY.AppSettings("constCodTipoDocumentoCIP"),
                    DocumentTypeCFA = KEY.AppSettings("constCodTipoDocumentoCFA"),
                    NewState = KEY.AppSettings("ContactoNuevo"),
                    ModifyState = KEY.AppSettings("ContactoModificar"),
                    ConsultState = KEY.AppSettings("ContactoConsulta"),
                    ConsultConst = KEY.AppSettings("ConsultConst"),
                    AddConst = KEY.AppSettings("AddConst"),
                    EditConst = KEY.AppSettings("EditConst"),
                    AddEditConst = KEY.AppSettings("AddEditConst"),
                    lstContactType = Claro.Web.Logging.ExecuteMethod<List<ContactType>>(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetContactType(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction); }),
                    lstContactFields = Claro.Web.Logging.ExecuteMethod<List<ContactTypeField>>(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetContactTypeField(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction); }),
                    lstDocumentType = Claro.Web.Logging.ExecuteMethod<List<DocumentType>>(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDocumentType(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction, objContactRequest.DocumentCode); }),
                    lstDataType = Claro.Web.Logging.ExecuteMethod<List<DataType>>(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDataTypeList(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction, objContactRequest.Type); }),
                    lstContact = Claro.Web.Logging.ExecuteMethod<List<Contact>>(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetContactList(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction, objContactRequest.CustomerID, objContactRequest.CustomerCode, objContactRequest.SolinCode, objContactRequest.CSCTNCode); }),
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objContactRequest.Audit.Session, objContactRequest.Audit.Transaction, ex.Message);
            }

            if (objContactResponse != null && objContactResponse.lstContact != null)
            {
                foreach (var item in objContactResponse.lstContact)
                {
                    string strXML = "<?xml version='1.0' encoding='utf-8' ?>";
                    strXML = strXML + item.CAMPOSADICIONALES;
                    lstAdditional = new List<POSTPAID.AdditionalFields>();
                    System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
                    XmlDoc.LoadXml(strXML);
                    System.Xml.XmlNodeList Nodo;
                    string strListaContactoXML = ConfigurationManager.AppSettings("ListaContactoXML");
                    Nodo = XmlDoc.SelectNodes("//" + strListaContactoXML.Trim());

                    for (int i = 0; i <= Nodo.Count - 1; i++)
                    {
                        POSTPAID.AdditionalFields objValor = new POSTPAID.AdditionalFields();
                        objValor.TCCVN_CODIGO = null;
                        objValor.SPERN_CODIGO = null;
                        if (Nodo.Item(i).ChildNodes[0] == null)
                        {
                            objValor.TCXCN_CODIGO = Claro.Constants.FormatSpace;
                        }
                        else
                        {
                            objValor.TCXCN_CODIGO = Nodo.Item(i).ChildNodes[0].InnerText.Trim();
                        }
                        if (Nodo.Item(i).ChildNodes[1] == null)
                        {
                            objValor.TCCCN_CODIGO = Claro.Constants.FormatSpace;
                        }
                        else
                        {
                            objValor.TCCCN_CODIGO = Nodo.Item(i).ChildNodes[1].InnerText.Trim();
                        }
                        if (Nodo.Item(i).ChildNodes[Claro.Constants.NumberTwo] == null)
                        {
                            objValor.TCCVV_VALOR = Claro.Constants.FormatSpace;
                        }
                        else
                        {
                            objValor.TCCVV_VALOR = Nodo.Item(i).ChildNodes[Claro.Constants.NumberTwo].InnerText.Trim();
                        }
                        lstAdditional.Add(objValor);
                    }
                    item.lstAdditional = lstAdditional;
                }
            }
            return objContactResponse;
        }

        /// <summary>
        /// Método para obtener la configuración de columnas.
        /// </summary>
        /// <param name="objContactTypeByFieldRequest">Contiene código.</param>
        /// <returns>Devuelve objeto ContactTypeByFieldResponse con información de configuración de columnas.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField.ContactTypeByFieldResponse GetColumnsConfiguration(Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField.ContactTypeByFieldRequest objContactTypeByFieldRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetContactTypeByField.ContactTypeByFieldResponse objContactResponse = new Entity.Dashboard.Postpaid.GetContactTypeByField.ContactTypeByFieldResponse()
            {
                lstContactTypeByField = Claro.Web.Logging.ExecuteMethod<List<ContactTypeByField>>(objContactTypeByFieldRequest.Audit.Session, objContactTypeByFieldRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetColumnsConfiguration(objContactTypeByFieldRequest.Audit.Session, objContactTypeByFieldRequest.Audit.Transaction, objContactTypeByFieldRequest.Code); })

            };
            return objContactResponse;
        }

        /// <summary>
        /// Método para guardar o eliminar contacto.
        /// </summary>
        /// <param name="objContactSaveRequest">Contiene id de customer, cuenta, teléfono, indicador de guardar o eliminar. </param>
        /// <returns>Devuelve objeto ContactSaveResponse con información del guardado del contacto.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave.ContactSaveResponse ContactSave(Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave.ContactSaveRequest objContactSaveRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave.ContactSaveResponse objContactSaveResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.ContactSave.ContactSaveResponse();
            string ResponseCode = string.Empty;
            if (((!string.IsNullOrEmpty(objContactSaveRequest.Save))) && (string.IsNullOrEmpty(objContactSaveRequest.Delete)))
            {
                objContactSaveResponse.ResponseMessage = Claro.Web.Logging.ExecuteMethod<string>(objContactSaveRequest.Audit.Session, objContactSaveRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.ContactSave(objContactSaveRequest.Audit.Session, objContactSaveRequest.Audit.Transaction, objContactSaveRequest.Audit.UserName, objContactSaveRequest.Account, objContactSaveRequest.Telephone, objContactSaveRequest.Save, null, out ResponseCode); });
                objContactSaveResponse.ResponseCode = ResponseCode;

            }
            else if ((string.IsNullOrEmpty(objContactSaveRequest.Save)) && (!string.IsNullOrEmpty(objContactSaveRequest.Delete)))
            {
                objContactSaveResponse.ResponseMessage = Claro.Web.Logging.ExecuteMethod<string>(objContactSaveRequest.Audit.Session, objContactSaveRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.ContactSave(objContactSaveRequest.Audit.Session, objContactSaveRequest.Audit.Transaction, objContactSaveRequest.Audit.UserName, objContactSaveRequest.Account, objContactSaveRequest.Telephone, null, objContactSaveRequest.Delete, out ResponseCode); });
                objContactSaveResponse.ResponseCode = ResponseCode;
            }
            return objContactSaveResponse;
        }

        /// <summary>
        /// Método para obtener los datos del cliente postpago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strSearchType">Tipo de búsqueda</param>
        /// <param name="strAccountCustomer">Cuenta de cliente</param>
        /// <param name="strNroCellphone">Número de celular</param>
        /// <param name="strApplication">Aplicación</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <returns>Devuelve objeto Customer con información del cliente.</returns>
        private static Entity.Dashboard.Postpaid.Customer GetDataCustomerPost(POSTPAID.GetCustomer.CustomerRequest objCustomerRequest)
        {
            List<POSTPAID.Legacy.GetDataCustomer.Common.listaOpcional> listOptional = null;
            Entity.Dashboard.Postpaid.Customer oCustomer = null;
            Entity.Dashboard.Postpaid.Service objService = null;
            string strModality = "";
            string strDocumentType = "";
            string strDocumentLong = "";
            string strCaracterRellenoNroDoc = "";
            string strTipoTelefonoPostPago = "";
            string plataform = objCustomerRequest.Plataform;
            string strEntityTypeCBIOS = "";
            string strEntityValueCBIOS = "";
            Int64 intCustomerId = 0;
            string strMsisdn = "", strCustomerType = "", strCodeError = "", strMessageError = "", strCodeResultCBios = "", strFlag = Claro.Constants.LetterY;

            string strIdSession = objCustomerRequest.Audit.Session;
            string strSearchType = objCustomerRequest.TypeSearch;
            string strAccountCustomer = objCustomerRequest.AccountCustomer;
            string strNroCellphone = objCustomerRequest.NumCellphone;
            string strApplication = objCustomerRequest.Application;
            string strIdTransaction = objCustomerRequest.Audit.Transaction;
            string strIpApplication = objCustomerRequest.IpApplication;
            string strApplicationName = objCustomerRequest.ApplicationName;
            string strUserApplication = objCustomerRequest.UserApplication;
            try
            {
                strDocumentType = KEY.AppSettings("strDocumentType");
                strDocumentLong = KEY.AppSettings("strDocumentLong");
                strCaracterRellenoNroDoc = KEY.AppSettings("strCaracterRellenoNroDoc");
                strTipoTelefonoPostPago = KEY.AppSettings("strTipoTelefonoPostPago");
                strEntityTypeCBIOS = KEY.AppSettings("strTipEntidadCBIOS");
                strEntityValueCBIOS = KEY.AppSettings("strModalidadBIOS");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }
            if (strApplication != null && (strSearchType.Equals(Claro.Constants.NumberOneString) || strSearchType.Equals(Claro.Constants.NumberTwoString) || strSearchType.Equals(Claro.Constants.NumberFourString) || strSearchType.Equals(Claro.Constants.NumberEightString) || strSearchType.Equals(Claro.Constants.NumberNineString)))
            {
                if (plataform.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (objCustomerRequest.strTecnologia != null && objCustomerRequest.strTecnologia.Equals("02")) //INICIATIVA-616 --> fija
                    {
                        listOptional = new List<POSTPAID.Legacy.GetDataCustomer.Common.listaOpcional>();

                        listOptional.Add(new POSTPAID.Legacy.GetDataCustomer.Common.listaOpcional()
                        {
                            clave = "proceso", 
                            valor = "fija"
                        });
                    } 
                    else
                    {
                        if (objCustomerRequest.outOptional != null && objCustomerRequest.outOptional.ListaOpcionalDat != null)
                        {
                            listOptional = new List<POSTPAID.Legacy.GetDataCustomer.Common.listaOpcional>();
                            var xlist = objCustomerRequest.outOptional.ListaOpcionalDat;
                            xlist.ForEach(x =>
                            {

                                listOptional.Add(new POSTPAID.Legacy.GetDataCustomer.Common.listaOpcional()
                                {
                                    clave = x.clave,
                                    valor = x.valor,
                                });


                            });

                        }
                    }
                    //INICIATIVA-616

                    
                    Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Request.request()
                    {
                        // Audit = objCustomerRequest.Audit,
                        obtenerDatosClienteRequest = new Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Request.obtenerDatosClienteRequestType()
                        {
                            custCode = strAccountCustomer,
                            dnNum = strNroCellphone,
                            listaOpcional = listOptional


                        }
                    };
                    //tobe
                    oCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Customer>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataCustomerTobe(objCustomerRequest.Audit, request, strAccountCustomer, strNroCellphone); });
                    oCustomer.CONTRATO_ID = objCustomerRequest != null && objCustomerRequest.outOptional != null ? oCustomer.CONTRATO_ID = objCustomerRequest.outOptional.CoidDat : oCustomer.CONTRATO_ID;

                }
                else
                {   //asis
                    oCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Customer>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataCustomer(strIdSession, strIdTransaction, strAccountCustomer, strNroCellphone); });

                }




                if (oCustomer != null)
                {
                    //Logica Segmento PROY-SEGMENTOVALOR 140351
                    string strSplitDNIRUC = KEY.AppSettings("strSplitDNIRUC");
                    string oTipoDoc = oCustomer.TIPO_DOC;

                    string[] strSplitSegmentoDocumentoF = KEY.AppSettings("strSplitSegmentoDocumento").Split('|')[0].Split(',');

                    string[] strSplitSegmentoDocumentoV = KEY.AppSettings("strSplitSegmentoDocumento").Split('|')[1].Split(',');

                    for (int i = 0; i < strSplitSegmentoDocumentoF.Length; i++)
                    {
                        if (oTipoDoc == strSplitSegmentoDocumentoF[i])
                        {
                            oTipoDoc = strSplitSegmentoDocumentoV[i];
                        }
                    }

                    try
                    {
                        Entity.Dashboard.Postpaid.Customer oCustomers = new Entity.Dashboard.Postpaid.Customer()
                        {
                            //INICIO PROY-SEGMENTOVALOR 140351
                            TIPO_DOC = strDocumentType,
                            NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + oCustomer.DNI_RUC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)),
                            LONG_NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + oCustomer.DNI_RUC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)).Length.ToString()
                            //FIN PROY-SEGMENTOVALOR 140351
                        };
                        oCustomer.SEGMENTO2 =
                            Claro.Web.Logging.ExecuteMethod<String>(strIdSession, strIdTransaction, () => { return Claro.SIACU.Data.Dashboard.Common.GetSegmentCustomerQuery(oCustomers, strIdSession, strUserApplication, strIpApplication, strIdTransaction, strApplicationName); });

                    }
                    catch (Exception ex)
                    {
                        oCustomer.SEGMENTO2 = "";
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }

                    try
                    {
                        if (oCustomer.oCUENTA.plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                            strModality = Claro.Web.Logging.ExecuteMethod<String>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetCustomerJanus(strIdSession, strIdTransaction, strNroCellphone); });
                        string strType = "";

                        try
                        {
                            if (plataform.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
                            {
                                //             objService =
                                //Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Service>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataLineTobe(objCustomerRequest.Audit, string.IsNullOrEmpty(oCustomer.CONTRATO_ID) ? Claro.Constants.NumberZero : int.Parse(oCustomer.CONTRATO_ID), oCustomer.coIdPub); });
                                if (objCustomerRequest.outOptional != null && objCustomerRequest.outOptional.IdStateLine != null)
                                {
                                    switch (objCustomerRequest.outOptional.IdStateLine)
                                    {
                                        case Claro.Constants.NumberOneString:
                                            oCustomer.ESTADO_LINEA = Claro.SIACU.Constants.Preactivated; break;
                                        case Claro.Constants.NumberTwoString:
                                            oCustomer.ESTADO_LINEA = Claro.SIACU.Constants.Active; break;
                                        case Claro.Constants.NumberThreeString:
                                            oCustomer.ESTADO_LINEA = Claro.SIACU.Constants.Suspend; break;
                                        case Claro.Constants.NumberFourString:
                                            oCustomer.ESTADO_LINEA = Claro.SIACU.Constants.Deactivated; break;
                                        default:
                                            oCustomer.ESTADO_LINEA = string.Empty;
                                            break;
                                    }
                                    Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("el id del estado de la linea es: {0} y su descripcion es: {1}", objCustomerRequest.outOptional.IdStateLine, oCustomer.ESTADO_LINEA));
                                }
                                else
                                {
                                    oCustomer.ESTADO_LINEA = string.Empty;
                                    Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("el id del estado de la linea es: {0} y su descripcion es: {1}", "No cargo el  Id ", oCustomer.ESTADO_LINEA));
                                }

                              

                            }
                            else
                            {
                                objService =
                                Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Service>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataLine(strIdSession, strIdTransaction, int.Parse(oCustomer.CONTRATO_ID)); });
                                oCustomer.ESTADO_LINEA = objService != null ? objService.ESTADO_LINEA : "";
                            }


                        }
                        catch (Exception ex)
                        {
                            objService = null;
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);

                        }

                        try
                        {
                            if (oCustomer.oCUENTA.plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                            {
                                strCodeResultCBios = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetCustomerCBIOS(strIdSession, strIdTransaction, strEntityTypeCBIOS, objCustomerRequest.NumCellphone, out intCustomerId, out strMsisdn, out strCustomerType, out strCodeError, out strMessageError); });
                                if ((strCodeError == Claro.SIACU.Constants.ZeroNumber) && (Convert.ToInt(intCustomerId) > Claro.Constants.NumberZero))
                                {

                                    strType = strEntityValueCBIOS;
                                    strFlag = Claro.Constants.LetterN;

                                }
                                string cTiposClientes = KEY.AppSettings("strCodTipoCli");
                                string cValorModalidad = KEY.AppSettings("strDefaultValueModalidad_Business_B2E");
                                if (oCustomer.COD_TIPO_CLIENTE == cTiposClientes.Split('|')[0] || oCustomer.COD_TIPO_CLIENTE == cTiposClientes.Split('|')[4])
                                {
                                    if (cValorModalidad != null || cValorModalidad != "")
                                    {
                                        oCustomer.MODALIDAD = cValorModalidad;
                                    }

                                }
                                if (strFlag == Claro.Constants.LetterY)
                                {
                                    if (strModality.Equals(Claro.Constants.LetterT))
                                    {
                                        strType = Claro.SIACU.Constants.Janus;
                                    }
                                    else
                                    {
                                        if (strSearchType.Equals(Claro.Constants.NumberOneString))
                                        {

                                            oCustomer.COD_PLAN_TARIFARIO = objService != null ? objService.COD_PLAN_TARIFARIO : "";

                                            if (objService != null && objService.FLAG_PLATAFORMA.Equals(Claro.Constants.LetterR))
                                            {
                                                strType = Claro.SIACU.Constants.Rice;
                                            }
                                        }
                                    }
                                }
                                oCustomer.oCUENTA.MODALIDAD = oCustomer.MODALIDAD + strType;
                            }
                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);

                        }


                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);

                    }
                    try
                    {
                        Entity.Dashboard.MembershipElectronic oMembershipElectronic =
                            Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.MembershipElectronic>(strIdSession, strIdTransaction, () => { return Claro.SIACU.Data.Dashboard.Common.GetMembershipVoucherElectronic(strIdSession, strIdTransaction, oCustomer.TIPO_DOC, oCustomer.DNI_RUC); });

                        oCustomer.AFILIACION = false;
                        if (oMembershipElectronic != null && oMembershipElectronic.ESTADO_AFILIACION.Equals(Claro.Constants.LetterA)) oCustomer.AFILIACION = true;
                    }
                    catch (Exception ex)
                    {
                        oCustomer.AFILIACION = false;
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);

                    }

                    try
                    {
                        oCustomer.BANNER_MESSAGE =
                             Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Dashboard.GetBanner(strIdSession, strIdTransaction, DateTime.Now, Claro.Constants.NumberOneString, strTipoTelefonoPostPago, Claro.SIACU.Constants.PostpaidMajuscule); });

                    }
                    catch (Exception ex)
                    {
                        oCustomer.BANNER_MESSAGE = "";
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);

                    }
                    try
                    {
                        string strContactNumberReference1 = string.Empty;
                        string strContactNumberReference2 = string.Empty;
                        string strContactCntCode = string.Empty;
                        if (oCustomer.oCUENTA.plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                        {
                            oCustomer.ContactCustomerCode = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetNumberCustomerContact(strIdSession, objCustomerRequest.Audit.ApplicationName, objCustomerRequest.Audit.Transaction, objCustomerRequest.Audit.IPAddress, objCustomerRequest.Audit.UserName, oCustomer.CUSTOMER_ID, out strContactNumberReference1, out strContactNumberReference2, out strContactCntCode); });
                            oCustomer.ContactNumberReference1 = strContactNumberReference1;
                            oCustomer.ContactNumberReference2 = strContactNumberReference2;
                            oCustomer.ContactCntCode = strContactCntCode;
                        }
                        else
                        {
                            if (objCustomerRequest.strTecnologia != null && objCustomerRequest.strTecnologia.Equals("02"))
                            {
                                oCustomer.ContactCustomerCode = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetNumberCustomerContact(strIdSession, objCustomerRequest.Audit.ApplicationName, objCustomerRequest.Audit.Transaction, objCustomerRequest.Audit.IPAddress, objCustomerRequest.Audit.UserName, oCustomer.CUSTOMER_ID, out strContactNumberReference1, out strContactNumberReference2, out strContactCntCode); });
                                oCustomer.ContactNumberReference1 = strContactNumberReference1;
                                oCustomer.ContactNumberReference2 = strContactNumberReference2;
                                oCustomer.ContactCntCode = strContactCntCode;
                            }
                            else
                            {
                                oCustomer.ContactCustomerCode = string.Empty;
                                oCustomer.ContactNumberReference1 = strContactNumberReference1;
                                oCustomer.ContactNumberReference2 = strContactNumberReference2;
                                oCustomer.ContactCntCode = strContactCntCode;
                            }
                            
                        }

                    }
                    catch (Exception ex)
                    {
                        oCustomer.ContactCustomerCode = "";
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);

                    }


                }



            }
            return oCustomer;
        }

        /// <summary>
        /// Método para obtener la información del cliente HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strSearchType">Tipo de búsqueda</param>
        /// <param name="strContractCode">Código de contacto</param>
        /// <param name="strApplication">Aplicación</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <returns>Devuelve objeto Customer con información del cliente.</returns>
        private static Entity.Dashboard.Postpaid.Customer GetDataCustomerHFC(POSTPAID.GetCustomer.CustomerRequest objCustomerRequest)
        {

            string strIdSession = objCustomerRequest.Audit.Session;
            string strSearchType = objCustomerRequest.TypeSearch;
            string strContractCode = objCustomerRequest.NumCellphone;
            string strApplication = objCustomerRequest.Application;
            string strIdTransaction = objCustomerRequest.Audit.Transaction;
            string strIpApplication = objCustomerRequest.IpApplication;
            string strApplicationName = objCustomerRequest.ApplicationName;
            string strUserApplication = objCustomerRequest.UserApplication;
            Entity.Dashboard.Postpaid.Customer oCustomer = null;
            if (strApplication != null && strSearchType.Equals(Claro.Constants.NumberOneString) || strSearchType.Equals(Claro.Constants.NumberThreeString) || strSearchType.Equals(Claro.Constants.NumberTenString))
            {
            	//INICIATIVA 616
                if (objCustomerRequest.Plataform.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
                {
                    List<POSTPAID.Branch> listBranch = null;
                    List<POSTPAID.Legacy.GetDataCustomer.Common.listaOpcional> listOptional = null;

                    string strDocumentType = KEY.AppSettings("strDocumentType");
                    string strDocumentLong = KEY.AppSettings("strDocumentLong");

                    string strLabelPlano = KEY.AppSettings("strLabelPlano");
                    string strLabelCodigoHUB = KEY.AppSettings("strLabelCodigoHUB");

                    listOptional = new List<POSTPAID.Legacy.GetDataCustomer.Common.listaOpcional>();
                    listOptional.Add(new POSTPAID.Legacy.GetDataCustomer.Common.listaOpcional()
                    {
                        clave = "proceso",
                        valor = "fija",
                    });

                    Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Request.request request = new Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Request.request()
                    {
                        // Audit = objCustomerRequest.Audit,
                        obtenerDatosClienteRequest = new Entity.Dashboard.Postpaid.Legacy.GetDataCustomer.Request.obtenerDatosClienteRequestType()
                        {
                            custCode = objCustomerRequest.AccountCustomer,
                            dnNum = strContractCode,
                            listaOpcional = listOptional


                        }
                    };
                    //tobe
                    oCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Customer>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataCustomerTobe(objCustomerRequest.Audit, request, string.Empty, string.Empty); });
                    oCustomer.CONTRATO_ID = objCustomerRequest != null && objCustomerRequest.outOptional != null ? oCustomer.CONTRATO_ID = objCustomerRequest.outOptional.CoidDat : oCustomer.CONTRATO_ID;
                    oCustomer.telefonoTOBE = request.obtenerDatosClienteRequest.dnNum;

                    if (oCustomer != null)
                    {

                        //Logica Segmento PROY-SEGMENTOVALOR 140351
                        string strCaracterRellenoNroDoc = KEY.AppSettings("strCaracterRellenoNroDoc");
                        string strSplitDNIRUC = KEY.AppSettings("strSplitDNIRUC");
                        string oTipoDoc = oCustomer.TIPO_DOC;

                        string[] strSplitSegmentoDocumentoF = KEY.AppSettings("strSplitSegmentoDocumento").Split('|')[0].Split(',');

                        string[] strSplitSegmentoDocumentoV = KEY.AppSettings("strSplitSegmentoDocumento").Split('|')[1].Split(',');

                        for (int i = 0; i < strSplitSegmentoDocumentoF.Length; i++)
                        {
                            if (oTipoDoc == strSplitSegmentoDocumentoF[i])
                            {
                                oTipoDoc = strSplitSegmentoDocumentoV[i];
                            }
                        }

                        try
                        {
                            Entity.Dashboard.Postpaid.Customer oCustomers = new Entity.Dashboard.Postpaid.Customer()
                            {
                                //INICIO PROY-SEGMENTOVALOR 140351
                                TIPO_DOC = strDocumentType,
                                NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + oCustomer.DNI_RUC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)),
                                LONG_NRO_DOC = (oTipoDoc + strSplitDNIRUC.Split('|')[0] + oCustomer.DNI_RUC).Trim().PadRight(System.Convert.ToInt32(strDocumentLong), System.Convert.ToChar(strCaracterRellenoNroDoc)).Length.ToString()
                                //FIN PROY-SEGMENTOVALOR 140351
                            };
                            oCustomer.SEGMENTO2 =
                                Claro.Web.Logging.ExecuteMethod<String>(strIdSession, strIdTransaction, () => { return Claro.SIACU.Data.Dashboard.Common.GetSegmentCustomerQuery(oCustomers, strIdSession, strUserApplication, strIpApplication, strIdTransaction, strApplicationName); });

                        }
                        catch (Exception ex)
                        {
                            oCustomer.SEGMENTO2 = "";
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                        }



                        oCustomer.COD_CENTRO_POBLADO = oCustomer.CODIGO_PLANO_INST;
                        oCustomer.COD_PLANO = strLabelPlano;
                        oCustomer.COD_HUB = strLabelCodigoHUB;
                        if (string.IsNullOrEmpty(oCustomer.CONTRATO_ID))
                        {
                            oCustomer.CONTRATO_ID = "0";
                        }
                        try
                        {
                            POSTPAID.Hub objHub = Claro.Web.Logging.ExecuteMethod<POSTPAID.Hub>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetHubTobe(strIdSession, strIdTransaction, Convert.ToInt(oCustomer.CUSTOMER_ID), Convert.ToInt(oCustomer.CONTRATO_ID)); });
                            oCustomer.DES_CENTRO_POBLADO = (objHub != null) ? objHub.HUB_DESC : "";
                        }
                        catch (Exception ex)
                        {
                            oCustomer.DES_CENTRO_POBLADO = "";
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                        }

                        try
                        {
                            listBranch = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Branch>>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetBranch(strIdSession, strIdTransaction, oCustomer.CUSTOMER_ID); });

                            if (listBranch != null && listBranch.Count > 0)
                            {
                                oCustomer.UBIGEO_INST = listBranch.First().ubigeo;
                                oCustomer.CODIGO_PLANO_INST = listBranch.First().plano;
                                oCustomer.DISTRITO_LEGAL = listBranch.First().distrito;
                                oCustomer.PROVINCIA_LEGAL = listBranch.First().provincia;
                                oCustomer.DEPARTEMENTO_LEGAL = listBranch.First().departamento;
                                oCustomer.DIRECCION_DESPACHO = listBranch.First().sucursal.Substring(listBranch.First().sucursal.ToString().IndexOf("(") + Claro.Constants.NumberOne).Replace(")", "");
                                oCustomer.URBANIZACION_LEGAL = listBranch.First().notas;
                            }

                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                        }

                        try
                        {
                            string strContactNumberReference1 = "";
                            string strContactNumberReference2 = "";
                            string strContactCntCode = "";
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, "Ingreso ContactCustomerCode TOBE");
                            oCustomer.ContactCustomerCode = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetNumberCustomerContact(strIdSession, objCustomerRequest.Audit.ApplicationName, objCustomerRequest.Audit.Transaction, objCustomerRequest.Audit.IPAddress, objCustomerRequest.Audit.UserName, oCustomer.CUSTOMER_ID, out strContactNumberReference1, out strContactNumberReference2, out strContactCntCode); });
                            oCustomer.ContactNumberReference1 = strContactNumberReference1;
                            oCustomer.ContactNumberReference2 = strContactNumberReference2;
                            oCustomer.ContactCntCode = strContactCntCode;
                        }
                        catch (Exception ex)
                        {
                            oCustomer.ContactCustomerCode = "";
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                        }


                    }
                }
                else
                {
                	if (strSearchType.Equals(Claro.Constants.NumberOneString))
                    {
                        try
                        {
                            strContractCode = GetCodeByAccountLine(strIdSession, strContractCode, strApplication, strIdTransaction, strIpApplication, strApplicationName, strUserApplication);
                        }
                        catch (Exception ex)
                        {
                            strContractCode = "";
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                        }

                    }
                    if (!string.IsNullOrEmpty(strContractCode))
                    {
                        try
                        {
                            oCustomer = GetDataCustomersHFC(objCustomerRequest);
                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                        }
                    }
                }
            	//INICIATIVA 616
            	
                
            }
            return oCustomer;
        }

        /// <summary>
        /// Método para obtener la información del cliente HFC.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strSearchType">Tipo de búsqueda</param>
        /// <param name="strContractCode">Código de contrato</param>
        /// <param name="strApplication">Aplicación</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <returns>Devuelve objeto Customer con información del cliente.</returns>
        private static Entity.Dashboard.Postpaid.Customer GetDataCustomersHFC(POSTPAID.GetCustomer.CustomerRequest objCustomerRequest)
        {

            Entity.Dashboard.Postpaid.Customer oCustomer = null;
            string strCustomerID = "";
            string strLabelPlano = "";
            string strLabelCodigoHUB = "";
            string strLabelCentroPoblado = "";
            string strLabelDesCentroPoblado = "";
            string strContactNumberReference1 = "";
            string strContactNumberReference2 = "";
            string strContactCntCode = "";
            string strTipoTelefonoPostPago = "";
            string strIdSession = objCustomerRequest.Audit.Session;
            string strSearchType = objCustomerRequest.TypeSearch;
            string strContractCode = objCustomerRequest.NumCellphone;
            string strApplication = objCustomerRequest.Application;
            string strIdTransaction = objCustomerRequest.Audit.Transaction;
            string strIpApplication = objCustomerRequest.IpApplication;
            string strApplicationName = objCustomerRequest.ApplicationName;
            string strUserApplication = objCustomerRequest.UserApplication;
            Entity.Dashboard.Postpaid.Customer objCustomer = null;
            List<POSTPAID.Branch> listBranch = null;
            List<Entity.Dashboard.Postpaid.Customer> oCustomers = null;
            string strFlagConsulta = "";

            try
            {
                strLabelPlano = KEY.AppSettings("strLabelPlano");
                strLabelCodigoHUB = KEY.AppSettings("strLabelCodigoHUB");
                strLabelCentroPoblado = KEY.AppSettings("strLabelCentroPoblado");
                strLabelDesCentroPoblado = KEY.AppSettings("strLabelDesCentroPoblado");
                strTipoTelefonoPostPago = KEY.AppSettings("strTipoTelefonoPostPago");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            string CODE_PRODUCT_FIXE = KEY.AppSettings("strTipoGeneralFIJA").Split('|')[1].ToString();
            string OPTIONS_PRODUCT_FIXE = KEY.AppSettings("strTipoGeneralFIJA").Split('|')[2].ToString();

            if (strApplication.Equals(Claro.SIACU.Constants.HFC) || objCustomerRequest.Application.Equals(CODE_PRODUCT_FIXE))
            {
                if (strSearchType.Equals(Claro.Constants.NumberTenString))
                {
                    try
                    {
                        strCustomerID = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetCustomerIDxCintillo(strIdSession, strIdTransaction, strContractCode); });
                    }
                    catch (Exception ex)
                    {
                        strCustomerID = "";
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }


                    if (!string.IsNullOrEmpty(strCustomerID))
                    {
                        try
                        {
                            oCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Customer>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataCustomerByCustomerIdHFC(strIdSession, strIdTransaction, strIpApplication, strApplicationName, strUserApplication, strCustomerID); });

                        }
                        catch (Exception ex)
                        {
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                        }
                    }
                }
                else
                {
                    try
                    {
                        oCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Customer>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataCustomerByContractCodeHFC(strIdSession, strIdTransaction, strIpApplication, strApplicationName, strUserApplication, strContractCode); });

                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }
                }

                if (oCustomer != null)
                {
                    oCustomer.COD_CENTRO_POBLADO = oCustomer.CODIGO_PLANO_INST;
                    oCustomer.COD_PLANO = strLabelPlano;
                    oCustomer.COD_HUB = strLabelCodigoHUB;
                    if (string.IsNullOrEmpty(oCustomer.CONTRATO_ID))
                    {
                        oCustomer.CONTRATO_ID = "0";
                    }
                    try
                    {
                        POSTPAID.Hub objHub = Claro.Web.Logging.ExecuteMethod<POSTPAID.Hub>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetHub(strIdSession, strIdTransaction, oCustomer.CUSTOMER_ID, oCustomer.CONTRATO_ID); });
                        oCustomer.DES_CENTRO_POBLADO = (objHub != null) ? objHub.HUB_DESC : "";
                    }
                    catch (Exception ex)
                    {
                        oCustomer.DES_CENTRO_POBLADO = "";
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }

                    try
                    {
                        listBranch = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Branch>>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetBranch(strIdSession, strIdTransaction, oCustomer.CUSTOMER_ID); });

                        if (listBranch != null && listBranch.Count > 0)
                        {
                            oCustomer.UBIGEO_INST = listBranch.First().ubigeo;
                            oCustomer.CODIGO_PLANO_INST = listBranch.First().plano;
                            oCustomer.DISTRITO_LEGAL = listBranch.First().distrito;
                            oCustomer.PROVINCIA_LEGAL = listBranch.First().provincia;
                            oCustomer.DEPARTEMENTO_LEGAL = listBranch.First().departamento;
                            oCustomer.DIRECCION_DESPACHO = listBranch.First().sucursal.Substring(listBranch.First().sucursal.ToString().IndexOf("(") + Claro.Constants.NumberOne).Replace(")", "");
                            oCustomer.URBANIZACION_LEGAL = listBranch.First().notas;
                        }

                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }
                }
            }
            else if (strApplication.Equals(Claro.SIACU.Constants.LTE))
            {
                try
                {
                    oCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Customer>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataCustomerByContractCodeLTE(strIdSession, strIdTransaction, strIpApplication, strApplicationName, strUserApplication, strContractCode); });

                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                }


                if (oCustomer != null)
                {
                    oCustomer.COD_PLANO = strLabelCentroPoblado;
                    oCustomer.COD_HUB = strLabelDesCentroPoblado;
                }
            }
            if (strApplication.Equals(Claro.SIACU.Constants.LTE) || strApplication.Equals(Claro.SIACU.Constants.HFC) || strApplication.Equals(CODE_PRODUCT_FIXE))
            {
                try
                {
                    string strUsuario = objCustomerRequest.Audit.UserName;
                    string strObjContact = string.Empty;

                    Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Inicio Busqueda Cliente HFC/LTE en BSCS"));
                    oCustomers = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Customer>>(strIdSession, strIdTransaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetCustomer(strIdSession, strIdTransaction, oCustomer.CUSTOMER_ID);
                    });
                    Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Cantidad de lineas en BSCS: {0}", oCustomers.Count));
                    Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Fin Busqueda Cliente HFC/LTE en BSCS"));

                    Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Inicio Busqueda Cliente HFC/LTE en Clarify"));
                    objCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Customer>(strIdSession, strIdTransaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetPreviousCustomerHFC(strIdSession, strIdTransaction, Claro.Constants.LetterH + oCustomer.CUSTOMER_ID, "", "", Claro.Constants.NumberOneString, out strFlagConsulta);
                    });
                    Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("strFlagConsulta: {0}", strFlagConsulta));
                    Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Fin Busqueda Cliente HFC/LTE en Clarify"));

                    if (strFlagConsulta != Constants.OK)
                    {
                        var dModality = new Dictionary<string, string>
                                {
                                     { "Empleado Claro", "Postpago Empleado Tim" },
                                    { "B2E", "Postpago Empleado Empresa" },
                                    { "Business", "Postpago Business" },
                                    { "Consumer", "Postpago Consumer" },
                                    { "Franquicias", "Postpago Franquicias" },
                                    { "Demo", "Postpago Demo" }
                                };

                        if (oCustomers.Count > 0)
                        {
                            oCustomers.First().MODALIDAD = dModality[oCustomers.First().TIPO_CLIENTE];
                            oCustomers.First().EMAIL = (oCustomers.First().EMAIL.Length > 80) ? oCustomers.First().EMAIL.Substring(0, 80) : oCustomers.First().EMAIL;
                            Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Inicio Creacion Cliente en Clarify"));
                            bool result = Claro.Web.Logging.ExecuteMethod<Boolean>(strIdSession, strIdTransaction, () =>
                            {
                                return Data.Dashboard.Postpaid.CreateCustomer(strIdSession, strIdTransaction, oCustomers.First(), strUsuario, ref strObjContact);
                            });
                            Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Cliente creado: {0}", result));
                            Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Fin Creacion Cliente en Clarify"));
                            if (result)
                            {
                                Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Inicio Actualización Nacionalidad en Clarify"));
                                bool resulta = Claro.Web.Logging.ExecuteMethod<Boolean>(strIdSession, strIdTransaction, () =>
                                {
                                    return Data.Dashboard.Postpaid.UpdateNationality(strIdSession, strIdTransaction, oCustomers.First());
                                });
                                Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Nacionalidad actualizado: {0}", resulta));
                                Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Fin Actualización Nacionalidad en Clarify"));
                            }

                        }
                    }
                    if (strFlagConsulta == Constants.OK && objCustomer.NRO_DOC == "88888888")
                    {
                        Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Inicio Actualiza Datos principales del Cliente HFC/LTE en Clarify"));
                        if (oCustomers.Count > 0)
                        {
                            bool resultado = Claro.Web.Logging.ExecuteMethod<Boolean>(strIdSession, strIdTransaction, () =>
                            {
                                return Data.Dashboard.Postpaid.UpdateClient(strIdSession, strIdTransaction, oCustomers.First());
                            });
                            Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Cliente actualizado: {0}", resultado));
                            Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("Fin Actualiza Datos principales del Cliente HFC/LTE en Clarify"));
                        }
                        else
                        {
                            Claro.Web.Logging.Info(strIdSession, strIdTransaction, string.Format("No existen lineas en BSCS"));
                        }
                    }

                }
                catch (Exception ex)
                {
                    objCustomer = null;
                    Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                }
            }
            if (oCustomer != null)
            {
                oCustomer.oCUENTA.MODALIDAD = "";
                try
                {
                    Entity.Dashboard.MembershipElectronic oMembershipElectronic = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.MembershipElectronic>(strIdSession, strIdTransaction, () => { return Claro.SIACU.Data.Dashboard.Common.GetMembershipVoucherElectronic(strIdSession, strIdTransaction, oCustomer.TIPO_DOC, oCustomer.NRO_DOC); });
                    oCustomer.AFILIACION = false;
                    if (oMembershipElectronic != null && oMembershipElectronic.ESTADO_AFILIACION.Equals(Claro.Constants.LetterA)) oCustomer.AFILIACION = true;
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                }

                try
                {
                    oCustomer.oCUENTA.CONSULTOR = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetSeller(strIdSession, strIdTransaction, oCustomer.NRO_DOC); });

                }
                catch (Exception ex)
                {
                    oCustomer.oCUENTA.CONSULTOR = "";
                    Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                }

                try
                {
                    oCustomer.ContactCustomerCode = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetNumberCustomerContact(strIdSession, objCustomerRequest.Audit.ApplicationName, objCustomerRequest.Audit.Transaction, objCustomerRequest.Audit.IPAddress, objCustomerRequest.Audit.UserName, oCustomer.CUSTOMER_ID, out strContactNumberReference1, out strContactNumberReference2, out strContactCntCode); });
                    oCustomer.ContactNumberReference1 = strContactNumberReference1;
                    oCustomer.ContactNumberReference2 = strContactNumberReference2;
                    oCustomer.ContactCntCode = strContactCntCode;
                }
                catch (Exception ex)
                {
                    oCustomer.ContactCustomerCode = "";
                    Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                }

                try
                {
                    oCustomer.BANNER_MESSAGE = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Dashboard.GetBanner(strIdSession, strIdTransaction, DateTime.Now, Claro.Constants.NumberOneString, strTipoTelefonoPostPago, Claro.SIACU.Constants.PostpaidMajuscule); });

                }
                catch (Exception ex)
                {
                    oCustomer.BANNER_MESSAGE = "";
                    Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                }
            }
            return oCustomer;
        }

        /// <summary>
        /// Método para obtener el código de contrato por cuenta.
        /// </summary>
        /// <param name="strValue">Valor</param>
        /// <param name="strApplication">Aplicación</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de aplicación</param>
        /// <returns>Devuelve cadena con el código de contrato.</returns>
        private static string GetCodeByAccountLine(string strIdSession, string strValue, string strApplication, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication)
        {
            string strContractCode = string.Empty;
            string strCustomerCode;
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstServiceGSM = null;
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstServiceISDN = null;

            if (strApplication.Equals(Claro.SIACU.Constants.HFC))
            {

                strCustomerCode = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetCodeByAccountLineHFC(strIdSession, strIdTransaction, strIpApplication, strApplicationName, strUserApplication, Claro.Constants.NumberOneString, strValue); });
                Claro.Web.Logging.ExecuteMethod<bool>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetServicesByCustomerCodeHFC(strIdSession, strIdTransaction, strIpApplication, strApplicationName, strUserApplication, strCustomerCode, ref lstServiceGSM, ref lstServiceISDN); });

            }
            else if (strApplication.Equals(Claro.SIACU.Constants.LTE))
            {
                strCustomerCode = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetCodeByAccountLineLTE(strIdSession, strIdTransaction, strIpApplication, strApplicationName, strUserApplication, Claro.Constants.NumberOneString, strValue); });
                Claro.Web.Logging.ExecuteMethod<bool>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetServicesByCustomerCodeLTE(strIdSession, strIdTransaction, strIpApplication, strApplicationName, strUserApplication, strCustomerCode, ref lstServiceGSM, ref lstServiceISDN); });

            }

            if (lstServiceISDN != null && lstServiceISDN.Count > 0)
            {
                strContractCode = lstServiceISDN[0].CODID;
            }
            else if (lstServiceGSM != null && lstServiceGSM.Count > 0)
            {
                strContractCode = lstServiceGSM[0].CODID;
            }
            return strContractCode;
        }
        private static List<POSTPAID.GetUsageThresholdsCounter.Member_Request> getListmember(string phone)
        {
            List<POSTPAID.GetUsageThresholdsCounter.Member_Request> lstMember_Request = null;
            if (!string.IsNullOrEmpty(phone)) phone = Claro.Constants.NumberFiftyOneString + phone;

            return lstMember_Request = new List<POSTPAID.GetUsageThresholdsCounter.Member_Request>()
            {
                new POSTPAID.GetUsageThresholdsCounter.Member_Request(){
                            name=KEY.AppSettings("originNodeType"),
                            value= new POSTPAID.GetUsageThresholdsCounter.Value2_Request(){
                                @string=KEY.AppSettings("AIR")
                            }
                  } ,
                new POSTPAID.GetUsageThresholdsCounter.Member_Request(){
                            name=KEY.AppSettings("originHostName"),
                            value=new POSTPAID.GetUsageThresholdsCounter.Value2_Request(){
                                @string=KEY.AppSettings("li2dbdbscf02"),
                            }
                  } ,
                new POSTPAID.GetUsageThresholdsCounter.Member_Request(){
                            name=KEY.AppSettings("originTransactionID"),
                            value=new POSTPAID.GetUsageThresholdsCounter.Value2_Request(){
                                @string=DateTime.Now.Year.ToString()+
                                DateTime.Now.Month.ToString()+
                                DateTime.Now.Day.ToString()+
                                DateTime.Now.Hour.ToString()+
                                DateTime.Now.Minute.ToString()+
                                DateTime.Now.Second.ToString()//"2019380515030143"
                            }
                  } ,
                new POSTPAID.GetUsageThresholdsCounter.Member_Request(){
                            name=KEY.AppSettings("originTimeStamp"),
                            value=new POSTPAID.GetUsageThresholdsCounter.Value2_Request(){
                                dateTime_iso8601=DateTime.Now.ToString("yyyyMMddTHH:mm:ss")+"-0500"//"20190305T15:38:01-0500"
                            }
                  } ,
                new POSTPAID.GetUsageThresholdsCounter.Member_Request(){
                            name=KEY.AppSettings("subscriberNumber"),
                            value=new POSTPAID.GetUsageThresholdsCounter.Value2_Request(){
                                @string=phone
                            }
                  }
            };

        }
        /// <summary>
        /// Método para obtener información de la línea o servicio de un cliente postpago.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strContratoID">Id de contrato</param>
        /// <param name="strApplication">Aplicación</param>
        /// <param name="strTelefono">Teléfono</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strCustomerType">Tipo de cliente</param>
        /// <param name="strDocumentType">Tipo de documento de identidad</param>
        /// <param name="strDocumentNumber">Número de documento de identidad</param>
        /// <returns>Devuelve objeto Service con información del servicio.</returns>
        private static Entity.Dashboard.Postpaid.Service GetDataServiceLinePost(POSTPAID.GetService.ServiceRequest objServiceRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.Service objService = null;
            string objItemTPI = "";
            string strTFI = "";
            string str2PLAY = "";
            string strTipoProductoDTH = "";
            string strPortabilityState = "";
            string cadenaSpeed = "";

            string strIdSession = objServiceRequest.Audit.Session;
            string strContratoID = objServiceRequest.ContractID;
            string strTelefono = objServiceRequest.Telephone;
            string strTelefonospeed = objServiceRequest.strphonespeed;
            string strIdTransaction = objServiceRequest.IdTransaction;
            string strCustomerType = objServiceRequest.CustomerType;
            string strDocumentType = objServiceRequest.DocumentType;
            string strDocumentNumber = objServiceRequest.DocumentNumber;
            string strModality = objServiceRequest.Modality;
            Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.MethodCall_Request objMethodCall_Request = null;
            try
            {
                strTipoProductoDTH = KEY.AppSettings("strTipoProductoDTH");
                if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
                    objService = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.Service>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataLineTobe(objServiceRequest.Audit, string.IsNullOrEmpty(strContratoID) ? Claro.Constants.NumberZero : int.Parse(strContratoID), objServiceRequest.coIdPub); });
                else objService = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.Service>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataLine(strIdSession, strIdTransaction, int.Parse(strContratoID)); });

                if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase)) cadenaSpeed = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetSpeed(objServiceRequest.strphonespeed, objServiceRequest.Audit); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            if (objService != null)
            {
                if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
                {//degradation ,tethering ,LIMITE CONSUMO, TIPO PLAN    TOBE
                    objService.TOPE_CONSUMO = objService.topeConsumo;
                    objMethodCall_Request = new POSTPAID.GetUsageThresholdsCounter.MethodCall_Request()
                    {

                        methodName = "GetUsageThresholdsAndCounters",
                        @params = new POSTPAID.GetUsageThresholdsCounter.Params_Request()
                        {
                            param = new POSTPAID.GetUsageThresholdsCounter.Param_Request()
                            {
                                value = new POSTPAID.GetUsageThresholdsCounter.Value_Request()
                                {
                                    @struct = new POSTPAID.GetUsageThresholdsCounter.Struct_Request()
                                    {
                                        member = getListmember(strTelefonospeed)
                                    }
                                }
                            }
                        }
                    };

                    Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.GetUsageThresholdsCounter GetUsageThresholdsCounterResponse = GetUsageThresholdsCounter(objMethodCall_Request, objServiceRequest.Audit);


                    objService.Tethering = Claro.Convert.ToString(GetUsageThresholdsCounterResponse.Thetering);

                    string TypePlan = string.Empty;
                    Claro.Web.Logging.Info(strIdSession, strIdTransaction, "objService.TOPE_CONSUMO " + objService.TOPE_CONSUMO + " ConfigurationManager.AppSettings('strExacto') " + ConfigurationManager.AppSettings("strExacto"));
                    if (objService.TOPE_CONSUMO.ToUpper().Equals(ConfigurationManager.AppSettings("strExacto").ToUpper()))
                    {
                        TypePlan = ConfigurationManager.AppSettings("strConsumptionLimit_TypePlanControl");
                    }
                    else
                    {
                        TypePlan = ConfigurationManager.AppSettings("strConsumptionLimit_TypePlanPostpaid");
                    };

                    objService.SEGMENTO_CLIENTE = Claro.Convert.ToString(TypePlan);
                }
                else
                {//degradation ,tethering  ASIS
                    if (!string.IsNullOrEmpty(cadenaSpeed))
                    {
                        objService.Degradation = cadenaSpeed.Split('|')[0];
                        objService.Tethering = cadenaSpeed.Split('|')[1];
                    }
                    else
                    {
                        objService.Degradation = "";
                        objService.Tethering = "";
                    }
                    //LIMITE CONSUMO ASIS
                    if (objService.FLAG_PLATAFORMA != null && objService.FLAG_PLATAFORMA.Equals(Claro.Constants.LetterR))
                    {
                        try
                        {
                            if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                            {
                                objService.TOPE_CONSUMO = GetDetalleCliente(strIdSession, strIdTransaction, strTelefono, strContratoID);
                            }
                            else
                            {
                                //aquise va llenar tobe
                                objService.TOPE_CONSUMO = string.Empty;
                            }

                        }
                        catch (Exception ex)
                        {
                            objService.TOPE_CONSUMO = "";
                            Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                        }
                    }
                    else
                    {
                        objService.TOPE_CONSUMO = Claro.SIACU.Constants.NotAvailable;
                    }
                    // TIPO PLAN ASIS
                    try
                    {
                        int intContratoID;
                        string strNroCel;
                        intContratoID = int.Parse(objService.CONTRATO_ID);
                        strNroCel = objService.NRO_CELULAR;

                        objService.SEGMENTO_CLIENTE = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetTypeSegmentLineBSCS(strIdSession, strIdTransaction, intContratoID, strNroCel); });

                    }
                    catch (Exception ex)
                    {
                        objService.TIPO_SOLUCION = "";
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }

                }



                try
                {
                    objService.FLAG_TFI = GetFlagTFI(strIdSession, strIdTransaction, objService.COD_PLAN_TARIFARIO);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                }

                objService.Portability = ValidateStatusPortability(strIdSession, strIdTransaction, objService.NRO_CELULAR, out strPortabilityState);
                objService.PortabilityState = strPortabilityState;
                if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                {
                    objService.Roaming = ValidateRoaming(strIdSession, strIdTransaction, strContratoID, objService.NRO_CELULAR, strCustomerType);
                }

                if (objService.NROICCID != null) objService.BANCA_MOVIL = GetValidateICCID(objService.NROICCID) ? Claro.SIACU.Constants.Yes : Claro.SIACU.Constants.Not;
                else objService.BANCA_MOVIL = Claro.SIACU.Constants.Not;




                try
                {
                    if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                    {
                        objService.TIPO_SOLUCION = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetTypeSolutionLine(strIdSession, strIdTransaction, strContratoID); });
                    }
                    else
                    {
                        objService.TIPO_SOLUCION = string.Empty;
                    }

                }
                catch (Exception ex)
                {
                    objService.TIPO_SOLUCION = "";
                    Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                }




                if (strCustomerType.Equals(Claro.SIACU.Constants.Business))
                {
                    try
                    {
                        if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                        {

                            string strPackage = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetValidateCompanyPackage(strIdSession, strIdTransaction, GetDocumentType(strDocumentType), strDocumentNumber); });

                            if (strPackage.Equals(Claro.Constants.NumberOneString))
                            {
                                objService.SERVICEPAQUETE = Claro.SIACU.Constants.PackageClearCompany;
                                objService.ESTADO_SERVICEPAQUETE = true;
                            }
                            else
                            {
                                objService.SERVICEPAQUETE = "";
                                objService.ESTADO_SERVICEPAQUETE = false;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        objService.SERVICEPAQUETE = "";
                        objService.ESTADO_SERVICEPAQUETE = false;
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }
                }
                if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                {
                    try
                    {

                        strTFI = GetTFI(strIdSession, strIdTransaction, objService.COD_PLAN_TARIFARIO);
                        objItemTPI = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetSearchTerminalTPI(strIdSession, strIdTransaction, strTelefono, ""); });


                    }
                    catch (Exception ex)
                    {
                        strTFI = "";
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }

                    try
                    {
                        str2PLAY = Get2PLAY(strIdSession, strIdTransaction, objService.COD_PLAN_TARIFARIO);
                    }
                    catch (Exception ex)
                    {
                        str2PLAY = "";
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }

                    if (objService.PLAN.ToUpper().Trim().Equals(strTipoProductoDTH))
                    {
                        objService.TFI = Claro.SIACU.Constants.TvSat;
                        objService.ESTADO_TFI = true;
                        objService.ESTADO_DTH = true;
                    }
                    else
                    {
                        if (str2PLAY.Equals(Claro.Constants.LetterN))
                        {
                            if (objService.FLAG_TFI.Equals(Claro.SIACU.Constants.Yes))
                            {
                                objService.TFI = Claro.SIACU.Constants.Fixed + strTFI;
                                objService.ESTADO_TFI = true;
                                if (string.Equals(strModality.Trim(), Claro.SIACU.Constants.Corporate, StringComparison.OrdinalIgnoreCase)) objService.TFI += Claro.Constants.FormatSpace + Claro.SIACU.Constants.BusinessSpanish;
                            }
                            else
                            {
                                objService.TFI = "" + strTFI;
                                if (string.Equals(strTFI.Trim(), Claro.SIACU.Constants.Rural, StringComparison.OrdinalIgnoreCase)) objService.ESTADO_TFI = true;
                            }
                        }
                        else if (str2PLAY.Equals(Claro.Constants.LetterS) && objService.FLAG_TFI.Equals(Claro.SIACU.Constants.Yes))
                        {
                            objService.TFI = Claro.SIACU.Constants.Play2;
                            objService.ESTADO_TFI = true;
                            if (string.Equals(strModality.Trim(), Claro.SIACU.Constants.Corporate, StringComparison.OrdinalIgnoreCase)) objService.TFI += Claro.Constants.FormatSpace + Claro.SIACU.Constants.BusinessSpanish;
                        }
                    }

                    try
                    {
                        string strCodeError = "";
                        objService.SERVICECOMBO = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetPlanServiceCombo(strIdSession, strIdTransaction, strContratoID, ref strCodeError); });

                        objService.ESTADO_SERVICECOMBO = (strCodeError.Equals(Claro.Constants.NumberZeroString));
                    }
                    catch (Exception ex)
                    {
                        objService.SERVICECOMBO = "";
                        Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
                    }


                }


            }





            return objService;
        }

        /// <summary>
        /// Método para obtener información de la línea o servicio de un cliente HFC.
        /// </summary>
        /// <param name="strContratoID">Id de contrato</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication"></param>
        /// <returns>Devuelve objeto Service con información del servicio.</returns>
        private static Entity.Dashboard.Postpaid.Service GetDataServiceLineHFC(POSTPAID.GetService.ServiceRequest objServiceRequest)
        {
            if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
            {
                return Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.Service>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDataLineTobeHFC(objServiceRequest.Audit, string.IsNullOrEmpty(objServiceRequest.ContractID) ? Claro.Constants.NumberZero : int.Parse(objServiceRequest.ContractID), objServiceRequest.coIdPub); });
            }
            else
            {
                return Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Service>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDataLineHFC(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.IpApplication, objServiceRequest.ApplicationName, objServiceRequest.UserApplication, int.Parse(objServiceRequest.ContractID)); });
            }

        }

        public static Entity.Dashboard.Postpaid.GetService.ServiceResponse GetServiceLineHFC(Entity.Dashboard.Postpaid.GetService.ServiceRequest objServiceRequest)
        {
            Entity.Dashboard.Postpaid.GetService.ServiceResponse objServiceResponse = new Entity.Dashboard.Postpaid.GetService.ServiceResponse()
            {

                ObjService = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Service>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDataLineHFC(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceRequest.Audit.IPAddress, objServiceRequest.Audit.ApplicationName, objServiceRequest.Audit.UserName, Convert.ToInt(objServiceRequest.ContractID)); }),
            };

            return objServiceResponse;
        }
        /// <summary>
        /// Método para obtener información de la línea o servicio de un cliente LTE.
        /// </summary>
        /// <param name="strContratoID">Id de contrato</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="strIpApplication">Ip de aplicación</param>
        /// <param name="strApplicationName">Nombre de aplicación</param>
        /// <param name="strUserApplication">Usuario de Aplicación</param>
        /// <returns>Devuelve objeto Service con información del servicio.</returns>
        private static Entity.Dashboard.Postpaid.Service GetDataServiceLineLTE(string strIdSession, string strContratoID, string strIdTransaction, string strIpApplication, string strApplicationName, string strUserApplication)
        {
            return Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Service>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataLineLTE(strIdSession, strIdTransaction, strIpApplication, strApplicationName, strUserApplication, int.Parse(strContratoID)); });

        }

        /// <summary>
        /// Método para obtener el tipo de documento.
        /// </summary>
        /// <param name="strDocumentType">Tipo de documento</param>
        /// <returns>Devuelve entero con valor de tipo de documento.</returns>
        private static int GetDocumentType(string strDocumentType)
        {
            int intType = 0;
            if (strDocumentType.Equals(Claro.SIACU.Constants.RUC))
            {
                intType = 0;
            }
            else if (strDocumentType.Equals(Claro.SIACU.Constants.Passport))
            {
                intType = 1;
            }
            else if (strDocumentType.Equals(Claro.SIACU.Constants.DNI))
            {
                intType = Claro.Constants.NumberTwo;
            }
            else if (strDocumentType.Equals(Claro.SIACU.Constants.CardForeign))
            {
                intType = 4;
            }
            return intType;
        }

        /// <summary>
        /// Método para obtener el límite de comsumo.
        /// </summary>
        /// <param name="strTelephone">Teléfono</param>
        /// <param name="strContractID">Id de contrato</param>
        /// <returns>Devuelve cadena con el máximo consumo.</returns>
        private static string GETConsumeLimit(string strIdSession, string strTransaction, string strTelephone, string strContractID)
        {
            string strMaximumConsumption = Claro.SIACU.Constants.Deactivated;
            List<POSTPAID.ConsumeLimit> lstMaximumConsumption = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ConsumeLimit>>(strIdSession, strTransaction, () => { return Data.Dashboard.Postpaid.GetConsumeLimit(strIdSession, strTransaction, strTelephone, int.Parse(strContractID)); });

            bool isState = false;

            if (lstMaximumConsumption != null && lstMaximumConsumption.Count > 0)
            {
                int intOutput;
                bool isCodeService;

                foreach (var item in lstMaximumConsumption)
                {
                    isCodeService = Int32.TryParse(item.CO_SER, out intOutput);
                    if (isCodeService)
                    {
                        isState = true;
                        break;
                    }
                }

                if (isState) strMaximumConsumption = Claro.SIACU.Constants.Active;
            }

            return strMaximumConsumption;
        }

        /// <summary>
        /// Método para validar ICCID.
        /// </summary>
        /// <param name="strICCID">ICCID</param>
        /// <returns>Devuelve booleano al validar ICCID.</returns>
        private static bool GetValidateICCID(string strICCID)
        {
            return (strICCID.Substring(8, 1).Equals(Claro.SIACU.Constants.TwoNumber) && (strICCID.Substring(9, 1).Equals(Claro.SIACU.Constants.ZeroNumber) || strICCID.Substring(9, 1).Equals(Claro.SIACU.Constants.TwoNumber)));
        }

        /// <summary>
        /// Método para validar Estado de Portabilidad.
        /// </summary>
        /// <param name="strTelefono">strTelefono</param>
        /// <returns>bool</returns>
        private static bool ValidateStatusPortability(string strIdSession, string strIdTransaction, string strTelefono, out string strPortabilityState)
        {
            string strPort = "";
            Entity.Common.GetPortability.PortabilityResponse objPortabilityResponse = new Entity.Common.GetPortability.PortabilityResponse()
            {
                ListPortability = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Portability>>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Common.GetPortability(strIdSession, strIdTransaction, strTelefono, out strPort); })
            };

            strPortabilityState = strPort;

            if (objPortabilityResponse.ListPortability.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método para validar Roaming
        /// </summary>
        /// <param name="strContratoID">strContratoID</param>
        /// <param name="strTelefono">strTelefono</param>
        /// /// <param name="strCustomerType">strCustomerType</param>
        /// <returns>bool</returns>
        private static bool ValidateRoaming(string strIdSession, string strIdTransaction, string strContratoID, string strTelefono, string strCustomerType)
        {
            bool ActiveRoaming;
            bool ClientVIP = true;
            int CodError = Claro.Constants.NumberZero;
            int IntCodApplication;
            int result = Claro.Constants.NumberZero;
            string CodAplication;
            string CodServiceRoaming;
            string desError = string.Empty;
            string ZonaEXC = string.Empty;
            string ZonaRM = string.Empty;
            string TypeClientVIP;
            string message = string.Empty;

            TypeClientVIP = KEY.AppSettings("strTypeClientVIP");
            CodAplication = KEY.AppSettings("ApplicationCodeRO");
            CodServiceRoaming = KEY.AppSettings("CodServiceRoaming");
            IntCodApplication = int.Parse(CodAplication);

            ActiveRoaming = Claro.Web.Logging.ExecuteMethod<bool>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetServicesCommercial(strIdSession, strIdTransaction, strContratoID, CodServiceRoaming, ref result, ref message); });
            if (ActiveRoaming)
            {
                ClientVIP = ValidateClientVIP(strIdSession, strIdTransaction, strTelefono);
                if (ClientVIP)
                {
                    Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.ShareRoaming>>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetShareRoaming(strIdSession, strIdTransaction, TypeClientVIP, IntCodApplication, 0, ref ZonaEXC, ref ZonaRM, ref CodError, ref desError); });
                    if (CodError == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.ShareRoaming>>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetShareRoaming(strIdSession, strIdTransaction, strCustomerType, IntCodApplication, 0, ref ZonaEXC, ref ZonaRM, ref CodError, ref desError); });
                    if (CodError == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método para validar Cliente  VIP
        /// </summary>
        /// <param name="strTelefono">strTelefono</param>
        /// <returns>bool</returns>
        private static bool ValidateClientVIP(string strIdSession, string strIdTransaction, string strTelefono)
        {
            string Query = string.Empty;
            string Message = string.Empty;
            string ZipCodePeru;
            string TelephoneInternational;

            ZipCodePeru = KEY.AppSettings("strZIPCodePeru");

            TelephoneInternational = ZipCodePeru + strTelefono;
            List<Claro.SIACU.Entity.Dashboard.Postpaid.ClientVIP> listClientVIP;

            listClientVIP = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.ClientVIP>>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.QueryClientVIP(strIdSession, strIdTransaction, TelephoneInternational, ref Message, ref Query); });

            if (listClientVIP != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método para verificar si el servicio es TFI.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="CodePlanTariff">Código de plan tarifario</param>
        /// <returns>Devuelve cadena flag de TFI.</returns>
        private static string GetFlagTFI(string strIdSession, string strIdTransaction, string CodePlanTariff)
        {
            string strFlagTFI = Claro.SIACU.Constants.Not;
            string strParameterPlanTFIPost = "";
            try
            {
                strParameterPlanTFIPost = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetParameter(strIdSession, strIdTransaction, KEY.AppSettings("gConstParametroPlanesTFI")); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }
            string[] ParameterPlanFixedPost = strParameterPlanTFIPost.Split(';');
            string[] ParameterTFIPost = ParameterPlanFixedPost[Claro.Constants.NumberZero].ToString().Split(',');
            foreach (string item in ParameterTFIPost) if (item == CodePlanTariff) strFlagTFI = Claro.SIACU.Constants.Yes;
            return strFlagTFI;
        }

        /// <summary>
        /// Método para obtener descripción de servicio TFI.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="CodePlanTariff">Código de plan tarifario</param>
        /// <returns>Devuelve cadena con descripción de servicio TFI.</returns>
        private static string GetTFI(string strIdSession, string strIdTransaction, string CodePlanTariff)
        {
            string strTFI = "";
            string strObtenerParametroTerminalTPI = "";
            try
            {
                strObtenerParametroTerminalTPI = KEY.AppSettings("gObtenerParametroTerminalTPI");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            string objItem = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetParameterTerminalTPI(strIdSession, strIdTransaction, Int32.Parse(strObtenerParametroTerminalTPI)); });


            strTFI = GetValueTFI(objItem, CodePlanTariff);
            return strTFI;
        }



        private static string GetValueTFI(string objItem, string CodePlanTariff)
        {
            string strTFI = "";
            bool isTFI = false;

            if (objItem != null)
            {
                string[] arrItem = objItem.Split(';');
                string[] arrSubItem, arrSubItems;
                for (int i = 0; i <= arrItem.Length - Claro.Constants.NumberTwo; i++)
                {
                    arrSubItem = arrItem[i].Split(':');
                    if (arrSubItem != null && !string.IsNullOrEmpty(arrSubItem[1].ToString()))
                    {
                        arrSubItems = arrSubItem[1].ToString().Split(',');
                        for (int p = 0; p <= arrSubItems.Length - 1; p++)
                        {
                            strTFI = "";
                            if (CodePlanTariff == (arrSubItems[p].Trim()).ToString())
                            {
                                strTFI = Claro.Constants.FormatSpace + Claro.SIACU.Constants.Rural;
                                isTFI = true;
                                break;
                            }
                        }
                    }
                    if (isTFI) break;
                }
            }

            return strTFI;

        }

        /// <summary>
        /// Método para obtener descripción de servicio TFI.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="CodePlanTariff">Código de plan tarifario</param>
        /// <returns>Devuelve si pertenece al tipo TPI para habilitar btn HistHR en el área de facturación.</returns>
        private static string GetTPI(string strIdSession, string strIdTransaction, string CodePlanTariff)
        {
            string strTPI = "";
            string strObtenerParametroTerminalTPI = "";
            try
            {
                strObtenerParametroTerminalTPI = KEY.AppSettings("gObtenerParametroTerminalTPI");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            string objItem = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetParameterTerminalTPI(strIdSession, strIdTransaction, Int32.Parse(strObtenerParametroTerminalTPI)); });

            bool isTPI = false;
            if (objItem != null)
            {
                string[] arrItem = objItem.Split(';');
                string[] arrSubItem, arrSubItems;
                for (int i = 0; i < arrItem.Length - Claro.Constants.NumberTwo; i++)
                {
                    arrSubItem = arrItem[i].Split(':');
                    if (arrSubItem != null && !string.IsNullOrEmpty(arrSubItem[1]) &&
                        (arrSubItem[0].Trim() == "TPI" || arrSubItem[0].Trim() == "Internet"))
                    {
                        arrSubItems = arrSubItem[1].ToString().Split(',');
                        for (int p = 0; p <= arrSubItems.Length - 1; p++)
                        {
                            if (CodePlanTariff == (arrSubItems[p].Trim()).ToString())
                            {
                                strTPI = "Ok";
                                isTPI = true;
                                break;
                            }
                            else
                            {
                                strTPI = "";
                            }
                        }
                    }
                    if (isTPI)
                    {
                        break;
                    }
                }
            }
            return strTPI;
        }

        /// <summary>
        /// Método para verificar el tipo de servicio.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="CodePlanTariff">Código de plan tarifario</param>
        /// <returns>Devuelve cadena nombre de tipo de servicio.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService.TypeServiceResponse ValidateTypeService(Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService.TypeServiceRequest objTypeServiceRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetTypeService.TypeServiceResponse objTypeServiceResponse = new POSTPAID.GetTypeService.TypeServiceResponse();
            string strGetParameterTerminalTPI, strGetParamTFIPost, strgConsEnableChangeTransferNumberOnlyTFI, objItem, objItem2;

            String[] strCHCTNSoloTFI, strCHCTNSoloTFI2, TFIPost, TFIPost4, TFIPost2, TFIPost3, arrItem, arrSubItem, arrSubItems;

            strGetParameterTerminalTPI = KEY.AppSettings("gObtenerParametroTerminalTPI");
            strGetParamTFIPost = KEY.AppSettings("gObtenerParametroSoloTFIPostpago");
            strgConsEnableChangeTransferNumberOnlyTFI = KEY.AppSettings("gConsHabilitaCambTrasladoNumeroCambPlanSoloTFI");

            objItem = Claro.Web.Logging.ExecuteMethod<string>(objTypeServiceRequest.Audit.Session, objTypeServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetParameterTerminalTPI(objTypeServiceRequest.Audit.Session, objTypeServiceRequest.Audit.Transaction, Int32.Parse(strGetParameterTerminalTPI)); });
            objItem2 = Claro.Web.Logging.ExecuteMethod<string>(objTypeServiceRequest.Audit.Session, objTypeServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetParameterTerminalTFI(objTypeServiceRequest.Audit.Session, objTypeServiceRequest.Audit.Transaction, Int32.Parse(strGetParamTFIPost)); });

            int flagFound = Claro.Constants.NumberZero;
            objTypeServiceResponse.NameTypeService = Claro.SIACU.Constants.PostpaidMajusculeSpanish;

            if (objItem != null)
            {
                arrItem = objItem.Split(';');
                for (int i = 0; i <= arrItem.Length - Claro.Constants.NumberTwo; i++)
                {
                    arrSubItem = arrItem[i].Split(':');
                    if (arrSubItem != null && !string.IsNullOrEmpty(arrSubItem[1].ToString()))
                    {
                        arrSubItems = arrSubItem[1].Split(',');
                        for (int p = 0; p <= arrSubItems.Length - 1; p++)
                        {
                            if (objTypeServiceRequest.CodePlanTariff.Equals((arrSubItems[p].Trim())))
                            {
                                objTypeServiceResponse.NameTypeService = arrSubItem[0].ToString();
                                flagFound = Claro.Constants.NumberOne;
                                break;
                            }
                            else
                            {
                                objTypeServiceResponse.NameTypeService = Claro.SIACU.Constants.PostpaidMajusculeSpanish;
                                flagFound = Claro.Constants.NumberZero;
                            }
                        }

                    }
                }
            }

            if (!string.IsNullOrEmpty(objItem2))
            {

                TFIPost = objItem2.Split(';');

                if (flagFound == Claro.Constants.NumberZero)
                {
                    for (int p = 0; p <= TFIPost.Length - Claro.Constants.NumberTwo; p++)
                    {
                        TFIPost2 = TFIPost[p].Split(',');
                        for (int t = 0; t <= TFIPost2.Length - Claro.Constants.NumberOne; t++)
                        {
                            if (objTypeServiceRequest.CodePlanTariff.Equals(TFIPost2[t].Trim()))
                            {
                                objTypeServiceResponse.NameTypeService = KEY.AppSettings("strTypeServiceFijoPost");
                                flagFound = Claro.Constants.NumberTwo;
                                break;
                            }
                            else
                            {
                                objTypeServiceResponse.NameTypeService = Claro.SIACU.Constants.PostpaidMajusculeSpanish;
                            }
                        }
                    }
                }

                strCHCTNSoloTFI = strgConsEnableChangeTransferNumberOnlyTFI.Split(';');
                strCHCTNSoloTFI2 = strCHCTNSoloTFI[0].Split(',');
                TFIPost3 = objItem2.ToString().Split(';');

                for (int tf = 0; tf <= TFIPost3.Length - Claro.Constants.NumberTwo; tf++)
                {
                    TFIPost4 = TFIPost3[tf].ToString().Split(',');
                    for (int t = 0; t <= TFIPost4.Length - Claro.Constants.NumberOne; t++)
                    {
                        if (objTypeServiceRequest.CodePlanTariff.Equals(TFIPost4[t].Trim()))
                        {
                            for (int t2 = 0; t2 <= strCHCTNSoloTFI2.Length - Claro.Constants.NumberOne; t2++)
                            {
                                if (objTypeServiceRequest.CodePlanTariff.Equals(strCHCTNSoloTFI2[t2].Trim()))
                                {
                                    objTypeServiceResponse.NameTypeService = KEY.AppSettings("strTypeServiceOnlyTFI");
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return objTypeServiceResponse;
        }

        /// <summary>
        /// Método para obtener indicador de 2Play.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="CodePlanTariff">Código de plan tarifario</param>
        /// <returns>Devuelve cadena de indicador 2Play.</returns>
        private static string Get2PLAY(string strIdSession, string strIdTransaction, string CodePlanTariff)
        {
            string str2PLAY = "";
            string strObtenerParametroSoloTFIPostpago = "";
            string strConstSoloTFIPostpago2PLAY = "";
            try
            {
                strObtenerParametroSoloTFIPostpago = KEY.AppSettings("gObtenerParametroSoloTFIPostpago");
                strConstSoloTFIPostpago2PLAY = KEY.AppSettings("gConstSoloTFIPostpago2PLAY");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            string objItemTFI = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetParameterTerminalTFI(strIdSession, strIdTransaction, Int32.Parse(strObtenerParametroSoloTFIPostpago)); });

            Claro.Web.Logging.Info(strIdSession, strIdTransaction, objItemTFI);
            string strTFI2PLAY = strConstSoloTFIPostpago2PLAY;
            string[] strTFI_2PLAY = strTFI2PLAY.ToString().Split(';');
            string[] strSubTFI_2PLAY = strTFI_2PLAY[0].ToString().Split(',');

            if (!String.IsNullOrEmpty(objItemTFI))
            {
                string[] arrItem = objItemTFI.Split(';');
                for (int i = 0; i < arrItem.Length - 1; i++)
                {
                    string[] arrSubItem = arrItem[i].Split(',');
                    if (arrSubItem != null)
                    {
                        for (int p = 0; p < arrSubItem.Length; p++)
                        {
                            if (CodePlanTariff == (arrSubItem[p].Trim()).ToString())
                            {
                                for (int m = 0; m < strSubTFI_2PLAY.Length; m++)
                                {
                                    if (CodePlanTariff == strSubTFI_2PLAY[m].ToString())
                                    {
                                        str2PLAY = Claro.Constants.LetterS;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(str2PLAY))
                {
                    str2PLAY = Claro.Constants.LetterN;
                }
            }
            return str2PLAY;
        }

        /// <summary>
        /// Método para obtener el monto de disputa, comportamiento de pago y Datos Parámetro TPI del cliente.
        /// </summary>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve objeto PaymentCollectionResponse con información de monto en disputa.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCollection.PaymentCollectionResponse GetPaymentCollection(Claro.SIACU.Entity.Dashboard.Postpaid.GetPaymentCollection.PaymentCollectionRequest objPaymentCollectionRequest)
        {
            POSTPAID.GetPaymentCollection.PaymentCollectionResponse objPaymentCollectionResponse = null;
            string strParameterId = "";
            try
            {
                strParameterId = KEY.AppSettings("gObtenerParametroTerminalTPI");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPaymentCollectionRequest.Audit.Session, objPaymentCollectionRequest.Audit.Transaction, ex.Message);
            }

            try
            {
                objPaymentCollectionResponse = new POSTPAID.GetPaymentCollection.PaymentCollectionResponse()
                {
                    ObjPaymentCollection = new POSTPAID.PaymentCollection()
                    {
                        AMOUT_DISPUTE = Claro.Web.Logging.ExecuteMethod<string>(objPaymentCollectionRequest.Audit.Session, objPaymentCollectionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetAmountDispute(objPaymentCollectionRequest.Audit.Session, objPaymentCollectionRequest.Audit.Transaction, Convert.ToDecimal(objPaymentCollectionRequest.CustomerId)); }),
                        //     PAYMENT_BEHAVIOR = Claro.Web.Logging.ExecuteMethod<string>(objPaymentCollectionRequest.Audit.Session, objPaymentCollectionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPaymentBehavior(objPaymentCollectionRequest.Audit.Session, objPaymentCollectionRequest.Audit.Transaction, Convert.ToInt64(objPaymentCollectionRequest.CustomerId)); }),
                        PAYMENT_BEHAVIOR = Claro.Web.Logging.ExecuteMethod<string>(objPaymentCollectionRequest.Audit.Session, objPaymentCollectionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPaymentBehaviorTobe(objPaymentCollectionRequest.Audit, objPaymentCollectionRequest.csIdPub); }),

                        PARAMETER_DATA = Claro.Web.Logging.ExecuteMethod<POSTPAID.Parameter>(objPaymentCollectionRequest.Audit.Session, objPaymentCollectionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTPIParameterTerminal(objPaymentCollectionRequest.Audit.Session, objPaymentCollectionRequest.Audit.Transaction, Convert.ToDecimal(strParameterId)); }),
                        PAYMENT_FORM = Claro.Web.Logging.ExecuteMethod<string>(objPaymentCollectionRequest.Audit.Session, objPaymentCollectionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPaymentMethodTobe(objPaymentCollectionRequest.Audit, objPaymentCollectionRequest.csIdPub); })
                    }
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPaymentCollectionRequest.Audit.Session, objPaymentCollectionRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            return objPaymentCollectionResponse;
        }

        /// <summary>
        /// Método para obtener el detalle del monto de disputa.
        /// </summary>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve objeto DetailAmountDisputeResponse con el listado de detalle de montos en disputa.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAmountDispute.DetailAmountDisputeResponse GetDetailAmountDispute(Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAmountDispute.DetailAmountDisputeRequest objDetailAmountDisputeRequest)
        {
            Entity.Dashboard.Postpaid.GetDetailAmountDispute.DetailAmountDisputeResponse objDetailAmountDisputeResponse = new POSTPAID.GetDetailAmountDispute.DetailAmountDisputeResponse()
            {
                ListDetailAmountDispute = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.DetailAmountDispute>>(objDetailAmountDisputeRequest.Audit.Session, objDetailAmountDisputeRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailAmountDispute(objDetailAmountDisputeRequest.Audit.Session, objDetailAmountDisputeRequest.Audit.Transaction, Convert.ToDecimal(objDetailAmountDisputeRequest.CustomerId)); })
            };

            return objDetailAmountDisputeResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de forma de pago del cliente.
        /// </summary>
        /// <param name="objAffiliationToDebitRequest">Código del cliente</param>
        /// <returns>Devuelve objeto AffiliationToDebitResponse con información de detalle forma de pago.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit.AffiliationToDebitResponse GetAffiliationToDebit(Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit.AffiliationToDebitRequest objAffiliationToDebitRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetAffiliationToDebit.AffiliationToDebitResponse objAffiliationToDebitResponse = new Entity.Dashboard.Postpaid.GetAffiliationToDebit.AffiliationToDebitResponse()
            {
                ObjMethodPayment = Claro.Web.Logging.ExecuteMethod<POSTPAID.AffiliationToDebit>(objAffiliationToDebitRequest.Audit.Session, objAffiliationToDebitRequest.Audit.Transaction,
                        () => { return Data.Dashboard.Postpaid.GetPaymentMethodDetail(objAffiliationToDebitRequest.Audit, objAffiliationToDebitRequest.Audit.Transaction, objAffiliationToDebitRequest.CustomerId, objAffiliationToDebitRequest.flagPlataforma, objAffiliationToDebitRequest.csIdPub); })
            };


            return objAffiliationToDebitResponse;

        }

        /// <summary>
        /// Método para obtener el monitoreo de casos.
        /// </summary>
        /// <param name="objMonitoringCasesRequest">Contiene id de caso, rango de fechas y cuenta de cliente</param>
        /// <returns>Devuelve objeto MonitoringCasesResponse con el listado de monitoreos de casos.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases.MonitoringCasesResponse GetMonitoringCases(Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases.MonitoringCasesRequest objMonitoringCasesRequest)
        {
            string strStartRow = "";
            string strEndRow = "";
            Claro.SIACU.Entity.Dashboard.Postpaid.GetMonitoringCases.MonitoringCasesResponse objMonitoringCasesResponse = null;
            List<POSTPAID.MonitoringCases> listMonitoringCases;
            try
            {
                strStartRow = KEY.AppSettings("strInicioFlagMonitoreoCasos");
                strEndRow = KEY.AppSettings("strFinFlagMonitoreoCasos");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMonitoringCasesRequest.Audit.Session, objMonitoringCasesRequest.Audit.Transaction, ex.Message);
            }

            try
            {
                objMonitoringCasesRequest.DateFrom = objMonitoringCasesRequest.DateFrom == "" ? KEY.AppSettings("strFechaDesdeCierreReaperturas") : objMonitoringCasesRequest.DateFrom;
                objMonitoringCasesRequest.DateTo = objMonitoringCasesRequest.DateTo == "" ? DateTime.Now.ToShortDateString() : objMonitoringCasesRequest.DateTo;

                listMonitoringCases = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.MonitoringCases>>(objMonitoringCasesRequest.Audit.Session, objMonitoringCasesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetMonitoringCases(objMonitoringCasesRequest.Audit.Session, objMonitoringCasesRequest.Audit.Transaction, Convert.ToDecimal(strStartRow), Convert.ToDecimal(strEndRow), objMonitoringCasesRequest.CaseId, objMonitoringCasesRequest.CustomerAccount, objMonitoringCasesRequest.DateFrom, objMonitoringCasesRequest.DateTo); });
                objMonitoringCasesResponse = new Entity.Dashboard.Postpaid.GetMonitoringCases.MonitoringCasesResponse()
                {
                    ListMonitoringCases = listMonitoringCases
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMonitoringCasesRequest.Audit.Session, objMonitoringCasesRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            return objMonitoringCasesResponse;
        }

        /// <summary>
        /// Método para obtener anotaciones.
        /// </summary>
        /// <param name="objAnnotationsRequest">Contiene id de customer, contrato, número de tickler y estado.</param>
        /// <returns>Devuelve objeto AnnotationsResponse con el listado de anotaciones.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse GetAnnotations(Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsRequest objAnnotationsRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse objAnnotationsResponse;
            List<POSTPAID.Annotation> listAnnotation;
            try
            {
                if (objAnnotationsRequest.Contract == string.Empty) objAnnotationsRequest.Contract = null;
                if (objAnnotationsRequest.Type == Claro.Constants.LetterC)
                {
                    objAnnotationsRequest.Contract = null;
                }
                else
                {
                    objAnnotationsRequest.CustomerId = null;
                }
                listAnnotation = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Annotation>>(objAnnotationsRequest.Audit.Session, objAnnotationsRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetAnnotations(objAnnotationsRequest.Audit.Session, objAnnotationsRequest.Audit.Transaction, objAnnotationsRequest.CustomerId, objAnnotationsRequest.Contract, objAnnotationsRequest.Status, objAnnotationsRequest.NumberTickler, objAnnotationsRequest.Type); });
                objAnnotationsResponse = new Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse();
                objAnnotationsResponse.ListAnnotation = listAnnotation;
            }
            catch (Exception ex)
            {
                objAnnotationsResponse = null;
                Claro.Web.Logging.Error(objAnnotationsRequest.Audit.Session, objAnnotationsRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            return objAnnotationsResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de anotaciones.
        /// </summary>
        /// <param name="objDetailAnnotationsRequest">Contiene id de cliente y número de tickler.</param>
        /// <returns>Devuelve objeto DetailAnnotation con información de detalle de anotaciones.</returns>
        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAnnotation.GetDetailAnnotationResponse GetDetailAnnotation(Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAnnotation.GetDetailAnnotationRequest objDetailAnnotationsRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetDetailAnnotation.GetDetailAnnotationResponse objDetailAnnotationResponse = null;
            POSTPAID.DetailAnnotation DetailAnnotation;
            try
            {
                DetailAnnotation = Claro.Web.Logging.ExecuteMethod<POSTPAID.DetailAnnotation>(objDetailAnnotationsRequest.Audit.Session, objDetailAnnotationsRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailAnnotation(objDetailAnnotationsRequest.Audit.Session, objDetailAnnotationsRequest.Audit.Transaction, objDetailAnnotationsRequest.CustomerId, objDetailAnnotationsRequest.NumberTickler); });
                objDetailAnnotationResponse = new Entity.Dashboard.Postpaid.GetDetailAnnotation.GetDetailAnnotationResponse()
                {
                    DetailAnnotation = DetailAnnotation
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDetailAnnotationsRequest.Audit.Session, objDetailAnnotationsRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            return objDetailAnnotationResponse;
        }

        /// <summary>
        /// Método para obtener información de la garantía.
        /// </summary>
        /// <param name="objGetWarrantyRequest">Contiene código y usuario de aplicación, tipo y número de documento de identidad e id de cliente.</param>
        /// <returns>Devuelve objeto Warranty con información de garantía.</returns>
        public static Entity.Dashboard.Postpaid.GetWarranty.WarrantyResponse GetWarranty(Entity.Dashboard.Postpaid.GetWarranty.WarrantyRequest objGetWarrantyRequest)
        {

            Entity.Dashboard.Postpaid.GetWarranty.WarrantyResponse objGetWarrantyResponse = new Entity.Dashboard.Postpaid.GetWarranty.WarrantyResponse();
            POSTPAID.Warranty objWarranty = new POSTPAID.Warranty();

            string strFlagSAPGarantias = "";
            string strTipoConsulta = "";
            string strOrigen = "";
            string strDgTotal = "";
            try
            {
                strFlagSAPGarantias = KEY.AppSettings("strFlagSAPGarantias");
                strTipoConsulta = KEY.AppSettings("strTipoConsultaCustomerId");
                strOrigen = KEY.AppSettings("constOrigenFacturacion");
                strDgTotal = KEY.AppSettings("constGarantiaTotal");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetWarrantyRequest.Audit.Session, objGetWarrantyRequest.Audit.Transaction, ex.Message);
            }

            try
            {
                if (String.Equals(strFlagSAPGarantias, Claro.Constants.NumberOneString, StringComparison.OrdinalIgnoreCase) && objGetWarrantyRequest.Aplication != Claro.SIACU.Constants.HFC)
                {
                    objGetWarrantyRequest.DocumentType = "";
                    objGetWarrantyRequest.DocumentNumber = "";
                    objGetWarrantyResponse.ObjWarranty = Claro.Web.Logging.ExecuteMethod<POSTPAID.Warranty>(objGetWarrantyRequest.Audit.Session, objGetWarrantyRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetWarranty(objGetWarrantyRequest.Audit.Session, objGetWarrantyRequest.Audit.Transaction, objGetWarrantyRequest.CodApplication, objGetWarrantyRequest.UserApplication, strTipoConsulta,
                                                                                             objGetWarrantyRequest.DocumentType, objGetWarrantyRequest.DocumentNumber, strOrigen, objGetWarrantyRequest.CustomerId,
                                                                                             strDgTotal);
                    });
                }
                else
                {
                    string srtTypeDoc = Data.Dashboard.Postpaid.GetCodeIdentityCard(objGetWarrantyRequest.Audit.Session, objGetWarrantyRequest.Audit.Transaction, objGetWarrantyRequest.DocumentType);
                    if (!string.IsNullOrEmpty(srtTypeDoc))
                    {
                        objWarranty.lstCabAccountWarranty = new List<POSTPAID.Warranty>();
                        objWarranty.lstDebAccountWarranty = new List<POSTPAID.Warranty>();
                        string strFechaIni = Claro.SIACU.Constants.DateDefaultWarranty;
                        string strFechaFin = DateTime.Now.ToString("yyyyMMdd");

                        objWarranty.lstCabAccountWarranty = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Warranty>>(objGetWarrantyRequest.Audit.Session, objGetWarrantyRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetWarrantyWS(objGetWarrantyRequest.Audit.Session, objGetWarrantyRequest.Audit.Transaction, srtTypeDoc, objGetWarrantyRequest.DocumentNumber, Claro.SIACU.Constants.DL, strFechaIni, strFechaFin); });
                        objWarranty.lstDebAccountWarranty = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Warranty>>(objGetWarrantyRequest.Audit.Session, objGetWarrantyRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetWarrantyWS(objGetWarrantyRequest.Audit.Session, objGetWarrantyRequest.Audit.Transaction, srtTypeDoc, objGetWarrantyRequest.DocumentNumber, Claro.SIACU.Constants.DE, strFechaIni, strFechaFin); });
                        objGetWarrantyResponse.ObjWarranty = objWarranty;

                    }

                }
            }
            catch (Exception ex)
            {
                objGetWarrantyResponse = null;
                Claro.Web.Logging.Error(objGetWarrantyRequest.Audit.Session, objGetWarrantyRequest.Audit.Transaction, ex.Message);
            }

            return objGetWarrantyResponse;
        }

        /// <summary>
        /// Método para obtener información de bolsa compartida.
        /// </summary>
        /// <param name="objGetSharedBagRequest">Contiene cuenta, id de cliente y teléfono. </param>
        /// <returns>Devuelve objeto SharedBagResponse con información de bolsa compartida.</returns>
        public static Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse GetSharedBag(Entity.Dashboard.Postpaid.GetSharedBag.SharedBagRequest objGetSharedBagRequest)
        {
            POSTPAID.SharedBag objSharedBag;
            Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse objGetSharedBagResponse;
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstService;

            if (string.IsNullOrEmpty(objGetSharedBagRequest.Telephone))
            {
                objGetSharedBagResponse = new Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse();
                lstService = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Service>>(objGetSharedBagRequest.Audit.Session, objGetSharedBagRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTelephoneNumbersPost(objGetSharedBagRequest.Audit.Session, objGetSharedBagRequest.Audit.Transaction, objGetSharedBagRequest.Customerid); });

                if (lstService != null && lstService.Count > 0)
                {
                    List<POSTPAID.SharedBag> lstSharedBagLine = new List<POSTPAID.SharedBag>();
                    foreach (Claro.SIACU.Entity.Dashboard.Postpaid.Service item in lstService)
                    {
                        lstSharedBagLine = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.SharedBag>>(objGetSharedBagRequest.Audit.Session, objGetSharedBagRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetSharedBagLine(objGetSharedBagRequest.Account, objGetSharedBagRequest.Application, objGetSharedBagRequest.IdTransaction, objGetSharedBagRequest.IPAplication, objGetSharedBagRequest.UsrAplication, item.NRO_CELULAR); });
                        if (lstSharedBagLine.Count > 0)
                        {
                            lstSharedBagLine.AddRange(lstSharedBagLine);
                        }
                    }

                    if (lstSharedBagLine.Count > 0)
                    {
                        objGetSharedBagResponse.ListSharedBagLine = lstSharedBagLine;
                    }

                }
                else
                {
                    objSharedBag = Claro.Web.Logging.ExecuteMethod<POSTPAID.SharedBag>(objGetSharedBagRequest.Audit.Session, objGetSharedBagRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetSharedBag(objGetSharedBagRequest.Audit.Session, objGetSharedBagRequest.Account, objGetSharedBagRequest.Application, objGetSharedBagRequest.Audit.Transaction, objGetSharedBagRequest.IPAplication, objGetSharedBagRequest.UsrAplication, objGetSharedBagRequest.Customerid, objGetSharedBagRequest.Telephone); });
                    objGetSharedBagResponse.ListSharedBag = objSharedBag.lstAccountSharedBag;
                }
            }
            else
            {
                objGetSharedBagResponse = new Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse();

                objSharedBag = Data.Dashboard.Postpaid.GetSharedBag(objGetSharedBagRequest.Audit.Session, objGetSharedBagRequest.Account, objGetSharedBagRequest.Application, objGetSharedBagRequest.Audit.Transaction, objGetSharedBagRequest.IPAplication, objGetSharedBagRequest.UsrAplication, objGetSharedBagRequest.Customerid, objGetSharedBagRequest.Telephone);
                objGetSharedBagResponse.ListSharedBag = objSharedBag.lstAccountSharedBag;

                if (!int.Equals(objSharedBag.lstAccountSharedBag.Count, 0))
                {
                    lstService = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Service>>(objGetSharedBagRequest.Audit.Session, objGetSharedBagRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTelephoneNumbersPost(objGetSharedBagRequest.Audit.Session, objGetSharedBagRequest.Audit.Transaction, objGetSharedBagRequest.Customerid); });

                    if (lstService.Count > 0)
                    {
                        List<POSTPAID.SharedBag> lstSharedBagLine = new List<POSTPAID.SharedBag>();
                        foreach (Claro.SIACU.Entity.Dashboard.Postpaid.Service item in lstService)
                        {
                            lstSharedBagLine = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.SharedBag>>(objGetSharedBagRequest.Audit.Session, objGetSharedBagRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetSharedBagLine(objGetSharedBagRequest.Account, objGetSharedBagRequest.Application, objGetSharedBagRequest.IdTransaction, objGetSharedBagRequest.IPAplication, objGetSharedBagRequest.UsrAplication, item.NRO_CELULAR); });
                            if (lstSharedBagLine.Count > 0)
                            {
                                lstSharedBagLine.AddRange(lstSharedBagLine);
                            }
                        }

                        if (lstSharedBagLine.Count > 0)
                        {
                            objGetSharedBagResponse.ListSharedBagLine = lstSharedBagLine;
                        }
                    }
                }
            }
            return objGetSharedBagResponse;
        }

        /// <summary>
        /// Método para obtener la relación de planes.
        /// </summary>
        /// <param name="objGetRelationPlansRequest">Contiene tipo de búsqueda, orden, bolsa compartida y ciclo.</param>
        /// <returns>Devuelve objeto RelationPlansResponse con información de la relación de planes.</returns>
        public static Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlansResponse GetRelationPlans(Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost.MassiveRequest objGetRelationPlansRequest)
        {
            Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlansResponse objGetRelationPlansResponse = null;

            objGetRelationPlansRequest.TypeSearch = Claro.Constants.LetterC;
            objGetRelationPlansRequest.Order = Claro.Constants.LetterN;
            objGetRelationPlansRequest.AscDes = Claro.Constants.LetterA;
            objGetRelationPlansRequest.ShBag = Claro.Constants.NumberZeroString;
            objGetRelationPlansRequest.Cycle = Claro.Constants.FormatDoubleZero;

            List<Entity.Dashboard.Postpaid.Bag> objBag = null;
            List<Entity.Dashboard.Postpaid.BagDetail> lstBag = null;

            try
            {
                objBag = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Bag>>(objGetRelationPlansRequest.Audit.Session, objGetRelationPlansRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetBag(objGetRelationPlansRequest.Audit.Session, objGetRelationPlansRequest.Audit.Transaction, objGetRelationPlansRequest.TypeSearch, objGetRelationPlansRequest.Account, objGetRelationPlansRequest.Order, objGetRelationPlansRequest.AscDes, objGetRelationPlansRequest.Cycle); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRelationPlansRequest.Audit.Session, objGetRelationPlansRequest.Audit.Transaction, ex.Message);
            }

            if (objBag != null)
            {
                objGetRelationPlansResponse = (objGetRelationPlansResponse == null) ? new Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlansResponse() : objGetRelationPlansResponse;
                foreach (var bag in objBag)
                {
                    if (string.IsNullOrEmpty(bag.DESACTIVA))
                    {
                        objGetRelationPlansResponse.ObjBag = new POSTPAID.Bag()
                        {
                            FLAG_DESACTIVA = Claro.Constants.NumberOne,
                            TIPO_BOLSA = bag.TIPO_BOLSA,
                            CARGO_FIJO = bag.CARGO_FIJO,
                            MIN_SOL = Convert.ToString(Convert.ToDouble(bag.MINUTOS1) + Convert.ToDouble(bag.MINUTOS2)),
                            FECHA_ACTIVACION = bag.FECHA_ACTIVACION,
                            CANTIDAD_LINEAS = bag.CANTIDAD_LINEAS,
                            DESACTIVA = bag.DESACTIVA
                        };
                        break;
                    }
                }
            }

            try
            {
                lstBag = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.BagDetail>>(objGetRelationPlansRequest.Audit.Session, objGetRelationPlansRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailBag(objGetRelationPlansRequest.Audit.Session, objGetRelationPlansRequest.Audit.Transaction, objGetRelationPlansRequest.TypeSearch, objGetRelationPlansRequest.Account, objGetRelationPlansRequest.Order, objGetRelationPlansRequest.AscDes, objGetRelationPlansRequest.ShBag, objGetRelationPlansRequest.Cycle); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRelationPlansRequest.Audit.Session, objGetRelationPlansRequest.Audit.Transaction, ex.Message);
            }

            if (lstBag != null)
            {
                List<POSTPAID.BagDetail> lstBagDetail = new List<POSTPAID.BagDetail>();
                foreach (POSTPAID.BagDetail item in lstBag)
                {
                    if (string.IsNullOrEmpty(item.DESACTIVA))
                    {
                        lstBagDetail.Add(new POSTPAID.BagDetail()
                        {
                            FLAG_DESACTIVA = Claro.Constants.NumberOne,
                            TIPO_BOLSA = item.TIPO_BOLSA,
                            CARGO_FIJO = item.CARGO_FIJO,
                            MIN_SOL = Convert.ToString(Convert.ToDouble(item.MINUTOS1) + Convert.ToDouble(item.MINUTOS2)),
                            FECHA_ACTIVACION = item.FECHA_ACTIVACION,
                            CANTIDAD_LINEAS = item.CANTIDAD_LINEAS,
                            DESACTIVA = item.DESACTIVA
                        });
                    }
                }
                if (lstBagDetail.Count > 0)
                {
                    objGetRelationPlansResponse = (objGetRelationPlansResponse == null) ? new Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlansResponse() : objGetRelationPlansResponse;
                    objGetRelationPlansResponse.ListBagDetail = lstBagDetail;
                }
            }

            try
            {
                objGetRelationPlansResponse = (objGetRelationPlansResponse == null) ? new Entity.Dashboard.Postpaid.GetRelationPlans.RelationPlansResponse() : objGetRelationPlansResponse;

                if (objGetRelationPlansRequest.CustomerType.ToUpper() != "BUSINESS" && objGetRelationPlansRequest.CustomerType.ToUpper() != "B2E")
                    objGetRelationPlansResponse.ListPlan = GetMassiveLines(objGetRelationPlansRequest);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetRelationPlansRequest.Audit.Session, objGetRelationPlansRequest.Audit.Transaction, ex.Message);
            }
            return objGetRelationPlansResponse;
        }

        /// <summary>
        /// Método para obtener la relación de planes.
        /// </summary>
        /// <param name="objRelationPlansRequest">Contiene tipo de búsqueda, orden, bolsa compartida y ciclo.</param>
        /// <returns>Devuelve objeto PlanMasivoResponse con información de la relación de planes masivo.</returns>
        public static POSTPAID.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost.MassiveResponse GetMassiveLines(POSTPAID.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost.MassiveRequest objRelationPlansRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<POSTPAID.GetRelationPlans.RelationPlanMassive.GetTabPlanesMassivePost.MassiveResponse>(objRelationPlansRequest.Audit.Session, objRelationPlansRequest.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetPlan(objRelationPlansRequest); });
        }

        /// <summary>
        /// Método para obtener la relación de Solicitudes.
        /// </summary>
        /// <param name="objSolicitudeRequests">Contiene las solicitudes.</param>
        /// <returns>Devuelve objeto SolicitudeResponse con información de las Solicitudes.</returns>
        public static POSTPAID.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeResponse GetSolicitude(POSTPAID.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeRequests objSolicitudeRequests)
        {
            return Claro.Web.Logging.ExecuteMethod<POSTPAID.GetRelationPlans.RelationPlanCorporate.GetSolicitude.SolicitudeResponse>(objSolicitudeRequests.Audit.Session, objSolicitudeRequests.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetSolicitude(objSolicitudeRequests); });
        }

        /// <summary>
        /// Método para registrar la Solicitud.
        /// </summary>
        /// <param name="objRegisterSolicitudeRequests">Registra las solicitud.</param>
        /// <returns>Devuelve objeto RegisterSolicitudeResponse con información de las Solicitudes.</returns>
        public static POSTPAID.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeResponse RegisterSolicitude(POSTPAID.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeRequests objRegisterSolicitudeRequests)
        {
            return Claro.Web.Logging.ExecuteMethod<POSTPAID.GetRelationPlans.RelationPlanCorporate.RegisterSolicitude.RegisterSolicitudeResponse>(objRegisterSolicitudeRequests.Audit.Session, objRegisterSolicitudeRequests.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.RegisterSolicitude(objRegisterSolicitudeRequests); });
        }

        public static POSTPAID.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeResponse GetDocumentSolicitude(POSTPAID.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeRequests objDocumentSolicitudeRequests)
        {
            return Claro.Web.Logging.ExecuteMethod<POSTPAID.GetRelationPlans.RelationPlanCorporate.GetDocumentSolicitude.DocumentSolicitudeResponse>(objDocumentSolicitudeRequests.Audit.Session, objDocumentSolicitudeRequests.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetDocumentSolicitude(objDocumentSolicitudeRequests); });
        }

        /// <summary>
        /// Método para obtener pin y puk.
        /// </summary>
        /// <param name="objPinPukRequest">Contiene tipo y cuenta.</param>
        /// <returns>Devuelve objeto PinPukResponse con información de Pin y puk.</returns>
        public static Entity.Dashboard.Postpaid.GetPinPuk.PinPukResponse GetPinPuk(Entity.Dashboard.Postpaid.GetPinPuk.PinPukRequest objPinPukRequest)
        {
            Entity.Dashboard.Postpaid.GetPinPuk.PinPukResponse objPinPukResponse = new Entity.Dashboard.Postpaid.GetPinPuk.PinPukResponse()
            {
                ListPinPuk = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.PinPuk>>(objPinPukRequest.Audit.Session, objPinPukRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPinPuk(objPinPukRequest.Audit.Session, objPinPukRequest.Audit.Transaction, objPinPukRequest.Account, objPinPukRequest.Type); })
            };
            return objPinPukResponse;
        }

        /// <summary>
        /// Método para obtener el contrato suspendido.
        /// </summary>
        /// <param name="objSuspendedContractRequest">Contiene rango de fechas e id de razón</param>
        /// <returns>Devuelve objeto SuspendedContractResponse con información de contrato suspendido.</returns>
        public static Entity.Dashboard.Postpaid.GetSuspendedContract.SuspendedContractResponse GetSuspendedContract(Entity.Dashboard.Postpaid.GetSuspendedContract.SuspendedContractRequest objSuspendedContractRequest)
        {
            Entity.Dashboard.Postpaid.GetSuspendedContract.SuspendedContractResponse objSuspendedContractResponse = new Entity.Dashboard.Postpaid.GetSuspendedContract.SuspendedContractResponse()
            {
                ListContract = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Contract>>(objSuspendedContractRequest.Audit.Session, objSuspendedContractRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetSuspendedContract(objSuspendedContractRequest.Audit.Session, objSuspendedContractRequest.Audit.Transaction, objSuspendedContractRequest.StartDate, objSuspendedContractRequest.EndDate, objSuspendedContractRequest.ReasonID); })
            };
            return objSuspendedContractResponse;
        }

        /// <summary>
        /// Método para obtener el listado de motivos de suspensión.
        /// </summary>
        /// <param name="objSuspendedTypeRequest">No tiene contenido.</param>
        /// <returns>Devuelve objeto SuspendedTypeResponse con listado de motivos de suspensión.</returns>
        public static Entity.Common.GetSuspendedType.SuspendedTypeResponse GetSuspendedType(Entity.Common.GetSuspendedType.SuspendedTypeRequest objSuspendedTypeRequest)
        {
            Entity.Common.GetSuspendedType.SuspendedTypeResponse objSuspendedTypeResponse = new Entity.Common.GetSuspendedType.SuspendedTypeResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objSuspendedTypeRequest.Audit.Session, objSuspendedTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("ListaMotivoSuspension"); })
            };
            return objSuspendedTypeResponse;
        }

        /// <summary>
        /// Método para obtener el listado de tipo de anotaciones.
        /// </summary>
        /// <param name="objAnnotationTypeRequest">No tiene contenido.</param>
        /// <returns>Devuelve objeto con el listado de tipo de anotaciones.</returns>
        public static Entity.Common.GetAnnotationType.AnnotationTypeResponse GetAnnotationType(Entity.Common.GetAnnotationType.AnnotationTypeRequest objAnnotationTypeRequest)
        {
            Entity.Common.GetAnnotationType.AnnotationTypeResponse objAnnotationTypeResponse = new Entity.Common.GetAnnotationType.AnnotationTypeResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objAnnotationTypeRequest.Audit.Session, objAnnotationTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("ListaTipoAnotaciones"); })
            };

            objAnnotationTypeResponse.ListItem.ForEach(x =>
            {

                if (objAnnotationTypeRequest.plataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase)) x.Code = x.Code.Split('|')[1].ToString();
                else x.Code = x.Code.Split('|')[0].ToString();
            });

            return objAnnotationTypeResponse;
        }

        /// <summary>
        /// Método para obtener las subcuentas.
        /// </summary>
        /// <param name="objSubAccountRequest">Contiene el id de cliente.</param>
        /// <returns>Devuelve objeto SubAccountResponse con el listado de subcuentas.</returns>
        public static Entity.Dashboard.Postpaid.GetSubAccount.SubAccountResponse GetSubAccount(Entity.Dashboard.Postpaid.GetSubAccount.SubAccountRequest objSubAccountRequest)
        {
            Entity.Dashboard.Postpaid.GetSubAccount.SubAccountResponse objSubAccountResponse = new Entity.Dashboard.Postpaid.GetSubAccount.SubAccountResponse()
            {
                ListSubAccount = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Account>>(objSubAccountRequest.Audit.Session, objSubAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetSubAccount(objSubAccountRequest.Audit.Session, objSubAccountRequest.Audit.Transaction, objSubAccountRequest.CustomerID); })
            };
            return objSubAccountResponse;
        }

        /// <summary>
        /// Método para obtener el límite de crédito.
        /// </summary>
        /// <param name="objCreditLimitRequest">Contiene id de cliente, usuario, aplicación, periodo y tipos de consulta.</param>
        /// <returns>Devuelve objeto CreditLimitResponse con listado de límite de crédito.</returns>
        public static Entity.Dashboard.Postpaid.GetCreditLimit.CreditLimitResponse GetCreditLimit(Entity.Dashboard.Postpaid.GetCreditLimit.CreditLimitRequest objCreditLimitRequest)
        {
            string strTipoConsultaPACOAC = "";
            Entity.Dashboard.Postpaid.GetCreditLimit.CreditLimitResponse objCreditLimitResponse = null;
            POSTPAID.CreditLimit objCreditLimit = null;
            try
            {
                strTipoConsultaPACOAC = KEY.AppSettings("sTipoConsultaPACOAC");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCreditLimitRequest.Audit.Session, objCreditLimitRequest.Audit.Transaction, ex.Message);
            }


            objCreditLimitRequest.TypeConsultationOAC = strTipoConsultaPACOAC;
            try
            {

                objCreditLimitResponse = new Entity.Dashboard.Postpaid.GetCreditLimit.CreditLimitResponse()
                {
                    ListCreditLimit = Claro.Web.Logging.ExecuteMethod<List<CreditLimit>>(objCreditLimitRequest.Audit.Session, objCreditLimitRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetCreditLimit(objCreditLimitRequest.User, objCreditLimitRequest.CustomerId, objCreditLimitRequest.TypeConsultationOAC, objCreditLimitRequest.TypeConsultationDetailOAC, objCreditLimitRequest.Audit.Session, objCreditLimitRequest.Audit.Transaction); })
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCreditLimitRequest.Audit.Session, objCreditLimitRequest.Audit.Transaction, ex.Message);

            }

            if (objCreditLimitResponse != null && objCreditLimitRequest.Application == Claro.SIACU.Constants.PostpaidMajuscule)
            {
                objCreditLimitRequest.Period = DateTime.Now.ToString("yyyyMM");

                try
                {
                    objCreditLimit = Claro.Web.Logging.ExecuteMethod<POSTPAID.CreditLimit>(objCreditLimitRequest.Audit.Session, objCreditLimitRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetCharge(objCreditLimitRequest.Audit.Session, objCreditLimitRequest.Audit.Transaction, objCreditLimitRequest.Period, objCreditLimitRequest.CustomerId); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objCreditLimitRequest.Audit.Session, objCreditLimitRequest.Audit.Transaction, ex.Message);
                }


            }
            else if (objCreditLimitResponse.ListCreditLimit != null && objCreditLimitRequest.Application == Claro.SIACU.Constants.PostpaidMajuscule)
            {


                foreach (POSTPAID.CreditLimit item in objCreditLimitResponse.ListCreditLimit)
                {
                    try
                    {
                        objCreditLimit = Claro.Web.Logging.ExecuteMethod<POSTPAID.CreditLimit>(objCreditLimitRequest.Audit.Session, objCreditLimitRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetCharge(objCreditLimitRequest.Audit.Session, objCreditLimitRequest.Audit.Transaction, objCreditLimitRequest.Period, objCreditLimitRequest.CustomerId); });
                    }
                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(objCreditLimitRequest.Audit.Session, objCreditLimitRequest.Audit.Transaction, ex.Message);
                    }
                }

            }
            if (objCreditLimit != null)
            {
                objCreditLimitResponse.ListCreditLimit = new List<POSTPAID.CreditLimit>();
                objCreditLimitResponse.ListCreditLimit.Add(objCreditLimit);
            }
            return objCreditLimitResponse;
        }

        /// <summary>
        /// Método para obtener la fecha PAC.
        /// </summary>
        /// <param name="DateBloquEjec">Fecha bloqueo ejecución</param>
        /// <param name="DateBloquProg">Fecha bloqueo programado</param>
        /// <param name="NroPac">Número PAC</param>
        /// <returns>Devuelve cadena con fecha PAC.</returns>
        public static string GetDatePAC(string DateBloquEjec, string DateBloquProg, string NroPac)
        {
            DateTime ComDate, ComDateVenc;
            string DateReturn;

            ComDate = Convert.ToDate(DateBloquEjec);

            if (ComDate >= DateTime.Now)
            {
                DateReturn = ComDate.ToString("yyyyMM");
            }
            else
            {
                ComDateVenc = Convert.ToDate(DateBloquProg);
                DateReturn = ComDateVenc.ToString("yyyyMM");
            }
            return DateReturn;
        }

        /// <summary>
        /// Método para obtener el detalle de límite de crédito.
        /// </summary>
        /// <param name="objCreditLimitDetailRequest"></param>
        /// <returns>Devuelve objeto CreditLimitDetail con información de detalle de límite de crédito.</returns>
        public static Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailResponse GetCreditLimitDetail(Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailRequest objCreditLimitDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailResponse objCreditLimitDetailResponse = new Entity.Dashboard.Postpaid.GetCreditLimitDetail.CreditLimitDetailResponse()
            {
                CreditLimitDetail = Claro.Web.Logging.ExecuteMethod<POSTPAID.CreditLimitDetail>(objCreditLimitDetailRequest.Audit.Session, objCreditLimitDetailRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailPAC(objCreditLimitDetailRequest.Audit.Session, objCreditLimitDetailRequest.Audit.Transaction, objCreditLimitDetailRequest.CodApplication, objCreditLimitDetailRequest.UserApplication, objCreditLimitDetailRequest.ServiceType, objCreditLimitDetailRequest.Account, objCreditLimitDetailRequest.DocumentType, objCreditLimitDetailRequest.DocumentNumber); })
            };
            return objCreditLimitDetailResponse;
        }

        /// <summary>
        /// Método para obtener la relación de planes HFC y LTE.
        /// </summary>
        /// <param name="objRelationPlanHFCLTERequest">Contiene id de cliente y aplicación.</param>
        /// <returns>Devuelve objeto RelationPlanHFCLTEResponse con información de relación de planes HFC y LTE.</returns>
        public static Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE.RelationPlanHFCLTEResponse GetRelationPlanHFCLTE(Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE.RelationPlanHFCLTERequest objRelationPlanHFCLTERequest)
        {
            POSTPAID.ServiceAccount objServiceAccount = null;
            List<POSTPAID.Equipment> listEquipment = null;
            try
            {
                objServiceAccount = Claro.Web.Logging.ExecuteMethod<POSTPAID.ServiceAccount>(objRelationPlanHFCLTERequest.Audit.Session, objRelationPlanHFCLTERequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetProductAccount(objRelationPlanHFCLTERequest.Audit.Session, objRelationPlanHFCLTERequest.Audit.Transaction, objRelationPlanHFCLTERequest.CustomerId); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRelationPlanHFCLTERequest.Audit.Session, objRelationPlanHFCLTERequest.Audit.Transaction, ex.Message);
            }

            if (objServiceAccount != null && objRelationPlanHFCLTERequest.Aplication == Claro.SIACU.Constants.LTE)
            {
                try
                {
                    objServiceAccount.ListServiceAccountLTE = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ServiceLTEAccount>>(objRelationPlanHFCLTERequest.Audit.Session, objRelationPlanHFCLTERequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetServiceCustomerId(objRelationPlanHFCLTERequest.Audit.Session, objRelationPlanHFCLTERequest.Audit.Transaction, objRelationPlanHFCLTERequest.CustomerId); });
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objRelationPlanHFCLTERequest.Audit.Session, objRelationPlanHFCLTERequest.Audit.Transaction, ex.Message);
                }
            }

            try
            {
                listEquipment = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Equipment>>(objRelationPlanHFCLTERequest.Audit.Session, objRelationPlanHFCLTERequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetEquipmentAccount(objRelationPlanHFCLTERequest.Audit.Session, objRelationPlanHFCLTERequest.Audit.Transaction, objRelationPlanHFCLTERequest.CustomerId); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRelationPlanHFCLTERequest.Audit.Session, objRelationPlanHFCLTERequest.Audit.Transaction, ex.Message);
            }

            Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE.RelationPlanHFCLTEResponse objRelationPlanHFCLTEResponse = new Entity.Dashboard.Postpaid.GetRelationPlanHFCLTE.RelationPlanHFCLTEResponse();

            if (objServiceAccount != null)
            {
                objRelationPlanHFCLTEResponse.ListServiceGSMAccount = objServiceAccount.ListServiceAccountGSM;
                objRelationPlanHFCLTEResponse.ListServiceHFCAccount = objServiceAccount.ListServiceAccountHFC;
                objRelationPlanHFCLTEResponse.ListServiceLTEAccount = objServiceAccount.ListServiceAccountLTE;
            }
            if (listEquipment != null) { objRelationPlanHFCLTEResponse.ListEquipment = listEquipment; }


            return objRelationPlanHFCLTEResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de llamadas de larga distancia.
        /// </summary>
        /// <param name="objDetailLongDistanceRequest">Contiene tipo de llamada y número de factura.</param>
        /// <returns>Devuelve objeto DetailLongDistanceResponse con información de detalle de llamadas de larga distancia.</returns>
        public static Entity.Dashboard.Postpaid.GetDetailLongDistance.DetailLongDistanceResponse GetDetailLongDistance(Entity.Dashboard.Postpaid.GetDetailLongDistance.DetailLongDistanceRequest objDetailLongDistanceRequest)
        {
            List<POSTPAID.CallDetail> oListCallDetail = null;
            int intCodeTypeCall;
            int intNationalCallType = Claro.Constants.NumberZero;
            int intInternationalCallType = Claro.Constants.NumberZero;
            try
            {
                intNationalCallType = Convert.ToInt(KEY.AppSettings("strNationalCallType"));
                intInternationalCallType = Convert.ToInt(KEY.AppSettings("strInternationalCallType"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objDetailLongDistanceRequest.Audit.Session, objDetailLongDistanceRequest.Audit.Transaction, ex.Message);
            }

            intCodeTypeCall = (objDetailLongDistanceRequest.TypeCall == Claro.SIACU.Constants.National) ? intNationalCallType : intInternationalCallType;

            Entity.Dashboard.Postpaid.GetDetailLongDistance.DetailLongDistanceResponse objGetDetailLongDistanceResponse = new Entity.Dashboard.Postpaid.GetDetailLongDistance.DetailLongDistanceResponse()
            {
                ListCallDetail = Claro.Web.Logging.ExecuteMethod<List<CallDetail>>(objDetailLongDistanceRequest.Audit.Session, objDetailLongDistanceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailLongDistance(objDetailLongDistanceRequest.Audit.Session, objDetailLongDistanceRequest.Audit.Transaction, intCodeTypeCall, objDetailLongDistanceRequest.InvoiceNumber); })

            };

            if (objGetDetailLongDistanceResponse.ListCallDetail != null && objGetDetailLongDistanceResponse.ListCallDetail.Count > 0)
            {
                oListCallDetail = new List<POSTPAID.CallDetail>();
                foreach (var item in objGetDetailLongDistanceResponse.ListCallDetail)
                {
                    if (intCodeTypeCall == intNationalCallType)
                    {
                        if (item.CALLORIGIN.ToString().IndexOf(";") >= 0)
                        {
                            item.CALLANTES = item.CALLORIGIN.ToString().Substring(0, item.CALLORIGIN.ToString().IndexOf(";"));
                            item.CALLDESPUES = item.CALLORIGIN.ToString().Substring(item.CALLORIGIN.ToString().IndexOf(";") + Claro.Constants.NumberOne);
                        }
                        else
                        {
                            item.CALLANTES = item.CALLORIGIN;
                            item.CALLDESPUES = item.CALLORIGIN;
                        }
                    }
                    else if (intCodeTypeCall == intInternationalCallType)
                    {
                        if (item.CALLORIGIN.ToString().IndexOf(";") >= 0)
                        {
                            item.CALLDESPUES = item.CALLORIGIN.ToString().Substring(item.CALLORIGIN.ToString().IndexOf(";") + Claro.Constants.NumberOne);
                        }
                        else
                        {
                            item.CALLDESPUES = item.CALLORIGIN;
                        }
                    }

                    if (string.IsNullOrEmpty(item.CALLANTES)) { item.CALLANTES = ""; }
                    if (string.IsNullOrEmpty(item.CALLDESPUES)) { item.CALLDESPUES = ""; }

                    oListCallDetail.Add(item);
                }
            }

            objGetDetailLongDistanceResponse.ListCallDetail = oListCallDetail;
            return objGetDetailLongDistanceResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de llamadas de roaming internacional.
        /// </summary>
        /// <param name="objInternationalRoamingDetailRequest">Contiene número de factura.</param>
        /// <returns>Devuelve objeto InternationalRoamingDetailResponse con información de detalle de llamadas de roaming internacional.</returns>
        public static Entity.Dashboard.Postpaid.GetInternationalRoamingDetail.InternationalRoamingDetailResponse GetInternationalRoamingDetail(Entity.Dashboard.Postpaid.GetInternationalRoamingDetail.InternationalRoamingDetailRequest objInternationalRoamingDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetInternationalRoamingDetail.InternationalRoamingDetailResponse objInternationalRoamingDetailResponse = new Entity.Dashboard.Postpaid.GetInternationalRoamingDetail.InternationalRoamingDetailResponse()
            {
                ListCallDetail = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.CallDetail>>(objInternationalRoamingDetailRequest.Audit.Session, objInternationalRoamingDetailRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetInternationalRoamingDetail(objInternationalRoamingDetailRequest.Audit.Session, objInternationalRoamingDetailRequest.Audit.Transaction, objInternationalRoamingDetailRequest.InvoiceNumber); })
            };
            return objInternationalRoamingDetailResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de deudas APADECE.
        /// </summary>
        /// <param name="objDebtDetailRequest">Contiene número de documento.</param>
        /// <returns>Devuelve objeto DebtDetailResponse con información de detalle de deudas APADECE.</returns>
        public static Entity.Dashboard.Postpaid.GetDebtDetail.DebtDetailResponse GetDebtDetail(Entity.Dashboard.Postpaid.GetDebtDetail.DebtDetailRequest objDebtDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetDebtDetail.DebtDetailResponse objDebtDetailResponse = new Entity.Dashboard.Postpaid.GetDebtDetail.DebtDetailResponse()
            {
                ListDebtDetail = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ApadeceDebt>>(objDebtDetailRequest.Audit.Session, objDebtDetailRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDebtDetail(objDebtDetailRequest.Audit.Session, objDebtDetailRequest.Audit.Transaction, objDebtDetailRequest.DocumentNumber); })
            };
            return objDebtDetailResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de tráfico local adicional TIMPRO y TIMMAX. 
        /// </summary>
        /// <param name="objAdditionalLocalTrafficDetailRequest"></param>
        /// <returns>Devuelve objeto AdditionalLocalTrafficDetailResponse con información del detalle de tráfico local adicional TIMPRO y TIMMAX.</returns>
        public static Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail.AdditionalLocalTrafficDetailResponse GetAdditionalLocalTrafficDetail(Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail.AdditionalLocalTrafficDetailRequest objAdditionalLocalTrafficDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail.AdditionalLocalTrafficDetailResponse objAdditionalLocalTrafficDetailResponse = new Entity.Dashboard.Postpaid.GetAdditionalLocalTrafficDetail.AdditionalLocalTrafficDetailResponse()
            {
                ListTimProLocalTrafficDetail = Claro.Web.Logging.ExecuteMethod<List<LocalTrafficDetail>>(objAdditionalLocalTrafficDetailRequest.Audit.Session, objAdditionalLocalTrafficDetailRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailLocalTrafficTimPro(objAdditionalLocalTrafficDetailRequest.Audit.Session, objAdditionalLocalTrafficDetailRequest.Audit.Transaction, Claro.SIACU.Constants.ConsultTimProAdditionalLocalTraffic, objAdditionalLocalTrafficDetailRequest.InvoiceNumber); }),
                ListTimMaxLocalTrafficDetail = Claro.Web.Logging.ExecuteMethod<List<LocalTrafficDetail>>(objAdditionalLocalTrafficDetailRequest.Audit.Session, objAdditionalLocalTrafficDetailRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailLocalTrafficTimMax(objAdditionalLocalTrafficDetailRequest.Audit.Session, objAdditionalLocalTrafficDetailRequest.Audit.Transaction, Claro.SIACU.Constants.ConsultTimMaxAdditionalLocalTraffic, objAdditionalLocalTrafficDetailRequest.InvoiceNumber); }),

            };

            return objAdditionalLocalTrafficDetailResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de tráfico local de consumo TIMPRO y TIMMAX. 
        /// </summary>
        /// <param name="objConsumeLocalTrafficDetailRequest">Contiene número de factura.</param>
        /// <returns>Devuelve objeto ConsumeLocalTrafficDetailResponse con información del detalle de tráfico local de consumo TIMPRO y TIMMAX.</returns>
        public static Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail.ConsumeLocalTrafficDetailResponse GetConsumeLocalTrafficDetail(Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail.ConsumeLocalTrafficDetailRequest objConsumeLocalTrafficDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail.ConsumeLocalTrafficDetailResponse objConsumeLocalTrafficDetailResponse = new Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetail.ConsumeLocalTrafficDetailResponse()
            {
                ListTimProLocalTrafficDetail = Claro.Web.Logging.ExecuteMethod<List<LocalTrafficDetail>>(objConsumeLocalTrafficDetailRequest.Audit.Session, objConsumeLocalTrafficDetailRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailLocalTrafficTimPro(objConsumeLocalTrafficDetailRequest.Audit.Session, objConsumeLocalTrafficDetailRequest.Audit.Transaction, Claro.SIACU.Constants.ConsultTimProConsumeLocalTraffic, objConsumeLocalTrafficDetailRequest.InvoiceNumber); }),
                ListTimMaxLocalTrafficDetail = Claro.Web.Logging.ExecuteMethod<List<LocalTrafficDetail>>(objConsumeLocalTrafficDetailRequest.Audit.Session, objConsumeLocalTrafficDetailRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailLocalTrafficTimPro(objConsumeLocalTrafficDetailRequest.Audit.Session, objConsumeLocalTrafficDetailRequest.Audit.Transaction, Claro.SIACU.Constants.ConsultTimMaxConsumeLocalTraffic, objConsumeLocalTrafficDetailRequest.InvoiceNumber); }),
            };

            return objConsumeLocalTrafficDetailResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de otros conceptos.
        /// </summary>
        /// <param name="objDetailsOtherConceptsRequest">Contiene número de factura y grupo.</param>
        /// <returns>Devuelve objeto DetailsOtherConceptsResponse con información de detalle de otros conceptos.</returns>
        public static Entity.Dashboard.Postpaid.GetDetailsOtherConcepts.DetailsOtherConceptsResponse GetDetailsOtherConcepts(Entity.Dashboard.Postpaid.GetDetailsOtherConcepts.DetailsOtherConceptsRequest objDetailsOtherConceptsRequest)
        {
            Entity.Dashboard.Postpaid.GetDetailsOtherConcepts.DetailsOtherConceptsResponse objDetailsOtherConceptsResponse = new Entity.Dashboard.Postpaid.GetDetailsOtherConcepts.DetailsOtherConceptsResponse()
            {
                ListOtherDetails = Claro.Web.Logging.ExecuteMethod<List<OtherDetails>>(objDetailsOtherConceptsRequest.Audit.Session, objDetailsOtherConceptsRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailsOtherConcepts(objDetailsOtherConceptsRequest.Audit.Session, objDetailsOtherConceptsRequest.Audit.Transaction, objDetailsOtherConceptsRequest.GroupBox, objDetailsOtherConceptsRequest.InvoiceNumber); }),
            };

            return objDetailsOtherConceptsResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de servicio TIM.
        /// </summary>
        /// <param name="objTimServiceDetailsRequest">Contiene número de factura.</param>
        /// <returns>Devuelve objeto TimServiceDetailsResponse con información de detalle de servicio TIM.</returns>
        public static Entity.Dashboard.Postpaid.GetTimServiceDetails.TimServiceDetailsResponse GetTimServiceDetails(Entity.Dashboard.Postpaid.GetTimServiceDetails.TimServiceDetailsRequest objTimServiceDetailsRequest)
        {
            Entity.Dashboard.Postpaid.GetTimServiceDetails.TimServiceDetailsResponse objTimServiceDetailsResponse = new Entity.Dashboard.Postpaid.GetTimServiceDetails.TimServiceDetailsResponse()
            {
                ListCallDetail = Claro.Web.Logging.ExecuteMethod<List<CallDetailTimService>>(objTimServiceDetailsRequest.Audit.Session, objTimServiceDetailsRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTimServiceDetails(objTimServiceDetailsRequest.Audit.Session, objTimServiceDetailsRequest.Audit.Transaction, objTimServiceDetailsRequest.InvoiceNumber); }),
            };

            return objTimServiceDetailsResponse;
        }

        /// <summary>
        /// Método para obtener el historial de facturas.
        /// </summary>
        /// <param name="objHistoryInvoiceRequest">Contiene id de cliente.</param>
        /// <returns>Devuelve objeto HistoryInvoiceResponse con información de historial de factura.</returns>
        public static Entity.Dashboard.Postpaid.GetHistoryInvoice.HistoryInvoiceResponse GetHistoryInvoice(Entity.Dashboard.Postpaid.GetHistoryInvoice.HistoryInvoiceRequest objHistoryInvoiceRequest)
        {
            Claro.Web.Logging.Error("GetHistoryInvoiceTobe", "GetHistoryInvoiceTobeBUSINESSS: strPlataforma", "********");
            Int32 intPageSize = Claro.Constants.NumberZero;
            try
            {
                intPageSize = Convert.ToInt(KEY.AppSettings("strConsCantRegSDF"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHistoryInvoiceRequest.Audit.Session, objHistoryInvoiceRequest.Audit.Transaction, ex.Message);
            }
            
            Entity.Dashboard.Postpaid.GetHistoryInvoice.HistoryInvoiceResponse objHistoryInvoiceResponse = new Entity.Dashboard.Postpaid.GetHistoryInvoice.HistoryInvoiceResponse()
            {
                ListReceiptHistory = Claro.Web.Logging.ExecuteMethod<List<ReceiptHistory>>(objHistoryInvoiceRequest.Audit.Session, objHistoryInvoiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetHistoryInvoice(objHistoryInvoiceRequest.Audit.Session, objHistoryInvoiceRequest.Audit.Transaction, intPageSize, objHistoryInvoiceRequest.CustomerID); }),
            };
            /*INICIO - TMOPOST-2672 _ Detalle de Facturas Anteriores*/
            if (objHistoryInvoiceResponse.ListReceiptHistory == null)
            {
                obtenerDatosFacturaRequest objObtenerDatosFacturaRequest = new obtenerDatosFacturaRequest()
                {
                    csId = objHistoryInvoiceRequest.CustomerID,
                    periodo = string.Empty
                };
                InvoceDetailsRequest objInvoceDetailsRequest = new InvoceDetailsRequest()
                {
                    obtenerDatosFacturaRequest = objObtenerDatosFacturaRequest
                };
                InvoceDetailsResponse objInvoceDetailsResponse = new InvoceDetailsResponse();
                objInvoceDetailsResponse = Claro.Web.Logging.ExecuteMethod<InvoceDetailsResponse>(objHistoryInvoiceRequest.Audit.Session, objHistoryInvoiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetHistoryInvoiceTobe(objHistoryInvoiceRequest.Audit, objInvoceDetailsRequest); });
                if (objInvoceDetailsResponse != null &&
                    objInvoceDetailsResponse.responseAudit != null &&
                    objInvoceDetailsResponse.responseData != null &&
                    objInvoceDetailsResponse.responseAudit.codigoRespuesta == Claro.Constants.NumberZeroString &&
                    objInvoceDetailsResponse.responseData.listaDatosFacturaType != null &&
                    objInvoceDetailsResponse.responseData.listaDatosFacturaType.listaDatosFactura != null)
                {
                    var listReceiptHistory = new List<ReceiptHistory>();

                    foreach (var item in objInvoceDetailsResponse.responseData.listaDatosFacturaType.listaDatosFactura)
                    {
                        var objReceiptHistory = new ReceiptHistory();
                        objReceiptHistory.FechaEmision = item.fechaEmisión;
                        objReceiptHistory.FechaVencimiento = item.fechaVencimiento;
                        objReceiptHistory.TotalCurrentCharges = Convert.ToDecimal(item.montoTotal);
                        objReceiptHistory.InvoiceNumber = item.numFactura;
                        objReceiptHistory.Periodo = item.periodo;
                        //
                        objReceiptHistory.NroCiclo = item.nroCiclo;
                        objReceiptHistory.NroDoc = item.nroDocumento;
                        objReceiptHistory.Mes = item.mes;
                        objReceiptHistory.Anho = item.anio;
                        objReceiptHistory.CodCliente = item.codCliente;
                        objReceiptHistory.Distrito = item.distrito;
                        objReceiptHistory.Departamento = item.departamento;
                        objReceiptHistory.Provincia = item.provincia;
                        listReceiptHistory.Add(objReceiptHistory);
                    }

                    objHistoryInvoiceResponse.ListReceiptHistory = listReceiptHistory;
                }
            }
            /*FIN - TMOPOST-2672 _ Detalle de Facturas Anteriores*/


            return objHistoryInvoiceResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de sms.
        /// </summary>
        /// <param name="objSMSDetailsRequest">Contiene número de factura y msisdn.</param>
        /// <returns>Devuelve objeto SMSDetailsResponse con información de detalle de sms.</returns>
        public static Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsResponse SMSDetails(Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsRequest objSMSDetailsRequest)
        {
            Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsResponse objSMSDetailsResponse = new Entity.Dashboard.Postpaid.GetSMSDetails.SMSDetailsResponse()
            {
                ListCallDetail = Claro.Web.Logging.ExecuteMethod<List<CallDetailSMS>>(objSMSDetailsRequest.Audit.Session, objSMSDetailsRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.SMSDetails(objSMSDetailsRequest.Audit.Session, objSMSDetailsRequest.Audit.Transaction, objSMSDetailsRequest.InvoiceNumber, objSMSDetailsRequest.Msisdn); }),
                DecCantidadSMS = Claro.Web.Logging.ExecuteMethod<decimal>(objSMSDetailsRequest.Audit.Session, objSMSDetailsRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetAmountSMSDetails(objSMSDetailsRequest.Audit.Session, objSMSDetailsRequest.Audit.Transaction, objSMSDetailsRequest.InvoiceNumber, objSMSDetailsRequest.Msisdn); }),
            };

            return objSMSDetailsResponse;
        }

        /// <summary>
        /// Método para obtener el contrato de la línea o servicio.
        /// </summary>
        /// <param name="objContractedBusinessServicesRequest">Contiene número de teléfono.</param>
        /// <returns>Devuelve objeto ContractedBusinessServicesResponse con información de contrato de línea.</returns>
        public static Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse GetPhoneContract(Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse objContractedBusinessServicesResponse = new Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse();

            //if (objRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS))
            //{
            //    objContractedBusinessServicesResponse.PhoneContracts = Claro.Web.Logging.ExecuteMethod<List<PhoneContract>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPhoneContract(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.User, objRequest.System, objRequest.Telephone); });
            //}
            //else
            //{

            objContractedBusinessServicesResponse.PhoneContracts = Claro.Web.Logging.ExecuteMethod<List<PhoneContract>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPhoneContractTobe(objRequest.Audit, objRequest.Audit.Transaction, objRequest.User, objRequest.System, objRequest.Telephone, objRequest.plataformaAT, objRequest.flagMigrado); });
            //}

            return objContractedBusinessServicesResponse;
        }


        /// <summary>
        /// Método para obtener el limite de consumo
        /// </summary>
        /// <returns>Devuelve el detalle de Limite de Consumo.</returns>
        public static string GetDetalleCliente(string strIdSession, string strTransaction,
            string strTelephone, string strContractID)
        {
            string dConsumo = null;

            List<POSTPAID.ConsumeLimit> lstMaximumConsumption = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ConsumeLimit>>
                (strIdSession, strTransaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetConsumeLimit(
                        strIdSession, strTransaction, strTelephone, int.Parse(strContractID));
                });

            List<POSTPAID.ContractServices> lstContratoServicio =
             (Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ContractServices>>(strIdSession, strTransaction, () => { return Data.Dashboard.Postpaid.GetContractServices(strIdSession, strTransaction, strContractID); }));


            if (lstContratoServicio.Count > 0 && lstContratoServicio != null)
            {

                foreach (var item in lstContratoServicio)
                {

                    if (item.COD_SERV == ConfigurationManager.AppSettings("strCodAdicionalFlexible") & item.COD_EXCLUYENTE == ConfigurationManager.AppSettings("strCoExcl") & item.ESTADO == ConfigurationManager.AppSettings("strEstadoLC"))
                    {
                        dConsumo = ConfigurationManager.AppSettings("strAdicionalFlexible");
                    }
                    else if (item.COD_SERV == ConfigurationManager.AppSettings("strCodExacto") & item.COD_EXCLUYENTE == ConfigurationManager.AppSettings("strCoExcl") & item.ESTADO == ConfigurationManager.AppSettings("strEstadoLC"))
                    {
                        dConsumo = ConfigurationManager.AppSettings("strExacto");
                    }
                    else if (item.COD_SERV == ConfigurationManager.AppSettings("strCodAdicional") & item.COD_EXCLUYENTE == ConfigurationManager.AppSettings("strCoExcl") & item.ESTADO == ConfigurationManager.AppSettings("strEstadoLC"))
                    {
                        dConsumo = ConfigurationManager.AppSettings("strAdicional");
                    }

                }
                if (dConsumo == null)
                {
                    if (lstMaximumConsumption != null && lstMaximumConsumption.Count > 0)
                    {
                        foreach (var val in lstMaximumConsumption)
                        {
                            if (val.DESC_SERV.ToUpper() == ConfigurationManager.AppSettings("strAdicionalFlexible").ToUpper())
                            {
                                dConsumo = ConfigurationManager.AppSettings("strAdicionalFlexible");
                            }
                            else
                            {
                                dConsumo = ConfigurationManager.AppSettings("gConstkeyConsumoAbierto");
                            }
                        }
                    }
                    else
                    {
                        dConsumo = ConfigurationManager.AppSettings("gConstkeyConsumoAbierto");
                    }
                }

            }
            else
            {
                if (lstMaximumConsumption != null && lstMaximumConsumption.Count > 0)
                {
                    foreach (var item in lstMaximumConsumption)
                    {
                        if (item.DESC_SERV.ToUpper() == ConfigurationManager.AppSettings("strAdicionalFlexible").ToUpper())
                        {
                            dConsumo = ConfigurationManager.AppSettings("strAdicionalFlexible");
                        }
                        else
                        {
                            dConsumo = ConfigurationManager.AppSettings("gConstkeyConsumoAbierto");
                        }
                    }
                }
                else
                {
                    dConsumo = ConfigurationManager.AppSettings("gConstkeyConsumoAbierto");
                }
            }

            return dConsumo;
        }

        /// <summary>
        /// Método para obtener información del servicio contratado.
        /// </summary>
        /// <param name="objContractedBusinessServicesRequest">Contiene id de contrato.</param>
        /// <returns>Devuelve objeto ContractedBusinessServicesResponse con información del servicio contratado.</returns>
        public static Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse GetContractServices(Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse objContractedBusinessServicesResponse = new Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse();

            if (objRequest.Application.Equals(Claro.SIACU.Constants.PostpaidMajuscule))
            {
                if (objRequest.origen.Equals(Claro.Constants.NumberZeroString))
                {
                    objContractedBusinessServicesResponse.ContractServices = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ContractServices>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetContractServices(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.ContractId); });
                    int strFlagResult = 0;
                    string strcodigoResponse = "";
                    string strMessage = "";
                    bool Valida4GLTE;
                    string fec = ConfigurationManager.AppSettings("gConstFechaVigencia4G");
                    int Vigencia = 0;
                    string telephone = objRequest.Telephone;
                    string user = objRequest.Audit.UserName;

                    Vigencia = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
                    if (Vigencia >= int.Parse(fec))
                    {
                        Valida4GLTE = false;
                    }
                    else
                    {
                        Valida4GLTE = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.ConsultationValidityPackage4G(telephone, objRequest.Audit, out strFlagResult, out strcodigoResponse, out strMessage); });
                    }

                    ContractServices objContractServices = new ContractServices();
                    objContractServices.POS_GRUPO = ConfigurationManager.AppSettings("strCodPosGrupoPaquete4G");
                    objContractServices.DES_GRUPO = ConfigurationManager.AppSettings("strDesGrupoPaquete4G");
                    objContractServices.DES_SERV = ConfigurationManager.AppSettings("strDesServicioPaquete4G");
                    objContractServices.COD_SERV = "4G";
                    objContractServices.ESTADO = (Valida4GLTE == true ? "Activo" : "Desactivo");
                    if (objContractedBusinessServicesResponse.ContractServices != null) objContractedBusinessServicesResponse.ContractServices.Add(objContractServices);

                }

                else
                {
                    objContractedBusinessServicesResponse.ContractServices = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ContractServices>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetContractServicesToBe(objRequest.Audit, objRequest.coIdPub, objRequest.Telephone); });
                }

            }
            else
            {
                if (objRequest.origen.Equals(Claro.Constants.NumberZeroString))
                {
                    objContractedBusinessServicesResponse.ContractServices = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ContractServices>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetContractServicesHFCLTE(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.ContractId); });
                }
                else
                {
                    string codProducto = string.Empty;

                    if (objRequest.Application.Contains(Claro.SIACU.Constants.HFC))
                    {
                        codProducto = "05";
                    }
                    else if(objRequest.Application.Contains("FTTH"))
                    {
                        codProducto = "09";
                    }
                    objContractedBusinessServicesResponse.ContractServices = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ContractServices>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetCurrencyServiceCbio(objRequest.Audit, objRequest.coIdPub, codProducto); });
                }
                
            }


            return objContractedBusinessServicesResponse;

        }


        /// <summary>
        /// Método para obtener listado de decodificadores.
        /// </summary>
        /// <param name="objComputerQueryRequest">Contiene id de contrato e id de cliente.</param>
        /// <returns>Devuelve objeto ComputerQueryResponse con el listado de decodificadores.</returns>
        public static Entity.Dashboard.Postpaid.GetComputerQuery.ComputerQueryResponse GetComputerQuery(Entity.Dashboard.Postpaid.GetComputerQuery.ComputerQueryRequest objComputerQueryRequest)
        {
            Entity.Dashboard.Postpaid.GetComputerQuery.ComputerQueryResponse objGetComputerQueryResponse = new Entity.Dashboard.Postpaid.GetComputerQuery.ComputerQueryResponse()
            {
                Decos = Claro.Web.Logging.ExecuteMethod<List<Deco>>(objComputerQueryRequest.Audit.Session, objComputerQueryRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetComputerQuery(objComputerQueryRequest.Audit.Session, objComputerQueryRequest.Audit.Transaction, objComputerQueryRequest.ContractID, objComputerQueryRequest.CustomerID); })
            };

            return objGetComputerQueryResponse;
        }

        /// <summary>
        /// Método para obtener las transacciones programadas.
        /// </summary>
        /// <param name="objScheduledTransactionRequest">Contiene id de contrato, cuenta, rango de fechas, estado, tipo de transacción, código de interacción, cac, dac y asesor.</param>
        /// <returns>Devuelve objeto ScheduledTransactionResponse con información de transacciones programadas.</returns>
        public static Entity.Dashboard.Postpaid.GetScheduledTransaction.ScheduledTransactionResponse GetScheduledTransaction(Entity.Dashboard.Postpaid.GetScheduledTransaction.ScheduledTransactionRequest objScheduledTransactionRequest)
        {
            Entity.Dashboard.Postpaid.GetScheduledTransaction.ScheduledTransactionResponse objScheduledTransactionResponse = new Entity.Dashboard.Postpaid.GetScheduledTransaction.ScheduledTransactionResponse()
            {
                ScheduledTransactions = Claro.Web.Logging.ExecuteMethod<List<ScheduledTransaction>>(objScheduledTransactionRequest.Audit.Session, objScheduledTransactionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetScheduledTransaction(objScheduledTransactionRequest.Audit.Session, objScheduledTransactionRequest.Audit.Transaction, objScheduledTransactionRequest.IdContract, objScheduledTransactionRequest.Account, objScheduledTransactionRequest.StartDate, objScheduledTransactionRequest.EndDate, objScheduledTransactionRequest.State, objScheduledTransactionRequest.Adviser, objScheduledTransactionRequest.TransactionType, objScheduledTransactionRequest.InteractionCode, objScheduledTransactionRequest.CacDac); }),
            };

            return objScheduledTransactionResponse;
        }

        /// <summary>
        /// Método para obtener servicio BSCS.
        /// </summary>
        /// <param name="objContractedBusinessServicesRequest">Contiene código de servicio.</param>
        /// <returns>Devuelve objeto ContractedBusinessServicesResponse con información de servicio BSCS.</returns>
        public static Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse GetServiceBSCS(Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesRequest objContractedBusinessServicesRequest)
        {
            Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse objContractedBusinessServicesResponse = new Entity.Dashboard.Postpaid.GetContractedBusinessServices.ContractedBusinessServicesResponse()
            {
                BSCSServices = Claro.Web.Logging.ExecuteMethod<List<ServiceBSCS>>(objContractedBusinessServicesRequest.Audit.Session, objContractedBusinessServicesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetServiceBSCS(objContractedBusinessServicesRequest.Audit.Session, objContractedBusinessServicesRequest.Audit.Transaction, objContractedBusinessServicesRequest.ServiceCode); }),
            };

            return objContractedBusinessServicesResponse;
        }

        /// <summary>
        /// Método para obtener tipo de peticiones.
        /// </summary>
        /// <param name="objPetitionsTypeRequest">No contiene información.</param>
        /// <returns>Devuelve objeto PetitionsTypeResponse con información de tipo de peticiones.</returns>
        public static Entity.Common.GetPetitionsType.PetitionsTypeResponse GetPetitionsType(Entity.Common.GetPetitionsType.PetitionsTypeRequest objPetitionsTypeRequest)
        {
            Entity.Common.GetPetitionsType.PetitionsTypeResponse objpetitionsypeResponse = new Entity.Common.GetPetitionsType.PetitionsTypeResponse()
            {
                PetitionsTypes = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(objPetitionsTypeRequest.Audit.Session, objPetitionsTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("ListaPeticiones"); }),
            };

            return objpetitionsypeResponse;
        }

        /// <remarks>GetPetitions</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>10/12/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>10/12/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Devuelve listado de objetos Petitions con información de peticiones To Be </Mot></item></list>

        public static Entity.Dashboard.Postpaid.GetPetitions.PetitionResponse GetPetitions(Claro.SIACU.Entity.Dashboard.Postpaid.GetPetitions.PetitionRequest objPetitionRequest)
        {

            Claro.Web.Logging.Info("IdSession: " + objPetitionRequest.Audit.Session,
               "Transaccion: " + objPetitionRequest.Audit.Transaction,
               string.Format("Capa Business-Metodo: {0}, Audit{1}, ContractId{2}, coIdPub{3},  migrado{4}, StartDate{5}, EndDate{6} , flagConvivencia{7} , flagPlataforma{8}", "GetPetitions", objPetitionRequest.Audit, objPetitionRequest.ContractId, objPetitionRequest.coIdPub, objPetitionRequest.migrado, objPetitionRequest.StartDate, objPetitionRequest.EndDate, objPetitionRequest.flagConvivencia, objPetitionRequest.flagPlataforma));

            Claro.SIACU.Entity.Dashboard.Postpaid.GetPetitions.PetitionResponse objPetitionResponse = new Entity.Dashboard.Postpaid.GetPetitions.PetitionResponse();
            string strFecIniPeticiones = "";
            try
            {
                strFecIniPeticiones = KEY.AppSettings("gConstFecIniPeticiones");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPetitionRequest.Audit.Session, objPetitionRequest.Audit.Transaction, ex.Message);
            }

            objPetitionRequest.StartDate = strFecIniPeticiones;

            objPetitionRequest.EndDate = DateTime.Now.ToShortDateString();

            // Si es ASIS
            if (objPetitionRequest.flagPlataforma.Equals(Claro.SIACU.Constants.ASIS))
            {

                objPetitionRequest.migrado = Claro.Constants.NumberZeroString;
                objPetitionResponse.Petitions = Claro.Web.Logging.ExecuteMethod<List<Petitions>>(objPetitionRequest.Audit.Session, objPetitionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPetitionsLegacy(objPetitionRequest.Audit, objPetitionRequest.ContractId, objPetitionRequest.coIdPub, objPetitionRequest.migrado, objPetitionRequest.StartDate, objPetitionRequest.EndDate); });
            }
            else
            {
                //Si es TOBE
                if (objPetitionRequest.flagConvivencia.Equals(Claro.Constants.NumberOneString))
                {
                    if (objPetitionRequest.Application == "HFC")
                    {
                        //objPetitionRequest.ContractId = string.Empty;
                        objPetitionRequest.migrado = Claro.Constants.NumberTwoString;
                        objPetitionResponse.Petitions = Claro.Web.Logging.ExecuteMethod<List<Petitions>>(objPetitionRequest.Audit.Session, objPetitionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPetitionsTobeFija(objPetitionRequest.Audit, objPetitionRequest.ContractId, objPetitionRequest.coIdPub, objPetitionRequest.migrado, objPetitionRequest.StartDate, objPetitionRequest.EndDate); });
                    }
                    else
                    {
                        objPetitionRequest.migrado = Claro.Constants.NumberTwoString;
                        objPetitionResponse.Petitions = Claro.Web.Logging.ExecuteMethod<List<Petitions>>(objPetitionRequest.Audit.Session, objPetitionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPetitionsLegacy(objPetitionRequest.Audit, objPetitionRequest.ContractId, objPetitionRequest.coIdPub, objPetitionRequest.migrado, objPetitionRequest.StartDate, objPetitionRequest.EndDate); });
                    }
                    

                }
                else
                {
                    if (objPetitionRequest.Application == "HFC")
                    {
                        objPetitionRequest.ContractId = string.Empty;
                        objPetitionRequest.migrado = Claro.Constants.NumberOneString;
                        objPetitionResponse.Petitions = Claro.Web.Logging.ExecuteMethod<List<Petitions>>(objPetitionRequest.Audit.Session, objPetitionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPetitionsTobeFija(objPetitionRequest.Audit, objPetitionRequest.ContractId, objPetitionRequest.coIdPub, objPetitionRequest.migrado, objPetitionRequest.StartDate, objPetitionRequest.EndDate); });
                    }
                    else
                    {
                        objPetitionRequest.migrado = Claro.Constants.NumberOneString;
                        objPetitionRequest.ContractId = string.Empty;
                        objPetitionResponse.Petitions = Claro.Web.Logging.ExecuteMethod<List<Petitions>>(objPetitionRequest.Audit.Session, objPetitionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPetitionsLegacy(objPetitionRequest.Audit, objPetitionRequest.ContractId, objPetitionRequest.coIdPub, objPetitionRequest.migrado, objPetitionRequest.StartDate, objPetitionRequest.EndDate); });
                    }
  
                }
            }

            if (objPetitionRequest.Application != "HFC")
            {
                if (objPetitionResponse != null &&
                objPetitionResponse.Petitions != null)
                {
                    var lista = objPetitionResponse.Petitions;

                    lista.ForEach(x =>
                    {
                        switch (x.Estado)
                        {
                            case Claro.Constants.NumberSevenString:
                            case Claro.Constants.NumberEightString: x.Estado = Claro.SIACU.Constants.FIN_OK; break;
                            case Claro.Constants.NumberNineString:
                            case Claro.Constants.NumberElevenString:
                            case Claro.Constants.NumberTwelveString: x.Estado = Claro.SIACU.Constants.ERROR; break;
                            case Claro.Constants.NumberOneString:
                            case Claro.Constants.NumberTwoString:
                            case Claro.Constants.NumberFourString:
                            case Claro.Constants.NumberTenString:
                            case Claro.Constants.NumberFourteenString:
                            case Claro.Constants.NumberFifteenString:
                            case Claro.Constants.NumberSixteenString: x.Estado = Claro.SIACU.Constants.Pending; break;

                        }

                    });

                    objPetitionResponse.Petitions = lista;
                }

                if (!string.IsNullOrEmpty(objPetitionRequest.PetitionType) && objPetitionResponse.Petitions != null)
                {
                    objPetitionResponse.Petitions = (from POSTPAID.Petitions p in objPetitionResponse.Petitions
                                                     where p.Estado.ToUpper().Equals(objPetitionRequest.PetitionType.ToUpper())
                                                     select p).ToList();
                }
            } else {
                Claro.Web.Logging.Error(objPetitionRequest.Audit.Session, objPetitionRequest.Audit.Transaction, "HFC Peticiones cuenta: " + objPetitionResponse.Petitions.Count.ToString());
            }

            return objPetitionResponse;
        }

        /// <summary>
        /// Método para obtener triaciones.
        /// </summary>
        /// <param name="objTriacionRequest">Contiene id de contrato.</param>
        /// <returns>Devuelve objeto TriacionResponse con información de triaciones.</returns>
        public static Entity.Dashboard.Postpaid.GetTriaciones.StriationsResponse GetTriaciones(Claro.SIACU.Entity.Dashboard.Postpaid.GetTriaciones.StriationsRequest objTriacionRequest)
        {
            Entity.Dashboard.Postpaid.GetTriaciones.StriationsResponse objTriacionResponse = new Entity.Dashboard.Postpaid.GetTriaciones.StriationsResponse()
            {
                Striations = Claro.Web.Logging.ExecuteMethod<List<Striations>>(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTriaciones(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, objTriacionRequest.ContractId); }),
            };

            string strPortadoPORTOUT = "";
            try
            {
                strPortadoPORTOUT = KEY.AppSettings("PortadoPORTOUT");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, ex.Message);
            }

            if (objTriacionResponse.Striations != null)
            {
                foreach (POSTPAID.Striations item in objTriacionResponse.Striations)
                {

                    string strTelephone = item.TELEFONO, strPort = "";
                    Entity.Common.GetPortability.PortabilityResponse objPortabilityResponse = new Entity.Common.GetPortability.PortabilityResponse()
                    {
                        ListPortability = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Portability>>(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetPortability(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, strTelephone.Substring(Claro.Constants.NumberTwo), out strPort); })
                    };

                    if (objPortabilityResponse.ListPortability.Count > Claro.Constants.NumberZero && strPort == strPortadoPORTOUT)
                    {
                        item.PORTABILIDAD = strPort;
                    }


                }
                objTriacionResponse.Striations = OrderedByFactor(objTriacionResponse.Striations, objTriacionRequest);
            }
            return objTriacionResponse;
        }

        /// <summary>
        /// Método para ordenar por factor.
        /// </summary>
        /// <param name="listStriations">Contiene listado de triaciones.</param>
        /// <returns>Devuelve listado de triaciones</returns>
        public static List<POSTPAID.Striations> OrderedByFactor(List<POSTPAID.Striations> listStriations, Claro.SIACU.Entity.Dashboard.Postpaid.GetTriaciones.StriationsRequest objTriacionRequest)
        {
            string strFactor1 = listStriations[0].FACTOR, strFactor2;
            int intCount = Claro.Constants.NumberZero;

            for (int i = 0; i < listStriations.Count; i++)
            {
                strFactor2 = listStriations[i].FACTOR;

                if (strFactor1 == strFactor2)
                {
                    intCount = intCount + Claro.Constants.NumberOne;
                }
                else
                {
                    intCount = Claro.Constants.NumberOne;
                }

                listStriations[i].NUM_TRIO = intCount;
                strFactor1 = strFactor2;
            }
            return listStriations;
        }

        /// <summary>
        /// Método para obtener el historial de triaciones.
        /// </summary>
        /// <param name="objHistoricalStriationsRequest">Contiene número de teléfono.</param>
        /// <returns>Devuelve objeto HistoricalStriationsResponse con información de historial de triaciones.</returns>
        public static Entity.Dashboard.Postpaid.GetHistoricalStriations.HistoricalStriationsResponse GetHistoricalStriations(Claro.SIACU.Entity.Dashboard.Postpaid.GetHistoricalStriations.HistoricalStriationsRequest objHistoricalStriationsRequest)
        {
            Entity.Dashboard.Postpaid.GetHistoricalStriations.HistoricalStriationsResponse objHistoricalStriationsResponse = new Entity.Dashboard.Postpaid.GetHistoricalStriations.HistoricalStriationsResponse()
            {
                HistoricalStriations = Claro.Web.Logging.ExecuteMethod<List<HistoricalStriations>>(objHistoricalStriationsRequest.Audit.Session, objHistoricalStriationsRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetHistoricalStriations(objHistoricalStriationsRequest.Audit.Session, objHistoricalStriationsRequest.Audit.Transaction, objHistoricalStriationsRequest.Telephone); }),
            };

            return objHistoricalStriationsResponse;
        }


        /// <remarks>Nombre de Método</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>07/01/2020.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>09/01/2020.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>Listar el consumo de Bolsas Compartidas</Mot></item></list>
        /// 
        public static Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse GetRecharges(Entity.Dashboard.Postpaid.GetRecharges.RechargesRequest objRechargesRequest)
        {
            string ClaroPuntos = "";
            string strCodigoRespuesta = "";
            string strMensajeRespuesta = "";
            string strCustomerCode = "";
            strCustomerCode = objRechargesRequest.Account;
            string strResponseCodeZone = "", strMessage = "";

            List<Recharge> lstRechargeBalancePost = new List<Recharge>();
            List<Recharge> lstRechargeBalancePostAddOns = null;
            List<Recharge> lstRechargeBalancePostTotal = null;
            string strConstConsumoSaldoBroker = KEY.AppSettings("gConstConsumoSaldoBroker");
            Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse objRechargesResponse = new Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse();

            if (objRechargesRequest.FlagPlataforma.Equals(Claro.SIACU.Constants.ASIS))
            // Si es ASIS
            {

                if (ValidateCustomer(objRechargesRequest.TypeCustomer, KEY.AppSettings("strTipoClienteConsumoSaldo")))
                {
                    POSTPAID.Recharge objRecharge = new POSTPAID.Recharge()
                    {
                        MSISDN = objRechargesRequest.Telephone,
                        ID_CLIENTE = objRechargesRequest.CustomerId,
                        PLATAFORMA = ""
                    };
                    Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse objRechargesResponse2 = new Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse()
                    {
                        Title = strConstConsumoSaldoBroker,
                        Telephone = objRechargesRequest.Telephone,

                    };
                    if (string.Compare(objRechargesRequest.StateLine, KEY.AppSettings("strEstadoSuspendido"), true) == 0)
                        objRechargesResponse2.ForeColorStateLine = "red";
                    else
                        objRechargesResponse2.ForeColorStateLine = "";

                    objRechargesRequest.Telephone = string.Format("{0}{1}", ConfigurationManager.AppSettings("strConstPrefijo"), objRechargesRequest.Telephone.Trim());
                    objRechargesRequest.Audit.ApplicationName = KEY.AppSettings("gConstAplicacionConSaldo");
                    lstRechargeBalancePost = Claro.Web.Logging.ExecuteMethod<List<Recharge>>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetBalancePostpaidConsumerB2E(objRechargesRequest.Audit, objRechargesRequest.Telephone, objRechargesRequest.CustomerId,
                         ref strCustomerCode, ref ClaroPuntos, ref strCodigoRespuesta, ref strMensajeRespuesta);
                    });
                    if (!string.IsNullOrEmpty(ClaroPuntos))
                    {
                        lstRechargeBalancePost.Add(
                            new Recharge()
                            {
                                TIPO_PAQUETE = "Claro Puntos",
                                TIPO_UNIDAD = "Puntos",
                                UNIDAD = ClaroPuntos
                            }
                            );
                    }


                    objRechargesResponse = objRechargesResponse2;
                    switch (Convert.ToInt(strCodigoRespuesta))
                    {
                        case -5:
                        case -4:
                        case -3:
                        case -2:
                        case -1:

                            objRechargesResponse.Message = KEY.AppSettings("strVuelvaIntentar");
                            objRechargesResponse.MessageVisible = true;
                            break;
                        case 0:

                            objRechargesResponse.Message = "";
                            objRechargesResponse.MessageVisible = true;
                            break;
                        case -6:

                            objRechargesResponse.Message = strMensajeRespuesta;
                            objRechargesResponse.MessageVisible = true;
                            break;
                        case 1:
                        case 2:
                        case 3:


                            objRechargesResponse.Message = KEY.AppSettings("strVuelvaIntentar");
                            objRechargesResponse.MessageVisible = true;
                            break;
                        case 4:

                            objRechargesResponse.Message = KEY.AppSettings("strNoSeEncontroContrato"); ;
                            objRechargesResponse.MessageVisible = true;
                            break;
                        case 6:

                            objRechargesResponse.Message = KEY.AppSettings("strNoTieneSaldoBolsas"); ;
                            objRechargesResponse.MessageVisible = true;
                            break;
                        case 7:
                        case 8:
                        case 9:
                        case 10:


                            objRechargesResponse.Message = KEY.AppSettings("strNoTieneSaldoBonosPromos");
                            objRechargesResponse.MessageVisible = true;
                            break;
                    }

                    List<Recharge> lstServiceZone = Claro.Web.Logging.ExecuteMethod<List<Recharge>>
                        (objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () =>
                        { return Data.Dashboard.Postpaid.GetServicesContractZone(objRecharge, objRechargesRequest.Audit, out strResponseCodeZone, out strMessage); });
                    if (lstServiceZone != null && lstServiceZone.Count > 0)
                    {
                        foreach (var item in lstServiceZone)
                        {
                            lstRechargeBalancePost.Add(item);
                        }
                    }

                    objRechargesResponse.Recharges = lstRechargeBalancePost;

                    Claro.Web.Logging.Info(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, new JavaScriptSerializer().Serialize(lstRechargeBalancePost));
                }
                else
                {
                    string strEstadoContrato = Claro.Web.Logging.ExecuteMethod<string>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetEstadoContrato(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, objRechargesRequest.Contract); });

                    if (!string.IsNullOrEmpty(strEstadoContrato) && strEstadoContrato != "null")
                    {
                        if (Claro.Constants.LetterA.Equals(strEstadoContrato.ToUpperInvariant()) || Claro.Constants.LetterS.Equals(strEstadoContrato.ToUpperInvariant()))
                        {
                            string strCustomerJanus = Claro.Web.Logging.ExecuteMethod<string>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetCustomerJanus(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, objRechargesRequest.Telephone); });

                            if (!string.IsNullOrEmpty(strCustomerJanus) && strCustomerJanus != "null")
                            {
                                if (Claro.Constants.LetterT.Equals(strCustomerJanus.ToUpperInvariant()))
                                {
                                    string strStateControlPlan = Claro.Web.Logging.ExecuteMethod<string>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetStatusControlPlan(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, Convert.ToInt(objRechargesRequest.Contract), Claro.Constants.NumberSeventyThree); }),
                                           strStatePospuroPlan = Claro.Web.Logging.ExecuteMethod<string>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetStatusControlPlan(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, Convert.ToInt(objRechargesRequest.Contract), Claro.Constants.NumberOneThousandFiveHundredSixtyOne); });
                                    bool isControl = false, isPospuro = false;

                                    if (!string.IsNullOrEmpty(strStateControlPlan) && strStateControlPlan != "null")
                                    {
                                        if (Claro.Constants.LetterA.Equals(strStateControlPlan.ToUpperInvariant()) || Claro.Constants.LetterS.Equals(strStateControlPlan.ToUpperInvariant()))
                                            isControl = true;
                                        if (!string.IsNullOrEmpty(strStatePospuroPlan) && strStatePospuroPlan != "null")
                                        {
                                            if (Claro.Constants.LetterA.Equals(strStatePospuroPlan.ToUpperInvariant()) || Claro.Constants.LetterS.Equals(strStatePospuroPlan.ToUpperInvariant()))
                                                isPospuro = true;

                                        }
                                    }

                                    if (isControl)
                                        objRechargesResponse = GetDataPrePago(objRechargesRequest);
                                    else if (isPospuro)
                                        objRechargesResponse = GetDataPostPagoPuro(objRechargesRequest);
                                    else
                                        objRechargesResponse = GetDataJanus(objRechargesRequest);

                                }
                                else
                                {
                                    if (Claro.Constants.LetterC.Equals(objRechargesRequest.FlagPlatform))
                                    {
                                        objRechargesResponse = GetDataPrePago(objRechargesRequest);
                                    }
                                    else
                                    {
                                        objRechargesResponse = GetDataPostPago(objRechargesRequest);
                                    }
                                }

                            }

                        }

                    }

                }

            }

            else
            {
                //TOBE

                lstRechargeBalancePost = Claro.Web.Logging.ExecuteMethod<List<Recharge>>(objRechargesRequest.Audit.Session, objRechargesRequest.FechaExpiracion, objRechargesRequest.Audit.Transaction, () =>
            {
                return Data.Dashboard.Postpaid.GetBalancePostpaidConsumerB2ELegacy(objRechargesRequest.Audit, objRechargesRequest.Telephone, objRechargesRequest.Contract, objRechargesRequest.FechaExpiracion,

           ref strCustomerCode, ref ClaroPuntos, ref strCodigoRespuesta, ref strMensajeRespuesta, objRechargesRequest.FechaExpiracion);

            });

                lstRechargeBalancePostAddOns = Claro.Web.Logging.ExecuteMethod<List<Recharge>>(objRechargesRequest.Audit.Session, objRechargesRequest.FechaExpiracion, objRechargesRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetAddOnsToBe(objRechargesRequest.Audit, objRechargesRequest.Telephone, objRechargesRequest.Contract, objRechargesRequest.FechaExpiracion,

                   ref strCustomerCode, ref ClaroPuntos, ref strCodigoRespuesta, ref strMensajeRespuesta, objRechargesRequest.FechaExpiracion);

                    });
                lstRechargeBalancePostTotal = lstRechargeBalancePost
                                            .Concat(lstRechargeBalancePostAddOns)
                                            .ToList();
                lstRechargeBalancePostTotal = lstRechargeBalancePostTotal
                                            .OrderBy(x => x.ID_ORDEN_BOLSA)
                                            .ThenBy(c => c.TIPO_UNIDAD)
                                            .ThenBy(c => c.UNIFICADA)
                                            .ToList();


                objRechargesResponse.Telephone = objRechargesRequest.Telephone;
                objRechargesResponse.Recharges = lstRechargeBalancePostTotal;
            }

            return objRechargesResponse;
        }


        public static Entity.Dashboard.Postpaid.GetDegradationTobe.GetDegradationResponseTobe GetBalancePostpaidConsumerB2ELegacyDegradation(Entity.Dashboard.Postpaid.GetDegradationTobe.GetDegradationRequestTobe request)
        {

            Entity.Dashboard.Postpaid.GetDegradationTobe.GetDegradationResponseTobe response = new POSTPAID.GetDegradationTobe.GetDegradationResponseTobe();
            try
            {

                response.listOptional = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetBalanceConsumption.Response.listaOpcional>>(request.Audit.Session, request.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetBalancePostpaidConsumerB2ELegacyDegradation(request.Audit, request.strMsidn, request.coIdPub);

                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, Claro.MessageException.GetOriginalExceptionMessage(ex));
                throw;
            }
            return response;
        }









        /// <summary>
        /// Método para obtener información JANUS.
        /// </summary>
        /// <param name="objRechargesRequest">Contiene número de teléfono e id de cliente.</param>
        /// <returns>Devuelve objeto RechargesResponse con información JANUS.</returns>
        public static Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse GetDataJanus(Entity.Dashboard.Postpaid.GetRecharges.RechargesRequest objRechargesRequest)
        {
            string strConstConsumoSaldoBroker = "",
                     strConstFormatoFechaBroker = "",
                     strConstTipoConsultaBroker = "",
                     strResponseCode = "",
                     strPostReply = "",
                     strPlatform = "",
                     strMessage = "",
                     strResponseCodeZone = "",
                    strResponseCodeM2M = "",
                    strMessageM2M = "";

            try
            {
                strConstConsumoSaldoBroker = KEY.AppSettings("gConstConsumoSaldoBroker");
                strConstFormatoFechaBroker = KEY.AppSettings("gConstFormatoFechaBroker");
                strConstTipoConsultaBroker = KEY.AppSettings("gConstTipoConsultaBroker");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, ex.Message);
            }

            Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse objRechargesResponse = new Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse()
            {
                Title = strConstConsumoSaldoBroker,
                Telephone = objRechargesRequest.Telephone
            };

            POSTPAID.Recharge objRecharge = new POSTPAID.Recharge()
            {
                MSISDN = objRechargesRequest.Telephone,
                ID_CLIENTE = objRechargesRequest.CustomerId,
                PLATAFORMA = ""
            };

            Claro.Web.Logging.Info("000000", "", new JavaScriptSerializer().Serialize(objRecharge));
            switch (strConstTipoConsultaBroker)
            {
                case Claro.Constants.NumberOneString:

                    objRechargesResponse.Recharges =
                    Claro.Web.Logging.ExecuteMethod<List<Recharge>>
                    (objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () =>
                    { return Data.Dashboard.Postpaid.GetBag(objRecharge, objRechargesRequest.Audit, out strResponseCode, out strPostReply, out strPlatform); });
                    objRechargesResponse.Message = strPostReply;

                    Claro.Web.Logging.Info("111111", "", new JavaScriptSerializer().Serialize(objRechargesResponse.Recharges));
                    break;
                case Claro.Constants.NumberTwoString:

                    objRechargesResponse.Recharges =
                     Claro.Web.Logging.ExecuteMethod<List<Recharge>>
                    (objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () =>
                    { return Data.Dashboard.Postpaid.GetBagBalance(objRecharge, objRechargesRequest.Audit, out strResponseCode, out strPostReply, out strPlatform); });
                    objRechargesResponse.Message = strPostReply;

                    Claro.Web.Logging.Info("2222222", "", new JavaScriptSerializer().Serialize(objRechargesResponse.Recharges));
                    break;
            }
            List<Recharge> lstBalanceM2M = Claro.Web.Logging.ExecuteMethod<List<Recharge>>
                (objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () =>
                { return Data.Dashboard.Postpaid.GetBalanceM2M(objRecharge, objRechargesRequest.Audit, out strResponseCodeM2M, out strMessageM2M); });


            List<Recharge> lstServiceZone = Claro.Web.Logging.ExecuteMethod<List<Recharge>>
                     (objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () =>
                     { return Data.Dashboard.Postpaid.GetServicesContractZone(objRecharge, objRechargesRequest.Audit, out strResponseCodeZone, out strMessage); });
            if (objRechargesResponse.Recharges != null)
            {
                if (lstServiceZone != null)
                    objRechargesResponse.Recharges.AddRange(lstServiceZone);
                if (lstBalanceM2M != null)
                    objRechargesResponse.Recharges.AddRange(lstBalanceM2M);
            }
            else
            {
                objRechargesResponse.Recharges = lstServiceZone;
                if (objRechargesResponse.Recharges != null)
                {
                    if (lstBalanceM2M != null)
                        objRechargesResponse.Recharges.AddRange(lstBalanceM2M);
                }
                else
                {
                    if (lstBalanceM2M != null)
                        objRechargesResponse.Recharges = lstBalanceM2M;
                }

            }


            Claro.Web.Logging.Info("GetDataJanus", "", new JavaScriptSerializer().Serialize(objRechargesResponse.Recharges));

            if (strResponseCodeZone == Claro.Constants.NumberZeroString || strResponseCode == Claro.Constants.NumberZeroString || strResponseCodeM2M == Claro.Constants.NumberZeroString)
            {

                if (strResponseCode != Claro.Constants.NumberZeroString)
                {

                    if (strConstTipoConsultaBroker == Claro.Constants.NumberOneString)
                    {

                        Claro.Web.Logging.Info(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, "Ocurrió un error al consultar el método ConsultarBolsas: {0}" + strPostReply);
                    }
                    else
                    {

                        Claro.Web.Logging.Info(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, "Ocurrió un error al consultar el método ConsultarSaldoBolsas: {0}" + strPostReply);
                    }
                    objRechargesResponse.Message = strPostReply;
                    objRechargesResponse.MessageVisible = true;
                }
                else if (strResponseCodeZone != Claro.Constants.NumberZeroString)
                {

                    Claro.Web.Logging.Info(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, "Ocurrió un error al consultar el método ServiciosContratoZona: {0}" + strMessage);
                    objRechargesResponse.MessageRecharges = "Paquete Viajero: " + strMessage;
                    objRechargesResponse.MessageRechargesVisible = true;
                }
                if (Claro.Convert.ToInt(strResponseCodeM2M) < Claro.Constants.NumberZero)
                {

                    objRechargesResponse.MessageBalanceM2M = "Advertencia para línea con Plan M2M AVL: " + strMessageM2M;
                    objRechargesResponse.MessageBalanceM2MVisible = true;
                }

            }
            else
            {

                objRechargesResponse.MessageRecharges = "Paquete Viajero: " + strMessage;
                objRechargesResponse.MessageRechargesVisible = true;
                objRechargesResponse.Message = strPostReply;
                objRechargesResponse.MessageVisible = true;

                if (Claro.Convert.ToInt(strResponseCodeM2M) < Claro.Constants.NumberZero)
                {

                    objRechargesResponse.MessageBalanceM2M = "Advertencia para línea con Plan M2M AVL: " + strMessageM2M;
                    objRechargesResponse.MessageBalanceM2MVisible = true;
                }

            }

            return objRechargesResponse;
        }

        /// <summary>
        /// Método para obtener información prepago.
        /// </summary>
        /// <param name="objRechargesRequest">Contiene id de cliente y número de teléfono.</param>
        /// <returns>Devuelve objeto RechargesResponse con información prepago.</returns>
        public static Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse GetDataPrePago(Entity.Dashboard.Postpaid.GetRecharges.RechargesRequest objRechargesRequest)
        {
            Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse objRechargesResponse = new Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse()
            {
                Title = Claro.SIACU.Constants.Balance,
                Service = Claro.Web.Logging.ExecuteMethod<Service>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDataService(objRechargesRequest.Audit.Session, objRechargesRequest.Telephone, objRechargesRequest.Audit.Transaction, objRechargesRequest.Audit.IPAddress, objRechargesRequest.Audit.ApplicationName); }),
            };

            return objRechargesResponse;
        }

        /// <summary>
        /// Método para obtener información postpago.
        /// </summary>
        /// <param name="objRechargesRequest">Contiene id de cliente y número de teléfono.</param>
        /// <returns>Devuelve objeto RechargesResponse con información postpago.</returns>
        public static Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse GetDataPostPago(Entity.Dashboard.Postpaid.GetRecharges.RechargesRequest objRechargesRequest)
        {
            Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse objRechargesResponse = new Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse()
            {
                Title = Claro.SIACU.Constants.Consumption,
                Telephone = objRechargesRequest.Telephone
            };

            switch (objRechargesRequest.FlagPlatform)
            {
                case Claro.Constants.LetterP:

                    int result = Claro.Web.Logging.ExecuteMethod<int>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.LoadBalancePostpaid(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, objRechargesRequest.Telephone); });

                    switch (result)
                    {
                        case Claro.Constants.NumberZero:
                        case Claro.Constants.NumberOne:

                            string activationDate = "",

                                data = Claro.Web.Logging.ExecuteMethod<string>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () =>
                                {
                                    return Data.Dashboard.Postpaid.GetBalancesPostpaid(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, objRechargesRequest.Telephone, out activationDate);
                                });


                            if (!string.IsNullOrEmpty(data.Trim()))
                            {
                                objRechargesResponse.Services = GetListService(objRechargesRequest.Audit, data, Claro.Constants.LetterP);
                                if (objRechargesResponse.Services.Count > Claro.Constants.NumberZero)
                                {
                                    objRechargesResponse.ActivationDate = activationDate;
                                }
                                objRechargesResponse.MessageVisible = false;
                            }
                            else
                            {
                                objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage1;
                                objRechargesResponse.MessageVisible = true;
                            }
                            break;
                        case Claro.Constants.NumberOneNegative:
                            objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage2;
                            objRechargesResponse.MessageVisible = true;
                            break;
                        case Claro.Constants.NumberTwoNegative:
                            objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage3;
                            objRechargesResponse.MessageVisible = true;
                            break;
                        case Claro.Constants.NumberThreeNegative:
                            objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage4;
                            objRechargesResponse.MessageVisible = true;
                            break;
                    }
                    break;
                case Claro.Constants.LetterR:

                    string externalOrigen = "",
                           consumerBagIdIn = "",
                           nroTamanoTransaccionRICE = "",
                           nroTransaccionRICE = "",
                           consKB = "";
                    try
                    {
                        externalOrigen = KEY.AppSettings("strExternalOrigen");
                        consumerBagIdIn = KEY.AppSettings("strConsumerBagIdIn");
                        nroTamanoTransaccionRICE = KEY.AppSettings("strNroTamanoTransaccionRICE");
                        nroTransaccionRICE = KEY.AppSettings("strNroTransaccionRICE");
                        consKB = KEY.AppSettings("strConsKB");

                        string correlative = Claro.Web.Logging.ExecuteMethod<string>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetCorrelativeRiceBalances(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction); }),

                               transactionId = nroTransaccionRICE + correlative,
                               balances = Claro.Web.Logging.ExecuteMethod<string>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetBalancesRiceServices(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, transactionId, objRechargesRequest.Telephone, externalOrigen, objRechargesRequest.CurrentUser, consumerBagIdIn); });
                        objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage7;
                        objRechargesResponse.MessageVisible = true;
                        objRechargesResponse.Services = GetListService(objRechargesRequest.Audit, balances, Claro.Constants.LetterR);


                    }
                    catch (Exception ex)
                    {
                        objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage6;
                        objRechargesResponse.MessageVisible = true;
                        Claro.Web.Logging.Error(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, ex.Message);
                    }
                    break;
            }
            return objRechargesResponse;
        }


        //nuevo
        public static Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse GetDataPostPagoPuro(Entity.Dashboard.Postpaid.GetRecharges.RechargesRequest objRechargesRequest)
        {
            Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse objRechargesResponse = new Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse()
            {
                Title = Claro.SIACU.Constants.Consumption,
                Telephone = objRechargesRequest.Telephone
            };
            try
            {
                int result = Claro.Web.Logging.ExecuteMethod<int>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.LoadBalancePostpaid(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, objRechargesRequest.Telephone); });

                switch (result)
                {
                    case Claro.Constants.NumberZero:
                    case Claro.Constants.NumberOne:

                        string activationDate = "",

                            data = Claro.Web.Logging.ExecuteMethod<string>(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, () =>
                            {
                                return Data.Dashboard.Postpaid.GetBalancesPostpaid(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, objRechargesRequest.Telephone, out activationDate);
                            });


                        if (!string.IsNullOrEmpty(data.Trim()))
                        {
                            objRechargesResponse.Services = GetListService(objRechargesRequest.Audit, data, Claro.Constants.LetterP);
                            if (objRechargesResponse.Services.Count > Claro.Constants.NumberZero)
                            {
                                objRechargesResponse.ActivationDate = activationDate;
                            }
                            objRechargesResponse.MessageVisible = false;
                        }
                        else
                        {
                            objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage1;
                            objRechargesResponse.MessageVisible = true;
                        }
                        break;
                    case Claro.Constants.NumberOneNegative:
                        objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage2;
                        objRechargesResponse.MessageVisible = true;
                        break;
                    case Claro.Constants.NumberTwoNegative:
                        objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage3;
                        objRechargesResponse.MessageVisible = true;
                        break;
                    case Claro.Constants.NumberThreeNegative:
                        objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage4;
                        objRechargesResponse.MessageVisible = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, ex.Message);
                objRechargesResponse.Message = Claro.SIACU.Constants.DataPostPaidMessage5;
            }


            return objRechargesResponse;
        }

        /// <summary>
        /// Método para obtener listado de servicios.
        /// </summary>
        /// <param name="objAudit">Información de auditoría.</param>
        /// <param name="strData">Información</param>
        /// <param name="strType">Tipo</param>
        /// <returns>Devuelve listado de objetos service con la información de servicio.</returns>
        public static List<POSTPAID.Service> GetListService(Claro.Entity.AuditRequest objAudit, string strData, string strType)
        {
            List<POSTPAID.Service> listService = null;

            if (strType.Equals(Claro.Constants.LetterP))
            {
                string[] aData = strData.Replace("<br>", ",").Split(',');

                if (aData.Length > Claro.Constants.NumberZero)
                {
                    listService = new List<POSTPAID.Service>();

                    foreach (var item in aData)
                    {
                        listService.Add(new POSTPAID.Service()
                        {
                            PAQUETE = item.Trim()
                        });
                    }
                }
                else
                {
                    listService = new List<POSTPAID.Service>();
                    listService.Add(new POSTPAID.Service()
                    {
                        PAQUETE = strData.Trim()
                    });
                }
            }
            else
            {
                string strConsKB = "",
                       strcodigosNoMostrarSixbell = "";
                try
                {
                    strConsKB = KEY.AppSettings("strConsKB");
                    strcodigosNoMostrarSixbell = KEY.AppSettings("strcodigosNoMostrarSixbell");
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
                }

                string[] aData = strData.Split('|');

                if (aData.Length > 0)
                {
                    listService = new List<POSTPAID.Service>();
                    int intQuotient = aData.Length / Claro.Constants.NumberThree,
                        intPlot = Claro.Constants.NumberZero,
                        intCoc = Claro.Constants.NumberZero,
                        intRes = Claro.Constants.NumberZero,
                        intValueKb = Convert.ToInt(strConsKB);

                    string strPackage = "",
                           strCoc = "",
                           strRes = "";
                    double douValue = 0.0;

                    for (int i = 0; i < intQuotient; i++)
                    {
                        if (aData[i * Claro.Constants.NumberThree] != strcodigosNoMostrarSixbell)
                        {
                            douValue = Convert.ToDouble(aData[i * Claro.Constants.NumberThree + Claro.Constants.NumberTwo]);
                            intPlot = Convert.ToInt(aData[i * Claro.Constants.NumberThree + 1]);

                            if (douValue < Claro.Constants.NumberZero)
                            {
                                douValue = douValue * (Claro.Constants.NumberOneNegative);
                            }


                            if (intPlot != Claro.Constants.NumberOne)
                            {
                                if (intPlot == Claro.Constants.NumberTwo || intPlot == Claro.Constants.NumberThree)
                                {
                                    intCoc = Convert.ToInt(Math.Truncate(douValue / Claro.Constants.NumberSixty));
                                    intRes = Convert.ToInt(douValue - (intCoc * Claro.Constants.NumberSixty));
                                    strCoc = string.Format("{0:00}", intCoc);
                                    strRes = string.Format("{0:00}", intCoc);
                                    strPackage = GetRiceBag("ListaBolsaRICE", Convert.ToString(aData[i * Claro.Constants.NumberThree]), objAudit) + " " + GetRiceBag("ListaTipoBolsaRICE", Convert.ToString(aData[i * Claro.Constants.NumberThree + 1]), objAudit) + ":" + strCoc + ":" + strRes;
                                }
                                else if (intPlot == Claro.Constants.NumberFive)
                                {
                                    strCoc = Convert.ToString(douValue / intValueKb);
                                    strRes = Convert.ToString(douValue % intValueKb);

                                    if (Convert.ToDecimal(strRes) != Claro.Constants.NumberZero)
                                    {
                                        strCoc = Convert.ToString(Math.Round(Convert.ToDecimal(douValue / intValueKb), Claro.Constants.NumberTwo));
                                    }
                                    strPackage = GetRiceBag("ListaBolsaRICE", Convert.ToString(aData[i * Claro.Constants.NumberThree]), objAudit) + " " + GetRiceBag("ListaTipoBolsaRICE", Convert.ToString(aData[i * Claro.Constants.NumberThree + 1]), objAudit) + ":" + strCoc;
                                }
                                else
                                {
                                    strPackage = GetRiceBag("ListaBolsaRICE", Convert.ToString(aData[i * Claro.Constants.NumberThree]), objAudit) + ":" + Convert.ToString(douValue);
                                }
                            }
                            else
                            {
                                string strValue = Convert.ToString(douValue);
                                strPackage = GetRiceBag("ListaBolsaRICE", Convert.ToString(aData[i * Claro.Constants.NumberThree]), objAudit) + " " + GetRiceBag("ListaTipoBolsaRICE", Convert.ToString(aData[i * Claro.Constants.NumberThree + 1]), objAudit) + ":" + strValue;
                            }

                            listService.Add(new POSTPAID.Service()
                            {
                                PAQUETE_ID = aData[i * Claro.Constants.NumberThree],
                                PAQUETE = strPackage,
                                CUOTA = aData[i * Claro.Constants.NumberThree + Claro.Constants.NumberTwo]
                            });
                        }
                    }
                }
            }
            return listService;
        }

        /// <summary>
        /// Método para obtener valores.
        /// </summary>
        /// <param name="strName">Nombre</param>
        /// <param name="strId">Id</param>
        /// <returns>Devuelve cadena con valor.</returns>
        public static string GetRiceBag(string strName, string strId, Claro.Entity.AuditRequest objAudit)
        {
            string strText = "";
            List<ListItem> listItem = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(objAudit.Session, objAudit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML(strName); });

            foreach (var item in listItem)
            {
                if (item.Code == strId)
                {
                    strText = item.Description;
                    break;
                }
            }
            return strText;
        }

        /// <summary>
        /// Método para obtener balance.
        /// </summary>
        /// <param name="objBalanceRequest">Contiene tipo de cliente y número de teléfono.</param>
        /// <returns>Devuelve objeto BalanceResponse con información de balance.</returns>
        public static Entity.Dashboard.Postpaid.GetBalance.BalanceResponse GetBalance(Entity.Dashboard.Postpaid.GetBalance.BalanceRequest objBalanceRequest)
        {
            Entity.Dashboard.Postpaid.GetBalance.BalanceResponse objBalanceResponse = new Entity.Dashboard.Postpaid.GetBalance.BalanceResponse();
            string strNoReturn = string.Empty,
                   strReturn = string.Empty,
                   strErrorSearch = string.Empty,
                   strZipCodePeru = string.Empty,
                   strMessage = string.Empty;

            try
            {
                strNoReturn = KEY.AppSettings("strTextoNoRetornoRegistrosConsultaConsumoSaldo");
                strReturn = KEY.AppSettings("strTextoRetornoRegistrosConsultaConsumoSaldo");
                strErrorSearch = KEY.AppSettings("strMsgErrorBusq");
                strZipCodePeru = KEY.AppSettings("strZIPCodePeru");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBalanceRequest.Audit.Session, objBalanceRequest.Audit.Transaction, ex.Message);
            }
            if (objBalanceRequest.Telephone.Substring(0, 2) != strZipCodePeru)
            {
                objBalanceRequest.Telephone = strZipCodePeru + objBalanceRequest.Telephone;
            }
            if (objBalanceRequest.CustomerType.Equals(Claro.SIACU.Constants.Business))
            {
                objBalanceResponse.Balances = Claro.Web.Logging.ExecuteMethod<List<Balance>>(objBalanceRequest.Audit.Session, objBalanceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetBalance(objBalanceRequest.Audit.Session, objBalanceRequest.Telephone, objBalanceRequest.Audit.Transaction, Convert.ToInt(Claro.Constants.NumberZeroString), out strMessage); });

                if (strMessage.Equals(Claro.SIACU.Constants.Success))
                {
                    objBalanceResponse.Balances = Claro.Web.Logging.ExecuteMethod<List<Balance>>(objBalanceRequest.Audit.Session, objBalanceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetBalance(objBalanceRequest.Audit.Session, objBalanceRequest.Telephone, objBalanceRequest.Audit.Transaction, Convert.ToInt(Claro.Constants.NumberThreeString), out strMessage); });
                    objBalanceResponse.Message = strReturn;
                }
                else
                {
                    objBalanceResponse.Message = strNoReturn;
                }
            }
            else if (objBalanceRequest.CustomerType.Equals(Constants.B2E) || objBalanceRequest.CustomerType.Equals(Constants.Consumer))
            {
                objBalanceResponse.Balances = Claro.Web.Logging.ExecuteMethod<List<Balance>>(objBalanceRequest.Audit.Session, objBalanceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetBalance(objBalanceRequest.Audit.Session, objBalanceRequest.Telephone, objBalanceRequest.Audit.Transaction, Convert.ToInt(Claro.Constants.NumberZeroString), out strMessage); });
            }

            if (objBalanceResponse.Balances != null && objBalanceResponse.Balances.Count.Equals(Claro.Constants.NumberOne) && objBalanceResponse.Balances[0].CONSUMO.Equals(Claro.Constants.LetterF))
            {
                objBalanceResponse.Message = strErrorSearch;
            }

            return objBalanceResponse;
        }

        /// <summary>
        /// Método para obtener información de deuda.
        /// </summary>
        /// <param name="objDueDocumentNumberRequest">Contiene tipo y número de documento de identidad.</param>
        /// <returns>Devuelve objeto DueDocumentNumber con información de deuda.</returns>
        public static Entity.Dashboard.Postpaid.GetDueDocumentNumber.DueDocumentNumberResponse DueNumberDocumentOAC(Entity.Dashboard.Postpaid.GetDueDocumentNumber.DueDocumentNumberRequest objDueDocumentNumberRequest)
        {
            Entity.Dashboard.Postpaid.GetDueDocumentNumber.DueDocumentNumberResponse objDueDocumentNumberResponse = new Entity.Dashboard.Postpaid.GetDueDocumentNumber.DueDocumentNumberResponse()
            {
                ObjDueDocumentNumber = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.DueDocumentNumber>(objDueDocumentNumberRequest.Audit.Session, objDueDocumentNumberRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.DueNumberDocumentOAC(objDueDocumentNumberRequest.Audit.Session, objDueDocumentNumberRequest.Audit.Transaction, objDueDocumentNumberRequest.UserAplication, objDueDocumentNumberRequest.DocumentIdentity, objDueDocumentNumberRequest.NumberDocument); })
            };

            return objDueDocumentNumberResponse;
        }

        public static Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse GetStatusAccountDifferent(Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountRequest objStatusAccountRequest)
        {
            Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse objStatusAccountResponse = new Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse();
           
            if(objStatusAccountRequest.plataformaAT.ToUpper()==Constants.TOBE){
               objStatusAccountResponse = GetDataStatusAccountToBe(objStatusAccountRequest);
            }else{
               objStatusAccountResponse = StatusAccount(objStatusAccountRequest);
            }
            return objStatusAccountResponse;
        }


        /// <summary>
        /// Método para obtener el estado de cuenta.
        /// </summary>
        /// <param name="objStatusAccountRequest">Contiene id de cliente.</param>
        /// <returns>Devuelve objeto StatusAccountResponsecon información del estado de cuenta.</returns>
        public static Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse StatusAccount(Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountRequest objStatusAccountRequest)
        {
            try
            {
                bool isEnabled;
                Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse objStatusAccountResponse = new Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse();

                bool blMessageStatusAccountVisible;
                string strMessageStatusAccount;
                string strErrCod = string.Empty;
                List<StatusAccount> listAccountStatus = null;
                List<StatusAccountAOC> listAccountStatusAOC;
                List<StatusAccountAOC> listAccountStatusFinal = new List<StatusAccountAOC>();
                List<StatusAccountAOC> listAccountStatusFinal2 = new List<StatusAccountAOC>();
                if (!objStatusAccountRequest.isHR)
                {
                    if (objStatusAccountRequest.OriginType.Equals(Claro.SIACU.Constants.PostpaidMajuscule) || objStatusAccountRequest.OriginType.Equals(Claro.SIACU.Constants.DTH) || objStatusAccountRequest.OriginType.Equals(Claro.SIACU.Constants.IFI))
                    {
                        listAccountStatus = Claro.Web.Logging.ExecuteMethod<List<StatusAccount>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.StatusAccount(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, int.Parse(objStatusAccountRequest.CustomerId)); });
                    }
                    listAccountStatusAOC = Claro.Web.Logging.ExecuteMethod<List<StatusAccountAOC>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetStatusAccountAOC(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, objStatusAccountRequest.UserName, objStatusAccountRequest.CustomerId, ref strErrCod); });
                    if (listAccountStatus == null)
                    {
                        listAccountStatus = new List<StatusAccount>();
                    }

                    if ((listAccountStatus != null) && (listAccountStatus.Count > 0))
                    {
                        foreach (var item2 in listAccountStatus)
                        {
                            StatusAccountAOC objStatusAcccountTemp = new StatusAccountAOC()
                            {

                                Abono = item2.Abono,
                                Cargo = item2.Cargo,
                                CorrelativoId = item2.CorrelativoId,
                                DescripcionPago = item2.DescripcionPago,
                                Documento = item2.Documento,
                                Fecha = item2.Fecha,
                                FechaEmision = item2.FechaEmision,
                                FechaRegistro = item2.FechaRegistro,
                                FechaVencimiento = item2.FechaVencimiento,
                                ImportePendiente = item2.ImportePendiente,
                                Tipo = item2.Tipo,
                                Usuario = item2.Usuario,
                                MontoReclamo = Claro.Constants.NumberZeroString

                            };
                            listAccountStatusFinal.Add(objStatusAcccountTemp);
                        }
                    }
                    if ((listAccountStatusAOC != null) && (listAccountStatusAOC.Count > 0))
                    {
                        for (int i = 0; i < listAccountStatusAOC.Count; i++)
                        {
                            int intNoExist = 0;
                            StatusAccountAOC objStatusAcccountTemp2;
                            objStatusAcccountTemp2 = listAccountStatusAOC[i];

                            if ((listAccountStatusFinal != null) && (listAccountStatusFinal.Count > 0))
                            {
                                for (int j = 0; j < listAccountStatusFinal.Count; j++)
                                {
                                    StatusAccountAOC objStatusAcccountTemp3;
                                    objStatusAcccountTemp3 = listAccountStatusFinal[j];
                                    string strDocStatus = objStatusAcccountTemp3 != null && !string.IsNullOrWhiteSpace(objStatusAcccountTemp3.Documento) ? objStatusAcccountTemp3.Documento.Trim() : "";
                                    string strDocStatus2 = objStatusAcccountTemp2 != null && !string.IsNullOrWhiteSpace(objStatusAcccountTemp2.Documento) ? objStatusAcccountTemp2.Documento.Trim() : "";
                                    if (strDocStatus == strDocStatus2)
                                    {
                                        if (listAccountStatusFinal[j] != null)
                                        {
                                            listAccountStatusFinal[j].ImportePendiente = objStatusAcccountTemp2 != null && !string.IsNullOrWhiteSpace(objStatusAcccountTemp2.ImportePendiente) ? objStatusAcccountTemp2.ImportePendiente : "";
                                            listAccountStatusFinal[j].MontoReclamo = objStatusAcccountTemp2 != null && !string.IsNullOrWhiteSpace(objStatusAcccountTemp2.MontoReclamo) ? objStatusAcccountTemp2.MontoReclamo : ""; //SD_Monto_Reclamo
                                            intNoExist = 1;
                                            break;
                                        }
                                    }
                                }
                                if (!(intNoExist == 1))
                                {
                                    if (listAccountStatusFinal2 != null && objStatusAcccountTemp2 != null)
                                    {
                                        listAccountStatusFinal2.Add(objStatusAcccountTemp2);
                                    }
                                }

                            }
                            else
                            {
                                if (listAccountStatusFinal2 != null && objStatusAcccountTemp2 != null)
                                {
                                    listAccountStatusFinal2.Add(objStatusAcccountTemp2);
                                }
                            }
                        }

                    }
                    if ((listAccountStatusFinal2 != null) && (listAccountStatusFinal2.Count > 0))
                    {
                        foreach (StatusAccountAOC item5 in listAccountStatusFinal2)
                        {
                            StatusAccountAOC objStatusAcccountTemp4 = new StatusAccountAOC();
                            objStatusAcccountTemp4.Tipo = item5.Tipo;
                            objStatusAcccountTemp4.Documento = item5.Documento;
                            objStatusAcccountTemp4.DescripcionPago = item5.DescripcionPago;
                            objStatusAcccountTemp4.DescripcionDetalle = item5.DescripcionDetalle;
                            objStatusAcccountTemp4.FechaRegistro = item5.FechaRegistro;
                            objStatusAcccountTemp4.FechaEmision = item5.FechaEmision;
                            objStatusAcccountTemp4.FechaVencimiento = item5.FechaVencimiento;
                            objStatusAcccountTemp4.Cargo = item5.Cargo;
                            objStatusAcccountTemp4.Abono = item5.Abono;
                            objStatusAcccountTemp4.ImportePendiente = item5.ImportePendiente;
                            objStatusAcccountTemp4.MontoReclamo = item5.MontoReclamo;
                            objStatusAcccountTemp4.flagBotonAnulacion = item5.flagBotonAnulacion;
                            objStatusAcccountTemp4.EstadoDocumento = item5.EstadoDocumento;
                            try
                            {
                                if (!((KEY.AppSettings("strExisteDescPagoOAC").Trim().ToUpper() == objStatusAcccountTemp4.DescripcionDetalle.Trim().ToUpper()) ||
                                   (KEY.AppSettings("strExisteDescPagoOAC").Trim().ToUpper() == objStatusAcccountTemp4.DescripcionPago.Trim().ToUpper()) ||
                                    (SearchString(objStatusAccountRequest.Audit, objStatusAcccountTemp4.DescripcionDetalle, KEY.AppSettings("Filtro_EECC_OAC")) &&
                                    (objStatusAcccountTemp4.Tipo.ToUpper() == KEY.AppSettings("strNotaCredito").Trim().ToUpper() || objStatusAcccountTemp4.Tipo.ToUpper() == KEY.AppSettings("strNotaDebito").Trim().ToUpper()))))
                                {
                                    listAccountStatusFinal.Add(objStatusAcccountTemp4);
                                }
                            }
                            catch (Exception ex2)
                            {
                                Claro.Web.Logging.Error(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "Error: " + ex2.Message);

                            }

                        }
                    }
                    listAccountStatusFinal = FilterPayments(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, listAccountStatusFinal, out isEnabled, out strMessageStatusAccount, out blMessageStatusAccountVisible);
                    listAccountStatusFinal = listAccountStatusFinal.OrderBy(a => DateTime.ParseExact(a.FechaEmision, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();

                    bool isEnabledAux = false;
                    string strMessageStatusAccountAux = "";
                    bool blMessageStatusAccountVisibleAux = false;
                    if (isEnabled)
                    {
                        isEnabledAux = isEnabled;
                        strMessageStatusAccountAux = strMessageStatusAccount;
                        blMessageStatusAccountVisibleAux = blMessageStatusAccountVisible;
                    }
                    Claro.Web.Logging.Info(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "isEnabledAux: " + isEnabledAux.ToString() + ", strMessageStatusAccountAux: " + strMessageStatusAccountAux + ", blMessageStatusAccountVisibleAux: " + blMessageStatusAccountVisibleAux.ToString());

                    objStatusAccountResponse.ListStatusAccount = FilterPayments(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, listAccountStatusFinal, out isEnabled, out strMessageStatusAccount, out blMessageStatusAccountVisible);
                    
                    if (isEnabledAux)
                    {
                        objStatusAccountResponse.IsEnabled = isEnabledAux;
                        objStatusAccountResponse.blMessageStatusAccountVisible = blMessageStatusAccountVisibleAux;
                        objStatusAccountResponse.strMessageStatusAccount = strMessageStatusAccountAux;
                    }
                    else
                    {
                    objStatusAccountResponse.IsEnabled = isEnabled;
                    objStatusAccountResponse.blMessageStatusAccountVisible = blMessageStatusAccountVisible;
                    objStatusAccountResponse.strMessageStatusAccount = strMessageStatusAccount;
                    }
                    Claro.Web.Logging.Info(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "objStatusAccountResponse.IsEnabled: " + objStatusAccountResponse.IsEnabled.ToString() + ", objStatusAccountResponse.strMessageStatusAccount: " + objStatusAccountResponse.strMessageStatusAccount + ", objStatusAccountResponse.blMessageStatusAccountVisible: " + objStatusAccountResponse.blMessageStatusAccountVisible.ToString());

                }
                else
                {
                    listAccountStatus = Claro.Web.Logging.ExecuteMethod<List<StatusAccount>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.StatusAccount(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, int.Parse(objStatusAccountRequest.CustomerId)); });
                    listAccountStatusAOC = Claro.Web.Logging.ExecuteMethod<List<StatusAccountAOC>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetStatusAccountAOC(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, objStatusAccountRequest.UserName, objStatusAccountRequest.CustomerId, ref strErrCod); });

                    if (listAccountStatus == null)
                    {
                        listAccountStatus = new List<StatusAccount>();
                    }

                    if ((listAccountStatus != null) && (listAccountStatus.Count > 0))
                    {
                        foreach (var item2 in listAccountStatus)
                        {
                            StatusAccountAOC objStatusAcccountTemp = new StatusAccountAOC()
                            {

                                Abono = item2.Abono,
                                Cargo = item2.Cargo,
                                CorrelativoId = item2.CorrelativoId,
                                DescripcionPago = item2.DescripcionPago,
                                Documento = item2.Documento,
                                FechaEmision = item2.FechaEmision,
                                FechaRegistro = item2.FechaRegistro,
                                FechaVencimiento = item2.FechaVencimiento,
                                ImportePendiente = item2.ImportePendiente,
                                Tipo = item2.Tipo,
                            };
                            listAccountStatusFinal.Add(objStatusAcccountTemp);
                        }
                    }
                    if ((listAccountStatusAOC != null) && (listAccountStatusAOC.Count > 0))
                    {
                        for (int i = 0; i < listAccountStatusAOC.Count; i++)
                        {


                            StatusAccountAOC objStatusAcccountTemp2 = new StatusAccountAOC()
                            {

                                Abono = listAccountStatusAOC[i].Abono,
                                Cargo = listAccountStatusAOC[i].Cargo,
                                CorrelativoId = listAccountStatusAOC[i].CorrelativoId,
                                DescripcionPago = listAccountStatusAOC[i].DescripcionPago,
                                Documento = listAccountStatusAOC[i].Documento,
                                FechaEmision = listAccountStatusAOC[i].FechaEmision,
                                FechaRegistro = listAccountStatusAOC[i].FechaRegistro,
                                FechaVencimiento = listAccountStatusAOC[i].FechaVencimiento,
                                ImportePendiente = listAccountStatusAOC[i].ImportePendiente,
                                Tipo = listAccountStatusAOC[i].Tipo,
                                flagBotonAnulacion = listAccountStatusAOC[i].flagBotonAnulacion,
                                EstadoDocumento = listAccountStatusAOC[i].EstadoDocumento
                            };

                            if (listAccountStatusFinal != null) listAccountStatusFinal.Add(objStatusAcccountTemp2);
                        }
                    }

                    listAccountStatusFinal = listAccountStatusFinal.OrderBy(a => DateTime.ParseExact(a.FechaEmision, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();

                    objStatusAccountResponse.ListStatusAccount = listAccountStatusFinal;
                    objStatusAccountResponse.blMessageStatusAccountVisible = false;
                    objStatusAccountResponse.strMessageStatusAccount = "";

                }

                return objStatusAccountResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "Error: " + ex.Message);
                return null;
            }


        }

        public static Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse GetDataStatusAccountToBe(Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountRequest objStatusAccountRequest)
        {
            try
            {
                bool isEnabled;
                Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse objStatusAccountResponse = new Entity.Dashboard.Postpaid.GetStatusAccount.StatusAccountResponse();

                bool blMessageStatusAccountVisible;
                string strMessageStatusAccount;
                string strErrCod = string.Empty;
                string flagToBe = ConfigurationManager.AppSettings("flagToBe");
                string strFlagMigrado = string.Empty;
                List<StatusAccount> listAccountStatus = null;
                List<StatusAccountAOC> listAccountStatusAOC = null;
                List<StatusAccountAOC> listAccountStatusFinal = new List<StatusAccountAOC>();
                List<StatusAccountAOC> listAccountStatusFinal2 = new List<StatusAccountAOC>();

                if (flagToBe.Equals("1"))
                {
                    if(!string.IsNullOrEmpty(objStatusAccountRequest.csIdPub) && !objStatusAccountRequest.csIdPub.Contains("CUST")) //MIGRADO
                    {
                        Claro.Web.Logging.Info(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "Cliente Migrado");
                        if (objStatusAccountRequest.OriginType.Equals(Claro.SIACU.Constants.PostpaidMajuscule) || objStatusAccountRequest.OriginType.Equals(Claro.SIACU.Constants.DTH) || objStatusAccountRequest.OriginType.Equals(Claro.SIACU.Constants.IFI))
                        {
                            listAccountStatus = Claro.Web.Logging.ExecuteMethod<List<StatusAccount>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.StatusAccount(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, int.Parse(objStatusAccountRequest.CustomerId)); });
                        }
                    }

                    if (objStatusAccountRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS))
                    {
                        listAccountStatusAOC = Claro.Web.Logging.ExecuteMethod<List<StatusAccountAOC>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetStatusAccountAOCToBe(objStatusAccountRequest.Audit, objStatusAccountRequest.Audit.Transaction, objStatusAccountRequest.UserName, objStatusAccountRequest.CustomerId); });
                    }
                    else if (objStatusAccountRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE))
                    {
                        listAccountStatusAOC = Claro.Web.Logging.ExecuteMethod<List<StatusAccountAOC>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetStatusAccountAOCToBe(objStatusAccountRequest.Audit, objStatusAccountRequest.Audit.Transaction, objStatusAccountRequest.UserName, objStatusAccountRequest.csIdPub); });
                    }

                    if (listAccountStatus == null)
                    {
                        listAccountStatus = new List<StatusAccount>();
                    }

                    if ((listAccountStatus != null) && (listAccountStatus.Count > 0))
                    {
                        Claro.Web.Logging.Info(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "listAccountStatusBscs no es nulo");
                        foreach (var item2 in listAccountStatus)
                        {
                            StatusAccountAOC objStatusAcccountTemp = new StatusAccountAOC()
                            {

                                Abono = item2.Abono,
                                Cargo = item2.Cargo,
                                CorrelativoId = item2.CorrelativoId,
                                DescripcionPago = item2.DescripcionPago,
                                Documento = item2.Documento,
                                Fecha = item2.Fecha,
                                FechaEmision = item2.FechaEmision,
                                FechaRegistro = item2.FechaRegistro,
                                FechaVencimiento = item2.FechaVencimiento,
                                ImportePendiente = item2.ImportePendiente,
                                Tipo = item2.Tipo,
                                Usuario = item2.Usuario,
                                MontoReclamo = Claro.Constants.NumberZeroString

                            };
                            listAccountStatusFinal.Add(objStatusAcccountTemp);
                        }
                    }

                    if ((listAccountStatusAOC != null) && (listAccountStatusAOC.Count > 0))
                    {
                        foreach (StatusAccountAOC item5 in listAccountStatusAOC)
                        {
                            StatusAccountAOC objStatusAcccountTemp4 = new StatusAccountAOC();
                            objStatusAcccountTemp4.Tipo = item5.Tipo;
                            objStatusAcccountTemp4.Documento = item5.Documento;
                            objStatusAcccountTemp4.DescripcionPago = item5.DescripcionPago;
                            objStatusAcccountTemp4.DescripcionDetalle = item5.DescripcionDetalle;
                            objStatusAcccountTemp4.FechaRegistro = item5.FechaRegistro;
                            objStatusAcccountTemp4.FechaEmision = item5.FechaEmision;
                            objStatusAcccountTemp4.FechaVencimiento = item5.FechaVencimiento;
                            objStatusAcccountTemp4.Cargo = item5.Cargo;
                            objStatusAcccountTemp4.Abono = item5.Abono;
                            objStatusAcccountTemp4.ImportePendiente = item5.ImportePendiente;
                            objStatusAcccountTemp4.MontoReclamo = item5.MontoReclamo;
                            objStatusAcccountTemp4.idOnbase = item5.idOnbase;
                            try
                            {
                                if (!((KEY.AppSettings("strExisteDescPagoOAC").Trim().ToUpper() == objStatusAcccountTemp4.DescripcionDetalle.Trim().ToUpper()) ||
                                   (KEY.AppSettings("strExisteDescPagoOAC").Trim().ToUpper() == objStatusAcccountTemp4.DescripcionPago.Trim().ToUpper()) ||
                                    (SearchString(objStatusAccountRequest.Audit, objStatusAcccountTemp4.DescripcionDetalle, KEY.AppSettings("Filtro_EECC_OAC")) &&
                                    (objStatusAcccountTemp4.Tipo.ToUpper() == KEY.AppSettings("strNotaCredito").Trim().ToUpper() || objStatusAcccountTemp4.Tipo.ToUpper() == KEY.AppSettings("strNotaDebito").Trim().ToUpper()))))
                                {
                                    listAccountStatusFinal.Add(objStatusAcccountTemp4);
                                }
                            }
                            catch (Exception ex2)
                            {
                                Claro.Web.Logging.Error(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "Error: " + ex2.Message);

                            }
                        }
                    }

                    listAccountStatusFinal = FilterPayments(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, listAccountStatusFinal, out isEnabled, out strMessageStatusAccount, out blMessageStatusAccountVisible);
                    listAccountStatusFinal = listAccountStatusFinal.OrderBy(a => DateTime.ParseExact(a.FechaEmision, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();

                    bool isEnabledAux = false;
                    string strMessageStatusAccountAux = "";
                    bool blMessageStatusAccountVisibleAux = false;
                    if (isEnabled)
                    {
                        isEnabledAux = isEnabled;
                        strMessageStatusAccountAux = strMessageStatusAccount;
                        blMessageStatusAccountVisibleAux = blMessageStatusAccountVisible;
                    }
                    Claro.Web.Logging.Info(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "isEnabledAux: " + isEnabledAux.ToString() + ", strMessageStatusAccountAux: " + strMessageStatusAccountAux + ", blMessageStatusAccountVisibleAux: " + blMessageStatusAccountVisibleAux.ToString());
					
                    objStatusAccountResponse.ListStatusAccount = FilterPayments(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, listAccountStatusFinal, out isEnabled, out strMessageStatusAccount, out blMessageStatusAccountVisible);
                    
                    if (isEnabledAux)
                    {
                        objStatusAccountResponse.IsEnabled = isEnabledAux;
                        objStatusAccountResponse.blMessageStatusAccountVisible = blMessageStatusAccountVisibleAux;
                        objStatusAccountResponse.strMessageStatusAccount = strMessageStatusAccountAux;
                    }
                    else
                    {
                    objStatusAccountResponse.IsEnabled = isEnabled;
                    objStatusAccountResponse.blMessageStatusAccountVisible = blMessageStatusAccountVisible;
                    objStatusAccountResponse.strMessageStatusAccount = strMessageStatusAccount;
                }
                    Claro.Web.Logging.Info(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "objStatusAccountResponse.IsEnabled: " + objStatusAccountResponse.IsEnabled.ToString() + ", objStatusAccountResponse.strMessageStatusAccount: " + objStatusAccountResponse.strMessageStatusAccount + ", objStatusAccountResponse.blMessageStatusAccountVisible: " + objStatusAccountResponse.blMessageStatusAccountVisible.ToString());

                }
                else if (flagToBe.Equals("0"))
                {
                    if (!objStatusAccountRequest.isHR)
                    {
                        if (objStatusAccountRequest.OriginType.Equals(Claro.SIACU.Constants.PostpaidMajuscule) || objStatusAccountRequest.OriginType.Equals(Claro.SIACU.Constants.DTH) || objStatusAccountRequest.OriginType.Equals(Claro.SIACU.Constants.IFI))
                        {
                            listAccountStatus = Claro.Web.Logging.ExecuteMethod<List<StatusAccount>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.StatusAccount(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, int.Parse(objStatusAccountRequest.CustomerId)); });
                        }
                        listAccountStatusAOC = Claro.Web.Logging.ExecuteMethod<List<StatusAccountAOC>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetStatusAccountAOCToBe(objStatusAccountRequest.Audit, objStatusAccountRequest.Audit.Transaction, objStatusAccountRequest.UserName, objStatusAccountRequest.CustomerId); });

                        if (listAccountStatus == null)
                        {
                            listAccountStatus = new List<StatusAccount>();
                        }

                        if ((listAccountStatus != null) && (listAccountStatus.Count > 0))
                        {
                            foreach (var item2 in listAccountStatus)
                            {
                                StatusAccountAOC objStatusAcccountTemp = new StatusAccountAOC()
                                {

                                    Abono = item2.Abono,
                                    Cargo = item2.Cargo,
                                    CorrelativoId = item2.CorrelativoId,
                                    DescripcionPago = item2.DescripcionPago,
                                    Documento = item2.Documento,
                                    Fecha = item2.Fecha,
                                    FechaEmision = item2.FechaEmision,
                                    FechaRegistro = item2.FechaRegistro,
                                    FechaVencimiento = item2.FechaVencimiento,
                                    ImportePendiente = item2.ImportePendiente,
                                    Tipo = item2.Tipo,
                                    Usuario = item2.Usuario,
                                    MontoReclamo = Claro.Constants.NumberZeroString

                                };
                                listAccountStatusFinal.Add(objStatusAcccountTemp);
                            }
                        }
                        if ((listAccountStatusAOC != null) && (listAccountStatusAOC.Count > 0))
                        {
                            for (int i = 0; i < listAccountStatusAOC.Count; i++)
                            {
                                int intNoExist = 0;
                                StatusAccountAOC objStatusAcccountTemp2;
                                objStatusAcccountTemp2 = listAccountStatusAOC[i];

                                if ((listAccountStatusFinal != null) && (listAccountStatusFinal.Count > 0))
                                {
                                    for (int j = 0; j < listAccountStatusFinal.Count; j++)
                                    {
                                        StatusAccountAOC objStatusAcccountTemp3;
                                        objStatusAcccountTemp3 = listAccountStatusFinal[j];
                                        string strDocStatus = objStatusAcccountTemp3 != null && !string.IsNullOrWhiteSpace(objStatusAcccountTemp3.Documento) ? objStatusAcccountTemp3.Documento.Trim() : "";
                                        string strDocStatus2 = objStatusAcccountTemp2 != null && !string.IsNullOrWhiteSpace(objStatusAcccountTemp2.Documento) ? objStatusAcccountTemp2.Documento.Trim() : "";

                                        if (strDocStatus == strDocStatus2 && listAccountStatusFinal[j] != null)
                                        {
                                            listAccountStatusFinal[j].ImportePendiente = objStatusAcccountTemp2 != null && !string.IsNullOrWhiteSpace(objStatusAcccountTemp2.ImportePendiente) ? objStatusAcccountTemp2.ImportePendiente : "";
                                            listAccountStatusFinal[j].MontoReclamo = objStatusAcccountTemp2 != null && !string.IsNullOrWhiteSpace(objStatusAcccountTemp2.MontoReclamo) ? objStatusAcccountTemp2.MontoReclamo : ""; //SD_Monto_Reclamo
                                            intNoExist = 1;
                                            break;
                                        }

                                    }

                                    if (intNoExist != 1 && listAccountStatusFinal2 != null && objStatusAcccountTemp2 != null)
                                    {
                                        listAccountStatusFinal2.Add(objStatusAcccountTemp2);
                                    }


                                }
                                else
                                {
                                    if (listAccountStatusFinal2 != null && objStatusAcccountTemp2 != null)
                                    {
                                        listAccountStatusFinal2.Add(objStatusAcccountTemp2);
                                    }
                                }
                            }

                        }
                        if ((listAccountStatusFinal2 != null) && (listAccountStatusFinal2.Count > 0))
                        {
                            foreach (StatusAccountAOC item5 in listAccountStatusFinal2)
                            {
                                StatusAccountAOC objStatusAcccountTemp4 = new StatusAccountAOC();
                                objStatusAcccountTemp4.Tipo = item5.Tipo;
                                objStatusAcccountTemp4.Documento = item5.Documento;
                                objStatusAcccountTemp4.DescripcionPago = item5.DescripcionPago;
                                objStatusAcccountTemp4.DescripcionDetalle = item5.DescripcionDetalle;
                                objStatusAcccountTemp4.FechaRegistro = item5.FechaRegistro;
                                objStatusAcccountTemp4.FechaEmision = item5.FechaEmision;
                                objStatusAcccountTemp4.FechaVencimiento = item5.FechaVencimiento;
                                objStatusAcccountTemp4.Cargo = item5.Cargo;
                                objStatusAcccountTemp4.Abono = item5.Abono;
                                objStatusAcccountTemp4.ImportePendiente = item5.ImportePendiente;
                                objStatusAcccountTemp4.MontoReclamo = item5.MontoReclamo;
                                try
                                {
                                    if (!((KEY.AppSettings("strExisteDescPagoOAC").Trim().ToUpper() == objStatusAcccountTemp4.DescripcionDetalle.Trim().ToUpper()) ||
                                       (KEY.AppSettings("strExisteDescPagoOAC").Trim().ToUpper() == objStatusAcccountTemp4.DescripcionPago.Trim().ToUpper()) ||
                                        (SearchString(objStatusAccountRequest.Audit, objStatusAcccountTemp4.DescripcionDetalle, KEY.AppSettings("Filtro_EECC_OAC")) &&
                                        (objStatusAcccountTemp4.Tipo.ToUpper() == KEY.AppSettings("strNotaCredito").Trim().ToUpper() || objStatusAcccountTemp4.Tipo.ToUpper() == KEY.AppSettings("strNotaDebito").Trim().ToUpper()))))
                                    {
                                        listAccountStatusFinal.Add(objStatusAcccountTemp4);
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    Claro.Web.Logging.Error(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "Error: " + ex2.Message);

                                }

                            }
                        }
                        listAccountStatusFinal = FilterPayments(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, listAccountStatusFinal, out isEnabled, out strMessageStatusAccount, out blMessageStatusAccountVisible);
                        listAccountStatusFinal = listAccountStatusFinal.OrderBy(a => DateTime.ParseExact(a.FechaEmision, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();

                        bool isEnabledAux = false;
                        string strMessageStatusAccountAux = "";
                        bool blMessageStatusAccountVisibleAux = false;
                        if (isEnabled)
                        {
                            isEnabledAux = isEnabled;
                            strMessageStatusAccountAux = strMessageStatusAccount;
                            blMessageStatusAccountVisibleAux = blMessageStatusAccountVisible;
                        }
                        Claro.Web.Logging.Info(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "isEnabledAux: " + isEnabledAux.ToString() + ", strMessageStatusAccountAux: " + strMessageStatusAccountAux + ", blMessageStatusAccountVisibleAux: " + blMessageStatusAccountVisibleAux.ToString());

                        objStatusAccountResponse.ListStatusAccount = FilterPayments(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, listAccountStatusFinal, out isEnabled, out strMessageStatusAccount, out blMessageStatusAccountVisible);
                        
                        if (isEnabledAux)
                        {
                            objStatusAccountResponse.IsEnabled = isEnabledAux;
                            objStatusAccountResponse.blMessageStatusAccountVisible = blMessageStatusAccountVisibleAux;
                            objStatusAccountResponse.strMessageStatusAccount = strMessageStatusAccountAux;
                        }
                        else
                        {
                        objStatusAccountResponse.IsEnabled = isEnabled;
                        objStatusAccountResponse.blMessageStatusAccountVisible = blMessageStatusAccountVisible;
                        objStatusAccountResponse.strMessageStatusAccount = strMessageStatusAccount;
                        }
                        Claro.Web.Logging.Info(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "objStatusAccountResponse.IsEnabled: " + objStatusAccountResponse.IsEnabled.ToString() + ", objStatusAccountResponse.strMessageStatusAccount: " + objStatusAccountResponse.strMessageStatusAccount + ", objStatusAccountResponse.blMessageStatusAccountVisible: " + objStatusAccountResponse.blMessageStatusAccountVisible.ToString());

                    }
                    else
                    {
                        listAccountStatus = Claro.Web.Logging.ExecuteMethod<List<StatusAccount>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.StatusAccount(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, int.Parse(objStatusAccountRequest.CustomerId)); });
                        listAccountStatusAOC = Claro.Web.Logging.ExecuteMethod<List<StatusAccountAOC>>(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetStatusAccountAOC(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, objStatusAccountRequest.UserName, objStatusAccountRequest.CustomerId, ref strErrCod); });

                        if (listAccountStatus == null)
                        {
                            listAccountStatus = new List<StatusAccount>();
                        }

                        if ((listAccountStatus != null) && (listAccountStatus.Count > 0))
                        {
                            foreach (var item2 in listAccountStatus)
                            {
                                StatusAccountAOC objStatusAcccountTemp = new StatusAccountAOC()
                                {

                                    Abono = item2.Abono,
                                    Cargo = item2.Cargo,
                                    CorrelativoId = item2.CorrelativoId,
                                    DescripcionPago = item2.DescripcionPago,
                                    Documento = item2.Documento,
                                    FechaEmision = item2.FechaEmision,
                                    FechaRegistro = item2.FechaRegistro,
                                    FechaVencimiento = item2.FechaVencimiento,
                                    ImportePendiente = item2.ImportePendiente,
                                    Tipo = item2.Tipo,
                                };
                                listAccountStatusFinal.Add(objStatusAcccountTemp);
                            }
                        }
                        if ((listAccountStatusAOC != null) && (listAccountStatusAOC.Count > 0))
                        {
                            for (int i = 0; i < listAccountStatusAOC.Count; i++)
                            {


                                StatusAccountAOC objStatusAcccountTemp2 = new StatusAccountAOC()
                                {

                                    Abono = listAccountStatusAOC[i].Abono,
                                    Cargo = listAccountStatusAOC[i].Cargo,
                                    CorrelativoId = listAccountStatusAOC[i].CorrelativoId,
                                    DescripcionPago = listAccountStatusAOC[i].DescripcionPago,
                                    Documento = listAccountStatusAOC[i].Documento,
                                    FechaEmision = listAccountStatusAOC[i].FechaEmision,
                                    FechaRegistro = listAccountStatusAOC[i].FechaRegistro,
                                    FechaVencimiento = listAccountStatusAOC[i].FechaVencimiento,
                                    ImportePendiente = listAccountStatusAOC[i].ImportePendiente,
                                    Tipo = listAccountStatusAOC[i].Tipo,
                                };

                                if (listAccountStatusFinal != null) listAccountStatusFinal.Add(objStatusAcccountTemp2);
                            }
                        }

                        listAccountStatusFinal = listAccountStatusFinal.OrderBy(a => DateTime.ParseExact(a.FechaEmision, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToList();

                        objStatusAccountResponse.ListStatusAccount = listAccountStatusFinal;
                        objStatusAccountResponse.blMessageStatusAccountVisible = false;
                        objStatusAccountResponse.strMessageStatusAccount = "";

                    }
                }


                return objStatusAccountResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objStatusAccountRequest.Audit.Session, objStatusAccountRequest.Audit.Transaction, "Error: " + ex.Message);
                return null;
            }


        }

        private static bool SearchString(Claro.Entity.AuditRequest objAudit, string strString, string strStringSearch)
        {
            int pos, start;
            bool result = false;
            try
            {
                strString = strString.Trim().ToUpperInvariant();
                strStringSearch = strStringSearch.Trim().ToUpperInvariant();
                start = Claro.Constants.NumberOne;
                for (int i = 0; i < strString.Length; i++)
                {
                    pos = strString.IndexOf(strStringSearch, start, StringComparison.InvariantCulture);
                    if (pos != Claro.Constants.NumberZero)
                    {
                        start = pos + Claro.Constants.NumberOne;
                        result = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }
            return result;
        }
        /// <summary>
        /// Método para obtener el listado de pagos filtrados.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strIdTransaction">Id de transacción</param>
        /// <param name="listAccountStatus">Lista de estados de cuenta</param>
        /// <returns>Devuelve el listado de objetos con información de los estados de cuenta.</returns>
        private static List<StatusAccountAOC> FilterPayments(string strIdSession, string strTransaction, List<StatusAccountAOC> listAccountStatus, out bool isEnabled, out string strMessageStatusAccount, out bool blMessageStatusAccountVisible)
        {
            List<StatusAccountAOC> listFilterAccountStatus = null;
            isEnabled = false;
            strMessageStatusAccount = "";
            blMessageStatusAccountVisible = false;

            int StateButton = Claro.Constants.NumberZero;

            if (listAccountStatus != null && listAccountStatus.Count > Claro.Constants.NumberZero)
            {
                string strConstTipoDocServicioOAC = "";

                try
                {
                    strConstTipoDocServicioOAC = KEY.AppSettings("strConstTipoDocServicioOAC");
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, strTransaction, "Error:" + ex.Message);
                }
                Claro.Web.Logging.Info(strIdSession, strTransaction, "strConstTipoDocServicioOAC: " + strConstTipoDocServicioOAC);
                listFilterAccountStatus = new List<StatusAccountAOC>();
                double dblPaymentStatusAccount = Claro.Constants.NumberZero;
                try
                {
                    foreach (var item in listAccountStatus)
                    {
                        if (item.Tipo == strConstTipoDocServicioOAC)
                        {
                            StateButton++;
                            isEnabled = true;

                            if (item.ImportePendiente == Claro.SIACU.Constants.ZeroNumber)
                            {
                                item.Saldo = CalculateBalance(item.Tipo, item.Cargo, item.Abono, ref dblPaymentStatusAccount);
                                listFilterAccountStatus.Add(item);
                            }
                        }
                        else
                        {
                            item.Saldo = CalculateBalance(item.Tipo, item.Cargo, item.Abono, ref dblPaymentStatusAccount);
                            listFilterAccountStatus.Add(item);
                        }
                    }
                }
                catch (Exception ex3)
                {
                    Claro.Web.Logging.Error(strIdSession, strTransaction, "Error : " + ex3.Message);
                }

            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, "StateButton: " + StateButton.ToString());
            if (StateButton > Claro.Constants.NumberZero)
            {
                isEnabled = true;
                strMessageStatusAccount = KEY.AppSettings("gConstMensajeCuotaEquipo");
                blMessageStatusAccountVisible = true;
            }
            if (listFilterAccountStatus == null) Claro.Web.Logging.Info(strIdSession, strTransaction, "listFilterAccountStatus == null ");
            Claro.Web.Logging.Info(strIdSession, strTransaction, "isEnabled: " + isEnabled.ToString() + ", strMessageStatusAccount: " + strMessageStatusAccount + ", blMessageStatusAccountVisible: " + blMessageStatusAccountVisible.ToString());
            return listFilterAccountStatus;
        }

        /// <summary>
        /// Método para calcular el saldo de la cuenta.
        /// </summary>
        /// <param name="strType">Tipo de la cuenta</param>
        /// <param name="strCharge">Cargo de la cuenta</param>
        /// <param name="strPayment">Abono de la cuenta</param>
        /// <param name="strAmountPending">Importe pendiente de la cuenta</param>
        /// <param name="dblPaymentStatusAccount">Abono del estado de cuenta</param>
        /// <returns>Devuelve el listado de la cuenta con su respectivo saldo calculado.</returns>
        private static string CalculateBalance(string strType, string strCharge, string strPayment, ref double dblPaymentStatusAccount)
        {
            string strBalance = "";
            double dblBalance;
            string strChargeA = (strCharge.Trim() == "" ? Claro.Constants.NumberZeroString : strCharge.Trim());
            string strPaymentA = (strPayment.Trim() == "" ? Claro.Constants.NumberZeroString : strPayment.Trim());
            try
            {
                if (string.Equals(strType.Trim().ToUpperInvariant(), Claro.SIACU.Constants.BillMajuscule, StringComparison.OrdinalIgnoreCase))
                {
                    dblBalance = Convert.ToDouble(strChargeA) - Convert.ToDouble(strPaymentA);
                    dblPaymentStatusAccount = dblPaymentStatusAccount + dblBalance;
                    strBalance = dblPaymentStatusAccount.ToString(Claro.Constants.FormatDouble);
                }
                else
                {
                    dblBalance = Convert.ToDouble(dblPaymentStatusAccount);
                    dblBalance = dblBalance + Convert.ToDouble(strChargeA) - Convert.ToDouble(strPaymentA);
                    dblPaymentStatusAccount = dblBalance;
                    strBalance = dblBalance.ToString(Claro.Constants.FormatDouble);
                }
            }
            catch (Exception ex4)
            {

                Claro.Web.Logging.Error("", "", "Error: " + ex4.Message);
            }


            return strBalance;
        }
        /// <summary>
        /// Método para obtener información histórica de la línea o servicio.
        /// </summary>
        /// <param name="objServiceRequest"></param>
        /// <returns>Devuelve objeto ServiceResponse con información del servicio.</returns>
        public static POSTPAID.GetService.ServiceResponse GetDataLineHistory(POSTPAID.GetService.ServiceRequest objServiceRequest)
        {
            POSTPAID.GetService.ServiceResponse objServiceResponse = new POSTPAID.GetService.ServiceResponse();
            if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE))
            {
                if (objServiceRequest.flagConvivencia.Equals(Claro.Constants.NumberZeroString))
                {

                    objServiceRequest.flagConvivencia = Claro.Constants.NumberOneString;
                    objServiceResponse.ListService = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.LineHistoryTobe(objServiceRequest.Audit, objServiceRequest.Audit.Transaction, int.Parse(objServiceRequest.ContractID), objServiceRequest.flagConvivencia, objServiceRequest.coIdPub); });
                }
                else
                {
                    objServiceRequest.flagConvivencia = Claro.Constants.NumberTwoString;
                    objServiceResponse.ListService = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.LineHistoryTobe(objServiceRequest.Audit, objServiceRequest.Audit.Transaction, int.Parse(objServiceRequest.ContractID), objServiceRequest.flagConvivencia, objServiceRequest.coIdPub); });
                }
            }

            else
            {
                objServiceResponse.ListService = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Service>>(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.LineHistory(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, int.Parse(objServiceRequest.ContractID)); });
            }
            return objServiceResponse;
        }

        /// <summary>
        /// Método para obtener información HLR.
        /// </summary>
        /// <param name="objHLRRequest">Contiene número de teléfono.</param>
        /// <returns>Devuelve objeto HLRResponse con información HLR.</returns>
        public static POSTPAID.GetHLR.HLRResponse GetHLR(POSTPAID.GetHLR.HLRRequest objHLRRequest)
        {

            string strMessage;
            string strErrorMessage;
            string strTecnologia4G;
            string strPhoneNumber = GetNumber(objHLRRequest.Audit, false, objHLRRequest.PhoneNumber, objHLRRequest);

            POSTPAID.GetHLR.HLRResponse objHLRResponse = new POSTPAID.GetHLR.HLRResponse()
            {


                ListHLR = ConsultHLR(objHLRRequest.Audit, strPhoneNumber, out strTecnologia4G, out strMessage, out strErrorMessage, objHLRRequest),


                Message = strMessage,
                ErrorMessage = strErrorMessage,
                Tecnologia4G = strTecnologia4G
            };

            return objHLRResponse;
        }

        /// <summary>
        /// Método para obtener número.
        /// </summary>
        /// <param name="objAudit">Información de auditoría</param>
        /// <param name="isPais">¿Es país?</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <returns>Devuelve cadena con el número.</returns>
        public static string GetNumber(Claro.Entity.AuditRequest objAudit, bool isPais, string strPhoneNumber, POSTPAID.GetHLR.HLRRequest objHLRRequest)
        {
            string strFechaAVM = "", strResponse = "";
            try
            {
                strFechaAVM = KEY.AppSettings("strFechaAVM");

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            DateTime dFechaAVM;
            DateTime dFechaActual;
            string strNumberGenerated;
            dFechaAVM = Convert.ToDate(strFechaAVM);
            dFechaActual = Convert.ToDate(DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString());

            if (string.IsNullOrEmpty(strPhoneNumber))
            {
                strResponse = "";
            }
            else
            {
                if (strPhoneNumber.Length == 9 && dFechaActual < dFechaAVM)
                {
                    strNumberGenerated = GetGeneratedNumber(objAudit.Session, objAudit.Transaction, strPhoneNumber, objHLRRequest);
                    strResponse = (strNumberGenerated != "") ? (isPais) ? Claro.Constants.NumberFiftyOneString + strNumberGenerated.Trim() : strNumberGenerated.Trim() : "";
                }
                else
                {
                    strResponse = (isPais) ? Claro.Constants.NumberFiftyOneString + strPhoneNumber : strPhoneNumber;
                }
            }


            return strResponse;

        }

        /// <summary>
        /// Método para obtener número generado.
        /// </summary>
        /// <param name="strPhoneNumber">Número de teléfono.</param>
        /// <returns>Devuelve cadena con el número generado.</returns>
        public static string GetGeneratedNumber(string strIdSession, string strTransaction, string strPhoneNumber, POSTPAID.GetHLR.HLRRequest objHLRRequest)
        {
            string strNroGenerado = Claro.Web.Logging.ExecuteMethod<string>(objHLRRequest.Audit.Session, objHLRRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.ObtenerNumeroEAI(strIdSession, strTransaction, strPhoneNumber); });

            if (strNroGenerado == "")
            {
                strNroGenerado = Claro.Web.Logging.ExecuteMethod<string>(objHLRRequest.Audit.Session, objHLRRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.ObtenerNumeroGWP(strIdSession, strTransaction, strPhoneNumber); });
            }

            return strNroGenerado;
        }

        /// <summary>
        /// Método para obtener información HLR.
        /// </summary>
        /// <param name="objAudit">Información</param>
        /// <param name="strPhoneNumber">Número de teléfono</param>
        /// <param name="strTechnology4G">Tecnología 4G</param>
        /// <param name="strMessage">Mensaje</param>
        /// <param name="strErrorMessage">Mensaje de error</param>
        /// <returns>Devuelve listado de objetos con información HLR.> listHLR</returns>
        private static List<Entity.Dashboard.HLR> ConsultHLR(Claro.Entity.AuditRequest objAudit, string strPhoneNumber, out string strTechnology4G, out string strMessage, out string strErrorMessage, POSTPAID.GetHLR.HLRRequest objHLRRequest)
        {

            string strError = "";
            string strMsjeError4G = "";
            string strActionId = "";
            string strFieldName = "";

            try
            {
                strMsjeError4G = KEY.AppSettings("strMsjeError4G");
                strActionId = KEY.AppSettings("strIdAccionConsultaUDB");
                strFieldName = KEY.AppSettings("strParamMSISDNConectorUDB");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            List<Entity.Dashboard.HLR> listHLR = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.HLR>>(objHLRRequest.Audit.Session, objHLRRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetHLRUDB(objHLRRequest.Audit.Session, objHLRRequest.Audit.Transaction, objHLRRequest.Audit.ApplicationName, objHLRRequest.Audit.IPAddress, objHLRRequest.Audit.UserName, strPhoneNumber, strActionId, strFieldName, String.Empty, out strError); });

            if (strError.Equals(Claro.SIACU.Constants.ZeroNumber))
            {
                strTechnology4G = Common.GetServicesState4G(objAudit, listHLR, strError);
                strMessage = "";
                strErrorMessage = "";
            }
            else
            {
                strTechnology4G = "";
                strMessage = "";
                strErrorMessage = strMsjeError4G;
            }
            return listHLR;
        }

        /// <summary>
        /// Método para obtener información de acuerdo antiguo.
        /// </summary>
        /// <param name="objAgreementRequest">Contiene tipo y valor</param>
        /// <returns>Devuelve booleano del resultado. </returns>
        public static Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse GetExistsAgreementOld(Entity.Dashboard.Postpaid.GetAgreement.AgreementRequest objAgreementRequest)
        {
            Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse objAgremeentResponse = new Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse()
            {
                Result = Claro.Web.Logging.ExecuteMethod<bool>(objAgreementRequest.Audit.Session, objAgreementRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetExistsAgreementOld(objAgreementRequest.Audit.Session, objAgreementRequest.Audit.Transaction, objAgreementRequest.Type, objAgreementRequest.Value); }),
            };

            return objAgremeentResponse;
        }

        /// <summary>
        /// Método para obtener información de acuerdo nuevo.
        /// </summary>
        /// <param name="objAgreementRequest">Contiene tipo y valor.</param>
        /// <returns>Devuelve booleano del resultado.</returns>
        public static Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse GetExistAgreementNew(Entity.Dashboard.Postpaid.GetAgreement.AgreementRequest objAgreementRequest)
        {
            Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse ObjAgreementResponse = new Entity.Dashboard.Postpaid.GetAgreement.AgreementResponse()
            {
                Result = Claro.Web.Logging.ExecuteMethod<bool>(objAgreementRequest.Audit.Session, objAgreementRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetExistAgreementNew(objAgreementRequest.Audit.Session, objAgreementRequest.Audit.Transaction, objAgreementRequest.Type, objAgreementRequest.Value); }),
            };

            return ObjAgreementResponse;
        }

        /// <summary>
        /// Método para obtener estado de cuenta LDI.
        /// </summary>
        /// <param name="objGetStatusAccountLDIRequest">Contiene id de cliente.</param>
        /// <returns>Devuelve objeto StatusAccountLDIResponse con información de estado de cuenta LDI.</returns>
        public static Entity.Dashboard.Postpaid.GetStatusAccountLDI.StatusAccountLDIResponse StatusAccountLDI(Entity.Dashboard.Postpaid.GetStatusAccountLDI.StatusAccountLDIRequest objGetStatusAccountLDIRequest)
        {
            string strTipoFactura = "";
            string strTipoPago = "";

            try
            {
                strTipoFactura = KEY.AppSettings("constTipoStatusAccountLDIFacturas");
                strTipoPago = KEY.AppSettings("constTipoStatusAccountLDIPagos");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetStatusAccountLDIRequest.Audit.Session, objGetStatusAccountLDIRequest.Audit.Transaction, ex.Message);
            }

            Entity.Dashboard.Postpaid.GetStatusAccountLDI.StatusAccountLDIResponse objGetStatusAccountLDIResponse = new Entity.Dashboard.Postpaid.GetStatusAccountLDI.StatusAccountLDIResponse();

            try
            {
                objGetStatusAccountLDIResponse.Bills = Claro.Web.Logging.ExecuteMethod<List<StatusAccountLDI>>(objGetStatusAccountLDIRequest.Audit.Session, objGetStatusAccountLDIRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.StatusAccountLDI(objGetStatusAccountLDIRequest.Audit.Session, objGetStatusAccountLDIRequest.Audit.Transaction, objGetStatusAccountLDIRequest.CustomerId, strTipoFactura); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetStatusAccountLDIRequest.Audit.Session, objGetStatusAccountLDIRequest.Audit.Transaction, ex.Message);
            }
            try
            {
                objGetStatusAccountLDIResponse.Payments = Claro.Web.Logging.ExecuteMethod<List<StatusAccountLDI>>(objGetStatusAccountLDIRequest.Audit.Session, objGetStatusAccountLDIRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.StatusAccountLDI(objGetStatusAccountLDIRequest.Audit.Session, objGetStatusAccountLDIRequest.Audit.Transaction, objGetStatusAccountLDIRequest.CustomerId, strTipoPago); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetStatusAccountLDIRequest.Audit.Session, objGetStatusAccountLDIRequest.Audit.Transaction, ex.Message);
            }

            return objGetStatusAccountLDIResponse;
        }

        /// <summary>
        /// Método para obtener listado de motivos de préstamo o alquiler.
        /// </summary>
        /// <param name="objLoanRentalTypeRequest">No contiene información.</param>
        /// <returns>Devuelve objeto LoanRentalResponse con información de motivos de préstamo o alquiler.</returns>
        public static Entity.Common.GetLoanRentalType.LoanRentalResponse GetLoanRentalType(Entity.Common.GetLoanRentalType.LoanRentalTypeRequest objLoanRentalTypeRequest)
        {
            Entity.Common.GetLoanRentalType.LoanRentalResponse ObjLoanRentalTypeResponse = new Entity.Common.GetLoanRentalType.LoanRentalResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(objLoanRentalTypeRequest.Audit.Session, objLoanRentalTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("ListaMotivoPrestamoAlquiler"); }),
            };

            return ObjLoanRentalTypeResponse;
        }

        /// <summary>
        /// Método para obtener el listado de zonas de almacén.
        /// </summary>
        /// <param name="objLoanRentalTypeRequest">No contiene información.</param>
        /// <returns>Devuelve objeto LoanRentalResponse con información de zonas de almacén.</returns>
        public static Entity.Common.GetLoanRentalType.LoanRentalResponse GetLoanRentalListwarehouseArea(Entity.Common.GetLoanRentalType.LoanRentalTypeRequest objLoanRentalTypeRequest)
        {
            Entity.Common.GetLoanRentalType.LoanRentalResponse ObjLoanRentalTypeResponse = new Entity.Common.GetLoanRentalType.LoanRentalResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(objLoanRentalTypeRequest.Audit.Session, objLoanRentalTypeRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("ListaZonasAlmacen"); }),
            };

            return ObjLoanRentalTypeResponse;

        }

        /// <summary>
        /// Método para obtener información de alquiler o préstamo.
        /// </summary>
        /// <param name="objLoanRentalResquest">Contiene rango de fechas, número de documento, número y modelo de equipo.</param>
        /// <returns>Devuelve objeto LoanRentalResponse con información de alquiler o préstamo.</returns>
        public static Entity.Dashboard.Postpaid.GetLoanRental.LoanRentalResponse LoanRental(Entity.Dashboard.Postpaid.GetLoanRental.LoanRentalResquest objLoanRentalResquest)
        {
            Entity.Dashboard.Postpaid.GetLoanRental.LoanRentalResponse objLoanRentalReponse = null;
            POSTPAID.LoanRental objLoanRental = new POSTPAID.LoanRental()
            {
                lstCabAccounLoanRental = new List<POSTPAID.LoanRental>()
            };
            try
            {
                if (string.IsNullOrEmpty(objLoanRentalResquest.Number))
                {
                    objLoanRental.lstCabAccounLoanRental = Claro.Web.Logging.ExecuteMethod<List<LoanRental>>(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetLoanRentalEquipmentListWS(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, objLoanRentalResquest.StarDate, objLoanRentalResquest.EndDate, objLoanRentalResquest.NroDocumento); });

                    if (objLoanRental.lstCabAccounLoanRental.Count == 0)
                    {
                        objLoanRental.lstCabAccounLoanRental = Claro.Web.Logging.ExecuteMethod<List<LoanRental>>(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetloanRentalEquipmentBD(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, objLoanRentalResquest.StarDate, objLoanRentalResquest.EndDate, objLoanRentalResquest.NroDocumento, "", ""); });
                    }
                }
                else
                {

                    if (objLoanRentalResquest.Model.Equals(Claro.SIACU.Constants.Imei))
                    {
                        objLoanRental.lstCabAccounLoanRental = Claro.Web.Logging.ExecuteMethod<List<LoanRental>>(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetLoanRentalEquipmentListIMEI(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, objLoanRentalResquest.Number, objLoanRentalResquest.StarDate, objLoanRentalResquest.EndDate, objLoanRentalResquest.NroDocumento); });

                        if (objLoanRental.lstCabAccounLoanRental.Count == 0)
                        {
                            objLoanRental.lstCabAccounLoanRental = Claro.Web.Logging.ExecuteMethod<List<LoanRental>>(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetloanRentalEquipmentBD(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, objLoanRentalResquest.StarDate, objLoanRentalResquest.EndDate, objLoanRentalResquest.NroDocumento, "", objLoanRentalResquest.Number); });
                        }
                    }
                    if (objLoanRentalResquest.Model.Equals(Claro.SIACU.Constants.SIM))
                    {
                        objLoanRental.lstCabAccounLoanRental = Claro.Web.Logging.ExecuteMethod<List<LoanRental>>(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetLoanRentalEquipmentListSIM(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, objLoanRentalResquest.Number, objLoanRentalResquest.StarDate, objLoanRentalResquest.EndDate, objLoanRentalResquest.NroDocumento); });

                        if (objLoanRental.lstCabAccounLoanRental.Count == 0)
                        {
                            objLoanRental.lstCabAccounLoanRental = Claro.Web.Logging.ExecuteMethod<List<LoanRental>>(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetloanRentalEquipmentBD(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, objLoanRentalResquest.StarDate, objLoanRentalResquest.EndDate, objLoanRentalResquest.NroDocumento, objLoanRentalResquest.Number, ""); });
                        }
                    }
                }
                objLoanRentalReponse = new Entity.Dashboard.Postpaid.GetLoanRental.LoanRentalResponse()
                {
                    ObjLoanRental = objLoanRental
                };
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objLoanRentalResquest.Audit.Session, objLoanRentalResquest.Audit.Transaction, ex.Message);
            }
            return objLoanRentalReponse;
        }

        /// <summary>
        /// Método para obtener histórico de acciones.
        /// </summary>
        /// <param name="objHistoryActionsRequest">Contiene rango de fechas y número.</param>
        /// <returns>Devuelve objeto HistoryActionsResponse con información de histórico de acciones.</returns>
        public static Entity.Dashboard.Postpaid.GetHistoryActions.HistoryActionsResponse GetHistoryActions(Entity.Dashboard.Postpaid.GetHistoryActions.HistoryActionsRequest objHistoryActionsRequest)
        {
            Entity.Dashboard.Postpaid.GetHistoryActions.HistoryActionsResponse objHistoryActionsResponse = new Entity.Dashboard.Postpaid.GetHistoryActions.HistoryActionsResponse();

            if (objHistoryActionsRequest.Telephone == null)
            {
                // Si es HFC
                // Si es ASIS
                if (objHistoryActionsRequest.plataform.Equals(Claro.SIACU.Constants.ASIS))
                {
                    // Flujo Normal
                    objHistoryActionsResponse.ListHistoryActions = Claro.Web.Logging.ExecuteMethod<List<HistoryActions>>(objHistoryActionsRequest.Audit.Session, objHistoryActionsRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetHistoryActionsHFC(objHistoryActionsRequest.Audit.Session, objHistoryActionsRequest.Audit.Transaction, objHistoryActionsRequest.Contract, objHistoryActionsRequest.StartDate, objHistoryActionsRequest.EndDate);
                    });
                }
                else
                {
                    //Si es TOBE
                    if (objHistoryActionsRequest.flagConvivencia.Equals(Claro.Constants.NumberOneString))
                    {
                        // Linea Nueva - Flujo Tobe
                        objHistoryActionsRequest.flagConvivencia = Claro.Constants.NumberTwoString;
                    }
                    else if (objHistoryActionsRequest.flagConvivencia.Equals(Claro.Constants.NumberZeroString))
                    {
                        // Flujo Normal
                        objHistoryActionsRequest.flagConvivencia = Claro.Constants.NumberOneString;
                    }

                    objHistoryActionsResponse.ListHistoryActions = Claro.Web.Logging.ExecuteMethod<List<HistoryActions>>(objHistoryActionsRequest.Audit.Session, objHistoryActionsRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetHistoryActionsTobe(objHistoryActionsRequest.Audit, objHistoryActionsRequest.Audit.Transaction, objHistoryActionsRequest.TelephoneTOBE, objHistoryActionsRequest.StartDate, objHistoryActionsRequest.EndDate, objHistoryActionsRequest.flagConvivencia);
                    });
                }

                
            }
            else
            {
                // Si es ASIS
                if (objHistoryActionsRequest.plataform.Equals(Claro.SIACU.Constants.ASIS))
                {
                    // Flujo Normal
                    objHistoryActionsResponse.ListHistoryActions = Claro.Web.Logging.ExecuteMethod<List<HistoryActions>>(objHistoryActionsRequest.Audit.Session, objHistoryActionsRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetHistoryActions(objHistoryActionsRequest.Audit.Session, objHistoryActionsRequest.Audit.Transaction, objHistoryActionsRequest.Telephone, objHistoryActionsRequest.StartDate, objHistoryActionsRequest.EndDate);
                    });
                }
                else
                {
                    //Si es TOBE
                    if (objHistoryActionsRequest.flagConvivencia.Equals(Claro.Constants.NumberOneString))
                    {
                        // Linea Nueva - Flujo Tobe
                        objHistoryActionsRequest.flagConvivencia = Claro.Constants.NumberTwoString;
                    }
                    else if (objHistoryActionsRequest.flagConvivencia.Equals(Claro.Constants.NumberZeroString))
                    {
                        // Flujo Normal
                        objHistoryActionsRequest.flagConvivencia = Claro.Constants.NumberOneString;
                    }

                    objHistoryActionsResponse.ListHistoryActions = Claro.Web.Logging.ExecuteMethod<List<HistoryActions>>(objHistoryActionsRequest.Audit.Session, objHistoryActionsRequest.Audit.Transaction, () =>
                    {
                        return Data.Dashboard.Postpaid.GetHistoryActionsTobe(objHistoryActionsRequest.Audit, objHistoryActionsRequest.Audit.Transaction, objHistoryActionsRequest.Telephone, objHistoryActionsRequest.StartDate, objHistoryActionsRequest.EndDate, objHistoryActionsRequest.flagConvivencia);
                    });

                }
            }

            return objHistoryActionsResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de carga fija TIMPRO.
        /// </summary>
        /// <param name="objFixedChargeDetailTimProRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeDetailTimProResponse con información de detalle de cargo fijo TIMPRO.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro.FixedChargeDetailTimProResponse GetFixedChargeDetailTimPro(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro.FixedChargeDetailTimProRequest objFixedChargeDetailTimProRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro.FixedChargeDetailTimProResponse objFixedChargeDetailTimProResponse = new Entity.Dashboard.Postpaid.GetFixedChargeDetailTimPro.FixedChargeDetailTimProResponse()
            {
                ListFixedChargeDetailTimPro = Claro.Web.Logging.ExecuteMethod<List<FixedChargeDetailTimPro>>(objFixedChargeDetailTimProRequest.Audit.Session, objFixedChargeDetailTimProRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeDetailTimPro(objFixedChargeDetailTimProRequest.Audit.Session, objFixedChargeDetailTimProRequest.Audit.Transaction, objFixedChargeDetailTimProRequest.GroupBox, objFixedChargeDetailTimProRequest.InvoiceNumber); }),
            };

            return objFixedChargeDetailTimProResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de carga fija TIMPRO 1. 
        /// </summary>
        /// <param name="objFixedChargeDetailTimProOneRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeDetailTimProOneResponse con información de detalle de cargo fijo TIMPRO.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne.FixedChargeDetailTimProOneResponse GetFixedChargeDetailTimProOne(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne.FixedChargeDetailTimProOneRequest objFixedChargeDetailTimProOneRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne.FixedChargeDetailTimProOneResponse objFixedChargeDetailTimProOneResponse = new Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProOne.FixedChargeDetailTimProOneResponse()
            {
                ListFixedChargeDetailTimProOne = Claro.Web.Logging.ExecuteMethod<List<FixedChargeDetailTimProOne>>(objFixedChargeDetailTimProOneRequest.Audit.Session, objFixedChargeDetailTimProOneRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeDetailTimProOne(objFixedChargeDetailTimProOneRequest.Audit.Session, objFixedChargeDetailTimProOneRequest.Audit.Transaction, objFixedChargeDetailTimProOneRequest.GroupBox, objFixedChargeDetailTimProOneRequest.InvoiceNumber); }),
            };

            return objFixedChargeDetailTimProOneResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de carga fija TIMPRO 2.
        /// </summary>
        /// <param name="objGetFixedChargeDetailTimProTwoRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeDetailTimProTwoResponse con información de detalle de cargo fijo TIMPRO.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo.FixedChargeDetailTimProTwoResponse GetFixedChargeDetailTimProTwo(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo.FixedChargeDetailTimProTwoRequest objGetFixedChargeDetailTimProTwoRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo.FixedChargeDetailTimProTwoResponse objGetFixedChargeDetailTimProTwoResponse = new Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo.FixedChargeDetailTimProTwoResponse();
            objGetFixedChargeDetailTimProTwoResponse.ListFixedChargeDetailTimProTwo = Claro.Web.Logging.ExecuteMethod<List<FixedChargeDetailTimProTwo>>(objGetFixedChargeDetailTimProTwoRequest.Audit.Session, objGetFixedChargeDetailTimProTwoRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeDetailTimProTwo(objGetFixedChargeDetailTimProTwoRequest.Audit.Session, objGetFixedChargeDetailTimProTwoRequest.Audit.Transaction, objGetFixedChargeDetailTimProTwoRequest.GroupBox, objGetFixedChargeDetailTimProTwoRequest.InvoiceNumber); });

            return objGetFixedChargeDetailTimProTwoResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de carga fija TIMMAX.
        /// </summary>
        /// <param name="objFixedChargeDetailTimMaxRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeDetailTimMaxResponse con información de detalle de cargo fijo TIMMAX.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax.FixedChargeDetailTimMaxResponse GetFixedChargeDetailTimMax(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax.FixedChargeDetailTimMaxRequest objFixedChargeDetailTimMaxRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax.FixedChargeDetailTimMaxResponse objFixedChargeDetailTimMaxResponse = new Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMax.FixedChargeDetailTimMaxResponse()
            {
                ListFixedChargeDetailTimMax = Claro.Web.Logging.ExecuteMethod<List<FixedChargeDetailTimMax>>(objFixedChargeDetailTimMaxRequest.Audit.Session, objFixedChargeDetailTimMaxRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeDetailTimMax(objFixedChargeDetailTimMaxRequest.Audit.Session, objFixedChargeDetailTimMaxRequest.Audit.Transaction, objFixedChargeDetailTimMaxRequest.GroupBox, objFixedChargeDetailTimMaxRequest.InvoiceNumber); }),
            };

            return objFixedChargeDetailTimMaxResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de carga fija TIMMAX 2.
        /// </summary>
        /// <param name="objFixedChargeDetailTimMaxTwoRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeDetailTimMaxTwoResponse con información de detalle de cargo fijo TIMMAX 2.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo.FixedChargeDetailTimMaxTwoResponse GetFixedChargeDetailTimMaxTwo(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo.FixedChargeDetailTimMaxTwoRequest objFixedChargeDetailTimMaxTwoRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo.FixedChargeDetailTimMaxTwoResponse objFixedChargeDetailTimMaxTwoResponse = new Entity.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo.FixedChargeDetailTimMaxTwoResponse()
            {
                ListFixedChargeDetailTimMaxTwo = Claro.Web.Logging.ExecuteMethod<List<FixedChargeDetailTimMaxTwo>>(objFixedChargeDetailTimMaxTwoRequest.Audit.Session, objFixedChargeDetailTimMaxTwoRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeDetailTimMaxTwo(objFixedChargeDetailTimMaxTwoRequest.Audit.Session, objFixedChargeDetailTimMaxTwoRequest.Audit.Transaction, objFixedChargeDetailTimMaxTwoRequest.GroupBox, objFixedChargeDetailTimMaxTwoRequest.InvoiceNumber); }),
            };

            return objFixedChargeDetailTimMaxTwoResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de descuentos por cargo fijo.
        /// </summary>
        /// <param name="objDiscountFixedChargeDetailRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto DiscountFixedChargeDetailResponse con información de descuentos por cargo fijo.</returns>
        public static Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail.DiscountFixedChargeDetailResponse GetDiscountFixedChargeDetail(Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail.DiscountFixedChargeDetailRequest objDiscountFixedChargeDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail.DiscountFixedChargeDetailResponse objDiscountFixedChargeDetailResponse = new Entity.Dashboard.Postpaid.GetDiscountFixedChargeDetail.DiscountFixedChargeDetailResponse()
            {
                ListDiscountFixedChargeDetail = Claro.Web.Logging.ExecuteMethod<List<DiscountFixedChargeDetail>>(objDiscountFixedChargeDetailRequest.Audit.Session, objDiscountFixedChargeDetailRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDiscountFixedChargeDetail(objDiscountFixedChargeDetailRequest.Audit.Session, objDiscountFixedChargeDetailRequest.Audit.Transaction, objDiscountFixedChargeDetailRequest.GroupBox, objDiscountFixedChargeDetailRequest.InvoiceNumber); }),
            };

            return objDiscountFixedChargeDetailResponse;
        }

        /// <summary>
        /// Método para obtener el compromiso de pago.
        /// </summary>
        /// <param name="objPaymentCommitmentRequest">Id de cliente</param>
        /// <returns>Devuelve objeto PaymentCommitmentResponse con información de compromiso de pago.</returns>
        public static Entity.Dashboard.Postpaid.GetPaymentCommitment.PaymentCommitmentResponse GetPaymentCommitment(Entity.Dashboard.Postpaid.GetPaymentCommitment.PaymentCommitmentRequest objPaymentCommitmentRequest)
        {
            Entity.Dashboard.Postpaid.GetPaymentCommitment.PaymentCommitmentResponse objPaymentCommitmentResponse = new Entity.Dashboard.Postpaid.GetPaymentCommitment.PaymentCommitmentResponse()
            {
                ListAccount = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Account>>(objPaymentCommitmentRequest.Audit.Session, objPaymentCommitmentRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetPaymentCommitment(objPaymentCommitmentRequest.Audit.Session, objPaymentCommitmentRequest.Audit.Transaction, objPaymentCommitmentRequest.IdCustomer); }),
            };

            return objPaymentCommitmentResponse;
        }

        /// <summary>
        /// Método para obtener el detalle de descuentos por cargo fijo.
        /// </summary>
        /// <param name="objFixedChargeDetailTimProBagRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeDetailTimProBagResponse con información de detalle de descuentos por cargo fijo.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag.FixedChargeDetailTimProBagResponse GetFixedChargeDetailTimProBag(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag.FixedChargeDetailTimProBagRequest objFixedChargeDetailTimProBagRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag.FixedChargeDetailTimProBagResponse objFixedChargeDetailTimProBagResponse = new Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBag.FixedChargeDetailTimProBagResponse()
            {
                ListFixedChargeDetailTimProBag = Claro.Web.Logging.ExecuteMethod<List<FixedChargeDetailTimProBag>>(objFixedChargeDetailTimProBagRequest.Audit.Session, objFixedChargeDetailTimProBagRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeDetailTimProBag(objFixedChargeDetailTimProBagRequest.Audit.Session, objFixedChargeDetailTimProBagRequest.Audit.Transaction, objFixedChargeDetailTimProBagRequest.GroupBox, objFixedChargeDetailTimProBagRequest.InvoiceNumber); }),
            };

            return objFixedChargeDetailTimProBagResponse;
        }

        /// <summary>
        /// Método para obtener detalle de cargo fijo TIMPRO 1.
        /// </summary>
        /// <param name="objFixedChargeDetailTimProBagOneRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeDetailTimProBagOneResponse con información de detalle de cargo fijo TIMPRO 1.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne.FixedChargeDetailTimProBagOneResponse GetFixedChargeDetailTimProBagOne(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne.FixedChargeDetailTimProBagOneRequest objFixedChargeDetailTimProBagOneRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne.FixedChargeDetailTimProBagOneResponse objFixedChargeDetailTimProBagOneResponse = new Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne.FixedChargeDetailTimProBagOneResponse()
            {
                ListFixedChargeDetailTimProBagOne = Claro.Web.Logging.ExecuteMethod<List<FixedChargeDetailTimProBagOne>>(objFixedChargeDetailTimProBagOneRequest.Audit.Session, objFixedChargeDetailTimProBagOneRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeDetailTimProBagOne(objFixedChargeDetailTimProBagOneRequest.Audit.Session, objFixedChargeDetailTimProBagOneRequest.Audit.Transaction, objFixedChargeDetailTimProBagOneRequest.GroupBox, objFixedChargeDetailTimProBagOneRequest.InvoiceNumber); }),
            };

            return objFixedChargeDetailTimProBagOneResponse;
        }

        /// <summary>
        /// Método para obtener detalle de cargo fijo TIMPRO 2.
        /// </summary>
        /// <param name="objFixedChargeDetailTimProBagTwoRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeDetailTimProBagTwoResponse con información de detalle de cargo fijo TIMPRO 1.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo.FixedChargeDetailTimProBagTwoResponse GetFixedChargeDetailTimProBagTwo(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo.FixedChargeDetailTimProBagTwoRequest objFixedChargeDetailTimProBagTwoRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo.FixedChargeDetailTimProBagTwoResponse objFixedChargeDetailTimProBagTwoResponse = new Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo.FixedChargeDetailTimProBagTwoResponse()
            {
                ListFixedChargeDetailTimProBagTwo = Claro.Web.Logging.ExecuteMethod<List<FixedChargeDetailTimProBagTwo>>(objFixedChargeDetailTimProBagTwoRequest.Audit.Session, objFixedChargeDetailTimProBagTwoRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeDetailTimProBagTwo(objFixedChargeDetailTimProBagTwoRequest.Audit.Session, objFixedChargeDetailTimProBagTwoRequest.Audit.Transaction, objFixedChargeDetailTimProBagTwoRequest.GroupBox, objFixedChargeDetailTimProBagTwoRequest.InvoiceNumber); }),
            };

            return objFixedChargeDetailTimProBagTwoResponse;
        }

        /// <summary>
        /// Método para obtener detalle de cargo fijo TIMPRO 3.
        /// </summary>
        /// <param name="objFixedChargeDetailTimProBagThreeRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeDetailTimProBagThreeResponse con información de detalle de cargo fijo TIMPRO 1.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree.FixedChargeDetailTimProBagThreeResponse GetFixedChargeDetailTimProBagThree(Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree.FixedChargeDetailTimProBagThreeRequest objFixedChargeDetailTimProBagThreeRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree.FixedChargeDetailTimProBagThreeResponse objFixedChargeDetailTimProBagThreeResponse = new Entity.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree.FixedChargeDetailTimProBagThreeResponse()
            {
                ListFixedChargeDetailTimProBagThree = Claro.Web.Logging.ExecuteMethod<List<FixedChargeDetailTimProBagThree>>(objFixedChargeDetailTimProBagThreeRequest.Audit.Session, objFixedChargeDetailTimProBagThreeRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeDetailTimProBagThree(objFixedChargeDetailTimProBagThreeRequest.Audit.Session, objFixedChargeDetailTimProBagThreeRequest.Audit.Transaction, objFixedChargeDetailTimProBagThreeRequest.GroupBox, objFixedChargeDetailTimProBagThreeRequest.InvoiceNumber); }),
            };

            return objFixedChargeDetailTimProBagThreeResponse;
        }

        /// <summary>
        /// Método para obtener histórico de Delivery.
        /// </summary>
        /// <param name="objHistoricDeliveryRequest">Contiene cliente.</param>
        /// <returns>Devuelve objeto HistoricDeliveryResponse con información histórica de delivery.</returns>
        public static POSTPAID.GetHistoricDelivery.HistoricDeliveryResponse GetHistoricDelivery(POSTPAID.GetHistoricDelivery.HistoricDeliveryRequest objHistoricDeliveryRequest)
        {
            Int32 intPageSize = Claro.Constants.NumberZero;
            try
            {
                intPageSize = Convert.ToInt(KEY.AppSettings("strConsCantRegSDF"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHistoricDeliveryRequest.Audit.Session, objHistoricDeliveryRequest.Audit.Transaction, ex.Message);
            }

            POSTPAID.GetHistoricDelivery.HistoricDeliveryResponse objHistoricDeliveryResponse = new POSTPAID.GetHistoricDelivery.HistoricDeliveryResponse()
            {
                ListHistoricDelivery = QueryHistoricDelivery(objHistoricDeliveryRequest.Audit.Session, objHistoricDeliveryRequest.Audit.Transaction, intPageSize, objHistoricDeliveryRequest.strCustomer, objHistoricDeliveryRequest.flagPlataforma)
            };
            return objHistoricDeliveryResponse;
        }

        /// <summary>
        /// Método para consultar el histórico de delivery.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransactionID">Id de transacción</param>
        /// <param name="intPageSize">Tamaño de página</param>
        /// <param name="strCustomerId">Id de cliente</param>
        /// <returns>Devuelve listado de objetos HistoricDelivery con información histórica de delivery.</returns>
        public static List<Entity.Dashboard.Postpaid.HistoricDelivery> QueryHistoricDelivery(string strIdSession, string strTransactionID, Int32 intPageSize, string strCustomerId, string flagPlataforma)
        {
            string strInvoiceNumber;
            string strFechaEmision;
            List<Entity.Dashboard.Postpaid.HistoricDelivery> lstHistoricDelivery = null;
            StringBuilder strRecibos = new StringBuilder();
            int cantidadRegistros = Claro.Constants.NumberZero;
            int totalRegistros = Convert.ToInt(KEY.AppSettings("strConsCantRegSDF"));


            if (flagPlataforma.Equals(Constants.TOBE))
            {
                List<Entity.Dashboard.Postpaid.HistoricDelivery> listRecibosTobe = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.HistoricDelivery>>(strIdSession, strTransactionID, () => { return Data.Dashboard.Postpaid.GetHistoricDeliveryToBe(strIdSession, strTransactionID, strCustomerId, intPageSize); });

                if (listRecibosTobe != null && listRecibosTobe.Count > 0)
                {
                    foreach (var item in listRecibosTobe)
                    {
                        strInvoiceNumber = item._RECIBO;
                        strRecibos.AppendFormat("{0}{1}", strInvoiceNumber, ",");
                        cantidadRegistros++;
                    }
                }

                if (cantidadRegistros <= totalRegistros)
                {
                    List<Entity.Dashboard.Postpaid.ReceiptHistory> lstReceiptHistory = Data.Dashboard.Postpaid.GetTolsReceipt(strIdSession, strTransactionID, intPageSize, strCustomerId);

                    if (lstReceiptHistory != null && lstReceiptHistory.Count > 0)
                    {
                        for (int i = 0; i < lstReceiptHistory.Count; i++)
                        {
                            strInvoiceNumber = lstReceiptHistory[i].InvoiceNumber;
                            strFechaEmision = lstReceiptHistory[i].FechaEmision;

                            strRecibos.AppendFormat("{0}{1}", Helper.GetNumberReceipt(strIdSession, strTransactionID, strInvoiceNumber, strFechaEmision), ",");
                            cantidadRegistros++;

                            if (cantidadRegistros == totalRegistros) break;
                        }
                    }
                }

            }
            else
            {
                List<Entity.Dashboard.Postpaid.ReceiptHistory> lstReceiptHistory = Data.Dashboard.Postpaid.GetTolsReceipt(strIdSession, strTransactionID, intPageSize, strCustomerId);

                if (lstReceiptHistory != null && lstReceiptHistory.Count > 0)
                {
                    for (int i = 0; i < lstReceiptHistory.Count; i++)
                    {
                        strInvoiceNumber = lstReceiptHistory[i].InvoiceNumber;
                        strFechaEmision = lstReceiptHistory[i].FechaEmision;

                        strRecibos.AppendFormat("{0}{1}", Helper.GetNumberReceipt(strIdSession, strTransactionID, strInvoiceNumber, strFechaEmision), ",");
                    }
                }
            }

            if (!string.IsNullOrEmpty(strRecibos.ToString()))
            {
                POSTPAID.HistoricDelivery objHistoricDeliv = new POSTPAID.HistoricDelivery()
                {
                    _RECIBO = strRecibos.ToString().Substring(0, strRecibos.ToString().Length - 1),
                    _FLAG = KEY.AppSettings("strDistFactFlag05x1")
                };

                lstHistoricDelivery = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.HistoricDelivery>>(strIdSession, strTransactionID, () => { return Data.Dashboard.Postpaid.GetHistoricDelivery(strIdSession, strTransactionID, Convert.ToString(objHistoricDeliv._RECIBO), Convert.ToString(objHistoricDeliv._FLAG)); });

            }

            return lstHistoricDelivery;
        }

        /// <summary>
        /// Método para obtener información de cargo fijo TIMPRO.
        /// </summary>
        /// <param name="objFixedChargeTimProBagDetailRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeTimProBagDetailResponse con información de cargo fijo TIMPRO.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail.FixedChargeTimProBagDetailResponse GetFixedChargeTimProBagDetail(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail.FixedChargeTimProBagDetailRequest objFixedChargeTimProBagDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail.FixedChargeTimProBagDetailResponse objFixedChargeTimProBagDetailResponse = new Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetail.FixedChargeTimProBagDetailResponse()
            {
                ListFixedChargeTimProBagDetail = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.FixedChargeTimProBagDetail>>(objFixedChargeTimProBagDetailRequest.Audit.Session, objFixedChargeTimProBagDetailRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeTimProBagDetail(objFixedChargeTimProBagDetailRequest.Audit.Session, objFixedChargeTimProBagDetailRequest.Audit.Transaction, objFixedChargeTimProBagDetailRequest.GroupBox, objFixedChargeTimProBagDetailRequest.InvoiceNumber); })
            };
            return objFixedChargeTimProBagDetailResponse;
        }

        /// <summary>
        /// Método para obtener información de cargo fijo TIMPRO 1.
        /// </summary>
        /// <param name="objFixedChargeTimProBagDetailOneRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeTimProBagDetailOneResponse con información de cargo fijo TIMPRO 1.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne.FixedChargeTimProBagDetailOneResponse GetFixedChargeTimProBagDetailOne(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne.FixedChargeTimProBagDetailOneRequest objFixedChargeTimProBagDetailOneRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne.FixedChargeTimProBagDetailOneResponse objFixedChargeTimProBagDetailOneResponse = new Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne.FixedChargeTimProBagDetailOneResponse()
            {
                ListFixedChargeTimProBagDetailOne = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.FixedChargeTimProBagDetailOne>>(objFixedChargeTimProBagDetailOneRequest.Audit.Session, objFixedChargeTimProBagDetailOneRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeTimProBagDetailOne(objFixedChargeTimProBagDetailOneRequest.Audit.Session, objFixedChargeTimProBagDetailOneRequest.Audit.Transaction, objFixedChargeTimProBagDetailOneRequest.GroupBox, objFixedChargeTimProBagDetailOneRequest.InvoiceNumber); })
            };
            return objFixedChargeTimProBagDetailOneResponse;
        }

        /// <summary>
        /// Método para obtener información de cargo fijo TIMPRO 2
        /// </summary>
        /// <param name="objFixedChargeTimProBagDetailTwoRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeTimProBagDetailTwoResponse con información de cargo fijo TIMPRO 2.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo.FixedChargeTimProBagDetailTwoResponse GetFixedChargeTimProBagDetailTwo(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo.FixedChargeTimProBagDetailTwoRequest objFixedChargeTimProBagDetailTwoRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo.FixedChargeTimProBagDetailTwoResponse objFixedChargeTimProBagDetailTwoResponse = new Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo.FixedChargeTimProBagDetailTwoResponse()
            {
                ListFixedChargeTimProBagDetailTwo = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.FixedChargeTimProBagDetailTwo>>(objFixedChargeTimProBagDetailTwoRequest.Audit.Session, objFixedChargeTimProBagDetailTwoRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeTimProBagDetailTwo(objFixedChargeTimProBagDetailTwoRequest.Audit.Session, objFixedChargeTimProBagDetailTwoRequest.Audit.Transaction, objFixedChargeTimProBagDetailTwoRequest.GroupBox, objFixedChargeTimProBagDetailTwoRequest.InvoiceNumber); })
            };
            return objFixedChargeTimProBagDetailTwoResponse;
        }

        /// <summary>
        /// Método para obtener información de cargo fijo TIMPRO 3.
        /// </summary>
        /// <param name="objFixedChargeTimProBagDetailThreeRequest">Contiene grupo y número de factura.</param>
        /// <returns>Devuelve objeto FixedChargeTimProBagDetailThreeResponse con información de cargo fijo TIMPRO 3.</returns>
        public static Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree.FixedChargeTimProBagDetailThreeResponse GetFixedChargeTimProBagDetailThree(Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree.FixedChargeTimProBagDetailThreeRequest objFixedChargeTimProBagDetailThreeRequest)
        {
            Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree.FixedChargeTimProBagDetailThreeResponse objFixedChargeTimProBagDetailThreeResponse = new Entity.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree.FixedChargeTimProBagDetailThreeResponse()
            {
                ListFixedChargeTimProBagDetailThree = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.FixedChargeTimProBagDetailThree>>(objFixedChargeTimProBagDetailThreeRequest.Audit.Session, objFixedChargeTimProBagDetailThreeRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFixedChargeTimProBagDetailThree(objFixedChargeTimProBagDetailThreeRequest.Audit.Session, objFixedChargeTimProBagDetailThreeRequest.Audit.Transaction, objFixedChargeTimProBagDetailThreeRequest.GroupBox, objFixedChargeTimProBagDetailThreeRequest.InvoiceNumber); })
            };
            return objFixedChargeTimProBagDetailThreeResponse;
        }

        /// <summary>
        /// Método para obtener tarifa de equipo.
        /// </summary>
        /// <param name="objFeeEquipmentRequest">Contiene id de cliente, y usuario de aplicación.</param>
        /// <returns>Obtiene listado de objetos FeeEquipmetResponse con información de tarifa de equipo.</returns>
        public static Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentResponse FeeEquipment(Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentRequest objFeeEquipmentRequest)
        {
            string strErrorCode = "", CodeApplication = "", strOrigin = "", strType = "";
            int IntCodApplication = Claro.Constants.NumberZero;
            try
            {
                CodeApplication = KEY.AppSettings("CodAplicacion");
                IntCodApplication = int.Parse(CodeApplication);
                strOrigin = KEY.AppSettings("gConstOrigenCuotaEquipo");
                strType = KEY.AppSettings("gConstTipoDocBaseDatos");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFeeEquipmentRequest.Audit.Session, objFeeEquipmentRequest.Audit.Transaction, ex.Message);
            }

            List<POSTPAID.StatusAccountAOC> LstStatusAccountAOC = null;
            List<POSTPAID.FeeEquipment> listFeeEquipmentccount = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.FeeEquipment>>(objFeeEquipmentRequest.Audit.Session, objFeeEquipmentRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetFeeEquipment(objFeeEquipmentRequest.Audit.Session, objFeeEquipmentRequest.Audit.Transaction, IntCodApplication, objFeeEquipmentRequest.Audit.UserName, strOrigin, objFeeEquipmentRequest.CustomerId); });


            List<POSTPAID.StatusAccountAOC> listStatusAccountAOC = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.StatusAccountAOC>>(objFeeEquipmentRequest.Audit.Session, objFeeEquipmentRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetStatusAccountAOC(objFeeEquipmentRequest.Audit.Session, objFeeEquipmentRequest.Audit.Transaction, objFeeEquipmentRequest.Audit.UserName, objFeeEquipmentRequest.CustomerId, ref strErrorCode); });
            if (strErrorCode == Claro.Constants.NumberZeroString && listStatusAccountAOC != null && listStatusAccountAOC.Count > Int32.Parse(Claro.Constants.NumberOneString))
            {
                LstStatusAccountAOC = new List<POSTPAID.StatusAccountAOC>();
                foreach (var item in listStatusAccountAOC)
                {
                    if (item.Tipo.Equals(strType))
                    {
                        LstStatusAccountAOC.Add(item);
                    }
                }
            }
            Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentResponse objFeeEquipmetResponse = PaymentsFilter(objFeeEquipmentRequest.Audit.Session, objFeeEquipmentRequest.Audit.Transaction, listFeeEquipmentccount, LstStatusAccountAOC);
            return objFeeEquipmetResponse;
        }

        /// <summary>
        /// Método para obtener filtro de pagos.
        /// </summary>
        /// <param name="strSession">Sesión</param>
        /// <param name="strTransaction">Transacción</param>
        /// <param name="listGeneral">Lista general</param>
        /// <param name="listStatusAccountAOC">Lista de estado de cuenta</param>
        /// <returns>Devuelve objeto FeeEquipmentResponse con información de filtro de pagos.</returns>
        private static Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentResponse PaymentsFilter(string strSession, string strTransaction, List<Entity.Dashboard.Postpaid.FeeEquipment> listGeneral, List<Entity.Dashboard.Postpaid.StatusAccountAOC> listStatusAccountAOC)
        {
            string strSumImporteOriginal = "", strsumImportePendiente = "";
            double DblSumImporteOriginal = Claro.Constants.NumberZero;
            double DblsumImportePendiente = Claro.Constants.NumberZero;
            string strStateFeeZero = "";
            string strHigherStateFeeZero = "";
            Int32 strNumDias = Claro.Constants.NumberZero;
            List<POSTPAID.FeeEquipment> listFeeEquipment = null;

            try
            {
                strNumDias = int.Parse(KEY.AppSettings("gConstDiasFechaCuota"));
                strStateFeeZero = KEY.AppSettings("gConstEstadoCuotaCero");
                strHigherStateFeeZero = KEY.AppSettings("gConstEstadoCuotaMayorCero");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strSession, strTransaction, ex.Message);
            }

            if (listGeneral != null && listGeneral.Count > int.Parse(Claro.Constants.NumberZeroString))
            {
                listFeeEquipment = new List<POSTPAID.FeeEquipment>();
                foreach (var itemCuota in listGeneral)
                {
                    foreach (var ItemEstado in listStatusAccountAOC)
                    {
                        if (itemCuota.numero_documento == ItemEstado.Documento)
                        {
                            itemCuota.numero_sisact = ItemEstado.DescripcionPago;
                            break;
                        }
                    }
                    if (String.IsNullOrEmpty(itemCuota.saldo_pendiente_cuota))
                    {
                        itemCuota.saldo_pendiente_cuota = Claro.Constants.NumberZeroString;

                    }
                    double NumberZeroDouble = 0;
                    itemCuota.estado_cuota = (Convert.ToDouble(itemCuota.saldo_pendiente_cuota) > NumberZeroDouble) ? strStateFeeZero : strHigherStateFeeZero;

                    if (String.IsNullOrEmpty(itemCuota.fecha_emision_cuota))
                    {
                        itemCuota.fecha_emision_cuota = "";
                    }
                    else
                    {
                        itemCuota.fecha_emision_cuota = itemCuota.fecha_emision_cuota.Substring(0, strNumDias);

                    }
                    if (String.IsNullOrEmpty(itemCuota.fecha_vencimiento_cuota))
                    {
                        itemCuota.fecha_vencimiento_cuota = "";
                    }
                    else
                    {
                        itemCuota.fecha_vencimiento_cuota = itemCuota.fecha_vencimiento_cuota.Substring(0, strNumDias);
                    }
                    if (String.IsNullOrEmpty(itemCuota.importe_original_cuota))
                    {
                        itemCuota.importe_original_cuota = Claro.Constants.NumberZeroString;
                    }
                    if (String.IsNullOrEmpty(itemCuota.saldo_pendiente_cuota))
                    {
                        itemCuota.saldo_pendiente_cuota = Claro.Constants.NumberZeroString;
                    }
                    DblSumImporteOriginal = Convert.ToDouble(DblSumImporteOriginal + Convert.ToDouble(itemCuota.importe_original_cuota));

                    DblsumImportePendiente = Convert.ToDouble(DblsumImportePendiente + Convert.ToDouble(itemCuota.saldo_pendiente_cuota));
                    strSumImporteOriginal = DblSumImporteOriginal.ToString("##,##0.00");
                    strsumImportePendiente = DblsumImportePendiente.ToString("##,##0.00");
                    listFeeEquipment.Add(itemCuota);
                }
            }
            Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentResponse ObjFeeEquipmetResponse = new Entity.Dashboard.Postpaid.GetFeeEquipment.FeeEquipmentResponse() { ListFeeEquipmentccount = listFeeEquipment, strSumImporteOriginal = strSumImporteOriginal, strsumImportePendiente = strsumImportePendiente };
            return ObjFeeEquipmetResponse;
        }

        /// <summary>
        /// Método para obtener detalle de tráfico local de llamadas TP.
        /// </summary>
        /// <param name="objLocalTrafficDetailCallTPRequest">Contiene número de factura, tipo de llamada y msisdn.</param>
        /// <returns>Devuelve objeto LocalTrafficDetailCallTPResponse con información de detalle de tráfico local de llamadas TP.</returns>
        public static Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP.LocalTrafficDetailCallTPResponse GetLocalTrafficDetailCallTP(Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP.LocalTrafficDetailCallTPRequest objLocalTrafficDetailCallTPRequest)
        {
            string strTypeCallTP = "";
            try
            {
                if (objLocalTrafficDetailCallTPRequest.TypeCallTP == KEY.AppSettings("strTypeCallRTP"))
                {
                    strTypeCallTP = Claro.Constants.NumberElevenString;
                }
                else if (objLocalTrafficDetailCallTPRequest.TypeCallTP == KEY.AppSettings("strTypeCallOnNetTP"))
                {
                    strTypeCallTP = Claro.Constants.NumberTwelveString;
                }
                else if (objLocalTrafficDetailCallTPRequest.TypeCallTP == KEY.AppSettings("strTypeCallOffNetFijo"))
                {
                    strTypeCallTP = Claro.Constants.NumberThirteenString;
                }
                else if (objLocalTrafficDetailCallTPRequest.TypeCallTP == KEY.AppSettings("strTypeCallOffNetCelular"))
                {
                    strTypeCallTP = Claro.Constants.NumberFourteenString;
                }
                else
                {
                    strTypeCallTP = Claro.Constants.NumberElevenToFourteenString;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objLocalTrafficDetailCallTPRequest.Audit.Session, objLocalTrafficDetailCallTPRequest.Audit.Transaction, ex.Message);
            }

            Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP.LocalTrafficDetailCallTPResponse objLocalTrafficDetailCallTPResponse = new Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTP.LocalTrafficDetailCallTPResponse()
            {
                ListLocalTrafficDetailCallTP = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.LocalTrafficDetailCallTP>>(objLocalTrafficDetailCallTPRequest.Audit.Session, objLocalTrafficDetailCallTPRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailLocalTrafficCallTP(objLocalTrafficDetailCallTPRequest.Audit.Session, objLocalTrafficDetailCallTPRequest.Audit.Transaction, objLocalTrafficDetailCallTPRequest.InvoiceNumber, objLocalTrafficDetailCallTPRequest.Msisdn, strTypeCallTP); }),
                LocalTrafficQuantityCallTP = Claro.Web.Logging.ExecuteMethod<string>(objLocalTrafficDetailCallTPRequest.Audit.Session, objLocalTrafficDetailCallTPRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetQuantityLocalTrafficCallTP(objLocalTrafficDetailCallTPRequest.Audit.Session, objLocalTrafficDetailCallTPRequest.Audit.Transaction, objLocalTrafficDetailCallTPRequest.InvoiceNumber, objLocalTrafficDetailCallTPRequest.Msisdn, strTypeCallTP); })
            };
            return objLocalTrafficDetailCallTPResponse;
        }

        /// <summary>
        /// Método para obtener detalle de tráfico local de llamadas TM.
        /// </summary>
        /// <param name="objLocalTrafficDetailCallTMRequest">Contiene número de factura, tipo de llamada y msisdn.</param>
        /// <returns>Devuelve objeto LocalTrafficDetailCallTMResponse con información de detalle de tráfico local de llamadas TM.</returns>
        public static Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM.LocalTrafficDetailCallTMResponse GetLocalTrafficDetailCallTM(Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM.LocalTrafficDetailCallTMRequest objLocalTrafficDetailCallTMRequest)
        {
            string strTypeCallTM = "";
            string strTariffTime = "";

            try
            {
                if (objLocalTrafficDetailCallTMRequest.TypeCallTM == KEY.AppSettings("strTypeCallOnNetTM"))
                {
                    strTypeCallTM = Claro.Constants.NumberElevenToTwelveString;
                    strTariffTime = Claro.SIACU.Constants.Nor01;
                }
                else if (objLocalTrafficDetailCallTMRequest.TypeCallTM == KEY.AppSettings("strTypeCallOffNet"))
                {

                    strTypeCallTM = Claro.Constants.NumberThirteenToFourteenString;
                    strTariffTime = Claro.SIACU.Constants.Nor01;
                }
                else if (objLocalTrafficDetailCallTMRequest.TypeCallTM == KEY.AppSettings("strTypeCallOnNetOffNet"))
                {

                    strTypeCallTM = Claro.Constants.NumberElevenToFourteenString;
                    strTariffTime = Claro.SIACU.Constants.Red01;

                }
                else
                {
                    strTypeCallTM = Claro.Constants.NumberElevenToFourteenString;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objLocalTrafficDetailCallTMRequest.Audit.Session, objLocalTrafficDetailCallTMRequest.Audit.Transaction, ex.Message);
            }

            Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM.LocalTrafficDetailCallTMResponse objLocalTrafficDetailCallTMResponse = new Entity.Dashboard.Postpaid.GetLocalTrafficDetailCallTM.LocalTrafficDetailCallTMResponse()
            {
                ListLocalTrafficDetailCallTM = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.LocalTrafficDetailCallTM>>(objLocalTrafficDetailCallTMRequest.Audit.Session, objLocalTrafficDetailCallTMRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailLocalTrafficCallTM(objLocalTrafficDetailCallTMRequest.Audit.Session, objLocalTrafficDetailCallTMRequest.Audit.Transaction, objLocalTrafficDetailCallTMRequest.InvoiceNumber, objLocalTrafficDetailCallTMRequest.Msisdn, strTypeCallTM, strTariffTime); }),
                LocalTrafficQuantityCallTM = Claro.Web.Logging.ExecuteMethod<string>(objLocalTrafficDetailCallTMRequest.Audit.Session, objLocalTrafficDetailCallTMRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetQuantityLocalTrafficCallTM(objLocalTrafficDetailCallTMRequest.Audit.Session, objLocalTrafficDetailCallTMRequest.Audit.Transaction, objLocalTrafficDetailCallTMRequest.InvoiceNumber, objLocalTrafficDetailCallTMRequest.Msisdn, strTypeCallTM, strTariffTime); })
            };

            return objLocalTrafficDetailCallTMResponse;
        }

        /// <summary>
        /// Método para obtener detalle de llamadas TP de tráfico local o consumo.
        /// </summary>
        /// <param name="objConsumeLocalTrafficDetailCallTPRequest">Contiene número de factura, tipo de llamada y msisdn.</param>
        /// <returns>Devuelve objeto ConsumeLocalTrafficDetailCallTPResponse con información de detalle de llamadas TP de tráfico local o consumo.</returns>
        public static Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP.ConsumeLocalTrafficDetailCallTPResponse GetConsumeLocalTrafficDetailCallTP(Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP.ConsumeLocalTrafficDetailCallTPRequest objConsumeLocalTrafficDetailCallTPRequest)
        {
            string strTypeCallTP = "";

            try
            {

                if (objConsumeLocalTrafficDetailCallTPRequest.TypeCallTP == KEY.AppSettings("strTypeCallRTP"))
                {
                    strTypeCallTP = Claro.Constants.NumberElevenString;
                }
                else if (objConsumeLocalTrafficDetailCallTPRequest.TypeCallTP == KEY.AppSettings("strTypeCallOnNetTP"))
                {
                    strTypeCallTP = Claro.Constants.NumberTwelveString;
                }
                else if (objConsumeLocalTrafficDetailCallTPRequest.TypeCallTP == KEY.AppSettings("strTypeCallOffNetFijo"))
                {
                    strTypeCallTP = Claro.Constants.NumberThirteenString;
                }
                else if (objConsumeLocalTrafficDetailCallTPRequest.TypeCallTP == KEY.AppSettings("strTypeCallOffNetCelular"))
                {
                    strTypeCallTP = Claro.Constants.NumberFourteenString;
                }
                else
                {
                    strTypeCallTP = Claro.Constants.NumberElevenToFourteenString;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsumeLocalTrafficDetailCallTPRequest.Audit.Session, objConsumeLocalTrafficDetailCallTPRequest.Audit.Transaction, ex.Message);
            }

            Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP.ConsumeLocalTrafficDetailCallTPResponse objConsumeLocalTrafficDetailCallTPResponse = new Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTP.ConsumeLocalTrafficDetailCallTPResponse()
            {
                ListConsumeLocalTrafficDetailCallTP = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ConsumeLocalTrafficDetailCallTP>>(objConsumeLocalTrafficDetailCallTPRequest.Audit.Session, objConsumeLocalTrafficDetailCallTPRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailConsumeLocalTrafficCallTP(objConsumeLocalTrafficDetailCallTPRequest.Audit.Session, objConsumeLocalTrafficDetailCallTPRequest.Audit.Transaction, objConsumeLocalTrafficDetailCallTPRequest.InvoiceNumber, objConsumeLocalTrafficDetailCallTPRequest.Msisdn, strTypeCallTP); }),
                ConsumeLocalTrafficQuantityCallTP = Claro.Web.Logging.ExecuteMethod<string>(objConsumeLocalTrafficDetailCallTPRequest.Audit.Session, objConsumeLocalTrafficDetailCallTPRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetConsumeQuantityLocalTrafficCallTP(objConsumeLocalTrafficDetailCallTPRequest.Audit.Session, objConsumeLocalTrafficDetailCallTPRequest.Audit.Transaction, objConsumeLocalTrafficDetailCallTPRequest.InvoiceNumber, objConsumeLocalTrafficDetailCallTPRequest.Msisdn, strTypeCallTP); })
            };
            return objConsumeLocalTrafficDetailCallTPResponse;
        }

        /// <summary>
        /// Método para obtener detalle de llamadas TM de tráfico local o consumo.
        /// </summary>
        /// <param name="objConsumeLocalTrafficDetailCallTMRequest">Contiene número de factura, tipo de llamada y msisdn.</param>
        /// <returns>Devuelve objeto ConsumeLocalTrafficDetailCallTMResponse con información de detalle de llamadas TM de tráfico local o consumo.</returns>
        public static Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM.ConsumeLocalTrafficDetailCallTMResponse GetConsumeLocalTrafficDetailCallTM(Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM.ConsumeLocalTrafficDetailCallTMRequest objConsumeLocalTrafficDetailCallTMRequest)
        {
            string strTypeCallTM = "";


            try
            {
                if (objConsumeLocalTrafficDetailCallTMRequest.TypeCallTM == KEY.AppSettings("strTypeCallOnNetTM"))
                {
                    strTypeCallTM = Claro.Constants.NumberElevenToTwelveString;

                }
                else if (objConsumeLocalTrafficDetailCallTMRequest.TypeCallTM == KEY.AppSettings("strTypeCallOffNet"))
                {
                    strTypeCallTM = Claro.Constants.NumberThirteenString;

                }
                else if (objConsumeLocalTrafficDetailCallTMRequest.TypeCallTM == KEY.AppSettings("strTypeCallOnNetOffNet"))
                {
                    strTypeCallTM = Claro.Constants.NumberFourteenString;
                }
                else
                {
                    strTypeCallTM = Claro.Constants.NumberElevenToFourteenString;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsumeLocalTrafficDetailCallTMRequest.Audit.Session, objConsumeLocalTrafficDetailCallTMRequest.Audit.Transaction, ex.Message);
            }

            Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM.ConsumeLocalTrafficDetailCallTMResponse objConsumeLocalTrafficDetailCallTMResponse = new Entity.Dashboard.Postpaid.GetConsumeLocalTrafficDetailCallTM.ConsumeLocalTrafficDetailCallTMResponse()
            {
                ListConsumeLocalTrafficDetailCallTM = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.ConsumeLocalTrafficDetailCallTM>>(objConsumeLocalTrafficDetailCallTMRequest.Audit.Session, objConsumeLocalTrafficDetailCallTMRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDetailConsumeLocalTrafficCallTM(objConsumeLocalTrafficDetailCallTMRequest.Audit.Session, objConsumeLocalTrafficDetailCallTMRequest.Audit.Session, objConsumeLocalTrafficDetailCallTMRequest.InvoiceNumber, objConsumeLocalTrafficDetailCallTMRequest.Msisdn, strTypeCallTM); }),
                ConsumeLocalTrafficQuantityCallTM = Claro.Web.Logging.ExecuteMethod<string>(objConsumeLocalTrafficDetailCallTMRequest.Audit.Session, objConsumeLocalTrafficDetailCallTMRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetConsumeQuantityLocalTrafficCallTM(objConsumeLocalTrafficDetailCallTMRequest.Audit.Session, objConsumeLocalTrafficDetailCallTMRequest.Audit.Session, objConsumeLocalTrafficDetailCallTMRequest.InvoiceNumber, objConsumeLocalTrafficDetailCallTMRequest.Msisdn, strTypeCallTM); })
            };
            return objConsumeLocalTrafficDetailCallTMResponse;
        }

        /// <summary>
        /// Método para obtener historial HR.
        /// </summary>
        /// <param name="objHistoryHRRequest">Contiene id de cliente.</param>
        /// <returns>Devuelve listado de objetos HistoryHRResponse con información de historial HR.</returns>
        public static Entity.Dashboard.Postpaid.GetHistoryHR.HistoryHRResponse GetHistoryHR(Entity.Dashboard.Postpaid.GetHistoryHR.HistoryHRRequest objHistoryHRRequest)
        {
            Int32 intPageSize = Claro.Constants.NumberZero;
            try
            {
                intPageSize = Convert.ToInt(KEY.AppSettings("strConsCantRegSDF"));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHistoryHRRequest.Audit.Session, objHistoryHRRequest.Audit.Transaction, ex.Message);
            }
            Entity.Dashboard.Postpaid.GetHistoryHR.HistoryHRResponse objHistoryHRResponse = new Entity.Dashboard.Postpaid.GetHistoryHR.HistoryHRResponse()
            {
                ListHistoryHR = Claro.Web.Logging.ExecuteMethod<List<ReceiptHistory>>(objHistoryHRRequest.Audit.Session, objHistoryHRRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTolsReceipt(objHistoryHRRequest.Audit.Session, objHistoryHRRequest.Audit.Session, intPageSize, objHistoryHRRequest.CustomerID); })
            };
            return objHistoryHRResponse;
        }

        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse GetOnlyDataCustomer(POSTPAID.GetCustomer.CustomerRequest objCustomerRequest)
        {

            POSTPAID.GetCustomer.CustomerResponse objCustomerResponse = new POSTPAID.GetCustomer.CustomerResponse();

            String strIdTransaction = objCustomerRequest.Audit.Transaction;
            String strIdSession = objCustomerRequest.Audit.Session;
            try
            {
                if (objCustomerRequest.Application != null && (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberOneString) || objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberTwoString) || objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberFourString) ||
                    objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberEightString) || objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberNineString)))
                {
                    objCustomerResponse.ObjCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Customer>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataCustomer(strIdSession, strIdTransaction, objCustomerRequest.AccountCustomer, objCustomerRequest.NumCellphone); });
                }
                else if (objCustomerRequest.Application != null && (objCustomerRequest.TypeSearch.Equals(Claro.Constants.NumberThreeString)))
                {
                    objCustomerResponse.ObjCustomer = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Customer>(strIdSession, strIdTransaction, () => { return Data.Dashboard.Postpaid.GetDataCustomerByContractCodeHFC(strIdSession, strIdTransaction, objCustomerRequest.IpApplication, objCustomerRequest.ApplicationName, objCustomerRequest.AccountCustomer, objCustomerRequest.NumCellphone); });

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaction, ex.Message);
            }

            return objCustomerResponse;
        }

        public static Claro.SIACU.Entity.Dashboard.Postpaid.GetService.ServiceResponse GetDataLine(POSTPAID.GetService.ServiceRequest objServiceRequest)
        {
            POSTPAID.GetService.ServiceResponse objServiceResponse = new POSTPAID.GetService.ServiceResponse();
            string strTFI = "";
            try
            {

                if (objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
                {
                    objServiceResponse.ObjService = Data.Dashboard.Postpaid.GetDataLineTobe(objServiceRequest.Audit, string.IsNullOrEmpty(objServiceRequest.ContractID) ? Claro.Constants.NumberZero : int.Parse(objServiceRequest.ContractID), objServiceRequest.coIdPub);

                }
                else
                {
                    objServiceResponse.ObjService = Data.Dashboard.Postpaid.GetDataLine(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, int.Parse(objServiceRequest.ContractID));
                }
                if (objServiceResponse.ObjService != null && objServiceRequest.plataformaAT.Equals(Claro.SIACU.Constants.ASIS, StringComparison.InvariantCultureIgnoreCase))
                {

                    strTFI = GetTPI(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, objServiceResponse.ObjService.COD_PLAN_TARIFARIO);
                    objServiceResponse.ObjService.TFI = strTFI;
                }
            }
            catch (Exception ex)
            {
                objServiceResponse.ObjService.TFI = "";
                Claro.Web.Logging.Error(objServiceRequest.Audit.Session, objServiceRequest.Audit.Transaction, ex.Message);
            }
            return objServiceResponse;
        }

        public static Entity.Dashboard.Postpaid.GetCustomerInformation.CustomerInformationResponse GetCustomerInformation(Entity.Dashboard.Postpaid.GetCustomerInformation.CustomerInformationRequest objCustomerInformationRequest)
        {
            Entity.Dashboard.Postpaid.GetCustomerInformation.CustomerInformationResponse objCustomerInformationResponse = new Entity.Dashboard.Postpaid.GetCustomerInformation.CustomerInformationResponse()
            {
                QueryOAC = Claro.Web.Logging.ExecuteMethod<QueryOAC>(objCustomerInformationRequest.Audit.Session, objCustomerInformationRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetCustomerInformation(objCustomerInformationRequest.Audit.Session, objCustomerInformationRequest.Audit.Transaction, objCustomerInformationRequest.IdCaso); })
            };

            return objCustomerInformationResponse;
        }

        public static Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseResponse GetStockWarehouse(Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseRequest objStockWarehouseRequest)
        {
            Claro.SIACU.Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseResponse objStockWarehouseResponse = new Claro.SIACU.Entity.Dashboard.Postpaid.GetStockWarehouse.StockWarehouseResponse();
            try
            {
                objStockWarehouseResponse.listStockWarehouse = Data.Dashboard.Postpaid.GetStockWarehouse(objStockWarehouseRequest.strSerie, objStockWarehouseRequest.strDescripcion, objStockWarehouseRequest.strRegion);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objStockWarehouseRequest.Audit.Session, objStockWarehouseRequest.Audit.Transaction, ex.Message);
            }
            return objStockWarehouseResponse;
        }

        public static Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailResponse GetTrackingDetail(Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailRequest objTrackingDetailRequest)
        {
            Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailResponse objTrackingDetailResponse = new Entity.Dashboard.Postpaid.GetTrackingDetail.TrackingDetailResponse()
            {
                QueryOACs = Claro.Web.Logging.ExecuteMethod<List<QueryOAC>>(objTrackingDetailRequest.Audit.Session, objTrackingDetailRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTrackingDetail(objTrackingDetailRequest.Audit.Session, objTrackingDetailRequest.Audit.Transaction, objTrackingDetailRequest.IdCaso); })
            };

            return objTrackingDetailResponse;
        }

        public static Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresResponse GetManagementOfClosures(Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresRequest objManagementOfClosuresRequest)
        {
            Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresResponse objManagementOfClosuresResponse = new Entity.Dashboard.Postpaid.GetManagementOfClosures.ManagementOfClosuresResponse()
            {
                Reclosures = Claro.Web.Logging.ExecuteMethod<List<Reclosures>>(objManagementOfClosuresRequest.Audit.Session, objManagementOfClosuresRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetManagementOfClosures(objManagementOfClosuresRequest.Audit.Session, objManagementOfClosuresRequest.Audit.Transaction, objManagementOfClosuresRequest.IdCaso); })
            };

            return objManagementOfClosuresResponse;
        }

        public static Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseResponse GetReopenOfTheCase(Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseRequest objReopenOfTheCaseRequest)
        {
            Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseResponse objReopenOfTheCaseResponse = new Entity.Dashboard.Postpaid.GetReopenOfTheCase.ReopenOfTheCaseResponse()
            {
                Reclosures = Claro.Web.Logging.ExecuteMethod<List<Reclosures>>(objReopenOfTheCaseRequest.Audit.Session, objReopenOfTheCaseRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetReopenOfTheCase(objReopenOfTheCaseRequest.Audit.Session, objReopenOfTheCaseRequest.Audit.Transaction, objReopenOfTheCaseRequest.IdCaso); })
            };

            return objReopenOfTheCaseResponse;
        }

        public static Entity.Dashboard.Postpaid.GetMaterials.MaterialsResponse GetMaterials(Entity.Dashboard.Postpaid.GetMaterials.MaterialsRequest objMaterialsRequest)
        {
            Entity.Dashboard.Postpaid.GetMaterials.MaterialsResponse objMaterialsResponse = new POSTPAID.GetMaterials.MaterialsResponse();
            try
            {
                objMaterialsResponse.ListMateriales = Data.Dashboard.Postpaid.GetMaterials(objMaterialsRequest.Audit.Session, objMaterialsRequest.Audit.Transaction);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMaterialsRequest.Audit.Session, objMaterialsRequest.Audit.Transaction, ex.Message);
            }

            return objMaterialsResponse;
        }

        public static Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingResponse GetSubTableTracking(Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingRequest objSubTableTrackingRequest)
        {
            Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingResponse objSubTableTrackingResponse = new Entity.Dashboard.Postpaid.GetSubTableTracking.SubTableTrackingResponse()
            {
                QueryOACs = Claro.Web.Logging.ExecuteMethod<List<QueryOAC>>(objSubTableTrackingRequest.Audit.Session, objSubTableTrackingRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetSubTableTracking(objSubTableTrackingRequest.Audit.Session, objSubTableTrackingRequest.Audit.Transaction, objSubTableTrackingRequest.IdCaso); })
            };

            return objSubTableTrackingResponse;
        }

        public static Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingResponse GetThirdTableTracking(Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingRequest objThirdTableTrackingRequest)
        {
            Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingResponse objThirdTableTrackingResponse = new Entity.Dashboard.Postpaid.GetThirdTableTracking.ThirdTableTrackingResponse();
            string strCaseClaimId = string.Empty,
                   strIdCaso = string.Empty;

            objThirdTableTrackingResponse.QueryOACs = Claro.Web.Logging.ExecuteMethod<List<QueryOAC>>(objThirdTableTrackingRequest.Audit.Session, objThirdTableTrackingRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetThirdTableTrackingPrevious(objThirdTableTrackingRequest.Audit.Session, objThirdTableTrackingRequest.Audit.Transaction, objThirdTableTrackingRequest.CaseClaimId); });

            if (objThirdTableTrackingResponse.QueryOACs != null)
                strCaseClaimId = objThirdTableTrackingResponse.QueryOACs[objThirdTableTrackingResponse.QueryOACs.Count - 1].ReclamoOhxactCaso;

            if (strCaseClaimId == "")
                strIdCaso = Constants.ZeroNumber;
            else
                strIdCaso = strCaseClaimId;

            objThirdTableTrackingResponse.QueryOACs = Claro.Web.Logging.ExecuteMethod<List<QueryOAC>>(objThirdTableTrackingRequest.Audit.Session, objThirdTableTrackingRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetThirdTableTracking(objThirdTableTrackingRequest.Audit.Session, objThirdTableTrackingRequest.Audit.Transaction, strIdCaso); });

            return objThirdTableTrackingResponse;
        }

        public static Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSResponse GetBalanceCBIOS(Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSRequest objBalanceCBIOSRequest)
        {
            string strResponseCode = "";
            string strMessage = "";

            Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSResponse objBalanceCBIOSResponse = new Entity.Dashboard.Postpaid.GetBalanceCBIOS.BalanceCBIOSResponse()
            {

                lstBagBalanceCBIOS = Claro.Web.Logging.ExecuteMethod<List<BagBalanceCBIOS>>(objBalanceCBIOSRequest.Audit.Session, objBalanceCBIOSRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetBalanceCBIOS(objBalanceCBIOSRequest.CustomerId, objBalanceCBIOSRequest.Host, objBalanceCBIOSRequest.Telephone, objBalanceCBIOSRequest.Audit, out strResponseCode, out strMessage); }),
                strMessage = strMessage,
                strResponseCode = strResponseCode
            };

            return objBalanceCBIOSResponse;
        }
        private static bool ValidateCustomer(string strTypeCustomer, string key)
        {
            if (!string.IsNullOrEmpty(strTypeCustomer))
            {
                foreach (var item in key.Split('|'))
                {
                    if (string.Compare(strTypeCustomer, item, true) == 0)
                    {
                        return true;

                    }

                }
            }
            return false;
        }
        public static int LoadDataBagByAccount(Entity.Dashboard.Postpaid.GetSharedBag.SharedBagRequest objSharedBagRequest, out string strNumberCellPhone, ref string strMessageByAccount)
        {

            int count = Claro.Constants.NumberZero;

            try
            {
                List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstService = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.Service>>(objSharedBagRequest.Audit.Session, objSharedBagRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTelephoneNumbersPost(objSharedBagRequest.Audit.Session, objSharedBagRequest.Audit.Transaction, objSharedBagRequest.Customerid); });
                if (lstService != null && lstService.Count > Claro.Constants.NumberZero)
                {
                    count = lstService.Count;
                    var result = (from l in lstService
                                  select l).FirstOrDefault();

                    strNumberCellPhone = result.NRO_CELULAR;

                }
                else
                {
                    strNumberCellPhone = string.Empty;
                    count = Claro.Constants.NumberZero;
                }
            }
            catch (Exception ex)
            {
                count = Claro.Constants.NumberZero;
                strMessageByAccount = ex.Message;
                throw new Exception(ex.Message);
            }

            return count;
        }
        public static int LoadDataBagByLine(Entity.Dashboard.Postpaid.GetSharedBag.SharedBagRequest objSharedBagRequest, ref string strMessageByLine, out List<Claro.SIACU.Entity.Dashboard.Postpaid.SharedBag> lstSharedBag)
        {

            int count = Claro.Constants.NumberZero;

            try
            {
                lstSharedBag = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.SharedBag>>(objSharedBagRequest.Audit.Session, objSharedBagRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDataBagBalanceShared(objSharedBagRequest.Audit, objSharedBagRequest.Account, objSharedBagRequest.Customerid, objSharedBagRequest.Telephone); });
                if (lstSharedBag != null && lstSharedBag.Count > Claro.Constants.NumberZero)
                {
                    count = lstSharedBag.Count;
                }



            }
            catch (Exception ex)
            {
                count = Claro.Constants.NumberZero;
                strMessageByLine = ex.Message;
                throw new Exception(ex.Message);
            }

            return count;
        }
        public static int LoadDataLine(Entity.Dashboard.Postpaid.GetSharedBag.SharedBagRequest objSharedBagRequest, ref string strMessageByLine, ref List<Claro.SIACU.Entity.Dashboard.Postpaid.SharedBag> lstSharedBaglines)
        {

            int count = Claro.Constants.NumberZero;
            List<Claro.SIACU.Entity.Dashboard.Postpaid.SharedBag> lstSharedBagline;
            try
            {
                List<Claro.SIACU.Entity.Dashboard.Postpaid.Service> lstService = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.Service>>(objSharedBagRequest.Audit.Session, objSharedBagRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTelephoneNumbersPost(objSharedBagRequest.Audit.Session, objSharedBagRequest.Audit.Transaction, objSharedBagRequest.Customerid); });
                if (lstService != null && lstService.Count > 0)
                {
                    foreach (Claro.SIACU.Entity.Dashboard.Postpaid.Service item in lstService)
                    {
                        lstSharedBagline = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.SharedBag>>(objSharedBagRequest.Audit.Session, objSharedBagRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetDataBagLineBalanceShared(objSharedBagRequest.Account, KEY.AppSettings("USRProceso"), objSharedBagRequest.Audit.Transaction, objSharedBagRequest.Audit.IPAddress, objSharedBagRequest.Audit.UserName, item.NRO_CELULAR); });

                        if (lstSharedBagline != null && lstSharedBagline.Count > 0)
                        {
                            lstSharedBaglines.AddRange(lstSharedBagline);
                        }
                    }
                    lstSharedBaglines.Sort();
                    count = lstSharedBaglines.Count;

                }

            }

            catch (Exception ex)
            {
                count = Claro.Constants.NumberZero;
                strMessageByLine = ex.Message;
                throw new Exception(ex.Message);
            }

            return count;
        }
        public static Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse GetDataBalanceShared(Entity.Dashboard.Postpaid.GetSharedBag.SharedBagRequest objSharedBagRequest)
        {

            string strNumberCellPhone = "";
            string strMessageByAccount = "";
            string strMessageByLine = "";
            string strMessageByDataLine = "";
            List<Claro.SIACU.Entity.Dashboard.Postpaid.SharedBag> lstSharedBag = new List<SharedBag>();
            List<Claro.SIACU.Entity.Dashboard.Postpaid.SharedBag> lstSharedBagLine = new List<SharedBag>();
            Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse objSharedBagResponse = new Entity.Dashboard.Postpaid.GetSharedBag.SharedBagResponse();
            try
            {


                if (ValidateCustomer(objSharedBagRequest.TypeCustomer, KEY.AppSettings("strTipoClienteBolsa")))
                {

                    if (string.IsNullOrEmpty(objSharedBagRequest.Telephone))
                    {

                        if (LoadDataBagByAccount(objSharedBagRequest, out strNumberCellPhone, ref strMessageByAccount) == Claro.Constants.NumberZero)
                        {
                            if (!string.IsNullOrEmpty(strMessageByAccount))
                            {
                                objSharedBagResponse.Message = strMessageByAccount;
                            }
                            else
                            {
                                objSharedBagResponse.MessageTypeCustomer = KEY.AppSettings("strMsjBolsaCompartida");
                            }
                        }
                        else
                        {
                            if (LoadDataBagByLine(objSharedBagRequest, ref strMessageByLine, out lstSharedBag) == Claro.Constants.NumberZero)
                            {
                                if (!string.IsNullOrEmpty(strMessageByLine))
                                {
                                    objSharedBagResponse.Message = strMessageByLine;
                                }
                                else
                                {
                                    objSharedBagResponse.MessageTypeCustomer = KEY.AppSettings("strMsjBolsaCompartida");
                                }
                            }
                            else
                            {
                                objSharedBagResponse.ListSharedBag = lstSharedBag;

                                if (LoadDataLine(objSharedBagRequest, ref strMessageByDataLine, ref lstSharedBagLine) == Claro.Constants.NumberZero)
                                {
                                    if (!string.IsNullOrEmpty(strMessageByDataLine))
                                    {
                                        objSharedBagResponse.Message = strMessageByDataLine;
                                    }
                                    objSharedBagResponse.CountSharedBag = lstSharedBagLine.Count.ToString();
                                }
                                else
                                {
                                    objSharedBagResponse.ListSharedBagLine = lstSharedBagLine;
                                    objSharedBagResponse.CountSharedBag = lstSharedBagLine.Count.ToString();
                                }

                            }

                        }


                    }
                    else
                    {


                        if (LoadDataBagByLine(objSharedBagRequest, ref strMessageByLine, out lstSharedBag) == Claro.Constants.NumberZero)
                        {
                            if (!string.IsNullOrEmpty(strMessageByLine))
                            {
                                objSharedBagResponse.Message = strMessageByLine;
                            }
                            else
                            {
                                objSharedBagResponse.MessageTypeCustomer = KEY.AppSettings("strMsjBolsaCompartida");
                            }
                        }
                        else
                        {
                            objSharedBagResponse.ListSharedBag = lstSharedBag;
                            if (LoadDataLine(objSharedBagRequest, ref strMessageByDataLine, ref lstSharedBagLine) == Claro.Constants.NumberZero)
                            {
                                if (!string.IsNullOrEmpty(strMessageByDataLine))
                                {
                                    objSharedBagResponse.Message = strMessageByDataLine;
                                }
                                objSharedBagResponse.CountSharedBag = lstSharedBagLine.Count.ToString();
                            }
                            else
                            {
                                objSharedBagResponse.ListSharedBagLine = lstSharedBagLine;
                                objSharedBagResponse.CountSharedBag = lstSharedBagLine.Count.ToString();
                            }
                        }

                    }

                }
                else
                {
                    objSharedBagResponse.MessageTypeCustomer = KEY.AppSettings("strMsjNoConsumer");
                }
            }
            catch (Exception)
            {
                objSharedBagResponse.Message = ConfigurationManager.AppSettings("strMsgErrorTransaccion");

            }

            return objSharedBagResponse;
        }



        public static Entity.Dashboard.Postpaid.GetHistoricalRecharge.HistoricalRechargeResponse GetHistoricalRecharge(Entity.Dashboard.Postpaid.GetHistoricalRecharge.HistoricalRechargeRequest objHistoricalRechargeRequest)
        {
            Entity.Dashboard.Postpaid.GetHistoricalRecharge.HistoricalRechargeResponse objHistoricalRechargeResponse = new POSTPAID.GetHistoricalRecharge.HistoricalRechargeResponse();
            objHistoricalRechargeRequest.strMsisdn = string.Format("{0}{1}", ConfigurationManager.AppSettings("strConstPrefijo"), objHistoricalRechargeRequest.strMsisdn.Trim());
            string strCodigoRespuesta = "", strMensajeRespuesta = "";
            if (string.IsNullOrEmpty(objHistoricalRechargeRequest.strDateStart) && string.IsNullOrEmpty(objHistoricalRechargeRequest.strDateEnd))
            {
                DateTime dtIni = DateTime.Now.AddMonths(-6);
                objHistoricalRechargeRequest.strDateStart = dtIni.ToShortDateString();
                objHistoricalRechargeRequest.strDateEnd = DateTime.Today.ToShortDateString();
            }
            List<Claro.SIACU.Entity.Dashboard.Postpaid.HistoricalRecharge> lstHistoricalRecharge = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.HistoricalRecharge>>(objHistoricalRechargeRequest.Audit.Session, objHistoricalRechargeRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetHistoricalRecharge(objHistoricalRechargeRequest.Audit, objHistoricalRechargeRequest.strDateStart, objHistoricalRechargeRequest.strMsisdn, objHistoricalRechargeRequest.strDateEnd, objHistoricalRechargeRequest.strFlagPlataform, ref strCodigoRespuesta, ref strMensajeRespuesta); });
            if (strCodigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                if (lstHistoricalRecharge.Count <= 0)
                {
                    objHistoricalRechargeResponse.strMensajeAlert = KEY.AppSettings("strNoTieneRecargasRealizadas");
                }
                else
                {
                    objHistoricalRechargeResponse.lstHistoricalRecharge = lstHistoricalRecharge;
                }
            }
            else if (strCodigoRespuesta.Equals(Claro.Constants.NumberOneString) || strCodigoRespuesta.Equals(Claro.Constants.NumberTwoString))
            {

                objHistoricalRechargeResponse.strMensajeAlert = KEY.AppSettings("strNoTieneRecargasRealizadas");
            }
            else
            {
                objHistoricalRechargeResponse.strMensajeAlert = KEY.AppSettings("strVuelvaIntentar");
            }


            return objHistoricalRechargeResponse;
        }

        public static Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeResponsePospaid GetConsumptionHistoricalRecharge(Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeRequestPospaid objConsumptionHistoricalRechargeRequestPospaid)
        {
            Entity.Dashboard.Postpaid.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeResponsePospaid objConsumptionHistoricalRechargeResponsePospaid = new POSTPAID.GetConsumptionHistoricalRecharge.ConsumptionHistoricalRechargeResponsePospaid();
            objConsumptionHistoricalRechargeRequestPospaid.strMsisdn = string.Format("{0}{1}", ConfigurationManager.AppSettings("strZIPCodePeru"), objConsumptionHistoricalRechargeRequestPospaid.strMsisdn.Trim());
            string strCodigoRespuesta = "", strMensajeRespuesta = "";
            if (string.IsNullOrEmpty(objConsumptionHistoricalRechargeRequestPospaid.strDateStart) && string.IsNullOrEmpty(objConsumptionHistoricalRechargeRequestPospaid.strDateEnd))
            {
                DateTime dtIni = DateTime.Now.AddMonths(-6);
                objConsumptionHistoricalRechargeRequestPospaid.strDateStart = dtIni.ToShortDateString();
                objConsumptionHistoricalRechargeRequestPospaid.strDateEnd = DateTime.Today.ToShortDateString();
            }
            if (objConsumptionHistoricalRechargeRequestPospaid.strTypeConsumption == null || objConsumptionHistoricalRechargeRequestPospaid.strTypeConsumption == "Todos")
            {
                objConsumptionHistoricalRechargeRequestPospaid.strTypeConsumption = "";
            }

            List<Claro.SIACU.Entity.Dashboard.Postpaid.ConsumptionRecharge> lstConsumptionRecharge = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.ConsumptionRecharge>>(objConsumptionHistoricalRechargeRequestPospaid.Audit.Session, objConsumptionHistoricalRechargeRequestPospaid.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetConsumptionHistoricalRecharge(objConsumptionHistoricalRechargeRequestPospaid.Audit, objConsumptionHistoricalRechargeRequestPospaid.strDateStart, objConsumptionHistoricalRechargeRequestPospaid.strMsisdn, objConsumptionHistoricalRechargeRequestPospaid.strDateEnd, objConsumptionHistoricalRechargeRequestPospaid.strTypeConsumption, ref strCodigoRespuesta, ref strMensajeRespuesta); });
            if (strCodigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                if (lstConsumptionRecharge.Count <= 0)
                {
                    objConsumptionHistoricalRechargeResponsePospaid.strMensajeAlert = KEY.AppSettings("strNoTieneConsumoRecargas");
                }
                else
                {
                    objConsumptionHistoricalRechargeResponsePospaid.lstConsumptionRecharge = lstConsumptionRecharge;
                }
            }
            else if (strCodigoRespuesta.Equals(Claro.Constants.NumberOneString) ||
                strCodigoRespuesta.Equals(Claro.Constants.NumberThreeString) ||
                strCodigoRespuesta.Equals(Claro.Constants.NumberFourString) ||
                strCodigoRespuesta.Equals(Claro.Constants.NumberTwoString))
            {

                objConsumptionHistoricalRechargeResponsePospaid.strMensajeAlert = KEY.AppSettings("strNoTieneConsumoRecargas");
            }
            else
            {
                objConsumptionHistoricalRechargeResponsePospaid.strMensajeAlert = KEY.AppSettings("strVuelvaIntentar");
            }


            return objConsumptionHistoricalRechargeResponsePospaid;
        }
        public static Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionResponse GetTypeConsumption(Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionRequest objGetTypeConsumptionRequest)
        {
            Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionResponse objGetTypeConsumptionResponse = new Entity.Dashboard.Postpaid.GetTypeConsumption.GetTypeConsumptionResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objGetTypeConsumptionRequest.Audit.Session, objGetTypeConsumptionRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML("ListaTipoDeConsumo"); })
            };
            return objGetTypeConsumptionResponse;
        }
        //mg13
        public static List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS> GetPackageAcquiredODCS(bool isTipifica, Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest, ref string CodigoRespuesta, ref string MensajeRespuesta)
        {
            string strCodigoRespuesta = "", strMensajeRespuesta = "";
            string idTransaccion, dobleCero;
            DateTime fechaActual = DateTime.Now;

            Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS objPackageODCS = new Entity.Dashboard.Prepaid.PackageODCS();

            objPackageODCS.Msisdn = objTotalMbPurchasedPackageRequest.objPackageODCS.Msisdn;
            objPackageODCS.Channel = KEY.AppSettings("gConstCanalODCS").Trim();
            objPackageODCS.PackageCode = KEY.AppSettings("gConstCodigoPaqueteODCS").Trim();
            objPackageODCS.State = KEY.AppSettings("gConstEstadoPaqueteODCS").Trim();
            if (string.IsNullOrEmpty(objTotalMbPurchasedPackageRequest.objPackageODCS.ActivationDate) && string.IsNullOrEmpty(objTotalMbPurchasedPackageRequest.objPackageODCS.ExpirationDate))
            {
                DateTime dtIni = DateTime.Now.AddMonths(-6);
                objTotalMbPurchasedPackageRequest.objPackageODCS.ActivationDate = dtIni.ToShortDateString();
                objTotalMbPurchasedPackageRequest.objPackageODCS.ExpirationDate = DateTime.Today.ToShortDateString();
            }
            objPackageODCS.LineTypeId = KEY.AppSettings("gConstTipoLineaODCS");
            objPackageODCS.FamilyPackage = KEY.AppSettings("gConstFamiliaPaqueteODCS").Trim();
            objPackageODCS.ActivationDate = objTotalMbPurchasedPackageRequest.objPackageODCS.ActivationDate;
            objPackageODCS.ExpirationDate = objTotalMbPurchasedPackageRequest.objPackageODCS.ExpirationDate;

            List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS>>(objTotalMbPurchasedPackageRequest.objAudit.Session, objTotalMbPurchasedPackageRequest.objAudit.Transaction, () => { return Data.Dashboard.Postpaid.GetPostConsumptionDataPacket(objTotalMbPurchasedPackageRequest.objAudit, objPackageODCS, ref strCodigoRespuesta, ref strMensajeRespuesta); });
            CodigoRespuesta = strCodigoRespuesta;
            MensajeRespuesta = strMensajeRespuesta;


            if (CodigoRespuesta == "0")
            {
                if (isTipifica)
                {
                    Claro.SIACU.Entity.Dashboard.Interaction objInteraction = new Entity.Dashboard.Interaction()
                    {

                        CLASE = KEY.AppSettings("strClaseTipifica"),
                        CODIGOEMPLEADO = objTotalMbPurchasedPackageRequest.objAudit.UserName,
                        CODIGOSISTEMA = KEY.AppSettings("USRProceso"),
                        FLAGCASO = Claro.Constants.NumberZeroString,
                        HECHOENUNO = Claro.Constants.NumberOneString,
                        METODOCONTACTO = "Teléfono",
                        NOTAS = KEY.AppSettings("strMensajeTipifica") + objPackageODCS.ActivationDate + " hasta " + objPackageODCS.ExpirationDate,
                        SUBCLASE = KEY.AppSettings("strSubClaseTipifica"),
                        TELEFONO = objPackageODCS.Msisdn,
                        TEXTRESULTADO = KEY.AppSettings("strNinguno"),
                        TIPOINTERACCION = KEY.AppSettings("strEntrante"),
                        TIPO = KEY.AppSettings("strPostpago")

                    };
                    bool result = Claro.Web.Logging.ExecuteMethod<bool>(objTotalMbPurchasedPackageRequest.objAudit.Session, objTotalMbPurchasedPackageRequest.objAudit.Transaction, () => { return Data.Dashboard.Postpaid.CreateInteraction(objTotalMbPurchasedPackageRequest.objAudit.Transaction, objInteraction, objTotalMbPurchasedPackageRequest.objAudit); });
                    Claro.Web.Logging.Info(objTotalMbPurchasedPackageRequest.objAudit.Session, objTotalMbPurchasedPackageRequest.objAudit.Transaction, "La interaccion se registro : " + result);
                }
            }
            return lstPackageODCS;
        }

        public static Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetTotalMbPurchasedPackageResponse(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS itemPackageODCS;
            double pTotalMBCiclo = 0;
            string strMB;
            string CodigoRespuesta = "", MensajeRespuesta = "";
            Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse objTotalMbPurchasedPackageResponse = new POSTPAID.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse();
            List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS = GetPackageAcquiredODCS(false, objTotalMbPurchasedPackageRequest, ref CodigoRespuesta, ref MensajeRespuesta);


            if (CodigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                if (lstPackageODCS != null && lstPackageODCS.Count > 0)
                {
                    itemPackageODCS = new Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS();
                    for (int i = 0; i < lstPackageODCS.Count; i++)
                    {
                        itemPackageODCS = lstPackageODCS[i];
                        if (!(itemPackageODCS.MBTotal == string.Empty))
                        {
                            pTotalMBCiclo = pTotalMBCiclo + Convert.ToDouble(itemPackageODCS.MBTotal);
                        }
                    }


                    strMB = ConfigurationManager.AppSettings("gConstMbODCS");
                    objTotalMbPurchasedPackageResponse.TotalMBCicle = pTotalMBCiclo.ToString("F") + strMB;
                    objTotalMbPurchasedPackageResponse.TotalRows = lstPackageODCS.Count.ToString();
                    objTotalMbPurchasedPackageResponse.lstPackageODCS = lstPackageODCS;

                }
            }

            return objTotalMbPurchasedPackageResponse;
        }

        //vtorremo - Nuevo Tobe
        public static Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse GetConsultarSaldoPostPagoTobe(Entity.Dashboard.Postpaid.GetRecharges.RechargesRequest objRechargesRequest, string plataforma)
        {
            string claroPuntos = "";
            string strCodigoRespuesta = "";
            string strMensajeRespuesta = "";
            string strCustomerCode = "";
            strCustomerCode = objRechargesRequest.Account;

            List<Recharge> lstConsultarSaldoPostPago = null;

            Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse objRechargesResponse = new Entity.Dashboard.Postpaid.GetRecharges.RechargesResponse();


            try
            {
                lstConsultarSaldoPostPago = Claro.Web.Logging.ExecuteMethod<List<Recharge>>(objRechargesRequest.Audit.Session, objRechargesRequest.FechaExpiracion, objRechargesRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetBalancePostpaidConsumerB2ELegacy(objRechargesRequest.Audit, objRechargesRequest.Telephone, objRechargesRequest.Contract, objRechargesRequest.FechaExpiracion, ref strCustomerCode, ref claroPuntos, ref strCodigoRespuesta, ref strMensajeRespuesta, objRechargesRequest.FechaExpiracion);

                });

                objRechargesResponse.Telephone = objRechargesRequest.Telephone;
                objRechargesResponse.Recharges = lstConsultarSaldoPostPago;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRechargesRequest.Audit.Session, objRechargesRequest.Audit.Transaction, "Message Error : " + ex.Message.ToString());
            }

            return objRechargesResponse;
        } //vtorremo - Nuevo Tobe


        public static Entity.Dashboard.Postpaid.GetMbBag.MbBagResponse GetMbBag(Entity.Dashboard.Postpaid.GetMbBag.MbBagRequest objMbBagRequest)
        {
            string strtCustomerCode = "", strCodigoRespuesta = "", strMensajeRespuesta = "", claroPuntos = "";
            Entity.Dashboard.Postpaid.GetMbBag.MbBagResponse objMbBagResponse = new POSTPAID.GetMbBag.MbBagResponse();


            objMbBagRequest.strMsidn = String.Format("{0}{1}", ConfigurationManager.AppSettings("strConstPrefijo"), objMbBagRequest.strMsidn.Trim());
            objMbBagRequest.objAudit.ApplicationName = KEY.AppSettings("gConstAplicacionIDBroker");
            List<Claro.SIACU.Entity.Dashboard.Postpaid.Recharge> lstRecharge =
                Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.Recharge>>(objMbBagRequest.objAudit.Session, objMbBagRequest.objAudit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetBalancePostpaidConsumerB2E(objMbBagRequest.objAudit, objMbBagRequest.strMsidn, objMbBagRequest.strCustomerId, ref strtCustomerCode, ref claroPuntos, ref strCodigoRespuesta, ref strMensajeRespuesta);
                });

            Claro.SIACU.Entity.Dashboard.Postpaid.Recharge objRecharge = lstRecharge.Where(s => s.ID_BOLSA == KEY.AppSettings("strBolsaMBDisponibles")).FirstOrDefault();
            if (objRecharge == null)
            {
                objMbBagResponse.strMbAvailable = string.Empty;
            }
            else
            {
                objMbBagResponse.strMbAvailable = objRecharge.SALDO + KEY.AppSettings("gConstMbODCS");
            }


            return objMbBagResponse;
        }


        public static Entity.Dashboard.Postpaid.GetTypePackage.TypePakageResponse GetTypePakage(Entity.Dashboard.Postpaid.GetTypePackage.TypePakageRequest objTypePakageRequest)
        {
            Entity.Dashboard.Postpaid.GetTypePackage.TypePakageResponse objTypePakageResponse = new Entity.Dashboard.Postpaid.GetTypePackage.TypePakageResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objTypePakageRequest.Audit.Session, objTypePakageRequest.Audit.Transaction, () =>
                {
                    if (objTypePakageRequest.Plataforma == Constants.TOBE)
                    {
                        return Data.Dashboard.Common.GetListValuesXML(KEY.AppSettings("gConstKeyTipoPaqueteToBe"));
                    }
                    else
                    {
                        return Data.Dashboard.Common.GetListValuesXML(KEY.AppSettings("gConstKeyTipoPaquete"));
                    }

                })
            };
            return objTypePakageResponse;
        }

        //vtorremo
        public static Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetHistoricalPackage(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest, string plataforma, string flagConvivencia)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS itemPackageODCS;
            string CodigoRespuesta = "", MensajeRespuesta = "";
            int numLongitud;
            string strdias;
            Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse objTotalMbPurchasedPackageResponse = new POSTPAID.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse();
            List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS = GetPackageAcquiredODCS(true, objTotalMbPurchasedPackageRequest, ref CodigoRespuesta, ref MensajeRespuesta);
            DateTime fechaActivacion, fechaExpiracion;
            objTotalMbPurchasedPackageResponse.IsVisibleMensaje = false;
            objTotalMbPurchasedPackageResponse.FlagOne = Claro.Constants.NumberZeroString;
            if (CodigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                if (lstPackageODCS != null && lstPackageODCS.Count > 0)
                {
                    itemPackageODCS = new Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS();
                    numLongitud = Convert.ToInt(KEY.AppSettings("gConstLongitudHoraODCS"));
                    for (int i = 0; i < lstPackageODCS.Count; i++)
                    {
                        itemPackageODCS = lstPackageODCS[i];
                        if (!((itemPackageODCS.ActivationDate == string.Empty) && (itemPackageODCS.ExpirationDatePost == string.Empty)))
                        {
                            fechaActivacion = DateTime.Parse(itemPackageODCS.ActivationDate);
                            fechaExpiracion = DateTime.Parse(itemPackageODCS.ExpirationDatePost);
                            string xActivationDate = itemPackageODCS.ActivationDate.Substring((itemPackageODCS.ActivationDate.Count()) - numLongitud, numLongitud);
                            if (fechaExpiracion >= fechaActivacion)
                            {
                                if (xActivationDate == KEY.AppSettings("gConstFormaHoraODCS"))
                                {
                                    itemPackageODCS.validity = Convert.ToString(((fechaExpiracion - fechaActivacion).TotalDays) + 1);
                                }
                                else
                                {
                                    itemPackageODCS.validity = Convert.ToString(((fechaExpiracion - fechaActivacion).TotalDays));
                                }


                            }
                            if (itemPackageODCS.validity == "1")
                            {
                                strdias = "dia";
                            }
                            else
                            {
                                strdias = "dias";
                            }

                            itemPackageODCS.validity = String.Format("{0} {1} - {2}", itemPackageODCS.validity, strdias, itemPackageODCS.ExpirationDatePost);
                        }

                        if (!string.IsNullOrEmpty(itemPackageODCS.cost))
                        {
                            if (IsNumeric(itemPackageODCS.cost))
                            {
                                double cost = Convert.ToDouble(itemPackageODCS.cost);
                                itemPackageODCS.cost = cost.ToString("F");
                            }
                        }
                        if (!string.IsNullOrEmpty(itemPackageODCS.validity))
                        {
                            string xValidity = itemPackageODCS.validity.Substring(0, itemPackageODCS.validity.Length - 19);
                            string xdate = itemPackageODCS.validity.Substring(itemPackageODCS.validity.Length - 19, 19);
                            DateTime dt;
                            DateTime.TryParseExact(xdate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                            if (dt != null)
                            {
                                xdate = dt.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                itemPackageODCS.validity = xValidity + xdate.Replace("AM", "a.m.").Replace("PM", "p.m.");
                            }


                        }

                        fechaActivacion = DateTime.Parse(itemPackageODCS.ActivationDate);
                        fechaExpiracion = DateTime.Parse(itemPackageODCS.ExpirationDatePost);
                        Claro.Web.Logging.Info(objTotalMbPurchasedPackageRequest.objAudit.Session, objTotalMbPurchasedPackageRequest.objAudit.Transaction, string.Format("fechaExpiracion : {0}", fechaExpiracion.ToString()));
                        Claro.Web.Logging.Info(objTotalMbPurchasedPackageRequest.objAudit.Session, objTotalMbPurchasedPackageRequest.objAudit.Transaction, string.Format("DateTime.Now : {0}", DateTime.Now.ToString()));
                        Claro.Web.Logging.Info(objTotalMbPurchasedPackageRequest.objAudit.Session, objTotalMbPurchasedPackageRequest.objAudit.Transaction, string.Format("fechaExpiracion es mayor a fecha actual : {0}", fechaExpiracion >= DateTime.Now));
                        if (plataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase) && flagConvivencia.Equals(Claro.Constants.NumberOneString) && !string.IsNullOrEmpty(itemPackageODCS.State) && fechaExpiracion >= DateTime.Now) { itemPackageODCS.State = "VIGENTE"; }
                        else if (!string.IsNullOrEmpty(itemPackageODCS.State))
                        {
                            List<Claro.SIACU.Entity.ListItem> lstListItem = GetStatePackage(objTotalMbPurchasedPackageRequest.objAudit, KEY.AppSettings("gConstKeyListaTipoEstadoPaqueteComprado"));
                            foreach (var item in lstListItem)
                            {
                                if (item.Code == itemPackageODCS.State)
                                {
                                    itemPackageODCS.State = item.Description;
                                }

                            }
                        }
                        if (!string.IsNullOrEmpty(itemPackageODCS.AcquisitionDate))
                        {
                            DateTime dt;
                            DateTime.TryParseExact(itemPackageODCS.AcquisitionDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                            if (dt != null)
                            {
                                itemPackageODCS.AcquisitionDate = dt.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                itemPackageODCS.AcquisitionDate = itemPackageODCS.AcquisitionDate.Replace("AM", "a.m.").Replace("PM", "p.m.");
                            }
                        }

                    }

                    objTotalMbPurchasedPackageResponse.lstPackageODCS = lstPackageODCS;
                    objTotalMbPurchasedPackageResponse.TotalRows = Convert.ToString(lstPackageODCS.Count);

                }
            }
            else
            {
                objTotalMbPurchasedPackageResponse.lstPackageODCS = null;
                objTotalMbPurchasedPackageResponse.TotalRows = Claro.Constants.NumberZeroString;
                objTotalMbPurchasedPackageResponse.strMensaje = MensajeRespuesta;
            }

            if (lstPackageODCS == null || lstPackageODCS.Count == 0)
            {

                //VATM
                List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS2 = GetBalancePost(objTotalMbPurchasedPackageRequest, ref CodigoRespuesta, ref MensajeRespuesta, plataforma);

                if (CodigoRespuesta.Equals(Claro.Constants.NumberZeroString))
                {
                    if (lstPackageODCS2 != null && lstPackageODCS2.Count > 0)
                    {
                        objTotalMbPurchasedPackageResponse.FlagOne = Claro.Constants.NumberOneString;
                        itemPackageODCS = new Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS();

                        for (int i = 0; i < lstPackageODCS2.Count; i++)
                        {
                            Claro.Web.Logging.Info("Session1", "idTransaccion1", lstPackageODCS2[i].NameBag);
                            Claro.Web.Logging.Info("Session2", "idTransaccion2", string.Format("Count: {0}", lstPackageODCS2.Count()));
                            itemPackageODCS = lstPackageODCS2[i];
                            if (!string.IsNullOrEmpty(itemPackageODCS.validity))
                            {
                                string cadena = itemPackageODCS.validity.Substring(0, 2);
                                if (cadena != "0 ")
                                {
                                    itemPackageODCS.validity = string.Format("{0} - {1}", itemPackageODCS.validity, itemPackageODCS.ExpirationDatePost);
                                }
                                else
                                {
                                    itemPackageODCS.validity = itemPackageODCS.ExpirationDatePost;
                                }
                                
                            }
                            else
                            {
                                itemPackageODCS.validity = string.Format("{0}", itemPackageODCS.ExpirationDatePost);
                            }
                            Claro.Web.Logging.Info("Session3", "idTransaccion3", itemPackageODCS.validity);
                        }

                        objTotalMbPurchasedPackageResponse.lstPackageODCS = lstPackageODCS2;
                        objTotalMbPurchasedPackageResponse.TotalRows = Convert.ToString(lstPackageODCS2.Count);
                        Claro.Web.Logging.Info("Session4", "idTransaccion4", string.Format("Count: {0}", objTotalMbPurchasedPackageResponse.lstPackageODCS.Count()));
                    }
                }
                else
                {
                    if (CodigoRespuesta == Claro.Constants.NumberOneString)
                    {
                        objTotalMbPurchasedPackageResponse.IsVisibleMensaje = false;
                        objTotalMbPurchasedPackageResponse.strMensajeAlert = KEY.AppSettings("strNoTienePaquetesComprados");
                    }
                    else
                    {
                        objTotalMbPurchasedPackageResponse.IsVisibleMensaje = true;
                        objTotalMbPurchasedPackageResponse.strMensajeAlert = KEY.AppSettings("strVuelvaIntentar");
                    }

                }
            }


            return objTotalMbPurchasedPackageResponse;
        }


        public static List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS> GetBalancePost(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest, ref string CodigoRespuesta, ref string MensajeRespuesta, string plataforma)
        {
            string strCodigoRespuesta = "", strMensajeRespuesta = "";

            List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS = null;

            Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS objPackageODCS = new Entity.Dashboard.Prepaid.PackageODCS();
            objTotalMbPurchasedPackageRequest.objAudit.ApplicationName = ConfigurationManager.AppSettings("USRProceso");
            objPackageODCS.Msisdn = String.Format("{0}{1}", ConfigurationManager.AppSettings("strConstPrefijo"), objTotalMbPurchasedPackageRequest.objPackageODCS.Msisdn.Trim());

            if (string.IsNullOrEmpty(objTotalMbPurchasedPackageRequest.objPackageODCS.ActivationDate) && string.IsNullOrEmpty(objTotalMbPurchasedPackageRequest.objPackageODCS.ExpirationDate))
            {
                DateTime dtIni = DateTime.Now.AddMonths(-6);
                objTotalMbPurchasedPackageRequest.objPackageODCS.ActivationDate = dtIni.ToShortDateString();
                objTotalMbPurchasedPackageRequest.objPackageODCS.ExpirationDate = DateTime.Today.ToShortDateString();
            }
            objPackageODCS.coIdPub = objTotalMbPurchasedPackageRequest.objPackageODCS.coIdPub;
            objPackageODCS.StartDate = objTotalMbPurchasedPackageRequest.objPackageODCS.ActivationDate;
            objPackageODCS.EndDate = objTotalMbPurchasedPackageRequest.objPackageODCS.ExpirationDate;
            if (objTotalMbPurchasedPackageRequest.objPackageODCS.TypePackage == null)
            {
                objPackageODCS.TypePackage = "";
            }
            else if (objTotalMbPurchasedPackageRequest.objPackageODCS.TypePackage == "")
            {
                objPackageODCS.TypePackage = "";
            }
            else if (objTotalMbPurchasedPackageRequest.objPackageODCS.TypePackage.ToUpper() == "MINUTOS")
            {
                objPackageODCS.TypePackage = Claro.SIACU.Constants.MIN;
            }
            else if (objTotalMbPurchasedPackageRequest.objPackageODCS.TypePackage.ToUpper() == "DATOS")
            {
                objPackageODCS.TypePackage = Claro.SIACU.Constants.MB;
            }
            else
            {
                objPackageODCS.TypePackage = objTotalMbPurchasedPackageRequest.objPackageODCS.TypePackage;
            }



            //CAMBIO

            if (plataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
            {
                lstPackageODCS = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS>>(objTotalMbPurchasedPackageRequest.objAudit.Session, objTotalMbPurchasedPackageRequest.objAudit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetHistoryPackageTobe(objTotalMbPurchasedPackageRequest.objAudit, objPackageODCS, ref strCodigoRespuesta, ref strMensajeRespuesta);
                });
                CodigoRespuesta = strCodigoRespuesta;
                MensajeRespuesta = strMensajeRespuesta;
            }
            else
            {
                objPackageODCS.TypePackage = string.Empty;
                //List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS =
                lstPackageODCS = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS>>(objTotalMbPurchasedPackageRequest.objAudit.Session, objTotalMbPurchasedPackageRequest.objAudit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetHistoricalPackage(objTotalMbPurchasedPackageRequest.objAudit, objPackageODCS, ref strCodigoRespuesta, ref strMensajeRespuesta);
                });

                CodigoRespuesta = strCodigoRespuesta;
                MensajeRespuesta = strMensajeRespuesta;
            }

            return lstPackageODCS;
        }

        public static Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetDataOnePackage(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest, string plataforma)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS itemPackageODCS;
            string CodigoRespuesta = "", MensajeRespuesta = "";

            Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse objTotalMbPurchasedPackageResponse = new POSTPAID.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse();
            objTotalMbPurchasedPackageResponse.IsVisibleMensaje = true;
            List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS2 = GetBalancePost(objTotalMbPurchasedPackageRequest, ref CodigoRespuesta, ref MensajeRespuesta, plataforma);
            if (CodigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                if (lstPackageODCS2 != null && lstPackageODCS2.Count > 0)
                {
                    objTotalMbPurchasedPackageResponse.FlagOne = Claro.Constants.NumberOneString;
                    if (!string.IsNullOrEmpty(objTotalMbPurchasedPackageRequest.objPackageODCS.TypePackage))
                    {
                        lstPackageODCS2 = lstPackageODCS2.Where(s => string.Compare(s.TypePackage, objTotalMbPurchasedPackageRequest.objPackageODCS.TypePackage, true) == 0).ToList();
                    }
                    itemPackageODCS = new Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS();
                    for (int i = 0; i < lstPackageODCS2.Count; i++)
                    {
                        itemPackageODCS = lstPackageODCS2[i];
                        if (!string.IsNullOrEmpty(itemPackageODCS.validity))
                        {
                            itemPackageODCS.validity = string.Format("{0} - {1}", itemPackageODCS.validity, itemPackageODCS.ExpirationDatePost);
                        }
                        else
                        {
                            itemPackageODCS.validity = string.Format("{0}", itemPackageODCS.ExpirationDatePost);
                        }

                    }
                    if (lstPackageODCS2 == null || lstPackageODCS2.Count <= 0)
                    {
                        objTotalMbPurchasedPackageResponse.IsVisibleMensaje = false;
                        objTotalMbPurchasedPackageResponse.strMensajeAlert = KEY.AppSettings("strNoTienePaquetesComprados");
                    }
                    objTotalMbPurchasedPackageResponse.lstPackageODCS = lstPackageODCS2;
                    objTotalMbPurchasedPackageResponse.TotalRows = Convert.ToString(lstPackageODCS2.Count);


                }
                else
                {
                    objTotalMbPurchasedPackageResponse.TotalRows = Claro.Constants.NumberZeroString;
                    objTotalMbPurchasedPackageResponse.IsVisibleMensaje = false;
                    objTotalMbPurchasedPackageResponse.strMensajeAlert = KEY.AppSettings("strNoTienePaquetesComprados");

                }
            }
            else
            {
                objTotalMbPurchasedPackageResponse.TotalRows = Claro.Constants.NumberZeroString;
                objTotalMbPurchasedPackageResponse.IsVisibleMensaje = false;
                objTotalMbPurchasedPackageResponse.strMensajeAlert = KEY.AppSettings("strNoTienePaquetesComprados");


            }


            return objTotalMbPurchasedPackageResponse;
        }

        public static Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse GetDataJanusPackage(Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageRequest objTotalMbPurchasedPackageRequest)
        {
            Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS itemPackageODCS;
            string CodigoRespuesta = "", MensajeRespuesta = "";
            int numLongitud;
            string strdias;
            Entity.Dashboard.Postpaid.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse objTotalMbPurchasedPackageResponse = new POSTPAID.GetTotalMbPurchasedPackage.TotalMbPurchasedPackageResponse();
            List<Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS> lstPackageODCS = GetPackageAcquiredODCS(true, objTotalMbPurchasedPackageRequest, ref CodigoRespuesta, ref MensajeRespuesta);
            DateTime fechaActivacion, fechaExpiracion;
            objTotalMbPurchasedPackageResponse.IsVisibleMensaje = true;
            if (CodigoRespuesta.Equals(Claro.Constants.NumberZeroString))
            {
                if (!string.IsNullOrEmpty(objTotalMbPurchasedPackageRequest.objPackageODCS.TypePackage))
                {
                    lstPackageODCS = lstPackageODCS.Where(s => string.Compare(s.TypePackage, objTotalMbPurchasedPackageRequest.objPackageODCS.TypePackage, true) == 0).ToList();
                }
                if (lstPackageODCS != null && lstPackageODCS.Count > 0)
                {
                    itemPackageODCS = new Claro.SIACU.Entity.Dashboard.Prepaid.PackageODCS();
                    numLongitud = Convert.ToInt(KEY.AppSettings("gConstLongitudHoraODCS"));
                    for (int i = 0; i < lstPackageODCS.Count; i++)
                    {
                        itemPackageODCS = lstPackageODCS[i];
                        if (!((itemPackageODCS.ActivationDate == string.Empty) && (itemPackageODCS.ExpirationDatePost == string.Empty)))
                        {
                            fechaActivacion = DateTime.Parse(itemPackageODCS.ActivationDate);
                            fechaExpiracion = DateTime.Parse(itemPackageODCS.ExpirationDatePost);
                            string xActivationDate = itemPackageODCS.ActivationDate.Substring((itemPackageODCS.ActivationDate.Count()) - numLongitud, numLongitud);
                            if (fechaExpiracion >= fechaActivacion)
                            {
                                if (xActivationDate == KEY.AppSettings("gConstFormaHoraODCS"))
                                {
                                    itemPackageODCS.validity = Convert.ToString(((fechaExpiracion - fechaActivacion).TotalDays) + 1);
                                }
                                else
                                {
                                    itemPackageODCS.validity = Convert.ToString(((fechaExpiracion - fechaActivacion).TotalDays));
                                }
                            }
                            if (itemPackageODCS.validity == "1")
                            {
                                strdias = "dia";
                            }
                            else
                            {
                                strdias = "dias";
                            }

                            itemPackageODCS.validity = String.Format("{0} {1} - {2}", itemPackageODCS.validity, strdias, itemPackageODCS.ExpirationDatePost);
                        }

                        if (!string.IsNullOrEmpty(itemPackageODCS.cost))
                        {
                            if (IsNumeric(itemPackageODCS.cost))
                            {
                                double cost = Convert.ToDouble(itemPackageODCS.cost);
                                itemPackageODCS.cost = cost.ToString("F");
                            }
                        }
                        if (!string.IsNullOrEmpty(itemPackageODCS.validity))
                        {
                            string xValidity = itemPackageODCS.validity.Substring(0, itemPackageODCS.validity.Length - 19);
                            string xdate = itemPackageODCS.validity.Substring(itemPackageODCS.validity.Length - 19, 19);
                            DateTime dt;
                            DateTime.TryParseExact(xdate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                            if (dt != null)
                            {
                                xdate = dt.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                itemPackageODCS.validity = xValidity + xdate.Replace("AM", "a.m.").Replace("PM", "p.m.");
                            }


                        }
                        if (!string.IsNullOrEmpty(itemPackageODCS.State))
                        {
                            List<Claro.SIACU.Entity.ListItem> lstListItem = GetStatePackage(null, KEY.AppSettings("gConstKeyListaTipoEstadoPaqueteComprado"));
                            foreach (var item in lstListItem)
                            {
                                if (item.Code == itemPackageODCS.State)
                                {
                                    itemPackageODCS.State = item.Description;
                                }

                            }
                        }
                        if (!string.IsNullOrEmpty(itemPackageODCS.AcquisitionDate))
                        {
                            DateTime dt;
                            DateTime.TryParseExact(itemPackageODCS.AcquisitionDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                            if (dt != null)
                            {
                                itemPackageODCS.AcquisitionDate = dt.ToString("dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                                itemPackageODCS.AcquisitionDate = itemPackageODCS.AcquisitionDate.Replace("AM", "a.m.").Replace("PM", "p.m.");
                            }
                        }

                    }

                    objTotalMbPurchasedPackageResponse.lstPackageODCS = lstPackageODCS;
                    objTotalMbPurchasedPackageResponse.TotalRows = Convert.ToString(lstPackageODCS.Count);

                }
                else
                {
                    objTotalMbPurchasedPackageResponse.lstPackageODCS = null;
                    objTotalMbPurchasedPackageResponse.TotalRows = Claro.Constants.NumberZeroString;
                    objTotalMbPurchasedPackageResponse.strMensajeAlert = KEY.AppSettings("strNoTienePaquetesComprados");
                    objTotalMbPurchasedPackageResponse.IsVisibleMensaje = false;

                }
            }
            else
            {
                objTotalMbPurchasedPackageResponse.lstPackageODCS = null;
                objTotalMbPurchasedPackageResponse.TotalRows = Claro.Constants.NumberZeroString;
                objTotalMbPurchasedPackageResponse.IsVisibleMensaje = false;
                objTotalMbPurchasedPackageResponse.strMensajeAlert = KEY.AppSettings("strVuelvaIntentar");
            }

            return objTotalMbPurchasedPackageResponse;
        }



        public static bool IsNumeric(string valor)
        {
            int result;
            return int.TryParse(valor, out result);
        }
        public static List<Claro.SIACU.Entity.ListItem> GetStatePackage(Claro.Entity.AuditRequest Audit, string valor)
        {

            return Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(Audit.Session, Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML(valor); });
        }


        public static Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderResponse GetTypeOrder(Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderRequest objTypeOrderRequest)
        {
            Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderResponse objTypeOrderResponse = new Entity.Dashboard.Postpaid.GeTypeOrder.TypeOrderResponse()
            {
                ListItem = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.ListItem>>(objTypeOrderRequest.Audit.Session, objTypeOrderRequest.Audit.Transaction, () => { return Data.Dashboard.Common.GetListValuesXML(KEY.AppSettings("gConstKeyListaOrdenConsumo")); })
            };
            return objTypeOrderResponse;
        }

        public static Entity.Dashboard.Postpaid.GetMotiveCancellation.MotiveCancellationResponse GetMotiveCancellation(Entity.Dashboard.Postpaid.GetMotiveCancellation.MotiveCancellationRequest objRequest)
        {
            var codeResponse = string.Empty;
            var messageResponse = string.Empty;
            var motiveCancellation = string.Empty;

            var objResponse = new Entity.Dashboard.Postpaid.GetMotiveCancellation.MotiveCancellationResponse
            {
                FlagResult = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetMotiveCancellation(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.ContractCode, objRequest.CustomerCode, ref motiveCancellation, ref codeResponse, ref messageResponse); })
            };

            objResponse.MotiveCancellation = motiveCancellation;
            objResponse.CodeError = codeResponse;
            objResponse.DescriptionError = messageResponse;

            return objResponse;
        }

        #region Servicios VoLTE y VoWiFi
        //PROY-31249
        public static Entity.Dashboard.StatusTechnologyVo GetStatusTechnologyVo(Claro.Entity.AuditRequest oAudit, string strSerie, string strTelefono)
        {
            int varSoporteTech = -1, varActiveTech = -1;
            bool boolActSrv = false;
            string strMsjeErrorSoporteSim = "", strMsjeErrorTechVo = "", strMsjSimNoSoporta = "", strMsjeErrorWSSoporteSim = "";
            Entity.Dashboard.StatusTechnologyVo oStatusTechnologyVo = new Entity.Dashboard.StatusTechnologyVo();
            string strActionIdIServiceVoLte = "", strActionIdIServiceVoWifi = "";

            try
            {
                strMsjeErrorSoporteSim = KEY.AppSettings("strMsjeErrorSoporteSim");
                strMsjeErrorTechVo = KEY.AppSettings("strMsjeErrorTechVo");
                strMsjSimNoSoporta = KEY.AppSettings("strMsjSimNoSoporta");

                varSoporteTech = Common.ConsultarTecVoLTE(oAudit, strSerie, out strMsjeErrorWSSoporteSim);
            }
            catch (Exception ex)
            {
                varSoporteTech = -3;
                Claro.Web.Logging.Error(oAudit.Session, oAudit.Transaction, ex.Message);
            }

            if (varSoporteTech == 0)
            {
                try
                {
                    strActionIdIServiceVoLte = KEY.AppSettings("strIdAccionConsultaServicioVoLTE");

                    varActiveTech = Common.ConsultarAprovisionamientoVoLTEoWifi(oAudit, strTelefono, strActionIdIServiceVoLte, out boolActSrv);
                    if (varActiveTech == 0)
                    {
                        oStatusTechnologyVo.TechnologyVoLte = boolActSrv ? Constants.Active : Constants.Deactivated;
                    }
                    else
                    {
                        oStatusTechnologyVo.TechnologyVoLte = strMsjeErrorTechVo;
                    }

                    strActionIdIServiceVoWifi = KEY.AppSettings("strIdAccionConsultaServicioVoWIFI");

                    varActiveTech = Common.ConsultarAprovisionamientoVoLTEoWifi(oAudit, strTelefono, strActionIdIServiceVoWifi, out boolActSrv);
                    if (varActiveTech == 0)
                    {
                        oStatusTechnologyVo.TechnologyVoWifi = boolActSrv ? Constants.Active : Constants.Deactivated;
                    }
                    else
                    {
                        oStatusTechnologyVo.TechnologyVoWifi = strMsjeErrorTechVo;
                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(oAudit.Session, oAudit.Transaction, ex.Message);
                    oStatusTechnologyVo.TechnologyVoLte = strMsjeErrorTechVo;
                    oStatusTechnologyVo.TechnologyVoWifi = strMsjeErrorTechVo;
                }
            }
            else
            {
                if (varSoporteTech == 1)
                {
                    oStatusTechnologyVo.TechnologyVoLte = strMsjSimNoSoporta;
                    oStatusTechnologyVo.TechnologyVoWifi = strMsjSimNoSoporta;
                }
                else
                {
                    oStatusTechnologyVo.TechnologyVoLte = strMsjeErrorSoporteSim;
                    oStatusTechnologyVo.TechnologyVoWifi = strMsjeErrorSoporteSim;
                    oStatusTechnologyVo.MessageError = strMsjeErrorWSSoporteSim;
                }
            }
            return oStatusTechnologyVo;
        }

        #endregion

        public static Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIResponse GetParameterTerminalTPI(Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIRequest objParameterTerminalTPIRequest)
        {
            string strAbrvPermissions = KEY.AppSettings("strAbrvPermissions");
            string strCodePlanTariffTFI = KEY.AppSettings("strCodePlanTariffTFI");
            string strMsj = "";
            string xCodePlanTariffTFI = "";
            string xAbrvPermissions = "";
            List<Parameter> lstParameter = Claro.Web.Logging.ExecuteMethod<List<Parameter>>(objParameterTerminalTPIRequest.Audit.Session, objParameterTerminalTPIRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetBSCSParameter(strCodePlanTariffTFI, objParameterTerminalTPIRequest.Audit.Session, objParameterTerminalTPIRequest.Audit.Transaction, out strMsj); });

            List<Parameter> lstParameterAbrv = Claro.Web.Logging.ExecuteMethod<List<Parameter>>(objParameterTerminalTPIRequest.Audit.Session, objParameterTerminalTPIRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetBSCSParameter(strAbrvPermissions, objParameterTerminalTPIRequest.Audit.Session, objParameterTerminalTPIRequest.Audit.Transaction, out strMsj); });
            xCodePlanTariffTFI = ConcatCharacters(lstParameter, '|');
            xAbrvPermissions = ConcatCharacters(lstParameterAbrv, ',');


            Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIResponse objParameterTerminalTPIResponse = new Entity.Dashboard.Postpaid.GetParameterTerminalTPI.ParameterTerminalTPIResponse()
            {
                AbrvPermissions = xAbrvPermissions,
                CodePlanTariffTFI = xCodePlanTariffTFI
            };
            return objParameterTerminalTPIResponse;
        }
        private static string ConcatCharacters(List<Parameter> lstParameter, char chCaracter)
        {
            string xResult = "";
            if (lstParameter != null)
            {
                foreach (var item in lstParameter)
                {
                    if (item.VALOR != "" && item.VALOR != null)
                    {
                        xResult += item.VALOR + chCaracter;
                    }
                }
                if (xResult != "")
                {
                    xResult = xResult.TrimEnd(chCaracter);
                }
            }
            return xResult;
        }
        public static Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse GetAnnotationWS(Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsRequest objAnnotationRequest)
        {
            string strCodeResult = "";
            string strMsgResult = "";
            Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse objAnnotationResponse = new Entity.Dashboard.Postpaid.GetAnnotations.AnnotationsResponse();
            if (objAnnotationRequest.plataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
            {

                switch (objAnnotationRequest.flagconvivencia)
                {
                    case Claro.Constants.NumberZeroString: objAnnotationRequest.flagconvivencia = Claro.Constants.NumberOneString; break;
                    case Claro.Constants.NumberOneString: objAnnotationRequest.flagconvivencia = Claro.Constants.NumberTwoString; break;
                    default: objAnnotationRequest.flagconvivencia = string.Empty; break;
                }


                objAnnotationResponse.ListAnnotation = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Annotation>>(objAnnotationRequest.Audit.Session, objAnnotationRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetAnnotationWSTobe(objAnnotationRequest.Audit, objAnnotationRequest.Contract, objAnnotationRequest.Status, objAnnotationRequest.flagconvivencia, out strCodeResult, out strMsgResult); });

            }
            else
            {
                objAnnotationResponse.ListAnnotation = Claro.Web.Logging.ExecuteMethod<List<POSTPAID.Annotation>>(objAnnotationRequest.Audit.Session, objAnnotationRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetAnnotationWS(objAnnotationRequest.Audit, objAnnotationRequest.CustomerId, objAnnotationRequest.Contract, objAnnotationRequest.Status, objAnnotationRequest.NumberTickler, out strCodeResult, out strMsgResult); });

            }


            return objAnnotationResponse;
        }
        /// <summary>
        /// Método para obtener el listado de estados de cuenta.
        /// </summary>
        /// <param name="BSS_StatusAccountRequest">Contiene datos de la consulta de estado de cuenta</param>
        /// <returns>Devuelve objeto BSS_StatusAccountResponse con el listado de estados de cuenta</returns>
        public static Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountResponse GetBSS_StatusAccount(Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountRequest objBSS_StatusAccountRequest)
        {
            Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountResponse objResponse =
            Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetBSS_StatusAccount.BSS_StatusAccountResponse>(objBSS_StatusAccountRequest.Audit.Session, objBSS_StatusAccountRequest.Audit.Transaction, () =>
            {
                return Data.Dashboard.Postpaid.GetBSS_StatusAccount(objBSS_StatusAccountRequest);
            });

            return objResponse;
        }

        public static POSTPAID.GetInteraction.InteractionResponse GetInteraction(POSTPAID.GetInteraction.InteractionRequest objInteractionRequest)
        {
            POSTPAID.GetInteraction.InteractionResponse objInteractionResponse = new POSTPAID.GetInteraction.InteractionResponse()
            {
                IdInteraction = Claro.Web.Logging.ExecuteMethod<Int64>(objInteractionRequest.Audit.Session, objInteractionRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetInteraction(objInteractionRequest.Audit.Session, objInteractionRequest.Audit.Transaction, objInteractionRequest.Document); })
            };
            return objInteractionResponse;
        }

        public static List<DataHistorical> getDataHistory(Claro.Entity.AuditRequest audit, string strIdSession, string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, string strCustomerID, string plataforma, string flagconvivencia)
        {
            List<DataHistorical> lista = null;
            if (plataforma.Equals(Claro.SIACU.Constants.TOBE, StringComparison.InvariantCultureIgnoreCase))
            {
                lista = Claro.Web.Logging.ExecuteMethod<List<DataHistorical>>(strIdSession, strIdTransaccion, () =>
                {
                    return Data.Dashboard.Postpaid.getDataHistoryTobe(audit, strCustomerID, flagconvivencia);
                });
            }
            else
            {
                lista = Claro.Web.Logging.ExecuteMethod<List<DataHistorical>>(strIdSession, strIdTransaccion, () =>
                {
                    return Data.Dashboard.Postpaid.getDataHistory(strIdSession, strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, strCustomerID);
                });
            }

            return lista;
        }
        public static POSTPAID.GetTypeDocumentDeubt.TypeDocumentDeubtResponse GetTypeDocumentDeubt(POSTPAID.GetTypeDocumentDeubt.TypeDocumentDeubtRequest objTypeDocumentDeubtRequest)
        {

            string strout_err_code = "";
            string out_err_msg = "";

            POSTPAID.GetTypeDocumentDeubt.TypeDocumentDeubtResponse objTypeDocumentDeubtResponse = new POSTPAID.GetTypeDocumentDeubt.TypeDocumentDeubtResponse()
            {
                lstCustomer = Claro.Web.Logging.ExecuteMethod<List<Customer>>(objTypeDocumentDeubtRequest.Audit.Session, objTypeDocumentDeubtRequest.Audit.Transaction, () => { return Data.Dashboard.Postpaid.GetTypeDocumentDeubt(objTypeDocumentDeubtRequest.Audit.Session, objTypeDocumentDeubtRequest.Audit.Transaction, objTypeDocumentDeubtRequest.strDocLinea, objTypeDocumentDeubtRequest.strCod_consulta, out strout_err_code, out out_err_msg); })
            };

            objTypeDocumentDeubtResponse.out_err_msg = out_err_msg;
            objTypeDocumentDeubtResponse.strout_err_code = strout_err_code;

            return objTypeDocumentDeubtResponse;
        }
        public static Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroResponse GetBonusStatusFullClaro(Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroRequest objRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Dashboard.Postpaid.GetBonusStatusFullClaro.ConsultBonusStatusFullClaroResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetBonusStatusFullClaro(objRequest); });
        }

        public static Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse GetDataCustomer2(Entity.Dashboard.Postpaid.GetCustomer.CustomerRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse objResponse = new Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse();

            objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetCustomer.CustomerResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetDataCustomer2(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Audit.IPAddress, objRequest.Audit.ApplicationName, objRequest.Audit.UserName, objRequest.AccountCustomer, objRequest.NumCellphone);
                });


            return objResponse;
        }

        public static Entity.Dashboard.Postpaid.GetInvoice.InvoiceResponse GetIdOnBase(Entity.Dashboard.Postpaid.GetInvoice.InvoiceRequest objRequest)
        {
            Entity.Dashboard.Postpaid.GetInvoice.InvoiceResponse objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetInvoice.InvoiceResponse>(() =>
                {
                    return Data.Dashboard.Postpaid.GetIdOnBase(objRequest.Audit, objRequest.customerId, objRequest.nroRecibo, objRequest.cantNroRecibo);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw;
            }



            return objResponse;
        }

        public static Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientResponse GetBonusStatusFullClaroClient(Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientRequest objRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetBonusFullClaroClient.BonusFullClaroClientResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetBonusStatusFullClaroClient(objRequest); });
        }

        public static Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerResponse GetValidateCustomer
            (Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerRequest objRequest,
            Claro.Entity.AuditRequest auditRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Legacy.GetValidateCustomer.GetValidateCustomerResponse>(auditRequest.Transaction, auditRequest.Session, () =>
            { return Data.Dashboard.Postpaid.GetValidateCustomer(objRequest, auditRequest); });
        }

        public static Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineResponse GetValidateLine
            (Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineRequest objRequest,
            Claro.Entity.AuditRequest auditRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Legacy.GetValidateLine.GetValidateLineResponse>(auditRequest.Transaction, auditRequest.Session, () =>
            { return Data.Dashboard.Postpaid.GetValidateLine(objRequest, auditRequest); });
        }

        public static Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.GetUsageThresholdsCounter GetUsageThresholdsCounter(Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.MethodCall_Request objMethodCall_Request, Claro.Entity.AuditRequest audit)
        {
            Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.Method_Response objResponse = null;
            Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.GetUsageThresholdsCounter objUsageThresholdsCounter = new POSTPAID.GetUsageThresholdsCounter.GetUsageThresholdsCounter();


            objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.Method_Response>(audit.Session, audit.Transaction, () =>
            {
                return Data.Dashboard.Postpaid.GetUsageThresholdsCounter(objMethodCall_Request, audit);
            });
            if (objResponse != null &&
                objResponse.@params != null &&
                objResponse.@params.param != null &&
                objResponse.@params.param.value != null &&
                objResponse.@params.param.value.@struct != null &&
                objResponse.@params.param.value.@struct.member != null)
            {
                foreach (var item in objResponse.@params.param.value.@struct.member)
                {
                    if (item.name.Equals("responseCode"))
                    {
                        objUsageThresholdsCounter.responseCode = item.value.i4;
                        break;
                    }
                }

                string ThresholdValue = string.Empty;
                string CounterValue_Degradation = string.Empty;
                string CounterValue_Tethering = string.Empty;
                if (objUsageThresholdsCounter.responseCode == "0")
                {
                    foreach (var item in objResponse.@params.param.value.@struct.member)
                    {
                        if (item.name.Equals("usageCounterUsageThresholdInformation"))
                        {
                            ThresholdValue = ReadUsageThreshold(item.value.array.data.value);
                            CounterValue_Degradation = ReadUsageCounter(item.value.array.data.value, ConfigurationManager.AppSettings("strDegradation_CounterID"));
                            CounterValue_Tethering = ReadUsageCounter(item.value.array.data.value, ConfigurationManager.AppSettings("strTethering_CounterID"));
                        }
                    }

                    if (Convert.ToDouble(ThresholdValue) < Convert.ToDouble(ConfigurationManager.AppSettings("strConsumptionLimit_ExactLimit_LessThan")))
                    {
                        objUsageThresholdsCounter.TypePlan = ConfigurationManager.AppSettings("strConsumptionLimit_TypePlanControl");
                        objUsageThresholdsCounter.ConsumptionLimit = ConfigurationManager.AppSettings("strConsumptionLimit_ExactLimit");
                    }
                    else if (Convert.ToDouble(ThresholdValue) >= Convert.ToDouble(ConfigurationManager.AppSettings("strConsumptionLimit_ExactLimit_LessThan"))
                        && Convert.ToDouble(ThresholdValue) < Convert.ToDouble(ConfigurationManager.AppSettings("strConsumptionLimit_OpenLimit_GreaterEqualTo")))
                    {
                        objUsageThresholdsCounter.TypePlan = ConfigurationManager.AppSettings("strConsumptionLimit_TypePlanPostpaid");
                        objUsageThresholdsCounter.ConsumptionLimit = ConfigurationManager.AppSettings("strConsumptionLimit_AdditionalLimit");
                    }
                    else if (Convert.ToDouble(ThresholdValue) >= Convert.ToDouble(ConfigurationManager.AppSettings("strConsumptionLimit_OpenLimit_GreaterEqualTo")))
                    {
                        objUsageThresholdsCounter.TypePlan = ConfigurationManager.AppSettings("strConsumptionLimit_TypePlanPostpaid");
                        objUsageThresholdsCounter.ConsumptionLimit = ConfigurationManager.AppSettings("strConsumptionLimit_OpenLimit");
                    }

                    if (CounterValue_Degradation == Claro.Constants.NumberZeroString)
                    {
                        objUsageThresholdsCounter.Degradation = ConfigurationManager.AppSettings("strDegradation_HighSpeed");
                    }
                    else if (CounterValue_Degradation == Claro.Constants.NumberOneString)
                    {
                        objUsageThresholdsCounter.Degradation = ConfigurationManager.AppSettings("strDegradation_GradientSpeed");
                    }

                    if (CounterValue_Tethering == Claro.Constants.NumberZeroString)
                    {
                        objUsageThresholdsCounter.Thetering = ConfigurationManager.AppSettings("strTethering_TetheringEnabled");
                    }
                    else if (CounterValue_Tethering == Claro.Constants.NumberOneString)
                    {
                        objUsageThresholdsCounter.Thetering = ConfigurationManager.AppSettings("strTethering_TetheringDisabled");
                    }
                }

            }

            return objUsageThresholdsCounter;
        }

        private static string ReadUsageCounter(List<Claro.SIACU.Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.Value_Response> value, string CounterID)
        {
            string result = string.Empty;
            foreach (var item in value)
            {
                foreach (var item2 in item.@struct.member)
                {

                    if (item2.name.Equals("usageCounterID") && item2.value.i4.Equals(CounterID))
                    {
                        foreach (var item3 in item.@struct.member)
                        {
                            if (item3.name.Equals("usageCounterValue"))
                            {
                                result = item3.value.@string;
                                break;
                            }
                        }
                    }


                }
            }
            return result;
        }

        private static string ReadUsageThreshold(List<Claro.SIACU.Entity.Dashboard.Postpaid.GetUsageThresholdsCounter.Value_Response> value)
        {
            string result = string.Empty;
            foreach (var item in value)
            {
                foreach (var item2 in item.@struct.member)
                {
                    if (item2.name.Equals("usageThresholdInformation"))
                    {
                        result = ReadUsageThreshold(item2.value.array.data.value);
                    }
                    else if (item2.name.Equals("usageThresholdID") && item2.value.i4.Equals(ConfigurationManager.AppSettings("strConsumptionLimitTypePlan_ThresholdID")))
                    {
                        foreach (var item3 in item.@struct.member)
                            if (item3.name.Equals("usageThresholdValue"))
                                result = item3.value.@string;
                    }
                }
            }
            return result;
        }
        public static Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppResponse GetMiClaroApp(Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppRequest objRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetMiClaroApp.MiClaroAppResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetMiClaroApp(objRequest); });
        }

        public static Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoResponse GetStateEquipo(Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoRequest objRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetStateEquipo.StateEquipoResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetStateEquipo(objRequest); });
        }

        public static List<Claro.SIACU.Entity.Dashboard.Postpaid.ListaBloDesblo> obtenerListaBloDesblo(Claro.Entity.AuditRequest auditoria, string ViCcontrato, out string MsgError)
        {
            string strMsgError = string.Empty;

            List<Claro.SIACU.Entity.Dashboard.Postpaid.ListaBloDesblo> lstBloDesblo = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Dashboard.Postpaid.ListaBloDesblo>>("obtenerListaBloDesblo", auditoria.Transaction, () =>
              { return Data.Dashboard.Postpaid.obtenerListaBloDesblo(auditoria, ViCcontrato, ref strMsgError); });
            MsgError = strMsgError;
            return lstBloDesblo;
        }
        public static POSTPAID.GetListaBloqueoDesbloqueo.Response.ResponseListaObtenerBloqueos GetListaBloqueoDesbloqueo(POSTPAID.GetListaBloqueoDesbloqueo.Request.RequestListaobtenerBloqueos objRequest, Claro.Entity.AuditRequest objAuditoriaRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<POSTPAID.GetListaBloqueoDesbloqueo.Response.ResponseListaObtenerBloqueos>(objAuditoriaRequest.Session, objAuditoriaRequest.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetListaBloqueoDesbloqueo(objRequest, objAuditoriaRequest); });
        }
        //PROY-140464 Ajustes - INI
        public static POSTPAID.GetReasonCancelInvoice.ReasonCancelInvoiceResponse GetReasonCancelInvoice(POSTPAID.GetReasonCancelInvoice.ReasonCancelInvoiceRequest objRequest)
        {
            return Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.GetReasonCancelInvoice.ReasonCancelInvoiceResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetReasonCancelInvoice(objRequest); });
        }

        public static CancelInvoiceResponse CancelInvoice(CancelInvoiceRequest objRequest)
        {
            return Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.CancelInvoice(objRequest); });
        }

        public static TypificationResponse GetTyficationList(TypificationRequest objTypificationRequest)
        {
            TypificationResponse objTypificationResponse = null;
            Customer client = null;

            try
            {
                client = Data.Dashboard.Postpaid.TypificationGetCustomer(objTypificationRequest);

                objTypificationResponse = Data.Dashboard.Postpaid.GetTyficationList(objTypificationRequest);

                objTypificationResponse.ObjSite = client.OBJID_SITE;
                objTypificationResponse.ContactSite = client.OBJID_CONTACTO;
                objTypificationResponse.Account = client.CUENTA;
                objTypificationResponse.PhoneNumber = client.TELEFONO;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypificationRequest.Audit.Session, objTypificationRequest.Audit.Transaction, ex.Message);
            }
            return objTypificationResponse;
        }

        public static EvaluateAmountResponse GetEvaluateAmount(EvaluateAmountRequest objRequest)
        {
            return Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetEvaluateAmount(objRequest); });
        }

        public static UserProfileResponse GetProfileUser(UserProfileRequest objRequest)
        {
            return Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            { return Data.Dashboard.Postpaid.GetProfileUser(objRequest); });
        }
        //PROY-140464 Ajustes - FIN

        public static List<Parameter> ObtenerBloqueosClaro(string strIdSession, string strTransaction, string nombre, out string mensaje)
        {
            string strMsj = string.Empty;
            List<Parameter> lstParameter = Claro.Web.Logging.ExecuteMethod<List<Parameter>>(strIdSession, strTransaction, () => { return Data.Dashboard.Postpaid.ObtenerBloqueosClaro(strIdSession, strTransaction, nombre, out   strMsj); });
            mensaje = strMsj;
            return lstParameter;
        }

        //INICIATIVA 616
        public static void GetDataServiceTOBE(Claro.Entity.AuditRequest audit, string customerId, string coId, ref string telefono, ref string internet, ref string cable)
        {
            Data.Dashboard.Postpaid.GetDataServiceTOBE(audit, customerId, coId, ref telefono, ref internet, ref cable);
        }
        //INICIATIVA 616

        //[INICIATIVA-616 | ONE FIJA - Postventa. Integración SIAC con CBIOS] KGC
        public static Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse GetProductosXCustomer(Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerRequest objRequest)
        {
            Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse objResponse = new Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<Entity.Dashboard.Postpaid.Legacy.GetProductosXCustomer.obtenerProductosXCustomerResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                { return Data.Dashboard.Postpaid.GetProductosXCustomer(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        //INICIATIVA 616
        public static List<Entity.Dashboard.Postpaid.Balance> GetBalanceFijaTobe(Claro.Entity.AuditRequest objAudit, string strMsidn, string coIdPub)
        {
            List<Entity.Dashboard.Postpaid.Balance> objResponse = new List<Entity.Dashboard.Postpaid.Balance>();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<List<Entity.Dashboard.Postpaid.Balance>>(objAudit.Session, objAudit.Transaction, () =>
                {
                    return Data.Dashboard.Postpaid.GetBalanceFijaTobe(objAudit, strMsidn, coIdPub);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.Session, objAudit.Transaction, ex.Message);
            }

            return objResponse;
        }

    }
}