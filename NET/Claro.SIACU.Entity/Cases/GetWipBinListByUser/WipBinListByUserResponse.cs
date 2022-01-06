using System.Collections.Generic;

namespace Claro.SIACU.Entity.Cases.GetWipBinListByUser
{
    public class WipBinListByUserResponse
    {
        public IEnumerable<WipBin> ListWipBin { get; set; }
    }
}