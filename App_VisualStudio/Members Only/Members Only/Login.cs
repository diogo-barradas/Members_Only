using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Members_Only
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        MySqlConnection connection = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");

        //Faz desaparecer o texto da textbox ao clicar
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "ID")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "ID";
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

        //Gerir a checkbox
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

        //abrir novo form 
        private void button8_Click(object sender, EventArgs e)
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

                        if (MessageBox.Show($"Bem-Vindo {user}! Deseja consultar a visão de Moderador?", "Notificação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                    MessageBox.Show("O ID e o PIN fornecidos não correspondem às informações em nossos registros.Verifique-as e tente novamente.");
                }
                connection.Close();
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

        //abrir novo form 
        private void label5_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
            Registar regist = new Registar();
            regist.ShowDialog();
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
    }
}
