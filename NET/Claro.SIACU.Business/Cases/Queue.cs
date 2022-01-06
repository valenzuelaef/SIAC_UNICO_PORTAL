using Claro.SIACU.Entity.Cases.GetCasesByQueueUser;
using Claro.SIACU.Entity.Cases.GetQueues;
using Claro.SIACU.Entity.Cases.GetQueuesByUser;

namespace Claro.SIACU.Business.Cases
{
    public static class Queue
    {
        public static Claro.SIACU.Entity.Cases.GetQueuesAll.QueuesAllResponse GetQueuesAll(string strIdSession, string strTransaction, string strUser, string strFlagWorkGroup)
        {
            return new Entity.Cases.GetQueuesAll.QueuesAllResponse()
            {
                ListQueue = Claro.SIACU.Data.Cases.Common.GetQueuesAll(strIdSession, strTransaction, strUser, strFlagWorkGroup),
            };
        }

        public static QueuesByUserResponse GetQueuesByUser(string strIdSession, string strTransaction, string strUser)
        {
            return new Entity.Cases.GetQueuesByUser.QueuesByUserResponse()
            {
                ListQueue = Claro.SIACU.Data.Cases.Common.GetQueuesByUser(strIdSession, strTransaction, strUser)
            };
        }

        public static QueuesResponse GetQueues(string strIdSession, string strTransaction, string strUser, string strFlagWorkGroup)
        {
            return new Entity.Cases.GetQueues.QueuesResponse()
            {
                ListQueuesUser = Claro.SIACU.Data.Cases.Common.GetQueuesByUser(strIdSession, strTransaction, strUser),
                ListQueuesAll = Claro.SIACU.Data.Cases.Common.GetQueuesAll(strIdSession, strTransaction, strUser, strFlagWorkGroup),
            };
        }

        public static CasesByQueueUserResponse GetCasesByQueueUser(string strIdSession, string strTransaction, string strUser, string strQueue)
        {
            return new Entity.Cases.GetCasesByQueueUser.CasesByQueueUserResponse()
            {
                Cases = Claro.SIACU.Data.Cases.Common.GetCasesByQueueUser(strIdSession, strTransaction, strUser, strQueue),
            };
        }
    }
}