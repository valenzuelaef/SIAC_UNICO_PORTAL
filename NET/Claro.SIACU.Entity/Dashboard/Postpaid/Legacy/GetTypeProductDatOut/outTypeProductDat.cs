using Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Entity.Dashboard.Postpaid.Legacy.GetTypeProductDatOut
{
    public class outTypeProductDat
    {
        public outTypeProductDat()
        {

        }
        public outTypeProductDat(string strOpcionalCoIdPub, string strOpcionalCsId, string strOpcionalCustCode, string coidDat, string idStateLine, Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Response.response.CaracteristicaAdicional outCaractAdi)
        {

            this.listaOpcionalDat = new List<listaOpcional>()
            {
            
                  new     listaOpcional ()
                    {
                                                      clave=KEY.AppSettings("keycoIdPubDat") ,//keycoIdPubDat
                                                      valor=strOpcionalCoIdPub
                    },
                  new     listaOpcional ()
                   {
                                                       clave=KEY.AppSettings("keycsIdDat"),//keycsIdDat
                                                       valor=strOpcionalCsId
                   },
                  new     listaOpcional ()
                   {
                                                       clave=KEY.AppSettings("keycustCodeDat"), //keycustCodeDat
                                                       valor=strOpcionalCustCode
                   }
            };
            this.coidDat = coidDat;
            this.idStateLine = idStateLine;
            this.outCaractAdi = outCaractAdi;
        }


        public List<listaOpcional> ListaOpcionalDat
        {
            get { return listaOpcionalDat; }
            set { listaOpcionalDat = value; }
        }


        public Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Response.response.CaracteristicaAdicional OutCaractAdi
        {
            get { return outCaractAdi; }
            set { outCaractAdi = value; }
        }



        public string CoidDat
        {
            get { return coidDat; }
            set { coidDat = value; }
        }
        public string IdStateLine
        {
            get { return idStateLine; }
            set { idStateLine = value; }
        }
        private string idStateLine;
        private string coidDat;
        private List<listaOpcional> listaOpcionalDat;
        private Entity.Dashboard.Postpaid.Legacy.GetTypeProductDat.Response.response.CaracteristicaAdicional outCaractAdi;
    }
}
