using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Members_Only
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = false;
        }

        MySqlConnection connection = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");

        //Faz desaparecer o texto da textbox ao clicar
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Digite o seu ID")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Digite o seu ID";
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

        //Gerir a checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(textBox2.Text == "Digite o seu PIN")
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

        //abrir novo form 
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Digite o seu ID" || textBox2.Text == "Digite o seu PIN")
            {
                textBox1.Text = "Digite o seu ID";
                textBox2.Text = "Digite o seu PIN";
                textBox2.UseSystemPasswordChar = false;
                MessageBox.Show("Todos os campos são obrigatórios!");
            }
            else if (textBox2.Text.Length != 4)
            {
                textBox2.Text = "Digite o seu PIN";
                textBox2.UseSystemPasswordChar = false;
                MessageBox.Show("O seu PIN encontra se no formato incorreto!");
            }
            else
            {
                MySqlCommand command = new MySqlCommand("SELECT Username,ID,PIN FROM registo WHERE ID=@ID AND PIN=@PIN", connection);

                command.Parameters.AddWithValue("@ID", textBox1.Text);
                command.Parameters.AddWithValue("@PIN", textBox2.Text);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        if (textBox1.Text == "1" && textBox2.Text == "1234")
                        {
                            var user = reader.GetString(0);
                            Class1.iduser = int.Parse(textBox1.Text);

                            if (MessageBox.Show($"Bem-Vindo {user}! Deseja consultar a visão de moderador?", "Notificação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                this.Hide();
                                FormAdmin admin = new FormAdmin();
                                admin.ShowDialog();
                            }
                            else
                            {
                                this.Hide();
                                Menu menuguest = new Menu();
                                menuguest.ShowDialog();
                            }
                        }
                        else
                        {
                            var user = reader.GetString(0);
                            MessageBox.Show($"Bem-Vindo {user}!");
                            Class1.iduser = int.Parse(textBox1.Text);
                            this.Hide();
                            Menu menu = new Menu();
                            menu.ShowDialog();
                        }
                    }
                    else
                    {
                        textBox1.Text = "Digite o seu ID";
                        textBox2.Text = "Digite o seu PIN";
                        textBox2.UseSystemPasswordChar = false;
                        MessageBox.Show("O ID e o PIN fornecidos não correspondem às informações dos nossos registros. Verifique-as e tente novamente.");
                    }
                    connection.Close();
                }
                catch (MySqlException)
                {
                    textBox1.Text = "Digite o seu ID";
                    textBox2.Text = "Digite o seu PIN";
                    textBox2.UseSystemPasswordChar = false;
                    MessageBox.Show("Insucesso durante a ligação á base de dados.", "Inicie o xampp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ox)
                {
                    textBox1.Text = "Digite o seu ID";
                    textBox2.Text = "Digite o seu PIN";
                    textBox2.UseSystemPasswordChar = false;
                    MessageBox.Show(ox.Message, "Notificação");
                }
            }    
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.FecharFinal1;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.closefinal5;
        }
       
        //abrir novo form 
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registar regist = new Registar();
            regist.ShowDialog();
        }

        //apenas numeros na textbox
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}