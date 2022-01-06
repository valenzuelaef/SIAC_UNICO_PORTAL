using Claro.SIACU.Entity.Management.GetInstant;
using Claro.SIACU.Entity.Management.GetListInstant;
using System;
using System.ServiceModel;

namespace Claro.SIACU.Web.Service
{
    public class FileService : IFileService
    {
        public Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse GetFileDefaultImpersonation(Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationRequest objFileDefaultImpersonationRequest)
        {
            Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse objFileDefaultImpersonationResponse = null;

            try
            {
                objFileDefaultImpersonationResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse>(1,() => { return Business.Dashboard.Dashboard.GetFileDefaultImpersonation(objFileDefaultImpersonationRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objFileDefaultImpersonationRequest.Audit.Session, objFileDefaultImpersonationRequest.Audit.Transaction, ex.Message);
                throw new FaultException(ex.Message);
            }

            return objFileDefaultImpersonationResponse;
        }
    }
}
