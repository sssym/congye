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
    public partial class FrmYssz : Form
    {
        DbConn dbConn = null;
        int if_insert = 1;
        string strSql = "";
        SqlDataAdapter sqlDataAdapter = null;
        DataSet dataSet = null;
        SqlDataReader sqlDataReader = null;
        public FrmYssz()
        {
            InitializeComponent();
            dbConn = new DbConn();
        }

        private void FrmYssz_Load(object sender, EventArgs e)
        {
            strSql = "select id,ysxm as 医师姓名,(case when ys1=1 then '是' else '否' end) as 体征,"
                + "(case when ys2=1 then '是' else '否' end) as 胸片,"
                + "(case when ys3=1 then '是' else '否' end) as 细菌性和阿米巴性痢病杆菌,"
                + "(case when ys4=1 then '是' else '否' end) as 伤寒或副伤寒,"
                + "(case when ys5=1 then '是' else '否' end) as 谷丙转氨酶,"
                + "(case when ys6=1 then '是' else '否' end) as HAV,"
                + "(case when ys7=1 then '是' else '否' end) as HEV "
                + "from table_jcys";
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if_insert = 1;
            textBox1.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if_insert = 0;
            textBox1.Text = dataGridView1[1, e.RowIndex].Value.ToString();
            lbl_id.Text = dataGridView1[0, e.RowIndex].Value.ToString();
            if (dataGridView1[2, e.RowIndex].Value.ToString() == "是")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
            if (dataGridView1[3, e.RowIndex].Value.ToString() == "是")
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }
            if (dataGridView1[4, e.RowIndex].Value.ToString() == "是")
            {
                checkBox3.Checked = true;
            }
            else
            {
                checkBox3.Checked = false;
            }
            if (dataGridView1[5, e.RowIndex].Value.ToString() == "是")
            {
                checkBox4.Checked = true;
            }
            else
            {
                checkBox4.Checked = false;
            }
            if (dataGridView1[6, e.RowIndex].Value.ToString() == "是")
            {
                checkBox5.Checked = true;
            }
            else
            {
                checkBox5.Checked = false;
            }
            if (dataGridView1[7, e.RowIndex].Value.ToString() == "是")
            {
                checkBox6.Checked = true;
            }
            else
            {
                checkBox6.Checked = false;
            }
            if (dataGridView1[8, e.RowIndex].Value.ToString() == "是")
            {
                checkBox7.Checked = true;
            }
            else
            {
                checkBox7.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ys1 = "0";
            string ys2 = "0";
            string ys3 = "0";
            string ys4 = "0";
            string ys5 = "0";
            string ys6 = "0";
            string ys7 = "0";
            string ys8= "0";
            if (checkBox1.Checked)
            {
                ys1 = "1";
            }
            if (checkBox2.Checked)
            {
                ys2 = "1";
            }
            if (checkBox3.Checked)
            {
                ys3 = "1";
            }
            if (checkBox4.Checked)
            {
                ys4= "1";
            }
            if (checkBox5.Checked)
            {
                ys5 = "1";
            }
            if (checkBox6.Checked)
            {
                ys6 = "1";
            }
            if (checkBox7.Checked)
            {
                ys7 = "1";
            }
            if (checkBox8.Checked)
            {
                ys8 = "1";
            }
            if (if_insert == 0)
            {
                strSql = "update table_jcys set ysxm='" + textBox1.Text + "',"
                    + "ys1=" + ys1 + ","
                    + "ys2=" + ys2 + ","
                    + "ys3=" + ys3 + ","
                    + "ys4=" + ys4 + ","
                    + "ys5=" + ys5 + ","
                    + "ys6=" + ys6 + ","
                    + "ys7=" + ys7 + ","
                    + "ys8="+ys8+" where id='" + lbl_id.Text + "'";
                if (dbConn.GetSqlCmd(strSql) != 0)
                {
                    MessageBox.Show("保存成功！");
                    
                }
            }
            else if(if_insert==1)
            {
                strSql = "insert into table_jcys(ysxm,ys1,ys2,ys3,ys4,ys5,ys6,ys7,ys8) values('"+textBox1.Text+"'," + ys1 + "," + ys2 + "," + ys3 + "," + ys4 + "," + ys5 + "," + ys6 + "," + ys7+","+ys8 + ")";
                if (dbConn.GetSqlCmd(strSql) != 0)
                {
                    MessageBox.Show("保存成功！");
                    strSql = "select max(id) from table_jcys";
                    sqlDataReader = dbConn.GetDataReader(strSql);
                    if (sqlDataReader.Read())
                    {
                        lbl_id.Text = sqlDataReader.GetValue(0).ToString();
                        if_insert = 0;
                    }
                }
            }
            FrmYssz_Load(sender,e);


        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // strSql = "insert into table_jcys(ysxm,ys1,ys2,ys3,ys4,ys5,ys6,ys7,ys8) values('" + textBox1.Text + "'," + ys1 + "," + ys2 + "," + ys3 + "," + ys4 + "," + ys5 + "," + ys6 + "," + ys7 + "," + ys8 + ")";
            strSql = "delete from table_jcys where id='" + lbl_id.Text + "'";
            if (dbConn.GetSqlCmd(strSql) != 0)
            {
                MessageBox.Show("保存成功！");

                lbl_id.Text = "";
                textBox1.Text = "";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                if_insert = 0;

            }
            FrmYssz_Load(sender, e);
        }
    }
}
