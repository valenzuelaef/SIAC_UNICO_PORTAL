using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Dashboard
{
    /// <summary>
    /// Descripción breve de ConsulSeguridad.
    /// </summary>
    public class ConsulSeguridad
    {
        private string _USUACCOD;
        private string _PERFCCOD;
        private string _USUACCODVENSAP;
        private string _APLICCOD;
        private string _OPCCODPAD;
        private string _OPCICCOD;
        private string _OPCICNIVPAD;
        private string _OPCICNIV;
        private string _OPCICDES;
        private string _OPCICABREV;
        private string _OPCICNOMPAG;
        private string _OPCICNUMORD;
        private string _OPCICD1;

        public ConsulSeguridad() { }

        public string USUACCOD
        {
            set { _USUACCOD = value; }
            get { return _USUACCOD; }

        }
        public string PERFCCOD
        {
            set { _PERFCCOD = value; }
            get { return _PERFCCOD; }
        }
        public string USUACCODVENSAP
        {
            set { _USUACCODVENSAP = value; }
            get { return _USUACCODVENSAP; }
        }
        public string APLICCOD
        {
            set { _APLICCOD = value; }
            get { return _APLICCOD; }
        }
        public string OPCCODPAD
        {
            set { _OPCCODPAD = value; }
            get { return _OPCCODPAD; }
        }
        public string OPCICCOD
        {
            set { _OPCICCOD = value; }
            get { return _OPCICCOD; }
        }
        public string OPCICNIVPAD
        {
            set { _OPCICNIVPAD = value; }
            get { return _OPCICNIVPAD; }
        }
        public string OPCICNIV
        {
            set { _OPCICNIV = value; }
            get { return _OPCICNIV; }
        }
        public string OPCICDES
        {
            set { _OPCICDES = value; }
            get { return _OPCICDES; }
        }
        public string OPCICABREV
        {
            set { _OPCICABREV = value; }
            get { return _OPCICABREV; }
        }
        public string OPCICNOMPAG
        {
            set { _OPCICNOMPAG = value; }
            get { return _OPCICNOMPAG; }
        }
        public string OPCICNUMORD
        {
            set { _OPCICNUMORD = value; }
            get { return _OPCICNUMORD; }
        }
        public string OPCICD1
        {
            set { _OPCICD1 = value; }
            get { return _OPCICD1; }
        }
    }
}
