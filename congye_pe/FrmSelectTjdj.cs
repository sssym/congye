using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace congye_pe
{
    public partial class FrmSelectTjdj : Form
    {
        public FrmSelectTjdj()
        {
            InitializeComponent();
        }

        private void FrmSelectTjdj_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                FrmTjdj f = new FrmTjdj();
                this.Hide();
                f.ShowDialog();

            }
            else
            {
                FrmRenYuan f = new FrmRenYuan();
                this.Hide();
                f.ShowDialog();

            }
        }
    }
}
