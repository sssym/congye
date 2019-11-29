using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
namespace congye_pe
{
    public partial class FrmSetupMain :Form
    {
        int i_mouseClick = 0;
        DbConn dbConn = null;
        ArrayList arrayList = new ArrayList();
        string str_yhqx = "";
        public FrmSetupMain()
        {
            InitializeComponent(); i_mouseClick = 0;
            dbConn = new DbConn();
            str_yhqx = FrmLogin.str_yhqx;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (str_yhqx[7].ToString() == "1")
            {
                FrmUser f = new FrmUser();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有操作本功能的权限！");
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (str_yhqx[8].ToString() == "1")
            {
                FrmYssz f = new FrmYssz();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有操作本功能的权限！");
            }


        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (str_yhqx[9].ToString() == "1")
            {
                FrmXxsz f = new FrmXxsz();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有操作本功能的权限！");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (str_yhqx[10].ToString() == "1")
            {
                FrmSjwh f = new FrmSjwh();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("您没有操作本功能的权限！");
            }

         
        }

        private void FrmSetupMain_MouseClick(object sender, MouseEventArgs e)
        {
            i_mouseClick++;
            if (i_mouseClick == 5)
            {
               DialogResult dr1= MessageBox.Show("确认初始化吗？","提醒！",MessageBoxButtons.YesNo);
                if (dr1.Equals(DialogResult.Yes))
                {
                     dr1 = MessageBox.Show("确认初始化吗？", "提醒！", MessageBoxButtons.YesNo);
                    if (dr1.Equals(DialogResult.Yes))
                    {
                        dr1 = MessageBox.Show("确认初始化吗？", "提醒！", MessageBoxButtons.YesNo);
                        if (dr1.Equals(DialogResult.Yes))
                        {
                            arrayList.Add("update table_canshu set value = '" + DateTime.Now.Year.ToString() + "000001'");
                            arrayList.Add("delete from table_jcys");
                            arrayList.Add("delete from table_morenzhi");
                            arrayList.Add("delete from table_renyuan");
                            arrayList.Add("delete from table_tjjg");
                            arrayList.Add("delete from table_yh where yhbm!='001'");
                            if (dbConn.GetTransaction(arrayList))
                            {
                                MessageBox.Show("初始化完成！");
                            }

                        }

                    }
                }
            }
        }

        private void FrmSetupMain_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (str_yhqx[11].ToString() == "1")
            {
                FrmDqtjh f = new FrmDqtjh();
                f.ShowDialog();
            }
            else
            {

                MessageBox.Show("您没有操作本功能的权限！");
            }
        }
    }
}
