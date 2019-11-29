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
    public partial class FrmSelect : Form
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
        public FrmSelect(string type)
        {
            InitializeComponent();
            dbConn = new DbConn();
            this.Text = "请选择"+type+"的检查结果";
            str_type = type;
            str_value = "";
        }

        private void FrmSelect_Load(object sender, EventArgs e)
        {
            strSql = "select value as 值 from table_set where name='" + str_type + "'"; 
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            dataGridView1.Columns[0].Width = 230;
            if (dataSet.Tables["table1"].Rows.Count > 0)
            {
                dataGridView1.Rows[0].Selected = false;
            }

        }

      

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

         
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            str_value = dataGridView1[0, e.RowIndex].Value.ToString();
            this.Close();
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
            str_value = dataGridView1[0, e.RowIndex].Value.ToString();
            this.Close();
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            str_value = dataGridView1[0, e.RowIndex].Value.ToString();
            this.Close();
        }
    }
}
