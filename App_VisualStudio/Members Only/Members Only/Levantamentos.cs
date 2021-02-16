﻿using System;
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
    public partial class Levantamentos : Form
    {
        MySqlConnection cnn = new MySqlConnection(@"server=127.0.0.1;uid=root;database=members_only");

        public Levantamentos()
        {
            InitializeComponent();
            panel8.Visible = false;
            tempo.Visible = false;
            label3.Text = Class1.moedatipo;

            cnn.Open();
            string bdlevantamentos = $"SELECT Descriçao, Hora, Valor FROM levantamentos WHERE (ID = {Class1.iduser})";
            MySqlCommand cmd = new MySqlCommand(bdlevantamentos, cnn);
            MySqlDataAdapter antiga = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            antiga.Fill(table);
            dataGridView1.DataSource = table;


            MySqlCommand command = new MySqlCommand($"SELECT Saldo FROM registo WHERE(ID = {Class1.iduser})", cnn);
            MySqlDataReader reader = command.ExecuteReader();
            reader.Read();
            _saldo = reader.GetDouble(0);
            Saldo.Text = _saldo.ToString();
            cnn.Close();
        }

        private bool olho = false;

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            if (olho == false)
            {
                pictureBox9.Image = Properties.Resources.eye;
                olho = true;
                panel8.Visible = true;
            }
            else
            {
                pictureBox9.Image = Properties.Resources.eye1;
                olho = false;
                panel8.Visible = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public double _saldo;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == " Montante" || textBox3.Text == " ex.Levantamentos")
                {
                    MessageBox.Show("Todos os campos são obrigatórios!");
                }
                else
                {
                    double retirarvalor = double.Parse(textBox1.Text);
                    if (retirarvalor <= 0)
                    {
                        MessageBox.Show("Introduza um valor válido");
                    }
                    else
                    {
                        if (retirarvalor > _saldo)
                        {
                            MessageBox.Show("Você não têm fundos");
                        }
                        else
                        {
                            double saldofinal = _saldo -= retirarvalor;
                            Saldo.Text = saldofinal.ToString();

                            MySqlDataAdapter adapter = new MySqlDataAdapter();
                            try
                            {
                                cnn.Open();
                                adapter.UpdateCommand = cnn.CreateCommand();
                                adapter.UpdateCommand.CommandText = ($"UPDATE registo SET Saldo = @Saldo WHERE (ID = {Class1.iduser})");
                                adapter.UpdateCommand.Parameters.AddWithValue("@Saldo", saldofinal);
                                adapter.UpdateCommand.ExecuteNonQuery();
                                cnn.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Notificação");
                            }

                            MessageBox.Show($"{retirarvalor}{Class1.moedatipo} foram Levantados!");
                            tempo.Text = DateTime.Now.ToShortTimeString();//recebe a hora atual

                            cnn.Open();
                            MySqlCommand comando = new MySqlCommand($"INSERT INTO levantamentos(Descriçao, Valor, ID, Hora) VALUES (@Descriçao, @Valor, {Class1.iduser}, @Hora)", cnn);
                            comando.Parameters.AddWithValue("@Descriçao", textBox3.Text);
                            comando.Parameters.AddWithValue("@Valor", retirarvalor);
                            comando.Parameters.AddWithValue("@Hora", tempo.Text);

                            comando.ExecuteNonQuery();

                            //atualizar o dataGrid
                            string bdlevantar = $"SELECT Descriçao, Hora, Valor FROM levantamentos WHERE (ID = {Class1.iduser})";
                            MySqlCommand cmd = new MySqlCommand(bdlevantar, cnn);
                            MySqlDataAdapter nova = new MySqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            nova.Fill(table);
                            dataGridView1.DataSource = table;
                            cnn.Close();
                            textBox1.Text = " Montante";
                            textBox3.Text = " ex.Levantamentos";
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Digite somente numeros!");
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == " Montante")
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = " Montante";
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == " ex.Levantamentos")
            {
                textBox3.Text = "";
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = " ex.Levantamentos";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}