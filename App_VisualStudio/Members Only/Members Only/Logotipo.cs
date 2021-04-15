using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Members_Only
{
    public partial class Logotipo : Form
    {
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

        public Logotipo()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
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