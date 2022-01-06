using System.Collections.Generic;

namespace Claro.SIACU.Entity.Cases.GetCasesByWipBin
{
    public class CasesByWipBinResponse
    {
        public IEnumerable<Case> Cases { get; set; }
    }
}