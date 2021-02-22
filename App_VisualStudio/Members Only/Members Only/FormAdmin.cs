using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Members_Only
{
    public partial class FormAdmin : Form
    {
        private bool dragging = false;
        private Point startPoint = new Point(0, 0);
        MySqlConnection cnn = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");
        MySqlCommand alo;
        MySqlDataAdapter baz;

        public FormAdmin()
        {
            InitializeComponent();

            button5.Visible = true;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;

            cnn.Open();
            string tabela = $"SELECT ID,Username,Idade,Email,Morada FROM registo;";
            MySqlCommand cmd = new MySqlCommand(tabela, cnn);
            MySqlDataAdapter antiga = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            antiga.Fill(table);
            dataGridView1.DataSource = table;
            cnn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;

            //atualizar o dataGrid
            cnn.Open();
            string bduser = $"SELECT ID,Username,Idade,Email,Morada FROM registo;";
            MySqlCommand cmd = new MySqlCommand(bduser, cnn);
            MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            nova.Fill(table);
            dataGridView1.DataSource = table;
            cnn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            button6.Visible = true;
            button7.Visible = false;
            button8.Visible = false;

            //atualizar o dataGrid
            cnn.Open();
            string bddeposito = $"SELECT * FROM depositos;";
            MySqlCommand cmd = new MySqlCommand(bddeposito, cnn);
            MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            nova.Fill(table);
            dataGridView1.DataSource = table;
            cnn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = true;
            button8.Visible = false;

            //atualizar o dataGrid
            cnn.Open();
            string bdlevantar = $"SELECT * FROM levantamentos;";
            MySqlCommand cmd = new MySqlCommand(bdlevantar, cnn);
            MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            nova.Fill(table);
            dataGridView1.DataSource = table;
            cnn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = true;

            //atualizar o dataGrid
            cnn.Open();
            string bdtrans = $"SELECT * FROM transferencias;";
            MySqlCommand cmd = new MySqlCommand(bdtrans, cnn);
            MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            nova.Fill(table);
            dataGridView1.DataSource = table;
            cnn.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Menu guest = new Menu();
            guest.ShowDialog();
        }

        //Arrastar Janela
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.FecharFinal2;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.MaximizarFinal1;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.MinimizarFinal1;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.exitfinal;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.exitfinal1;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.MinimizarFinal;
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.MaximizarFinal;
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.FecharFinal1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //apagar row da tabela
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int id = Convert.ToInt32(selected);
                if (id == 1)
                {
                    MessageBox.Show("O admin nao pode ser apagado!");
                }
                else
                {
                    string sql = "DELETE FROM registo WHERE ID =" + id + "";
                    alo = new MySqlCommand(sql, cnn);
                    cnn.Open();
                    try
                    {
                        baz = new MySqlDataAdapter(alo);
                        baz.DeleteCommand = cnn.CreateCommand();
                        baz.DeleteCommand.CommandText = sql;
                        if (MessageBox.Show("Tem a certeza ?", "Notificação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (alo.ExecuteNonQuery() > 0)
                            {
                                string bduser = $"SELECT ID,Username,Idade,Email,Morada FROM registo;";
                                MySqlCommand cmd = new MySqlCommand(bduser, cnn);
                                MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
                                DataTable table = new DataTable();
                                nova.Fill(table);
                                dataGridView1.DataSource = table;
                                MessageBox.Show("Utilizador apagado!");
                            }
                        }
                    }
                    catch (MySqlException)
                    {
                        MessageBox.Show("Para apagar este Utilizador você precisa :\n - Eliminar os seus Depósitos.\n - Eliminar os seus Levantamentos.\n - Eliminar as suas Transferências.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Notificação ");
                    }
                    finally
                    {
                        cnn.Close();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione uma row!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //apagar row da tabela
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int id = Convert.ToInt32(selected);
                string sql = "DELETE FROM depositos WHERE idDepositos =" + id + "";
                alo = new MySqlCommand(sql, cnn);
                cnn.Open();
                try
                {
                    baz = new MySqlDataAdapter(alo);
                    baz.DeleteCommand = cnn.CreateCommand();
                    baz.DeleteCommand.CommandText = sql;
                    if (MessageBox.Show("Tem a certeza ?", "Notificação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (alo.ExecuteNonQuery() > 0)
                        {
                            string bddeposito = $"SELECT * FROM depositos;";
                            MySqlCommand cmd = new MySqlCommand(bddeposito, cnn);
                            MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            nova.Fill(table);
                            dataGridView1.DataSource = table;
                            MessageBox.Show("Depósito apagado!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Notificação ");
                }
                finally
                {
                    cnn.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione uma row!");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                //apagar row da tabela
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int id = Convert.ToInt32(selected);
                string sql = "DELETE FROM levantamentos WHERE idLevantamentos =" + id + "";
                alo = new MySqlCommand(sql, cnn);
                cnn.Open();
                try
                {
                    baz = new MySqlDataAdapter(alo);
                    baz.DeleteCommand = cnn.CreateCommand();
                    baz.DeleteCommand.CommandText = sql;
                    if (MessageBox.Show("Tem a certeza ?", "Notificação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (alo.ExecuteNonQuery() > 0)
                        {
                            string bdlevantar = $"SELECT * FROM levantamentos;";
                            MySqlCommand cmd = new MySqlCommand(bdlevantar, cnn);
                            MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            nova.Fill(table);
                            dataGridView1.DataSource = table;
                            MessageBox.Show("Levantamento apagado!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Notificação ");
                }
                finally
                {
                    cnn.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione uma row!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                //apagar row da tabela
                string selected = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int id = Convert.ToInt32(selected);
                string sql = "DELETE FROM transferencias WHERE idTransferencias =" + id + "";
                alo = new MySqlCommand(sql, cnn);
                cnn.Open();
                try
                {
                    baz = new MySqlDataAdapter(alo);
                    baz.DeleteCommand = cnn.CreateCommand();
                    baz.DeleteCommand.CommandText = sql;
                    if (MessageBox.Show("Tem a certeza ?", "Notificação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (alo.ExecuteNonQuery() > 0)
                        {
                            string bdtrans = $"SELECT * FROM transferencias;";
                            MySqlCommand cmd = new MySqlCommand(bdtrans, cnn);
                            MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            nova.Fill(table);
                            dataGridView1.DataSource = table;
                            MessageBox.Show("Transferência apagada!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Notificação ");
                }
                finally
                {
                    cnn.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione uma row!");
            }
        }
    }
}