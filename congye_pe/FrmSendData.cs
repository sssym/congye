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
    public partial class FrmSendData : Form
    {
        string strSql = "";
        SqlDataAdapter sqlDataAdapter = null;
        SqlDataReader sqlDataReader = null;
        DataSet dataSet = null;
        DbConn dbConn = null;
        ArrayList arrayList = null;
        int i_selectRadio = 0;
        string str_dcbh = "";
        string str_sczt = "";

        public FrmSendData()
        {
            InitializeComponent();
            dbConn = new DbConn();
        }

        private void FrmSendData_Load(object sender, EventArgs e)
        {
            //strSql = "select '身份证' as 证件类别,a.sfzhm as 证件号码,a.xm as 持证人姓名,"
            //    + "a.xb as 性别,a.djrq as 体检日期,a.tjbh as 健康证证号,a.djrq as 申请日期,a.nl as 年龄,"
            //    + "a.hylbdl as 健康证类别,"
            //    + "a.gzdw as 工作单位,a.lxdh as 单位联系电话,a.sfzzp as 相片,0 as 证书状态 from table_renyuan a "
            //    + "where isdelete=0 and sfsh = 1 and sfsc = 0";
            //strSql = strSql + " order by a.tjbh";
            //sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            //dataSet = new DataSet();
            //sqlDataAdapter.Fill(dataSet, "table1");
            //dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            radioButton2.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //    dataGridView1 = new DataGridView();
            //    dataGridView2 = new DataGridView();
            if (radioButton1.Checked)
            {

                //strSql = "select '身份证' as 证件类别,a.sfzhm as 证件号码,a.xm as 持证人姓名,"
                //    + "a.xb as 性别,a.djrq as 体检日期,a.tjbh as 健康证证号,a.djrq as 申请日期,a.nl as 年龄,"
                //    + "a.hylbdl as 健康证类别,"
                //    + "a.gzdw as 工作单位,a.lxdh as 单位联系电话,a.sfzzp as 相片,0 as 证书状态 from table_renyuan a "
                //    + "where isdelete=0 and sfsh = 1" + " and a.djrq>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and a.djrq<'" + dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";
                //i_selectRadio = 1;
            }
            else if (radioButton2.Checked)
            {

                strSql = "select dcbh as 导出编号,convert(varchar(10),dcrq,120) as 导出日期,count(*) as 记录条数,"
                    +"(case when sczt=1 then '上传成功' else '未上传或上传失败' end ) as 上传状态,"
                    +"convert(varchar(10),scrq,120) as 上传日期 from table_renyuan "
                    +"where (isdelete=0 or isdelete is null) and sfsc=1 "
                    + " and dcrq>='"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"' and dcrq<'"+dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd")+"'"
                    + " group by dcbh,convert(varchar(10),dcrq,120),sczt,convert(varchar(10),scrq,120)  order by dcbh";
                sqlDataAdapter = dbConn.GetDataAdapter(strSql);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "table1");
                dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
               // dataGridView1.Columns[1].DefaultCellStyle.Format = "yyyy-MM-dd";
                //strSql = "select '身份证' as 证件类别,a.sfzhm as 证件号码,a.xm as 持证人姓名,"
                //                + "a.xb as 性别,a.djrq as 体检日期,a.tjbh as 健康证证号,a.djrq as 申请日期,a.nl as 年龄,"
                //                + "a.hylbdl as 健康证类别,"
                //                + "a.gzdw as 工作单位,a.lxdh as 单位联系电话,a.sfzzp as 相片,0 as 证书状态 from table_renyuan a "
                //                + "where isdelete=0 and sfsh = 1 and sfsc = 1" + " and a.djrq>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and a.djrq<'" + dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";
                i_selectRadio = 2;
                tabControl1.SelectedIndex = 0;

            }
            else if (radioButton3.Checked)
            {
                strSql = "select '身份证' as 证件类别,a.sfzhm as 证件号码,a.xm as 持证人姓名,"
                           + "a.xb as 性别,a.djrq as 体检日期,a.tjbh as 健康证证号,a.djrq as 申请日期,a.nl as 年龄,"
                           + "a.hylbdl as 健康证类别,"
                           + "a.gzdw as 工作单位,a.lxdh as 单位联系电话,a.sfzzp as 相片,0 as 证书状态 from table_renyuan a "
                           + "where isdelete=0 and sfsh = 1 and dcbh is null and sfsc = 0" + " and a.djrq>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and a.djrq<'" + dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd") + "'";
                i_selectRadio = 3;
                strSql = strSql + " order by a.tjbh";
                sqlDataAdapter = dbConn.GetDataAdapter(strSql);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet, "table1");
                dataGridView2.DataSource = dataSet.Tables["table1"].DefaultView;
                dataGridView2.Columns[6].DefaultCellStyle.Format = "yyyy-MM-dd";
                dataGridView2.Columns[4].DefaultCellStyle.Format = "yyyy-MM-dd";
                //button3.Enabled = true;
                tabControl1.SelectedIndex = 1;
                if (dataGridView2.RowCount == 0)
                {
                    button3.Enabled = false;
                }
                else
                {
                    button3.Enabled = true;
                }
            }
            //strSql = strSql + " order by a.tjbh";
            //sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            //dataSet = new DataSet();
            //sqlDataAdapter.Fill(dataSet, "table1");
            //dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            //dataGridView1.Columns[6].DefaultCellStyle.Format = "yyyy-MM-dd";
            //dataGridView1.Columns[4].DefaultCellStyle.Format = "yyyy-MM-dd";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (i_selectRadio == 3)
            {

                ClsPublic.getXML(dataGridView1);
                arrayList = new ArrayList();
                string str_url = @"http://219.135.157.134:9090/jkz/";
                //string str_zs = @"http://219.135.157.143:9090/jkz/";
                ClassDLL.CoporationReg();
                if (DbConn.sfzs == "0")
                {
                    str_url = @"http://219.135.157.134:9090/jkz";
                }
                else
                {

                    str_url = @"http://219.135.157.143:9090/jkz";
                }
                if (ClassDLL.DLLInit(str_url, "test") == 0)
                {
                    MessageBox.Show("初始化成功！正在准备上传数据！");
                    int int_count = ClassDLL.UploadInfo("cyjkz_jkzxx", @".\jkz.Xml");
                    if (int_count > 0)
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            strSql = "update table_renyuan set sfsc=1 where tjbh='" + dataGridView1[5, i].Value.ToString();
                            arrayList.Add(strSql);
                        }
                        if (dbConn.GetTransaction(arrayList))
                        {
                            MessageBox.Show("成功上传 " + int_count.ToString() + " 条数据！");
                        }
                    }
                    else
                    {
                        MessageBox.Show("上传失败！");
                    }
                }
                else
                {
                    MessageBox.Show("初始化失败，请联系管理员！");
                }
            }
            else
            {
                MessageBox.Show("只有选择未上传的查询结果才可以上传！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (i_selectRadio == 2)
            {
                FrmSelectSCZT f = new FrmSelectSCZT();
                f.ShowDialog();
                if (FrmSelectSCZT.str_date == "")
                {
                    return;
                }
                else if (FrmSelectSCZT.str_date == "1")
                {
                    strSql = "update table_renyuan set sczt=0,scrq='' where dcbh='" + str_dcbh + "'";

                }
                else if (FrmSelectSCZT.str_date == "2")
                {
                    if (str_sczt == "上传成功")
                    {
                        MessageBox.Show("上传成功的状态不允许作废");
                        return;
                    }
                    DialogResult dir = MessageBox.Show("确定作废导出记录？这可能会导致上传数据出错！", "警告", MessageBoxButtons.YesNo);
                    if (dir == DialogResult.Yes)
                    {
                        strSql = "update table_renyuan set sfsc=0,sczt=0,scrq='',dcbh=null,dcrq=null where dcbh='" + str_dcbh + "'";


                    }
                }
                else
                {
                    strSql = "update table_renyuan set sczt=1,scrq='" + FrmSelectSCZT.str_date + "' where dcbh='" + str_dcbh + "'";

                }
                if (dbConn.GetSqlCmd(strSql) != 0)
                {
                    MessageBox.Show("标识成功！");
                    strSql = "select dcbh as 导出编号,convert(varchar(10),dcrq,120) as 导出日期,count(*) as 记录条数,"
               + "(case when sczt=1 then '上传成功' when sczt=0 then '未上传' else '上传成功' end ) as 上传状态,"
               + "convert(varchar(10),scrq,120) as 上传日期 from table_renyuan "
               + "where (isdelete=0 or isdelete is null) and sfsc=1 "
               + " and dcrq>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and dcrq<'" + dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd") + "'"
               + " group by dcbh,convert(varchar(10),dcrq,120),sczt,convert(varchar(10),scrq,120)  order by dcbh";
                    sqlDataAdapter = dbConn.GetDataAdapter(strSql);
                    dataSet = new DataSet();
                    sqlDataAdapter.Fill(dataSet, "table1");
                    dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
                    dataGridView1.Columns[1].DefaultCellStyle.Format = "yyyy-MM-dd";
                    tabControl1.SelectedIndex = 0;
                    button3.Enabled = false;

                }
                else
                {
                    MessageBox.Show("标识失败！");
                }
            }
            else if (i_selectRadio == 3)
            {
                string str_dcbh = "";
                strSql = "select max(dcbh) from table_renyuan where (dcbh is not null or dcbh!='null' or dcbh='')  and  substring(dcbh,0,9)='" + DateTime.Now.ToString("yyyyMMdd") + "'";
                sqlDataReader = dbConn.GetDataReader(strSql);
                if (sqlDataReader.Read())
                {
                    if (sqlDataReader.HasRows)
                    {
                        str_dcbh = DateTime.Now.ToString("yyyyMMdd") + "01";
                    }
                    else
                    {
                        double i_no = double.Parse(sqlDataReader.GetValue(0).ToString()) + 1;
                        str_dcbh = i_no.ToString();
                    }
                }
                else
                {
                    str_dcbh = DateTime.Now.ToString("yyyyMMdd") + "01";
                }
                ClsPublic c = new ClsPublic();
                c.saveXml(dataGridView2, str_dcbh);
              
                arrayList = new ArrayList();
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    strSql = "update table_renyuan set sfsc=1,dcrq=getdate(),dcbh='" + str_dcbh + "' where tjbh='" + dataGridView2[5, i].Value.ToString() + "'";
                    arrayList.Add(strSql);
                }
                if (dbConn.GetTransaction(arrayList))
                {
                    MessageBox.Show("保存XML成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }



        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label1.Text = "导出日期";
                button3.Text = "标识上传状态";
                button3.Enabled = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton3.Checked)
            {
                label1.Text = "登记日期";
                button3.Text = "导出上传文件";
                button3.Enabled = false;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            strSql = "select '身份证' as 证件类别,a.sfzhm as 证件号码,a.xm as 持证人姓名,"
                            + "a.xb as 性别,a.djrq as 体检日期,a.tjbh as 健康证证号,a.djrq as 申请日期,a.nl as 年龄,"
                            + "a.hylbdl as 健康证类别,"
                            + "a.gzdw as 工作单位,a.lxdh as 单位联系电话,a.sfzzp as 相片,0 as 证书状态 from table_renyuan a "
                            + "where dcbh='"+dataGridView1[0,e.RowIndex].Value.ToString()+ "' order by a.tjbh";

            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView2.DataSource = dataSet.Tables["table1"].DefaultView;
            tabControl1.SelectedIndex = 1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            button3.Enabled = true;
            str_sczt = dataGridView1[3, e.RowIndex].Value.ToString();
            str_dcbh = dataGridView1[0, e.RowIndex].Value.ToString();
          
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                tabControl1.SelectedIndex = 1;
            }
            if (radioButton2.Checked)
            {
                if (tabControl1.SelectedIndex == 1)
                {
                    button3.Enabled = false;
                }
            }
        }
    }
}
