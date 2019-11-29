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
    public partial class FrmXxsz : Form
    {
        string strSql = "";
        SqlDataAdapter sqlDataAdapter;
        DbConn dbConn = null;
        DataSet dataSet;
        string i_id = "";
        public FrmXxsz()
        {
            InitializeComponent();
            dbConn = new DbConn();
        }
        private void LoadGv()
        {
            strSql = "select id,name as 参数类型,value as 参数 from table_set order by type ";
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;


            dataGridView1.Columns[0].Visible = false;



        }

        private void FrmXxsz_Load(object sender, EventArgs e)
        {
            LoadGv();
            button1.Enabled = true;
            button3.Enabled = false;
            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            comboBox1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            strSql = "delete from table_set where id=" + i_id;
            if (dbConn.GetSqlCmd(strSql) != 0)
            {
                MessageBox.Show("删除成功！");
                LoadGv();
            }
            else
            {
                MessageBox.Show("删除失败！");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = true;
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            textBox1.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            button1.Enabled = true;
            button4.Enabled = true;
            button3.Enabled = false;
            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            comboBox1.SelectedItem = dataGridView1[1, e.RowIndex].Value.ToString();
            textBox1.Text = dataGridView1[2, e.RowIndex].Value.ToString();
            i_id = dataGridView1[0, e.RowIndex].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                return;
            }
            strSql = "insert into table_set(type,name,value) values(" + comboBox1.SelectedIndex.ToString() + ",'" + comboBox1.SelectedItem.ToString() + "','" + textBox1.Text + "')";
            if (dbConn.GetSqlCmd(strSql) != 0)
            {
                MessageBox.Show("新增成功！");
                button4.Enabled = true;
                button1.Enabled = true;
                button3.Enabled = false;
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
                    LoadGv();
            }
            else
            { MessageBox.Show("修改失败！"); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
