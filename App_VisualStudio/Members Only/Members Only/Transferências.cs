using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Members_Only
{
    public partial class Transferências : Form
    {
        MySqlConnection cnn = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");

        public Transferências()
        {
            InitializeComponent();
            panelbd.Visible = false;
            panel8.Visible = false;
            tempo.Visible = false;
            label3.Text = Class1.moedatipo;

            cnn.Open();
            string bdtranferencias = $"SELECT Descriçao, Hora, Valor, idDestinatario FROM transferencias WHERE (ID = {Class1.iduser})";
            MySqlCommand cmd = new MySqlCommand(bdtranferencias, cnn);
            MySqlDataAdapter antiga = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            antiga.Fill(table);
            dataGridView1.DataSource = table;
            cnn.Close();

            cnn.Open();
            MySqlCommand command = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", cnn);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            _saldo = reader.GetDouble(0);
            Saldo.Text = _saldo.ToString();
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
        public double _saldodestino;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == " Montante" || textBox3.Text == " ex.Transferências" || textBox2.Text == " ID Destinatário")
                {
                    MessageBox.Show("Todos os campos são obrigatórios!");
                }
                else
                {
                    double transferirvalor = double.Parse(textBox1.Text);
                    if (transferirvalor <= 0)
                    {
                        MessageBox.Show("Introduza um valor válido");
                    }
                    else
                    {
                        if (transferirvalor > _saldo)
                        {
                            MessageBox.Show("Você não têm fundos");
                        }
                        else
                        {
                            int _iddestino = Convert.ToInt32(textBox2.Text);
                            if (_iddestino == Class1.iduser)
                            {
                                MessageBox.Show("Você não pode transferir para si mesmo!");
                            }
                            else
                            {
                                try
                                {
                                    cnn.Open();
                                    MySqlCommand xpto = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {_iddestino})", cnn);
                                    MySqlDataReader abc = xpto.ExecuteReader();
                                    if (abc.Read())
                                    {
                                        _saldodestino = abc.GetDouble(0);
                                        double saldofinal = _saldo -= transferirvalor;
                                        double saldofinaldestino = _saldodestino += transferirvalor;
                                        Saldo.Text = saldofinal.ToString();
                                        cnn.Close();

                                        cnn.Open();
                                        MySqlDataAdapter adapter = new MySqlDataAdapter();

                                        adapter.UpdateCommand = cnn.CreateCommand();
                                        adapter.UpdateCommand.CommandText = ($"UPDATE registo SET Saldo = @Saldo WHERE (ID = {Class1.iduser})");
                                        adapter.UpdateCommand.Parameters.AddWithValue("@Saldo", saldofinal);
                                        adapter.UpdateCommand.ExecuteNonQuery();

                                        //acrescentar valor ao idDestinatario
                                        adapter.UpdateCommand = cnn.CreateCommand();
                                        adapter.UpdateCommand.CommandText = ($"UPDATE registo SET Saldo = @Saldonovo WHERE (ID = {_iddestino})");
                                        adapter.UpdateCommand.Parameters.AddWithValue("@Saldonovo", saldofinaldestino);
                                        adapter.UpdateCommand.ExecuteNonQuery();

                                        MessageBox.Show($"{transferirvalor}{Class1.moedatipo} foram Transferidos!");
                                        tempo.Text = DateTime.Now.ToShortTimeString();//recebe a hora atual

                                        MySqlCommand comando = new MySqlCommand($"INSERT INTO transferencias(Descriçao, Valor, Hora, ID, idDestinatario) VALUES (@Descriçao, @Valor, @Hora, {Class1.iduser}, {_iddestino})", cnn);
                                        comando.Parameters.AddWithValue("@Descriçao", textBox3.Text);
                                        comando.Parameters.AddWithValue("@Valor", transferirvalor);
                                        comando.Parameters.AddWithValue("@Hora", tempo.Text);
                                        comando.Parameters.AddWithValue("@idDestinatario", textBox2.Text);

                                        comando.ExecuteNonQuery();

                                        //atualizar o dataGrid
                                        string bdtranferencias = $"SELECT Descriçao, Hora, Valor, idDestinatario FROM transferencias WHERE (ID = {Class1.iduser})";
                                        MySqlCommand cmd = new MySqlCommand(bdtranferencias, cnn);
                                        MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
                                        DataTable table = new DataTable();
                                        nova.Fill(table);
                                        dataGridView1.DataSource = table;

                                        textBox1.Text = " Montante";
                                        textBox3.Text = " ex.Transferências";
                                        textBox2.Text = " ID Destinatário";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Não existe um utilizador com este ID!");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Notificação");
                                }
                                finally
                                {
                                    cnn.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Notificação");
            }
            catch (Exception)
            {
                MessageBox.Show("Digite somente numeros!");
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == " Montante")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = " Montante";
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == " ex.Transferências")
            {
                textBox3.Text = "";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = " ex.Transferências";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == " ID Destinatário")
            {
                textBox2.Text = "";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = " ID Destinatário";
            }
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            panelbd.Visible = true;

            cnn.Open();
            string bdconsult = $"SELECT ID, Username FROM registo WHERE (ID != {Class1.iduser})";
            MySqlCommand cons = new MySqlCommand(bdconsult, cnn);
            MySqlDataAdapter consse = new MySqlDataAdapter(cons);
            DataTable tabela = new DataTable();
            consse.Fill(tabela);
            dataGridView2.DataSource = tabela;
            cnn.Close();
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panelbd.Visible = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}