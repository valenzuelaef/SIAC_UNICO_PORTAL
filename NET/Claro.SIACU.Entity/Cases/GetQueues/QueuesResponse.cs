using System.Collections.Generic;

namespace Claro.SIACU.Entity.Cases.GetQueues
{
    public class QueuesResponse
    {
        public IEnumerable<Queue> ListQueuesAll { get; set; }
        public IEnumerable<Queue> ListQueuesUser  { get; set; }
    }
}