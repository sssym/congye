using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace congye_pe
{
    public partial class FrmGrzx : Form
    {
        DbConn dbConn = null;
        string strSql = "";
        string strYhmm = "";
        SqlDataReader sqlDataReader = null;
        ClsBase64 clsBase64 ;
        public FrmGrzx()
        {
            InitializeComponent();
            dbConn = new DbConn(); clsBase64 = new ClsBase64();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmGrzx_Load(object sender, EventArgs e)
        {
            label6.Text = FrmLogin.str_yhbm;
            label5.Text = FrmLogin.str_yhxm;
            strSql = "select yhmm from table_yh where yhbm='"+ FrmLogin.str_yhbm+"'";
            sqlDataReader = dbConn.GetDataReader(strSql);
            if (sqlDataReader.Read())
            {
               
                strYhmm = clsBase64.Decodebase64(sqlDataReader.GetValue(0).ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == strYhmm)
            {
                if (textBox2.Text == textBox3.Text)
                {

                    strSql = "update table_yh set yhmm='" + clsBase64.Encodebase64(textBox2.Text) + "' where yhbm='" + FrmLogin.str_yhbm + "'";
                    if (dbConn.GetSqlCmd(strSql) != 0)
                    {
                        MessageBox.Show("更改成功！");
                    }
                }
                else
                {
                    MessageBox.Show("两次输入的密码不一致！");
                }
            }
            else
            {

                MessageBox.Show("原始密码不正确！");return;
            }
        }
    }
}
