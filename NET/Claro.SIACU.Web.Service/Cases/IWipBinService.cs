using System.ServiceModel;

namespace Claro.SIACU.Web.Service.Cases
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWipBinService" in both code and config file together.
    [ServiceContract]
    public interface IWipBinService
    {
        [OperationContract]
        Claro.SIACU.Entity.Cases.GetWipBinListByUser.WipBinListByUserResponse GetWipBinListByUser(Claro.SIACU.Entity.Cases.GetWipBinListByUser.WipBinListByUserRequest objWipBinListByUserRequest);

        [OperationContract]
        Claro.SIACU.Entity.Cases.GetCasesByWipBin.CasesByWipBinResponse GetCasesByWipBin(Claro.SIACU.Entity.Cases.GetCasesByWipBin.CasesByWipBinRequest objCasesByWipBinRequest);

        [OperationContract]
        Claro.SIACU.Entity.Cases.GetQueuesAll.QueuesAllResponse GetQueuesAll(Claro.SIACU.Entity.Cases.GetQueuesAll.QueuesAllRequest objQueuesAllRequest);

        [OperationContract]
        Claro.SIACU.Entity.Cases.GetQueuesByUser.QueuesByUserResponse GetQueuesByUser(Claro.SIACU.Entity.Cases.GetQueuesByUser.QueuesByUserRequest objQueuesByUserRequest);

        [OperationContract]
        Claro.SIACU.Entity.Cases.GetQueues.QueuesResponse GetQueues(Claro.SIACU.Entity.Cases.GetQueues.QueuesRequest objQueuesRequest);

        [OperationContract]
        Claro.SIACU.Entity.Cases.GetCasesByQueueUser.CasesByQueueUserResponse GetCasesByQueueUser(Claro.SIACU.Entity.Cases.GetCasesByQueueUser.CasesByQueueUserRequest objCasesByQueueUserRequest);
   
        [OperationContract]
        Claro.SIACU.Entity.Cases.GetCaseNotes.CaseNotesResponse GetCaseNotes(Claro.SIACU.Entity.Cases.GetCaseNotes.CaseNotesRequest objCaseNotesRequest);

        [OperationContract]
        Claro.SIACU.Entity.Cases.GetSubCaseNotes.SubCaseNotesResponse GetSubCaseNotes(Claro.SIACU.Entity.Cases.GetSubCaseNotes.SubCaseNotesRequest objSubCaseNotesRequest);

        [OperationContract]
        Claro.SIACU.Entity.Cases.GetCasesById.CasesByIdResponse GetCasesById(Claro.SIACU.Entity.Cases.GetCasesById.CasesByIdRequest objCasesByIdRequest);
    }
}