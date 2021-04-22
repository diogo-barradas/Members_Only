using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace Members_Only
{
    public partial class Login : Form
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

        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            textBox2.UseSystemPasswordChar = false;
        }

        public string pinadmin;
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
                command.Parameters.AddWithValue("@PIN", hc.PassHash(textBox2.Text));
                string verficaradmin = hc.PassHash(textBox2.Text);

                try
                {
                    connection.Open();
                    MySqlCommand com = new MySqlCommand("SELECT PIN FROM registo WHERE(ID = 1)", connection);
                    MySqlDataReader ler = com.ExecuteReader();
                    ler.Read();
                    pinadmin = ler.GetString(0);
                    connection.Close();

                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        if (textBox1.Text == "1" && verficaradmin == pinadmin)
                        {
                            MessageBox.Show("Isto é uma área ultra secreta!");
                            this.Hide();
                            FormAdmin admin = new FormAdmin();
                            admin.ShowDialog();
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