using Claro.SIACU.Entity.Management.GetInstant;
using Claro.SIACU.Entity.Management.GetListInstant;
using System.ServiceModel;

namespace Claro.SIACU.Web.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICommonService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
        Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationResponse GetFileDefaultImpersonation(Claro.SIACU.Entity.File.GetFileDefaultImpersonation.FileDefaultImpersonationRequest objFileDefaultImpersonationRequest);
    }
}
