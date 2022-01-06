using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Dashboard.Postpaid
{
    [DataContract(Name = "ContactPostPaid")]
    public class Contact
    {


        [DataMember]
        [Data.DbColumn("CSCTN_CODIGO")]
        public int CSCTN_CODIGO { get; set; }
        [DataMember]
        [Data.DbColumn("SPERN_CODIGO")]
        public int SPERN_CODIGO { get; set; }
        [DataMember]
        [Data.DbColumn("SOLIN_CODIGO")]
        public int SOLIN_CODIGO { get; set; }
        [DataMember]
        [Data.DbColumn("TCCTN_CODIGO")]
        public int TCCTN_CODIGO { get; set; }
        [DataMember]
        [Data.DbColumn("SPERV_ESTADO")]
        public string SPERV_ESTADO { get; set; }
        [DataMember]
        [Data.DbColumn("SPERV_USU_CREA")]
        public string SPERV_USU_CREA { get; set; }
        [DataMember]
        [Data.DbColumn("SPERD_FEC_REG")]
        public DateTime SPERD_FEC_REG { get; set; }

        [DataMember]
        [Data.DbColumn("CAMPOSADICIONALES")]
        public string CAMPOSADICIONALES { get; set; }

        [DataMember]
        public string TDOCV_DESCRIPCION { get; set; }
        [DataMember]
        public int P_CUSTOMER_ID { get; set; }
        [DataMember]
        public string P_CUSTCODE { get; set; }
        [DataMember]
        public int P_SOLIN_CODIGO { get; set; }
        [DataMember]
        public int P_CSCTN_CODIGO { get; set; }




        [DataMember]
        public List<AdditionalFields> lstAdditional { get; set; }

        private string _SPERV_NOMBRE { get; set; }
        private string _SPERV_APEPATERNO { get; set; }
        private string _SPERV_APEMATERNO { get; set; }
        private string _SPERV_CARGO { get; set; }
        private string _TDOCC_CODIGO { get; set; }
        private string _SPERV_NUM_DOC { get; set; }
        private string _SPERV_TEL1 { get; set; }
        private char _SPERC_TIPO { get; set; }

        [DataMember]
        public char SPERC_TIPO
        {
            get
            {
                return (TCCTN_CODIGO == 1 ? 'F' : 'C');
            }
            set { this._SPERC_TIPO = value; }
        }

        [DataMember]
        public string SPERV_NOMBRE
        {
            get
            {
                if (this.lstAdditional != null)
                {
                    this._SPERV_NOMBRE = ValueAdditionalFields(CamposAntiguos.SPERV_NOMBRE_CODIGO);
                }
                return _SPERV_NOMBRE;
            }
            set
            {
                this._SPERV_NOMBRE = value;
            }
        }

        [DataMember]
        public string SPERV_APEPATERNO
        {
            get
            {
                if (this.lstAdditional != null)
                {
                    this._SPERV_APEPATERNO = ValueAdditionalFields(CamposAntiguos.SPERV_APEPATERNO_CODIGO);
                }
                return _SPERV_APEPATERNO;
            }
            set
            {
                this._SPERV_APEPATERNO = value;
            }
        }
        [DataMember]
        public string SPERV_APEMATERNO
        {
            get
            {
                if (this.lstAdditional != null)
                {
                    this._SPERV_APEMATERNO = ValueAdditionalFields(CamposAntiguos.SPERV_APEMATERNO_CODIGO);
                }
                return _SPERV_APEMATERNO;
            }
            set
            {
                this._SPERV_APEMATERNO = value;
            }
        }

        [DataMember]
        public string SPERV_CARGO
        {
            get
            {
                if (this.lstAdditional != null)
                {
                    this._SPERV_CARGO = ValueAdditionalFields(CamposAntiguos.SPERV_CARGO_CODIGO);
                }
                return _SPERV_CARGO;
            }
            set
            {
                this._SPERV_CARGO = value;
            }
        }

        [DataMember]
        public string TDOCC_CODIGO
        {
            get
            {
                if (this.lstAdditional != null)
                {
                    this._TDOCC_CODIGO = ValueAdditionalFields(CamposAntiguos.TDOCC_CODIGO_CODIGO);
                }
                return _TDOCC_CODIGO;
            }
            set
            {
                this._TDOCC_CODIGO = value;
            }
        }

        [DataMember]
        public string SPERV_NUM_DOC
        {
            get
            {
                if (this.lstAdditional != null)
                {
                    this._SPERV_NUM_DOC = ValueAdditionalFields(CamposAntiguos.SPERV_NUM_DOC_CODIGO);
                }
                return _SPERV_NUM_DOC;
            }
            set
            {
                this._SPERV_NUM_DOC = value;
            }
        }

        [DataMember]
        public string SPERV_TEL1
        {
            get
            {
                if (this.lstAdditional != null)
                {
                    this._SPERV_TEL1 = ValueAdditionalFields(CamposAntiguos.SPERV_TEL1_CODIGO);
                }
                return _SPERV_TEL1;
            }
            set
            {
                this._SPERV_TEL1 = value;
            }
        }

        private string ValueAdditionalFields(CamposAntiguos FieldCode)
        {
            foreach (var objItem in this.lstAdditional)
            {
                if (Convert.ToInt64(objItem.TCCCN_CODIGO.Trim()) == FieldCode.GetHashCode())
                {
                    return objItem.TCCVV_VALOR;
                }
            }
            return null;
        }
    }
}
