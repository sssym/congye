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
    public partial class FrmSendCount : Form
    {
        DataGridView dataGridView;
        string str1;
        public FrmSendCount()
        {
            InitializeComponent();
        }
        public FrmSendCount(DataGridView dv,string str)
        {
            InitializeComponent();
            dataGridView = dv;
            str1 = str;
        }

        private void FrmSendCount_Load(object sender, EventArgs e)
        {
            ClsPublic c = new ClsPublic();
            c.saveXml(dataGridView, str1);
            this.Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = ClsPublic.icount.ToString() + "/" + dataGridView.RowCount.ToString();
        }
    }
}
