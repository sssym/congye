using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace congye_pe
{
    public partial class FrmSelectDY : Form
    {
        public static int i_check_index=-1 ;
        public FrmSelectDY()
        {
            InitializeComponent();
        }

        private void FrmSelectDY_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            i_check_index = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            { i_check_index = 0; }
            else if(radioButton2.Checked)
            { i_check_index = 1; }
            this.Close();
        }

        private void FrmSelectDY_FormClosing(object sender, FormClosingEventArgs e)
        {
            //i_check_index = -1;
        }
    }
}
