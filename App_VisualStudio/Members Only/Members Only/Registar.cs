using MySql.Data.MySqlClient;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Members_Only
{
    public partial class Registar : Form
    {
        HashCode hc = new HashCode();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public Registar()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            textBox2.UseSystemPasswordChar = false;
        }

        MySqlConnection connection = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Digite a sua morada")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Digite a sua morada";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            if (textBox2.Text == "Digite o seu PIN")
            {
                textBox2.Text = "";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Digite o seu PIN";
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Digite o seu email")
            {
                textBox3.Text = "";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = "Digite o seu email";
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Digite o seu ano de nascimento")
            {
                textBox4.Text = "";
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Digite o seu ano de nascimento";
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Digite o seu nome")
            {
                textBox5.Text = "";
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Digite o seu nome";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "Digite o seu PIN")
            {
                //não faz nada pois não está la o pin.
            }
            else
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
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (textBox1.Text == "Digite a sua morada" || textBox2.Text == "Digite o seu PIN" || textBox3.Text == "Digite o seu email" || textBox4.Text == "Digite o seu ano de nascimento" || textBox5.Text == "Digite o seu nome")
                {
                    connection.Close();
                    textBox1.Text = "Digite a sua morada";
                    textBox2.Text = "Digite o seu PIN";
                    textBox3.Text = "Digite o seu email";
                    textBox4.Text = "Digite o seu ano de nascimento";
                    textBox5.Text = "Digite o seu nome";
                    textBox2.UseSystemPasswordChar = false;
                    MessageBox.Show("Todos os campos são obrigatórios!");
                }
                else if (textBox2.Text.Length != 4)
                {
                    connection.Close();
                    textBox2.Text = "Digite o seu PIN";
                    textBox2.UseSystemPasswordChar = false;
                    MessageBox.Show("O seu PIN deve conter quatro digitos ou letras.");
                }
                else if (textBox4.Text.Length != 4)
                {
                    connection.Close();
                    textBox4.Text = "Digite o seu ano de nascimento";
                    MessageBox.Show("O seu Ano de Nascimento deve estar completo!");
                }
                else if (textBox3.Text.Contains("@") && textBox3.Text.Contains("."))
                {
                    try
                    {
                        int Idadeuser = int.Parse(textBox4.Text);
                        if (Idadeuser > 2003)
                        {
                            connection.Close();
                            textBox4.Text = "Digite o seu ano de nascimento";
                            MessageBox.Show("A Idade mínima para usar Members Only é de 18 anos!");
                        }
                        else
                        {
                            MySqlCommand command = new MySqlCommand("INSERT INTO registo(PIN, Idade, Email, Morada, Username, Saldo) VALUES(@PIN, @Idade, @Email, @Morada, @Username, 0)", connection);

                            command.Parameters.AddWithValue("@Username", textBox5.Text);
                            command.Parameters.AddWithValue("@PIN", hc.PassHash(textBox2.Text));
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
                        textBox1.Text = "Digite a sua morada";
                        textBox2.Text = "Digite o seu PIN";
                        textBox3.Text = "Digite o seu email";
                        textBox4.Text = "Digite o seu ano de nascimento";
                        textBox5.Text = "Digite o seu nome";
                        textBox2.UseSystemPasswordChar = false;
                        MessageBox.Show(ex.Message, "Este utilizador já existe!");
                    }
                    catch (Exception)
                    {
                        connection.Close();
                        textBox1.Text = "Digite a sua morada";
                        textBox2.Text = "Digite o seu PIN";
                        textBox3.Text = "Digite o seu email";
                        textBox4.Text = "Digite o seu ano de nascimento";
                        textBox5.Text = "Digite o seu nome";
                        textBox2.UseSystemPasswordChar = false;
                        MessageBox.Show("Reveja os seus dados!");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else
                {
                    connection.Close();
                    textBox3.Text = "Digite o seu email";
                    MessageBox.Show("O email deve conter um @ e .com");
                }
            }
            catch (MySqlException)
            {
                textBox1.Text = "Digite a sua morada";
                textBox2.Text = "Digite o seu PIN";
                textBox3.Text = "Digite o seu email";
                textBox4.Text = "Digite o seu ano de nascimento";
                textBox5.Text = "Digite o seu nome";
                textBox2.UseSystemPasswordChar = false;
                MessageBox.Show("Insucesso durante a ligação á base de dados.", "Inicie o xampp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ox)
            {
                textBox1.Text = "Digite a sua morada";
                textBox2.Text = "Digite o seu PIN";
                textBox3.Text = "Digite o seu email";
                textBox4.Text = "Digite o seu ano de nascimento";
                textBox5.Text = "Digite o seu nome";
                textBox2.UseSystemPasswordChar = false;
                MessageBox.Show(ox.Message, "Notificação");
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }
    }
}