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
    public partial class FrmSjgl : Form
    {
        DbConn dbConn=null;
        string strSql = "";
        SqlDataAdapter sqlDataAdapter = null;
        SqlDataReader sqlDataReader = null;
        DataSet dataSet = null;
        int i_flag = 0;
        int i_rowIndex = -1;
        int i_comIndex = -1;
        public FrmSjgl(int i_flag)
        {
            InitializeComponent();
            dbConn = new DbConn();
            this.i_flag = i_flag;
        }

        private void FrmSjgl_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            radioButton1.Checked = true;
            if (i_flag == 1)
            {
                button2.Text = "导出";
            }
            else
            {
                button2.Text = "隐藏";
                button2.Enabled = false;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (i_flag == 1)
            {
                strSql = "select table_renyuan.tjbh as 体检编号,djrq as 登记日期,xm as 姓名,xb as 性别,"
                    + "nl as 年龄,sfzhm as 证件号码 ,lxdh as 联系电话,gzdw as 工作单位,"
                    + "hylbdl as 行业类别,ganyan as 肝炎,liji as 痢疾,shanghan as 伤寒,feijiehe as 肺结核,"
                    + "pifubing as 皮肤病,qita as 其他,xin as 心,gan as 肝,pi as 脾,fei as 肺,"
                    + "case when pifu1=1 then '*' else '' end as 渗出性皮肤病 ,"
                    + "case when pifu2=1 then '*' else '' end as 化脓性皮肤病,"
                    + "case when pifu3=1 then '*' else '' end as 皮肤其他, tizhengqita as 体征其他,"
                    + "xiongpian as 胸片,dabian1 as 细菌性和阿米巴性痢病杆菌,dabian2 as 伤寒或副伤寒,"
                    + "gangongneng1 as 谷丙转氨酶,gangongneng2 as HAV,gangongneng3 as HEV , huayanqita as 化验其他,jcjl as 检查结论,jcjgyj as 检查机构意见,table_zjdy.dyyh as 打印用户"
                    + " from table_renyuan "
                    + " left join table_zjdy on table_zjdy.tjbh=table_renyuan.tjbh "
                    + "left join table_tjjg on table_renyuan.tjbh = table_tjjg.tjbh where 1=1  and (isdelete!=1 or isdelete is null) ";
                if (textBox1.Text != "")
                {
                    if (comboBox1.SelectedIndex == 0)
                    {
                        strSql = strSql + " and sfzhm='" + textBox1.Text + "'";
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    { strSql = strSql + " and table_renyuan.tjbh='" + textBox1.Text + "'"; }
                    else if (comboBox1.SelectedIndex == 2)
                    { strSql = strSql + " and xm like '%" + textBox1.Text + "%'"; }
                    else if (comboBox1.SelectedIndex == 3)
                    { strSql = strSql + " and gzdw like '%" + textBox1.Text + "%'"; }
                }
                if (radioButton2.Checked)
                {
                    strSql = strSql + " and sfsh=0";
                }
                else if (radioButton3.Checked)
                {
                    strSql = strSql + " and sfsh!=0";
                }
                strSql = strSql + " and djrq >= '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and djrq <'" + dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";
                strSql = strSql + " order by table_renyuan.tjbh";
            }
            else
            {
                strSql = "select table_renyuan.tjbh as 体检编号,djrq as 登记日期,xm as 姓名,xb as 性别,"
                   + "nl as 年龄,sfzhm as 证件号码 ,lxdh as 联系电话,gzdw as 工作单位,"
                   + "hylbdl as 行业类别,case when isdelete=1 then '已隐藏' else '正常' end as 状态 "
                   + " from table_renyuan "
                   + "left join table_tjjg on table_renyuan.tjbh = table_tjjg.tjbh where 1=1  ";
                if (textBox1.Text != "")
                {
                    if (comboBox1.SelectedIndex == 0)
                    {
                        strSql = strSql + " and sfzhm like'%" + textBox1.Text + "%'";
                    }
                    else if (comboBox1.SelectedIndex == 1)
                    { strSql = strSql + " and table_renyuan.tjbh like '%" + textBox1.Text + "%'"; }
                    else if (comboBox1.SelectedIndex == 2)
                    { strSql = strSql + " and xm like '%" + textBox1.Text + "%'"; }
                    else if (comboBox1.SelectedIndex == 3)
                    { strSql = strSql + " and gzdw like '%" + textBox1.Text + "%'"; }
                }
                if (radioButton2.Checked)
                {
                    strSql = strSql + " and sfsh=0";
                }
                else if (radioButton3.Checked)
                {
                    strSql = strSql + " and sfsh!=0";
                }
                strSql = strSql + " and djrq between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";
                strSql = strSql + " order by table_renyuan.tjbh";
                button2.Enabled = false;
            }

            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;

            DataRow Row_add3 = dataSet.Tables["table1"].NewRow();
            Row_add3[0] = "合计：" + dataGridView1.RowCount.ToString() + "人";
           

            dataSet.Tables["table1"].Rows.Add(Row_add3);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (button2.Text == "导出")
            {
                ToExcel t = new ToExcel();
                t.DataToExcel(dataGridView1, "数据查询");
            }
            else if (button2.Text == "隐藏")
            {
                strSql = "select sfsh from table_renyuan where tjbh='" + dataGridView1[0, i_rowIndex].Value.ToString() + "'";
                sqlDataReader = dbConn.GetDataReader(strSql);
                if (sqlDataReader.Read())
                {
                    if (sqlDataReader.GetValue(0).ToString() == "0")
                    {
                        DialogResult dir = MessageBox.Show("确定隐藏体检编号为【" + dataGridView1[0, i_rowIndex].Value.ToString() + "】体检信息吗？", "警告", MessageBoxButtons.YesNo);

                        if (dir == DialogResult.Yes)
                        {
                            strSql = "update table_renyuan set isdelete=1 where tjbh='" + dataGridView1[0, i_rowIndex].Value.ToString() + "'";
                            if (dbConn.GetSqlCmd(strSql) != 0)
                            {
                                MessageBox.Show("隐藏成功！");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("该记录已经审核，不可隐藏！");
                    }
                }

            }
            else if (button2.Text == "还原")
            {

                DialogResult dir = MessageBox.Show("确定还原体检编号为【" + dataGridView1[0, i_rowIndex].Value.ToString() + "】体检信息吗？", "警告", MessageBoxButtons.YesNo);

                if (dir == DialogResult.Yes)
                {
                    strSql = "update table_renyuan set isdelete=0 where tjbh='" + dataGridView1[0, i_rowIndex].Value.ToString() + "'";
                    if (dbConn.GetSqlCmd(strSql) != 0)
                    {
                        MessageBox.Show("还原成功！");
                    }
                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            i_rowIndex = e.RowIndex;
            if (i_flag != 1)
            {
                if (dataGridView1[9, i_rowIndex].Value.ToString() == "已隐藏")
                {
                    button2.Text = "还原";
                    button2.Enabled = true;
                }
                else
                {

                    button2.Text = "隐藏";
                    button2.Enabled = true;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
