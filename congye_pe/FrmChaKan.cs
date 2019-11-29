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
    public partial class FrmChaKan : Form
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
        public FrmChaKan()
        {
            InitializeComponent();
            dbConn = new DbConn();
        }
        public FrmChaKan(string str_tjbh)
        {
            InitializeComponent();
            dbConn = new DbConn();
            str_tjbh1 = str_tjbh;
        }

        private void FrmChaKan_Load(object sender, EventArgs e)
        {
            strSql = "select b.xm,b.xb,b.gzdw,a.ganyan,a.liji,a.shanghan,"
                + "a.feijiehe,a.pifubing,a.qita,a.xin,a.gan,a.pi,a.fei,a.pifu1,"
                + "a.pifu2,a.pifu3,a.tizhengqita,a.xiongpian,a.dabian1,a.dabian2,"
                + "a.gangongneng1,a.gangongneng2,a.gangongneng3,a.huayanqita,b.nl,b.djrq,ys1,ys2,ys3,ys4,ys5,ys6,ys7 "
                + " from table_tjjg a, table_renyuan b where a.tjbh = b.tjbh and b.tjbh = '" + str_tjbh1 + "'";
            sqlDataReader = dbConn.GetDataReader(strSql);
            if (sqlDataReader.Read())
            {
                lbxm.Text = sqlDataReader.GetValue(0).ToString();
                lbxb.Text = sqlDataReader.GetValue(1).ToString();
                lbdw.Text = sqlDataReader.GetValue(2).ToString();
                lbgysj.Text = sqlDataReader.GetValue(3).ToString();
                lbljsj.Text = sqlDataReader.GetValue(4).ToString();
                lbshsj.Text = sqlDataReader.GetValue(5).ToString();
                lbfjhsj.Text = sqlDataReader.GetValue(6).ToString();
                lbpfbsj.Text = sqlDataReader.GetValue(7).ToString();
                lbqtsj.Text = sqlDataReader.GetValue(8).ToString();
                lbxin.Text = sqlDataReader.GetValue(9).ToString();
                lbgan1.Text = sqlDataReader.GetValue(10).ToString();
                lbpi.Text = sqlDataReader.GetValue(11).ToString();
                lbfei1.Text = sqlDataReader.GetValue(12).ToString();
                if (sqlDataReader.GetValue(13).ToString() == "1")
                {
                    lbpf.Text = "渗出性皮肤病";
                }
                else if (sqlDataReader.GetValue(14).ToString() == "1")
                {
                    lbpf.Text = "化脓性皮肤病";
                }
                else if (sqlDataReader.GetValue(15).ToString() == "1")
                {
                    lbpf.Text = "其他";
                }
                lbtzqt.Text = sqlDataReader.GetValue(16).ToString();
                lbxiongpian.Text = sqlDataReader.GetValue(17).ToString();
                lbdabian1.Text = sqlDataReader.GetValue(18).ToString();
                lbdabian2.Text = sqlDataReader.GetValue(19).ToString();
                lbgangongneng1.Text = sqlDataReader.GetValue(20).ToString();
                lbgangongneng2.Text = sqlDataReader.GetValue(21).ToString();
                lbgangongneng3.Text = sqlDataReader.GetValue(22).ToString();
                lbhyqt.Text = sqlDataReader.GetValue(23).ToString();
                lbnl.Text = sqlDataReader.GetValue(24).ToString();
                lbnian.Text = sqlDataReader.GetValue(25).ToString().Substring(0,4);
                lbyue.Text = sqlDataReader.GetValue(25).ToString().Substring(5, 2);
                lbri.Text = sqlDataReader.GetValue(25).ToString().Substring(8,2);
                tzys.Text = sqlDataReader.GetValue(26).ToString();
                xpys.Text = sqlDataReader.GetValue(27).ToString();
                dabian1ys.Text = sqlDataReader.GetValue(28).ToString();
                dabian2ys.Text = sqlDataReader.GetValue(29).ToString();
                gangongneng1ys.Text = sqlDataReader.GetValue(30).ToString();
                gangongneng2ys.Text = sqlDataReader.GetValue(31).ToString();
                gangongneng3ys.Text = sqlDataReader.GetValue(32).ToString();
            }
            else
            {
                lbxm.Text = "";
                lbxb.Text = "";
                lbdw.Text = "";
                lbgysj.Text = "";
                lbljsj.Text = "";
                lbshsj.Text = "";
                lbfjhsj.Text = "";
                lbpfbsj.Text = "";
                lbqtsj.Text = "";
                lbxin.Text = "";
                lbgan1.Text = "";
                lbpi.Text = "";
                lbfei1.Text = "";
                lbpf.Text = "";
                lbtzqt.Text = "";
                lbxiongpian.Text = "";
                lbdabian1.Text = "";
                lbdabian2.Text = "";
                lbgangongneng1.Text = "";
                lbgangongneng2.Text = "";
                lbgangongneng3.Text = "";
                lbhyqt.Text = "";
            }

        }

        private void lbgangongneng2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
