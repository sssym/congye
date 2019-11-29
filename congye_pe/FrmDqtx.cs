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
    public partial class FrmDqtx : Form
    {
        DbConn dbConn = null;
        string strSql = "";
        SqlDataAdapter sqlDataAdapter;
        DataSet dataSet;
        public FrmDqtx()
        {
            InitializeComponent();
            dbConn = new DbConn();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            strSql = "select djrq as 登记日期,tjbh as 体检编号,xm as 姓名,gzdw as 工作单位,lxdh as 联系电话,case when clzt!=0 then '已处理' else '未处理' end as 处理状态,'选中' as 选中 from table_renyuan where isdelete=0 and djrq>='"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"' and djrq<'"+dateTimePicker2.Value.AddDays(1).ToString()+"'";
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            for (int i = 1; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
            }
            
        }
    }
}
