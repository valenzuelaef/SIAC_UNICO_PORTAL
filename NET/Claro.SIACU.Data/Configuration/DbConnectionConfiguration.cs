using Claro.Data;

namespace Claro.SIACU.Data.Configuration
{

    internal struct DbConnectionConfiguration : IDbConnectionConfiguration
    {
        public static readonly IDbConnectionConfiguration SIAC_POST_BSCS = DbConnectionConfiguration.Create("SIAC_POST_BSCS");
        public static readonly IDbConnectionConfiguration SIAC_POST_CLARIFY = DbConnectionConfiguration.Create("SIAC_POST_CLARIFY");
        public static readonly IDbConnectionConfiguration SIAC_POST_COBIL = DbConnectionConfiguration.Create("SIAC_POST_COBIL");
        public static readonly IDbConnectionConfiguration SIAC_POST_DB = DbConnectionConfiguration.Create("SIAC_POST_DB");
        public static readonly IDbConnectionConfiguration SIAC_POST_DBTO = DbConnectionConfiguration.Create("SIAC_POST_DBTO");
        public static readonly IDbConnectionConfiguration SIAC_POST_EAIAVM = DbConnectionConfiguration.Create("SIAC_POST_EAIAVM");
        public static readonly IDbConnectionConfiguration SIAC_POST_GWP = DbConnectionConfiguration.Create("SIAC_POST_GWP");
        public static readonly IDbConnectionConfiguration SIAC_POST_MGR = DbConnectionConfiguration.Create("SIAC_POST_MGR");
        public static readonly IDbConnectionConfiguration SIAC_POST_OAC = DbConnectionConfiguration.Create("SIAC_POST_OAC");
        public static readonly IDbConnectionConfiguration SIAC_POST_PVU = DbConnectionConfiguration.Create("SIAC_POST_PVU");
        public static readonly IDbConnectionConfiguration SIAC_POST_RICE = DbConnectionConfiguration.Create("SIAC_POST_RICE");
        public static readonly IDbConnectionConfiguration SIAC_POST_SACE = DbConnectionConfiguration.Create("SIAC_POST_SACE");
        public static readonly IDbConnectionConfiguration SIAC_POST_SGA = DbConnectionConfiguration.Create("SIAC_POST_SGA");
        public static readonly IDbConnectionConfiguration SIAC_POST_SIGA = DbConnectionConfiguration.Create("SIAC_POST_SIGA");
        public static readonly IDbConnectionConfiguration SIAC_POST_DWO = DbConnectionConfiguration.Create("SIAC_POST_DWO");
        public static readonly IDbConnectionConfiguration SIAC_CLAROCLUB = DbConnectionConfiguration.Create("SIAC_CLAROCLUB");
        public static readonly IDbConnectionConfiguration SIAC_SIXPROV = DbConnectionConfiguration.Create("SIAC_SIXPROV");
        public static readonly IDbConnectionConfiguration SIAC_POST_BD_MSSAP = DbConnectionConfiguration.Create("MSSAPDB");
        public static readonly IDbConnectionConfiguration TIMGLOBAL = DbConnectionConfiguration.Create("TIMGLOBAL"); 
       // public static readonly IDbConnectionConfiguration SIAC_BDPROM = DbConnectionConfiguration.Create("SIAC_BDPROM"); 
        public static readonly IDbConnectionConfiguration SIAC_POST_SIGA_NEW = DbConnectionConfiguration.Create("SIAC_POST_SIGA_NEW");

        #region [Fields]

        private string m_name;

        #endregion

        #region [ Properties ]

        #region Name

        public string Name
        {
            get
            {
                return this.m_name;
            }
        }

        #endregion

        #endregion

        #region SetName

        private void SetName(string name)
        {
            this.m_name = name;
        }

        #endregion

        private static IDbConnectionConfiguration Create(string name)
        {
            DbConnectionConfiguration helper = new DbConnectionConfiguration();

            helper.SetName(name);

            return helper;
        }
    }
}
