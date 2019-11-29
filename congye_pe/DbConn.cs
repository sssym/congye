using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace congye_pe
{
    class DbConn
    {
        private string StrConn;
        public SqlConnection SqlDrConn = null;
        public SqlConnection SqlDaConn = null;
        public SqlConnection SqlCmdConn = null;
        public static string ServerName = "";
        public static string Sfxwzyy = "";
        public static string SerUser = "";
        public static string SerPassword = "";
        public static string Database = "";
        public static string Print = "";
        public static bool SerLogin = true;  //登录模式
        private XmlDocument XmlDoc = new XmlDocument();
        private string FilePath = @".\SetupDataBase.Xml";
        public static string x1 = "";
        public static string x2 = "";
        public static string x3 = "";
        public static string x4 = "";
        public static string x5 = "";
        public static string x6 = "";
        public static string x7 = "";
        public static string x8 = "";
        public static string y1 = "";
        public static string y2 = "";
        public static string y3 = "";
        public static string y4 = "";
        public static string y5 = "";
        public static string y6 = "";
        public static string y7 = "";
        public static string y8 = "";
        public static string gao = "";
        public static string kuan = "";
        public static string fontsize = "";
        public static string appCode = "";
        public static string version = "";
        public static string ORGCODE = "";
        public static string HOSNAME = "";
        public static string sfzs = "";
        public static string sfptqrmyy = "";
        public static string tjf = "";


        public DbConn()
        {
            //StrConn = "server=localhost;integrated security=sspi;database=housing";
            ReadXml();
            //integrated security=sspi;
        }

        public SqlDataReader GetDataReader(string StrSql)
        {
            try
            {
                SqlDrConn = new SqlConnection(StrConn);
                SqlDrConn.Open();
                SqlCommand SqlCmd = new SqlCommand(StrSql, SqlDrConn);
                SqlDataReader SqlDr = SqlCmd.ExecuteReader();
                return SqlDr;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                SqlDrConn.Close();
                return null;
            }
            finally
            {

            }
        }

        public SqlDataAdapter GetDataAdapter(string StrSql)
        {
            if (SqlDaConn != null && SqlDaConn.State == ConnectionState.Open)
            {
                SqlDaConn.Close();
            }
            else
            {
                SqlDaConn = new SqlConnection(StrConn);
            }
            try
            {
                SqlDaConn.Open();
                SqlDataAdapter SqlDa = new SqlDataAdapter(StrSql, SqlDaConn);
                SqlCommandBuilder SqlCb = new SqlCommandBuilder(SqlDa);
                return SqlDa;
            }
            catch (Exception Msg)
            {
                Console.WriteLine(Msg.ToString());
                SqlDaConn.Close();
                return null;
            }
            finally
            {

            }
        }

        public bool GetTransaction(System.Collections.ArrayList StrSqlList)
        {
            SqlConnection SqlTrConn = new SqlConnection(StrConn);
            SqlTransaction SqlTr = null;
            //----------------------------------------------------------
            SqlTrConn.Open();
            SqlTr = SqlTrConn.BeginTransaction();
            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = SqlTrConn;
            SqlCmd.Transaction = SqlTr;
            try
            {
                for (int i = 0; i < StrSqlList.Count; i++)
                {
                    SqlCmd.CommandText = StrSqlList[i].ToString();
                    SqlCmd.ExecuteNonQuery();
                }
                SqlTr.Commit();
            }
            catch (Exception ex)
            {
                SqlTr.Rollback();
                SqlTrConn.Close();
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString() + SqlCmd.CommandText);
                return false;
            }
            SqlTrConn.Close();
            return true;
        }

        public int GetSqlCmd(string StrSql)
        {
            int k = 0;
            try
            {
                
                if (SqlCmdConn != null && SqlCmdConn.State == ConnectionState.Open)
                {
                    SqlCmdConn.Close();
                }
                else
                {
                    SqlCmdConn = new SqlConnection(StrConn);
                }
                SqlCmdConn = new SqlConnection(StrConn);
                SqlCommand SqlCmd = new SqlCommand(StrSql, SqlCmdConn);
                SqlCmdConn.Open();
                k = SqlCmd.ExecuteNonQuery();
                SqlCmdConn.Close();
                return k;
            }
            catch (Exception Msg)
            {
                System.Windows.Forms.MessageBox.Show(Msg.Message);
                return k;
            }
            finally
            {
               
            }
        }

        private void ReadXml()
        {
            //读取Xml文档
            string StrNode = "";
            XmlNodeReader reader = null;
            try
            {
                // 装入指定的XML文档
                XmlDoc.Load(FilePath);
                // 设定XmlNodeReader对象来打开XML文件
                reader = new XmlNodeReader(XmlDoc);
                // 读取XML文件中的数据，并显示出来
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            StrNode = reader.Name;
                            break;
                        case XmlNodeType.Text:
                            if (StrNode.Equals("ServerName"))
                            {
                                ServerName = reader.Value;
                            }
                            else if (StrNode.Equals("SerUser"))
                            {
                                SerUser = reader.Value;
                            }
                            else if (StrNode.Equals("SerPassword"))
                            {
                                SerPassword = reader.Value;

                            }
                            else if (StrNode.Equals("SerLogin"))
                            {
                                SerLogin = Convert.ToBoolean(reader.Value);
                            }
                            else if (StrNode.Equals("Database"))
                            { Database = reader.Value; }
                            else if (StrNode.Equals("x1"))
                            { x1 = reader.Value; }
                            else if (StrNode.Equals("x2"))
                            { x2 = reader.Value; }
                            else if (StrNode.Equals("x3"))
                            { x3 = reader.Value; }
                            else if (StrNode.Equals("x4"))
                            { x4 = reader.Value; }
                            else if (StrNode.Equals("x5"))
                            { x5 = reader.Value; }
                            else if (StrNode.Equals("x6"))
                            { x6 = reader.Value; }
                            else if (StrNode.Equals("x7"))
                            { x7 = reader.Value; }
                            else if (StrNode.Equals("x8"))
                            { x8 = reader.Value; }
                            else if (StrNode.Equals("y1"))
                            { y1 = reader.Value; }
                            else if (StrNode.Equals("y2"))
                            { y2 = reader.Value; }
                            else if (StrNode.Equals("y3"))
                            { y3 = reader.Value; }
                            else if (StrNode.Equals("y4"))
                            { y4 = reader.Value; }
                            else if (StrNode.Equals("y5"))
                            { y5 = reader.Value; }
                            else if (StrNode.Equals("y6"))
                            { y6 = reader.Value; }
                            else if (StrNode.Equals("y7"))
                            { y7 = reader.Value; }
                            else if (StrNode.Equals("y8"))
                            { y8 = reader.Value; }
                            else if (StrNode.Equals("gao"))
                            { gao = reader.Value; }
                            else if (StrNode.Equals("kuan"))
                            { kuan = reader.Value; }
                            else if (StrNode.Equals("fontsize"))
                            { fontsize = reader.Value; }
                            else if (StrNode.Equals("APPCODE"))
                            { appCode = reader.Value; }
                            else if (StrNode.Equals("VERSION"))
                            { version = reader.Value; }
                            else if (StrNode.Equals("ORGCODE"))
                            { ORGCODE = reader.Value; }
                            else if (StrNode.Equals("HOSNAME"))
                            { HOSNAME = Decodebase64(reader.Value); }
                            else if (StrNode.Equals("print"))
                            { Print = reader.Value; }
                            else if (StrNode.Equals("sfzs"))
                            { sfzs = reader.Value; }
                            else if (StrNode.Equals("sfxwzyy"))
                            { Sfxwzyy = reader.Value; }
                            else if (StrNode.Equals("sfptqrmyy"))
                            { sfptqrmyy = reader.Value; }
                            else if (StrNode.Equals("tjf"))
                            { tjf = reader.Value; }

                            break;
                    }
                }
                if (ServerName.Equals(""))
                {
                    System.Windows.Forms.MessageBox.Show("配置文件错误，无法连接！", "系统提示"
                        , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else if (SerLogin.Equals(""))
                {
                    System.Windows.Forms.MessageBox.Show("配置文件错误，无法连接！", "系统提示"
                        , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }
                else
                {
                    StrConn = ToStrConn(ServerName, SerLogin, SerUser, SerPassword, Database);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("数据库配置文件错误，请重新配置文件！", "错误提示"
                    , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                // System.Windows.Forms.Application.Exit();
            }
            finally
            {
                //清除打开的数据流
                if (reader != null)
                    reader.Close();
            }
        }

        public static string ToStrConn(string ServerName, bool SerLogin, string SerUser, string SerPassword, string dataBase)
        {
            if (ServerName.Equals(".") || ServerName.Equals(""))
            {
                ServerName = "(local)";
            }
            if (SerLogin == true)
            {
                return "server=" + ServerName + ";integrated security=sspi;database=" + dataBase;
            }
            else
            {
                return "server=" + ServerName + ";database=" + dataBase + ";uid=" + SerUser + ";pwd=" + SerPassword;
            }
        }

        private string Encodebase64(string code)
        {

            string encode = "";
            byte[] bytes = Encoding.Default.GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }

        private string Decodebase64(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.Default.GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        public void LoadGv(string strSql,System.Windows.Forms.DataGridView dataGridView1)
        {
            SqlDataAdapter sqlDataAdapter = GetDataAdapter(strSql);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
        }
    }
}
