using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Members_Only
{
    public partial class Levantamentos : Form
    {
        MySqlConnection cnn = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");

        public Levantamentos()
        {
            InitializeComponent();
            panel8.Visible = false;
            tempo.Visible = false;

            cnn.Open();
            string bdlevantamentos = $"SELECT Descriçao, Valor FROM levantamentos WHERE (ID = {Class1.iduser})";
            MySqlCommand cmd = new MySqlCommand(bdlevantamentos, cnn);
            MySqlDataAdapter antiga = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            antiga.Fill(table);
            dataGridView1.DataSource = table;


            MySqlCommand command = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", cnn);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            _saldo = reader.GetDouble(0);
            Saldo.Text = $"{_saldo}{Class1.moedatipo}";
            cnn.Close();
        }

        private bool olho = false;

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (olho == false)
            {
                pictureBox9.Image = Properties.Resources.eye;
                olho = true;
                panel8.Visible = true;
            }
            else
            {
                pictureBox9.Image = Properties.Resources.eye1;
                olho = false;
                panel8.Visible = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public double _saldo;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "Valor a Levantar" || textBox3.Text == "Digite a Descrição")
                {
                    MessageBox.Show("Todos os campos são obrigatórios!");
                }
                else
                {
                    double retirarvalor = double.Parse(textBox1.Text);
                    if (retirarvalor <= 0)
                    {
                        textBox1.Text = "Valor a Levantar";
                        MessageBox.Show("Introduza um valor válido");
                    }
                    else if (textBox1.Text.Length > 5)
                    {
                        textBox1.Text = "Valor a Levantar";
                        MessageBox.Show("Não podemos Levantar esse valor!!");
                    }
                    else
                    {
                        if (retirarvalor > _saldo)
                        {
                            textBox1.Text = "Valor a Levantar";
                            MessageBox.Show("Você não têm fundos");
                        }
                        else
                        {
                            double saldofinal = _saldo -= retirarvalor;
                            Saldo.Text = $"{saldofinal}{Class1.moedatipo}";

                            MySqlDataAdapter adapter = new MySqlDataAdapter();
                            try
                            {
                                cnn.Open();
                                adapter.UpdateCommand = cnn.CreateCommand();
                                adapter.UpdateCommand.CommandText = ($"UPDATE registo SET Saldo = @Saldo WHERE (ID = {Class1.iduser})");
                                adapter.UpdateCommand.Parameters.AddWithValue("@Saldo", saldofinal);
                                adapter.UpdateCommand.ExecuteNonQuery();
                                cnn.Close();
                            }
                            catch (Exception ex)
                            {
                                textBox1.Text = "Valor a Levantar";
                                textBox3.Text = "Digite a Descrição";
                                MessageBox.Show(ex.Message, "Notificação");
                            }

                            MessageBox.Show($"{retirarvalor}{Class1.moedatipo} foram Levantados!");
                            tempo.Text = DateTime.Now.ToShortTimeString();//recebe a hora atual

                            cnn.Open();
                            MySqlCommand comando = new MySqlCommand($"INSERT INTO levantamentos(Descriçao, Valor, ID, Hora) VALUES (@Descriçao, @Valor, {Class1.iduser}, @Hora)", cnn);
                            comando.Parameters.AddWithValue("@Descriçao", textBox3.Text);
                            comando.Parameters.AddWithValue("@Valor", retirarvalor);
                            comando.Parameters.AddWithValue("@Hora", tempo.Text);

                            comando.ExecuteNonQuery();

                            //atualizar o dataGrid
                            string bdlevantar = $"SELECT Descriçao, Valor FROM levantamentos WHERE (ID = {Class1.iduser})";
                            MySqlCommand cmd = new MySqlCommand(bdlevantar, cnn);
                            MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            nova.Fill(table);
                            dataGridView1.DataSource = table;
                            cnn.Close();
                            textBox1.Text = "Valor a Levantar";
                            textBox3.Text = "Digite a Descrição";
                        }
                    }
                }
            }
            catch (Exception)
            {
                textBox1.Text = "Valor a Levantar";
                MessageBox.Show("Digite somente numeros!");
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Valor a Levantar")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Valor a Levantar";
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Digite a Descrição")
            {
                textBox3.Text = "";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Digite a Descrição";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public double saldorefresh;

        private void panel1_MouseHover(object sender, EventArgs e)
        {
            cnn.Open();
            MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", cnn);
            MySqlDataReader reaaaaader = commmand.ExecuteReader();
            reaaaaader.Read();
            saldorefresh = reaaaaader.GetDouble(0);
            cnn.Close();

            Saldo.Text = $"{saldorefresh}{Class1.moedatipo}";
        }
    }
}