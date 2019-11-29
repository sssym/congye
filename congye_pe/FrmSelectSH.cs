using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace congye_pe
{
    public partial class FrmSelectSH : Form
    {
       public static int i_flag ;
        public FrmSelectSH()
        {
            InitializeComponent();
        }

        private void FrmSelectSH_FormClosing(object sender, FormClosingEventArgs e)
        {
           // i_flag = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                i_flag = 1;
            }
            if (radioButton2.Checked)
            {
                i_flag = 2;
            }
            this.Close();
        }

        private void FrmSelectSH_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            i_flag = 0;
        }
    }
}
