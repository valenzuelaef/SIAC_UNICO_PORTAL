using System.Collections.Generic;

namespace Claro.SIACU.Entity.Cases.GetQueuesAll
{
    public class QueuesAllResponse
    {
        public IEnumerable<Queue> ListQueue { get; set; }
    }
}