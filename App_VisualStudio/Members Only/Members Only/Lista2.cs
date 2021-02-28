using System.Windows.Forms;

namespace Members_Only
{
    public partial class Lista2 : UserControl
    {
        public Lista2()
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