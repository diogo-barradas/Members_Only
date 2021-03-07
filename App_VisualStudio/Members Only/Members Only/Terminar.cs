using System;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Members_Only
{
    public partial class Terminar : Form
    {
        public Terminar()
        {
            InitializeComponent();
            panel2.Visible = false;
        }

        MySqlConnection connection = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Sair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que deseja sair da aplicação?", "Fechar Members Only", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Class1.moedatipo == "$") // Dólares Para Euros
                {
                    connection.Open();
                    MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
                    MySqlDataReader reaader = commmand.ExecuteReader();
                    reaader.Read();
                    double dolaretoeuro = reaader.GetDouble(0);
                    connection.Close();

                    double eurodolar = (dolaretoeuro * 0.8392); // valor de 1 dolar em euros
                    double saldonovo = Math.Round(eurodolar, 2);

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    try
                    {
                        connection.Open();
                        adapter.UpdateCommand = connection.CreateCommand();
                        adapter.UpdateCommand.CommandText = ($"UPDATE registo SET Saldo = @Saldo WHERE (ID = {Class1.iduser})");
                        adapter.UpdateCommand.Parameters.AddWithValue("@Saldo", saldonovo);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Notificação");
                    }
                    MessageBox.Show("O dinheiro foi atualizado para euros!");
                }
                else if (Class1.moedatipo == "£") //Libras para Euros
                {
                    connection.Open();
                    MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
                    MySqlDataReader reaader = commmand.ExecuteReader();
                    reaader.Read();
                    double libratoeuro = reaader.GetDouble(0);
                    connection.Close();

                    double eurolibra = (libratoeuro * 1.1612); // valor de 1 libra em euros
                    double saldonovo = Math.Round(eurolibra, 2);

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    try
                    {
                        connection.Open();
                        adapter.UpdateCommand = connection.CreateCommand();
                        adapter.UpdateCommand.CommandText = ($"UPDATE registo SET Saldo = @Saldo WHERE (ID = {Class1.iduser})");
                        adapter.UpdateCommand.Parameters.AddWithValue("@Saldo", saldonovo);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Notificação");
                    }
                    MessageBox.Show("O dinheiro foi atualizado para euros!");
                }
                else
                {
                    //não faz nada
                }
                Class1.moedatipo = "€";
                Application.Exit();
            }
        }

        private void Sessao_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que deseja terminar sessão?", "Terminar Sessão", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Class1.moedatipo == "$") // Dólares Para Euros
                {
                    connection.Open();
                    MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
                    MySqlDataReader reaader = commmand.ExecuteReader();
                    reaader.Read();
                    double dolaretoeuro = reaader.GetDouble(0);
                    connection.Close();

                    double eurodolar = (dolaretoeuro * 0.8392); // valor de 1 dolar em euros
                    double saldonovo = Math.Round(eurodolar, 2);

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    try
                    {
                        connection.Open();
                        adapter.UpdateCommand = connection.CreateCommand();
                        adapter.UpdateCommand.CommandText = ($"UPDATE registo SET Saldo = @Saldo WHERE (ID = {Class1.iduser})");
                        adapter.UpdateCommand.Parameters.AddWithValue("@Saldo", saldonovo);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Notificação");
                    }
                    MessageBox.Show("O dinheiro foi atualizado para euros!");
                }
                else if (Class1.moedatipo == "£") //Libras para Euros
                {
                    connection.Open();
                    MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
                    MySqlDataReader reaader = commmand.ExecuteReader();
                    reaader.Read();
                    double libratoeuro = reaader.GetDouble(0);
                    connection.Close();

                    double eurolibra = (libratoeuro * 1.1612); // valor de 1 libra em euros
                    double saldonovo = Math.Round(eurolibra, 2);

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    try
                    {
                        connection.Open();
                        adapter.UpdateCommand = connection.CreateCommand();
                        adapter.UpdateCommand.CommandText = ($"UPDATE registo SET Saldo = @Saldo WHERE (ID = {Class1.iduser})");
                        adapter.UpdateCommand.Parameters.AddWithValue("@Saldo", saldonovo);
                        adapter.UpdateCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Notificação");
                    }
                    MessageBox.Show("O dinheiro foi atualizado para euros!");
                }
                else
                {
                    //não faz nada
                }
                Class1.moedatipo = "€";

                this.Hide();
                // fazer fechar o por baixo -> BUGGGGGGGGGGGGGG
                Login login = new Login();
                login.ShowDialog();
            }
        }

        private void Adicionar_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
                Sessao.Visible = true;
                Sair.Visible = true;
                Eliminar.Visible = true;
            }
            else
            {
                panel2.Visible = true;
                Sessao.Visible = false;
                Sair.Visible = false;
                Eliminar.Visible = false;
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que deseja excluir permanentemente a sua conta?", "Excluir a minha Conta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                connection.Open();

                MySqlCommand comand = new MySqlCommand($"DELETE FROM depositos WHERE(ID = {Class1.iduser})", connection);
                comand.ExecuteNonQuery();

                MySqlCommand comad = new MySqlCommand($"DELETE FROM levantamentos WHERE(ID = {Class1.iduser})", connection);
                comad.ExecuteNonQuery();

                MySqlCommand cmad = new MySqlCommand($"DELETE FROM transferencias WHERE(ID = {Class1.iduser})", connection);
                cmad.ExecuteNonQuery();

                MySqlCommand command = new MySqlCommand($"DELETE FROM registo WHERE(ID = {Class1.iduser})", connection);
                command.ExecuteNonQuery();

                connection.Close();

                Thread.Sleep(700);
                MessageBox.Show("A sua conta foi Eliminada!");

                //voltar ao login
                this.Hide();
                Login login = new Login();
                login.ShowDialog();
            }
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.closefinal5;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.FecharFinal1;
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "OPIN")
            {
                textBox2.Text = "";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "OPIN";
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Idade")
            {
                textBox4.Text = "";
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Idade";
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Morada")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Morada";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Morada" || textBox2.Text == "OPIN" || textBox4.Text == "Idade" || textBox2.Text.Length != 4)
            {
                MessageBox.Show("Todos os campos são obrigatórios!\nPIN = 4 Digitos/Letras");
            }
            else
            {
                connection.Open();
                try
                {
                    int Idadeuser = int.Parse(textBox4.Text);
                    if (Idadeuser < 18)
                    {
                        MessageBox.Show("A Idade minima é 18!");
                    }
                    else
                    {
                        MySqlCommand coand = new MySqlCommand($"UPDATE registo SET Morada=@Morada, PIN=@PIN, Idade=@Idade WHERE (ID = {Class1.iduser})", connection);
                        coand.Parameters.AddWithValue("@Morada", textBox1.Text);
                        coand.Parameters.AddWithValue("@PIN", textBox2.Text);
                        coand.Parameters.AddWithValue("@Idade", textBox4.Text);
                        coand.ExecuteNonQuery();
                        MessageBox.Show("Os seus dados foram atualizados!");

                        panel2.Visible = false;
                        Sessao.Visible = true;
                        Sair.Visible = true;
                        Eliminar.Visible = true;
                        Adicionar.Visible = true;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Reveja os seus Dados!");
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            Sessao.Visible = true;
            Sair.Visible = true;
            Eliminar.Visible = true;
            Adicionar.Visible = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}