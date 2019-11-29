using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace congye_pe
{
    public partial class FrmSelectSCZT : Form
    {
        public static string str_date = "";
        public FrmSelectSCZT()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Visible = false;
        }

        private void FrmSelectSCZT_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                str_date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            }
            else if (radioButton2.Checked)
            {
                str_date = "1";
            }
            else if (radioButton3.Checked)
            {
                str_date = "2";
            }
            this.Close();

        }
    }
}
