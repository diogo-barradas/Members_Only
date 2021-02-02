using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Members_Only
{
    public partial class Lista : UserControl
    {
        public Lista()
        {
            InitializeComponent();
        }

        private string _NomeEmpresa;
        private string _Hora;
        private string _Valor;

        public string NNomeEmpresa
        {
            get { return _NomeEmpresa; }
            set { _NomeEmpresa = value; Nome.Text = value; }
        }

        public string HHora
        {
            get { return _Hora; }
            set { _Hora = value; Hora.Text = value; }
        }

        public string VValor
        {
            get { return _Valor; }
            set { _Valor = value; Valor.Text = value + Class1.moedatipo; }
        }
    }
}