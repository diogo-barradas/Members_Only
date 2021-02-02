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
    public partial class Registar : Form
    {
        public Registar()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");

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

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Email")
            {
                textBox3.Text = "";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Email";
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

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Username")
            {
                textBox5.Text = "";
            }
        }
        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Username";
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
            try
            {
                connection.Open();
                if (textBox1.Text == "Morada" || textBox2.Text == "OPIN" || textBox3.Text == "Email" || textBox4.Text == "Idade" || textBox5.Text == "Username" || textBox2.Text.Length != 4)
                {
                    connection.Close();
                    MessageBox.Show("Todos os campos são obrigatórios!\nPIN = 4 Digitos/Letras");
                }
                else if (textBox3.Text.Contains("@") && textBox3.Text.Contains("."))
                {
                    try
                    {
                        int Idadeuser = int.Parse(textBox4.Text);
                        if (Idadeuser < 18)
                        {
                            connection.Close();
                            MessageBox.Show("A Idade minima é 18!");
                        }
                        else
                        {
                            MySqlCommand command = new MySqlCommand("INSERT INTO registo(PIN, Idade, Email, Morada, Username, Saldo) VALUES(@PIN, @Idade, @Email, @Morada, @Username, 0)", connection);

                            command.Parameters.AddWithValue("@Username", textBox5.Text);
                            command.Parameters.AddWithValue("@PIN", textBox2.Text);
                            command.Parameters.AddWithValue("@Idade", textBox4.Text);
                            command.Parameters.AddWithValue("@Email", textBox3.Text);
                            command.Parameters.AddWithValue("@Morada", textBox1.Text);

                            command.ExecuteNonQuery();

                            string __email = textBox3.Text;
                            string __username = textBox5.Text;
                            MySqlCommand coma = new MySqlCommand("SELECT ID, Email, Username FROM registo WHERE Email=@Email and Username=@Username", connection);
                            coma.Parameters.AddWithValue("@Email", __email);
                            coma.Parameters.AddWithValue("@Username", __username);
                            coma.ExecuteNonQuery();

                            MySqlDataReader teste = coma.ExecuteReader();
                            if (teste.Read())
                            {
                                int __id = teste.GetInt32(0);

                                MessageBox.Show($"O seu id é {__id}"); //Apareca o ID do utilizador que acabou de criar
                                this.Hide();
                                Login login = new Login();
                                login.ShowDialog();
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        connection.Close();
                        MessageBox.Show(ex.Message, "Este utilizador já existe!");
                    }
                    catch (Exception)
                    {
                        connection.Close();
                        MessageBox.Show("Reveja os seus Dados!");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else
                {
                    connection.Close();
                    MessageBox.Show("O email deve conter @ e .com");
                }
            }
            catch (MySqlException)
            {
                MessageBox.Show("Você necessita abrir o XAMPP!", "Sem conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ox)
            {
                MessageBox.Show(ox.Message, "Notificação");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.closefinal5;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.FecharFinal1;
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