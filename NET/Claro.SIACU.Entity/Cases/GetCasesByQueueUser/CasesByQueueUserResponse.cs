using System.Collections.Generic;

namespace Claro.SIACU.Entity.Cases.GetCasesByQueueUser
{
    public class CasesByQueueUserResponse
    {
        public IEnumerable<Case> Cases { get; set; }
    }
}
