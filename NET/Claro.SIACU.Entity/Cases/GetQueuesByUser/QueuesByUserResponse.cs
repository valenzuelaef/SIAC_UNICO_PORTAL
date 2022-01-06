using System.Collections.Generic;

namespace Claro.SIACU.Entity.Cases.GetQueuesByUser
{
    public class QueuesByUserResponse
    {
        public IEnumerable<Queue> ListQueue { get; set; }
    }
}