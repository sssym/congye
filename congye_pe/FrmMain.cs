using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace congye_pe
{
    public partial class FrmMain : Form
    {
        ArrayList al1 = null;
        string str_yhqx = "";
        public FrmMain()
        {
            InitializeComponent();
            str_yhqx = FrmLogin.str_yhqx;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (str_yhqx[0] == '1')
            {

                //FrmTjdj f = new FrmTjdj();

                //f.ShowDialog();
                FrmSelectTjdj f = new FrmSelectTjdj();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该操作权限！");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmTjjglr f = new FrmTjjglr();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (str_yhqx[4] == '1')
            {
                FrmJcjl f = new FrmJcjl();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该操作权限！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (str_yhqx[6] == '1')
            {
                FrmZjdy f = new FrmZjdy();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该操作权限！");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void jcsz_Click(object sender, EventArgs e)
        {
            //FrmJcsjwh f = new FrmJcsjwh();
            //f.ShowDialog();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            FrmSjwh a = new FrmSjwh();
            a.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            if (str_yhqx[8] == '1')
            {

                FrmYssz a = new FrmYssz();
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该操作权限！");
            }
        }

        private void 用户维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (str_yhqx[10] == '1')
            {

                FrmUser a = new FrmUser();
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该操作权限！");
            }

        }

        private void 检查医师维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (str_yhqx[9] == '1')
            {

                FrmYssz a = new FrmYssz();
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该操作权限！");
            }

        }

        private void 选项默认值维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (str_yhqx[8] == '1')
            {

                FrmSjwh a = new FrmSjwh();
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该操作权限！");
            }
        }

        private void 选项值维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (str_yhqx[7] == '1')
            {
                FrmXxsz f = new FrmXxsz();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该操作权限！");
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

            TsLabUser.Text = "当前用户：" + FrmLogin.str_yhxm;
            str_yhqx = FrmLogin.str_yhqx;
            toolStripStatusLabel1.Text = "使用单位：" + DbConn.HOSNAME;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TsLabTime.Text = "日期:" + DateTime.Now.Date.ToString("yyyy年MM月dd日") + "  " + "时间:" + DateTime.Now.ToLongTimeString();

        }

        private void 数据备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if (str_yhqx[7].ToString() == "1" || str_yhqx[8].ToString() == "1" || str_yhqx[9].ToString() == "1" || str_yhqx[10].ToString() == "1" )
            {
                FrmSetupMain a = new FrmSetupMain();
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有该操作权限！");
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (str_yhqx[5].ToString() == "1")
            {
                FrmSjsc a = new FrmSjsc();
                a.ShowDialog();
            }

            else
            {
                MessageBox.Show("您没有该操作权限！");
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            if (str_yhqx[12].ToString() == "1")
            {
                FrmGrzx f = new FrmGrzx();
                f.ShowDialog();
            }
            else
            {

                MessageBox.Show("您没有该操作权限！");
            }
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
