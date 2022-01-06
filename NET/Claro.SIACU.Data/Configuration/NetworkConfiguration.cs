﻿using Claro.Data;

namespace Claro.SIACU.Data.Configuration
{
    internal struct  NetworkConfiguration : INetworkConfiguration
    {
        public static readonly INetworkConfiguration SIAC_POST_DirFacturas = NetworkConfiguration.Create("SIAC_POST_DirFacturas");

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

        private static INetworkConfiguration Create(string name)
        {
            NetworkConfiguration helper = new NetworkConfiguration();

            helper.SetName(name);

            return helper;
        }
    }
}
