using System;
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
    public partial class FrmSjwh : Form
    {
        DbConn dbConn = null;
        string strSql = "";
        ArrayList al = null;
        SqlDataAdapter sqlDataAdapter = null;
        DataSet dataSet = null;
        SqlDataReader sqlDataReader = null;
        public FrmSjwh()
        {
            InitializeComponent();
            dbConn = new DbConn();
        }

        private void FrmSjwh_Load(object sender, EventArgs e)
        {
            strSql = "select [type],[name] as 参数类型 from table_set group by [type],[name] order by [type]";
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            dataGridView1.Columns[0].Visible = false;
            DataGridViewComboBoxColumn combox = new DataGridViewComboBoxColumn();
            //DataGridViewComboBoxColumn combox1 = new DataGridViewComboBoxColumn();

            combox.HeaderText = "默认值";
           // combox1.HeaderText = "默认医生";


            //combox.DataSource = dataSet.Tables["table2"].Columns;
            //combox.HeaderText = s;
            try
            {
                dataGridView1.Columns.Add(combox);
               // dataGridView1.Columns.Add(combox1);
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    strSql = "select [value] from table_set where [type]='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                    sqlDataReader = dbConn.GetDataReader(strSql);
                    DataGridViewComboBoxCell cell = dataGridView1.Rows[i].Cells[2] as DataGridViewComboBoxCell;
                    while (sqlDataReader.HasRows)
                    {
                        
                        while (sqlDataReader.Read())
                        {
                            cell.Items.Add(sqlDataReader.GetValue(0).ToString());
                            // (DataGridViewComboBoxCell)(dataGridView1.Rows[i].Cells[2]).Items.Add(sqlDataReader.GetValue(0).ToString());

                        }
                        
                        sqlDataReader.NextResult();
                    }
                    //strSql = "select ysxm from table_jcys where ys"+(i+1).ToString()+"=1";
                    //sqlDataReader = dbConn.GetDataReader(strSql);
                    //DataGridViewComboBoxCell cell1 = dataGridView1.Rows[i].Cells[3] as DataGridViewComboBoxCell;
                    //while (sqlDataReader.HasRows)
                    //{

                    //    while (sqlDataReader.Read())
                    //    {
                    //        cell1.Items.Add(sqlDataReader.GetValue(0).ToString());
                    //        // (DataGridViewComboBoxCell)(dataGridView1.Rows[i].Cells[2]).Items.Add(sqlDataReader.GetValue(0).ToString());

                    //    }

                    //    sqlDataReader.NextResult();
                    //}
                    strSql = "select [value] from table_set where sfmrz=1 and [type]='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                    SqlDataReader sqlDataReader1 = dbConn.GetDataReader(strSql);
                    if (sqlDataReader1.Read())
                    {
                        cell.Value = sqlDataReader1.GetValue(0).ToString();
                    }
                    //strSql = "select table_jcys.ysxm from table_jcys,table_ysmrz "
                    //    + " where table_jcys.id = table_ysmrz.ys and [settype]='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                    //sqlDataReader1 = dbConn.GetDataReader(strSql);
                    //if (sqlDataReader1.Read())
                    //{
                    //    cell1.Value = sqlDataReader1.GetValue(0).ToString();
                    //}
                }
              


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            al = new ArrayList();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
              
                strSql = "update table_set set sfmrz=1 where value='"+dataGridView1.Rows[i].Cells[2].Value.ToString()+"' and type='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                al.Add(strSql);
                strSql = "update table_set set sfmrz=0 where value!='" + dataGridView1.Rows[i].Cells[2].Value.ToString() + "' and type='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'";
                al.Add(strSql);
                if(i==14)
                MessageBox.Show(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }
            if (dbConn.GetTransaction(al))
            {
                MessageBox.Show("保存成功！");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
