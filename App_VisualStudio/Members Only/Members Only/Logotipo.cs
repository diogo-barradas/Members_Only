using System;
using System.Windows.Forms;

namespace Members_Only
{
    public partial class Logotipo : Form
    {
        public Logotipo()
        {
            InitializeComponent();
            progressBar1.Visible = false;
            Data.Visible = false;
            Data2.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Data.Text = "" + DateTime.Now.ToString("HH:mm:ss, dd-MM-yyyy");
            Data2.Text = "" + DateTime.Now.ToString("HH:mm:ss, dd-MM-yyyy");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            progressBar1.Increment(3);
            if (progressBar1.Value <= 34.99)
            {
                Data2.Visible = true;
            }
            else if(progressBar1.Value == 100)
            {
                timer2.Enabled = false;
                Login form = new Login();
                form.Show();
                this.Hide();
            }
            else
            {
                Data2.Visible = false;
                Data.Visible = true;
            }
        }
    }
}