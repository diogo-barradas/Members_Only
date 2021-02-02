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
    public partial class Lista3 : UserControl
    {
        public Lista3()
        {
            InitializeComponent();
        }

        private string _NomeEmpresa;
        private string _Hora;
        private string _Valor;
        private string _IDdestinatario;
        private string _ID;

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
            set { _Valor = value; Valor.Text = value + Class1.moedatipo; label8.Text = value + Class1.moedatipo; label9.Text = value + Class1.moedatipo; }
        }

        public string IIDdestinatario
        {
            get { return _IDdestinatario; }
            set { _IDdestinatario = value; IDdestinatario.Text = value; }
        }
        public string IID
        {
            get { return _ID; }
            set { _ID = value; IDremetente.Text = value; }
        }
    }
}
