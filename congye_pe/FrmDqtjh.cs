using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace congye_pe
{
    public partial class FrmDqtjh : Form
    {
        string strSql = "";
        DbConn dbConn = null;
        SqlDataAdapter sqlDataAdapter = null;
        DataSet dataSet = null;
        ArrayList arrayList;
        public FrmDqtjh()
        {
            dbConn = new DbConn();
            InitializeComponent();
        }

        private void FrmDqtjh_Load(object sender, EventArgs e)
        {
            strSql = "select [type] as 参数编号,beizhu as 参数名称,value as 参数值 from table_canshu";
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            arrayList = new ArrayList();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                strSql = "update table_canshu set value='"+dataGridView1[2,i].Value.ToString()+"' where [type]='"+dataGridView1[0,i].Value.ToString()+"'";
                arrayList.Add(strSql);
            }
            if(dbConn.GetTransaction(arrayList))
            {

                MessageBox.Show("保存成功！");
            }
        }
    }
}
