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
    public partial class frmEdit :Form
    {
        DbConn dbConn;
        string strSql = "";
        SqlDataReader sqlDataReader ;
        public static string strTjbh = "";
       public static ArrayList arrayList;
        public frmEdit()
        {
            InitializeComponent();
            arrayList = new ArrayList();
            dbConn = new DbConn();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                this.Close();
            }
            arrayList = new ArrayList();
            strTjbh = "";
            strSql = "select table_renyuan.tjbh,xm,xb,nl,sfzhm,lxdh,gzdw,hylbdl,sfzzp,"
                + "ganyan,liji,shanghan,feijiehe,pifubing,qita,sfsfz,djrq "
                + "from table_renyuan,table_tjjg where table_renyuan.tjbh=table_tjjg.tjbh and sfsh=0 and (isdelete!=1 or isdelete is null) ";
            if (comboBox1.SelectedIndex == 0)
            { strSql = strSql + "and table_renyuan.tjbh like '%" + textBox1.Text + "%' "; }
            else if (comboBox1.SelectedIndex == 1)
            {
                strSql = strSql + " and table_renyuan.sfzhm like '%"+textBox1.Text+"%'";
            }
            strSql = strSql + " order by table_renyuan.tjbh desc";
            sqlDataReader = dbConn.GetDataReader(strSql);
            if (sqlDataReader.Read())
            {
                for (int i = 0; i < 17; i++)
                {
                    arrayList.Add(sqlDataReader.GetValue(i).ToString());
                }
            }
            this.Close();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
