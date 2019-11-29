using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections;

namespace congye_pe
{
    public partial class FrmLogin : Form
    {
        DbConn dbConn = null;
        string strSql = "";
        SqlDataReader sqlDataReader = null;
        public static string str_yhxm = "";
        public static string str_yhqx = "";
        public static string str_yhbm = "";
        ClsBase64 clsBase64 = null;
        public FrmLogin()
        {
            InitializeComponent();
            dbConn = new DbConn();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            strSql = "select yhbm from table_yh where sfty=0";
            sqlDataReader = dbConn.GetDataReader(strSql);
            comboBox1.Items.Clear();
            while (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    comboBox1.Items.Add(sqlDataReader.GetValue(0).ToString());

                    
                }
                sqlDataReader.NextResult();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                clsBase64 = new ClsBase64();
                str_yhbm = comboBox1.SelectedItem.ToString();
                string str_yhmm = clsBase64.Encodebase64(textBox1.Text);
                //string str_yhmm = textBox1.Text;
                strSql = "select count(*) from table_canshu where type=1 and value like '"+DateTime.Now.Year+"%'";
                sqlDataReader = dbConn.GetDataReader(strSql);
                if (sqlDataReader.Read())
                {
                    if (sqlDataReader.GetValue(0).ToString() == "0")
                    {
                        strSql = "UPDATE table_canshu SET value='"+DateTime.Now.Year+ "000001' where type=1";
                        dbConn.GetSqlCmd(strSql);

                    }
                }

                strSql = "select yhxm,yhqx from table_yh where yhbm='" + str_yhbm + "' and yhmm='" + str_yhmm + "'";
                sqlDataReader = dbConn.GetDataReader(strSql);
                if (sqlDataReader.Read())
                {

                    FrmMain a = new FrmMain();
                    str_yhxm = sqlDataReader.GetValue(0).ToString();
                    str_yhqx = sqlDataReader.GetValue(1).ToString();

                    this.Hide();
                    a.ShowDialog();
                    //this.Close();
                    //Application.Exit();
                }
                else
                {
                    MessageBox.Show("密码错误！");
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show("未选择用户名"+EX.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int i = 100;

           i=  ClassDLL.DLLInit(@"http://219.135.157.134:9090/jkz","");
            MessageBox.Show(i.ToString());
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
