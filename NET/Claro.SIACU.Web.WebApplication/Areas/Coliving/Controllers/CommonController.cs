using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Web.WebApplication.ColivingService;
using Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.CustomerSearch;
using Claro.SIACU.Web.WebApplication.Areas.Coliving.Models.SalesCriteria;
using Claro.SIACU.Web.WebApplication.CommonService;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
namespace Claro.SIACU.Web.WebApplication.Areas.Coliving.Controllers
{
    public class CommonController : Controller
    {
        ColivingServiceClient oColivingService = new ColivingServiceClient();
        CommonServiceClient oCommonService = new CommonServiceClient();

        static List<ColivingParameters> lstColivingParameters = null;
        #region Services
        public CustomerInfoResponse GetCustomerInfoService(string strIdSession, SearchTypeModel objSearchType)
        {
            CustomerInfoResponse objCustomerInfoResponse = null;
            CustomerInfoRequest objCustomerInfoRequest = new CustomerInfoRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession),
                CustomerId = objSearchType.CustomerId,
                DocumentType = objSearchType.DocumentType,
                DocumentNumber = objSearchType.DocumentNumber,
                ServiceIdentifier = objSearchType.LineNumber
            };
            objCustomerInfoRequest.audit.ipAddress = App_Code.Common.GetApplicationIpServer();
            try
            {
                objCustomerInfoResponse = Claro.Web.Logging.ExecuteMethod<ColivingService.CustomerInfoResponse>(
                () => { return oColivingService.GetCustomerInfo(objCustomerInfoRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession: " + objCustomerInfoRequest.audit.Session, "Transaccion: " + objCustomerInfoRequest.audit.transaction, "(Exception) Error :" + ex.Message);
            }
            return objCustomerInfoResponse;
        }
        #endregion
        #region SIACC Parameters

        public JsonResult InitParametersColiving(string strIdSession)
        {
            var audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info("IdSession: " + audit.Session, "Transaccion: " + audit.transaction, "Begin a InitParametersColiving Capa WebApplication");
            List<ColivingParameters> listParameters = new List<ColivingParameters>();
            try
            {
                lstColivingParameters = Claro.Web.Logging.ExecuteMethod<List<ColivingParameters>>(() =>
                {
                    return oColivingService.GetColivingParameters(strIdSession, audit.transaction);
                });
                Claro.Web.Logging.Info("IdSession: " + audit.Session, "Transaccion: " + audit.transaction, "End a InitParametersColiving Capa WebApplication -> Parametro de salida: listParameters: " + lstColivingParameters != null ? lstColivingParameters.Count.ToString() : "null");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("IdSession: " + audit.Session, "Transaccion: " + audit.transaction, "(Exception) Error :" + ex.Message);
            }

            return Json(new { data = lstColivingParameters != null ? lstColivingParameters.Count.ToString() : null }, JsonRequestBehavior.AllowGet);

        }
        public List<ColivingParameters> GetSessionListParameters()
        {
            List<ColivingParameters> listado = new List<ColivingParameters>();
            if (lstColivingParameters != null && lstColivingParameters.Count > 0)
            {
                listado = lstColivingParameters;
            }
            else
            {
                InitParametersColiving("");
                listado = lstColivingParameters;
            }
            return listado;
        }

        public JsonResult getSearchType()
        {
            List<ItemGeneric> listSearchType = new List<ItemGeneric>();
            try
            {
                var strColiving = KEY.AppSettings("strKeyValueC_SiacParam").ToString();
                var intTipoBusqueda = int.Parse(KEY.AppSettings("strKeyValueN_SiacParam").Split('|')[6].Split(';')[1]);
                List<ColivingParameters> listParameters = GetSessionListParameters();

                ItemGeneric obj = null;
                foreach (var item in listParameters.Where(x => x.Value_C.ToUpper() == strColiving && x.Value_N == intTipoBusqueda))
                {
                    obj = new ItemGeneric();
                    obj.Code = item.Parameter_Id.ToString();
                    obj.Description = item.Name;
                    listSearchType.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("", "", "(Exception) Error :" + ex.Message);
            }
            return Json(new { data = listSearchType }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getDocumentType()
        {
            List<ItemGeneric> listDocumentType = new List<ItemGeneric>();
            try
            {
                var strColiving = KEY.AppSettings("strKeyValueC_SiacParam").ToString();
                var intTipoBusqueda = int.Parse(KEY.AppSettings("strKeyValueN_SiacParam").Split('|')[5].Split(';')[1]);
                var intDocTodos = int.Parse(KEY.AppSettings("strKeyDocDescTodos"));
                List<ColivingParameters> listParameters = GetSessionListParameters();

                ItemGeneric obj = null;
                listParameters = listParameters.Where(x => x.Value_C.ToUpper() == strColiving && x.Value_N == intTipoBusqueda && x.Parameter_Id != intDocTodos).ToList();
                foreach (var item in listParameters)
                {
                    obj = new ItemGeneric();
                    obj.Code = item.Parameter_Id.ToString();
                    obj.Description = item.Name;
                    listDocumentType.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("", "", "(Exception) Error :" + ex.Message);
            }
            return Json(new { data = listDocumentType }, JsonRequestBehavior.AllowGet);
        }
        public String getParameter(string parameter_id)
        {
            int parameter = int.Parse(parameter_id);
            List<ColivingParameters> listParameters = GetSessionListParameters();
            String description = listParameters.Where(x => x.Parameter_Id == parameter).Select(x => x.Description).FirstOrDefault();
            return description;
        }
        public JsonResult getOperationType(FilterParametersModel FilterParameters)
        {
            List<CustomerType> listCustomerType = new List<CustomerType>();
            List<ItemGeneric> listOperationType = new List<ItemGeneric>();
            ItemGeneric obj = null;
            if (FilterParameters.CustomerTypeId != null)
            {

                CriteriaSaleItems item = LoadCriteriasSale_Json();
                listCustomerType = item.ListModality.Where(x => x.Description == FilterParameters.Segment).Select(x => x.ListCustomerType.Where(y => y.Code == FilterParameters.CustomerTypeId)).FirstOrDefault().ToList();
                if (listCustomerType.Count() > 0)
                {
                    foreach (var x in listCustomerType.Select(z => z.ListOperationType.Where(za => za.Status == true).ToList()).FirstOrDefault())
                    {
                        obj = new ItemGeneric();
                        obj.Code = x.Code;
                        obj.Description = getParameter(x.Code);
                        listOperationType.Add(obj);
                    }

                }
            }
            return Json(new { data = listOperationType }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCustomerSubType(FilterParametersModel FilterParameters)
        {
            List<OperationType> listOperationType = new List<OperationType>();
            List<ItemGeneric> listCustomerSubType = new List<ItemGeneric>();
            ItemGeneric obj = null;

            CriteriaSaleItems item = LoadCriteriasSale_Json();
            listOperationType = item.ListModality.Where(x => x.Description == FilterParameters.Segment).Select(x => x.ListCustomerType.Where(y => y.Code == FilterParameters.CustomerTypeId)).First().ToList().Select(z => z.ListOperationType.Where(za => za.Status == true).ToList()).FirstOrDefault();
            if (listOperationType.Count() > 0)
            {
                foreach (var x in listOperationType.Where(zx => zx.Code == FilterParameters.OperationTypeId).Select(z => z.ListCustomerSubType.ToList()).FirstOrDefault())
                {
                    obj = new ItemGeneric();
                    obj.Code = x.Code;
                    obj.Description = getParameter(x.Code);
                    listCustomerSubType.Add(obj);
                }

            }
            return Json(new { data = listCustomerSubType }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getProductType(FilterParametersModel FilterParameters)
        {
            List<CustomerSubType> listCustomerSubType = new List<CustomerSubType>();
            List<ItemGeneric> listProductType = new List<ItemGeneric>();
            ItemGeneric obj = null;

            CriteriaSaleItems item = LoadCriteriasSale_Json();
            listCustomerSubType = item.ListModality.Where(x => x.Description == FilterParameters.Segment).Select(x => x.ListCustomerType.Where(y => y.Code == FilterParameters.CustomerTypeId)).First().ToList().Select(z => z.ListOperationType.Where(za => za.Status == true && za.Code == FilterParameters.OperationTypeId).ToList()).FirstOrDefault().Select(z => z.ListCustomerSubType.ToList()).FirstOrDefault();
            if (listCustomerSubType.Count() > 0)
            {
                foreach (var x in listCustomerSubType.Where(zx => zx.Code == FilterParameters.CustomerSubTypeId).Select(z => z.ListProductType.ToList()).FirstOrDefault())
                {
                    obj = new ItemGeneric();
                    obj.Code = x.Code;
                    obj.Description = getParameter(x.Code);
                    listProductType.Add(obj);
                }

            }
            return Json(new { data = listProductType }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCriteriaSaleUrl(string strIdSession, FilterParametersModel FilterParameters)
        {
            List<DocumentType> listDocumentType = new List<DocumentType>();
            MigrationStatus objMigration = null;
            if (FilterParameters != null)
            {
                var audit = App_Code.Common.CreateAuditRequest<ColivingService.AuditRequest>(strIdSession);
                Claro.Web.Logging.Info("IdSession: " + audit.Session, "Transaccion: " + audit.transaction, "Begin a GetCriteriaSaleUrl Capa WebApplication");
                try
                {
                    CriteriaSaleItems item = LoadCriteriasSale_Json();
                    var strDocTodos = KEY.AppSettings("strKeyDocDescTodos").ToString();
                    string documentId = FilterParameters.DocumentTypeId != null ? KEY.AppSettings("strkeyDocument").Split('|').ToList().Where(x => x.Split(';')[1] == FilterParameters.DocumentTypeId).Select(x => x.Split(';')[3]).FirstOrDefault() : "";
                    listDocumentType = item.ListModality.Where(x => x.Description == FilterParameters.Segment).Select(x => x.ListCustomerType.Where(y => y.Code == FilterParameters.CustomerTypeId)).First().ToList()
                        .Select(z => z.ListOperationType.Where(za => za.Status == true && za.Code == FilterParameters.OperationTypeId).ToList()).FirstOrDefault()
                        .Select(z => z.ListCustomerSubType.Where(zb => zb.Code == FilterParameters.CustomerSubTypeId).ToList()).FirstOrDefault()
                        .Select(zc => zc.ListProductType.Where(zd => zd.Code == FilterParameters.ProductTypeId).ToList()).FirstOrDefault()
                        .Select(ze => ze.ListDocumentType.First().Code == strDocTodos ? ze.ListDocumentType.ToList() : ze.ListDocumentType.Where(zf => zf.Code == documentId).ToList()).FirstOrDefault();
                    if (listDocumentType.Count > 0)
                    {
                        foreach (var x in listDocumentType.Select(x => x.MigrationStatus.Where(y => y.Code == FilterParameters.MigrationStatusId)).FirstOrDefault())
                        {
                            objMigration = new MigrationStatus();
                            objMigration.Code = x.Code;
                            objMigration.Description = x.Description;
                            objMigration.CodePage = x.CodePage;
                            objMigration.Page = x.Page;
                            objMigration.Url = getParameter(objMigration.CodePage);
                            break;
                        }
                    }
                    Claro.Web.Logging.Info("IdSession: " + audit.Session, "Transaccion: " + audit.transaction, string.Format("End a GetCriteriaSaleUrl Capa WebApplication -> Parametro de salida: objMigration Page: {0}", objMigration != null ? objMigration.Page : null));
                    Claro.Web.Logging.Info("IdSession: " + audit.Session, "Transaccion: " + audit.transaction, string.Format("End a GetCriteriaSaleUrl Capa WebApplication -> Parametro de salida: objMigration URL: {0}", objMigration != null ? objMigration.Url : null));
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(audit.Session, audit.transaction, "Error al obtener la Url para el redireccionamiento de los Criterios de Venta: " + ex.Message);
                }

            }
            return Json(new { data = objMigration }, JsonRequestBehavior.AllowGet);
        }

        public CriteriaSaleItems LoadCriteriasSale_Json()
        {
            CriteriaSaleItems items = null;
            try
            {
                string file = KEY.AppSettings("strKeyFileConfigurable");
                string strApplicationPath;
                strApplicationPath = new Claro.Utils().GetApplicationPath();
                string fullPath = strApplicationPath + file;
                using (StreamReader r = new StreamReader(fullPath))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<CriteriaSaleItems>(json);
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error("", "", "Exception: " + ex.Message);
            }
            return items;
        }
        #endregion

    }
}