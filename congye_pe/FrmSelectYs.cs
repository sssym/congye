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
    public partial class FrmSelectYs : Form
    {
        DbConn dbConn = null;
        string strSql = "";
        string str_type = "";
        ArrayList al = null;
        SqlDataAdapter sqlDataAdapter = null;
        DataSet dataSet = null;
        SqlDataReader sqlDataReader = null;
        static string str_tjbh = "";
        public static string str_value = "";
        string str_lb = "";
        Point p ;
        public FrmSelectYs(string type,string lb,Point p1)
        {
            p = p1;
            InitializeComponent();
            dbConn = new DbConn();
            this.Text = "请选择" + type + "的检查医师";
            str_type = type;
            str_lb = lb;
            str_value = "";
        }

        private void FrmSelectYs_Load(object sender, EventArgs e)
        {
            strSql = "select ysxm as 医师姓名 from table_jcys where "+str_lb+"=1";
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            dataGridView1.Columns[0].Width = 230;
            if (dataSet.Tables["table1"].Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = false;
            }
            this.Location = p;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            str_value = "";
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            str_value = dataGridView1[0, e.RowIndex].Value.ToString();
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            str_value = dataGridView1[0, e.RowIndex].Value.ToString();
            this.Close();

        }
    }
}
