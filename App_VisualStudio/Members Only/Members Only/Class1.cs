using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members_Only
{
    class Class1
    {
        private static int _iduser;
        public static int iduser
        {
            get { return _iduser; }
            set { _iduser = value; }
        }
        //guardar o id ao logar

        private static string _username;
        public static string username
        {
            get { return _username; }
            set { _username = value; }
        }
        //guarda o nome do utilizador ao logar

        private static string _moedatipo;
        public static string moedatipo
        {
            get { return _moedatipo; }
            set { _moedatipo = value; }
        }
    }
}