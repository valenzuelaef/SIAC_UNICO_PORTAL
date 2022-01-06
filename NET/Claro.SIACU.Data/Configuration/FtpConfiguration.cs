using Claro.Data;

namespace Claro.SIACU.Data.Configuration
{
    internal struct FtpConfiguration : IFtpConfiguration
    {

        public static readonly IFtpConfiguration SIACU_FtpFacturas = FtpConfiguration.Create("SIACU_FtpFacturas");

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

        private static IFtpConfiguration Create(string name)
        {
            FtpConfiguration helper = new FtpConfiguration();

            helper.SetName(name);

            return helper;
        }
    }
}
