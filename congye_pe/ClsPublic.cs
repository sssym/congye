using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Drawing.Drawing2D;
using System.Web.Security;
using System.IO;
using System.Data.SqlClient;
using System.Collections;
namespace congye_pe
{
    class ClsPublic
    {

        public static string str_sign = "";
        public string str_appcode = "";
        public string str_version = "";
        public string str_orgcode = "";
        SqlDataReader sqlDataReader = null;
        public DbConn dbConn = null;
      public   string  strSql = "";
       public static  ArrayList arrayList;
        static public int icount = 0;
        private XmlDocument XmlDoc = new XmlDocument();
        public ClsPublic()
        {
            str_appcode = DbConn.appCode;
            str_version = DbConn.version;
            str_orgcode = DbConn.ORGCODE;
            dbConn = new DbConn();
        }
        public  string getAdress(string str_type)
        {
            strSql = "select value from table_canshu where beizhu='" + str_type+"地点'";
            sqlDataReader = dbConn.GetDataReader(strSql);
            if (sqlDataReader.Read())
            {
                return sqlDataReader.GetValue(0).ToString();
            }
            else
            {
                return "";
            }

        }
        public static string byteToHexStr(string imagepath)
        {
           
            byte[] bytes = GetPictureData(imagepath); ;
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }
        public static StringBuilder  aa()
        {
            StringBuilder ss = new StringBuilder(); ;
            FileStream fs = new FileStream(@".\zp.bmp", FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            StreamWriter sw = new StreamWriter("d:\\bb.txt");
            int length = (int)fs.Length;
            while (length > 0)
            {
                byte tempByte = br.ReadByte();
                string tempStr = Convert.ToString(tempByte, 16);
                sw.WriteLine(tempStr);
                ss = ss.Append(tempStr);
                length--;
                
            }
            fs.Close();
            br.Close();
            sw.Close();
            return ss;
        }
        public static void getXML(DataGridView dgv)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration dec = xmldoc.CreateXmlDeclaration("1.0", "GBK", null);
            xmldoc.AppendChild(dec);
            XmlElement DataInfo = xmldoc.CreateElement("dataset");
            xmldoc.AppendChild(DataInfo);

            XmlNode Data_fields1 = xmldoc.SelectSingleNode("dataset");
            for (int i = 0; i < dgv.RowCount; i++) {
                XmlElement t_cyjkz_jkzxx = xmldoc.CreateElement("t_cyjkz_jkzxx");
                t_cyjkz_jkzxx.SetAttribute("ZJLB", dgv[0, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("ZJHM", dgv[1, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("CZRXM", dgv[2, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("XB", dgv[3, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("TJRQ", dgv[4, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("JKZZH", dgv[5, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("SQRQ", dgv[6, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("NL", dgv[7, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("GZ", dgv[8, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("GZDW", dgv[9, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("DWLXDH", dgv[10, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("SQDH", dgv[11, i].Value.ToString());
                t_cyjkz_jkzxx.SetAttribute("ZSZT", dgv[12, i].Value.ToString());
                DataInfo.AppendChild(t_cyjkz_jkzxx);
            }
            if (File.Exists(@".\jkz.xml"))
            {
                File.Delete(@".\jkz.xml");
            }
            xmldoc.Save(@".\jkz.xml");//你要保存的路径
        }
        public static void getXML1(DataGridView dgv)
        {

            string str = "";
            str = str + "<?xml version='1.0' encoding='GB2312'?><DataInfo>";// "<dataset><t_cyjkz_jkzxx"
            for (int i = 0; i < dgv.RowCount; i++)
            {
                str = str + "< Data - fields ID = \"" + (i + 1).ToString() + "\" >"
                  + " <ZJLB>0</ZJLB>"
                  + " <ZJHM>" + dgv[1, i].Value.ToString() + "</ZJHM>"
                  + " <CZRXM>" + dgv[2, i].Value.ToString() + "</CZRXM>"
                  + " <XB>" + dgv[3, i].Value.ToString() + "</CZRXM>"
                  + " <TJRQ>" + DateTime.Parse(dgv[4, i].Value.ToString()).ToString("yyyy-MM-dd") + "</TJRQ>"
                  + " <JKZZH>" + dgv[5, i].Value.ToString() + " </JKZZH>"
                  + " <DYZT>1</DYZT>"
                  + " <LZZT>1</LZZT>"
                  + " <ZSZT>1</ZSZT>"
                  + " <SQRQ>" + DateTime.Parse(dgv[6, i].Value.ToString()).ToString("yyyy-MM-dd") + "</SQRQ>"
                  + " <NL>" + dgv[7, i].Value.ToString() + "</NL>";
                if (dgv[8, i].Value.ToString() == "食品安全")
                { str = str + " <JKZLB>0</JKZLB>"; }
                else if (dgv[8, i].Value.ToString() == "公共卫生")
                { str = str + " <JKZLB>1</JKZLB>"; }
                else if (dgv[8, i].Value.ToString() == "劳动卫生")
                { str = str + " <JKZLB>2</JKZLB>"; }

                str=str+ " <JKZJCDW>" +DbConn.HOSNAME + "</JKZJCDW>"
                  + " <JCDWSZDQ>" + DbConn.ORGCODE + "</JCDWSZDQ>"
                  + " <GZDW>" + dgv[9, i].Value.ToString() + "</GZDW>"
                  + " <LXDH>" + dgv[10, i].Value.ToString() + "</LXDH>"
                  + " <ZP>" + Base64Tobyte16(dgv[11, i].Value.ToString()) + "</ZP>"
                  + " </Data-fields>";


            }
            str = str + "  </DataInfo>";
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml(@".\jkz.xml");
            //XmlNode Peoplelist = doc.SelectSingleNode("OnlineEdu/body/PeopleList");
            StringBuilder sb = new StringBuilder();
            sb.Append(str);
            if (File.Exists(@"D:\jkz.xml"))
            {
                File.Delete(@"D:\jkz.xml");
            }


            FileStream sf = new FileStream(@".\jkz.xml", FileMode.Create);
            sf.Flush();
            sf.Close();


            StreamWriter sw = new StreamWriter(@".\jkz.xml", true);

            sw.WriteLine(sb);
            sw.Flush();
            sw.Close();



            // return str;

        }
        public void saveXml(DataGridView dgv,string str_title)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration dec = xmldoc.CreateXmlDeclaration("1.0", "GB2312", null);
            xmldoc.AppendChild(dec);
            XmlElement DataInfo = xmldoc.CreateElement("DataInfo");
            xmldoc.AppendChild(DataInfo);

            XmlNode Data_fields1 = xmldoc.SelectSingleNode("DataInfo");
            for (int i = 0; i < dgv.RowCount; i++)
            {
                XmlElement xmlElement = xmldoc.CreateElement("Data-fields");
                DataInfo.AppendChild(xmlElement);
                xmlElement.SetAttribute("ID", (i + 1).ToString());
                XmlNode Data_fields2 = xmldoc.SelectSingleNode("Data-fields");
                XmlElement xml1 = xmldoc.CreateElement("ZJLB");
                xml1.InnerText = "0";
                xmlElement.AppendChild(xml1);
                XmlElement xml2 = xmldoc.CreateElement("ZJHM");
                xml2.InnerText = dgv[1, i].Value.ToString();
                xmlElement.AppendChild(xml2);
                XmlElement xml3 = xmldoc.CreateElement("CZRXM");
                xml3.InnerText = dgv[2, i].Value.ToString();
                xmlElement.AppendChild(xml3);
                XmlElement xml4 = xmldoc.CreateElement("XB");
                if (dgv[3, i].Value.ToString() == "男")
                {
                    xml4.InnerText = "1";
                }
                else
                {
                    xml4.InnerText = "2";
                }
                xmlElement.AppendChild(xml4);
                XmlElement xml5 = xmldoc.CreateElement("TJRQ");
                xml5.InnerText = DateTime.Parse(dgv[4, i].Value.ToString()).ToString("yyyy-MM-dd");
                xmlElement.AppendChild(xml5);
                XmlElement xml6 = xmldoc.CreateElement("JKZZH");
                xml6.InnerText = dgv[5, i].Value.ToString();
                xmlElement.AppendChild(xml6);
                XmlElement xml7 = xmldoc.CreateElement("DYZT");
                xml7.InnerText = "1";
                xmlElement.AppendChild(xml7);
                XmlElement xml8 = xmldoc.CreateElement("LZZT");
                xml8.InnerText = "1";
                xmlElement.AppendChild(xml8);
                XmlElement xml9 = xmldoc.CreateElement("ZSZT");
                xml9.InnerText = "0";
                xmlElement.AppendChild(xml9);
                XmlElement xml10 = xmldoc.CreateElement("SQRQ");
                xml10.InnerText = DateTime.Parse(dgv[6, i].Value.ToString()).ToString("yyyy-MM-dd");
                xmlElement.AppendChild(xml10);
                XmlElement xml11 = xmldoc.CreateElement("NL");
                xml11.InnerText = dgv[7, i].Value.ToString();
                xmlElement.AppendChild(xml11);
                XmlElement xml12 = xmldoc.CreateElement("JKZLB");
                if (dgv[8, i].Value.ToString() == "食品安全")
                {
                    xml12.InnerText = "0";
                }
                else if (dgv[8, i].Value.ToString() == "公共卫生")
                {
                    xml12.InnerText = "1";
                }
                else if (dgv[8, i].Value.ToString() == "劳动卫生")
                { xml12.InnerText = "2"; }
               xmlElement.AppendChild(xml12);
                XmlElement xml13 = xmldoc.CreateElement("JKZJCDW");
                xml13.InnerText = DbConn.HOSNAME;
                xmlElement.AppendChild(xml13);
                XmlElement xml14 = xmldoc.CreateElement("CDWSSDQ");
                xml14.InnerText = DbConn.ORGCODE;
                xmlElement.AppendChild(xml14);
                XmlElement xml15 = xmldoc.CreateElement("GZDW");
                xml15.InnerText = dgv[9, i].Value.ToString();
                xmlElement.AppendChild(xml15);
                XmlElement xml16 = xmldoc.CreateElement("LXDH");
                xml16.InnerText = dgv[10, i].Value.ToString();
                xmlElement.AppendChild(xml16);
                XmlElement xml17 = xmldoc.CreateElement("ZP");
                xml17.InnerText = Base64Tobyte16(dgv[11, i].Value.ToString());
                xmlElement.AppendChild(xml17);
                //Data_fields.AppendChild();
                DataInfo.AppendChild(xmlElement);
                icount = i;
            }
            SaveFileDialog kk = new SaveFileDialog();
            kk.Title = "保存XML文件";
            kk.Filter = "XML文件(*.XML) |*.XML |所有文件(*.*) |*.*";
            kk.FilterIndex = 1;
            kk.FileName = str_title+".xml";
            if (kk.ShowDialog() == DialogResult.OK)
            {
                string FileName = kk.FileName;
                if (File.Exists(FileName))
                    File.Delete(FileName);
                xmldoc.Save(FileName);//你要保存的路径
            }
           
        }
        public static string imageTobyte16(string fileName)
        {

            string strData = "";
            using (FileStream fsReade = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                // 声明一个byte数组用来读取照片：  
                byte[] buf = new byte[60000];
                int n = 0;
                int len = 0;
                // 循环读取照片，并存入byte数组中：  
                while (true)
                {
                    len = fsReade.Read(buf, n, 1024);
                    if (len == 0)
                    {
                        break;
                    }
                    n = n + len;
                }
                for (int i = 0; i < n; i++)
                {
                    // 将byte数组中的每一个字节都转换成16进制字符串：  
                    strData += buf[i].ToString("X2");
                }
                return strData;
            }
        }
        public static string Base64Tobyte16(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            string a ="";
            for (int i = 0; i < bytes.Length; i++)
            {
               // a += bytes[i].ToString(byte);
                decode += bytes[i].ToString("X2");
            }
            
            return decode;
        }
        public static byte[] GetPictureData(string imagepath)
        {
            /**/////根据图片文件的路径使用文件流打开，并保存为byte[] 
            FileStream fs = new FileStream(imagepath, FileMode.Open);//可以是其他重载方法 
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();

            
            return byData;
        }
       

        public static string getIndexString(string str,int int_count)
        {
            return str.Substring(0, int_count);
        }
        public static string ToMD5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        }
//        public XmlDocument QueryWebService(string method, string reqArg)
//        {

//            string RequestParam = @"<REQUEST>
//<HEADER>
//<APPCODE>{0}</APPCODE>
//<METHOD>{1}</METHOD>
//<VERSION>{2}</VERSION>
//<REQTIME>{3:yyyy-MM-dd HH:mm:ss}</REQTIME>
//<ORGCODE>{4}</ORGCODE>
//</HEADER>
//<BODY>{2}</BODY>
//</REQUEST>";
//            DateTime dt = DateTime.Now;
//            string fmtParms = String.Format(RequestParam, str_appcode, method, str_version, dt, str_orgcode, reqArg);

//            pe_webservice.WebserviceCallEntranceClient webClient = new pe_webservice.WebserviceCallEntranceClient();
//            byte[] byteStr = Encoding.UTF8.GetBytes(fmtParms);
//            string args0 = Convert.ToBase64String(byteStr);
//            string result = webClient.CallFun(args0);

//            return ReadXmlResponse(Encoding.UTF8.GetString(Convert.FromBase64String(result)));
//        }
        //public string QueryWebService(string method, string reqArg)
        //{

        //    string RequestParam = @"<REQUEST>
        //                            <HEADER><APPCODE>{0}</APPCODE>
        //                            <METHOD>{1}</METHOD>
        //                            <VERSION>{2}</VERSION>
        //                            <REQTIME>{3:yyyy-MM-dd HH:mm:ss}</REQTIME>
        //                            <ORGCODE>{4}</ORGCODE>
        //                            </HEADER>
        //                            <BODY>{5}</BODY>
        //                            </REQUEST>";
        //    DateTime dt = DateTime.Now;
        //    string fmtParms = String.Format(RequestParam, str_appcode, method, str_version, dt, str_orgcode, reqArg);

        //  //  pe_webservice.WebserviceCallEntranceClient webClient = new pe_webservice.WebserviceCallEntranceClient();
        //    byte[] byteStr = Encoding.UTF8.GetBytes(fmtParms);
        //    string args0 = Convert.ToBase64String(byteStr);
        //    string result = webClient.CallFun(args0);
        //    return Encoding.UTF8.GetString(Convert.FromBase64String(result));
        //}
//        public string QueryWebServiceBySign(string method, string reqArg)
//        {
//            string SIGNATURE = getSign("900100");
//            string RequestParam = @"<REQUEST>
//<HEADER>
//<APPCODE>{0}</APPCODE>
//<METHOD>{1}</METHOD>
//<VERSION>{2}</VERSION>
//<SIGN>{3}</SIGN>
//<REQTIME>{4:yyyy-MM-dd HH:mm:ss}</REQTIME>
//<ORGCODE>{5}</ORGCODE>
//</HEADER>
//<BODY>{6}</BODY>
//</REQUEST>";
//            DateTime dt = DateTime.Now;
//            string fmtParms = String.Format(RequestParam, str_appcode, method, str_version, SIGNATURE, dt, str_orgcode, reqArg);

//            pe_webservice.WebserviceCallEntranceClient webClient = new pe_webservice.WebserviceCallEntranceClient();
//            byte[] byteStr = Encoding.UTF8.GetBytes(fmtParms);
//            string args0 = Convert.ToBase64String(byteStr);
//            string result = webClient.CallFun(args0);
//            return Encoding.UTF8.GetString(Convert.FromBase64String(result));
//        }
        private XmlDocument ReadXmlResponse(string retXml)
        {
            if (retXml != "")
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(retXml);
                return doc;
            }
            else
            {
                return null;
            }
        }
        //public string getSign(string method)
        //{

        //    //string str_Signature_xml = @"<METHOD>{}</METHOD>"
        //    //    + "<ORGCODE>" + DbConn.ORGCODE + "</ORGCODE>"
        //    //    + "<USER>" + DbConn.ORGCODE + "</USER>"
        //    //    + "<PASSWORD>e10adc3949ba59abbe56e057f20f883e</PASSWORD>"
        //    //    + "<UUID>" + DbConn.appCode + @"</UUID>";
        //    string str_Signature_xml = @"<METHOD>{0}</METHOD>"
        //        + "<ORGCODE>{1}</ORGCODE>"
        //        + "<USER>{2}</USER>"
        //        + "<PASSWORD>{3}</PASSWORD>"
        //        + "<UUID>{4}</UUID>";
        //    string fmtParms = String.Format(str_Signature_xml, method, DbConn.ORGCODE, DbConn.ORGCODE, "e10adc3949ba59abbe56e057f20f883e", DbConn.appCode);


        //    string sign = QueryWebService(method, fmtParms);
        //    string str_sign = getXmlNode(sign, "SIGNATURE");
        //    return ToMD5(str_sign);
        //}
        public string getXmlNode(string xmlDoc, string NodeName)
        {
            string StrNode = "";
            string return_string = "";
            XmlNodeReader reader = null;
            try
            {
                // 装入指定的XML文档
                XmlDoc.LoadXml(xmlDoc);
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
                            if (StrNode.Equals(NodeName))
                            {
                                return_string = reader.Value;
                            }

                            break;
                    }
                }

            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("数据接口连接错误！", "错误提示"
                    , System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                // System.Windows.Forms.Application.Exit();
            }
            finally
            {
                //清除打开的数据流
                if (reader != null)
                    reader.Close();
            }
            return return_string;
        }
    }

}
