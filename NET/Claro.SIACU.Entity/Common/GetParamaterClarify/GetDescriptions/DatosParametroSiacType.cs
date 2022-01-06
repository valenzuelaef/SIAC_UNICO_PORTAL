using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Common.GetParamaterClarify.GetDescriptions
{
   public class DatosParametroSiacType
    {
       [DataMember]
       public string tipoProceso { get; set; }

       [DataMember]
       public string datoEvalua { get; set; }
       [DataMember]
       public string parametro1 { get; set; }
       [DataMember]
       public string parametro2 { get; set; }

       [DataMember]
       public string estado { get; set; }


    }
}
