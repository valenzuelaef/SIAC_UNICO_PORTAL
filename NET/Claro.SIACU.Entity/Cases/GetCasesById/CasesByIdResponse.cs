using System.Collections.Generic;

namespace Claro.SIACU.Entity.Cases.GetCasesById
{
    public class CasesByIdResponse
    {
        public IEnumerable<Case> Cases { get; set; }
    }
}