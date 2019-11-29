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
    public partial class FrmTjjglr : Form
    {
        DbConn dbConn = null;
        string strSql = "";
        string strSql1 = "";
        ArrayList al = null;
        SqlDataAdapter sqlDataAdapter = null;
        DataSet dataSet = null;
        SqlDataReader sqlDataReader = null;
        string str_tjbh1 = "";
        SqlDataReader sqlDataReader1 = null;
        string str_yhqx = "";
        public FrmTjjglr()
        {
            InitializeComponent();
            dbConn = new DbConn();
            str_yhqx = FrmLogin.str_yhqx;
        }
        public FrmTjjglr(string str_tjbh)
        {
            InitializeComponent();
            dbConn = new DbConn();
            str_tjbh1 = str_tjbh;
            str_yhqx = FrmLogin.str_yhqx;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton1.Checked)
            //{
            //    strSql = "select tjbh as 体检编号,xm as 姓名 from table_renyuan where sfsh=0 and djrq='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
            //    sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            //    dataSet = new DataSet();
            //    sqlDataAdapter.Fill(dataSet, "table1");
            //    dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            //}
        }
        private void LoadList()
        {
            strSql = "select tjbh as 体检编号,xm as 姓名 from table_renyuan where (isdelete!=1 or isdelete is null) and sfsh=0 order by tjbh desc";// "and djrq='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
        }
        private void FrmTjjglr_Load(object sender, EventArgs e)
        {
            setTzFalse();
            setXpFalse();
            setHyFalse();
            LoadList();
            comboBox1.SelectedIndex = 0;
            //radioButton1.Checked = true;

            pictureBox14.Enabled = false;
        }
        private void setTzFalse()
        {
            xin.Enabled = false;
            gan.Enabled = false;
            pi.Enabled = false;
            fei.Enabled = false;
            tizhengqita.Enabled = false;
            ys1.Enabled = false;
            pictureBox8.Enabled = false;
            pictureBox9.Enabled = false;
            pictureBox10.Enabled = false;
            pictureBox11.Enabled = false;
            pictureBox12.Enabled = false;
            pifu1.Enabled = false;
            pifu2.Enabled = false;
            pifu3.Enabled = false;
        }
        private void setTzTrue()
        {
            xin.Enabled = true;
            gan.Enabled = true;
            pi.Enabled = true;
            fei.Enabled = true;
            tizhengqita.Enabled = true;
            ys1.Enabled = true;
            pictureBox8.Enabled = true;
            pictureBox9.Enabled = true;
            pictureBox10.Enabled = true;
            pictureBox11.Enabled = true;
            pictureBox12.Enabled = true;
            pifu1.Enabled = true;
            pifu2.Enabled = true;
            pifu3.Enabled = true;
        }
        private void setXpFalse()
        {
            xiongpian.Enabled = false;
            pictureBox7.Enabled = false;
            ys2.Enabled = false;


        }
        private void setXpTrue()
        {
            xiongpian.Enabled = true;
            pictureBox7.Enabled = true;
            ys2.Enabled = true;


        }
        private void setHyFalse()
        {
            dabian1.Enabled = false;
            dabian2.Enabled = false;
            gangongneng1.Enabled = false;
            gangongneng2.Enabled = false;
            gangongneng3.Enabled = false;
            huayanqita.Enabled = false;
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = false;
            pictureBox3.Enabled = false;
            pictureBox4.Enabled = false;
            pictureBox5.Enabled = false;
            ys3.Enabled = false;
            ys4.Enabled = false;
            ys5.Enabled = false;
            ys6.Enabled = false;
            ys7.Enabled = false;
        }
        private void setHyTrue()
        {
            dabian1.Enabled = true;
            dabian2.Enabled = true;
            gangongneng1.Enabled = true;
            gangongneng2.Enabled = true;
            gangongneng3.Enabled = true;
            huayanqita.Enabled = true;
            pictureBox1.Enabled = true;
            pictureBox2.Enabled = true;
            pictureBox3.Enabled = true;
            pictureBox4.Enabled = true;
            pictureBox5.Enabled = true;
            ys3.Enabled = true;
            ys4.Enabled = true;
            ys5.Enabled = true;
            ys6.Enabled = true;
            ys7.Enabled = true;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton2.Checked)
            //{
            //    strSql = "select tjbh as 体检编号,xm as 姓名 from table_renyuan where sfsh=1 and djrq='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'";
            //    sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            //    dataSet = new DataSet();
            //    sqlDataAdapter.Fill(dataSet, "table1");
            //    dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            //}
        }

        private void button7_Click(object sender, EventArgs e)
        {

            strSql = "select tjbh as 体检编号,xm as 姓名 from table_renyuan where  1=1";
            if (comboBox1.SelectedIndex == 0)
            {
                strSql = strSql + " and sfzhm like '%" + txtTjbh.Text + "%'";

            }
            else if (comboBox1.SelectedIndex == 1)
            {
                strSql = strSql + " and tjbh like '%" + txtTjbh.Text + "%'";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                strSql = strSql + " and xm like '%" + txtTjbh.Text + "%'";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                strSql = strSql + " and gzdw like '%" + txtTjbh.Text + "%'";
            }
            if (checkBox1.Checked)
            {
                strSql = strSql + " and djrq>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and djrq<'"+dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd")+"'";
            }
            strSql = strSql + " and  (isdelete!=1 or isdelete is null) and sfsh=0 order by tjbh desc";

            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (str_yhqx[1] == '1')
            {
                setTzTrue();
            }
            else
            {
                setTzFalse();
            }
            if (str_yhqx[2] == '1')
            {
                setXpTrue();
            }
            else
            {
                setXpFalse();
            }
            if (str_yhqx[3] == '1')
            {
                setHyTrue();
            }
            else
            {
                setHyFalse();
            }
            if (e.RowIndex == -1)
            { return; }

            string str_tjbh = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            tjbh.Text = str_tjbh;
            strSql = "select xin,gan,pi,fei,pifu1,pifu2,pifu3,tizhengqita,xiongpian,"
                + "dabian1,dabian2,gangongneng1,gangongneng2,gangongneng3,huayanqita,table_tjjg.tjbh,table_tjjg.rowid,sfbclr, "
                + "table_renyuan.xm,table_renyuan.nl,table_renyuan.xb,table_renyuan.gzdw,table_renyuan.djrq,ys1,ys2,ys3,ys4,ys5,ys6,ys7,ganyan,liji,shanghan,feijiehe,pifubing,qita"
                + " from  table_tjjg,table_renyuan "
                + " where table_tjjg.tjbh=table_renyuan.tjbh and table_tjjg.tjbh='" + str_tjbh + "'";

            sqlDataReader = dbConn.GetDataReader(strSql);
            if (sqlDataReader.Read())
            {

                if (sqlDataReader.GetValue(17).ToString() == "1")
                {
                    xin.Text = sqlDataReader.GetValue(0).ToString();
                    gan.Text = sqlDataReader.GetValue(1).ToString();
                    pi.Text = sqlDataReader.GetValue(2).ToString();
                    fei.Text = sqlDataReader.GetValue(3).ToString();
                    tizhengqita.Text = sqlDataReader.GetValue(7).ToString();
                    xiongpian.Text = sqlDataReader.GetValue(8).ToString();
                    dabian1.Text = sqlDataReader.GetValue(9).ToString();
                    dabian2.Text = sqlDataReader.GetValue(10).ToString();
                    gangongneng1.Text = sqlDataReader.GetValue(11).ToString();
                    gangongneng2.Text = sqlDataReader.GetValue(12).ToString();
                    gangongneng3.Text = sqlDataReader.GetValue(13).ToString();
                    huayanqita.Text = sqlDataReader.GetValue(14).ToString();

                    ys1.Text = sqlDataReader.GetValue(23).ToString();
                    ys2.Text = sqlDataReader.GetValue(24).ToString();
                    ys3.Text = sqlDataReader.GetValue(25).ToString();
                    ys4.Text = sqlDataReader.GetValue(26).ToString();
                    ys5.Text = sqlDataReader.GetValue(27).ToString();
                    ys6.Text = sqlDataReader.GetValue(28).ToString();
                    ys7.Text = sqlDataReader.GetValue(29).ToString();

                }
                else
                {

                    xin.Text = getValue("心");
                    gan.Text = getValue("肝");

                    pi.Text = getValue("脾");

                    fei.Text = getValue("肺");

                    tizhengqita.Text = getValue("其他");

                    xiongpian.Text = getValue("X光");

                    dabian1.Text = getValue("细菌性和阿米巴性痢病杆菌");

                    dabian2.Text = getValue("伤寒或副伤寒");

                    gangongneng1.Text = getValue("谷丙转氨酶");

                    gangongneng2.Text = getValue("HAV-IgM");

                    gangongneng3.Text = getValue("HEV-IgM");

                    huayanqita.Text = getValue("化验其他");
                    ys1.Text = getYs("体征");
                    ys2.Text = getYs("胸片");
                    ys3.Text = getYs("细菌性");
                    ys4.Text = getYs("伤寒或副伤寒");
                    ys5.Text = getYs("谷丙转氨酶");
                    ys6.Text = getYs("HAV");
                    ys7.Text = getYs("HEV");
                }
                if (sqlDataReader.GetValue(4).ToString() == "True")
                {
                    pifu1.Checked = true;
                }
                else
                {
                    pifu1.Checked = false;
                }

                if (sqlDataReader.GetValue(5).ToString() == "True")
                {
                    pifu2.Checked = true;
                }
                else
                {
                    pifu2.Checked = false;
                }
                if (sqlDataReader.GetValue(6).ToString() == "True")
                {
                    pifu3.Checked = true;
                }
                else
                {
                    pifu3.Checked = false;
                }
                tjbh.Text = sqlDataReader.GetValue(15).ToString();
                id.Text = sqlDataReader.GetValue(16).ToString();
                label1.Text = sqlDataReader.GetValue(18).ToString();
                label16.Text = sqlDataReader.GetValue(19).ToString() + "岁";

                label14.Text = sqlDataReader.GetValue(20).ToString();


                label15.Text = sqlDataReader.GetValue(21).ToString();
                label11.Text = DateTime.Parse(sqlDataReader.GetValue(22).ToString()).Year.ToString();
                label12.Text = DateTime.Parse(sqlDataReader.GetValue(22).ToString()).Month.ToString();
                label13.Text = DateTime.Parse(sqlDataReader.GetValue(22).ToString()).Day.ToString();
                label5.Text = sqlDataReader.GetValue(30).ToString();
                label6.Text = sqlDataReader.GetValue(31).ToString();
                label7.Text = sqlDataReader.GetValue(32).ToString();
                label8.Text = sqlDataReader.GetValue(33).ToString();
                label9.Text = sqlDataReader.GetValue(34).ToString();
                label10.Text = sqlDataReader.GetValue(35).ToString();
                pictureBox14.Enabled = true;
            }
            else
            {
                xin.Text = "";
                gan.Text = "";
                pi.Text = "";
                fei.Text = "";
                tizhengqita.Text = "";
                xiongpian.Text = "";
                dabian1.Text = "";
                dabian2.Text = "";
                gangongneng1.Text = "";
                gangongneng2.Text = "";
                gangongneng3.Text = "";
                huayanqita.Text = "";

                pifu1.Checked = false;

                pifu2.Checked = false;

                pifu3.Checked = false;

            }
        }
        public string getYs(string str_type)
        {
            strSql1 = "select value from table_canshu where beizhu='"+ str_type+"默认医师'";
            sqlDataReader1 = dbConn.GetDataReader(strSql1);
            if (sqlDataReader1.Read())
            {
                return sqlDataReader1.GetValue(0).ToString();
            }
            else
            {
                return "";
            }
        }
        public string getValue(string str_type)
        {
            strSql1 = "select value from table_set where sfmrz=1 and name='" + str_type + "'";
            sqlDataReader1 = dbConn.GetDataReader(strSql1);
            if (sqlDataReader1.Read())
            {
                return sqlDataReader1.GetValue(0).ToString();
            }
            else
            {
                return "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (tjbh.Text == null || tjbh.Text == "")
            {
                MessageBox.Show("请选择体检人员！");
                return;
            }
            string str_pifu1 = "";
            string str_pifu2 = "";
            string str_pifu3 = "";
            if (pifu1.Checked)
            {
                str_pifu1 = "1";
            }
            else
            {
                str_pifu1 = "0";
            } 
            if (pifu2.Checked)
            {
                str_pifu2 = "1";
            }
            else
            {
                str_pifu2 = "0";
            } 
            if (pifu3.Checked)
            {
                str_pifu3 = "1";
            }
            else
            {
                str_pifu3 = "0";
            }
                strSql = "update table_tjjg set sfbclr=1, xin='" + xin.Text + "',"
                    + "gan='" + gan.Text + "',"
                    + "pi='" + pi.Text + "',"
                    + "fei='" + fei.Text + "',"
                    + "pifu1='" + str_pifu1 + "',"
                    + "pifu2='" + str_pifu2 + "',"
                    + "pifu3='" + str_pifu3 + "',"
                    + "tizhengqita='" + tizhengqita.Text + "',"
                    + "xiongpian='" + xiongpian.Text + "',"
                    + "dabian1='" + dabian1.Text + "',"
                    + "dabian2='" + dabian2.Text + "',"
                    + "gangongneng1='" + gangongneng1.Text + "',"
                    + "gangongneng2='" + gangongneng2.Text + "',"
                    + "gangongneng3='" + gangongneng3.Text + "',"
                    + "huayanqita='" + huayanqita.Text + "',"
                    + "ys1='" + ys1.Text + "',"
                    + "ys2='" + ys2.Text + "',"
                    + "ys3='" + ys3.Text + "',"
                    + "ys4='" + ys4.Text + "',"
                    + "ys5='" + ys5.Text + "',"
                    + "ys6='" + ys6.Text + "',"
                    + "ys7='" + ys7.Text + "'"
                    + " where tjbh=" + tjbh.Text;
                if (dbConn.GetSqlCmd(strSql) != 0)
                {
                    MessageBox.Show("保存成功！");
                
                }
                else
                {
                    MessageBox.Show("保存失败！");
                }
            LoadList();
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellDoubleClick(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("心");
            f.ShowDialog();
            xin.Text = xin.Text+FrmSelect.str_value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("肝");
            f.ShowDialog();
            gan.Text = gan.Text+ FrmSelect.str_value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("脾");
            f.ShowDialog();
            pi.Text = pi.Text +FrmSelect.str_value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("肺");
            f.ShowDialog();
            fei.Text = fei.Text+ FrmSelect.str_value;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("其他");
            f.ShowDialog();
            tizhengqita.Text = tizhengqita.Text+FrmSelect.str_value;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("X光");
            f.ShowDialog();
            xiongpian.Text = xiongpian.Text + FrmSelect.str_value;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("细菌性和阿米巴性痢病杆菌");
            f.ShowDialog();
            dabian1.Text = dabian1.Text + FrmSelect.str_value;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("伤寒或副伤寒");
            f.ShowDialog();
            dabian2.Text = dabian2.Text + FrmSelect.str_value;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("谷丙转氨酶");
            f.ShowDialog();
            gangongneng1.Text = gangongneng1.Text + FrmSelect.str_value;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("HAV-IgM");
            f.ShowDialog();
            gangongneng2.Text = gangongneng2.Text + FrmSelect.str_value;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("HEV-IgM");
            f.ShowDialog();
            gangongneng3.Text = gangongneng3.Text + FrmSelect.str_value;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("化验其他");
            f.ShowDialog();
            huayanqita.Text = huayanqita.Text + FrmSelect.str_value;
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            button7_Click(sender, e);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            button8_Click(sender, e);
        }

        private void ys1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point p1 = Control.MousePosition;
            if (xin.Text == "" || gan.Text == "" || pi.Text == "" || fei.Text == "")
            {
                MessageBox.Show("填写不完整，不允许选择医生！");
                return;
            }
            FrmSelectYs a = new FrmSelectYs("体征","ys1",p1);
            a.ShowDialog();
            ys1.Text = FrmSelectYs.str_value;
        }

        private void ys1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ys2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point p1 = Control.MousePosition;
            if (xiongpian.Text=="")
            {
                MessageBox.Show("填写不完整，不允许选择医生！");
                return;
            }
            FrmSelectYs a = new FrmSelectYs("胸片", "ys2",p1);
            a.ShowDialog();
            ys2.Text = FrmSelectYs.str_value;
        }

        private void ys3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point p1 = Control.MousePosition;
            if (dabian1.Text=="")
            {
                MessageBox.Show("填写不完整，不允许选择医生！");
                return;
            }
            FrmSelectYs a = new FrmSelectYs("细菌性和阿米巴性痢病杆菌", "ys3",p1);
            a.ShowDialog();
            ys3.Text = FrmSelectYs.str_value;
        }

        private void ys4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point p1 = Control.MousePosition;
            if (dabian2.Text=="")
            {
                MessageBox.Show("填写不完整，不允许选择医生！");
                return;
            }
            FrmSelectYs a = new FrmSelectYs("伤寒或副伤寒", "ys4",p1);
            a.ShowDialog();
            ys4.Text = FrmSelectYs.str_value;
        }

        private void ys5_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point p1 = Control.MousePosition;
            if (gangongneng1.Text=="")
            {
                MessageBox.Show("填写不完整，不允许选择医生！");
                return;
            }
            FrmSelectYs a = new FrmSelectYs("谷丙转氨酶", "ys5",p1);
            a.ShowDialog();
            ys5.Text = FrmSelectYs.str_value;
        }

        private void ys6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point p1 = Control.MousePosition;
            if (gangongneng2.Text=="")
            {
                MessageBox.Show("填写不完整，不允许选择医生！");
                return;
            }
            FrmSelectYs a = new FrmSelectYs("HAV-IgM", "ys6",p1);
            a.ShowDialog();
            ys6.Text = FrmSelectYs.str_value;
        }

        private void ys7_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Point p1 = Control.MousePosition;
            if (gangongneng3.Text=="")
            {
                MessageBox.Show("填写不完整，不允许选择医生！");
                return;
            }
            FrmSelectYs a = new FrmSelectYs("HEV-IgM", "ys7",p1);
            a.ShowDialog();
            ys7.Text = FrmSelectYs.str_value;
        }

        private void button11_Click_1(object sender, EventArgs e)
        {

        }

        private void ys4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void ys2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ys3_TextChanged(object sender, EventArgs e)
        {

        }

        private void ys5_TextChanged(object sender, EventArgs e)
        {

        }

        private void ys6_TextChanged(object sender, EventArgs e)
        {

        }

        private void ys7_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmSelect f = new FrmSelect("心");
            f.ShowDialog();
        }
    }
}
