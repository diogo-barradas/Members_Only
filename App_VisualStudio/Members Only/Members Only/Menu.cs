using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Members_Only
{
    public partial class Menu : Form
    {
        public double saldo;
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);

        MySqlConnection connection = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");
        private string _username;

        public Menu()
        {
            InitializeComponent();
            
            panel3.Visible = false;
            panel1.Visible = false;
            Slidepanel.Height = (button1.Height - 15);
            Slidepanel.Top = (button1.Top + 10);
            Class1.moedatipo = "€"; //euro é a moeda caso o user nao mude 

            connection.Open();
            MySqlCommand command = new MySqlCommand($"SELECT Username FROM registo WHERE(ID = {Class1.iduser})", connection);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            _username = reader.GetString(0);
            connection.Close();

            connection.Open();
            MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
            MySqlDataReader reaader = commmand.ExecuteReader();
            reaader.Read();
            saldo = reaader.GetDouble(0);
            connection.Close();

            label_nome.Text = $"Nome: {_username}";
            label_id.Text = $"ID: {Class1.iduser}";
            label_saldo.Text = $"Saldo: {saldo}{Class1.moedatipo}";
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
            openChildForm(new Terminar());
            Slidepanel.Height = (button7.Height - 15);
            Slidepanel.Top = (button7.Top + 10);
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

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
            toolTip.SetToolTip(pictureBox2, $"Members Only é uma empresa de pagamentos online.\n\n{_username} você é o nosso utilizador número {Class1.iduser}.");
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
            connection.Open();
            MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
            MySqlDataReader reaader = commmand.ExecuteReader();
            reaader.Read();
            saldo = reaader.GetDouble(0);
            connection.Close();

            label_saldo.Text = $"Saldo: {saldo}{Class1.moedatipo}";

            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_hora.Text = "" + DateTime.Now.ToString("HH:mm:ss, dd-MM-yyyy");
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.exemplo_user2;

            connection.Open();
            MySqlCommand commmand = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", connection);
            MySqlDataReader reaader = commmand.ExecuteReader();
            reaader.Read();
            saldo = reaader.GetDouble(0);
            connection.Close();

            label_saldo.Text = $"Saldo: {saldo}{Class1.moedatipo}";

            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.exemplo_user;
        }

        private void pictureBox20_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Visible = false;
            panel4.Visible = false;
            panel3.Visible = true;
            openChildForm(new Transferências());
            Slidepanel.Height = (button5.Height - 15);
            Slidepanel.Top = (button5.Top + 10);
        }
    }
}