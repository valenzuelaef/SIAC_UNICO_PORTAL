using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Claro.SIACU.Entity.Coliving.Common;
namespace Claro.SIACU.Business.Coliving
{
    public class Common
    {
        public static List<Claro.SIACU.Entity.Coliving.Common.ColivingParameters> GetColivingParameters(string strIdSession, string strTransaction)
        {
            List<Claro.SIACU.Entity.Coliving.Common.ColivingParameters> list = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Coliving.Common.ColivingParameters>>(strIdSession, strTransaction, () =>
            {
                return Data.Coliving.Common.GetColivingParameters(strIdSession, strTransaction);
            });

            return list;
        }
    }
}
