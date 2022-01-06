using System;
using System.ServiceModel;

namespace Claro.SIACU.Web.Service.Cases
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WipBinService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WipBinService.svc or WipBinService.svc.cs at the Solution Explorer and start debugging.
    public class WipBinService : IWipBinService
    {
       

        public Claro.SIACU.Entity.Cases.GetWipBinListByUser.WipBinListByUserResponse GetWipBinListByUser(Entity.Cases.GetWipBinListByUser.WipBinListByUserRequest objWipBinListByUserRequest)
        {
            try
            {
                Claro.SIACU.Entity.Cases.GetWipBinListByUser.WipBinListByUserResponse objWipBinListByUserResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Cases.GetWipBinListByUser.WipBinListByUserResponse>(() =>
                {
                    return Business.Cases.WipBin.GetWipBinListByUser(objWipBinListByUserRequest.Audit.Session, objWipBinListByUserRequest.Audit.Transaction, objWipBinListByUserRequest.Audit.UserName);
                });

                return objWipBinListByUserResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objWipBinListByUserRequest.Audit.Session, objWipBinListByUserRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
        }

        public Claro.SIACU.Entity.Cases.GetCasesByWipBin.CasesByWipBinResponse GetCasesByWipBin(Entity.Cases.GetCasesByWipBin.CasesByWipBinRequest objCasesByWipBinRequest)
        {
            try
            {
                Claro.SIACU.Entity.Cases.GetCasesByWipBin.CasesByWipBinResponse objCasesByWipBinResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Cases.GetCasesByWipBin.CasesByWipBinResponse>(() =>
                {
                    return Business.Cases.WipBin.GetCasesByWipBin(objCasesByWipBinRequest.Audit.Session,
                        objCasesByWipBinRequest.Audit.Transaction, objCasesByWipBinRequest.Audit.UserName, objCasesByWipBinRequest.WipBinName);
                });

                return objCasesByWipBinResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCasesByWipBinRequest.Audit.Session, objCasesByWipBinRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
        }
        
        public Entity.Cases.GetQueuesAll.QueuesAllResponse GetQueuesAll(Entity.Cases.GetQueuesAll.QueuesAllRequest objQueuesAllRequest)
        {
            try
            {
                Claro.SIACU.Entity.Cases.GetQueuesAll.QueuesAllResponse objQueuesAllResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Cases.GetQueuesAll.QueuesAllResponse>(() =>
               {
                   return Business.Cases.Queue.GetQueuesAll(objQueuesAllRequest.Audit.Session,
                       objQueuesAllRequest.Audit.Transaction, objQueuesAllRequest.Audit.UserName, objQueuesAllRequest.FlagWorkGroup);
               });

                return objQueuesAllResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objQueuesAllRequest.Audit.Session, objQueuesAllRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
        }

        public Entity.Cases.GetQueuesByUser.QueuesByUserResponse GetQueuesByUser(Entity.Cases.GetQueuesByUser.QueuesByUserRequest objQueuesByUserRequest)
        {
            try
            {
                Claro.SIACU.Entity.Cases.GetQueuesByUser.QueuesByUserResponse objQueuesByUserResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Cases.GetQueuesByUser.QueuesByUserResponse>(() =>
                {
                    return Business.Cases.Queue.GetQueuesByUser(objQueuesByUserRequest.Audit.Session, objQueuesByUserRequest.Audit.Transaction,
                        objQueuesByUserRequest.Audit.UserName);
                });
                return objQueuesByUserResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objQueuesByUserRequest.Audit.Session, objQueuesByUserRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
        }

        public Entity.Cases.GetQueues.QueuesResponse GetQueues(Entity.Cases.GetQueues.QueuesRequest objQueuesRequest)
        {
            try
            {
                Entity.Cases.GetQueues.QueuesResponse objQueuesResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Cases.GetQueues.QueuesResponse>(() =>
                {
                    return Business.Cases.Queue.GetQueues(objQueuesRequest.Audit.Session, objQueuesRequest.Audit.Transaction,
                        objQueuesRequest.Audit.UserName, objQueuesRequest.FlagWorkGroup);
                });

                return objQueuesResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objQueuesRequest.Audit.Session, objQueuesRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
        }

        public Entity.Cases.GetCasesByQueueUser.CasesByQueueUserResponse GetCasesByQueueUser(Entity.Cases.GetCasesByQueueUser.CasesByQueueUserRequest objCasesByQueueUserRequest)
        {
            try
            {
                Entity.Cases.GetCasesByQueueUser.CasesByQueueUserResponse objCasesByQueueUserResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Cases.GetCasesByQueueUser.CasesByQueueUserResponse>(() =>
                {
                    return Business.Cases.Queue.GetCasesByQueueUser(objCasesByQueueUserRequest.Audit.Session, objCasesByQueueUserRequest.Audit.Transaction,
                        objCasesByQueueUserRequest.Audit.UserName, objCasesByQueueUserRequest.Queue);
                });

                return objCasesByQueueUserResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCasesByQueueUserRequest.Audit.Session, objCasesByQueueUserRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
        }

        public Entity.Cases.GetCaseNotes.CaseNotesResponse GetCaseNotes(Entity.Cases.GetCaseNotes.CaseNotesRequest objCaseNotesRequest)
        {
            try
            {
                Entity.Cases.GetCaseNotes.CaseNotesResponse objCaseNotesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Cases.GetCaseNotes.CaseNotesResponse>(() =>
                {
                    return Business.Cases.Shared.GetCaseNotes(objCaseNotesRequest.Audit.Session, objCaseNotesRequest.Audit.Transaction,
                        objCaseNotesRequest.CaseId);
                });

                return objCaseNotesResponse;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCaseNotesRequest.Audit.Session, objCaseNotesRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
        }

        public Entity.Cases.GetSubCaseNotes.SubCaseNotesResponse GetSubCaseNotes(Entity.Cases.GetSubCaseNotes.SubCaseNotesRequest objSubCaseNotesRequest)
        {
            throw new NotImplementedException();
        }

        public Entity.Cases.GetCasesById.CasesByIdResponse GetCasesById(Entity.Cases.GetCasesById.CasesByIdRequest objCasesByIdRequest)
        {
            try
            {
                Claro.SIACU.Entity.Cases.GetCasesById.CasesByIdResponse objCasesByIdResponse = Claro.Web.Logging.ExecuteMethod<Entity.Cases.GetCasesById.CasesByIdResponse>(() =>
                {
                    return Business.Cases.Search.GetCasesByQueueUser(objCasesByIdRequest.Audit.Session, objCasesByIdRequest.Audit.Transaction,
                        objCasesByIdRequest.CaseId, objCasesByIdRequest.ReportId, objCasesByIdRequest.ComplaintId);
                });

                return objCasesByIdResponse;
            } 
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCasesByIdRequest.Audit.Session, objCasesByIdRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }
        }
    }
}