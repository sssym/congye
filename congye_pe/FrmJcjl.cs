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
    public partial class FrmJcjl : Form
    {
        DbConn dbConn = null;
        string strSql = "";
        ArrayList al = null;
        SqlDataAdapter sqlDataAdapter = null;
        DataSet dataSet = null;
        SqlDataReader sqlDataReader = null;
        static string str_tjbh = "";
        FrmTjjglr frmTjjglr = null;
        public FrmJcjl()
        {
            InitializeComponent();
            dbConn = new DbConn();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            strSql = "select table_renyuan.tjbh as 体检编号,xm as 姓名 from table_renyuan,table_tjjg where  table_renyuan.tjbh=table_tjjg.tjbh and table_tjjg.sfbclr=1  ";

            if (comboBox1.SelectedIndex == 0)
            {
                strSql = strSql + " and sfzhm like '%" + textBox2.Text + "%'";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                strSql = strSql + " and table_renyuan.tjbh like '%" + textBox2.Text + "%'";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                strSql = strSql + " and xm like '%" + textBox2.Text + "%'";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                strSql = strSql + " and gzdw like '%" + textBox2.Text + "%'";
            }
            if (checkBox1.Checked)
            {
                strSql = strSql + " and djrq >= '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and djrq<'"+dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd")+"'";
            }
          if (radioButton1.Checked)
            {
                strSql = strSql + " and sfsh=0 and (isdelete!=1 or isdelete is null) order by table_renyuan.tjbh desc";
            }
            if (radioButton2.Checked)
            {
                strSql = strSql + " and sfsh=1 and  (isdelete!=1 or isdelete is null) order by table_renyuan.tjbh desc";
            }
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                strSql = "select table_renyuan.tjbh as 体检编号,xm as 姓名 from table_renyuan,table_tjjg "
                    + "where table_tjjg.tjbh=table_renyuan.tjbh and table_tjjg.sfbclr=1 and sfsh=0 and (isdelete!=1 or isdelete is null) order by table_renyuan.tjbh desc";// "and djrq='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
                sqlDataAdapter = dbConn.GetDataAdapter(strSql);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "table1");
                dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                strSql = "select tjbh as 体检编号,xm as 姓名 from table_renyuan where sfsh!=0 and (isdelete!=1 or isdelete is null) order by tjbh desc";// and djrq='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
                sqlDataAdapter = dbConn.GetDataAdapter(strSql);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "table1");
                dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tjbh.Text == null || tjbh.Text == "")
            {
                MessageBox.Show("请选择体检人员！");
                return;
            }

            strSql = "update table_tjjg set sfbcjl=1 , jcjl='" + textBox1.Text + "',jcjgyj='" + textBox3.Text + "',ys8='"+ys1.Text+"' where tjbh='" + tjbh.Text + "'";
            if (dbConn.GetSqlCmd(strSql) != 0)
            {
                MessageBox.Show("保存成功！");
                pictureBox6.Enabled = true;
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void FrmJcjl_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            pictureBox4.Enabled = false;
            pictureBox5.Enabled = false;
            pictureBox6.Enabled = false;
            comboBox1.SelectedIndex = 0;
            tjbh.Text = "";
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            ys1.Enabled = false;
            label3.Text = "";
            //button2.Enabled = false;
            //button3.Enabled = false;
            //button4.Enabled = false;
        }
        private Boolean shenhe()
        {
            strSql = "select ys1,ys2,ys3,ys4,ys5,gangongneng2,ys6,gangongneng3,ys7,ys8 from table_tjjg where tjbh='" + tjbh.Text + "'";
           SqlDataReader sqlDataReader1 = dbConn.GetDataReader(strSql);
            if (sqlDataReader1.Read())
            {
                if (sqlDataReader1.GetValue(0).ToString() == "" || sqlDataReader1.GetValue(1).ToString() == ""
                    || sqlDataReader1.GetValue(2).ToString() == "" || sqlDataReader1.GetValue(3).ToString() == ""
                    || sqlDataReader1.GetValue(4).ToString() == "")
                {
                    return false;
                }
                else
                {
                    if (sqlDataReader1.GetValue(5).ToString() != "" && sqlDataReader1.GetValue(6).ToString() == "")
                    {
                        return false;
                    }
                    if (sqlDataReader1.GetValue(7).ToString() != "" && sqlDataReader1.GetValue(8).ToString() == "")
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            strSql = "select sfbclr from table_tjjg where tjbh='" + tjbh.Text + "'";
            sqlDataReader = dbConn.GetDataReader(strSql);
            if (sqlDataReader.Read())
            {

                if (shenhe())
                {
                    if (sqlDataReader.GetValue(0).ToString() == "1")
                    {
                        strSql = "select sfsh from table_renyuan where tjbh='" + tjbh.Text + "'";
                        sqlDataReader = dbConn.GetDataReader(strSql);
                        if (sqlDataReader.Read())
                        {

                            if (sqlDataReader.GetValue(0).ToString() == "0")
                            {
                                FrmSelectSH f = new FrmSelectSH();
                                f.ShowDialog();
                                if (FrmSelectSH.i_flag!=0)
                                {
                                    strSql = "update table_renyuan set sfsh="+FrmSelectSH.i_flag.ToString()+" where tjbh='" + tjbh.Text + "'";
                                    if (dbConn.GetSqlCmd(strSql) != 0)
                                    {
                                        MessageBox.Show("审核成功！");
                                        pictureBox6.Enabled = false;
                                        if (FrmSelectSH.i_flag == 1)
                                        {
                                            label3.Text = "审核通过！";
                                        }
                                        else
                                        {
                                            label3.Text = "审核未通过！";
                                        }
                                        button1_Click(sender, e);
                                    }
                                    else
                                    {
                                        MessageBox.Show("审核失败！");
                                    }
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                DialogResult dr = MessageBox.Show("该人员已经审核，是否反审核？", "审核提示", MessageBoxButtons.YesNo);
                                if (dr == DialogResult.Yes)
                                {
                                    strSql = "update table_renyuan set sfsh=0,dycs=0,dyrq=null where tjbh='" + tjbh.Text + "'";
                                    if (dbConn.GetSqlCmd(strSql) != 0)
                                    {
                                        MessageBox.Show("反审核成功！");
                                        button1_Click(sender, e);
                                        label3.Text = "未审核";

                                    }
                                    else
                                    {
                                        MessageBox.Show("反审核失败！");
                                    }
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show("体检结果未录入保存，请返回结果录入!");
                    }
                }
                else
                {
                    MessageBox.Show("检查医师选择不完整，不允许审核。");
                }
            }
        }


    

        private void button2_Click(object sender, EventArgs e)
        {
            FrmChaKan f = new FrmChaKan(tjbh.Text);
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("检查结论");
            f.ShowDialog();
            textBox1.Text = textBox1.Text + FrmSelect.str_value;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("体检机构意见");
            f.ShowDialog();
            textBox3.Text = textBox3.Text + FrmSelect.str_value;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ys1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point p1 = Control.MousePosition;
            FrmSelectYs a = new FrmSelectYs("检查结论", "ys8",p1);
            a.ShowDialog();
            ys1.Text = FrmSelectYs.str_value;
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            str_tjbh = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            frmTjjglr = new FrmTjjglr();
            tjbh.Text = str_tjbh;
            strSql = "select jcjl,jcjgyj,sfbcjl,ys8,sfsh from table_tjjg,table_renyuan where table_renyuan.tjbh=table_tjjg.tjbh and table_renyuan.tjbh='" + str_tjbh + "'";
            sqlDataReader = dbConn.GetDataReader(strSql);
            if (sqlDataReader.Read())
            {
                if (sqlDataReader.GetValue(2).ToString() != "1")
                {
                    textBox1.Text = frmTjjglr.getValue("检查结论");
                    textBox3.Text = frmTjjglr.getValue("体检机构意见");
                    ys1.Text = getYs("体检结论");


                }
                else
                {
                    textBox1.Text = sqlDataReader.GetValue(0).ToString();
                    textBox3.Text = sqlDataReader.GetValue(1).ToString();
                    ys1.Text = sqlDataReader.GetValue(3).ToString();
                    if (sqlDataReader.GetValue(4).ToString() == "0")
                    {
                        label3.Text = "未审核";
                    }
                    else if (sqlDataReader.GetValue(4).ToString() == "1")
                    {
                        label3.Text = "审核通过";
                    }
                    else if (sqlDataReader.GetValue(4).ToString() == "2")
                    {
                        label3.Text = "审核未通过";
                    }

                }
                pictureBox4.Enabled = true;

                pictureBox5.Enabled = true;
                pictureBox6.Enabled = true;
                textBox1.Enabled = true;
                textBox3.Enabled = true;
                ys1.Enabled = true;
                //button2.Enabled = true;
                //button3.Enabled = true;
                //button4.Enabled = true;
            }
        }
        public string getYs(string str_type)
        {
          string  strSql1 = "select value from table_canshu where beizhu='" + str_type + "默认医师'";
           SqlDataReader sqlDataReader1 = dbConn.GetDataReader(strSql1);
            if (sqlDataReader1.Read())
            {
                return sqlDataReader1.GetValue(0).ToString();
            }
            else
            {
                return "";
            }
        }
        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
