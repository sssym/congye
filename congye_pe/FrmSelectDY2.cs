using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace congye_pe
{
    public partial class FrmSelectDY2 : Form
    {
        public static int i_flag = 0;
        public FrmSelectDY2()
        {
            InitializeComponent();
        }

        private void FrmSelectDY2_Load(object sender, EventArgs e)
        {
            i_flag = 0;
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {

                i_flag = 1;
            }
            else if(radioButton2.Checked)
            {
                i_flag = 2;
            }
            this.Close();
        }
    }
}
