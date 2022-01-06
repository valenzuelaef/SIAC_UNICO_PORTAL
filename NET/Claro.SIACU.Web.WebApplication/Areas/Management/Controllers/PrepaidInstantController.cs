using Claro.SIACU.Web.WebApplication.Areas.Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.Areas.Management.Controllers
{
    public class PrepaidInstantController : Controller
    {
        readonly Models.InstantModel objDataInstantModel = new Models.InstantModel();
        readonly Helpers.Instant.DataInstant objDataInstant = new Helpers.Instant.DataInstant();

        public ActionResult Index(string strIdSession, string strPhone, string strOriginType)
        {
            objDataInstantModel.OriginType = strOriginType;
            return View(objDataInstantModel);
        }

        public ActionResult DetailInstant(string strIdSession, string strTelephoneCustomer, string strDateFrom, string strOriginType)
        {
            string strtype = "";
            if (strOriginType == Claro.SIACU.Constants.PrepaidMajuscule)
                strtype = Claro.Constants.NumberTenString;
            if (strOriginType == Claro.SIACU.Constants.PostpaidMajuscule)
                strtype = Claro.Constants.NumberTwelveString;

            int intRegistros = Claro.Constants.NumberZero;
            List<Helpers.Instant.DataInstant> listInstant;

            CommonService.ListInstantResponseManagement objListInstantResponseManagement;
            CommonService.ListInstantRequestManagement objListInstantRequestManagement = new CommonService.ListInstantRequestManagement
            {
                fecha = strDateFrom,
                vDato = strTelephoneCustomer,
                vTipoTelefono = strtype,


                audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession)
            };

            try
            {
                objListInstantResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.ListInstantResponseManagement>(() => { return new CommonService.CommonServiceClient().ListInstant(objListInstantRequestManagement); });
            }
            catch (Exception ex)
            {
                objListInstantResponseManagement = null;
                Claro.Web.Logging.Error(strIdSession, objListInstantRequestManagement.audit.transaction, ex.Message);
                throw new Claro.MessageException(objListInstantRequestManagement.audit.transaction);
            }
            listInstant = new List<Helpers.Instant.DataInstant>();
            if (objListInstantResponseManagement.listInstant != null)
            {
                foreach (CommonService.Instant item in objListInstantResponseManagement.listInstant)
                {
                    listInstant.Add(new Helpers.Instant.DataInstant()
                    {
                        IdInstant = item.ID_INSTANTANEA,
                        Validity = item.VIGENCIA,
                        Description = item.DESCRIPCION,
                        DateValidityStart = item.FECHA_VIGENCIA_INICIO,
                        DateValidityEnd = item.FECHA_VIGENCIA_FIN,
                        Color = item.COLOR,
                    });
                    intRegistros++;

                }
            }
            InstantModel _objDataInstantModel = new InstantModel()
            {

                listInstant = listInstant,
                NumberRegisters = intRegistros.ToString(),
            };

            return View(_objDataInstantModel);

        }

        public ActionResult ManagementInstant(string strIdSession, string strTelephoneCustomer, string stridInstant, string strOption)
        {

            CommonService.InstantByIdResponseManagement objInstantByIdResponseManagement = null;


            if (strOption == Claro.Constants.LetterN)
            {

                objDataInstant.Description = "";
                objDataInstant.IdInstant = 0;
                objDataInstant.DateValidityStart = DateTime.Now;
                objDataInstant.DateValidityEnd = DateTime.Now;
                objDataInstant.Color = "";
                objDataInstantModel.StartHour = "";
                objDataInstantModel.StartMinutes = "";
                objDataInstantModel.StartType = "";
                objDataInstantModel.EndHour = "";
                objDataInstantModel.EndMinutes = "";
                objDataInstantModel.EndType = "";
                objDataInstantModel.EndType = "";



                objDataInstantModel.objInstant = objDataInstant;
                objDataInstantModel.option = strOption;
                return View(objDataInstantModel);

            } if (strOption == Claro.Constants.LetterU || strOption == Claro.Constants.LetterD)
            {



                CommonService.InstantByIdRequestManagement objInstantByIdRequestManagement = new CommonService.InstantByIdRequestManagement()
                {

                    IdInstant = Int32.Parse(stridInstant),

                    audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession)
                }; try
                {
                    objInstantByIdResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.InstantByIdResponseManagement>(() => { return new CommonService.CommonServiceClient().GetinstantById(objInstantByIdRequestManagement); });
                }
                catch (Exception ex)
                {
                    objInstantByIdResponseManagement = null;
                    Claro.Web.Logging.Error(strIdSession, objInstantByIdRequestManagement.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objInstantByIdRequestManagement.audit.transaction);
                }

                objDataInstant.Description = objInstantByIdResponseManagement.instant.DESCRIPCION;
                objDataInstant.IdInstant = objInstantByIdResponseManagement.instant.ID_INSTANTANEA;
                objDataInstant.DateValidityStart = objInstantByIdResponseManagement.instant.FECHA_VIGENCIA_INICIO;
                objDataInstant.DateValidityEnd = objInstantByIdResponseManagement.instant.FECHA_VIGENCIA_FIN;
                objDataInstant.Color = objInstantByIdResponseManagement.instant.COLOR;
                objDataInstantModel.StartHour = String.Format("{0:hh}", objDataInstant.DateValidityStart);
                objDataInstantModel.StartMinutes = String.Format("{0:mm}", objDataInstant.DateValidityStart);
                objDataInstantModel.StartType = String.Format("{0:tt}", objDataInstant.DateValidityStart);
                objDataInstantModel.StartType = objDataInstantModel.StartType.Replace(Claro.SIACU.Constants.Point, "");
                objDataInstantModel.EndHour = String.Format("{0:hh}", objInstantByIdResponseManagement.instant.FECHA_VIGENCIA_FIN);
                objDataInstantModel.EndMinutes = String.Format("{0:mm}", objInstantByIdResponseManagement.instant.FECHA_VIGENCIA_FIN);
                objDataInstantModel.EndType = String.Format("{0:tt}", objInstantByIdResponseManagement.instant.FECHA_VIGENCIA_FIN);
                objDataInstantModel.EndType = objDataInstantModel.EndType.Replace(Claro.SIACU.Constants.Point, "");




                objDataInstantModel.objInstant = objDataInstant;
                objDataInstantModel.option = strOption;

                return View(objDataInstantModel);
            }
            return View();

        }

        public ActionResult MessageInstant(string strIdSession, string strTelephoneCustomer, string stridInstant, string strOption, string strcomment, string strDateValidityStart, string strDateValidityEnd, string strColour, string strbusqInstant, string strcontactoId)
        {

            string account = "";
            if (strbusqInstant == "Telefono")
            {
                account = "";
            }
            else if (strbusqInstant == "Cuenta")
            {
                account = strcontactoId;
            }
           
            CommonService.Instant objInstant = new CommonService.Instant()
            {
                FECHA_VIGENCIA_INICIO = System.Convert.ToDateTime(strDateValidityStart),
                FECHA_VIGENCIA_FIN = System.Convert.ToDateTime(strDateValidityEnd),
                DESCRIPCION = strcomment,
                DATO = strTelephoneCustomer,
                TIPOTELEFONO = Claro.Constants.NumberTenString,
                LOGIN = Environment.UserName,
                ID_INSTANTANEA = Convert.ToInt(stridInstant),
                COLOR = strColour,
                CUENTA=account,

            };

            CommonService.InstantResponseManagement objInstantResponseManagement = null;
            if (strOption == Claro.Constants.LetterN || strOption == Claro.Constants.LetterU || strOption == Claro.Constants.LetterD)
            {
                CommonService.InstantRequestManagement objInstantRequestManagement = new CommonService.InstantRequestManagement()
                {
                    instant = objInstant,
                    audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession)
                }; try
                {
                    if (strOption == Claro.Constants.LetterN)
                    {
                        objInstantResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.InstantResponseManagement>(() => { return new CommonService.CommonServiceClient().InsertInstant(objInstantRequestManagement); });
                    }
                    else if (strOption == Claro.Constants.LetterU)
                    {
                        objInstantResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.InstantResponseManagement>(() => { return new CommonService.CommonServiceClient().UpdateInstant(objInstantRequestManagement); });
                    }
                    else if (strOption == Claro.Constants.LetterD)
                    {
                        objInstantResponseManagement = Claro.Web.Logging.ExecuteMethod<CommonService.InstantResponseManagement>(() => { return new CommonService.CommonServiceClient().DeactivateInstant(objInstantRequestManagement); });
                    }
                    objDataInstantModel.MessageResult = objInstantResponseManagement.message;
                    objDataInstantModel.ResultFlag = objInstantResponseManagement.result;
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, objInstantRequestManagement.audit.transaction, ex.Message);
                    throw new Claro.MessageException(objInstantRequestManagement.audit.transaction);
                }

            }
            return View(objDataInstantModel);
        }


    }
}
