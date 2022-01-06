using Claro.SIACU.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Claro.SIACU.Data
{
    public static class Helper
    {
        /// <summary>
        /// Método que devuelve una lista XML de acuerdo a una función y un código.
        /// </summary>
        /// <param name="strNameFunction">Nombre de función</param>
        /// <param name="strFlagGetCode2">Código de flag</param>
        /// <returns>Devuelve listado solicitado.</returns>
        private static List<ListItem> GetListXML(string strNameFunction, bool strFlagGetCode2)
        {

            List<ListItem> listGenericItem = new List<ListItem>();

            string file = "Data.xml";
            string strApplicationPath;
            strApplicationPath = new Claro.Utils().GetApplicationPath();

            string fullPath = strApplicationPath + file;

            XmlDocument documento = new XmlDocument();
            documento.Load(fullPath);
            XmlNodeList nodos = documento.SelectNodes("descendant::" + strNameFunction + "/item");

            ListItem oGenericItem;
            foreach (XmlNode nodo in nodos)
            {
                oGenericItem = new ListItem();
                if (strFlagGetCode2.Equals(true))
                {
                    oGenericItem.Code = nodo["codigo"].InnerText;
                    oGenericItem.Code2 = nodo["codigo2"].InnerText;
                    oGenericItem.Description = nodo["descripcion"].InnerText;
                }
                else
                {
                    oGenericItem.Code = nodo["codigo"].InnerText;
                    oGenericItem.Description = nodo["descripcion"].InnerText;
                }
                listGenericItem.Add(oGenericItem);
            }

            return listGenericItem;
        }

        /// <summary>
        /// Método que devuelve un número del factor de bolsa. 
        /// </summary>
        /// <param name="strValue">Valor</param>
        /// <param name="strNameFunction">Nombre de función</param>
        /// <returns>Devuelve factor.</returns>
        public static double GetFactorBag(string strValue, string strNameFunction)
        {
            double factor = 0;

            List<ListItem> list = GetListXML(strNameFunction, true);

            var listResult = from m in list
                             where m.Code.Equals(strValue)
                             select m;

            foreach (ListItem p in listResult)
            {
                factor = System.Convert.ToDouble(p.Code2.ToString());
                break;
            }

            return factor;

        }

        public static string ReturnTFI(string strTelephone)
        {
            string strValueSearch = "";

            if (strTelephone.Length != 9) strValueSearch = strTelephone.PadLeft(11, char.Parse(Claro.Constants.NumberZeroString));
            else strValueSearch = strTelephone;
            return strValueSearch.Trim();
        }

        /// <summary>
        /// Método para obtener un valor de un archivo Xml.
        /// </summary>
        /// <param name="strValue">valor</param>
        /// <param name="strNameFunction">Nombre de la función</param>
        /// <returns>Devuelve cadena con valor de un archivo xml.</returns>
        public static string GetValueXML(string strValue, string strNameFunction)
        {
            string response = string.Empty;

            List<ListItem> list = GetListXML(strNameFunction, false);

            var listResult = from m in list
                             where m.Code.Equals(strValue)
                             select m;

            foreach (ListItem p in listResult)
            {
                response = p.Description.ToString();
                break;
            }

            return response;
        }
    }
}
