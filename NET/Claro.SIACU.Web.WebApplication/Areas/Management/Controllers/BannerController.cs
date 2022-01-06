using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.Areas.Management.Controllers
{
    public class BannerController : Controller
    {
        readonly CommonService.CommonServiceClient objCommonService = new CommonService.CommonServiceClient();

        public ActionResult BannerList()
        {
            return View();
        }

        public JsonResult GetBanner(string strIdSession, DateTime date, string status)
        {
            CommonService.BannerRequestManagement objBannerRequestManagement = new CommonService.BannerRequestManagement();
            CommonService.BannerResponseManagement objBannerResponseManagement = null;
            objBannerRequestManagement.Date = date;
            objBannerRequestManagement.Status = status;
            objBannerRequestManagement.audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);

            try
            {
                objBannerResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.BannerResponseManagement>(() => { return objCommonService.GetBanner(objBannerRequestManagement); });

            }
            catch (Exception ex)
            {
                objBannerResponseManagement = null;
                Claro.Web.Logging.Error(strIdSession, objBannerRequestManagement.audit.transaction, ex.Message);
            }

            Areas.Management.Helpers.BannerHelper.Banner objBanner = null;
            List<Areas.Management.Helpers.BannerHelper.Banner> listBanner = new List<Areas.Management.Helpers.BannerHelper.Banner>();
            Areas.Management.Models.BannerModel objBannerModel = new Areas.Management.Models.BannerModel();

            if (objBannerResponseManagement != null)
            {
                foreach (CommonService.BannerManagement item in objBannerResponseManagement.ListBanner)
                {
                    objBanner = new Areas.Management.Helpers.BannerHelper.Banner();
                    objBanner.idBanner = item.ID_BANNER;
                    objBanner.message = item.MENSAJE;
                    objBanner.priorityOrder = item.ORDEN_PRIORIDAD;
                    objBanner.dateValidityStart = item.FECHA_VIGENCIA_INICIO;
                    objBanner.dateValidityEnd = item.FECHA_VIGENCIA_FIN;
                    objBanner.status = item.ESTADO;
                    objBanner.loginRegistry = item.LOGIN_REGISTRO;
                    objBanner.loginModification = item.LOGIN_MODIFICACION;
                    listBanner.Add(objBanner);
                }
                objBannerModel.salida = listBanner;
            }
            return Json(new { data = objBannerModel }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult GetCreate(string strIdSession, int ID_BANNER, string MENSAJE, DateTime FECHA_VIGENCIA_INICIO, DateTime FECHA_VIGENCIA_FIN, int ORDEN_PRIORIDAD, DateTime DATE, string STATUS)
        {
            CommonService.CreateRequestManagement objCreateRequestManagement = new CommonService.CreateRequestManagement();
            CommonService.CreateResponseManagement objCreateResponseManagement = null;
            objCreateRequestManagement.LOGIN = App_Code.Common.CurrentUser;
            objCreateRequestManagement.ID_BANNER = ID_BANNER;
            objCreateRequestManagement.MENSAJE = MENSAJE;
            objCreateRequestManagement.FECHA_VIGENCIA_INICIO = FECHA_VIGENCIA_INICIO;
            objCreateRequestManagement.FECHA_VIGENCIA_FIN = FECHA_VIGENCIA_FIN;
            objCreateRequestManagement.ORDEN_PRIORIDAD = ORDEN_PRIORIDAD;
            objCreateRequestManagement.DATE = DATE;
            objCreateRequestManagement.STATUS = STATUS;
            objCreateRequestManagement.audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);

            try
            {
                objCreateResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.CreateResponseManagement>(() => { return objCommonService.GetCreate(objCreateRequestManagement); });

            }
            catch (Exception ex)
            {
                objCreateResponseManagement = null;
                Claro.Web.Logging.Error(strIdSession, objCreateRequestManagement.audit.transaction, ex.Message);
            }

            Areas.Management.Helpers.BannerHelper.Banner objBanner = null;
            List<Areas.Management.Helpers.BannerHelper.Banner> listBanner = new List<Areas.Management.Helpers.BannerHelper.Banner>();
            Areas.Management.Models.BannerModel objBannerModel = new Areas.Management.Models.BannerModel();

            if (objCreateResponseManagement != null)
            {
                foreach (CommonService.BannerManagement item in objCreateResponseManagement.ListBanner)
                {
                    objBanner = new Areas.Management.Helpers.BannerHelper.Banner();
                    objBanner.idBanner = item.ID_BANNER;
                    objBanner.message = item.MENSAJE;
                    objBanner.priorityOrder = item.ORDEN_PRIORIDAD;
                    objBanner.dateValidityStart = item.FECHA_VIGENCIA_INICIO;
                    objBanner.dateValidityEnd = item.FECHA_VIGENCIA_FIN;
                    objBanner.status = item.ESTADO;
                    objBanner.loginRegistry = item.LOGIN_REGISTRO;
                    objBanner.loginModification = item.LOGIN_MODIFICACION;
                    listBanner.Add(objBanner);
                }
                objBannerModel.salida = listBanner;
            }
            return Json(new { data = objBannerModel }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Modify()
        {
            return View();
        }

        public JsonResult GetBannerId(string strIdSession, int intIdBanner)
        {
            CommonService.BannerIdRequestManagement objBannerIdRequestManagement = new CommonService.BannerIdRequestManagement();
            CommonService.BannerIdResponseManagement objBannerIdResponseManagement = null;
            objBannerIdRequestManagement.ID_BANNER = intIdBanner;
            objBannerIdRequestManagement.audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);

            try
            {
                objBannerIdResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.BannerIdResponseManagement>(() => { return objCommonService.GetBannerId(objBannerIdRequestManagement); });

            }
            catch (Exception ex)
            {
                objBannerIdResponseManagement = null;
                Claro.Web.Logging.Error(strIdSession, objBannerIdRequestManagement.audit.transaction, ex.Message);
            }
            Areas.Management.Helpers.BannerHelper.Banner objBanner = null;
            Areas.Management.Models.BannerModel objBannerModel = new Areas.Management.Models.BannerModel();

            if (objBannerIdResponseManagement != null)
            {

                CommonService.BannerManagement oBanner = objBannerIdResponseManagement.Banner;
                objBanner = new Helpers.BannerHelper.Banner()
                {

                    idBanner = oBanner.ID_BANNER,
                    message = oBanner.MENSAJE,
                    priorityOrder = oBanner.ORDEN_PRIORIDAD,
                    dateValidityStart = oBanner.FECHA_VIGENCIA_INICIO,
                    dateValidityEnd = oBanner.FECHA_VIGENCIA_FIN,
                    status = oBanner.ESTADO,
                    loginRegistry = oBanner.LOGIN_REGISTRO,
                    loginModification = oBanner.LOGIN_MODIFICACION,
                };
                objBannerModel.Banner = objBanner;

            }
            return Json(new { data = objBannerModel }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetModify(string strIdSession, int ID_BANNER, string MENSAJE, DateTime FECHA_VIGENCIA_INICIO, DateTime FECHA_VIGENCIA_FIN, int ORDEN_PRIORIDAD, DateTime DATE, string STATUS)
        {
            CommonService.ModifyRequestManagement objModifyRequestManagement = new CommonService.ModifyRequestManagement();
            CommonService.ModifyResponseManagement objModifyResponseManagement = null;
            objModifyRequestManagement.LOGIN = App_Code.Common.CurrentUser;
            objModifyRequestManagement.ID_BANNER = ID_BANNER;
            objModifyRequestManagement.MENSAJE = MENSAJE;
            objModifyRequestManagement.FECHA_VIGENCIA_INICIO = FECHA_VIGENCIA_INICIO;
            objModifyRequestManagement.FECHA_VIGENCIA_FIN = FECHA_VIGENCIA_FIN;
            objModifyRequestManagement.ORDEN_PRIORIDAD = ORDEN_PRIORIDAD;
            objModifyRequestManagement.DATE = DATE;
            objModifyRequestManagement.STATUS = STATUS;
            objModifyRequestManagement.audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);

            try
            {
                objModifyResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.ModifyResponseManagement>(() => { return objCommonService.GetModify(objModifyRequestManagement); });

            }
            catch (Exception ex)
            {
                objModifyResponseManagement = null;
                Claro.Web.Logging.Error(strIdSession, objModifyRequestManagement.audit.transaction, ex.Message);
            }

            Areas.Management.Helpers.BannerHelper.Banner objBanner = null;
            List<Areas.Management.Helpers.BannerHelper.Banner> listBanner = new List<Areas.Management.Helpers.BannerHelper.Banner>();
            Areas.Management.Models.BannerModel objBannerModel = new Areas.Management.Models.BannerModel();

            if (objModifyResponseManagement != null)
            {
                foreach (CommonService.BannerManagement item in objModifyResponseManagement.ListBanner)
                {
                    objBanner = new Areas.Management.Helpers.BannerHelper.Banner();
                    objBanner.idBanner = item.ID_BANNER;
                    objBanner.message = item.MENSAJE;
                    objBanner.priorityOrder = item.ORDEN_PRIORIDAD;
                    objBanner.dateValidityStart = item.FECHA_VIGENCIA_INICIO;
                    objBanner.dateValidityEnd = item.FECHA_VIGENCIA_FIN;
                    objBanner.status = item.ESTADO;
                    objBanner.loginRegistry = item.LOGIN_REGISTRO;
                    objBanner.loginModification = item.LOGIN_MODIFICACION;
                    listBanner.Add(objBanner);
                }
                objBannerModel.salida = listBanner;
            }
            return Json(new { data = objBannerModel }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Deactivate()
        {
            return View();
        }

        public JsonResult GetDeactivate(string strIdSession, int ID_BANNER, DateTime DATE, string STATUS)
        {
            CommonService.DeactivateRequestManagement objDeactivateRequestManagement = new CommonService.DeactivateRequestManagement();
            CommonService.DeactivateResponseManagement objDeactivateResponseManagement = null;
            objDeactivateRequestManagement.LOGIN = App_Code.Common.CurrentUser;
            objDeactivateRequestManagement.ID_BANNER = ID_BANNER;
            objDeactivateRequestManagement.DATE = DATE;
            objDeactivateRequestManagement.STATUS = STATUS;
            objDeactivateRequestManagement.audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);

            try
            {
                objDeactivateResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.DeactivateResponseManagement>(() => { return objCommonService.GetDeactivate(objDeactivateRequestManagement); });

            }
            catch (Exception ex)
            {
                objDeactivateResponseManagement = null;
                Claro.Web.Logging.Error(strIdSession, objDeactivateRequestManagement.audit.transaction, ex.Message);
            }

            Areas.Management.Helpers.BannerHelper.Banner objBanner = null;
            List<Areas.Management.Helpers.BannerHelper.Banner> listBanner = new List<Areas.Management.Helpers.BannerHelper.Banner>();
            Areas.Management.Models.BannerModel objBannerModel = new Areas.Management.Models.BannerModel();

            if (objDeactivateResponseManagement != null)
            {
                foreach (CommonService.BannerManagement item in objDeactivateResponseManagement.ListBanner)
                {
                    objBanner = new Areas.Management.Helpers.BannerHelper.Banner();
                    objBanner.idBanner = item.ID_BANNER;
                    objBanner.message = item.MENSAJE;
                    objBanner.priorityOrder = item.ORDEN_PRIORIDAD;
                    objBanner.dateValidityStart = item.FECHA_VIGENCIA_INICIO;
                    objBanner.dateValidityEnd = item.FECHA_VIGENCIA_FIN;
                    objBanner.status = item.ESTADO;
                    objBanner.loginRegistry = item.LOGIN_REGISTRO;
                    objBanner.loginModification = item.LOGIN_MODIFICACION;
                    listBanner.Add(objBanner);
                }
                objBannerModel.salida = listBanner;
            }
            return Json(new { data = objBannerModel }, JsonRequestBehavior.AllowGet);
        }
    }
}