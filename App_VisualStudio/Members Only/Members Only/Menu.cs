﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Members_Only
{
    public partial class Menu : Form
    {
        public double saldo;
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);
        private int userexemplo1, userexemplo2, userexemplo3;

        MySqlConnection connection = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");
        MySqlDataAdapter da;

        public Menu()
        {
            InitializeComponent();

            connection.Open();
            MySqlCommand command = new MySqlCommand($"SELECT Username FROM registo WHERE(ID = {Class1.iduser})", connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            Class1.username = reader.GetString(0);
            connection.Close();

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
                pictureBox1.Image = Image.FromStream(mse);
                da.Dispose();
                connection.Close();
            }
            catch
            {
                // caso o utilizador não tenha foto
                connection.Close();
                pictureBox1.Image = Properties.Resources.uploaaad;
                goto SemFoto;
            }

        SemFoto:
            label11.Text = $"Olá {Class1.username}!";
            notificacoespanel.Visible = false;
            panel3.Visible = false;
            panel1.Visible = false;
            Slidepanel.Height = (button1.Height - 15);
            Slidepanel.Top = (button1.Top + 10);
            Class1.moedatipo = "€"; //euro é a moeda caso o user nao mude 

            connection.Open();
            MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
            MySqlDataReader reaader = commmand.ExecuteReader();
            reaader.Read();
            saldo = reaader.GetDouble(0);
            connection.Close();
            label_id.Text = $"ID: {Class1.iduser}";
            label_saldo.Text = $"{saldo}{Class1.moedatipo}";

            if (label_saldo.Text.Length == 4)
            {
                label_saldo.Location = new Point(153, 62);
            }
            else if (label_saldo.Text.Length == 5)
            {
                label_saldo.Location = new Point(145, 62);
            }
            else if (label_saldo.Text.Length == 6)
            {
                label_saldo.Location = new Point(135, 62);
            }
            else if (label_saldo.Text.Length <= 3)
            {
                label_saldo.Location = new Point(171, 62);
            }
            else
            {
                label_saldo.Location = new Point(94, 62);
            }

            Random rnd = new Random();
            do
            {
                // 3 utilizadores random da base de dados
                userexemplo1 = rnd.Next(2, 10);
                userexemplo2 = rnd.Next(2, 10);
                userexemplo3 = rnd.Next(2, 10);
            }
            while (userexemplo1 == userexemplo2 || userexemplo2 == userexemplo3 || userexemplo1 == userexemplo3 || userexemplo1 == Class1.iduser || userexemplo2 == Class1.iduser || userexemplo3 == Class1.iduser);
            //mostrar as pessoas recomendadas
            try
            {
                connection.Open();
                MySqlCommand commmmand = new MySqlCommand($"SELECT Username,Email FROM registo WHERE(ID = {userexemplo1})", connection);
                MySqlDataReader readeeeer = commmmand.ExecuteReader();
                readeeeer.Read();
                string nomeexemplo1 = readeeeer.GetString(0);
                string emailexemplo1 = readeeeer.GetString(1);
                label5.Text = $"{nomeexemplo1}";
                label6.Text = $"{emailexemplo1}";
                connection.Close();
            }
            catch
            {
                connection.Close();
                label5.Text = "sem registos";
                label6.Text = "sem registos";
                goto semuser1;
            }
        semuser1:
            try
            {
                connection.Open();
                MySqlCommand xpto1 = new MySqlCommand($"SELECT * FROM imagens WHERE(ID = {userexemplo1})", connection);
                da = new MySqlDataAdapter(xpto1);
                DataTable table1 = new DataTable();
                da.Fill(table1);
                byte[] idf1 = (byte[])table1.Rows[0][1];
                MemoryStream mse1 = new MemoryStream(idf1);
                pictureBox12.Image = Image.FromStream(mse1);
                da.Dispose();
                connection.Close();
            }
            catch
            {
                connection.Close();
                pictureBox12.Image = Properties.Resources.uploaaad;
                goto semfoto1;
            }
        semfoto1:
            try
            {
                connection.Open();
                MySqlCommand com = new MySqlCommand($"SELECT Username,Email FROM registo WHERE(ID = {userexemplo2})", connection);
                MySqlDataReader rae = com.ExecuteReader();
                rae.Read();
                string nomeexemplo2 = rae.GetString(0);
                string emailexemplo2 = rae.GetString(1);
                label7.Text = $"{nomeexemplo2}";
                label8.Text = $"{emailexemplo2}";
                connection.Close();
            }
            catch
            {
                connection.Close();
                label7.Text = "sem registos";
                label8.Text = "sem registos";
                goto semuser2;
            }
        semuser2:
            try
            {
                connection.Open();
                MySqlCommand xpto2 = new MySqlCommand($"SELECT * FROM imagens WHERE(ID = {userexemplo2})", connection);
                da = new MySqlDataAdapter(xpto2);
                DataTable table2 = new DataTable();
                da.Fill(table2);
                byte[] idf2 = (byte[])table2.Rows[0][1];
                MemoryStream mse2 = new MemoryStream(idf2);
                pictureBox13.Image = Image.FromStream(mse2);
                da.Dispose();
                connection.Close();
            }
            catch
            {
                connection.Close();
                pictureBox13.Image = Properties.Resources.uploaaad;
                goto semfoto2;
            }
        semfoto2:
            try
            {
                connection.Open();
                MySqlCommand coma = new MySqlCommand($"SELECT Username,Email FROM registo WHERE(ID = {userexemplo3})", connection);
                MySqlDataReader rea = coma.ExecuteReader();
                rea.Read();
                string nomeexemplo3 = rea.GetString(0);
                string emailexemplo3 = rea.GetString(1);
                label9.Text = $"{emailexemplo3}";
                label10.Text = $"{nomeexemplo3}";
                connection.Close();
            }
            catch
            {
                connection.Close();
                label9.Text = "sem registos";
                label10.Text = "sem registos";
                goto semuser3;
            }
        semuser3:
            try
            {
                connection.Open();
                MySqlCommand xpto3 = new MySqlCommand($"SELECT * FROM imagens WHERE(ID = {userexemplo3})", connection);
                da = new MySqlDataAdapter(xpto3);
                DataTable table3 = new DataTable();
                da.Fill(table3);
                byte[] idf3 = (byte[])table3.Rows[0][1];
                MemoryStream mse3 = new MemoryStream(idf3);
                pictureBox14.Image = Image.FromStream(mse3);
                da.Dispose();
                connection.Close();
            }
            catch
            {
                connection.Close();
                pictureBox14.Image = Properties.Resources.uploaaad;
                goto semfoto3;
            }
        semfoto3:;
        }

        private void button9_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Visible = false;
            if (Class1.moedatipo == "€")
            {
                MessageBox.Show("O dinheiro já se encontra em Euros!");
            }
            else
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
                }
                else  //Libras para Euros
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
                }
                Class1.moedatipo = "€";
                MessageBox.Show($"O dinheiro foi atualizado para Euros ({Class1.moedatipo})");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            if (Class1.moedatipo == "$")
            {
                MessageBox.Show("O dinheiro já se encontra em Dólares!");
            }
            else
            {
                if (Class1.moedatipo == "€") // Euros para Dolares
                {
                    connection.Open();
                    MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
                    MySqlDataReader reaader = commmand.ExecuteReader();
                    reaader.Read();
                    double eurotodolar = reaader.GetDouble(0);
                    connection.Close();

                    double dolareuro = (eurotodolar * 1.1916); // valor de 1 euro em dolares
                    double saldonovo = Math.Round(dolareuro, 2);

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
                }
                else  //Libras para Dolares
                {
                    connection.Open();
                    MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
                    MySqlDataReader reaader = commmand.ExecuteReader();
                    reaader.Read();
                    double libratodolar = reaader.GetDouble(0);
                    connection.Close();

                    double dolarlibra = (libratodolar * 1.3833); // valor de 1 libra em dolares
                    double saldonovo = Math.Round(dolarlibra, 2);

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
                }
                Class1.moedatipo = "$";
                MessageBox.Show($"O dinheiro foi atualizado para Dólares ({Class1.moedatipo})");
            }
        }

        private void button11_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Visible = false;
            if (Class1.moedatipo == "£")
            {
                MessageBox.Show("O dinheiro já se encontra em Libras!");
            }
            else
            {
                if (Class1.moedatipo == "€") // Euros para Libras
                {
                    connection.Open();
                    MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
                    MySqlDataReader reaader = commmand.ExecuteReader();
                    reaader.Read();
                    double eurotolibra = reaader.GetDouble(0);
                    connection.Close();

                    double libraeuro = (eurotolibra * 0.8612); // valor de 1 euro em libras
                    double saldonovo = Math.Round(libraeuro, 2);

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
                }
                else  //Dolares para Libras
                {
                    connection.Open();
                    MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
                    MySqlDataReader reaader = commmand.ExecuteReader();
                    reaader.Read();
                    double dolartolibra = reaader.GetDouble(0);
                    connection.Close();

                    double libradolar = (dolartolibra * 0.7225); // valor de 1 dolar em libras
                    double saldonovo = Math.Round(libradolar, 2);

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
                }
                Class1.moedatipo = "£";
                MessageBox.Show($"O dinheiro foi atualizado para Libras ({Class1.moedatipo})");
            }
        }

        //Minimizar App
        private void MinimizarApp_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //Maximizar App
        private void MaximizarApp_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        //Fechar App
        private void FecharApp_Click(object sender, EventArgs e)
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

        //Arrastar Janela
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        //Arrastar Janela
        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        //Arrastar Janela
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        //Abrir e fechar Painel 1
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
            if (panel1.Visible == false)
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
            Slidepanel.Height = (button1.Height - 15);
            Slidepanel.Top = (button1.Top + 10);
        }

        //MDI
        private Form activeForm = null;

        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            openChildForm(new Consultar());
            Slidepanel.Height = (button2.Height - 15);
            Slidepanel.Top = (button2.Top + 10);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            openChildForm(new Depositos());
            Slidepanel.Height = (button3.Height - 15);
            Slidepanel.Top = (button3.Top + 10);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            openChildForm(new Levantamentos());
            Slidepanel.Height = (button4.Height - 15);
            Slidepanel.Top = (button4.Top + 10);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            openChildForm(new Transferências());
            Slidepanel.Height = (button5.Height - 15);
            Slidepanel.Top = (button5.Top + 10);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            openChildForm(new Donativos());
            Slidepanel.Height = (button6.Height - 15);
            Slidepanel.Top = (button6.Top + 10);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            Slidepanel.Height = (button7.Height - 15);
            Slidepanel.Top = (button7.Top + 10);

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
                Login login = new Login();
                login.ShowDialog();
            }
        }

        private void FecharApp_MouseHover(object sender, EventArgs e)
        {
            FecharApp.Image = Properties.Resources.FecharFinal1;
        }

        private void FecharApp_MouseLeave(object sender, EventArgs e)
        {
            FecharApp.Image = Properties.Resources.FecharFinal3;
        }

        private void MaximizarApp_MouseHover(object sender, EventArgs e)
        {
            MaximizarApp.Image = Properties.Resources.MaximizarFinal;
        }

        private void MaximizarApp_MouseLeave(object sender, EventArgs e)
        {
            MaximizarApp.Image = Properties.Resources.MaximizarFinal3;
        }

        private void MinimizarApp_MouseHover(object sender, EventArgs e)
        {
            MinimizarApp.Image = Properties.Resources.MinimizarFinal;
        }

        private void MinimizarApp_MouseLeave(object sender, EventArgs e)
        {
            MinimizarApp.Image = Properties.Resources.MinimizarFinal3;
        }

        private void panel4_MouseHover(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
        }

        private void panel3_MouseHover(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
        }

        private void panelChildForm_MouseHover(object sender, EventArgs e)
        {
            Slidepanel.Height = (button1.Height - 15);
            Slidepanel.Top = (button1.Top + 10);

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
                pictureBox1.Image = Image.FromStream(mse);
                da.Dispose();
                connection.Close();
            }
            catch
            {
                // caso o utilizador não tenha foto
                connection.Close();
                pictureBox1.Image = Properties.Resources.uploaaad;
                goto SemFoto;
            }

        SemFoto:

            connection.Open();
            MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
            MySqlDataReader reaader = commmand.ExecuteReader();
            reaader.Read();
            saldo = reaader.GetDouble(0);
            connection.Close();
            label_saldo.Text = $"{saldo}{Class1.moedatipo}";

            if (label_saldo.Text.Length == 4)
            {
                label_saldo.Location = new Point(153, 62);
            }
            else if (label_saldo.Text.Length == 5)
            {
                label_saldo.Location = new Point(145, 62);
            }
            else if (label_saldo.Text.Length == 6)
            {
                label_saldo.Location = new Point(135, 62);
            }
            else if (label_saldo.Text.Length <= 3)
            {
                label_saldo.Location = new Point(171, 62);
            }
            else
            {
                label_saldo.Location = new Point(94, 62);
            }

            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
            notificacoespanel.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_hora.Text = "" + DateTime.Now.ToString("HH:mm:ss, dd-MM-yyyy");
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            //meter um efeito para editar
            label14.Visible = true;

            connection.Open();
            MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
            MySqlDataReader reaader = commmand.ExecuteReader();
            reaader.Read();
            saldo = reaader.GetDouble(0);
            connection.Close();
            label_saldo.Text = $"{saldo}{Class1.moedatipo}";

            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            //tirar o efeito para editar
            label14.Visible = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja alterar a sua imagem de perfil?", "Notificação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(opf.FileName);
                }
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            openChildForm(new Terminar());
        }

        private void pictureBox20_MouseClick(object sender, MouseEventArgs e)
        {
            //abrir as Transferências
            panel1.Visible = false;
            panel4.Visible = false;
            panel3.Visible = true;
            openChildForm(new Transferências());
            Slidepanel.Height = (button5.Height - 15);
            Slidepanel.Top = (button5.Top + 10);
        }

        private void pictureBox18_MouseClick(object sender, MouseEventArgs e)
        {
            if (notificacoespanel.Visible == false)
            {
                notificacoespanel.Visible = true;
            }
            else
            {
                notificacoespanel.Visible = false;
            }
        }
    }
}