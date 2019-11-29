using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace congye_pe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClsBase64 c = new ClsBase64();
            Bitmap b = new Bitmap("1.jpg");
            textBox1.Text = Base64Tobyte16(c.ImgToBase64String(b));
        }
        public static string Base64Tobyte16(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            string a = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                // a += bytes[i].ToString(byte);
                decode += bytes[i].ToString("X2");
            }

            return decode;
        }
    }
}
