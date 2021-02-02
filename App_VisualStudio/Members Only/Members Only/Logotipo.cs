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
    public partial class Logotipo : Form
    {
        public Logotipo()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Data.Text = "Time: " + DateTime.Now.ToString("dd-MM-yyyy, HH:mm");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            progressBar1.Increment(3);
            if (progressBar1.Value == 100)
            {
                timer2.Enabled = false;
                Login form = new Login();
                form.Show();
                this.Hide();
            }
        }
    }
}