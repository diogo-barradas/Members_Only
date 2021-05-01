using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Members_Only
{
    public partial class Terminar : Form
    {
        MySqlConnection connection = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");
        MySqlDataAdapter da;
        HashCode hc = new HashCode();

        public Terminar()
        {
            InitializeComponent();
            panel2.Visible = false;
            textBox2.UseSystemPasswordChar = false;

            try
            {
                // caso o utilizador tenha foto 
                connection.Open();
                MySqlCommand xpto = new MySqlCommand($"SELECT * FROM imagens WHERE(ID = {Class1.iduser})", connection);
                da = new MySqlDataAdapter(xpto);
                DataTable table = new DataTable();
                da.Fill(table);
                byte[] idf = (byte[])table.Rows[0][1];
                MemoryStream mse = new MemoryStream(idf);
                guna2PictureBox1.Image = Image.FromStream(mse);
                da.Dispose();
                connection.Close();
            }
            catch
            {
                // caso o utilizador não tenha foto
                connection.Close();
                guna2PictureBox1.Image = Properties.Resources.uploaaad;
                goto SemFoto;
            }

        SemFoto:
            connection.Open();
            MySqlCommand command = new MySqlCommand($"SELECT Idade, Email, Morada, Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int idadeuser = reader.GetInt32(0);
            string emailuser = reader.GetString(1);
            string moradauser = reader.GetString(2);
            double saldouser = reader.GetDouble(3);
            connection.Close();

            idlabel.Text = $"ID: {Class1.iduser}";
            nomelabel.Text = $"Nome: {Class1.username}";
            idadelabel.Text = $"Ano de nascimento: {idadeuser}";
            emaillabel.Text = $"E-mail: {emailuser}";
            moradalabel.Text = $"Morada: {moradauser}";
            saldolabel.Text = $"Saldo: {saldouser}{Class1.moedatipo}";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Adicionar_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == true)
            {
                panel2.Visible = false;
            }
            else
            {
                panel2.Visible = true;
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que deseja excluir permanentemente a sua conta?", "Excluir a minha Conta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                connection.Open();
                MySqlCommand comand1 = new MySqlCommand($"DELETE FROM imagens WHERE(ID = {Class1.iduser})", connection);
                comand1.ExecuteNonQuery();
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
                MessageBox.Show("A sua conta foi eliminada, vamos sentir a sua falta!");

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
            textBox2.UseSystemPasswordChar = true;
            if (textBox2.Text == "Digite o seu novo PIN")
            {
                textBox2.Text = "";
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Digite o seu novo PIN";
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "Digite o seu novo PIN")
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
            if (textBox1.Text == "Digite a sua nova morada" || textBox2.Text == "Digite o seu novo PIN" || textBox4.Text == "Digite o seu ano de nascimento")
            {
                MessageBox.Show("Todos os campos são obrigatórios!");
            }
            else if (textBox2.Text.Length != 4)
            {
                textBox2.Text = "";
                textBox2.UseSystemPasswordChar = false;
                MessageBox.Show("O seu PIN deve conter quatro digitos ou letras.");
            }
            else if (textBox4.Text.Length != 4)
            {
                textBox4.Text = "";
                MessageBox.Show("O seu Ano de Nascimento deve estar completo!");
            }
            else
            {
                connection.Open();
                try
                {
                    int Idadeuser = int.Parse(textBox4.Text);
                    if (Idadeuser > 2003)
                    {
                        textBox4.Text = "";
                        MessageBox.Show("A Idade minima é 18!");
                    }
                    else
                    {
                        MySqlCommand coand = new MySqlCommand($"UPDATE registo SET Morada=@Morada, PIN=@PIN, Idade=@Idade WHERE (ID = {Class1.iduser})", connection);
                        coand.Parameters.AddWithValue("@Morada", textBox1.Text);
                        coand.Parameters.AddWithValue("@PIN", hc.PassHash(textBox2.Text));
                        coand.Parameters.AddWithValue("@Idade", textBox4.Text);
                        coand.ExecuteNonQuery();
                        MessageBox.Show("Os seus dados foram atualizados!");

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox4.Text = "";
                        textBox2.UseSystemPasswordChar = false;
                        panel2.Visible = false;
                        Eliminar.Visible = true;
                        Adicionar.Visible = true;

                        MySqlCommand command = new MySqlCommand($"SELECT Idade, Email, Morada, Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
                        MySqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        int idadeuser = reader.GetInt32(0);
                        string emailuser = reader.GetString(1);
                        string moradauser = reader.GetString(2);
                        double saldouser = reader.GetDouble(3);

                        idlabel.Text = $"ID: {Class1.iduser}";
                        nomelabel.Text = $"Nome: {Class1.username}";
                        idadelabel.Text = $"Ano de nascimento: {idadeuser}";
                        emaillabel.Text = $"E-mail: {emailuser}";
                        moradalabel.Text = $"Morada: {moradauser}";
                        saldolabel.Text = $"Saldo: {saldouser}{Class1.moedatipo}";

                    }
                }
                catch (Exception)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox4.Text = "";
                    textBox2.UseSystemPasswordChar = false;
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
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = ""; ;
            textBox2.UseSystemPasswordChar = false;

            panel2.Visible = false;
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja alterar a sua imagem de perfil?", "Notificação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    guna2PictureBox1.Image = Image.FromFile(opf.FileName);
                }
                MemoryStream ms = new MemoryStream();
                guna2PictureBox1.Image.Save(ms, guna2PictureBox1.Image.RawFormat);
                byte[] img = ms.ToArray();

                connection.Open();
                try
                {
                    MySqlCommand command1 = new MySqlCommand($"UPDATE imagens SET Imagem=@Imagem WHERE (ID = {Class1.iduser})", connection);
                    command1.Parameters.AddWithValue("@Imagem", img);
                    if (command1.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("A sua imagem foi alterada nos nossos registos!");
                        goto comsucesso;
                    }
                }
                catch
                {
                    goto insertimg;
                }
            insertimg:
                try
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO imagens(Imagem, ID) VALUES(@Imagem, @ID)", connection);
                    command.Parameters.AddWithValue("@Imagem", img);
                    command.Parameters.AddWithValue("@ID", Class1.iduser);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("A sua imagem foi guardada nos nossos registos!");
                        goto comsucesso;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            comsucesso:
                connection.Close();
            }
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                // caso o utilizador tenha foto 
                connection.Open();
                MySqlCommand xpto = new MySqlCommand($"SELECT * FROM imagens WHERE(ID = {Class1.iduser})", connection);
                da = new MySqlDataAdapter(xpto);
                DataTable table = new DataTable();
                da.Fill(table);
                byte[] idf = (byte[])table.Rows[0][1];
                MemoryStream mse = new MemoryStream(idf);
                guna2PictureBox1.Image = Image.FromStream(mse);
                da.Dispose();
                connection.Close();
            }
            catch
            {
                // caso o utilizador não tenha foto
                connection.Close();
                guna2PictureBox1.Image = Properties.Resources.uploaaad;
                goto SemFoto;
            }

        SemFoto:
            connection.Open();
            MySqlCommand command = new MySqlCommand($"SELECT Idade, Email, Morada, Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int idadeuser = reader.GetInt32(0);
            string emailuser = reader.GetString(1);
            string moradauser = reader.GetString(2);
            double saldouser = reader.GetDouble(3);
            connection.Close();

            idlabel.Text = $"ID: {Class1.iduser}";
            nomelabel.Text = $"Nome: {Class1.username}";
            idadelabel.Text = $"Ano de nascimento: {idadeuser}";
            emaillabel.Text = $"E-mail: {emailuser}";
            moradalabel.Text = $"Morada: {moradauser}";
            saldolabel.Text = $"Saldo: {saldouser}{Class1.moedatipo}";
        }
    }
}