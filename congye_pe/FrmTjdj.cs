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
using AForge;
using AForge.Controls;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.Drawing.Printing;

using System.Runtime.InteropServices;
using System.Threading;

namespace congye_pe
{
    public partial class FrmTjdj : Form
    {
        DbConn dbConn = null;
        
        ArrayList arrayList = null;
        string str_Signature_xml;
        StringFormat sf;
        string strSql = "";
        ArrayList al = null;
        FilterInfoCollection videoDevices;
         VideoCaptureDevice videoSource;
        string str_picBase64_1 = "";
        string str_picBase64_2 = "";
        public int selectedDeviceIndex = 0;
        int ifsfz = 0;
        ClsBase64 clsBase64 = null;
        public StringBuilder name;     //姓名
        public String sex;      //性别
        public String people;    //民族，护照识别时此项为空
        public String birthday;   //出生日期
        public String address;  //地址，在识别护照时导出的是国籍简码
        public string number;  //地址，在识别护照时导出的是国籍简码
        public string signdate;   //签发日期，在识别护照时导出的是有效期至 
        public string validtermOfStart;  //有效起始日期，在识别护照时为空
        public string validtermOfEnd;  //有效截止日期，在识别护照时为空
        bool isClose = false;
        int iRetUSB = 0, iRetCOM = 0;
        
        ClsPublic clsPublic;

        public FrmTjdj()
        {
            InitializeComponent();
            dbConn = new DbConn();
            clsPublic = new ClsPublic();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbo_xb.SelectedIndex = 0;
            cbo_hylbdl.SelectedIndex = 0;
            dtp_djrq.Value = DateTime.Now;
            clsBase64 = new ClsBase64();
            ifsfz = 0;
             str_picBase64_1 = "";
             str_picBase64_2 = "";
            FrmVideo.str_picBase64_1 = "";
            FrmVideo.str_picBase64_2 = "";
            FrmVideo.bidui = false;
            pictureBox8.Enabled = false;
            pictureBox3.Visible = true;
            txt_xm.Enabled = true;
            cbo_xb.Enabled = true;
            txt_nl.Enabled = true;
            txt_sfzhm.Enabled = true;
            //button6.Enabled = false;
            try
            {

                int iPort;
                for (iPort = 1001; iPort <= 1016; iPort++)
                {
                    iRetUSB = CVRSDK.CVR_InitComm(iPort);
                    if (iRetUSB == 1)
                    {
                        break;
                    }
                }
                if (iRetUSB != 1)
                {
                    for (iPort = 1; iPort <= 4; iPort++)
                    {
                        iRetCOM = CVRSDK.CVR_InitComm(iPort);
                        if (iRetCOM == 1)
                        {
                            break;
                        }
                    }
                }

                if ((iRetCOM == 1) || (iRetUSB == 1))
                {
                   // this.label9.Text = "初始化成功！";
                }
                else
                {
                    MessageBox.Show("身份证读卡器连接失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            al = new ArrayList();
            string xm = this.txt_xm.Text;
            string xb = cbo_xb.SelectedItem.ToString();
           // txt_nl.Text = getAge(txt_sfzhm.Text);
            string nl = this.txt_nl.Text;
            string sfzhm = txt_sfzhm.Text;
            string gzdw = txt_gzdw.Text;
            string lxdh = txt_lxdh.Text;
            string tjbh = "";
            string gy = txt_gy.Text;
            string lj = txt_lj.Text;
            string sh = txt_sh.Text;
            string fjh = txt_fjh.Text;
            string pfb = txt_pfb.Text;
            string qt = txt_qt.Text;
            string hylbdl = cbo_hylbdl.SelectedItem.ToString();

            string sfzzp = clsBase64.ImgToBase64String((Bitmap)pictureBox1.Image);
            string lbzp = str_picBase64_2;
            string djrq = dtp_djrq.Value.ToString("yyyy-MM-dd");
            string NAME = txt_xm.Text;
            string IDCARD = txt_sfzhm.Text;
            string GENDER = cbo_xb.SelectedIndex.ToString();
            if (CheckIDCard(txt_sfzhm.Text))
            {
                string AGE = txt_nl.Text;

                #region 2018-04-26新接口没有用到，注释。
                /*2018-04-26新接口没有用到，注释。
                string mz = "";
                if (cbo_mz.SelectedItem.ToString() == "ZZ其他")
                {
                    mz = cbo_mzqt.SelectedItem.ToString().Substring(0, 2);
                }
                else { mz = cbo_mz.SelectedItem.ToString().Substring(0, 2); }
                string hylbxl = cbo_hylbxl.SelectedItem.ToString().Substring(0,2); 

                string dglx = cbo_dglx.SelectedItem.ToString().Substring(0,1);

                string ORDERTYPE = ClsPublic.getIndexString(cbo_dglx.SelectedItem.ToString(), 1);
                */
                #endregion
                if (label1.Text.Length == 0)
                {
                    if (xm.Length != 0 && nl.Length != 0 && sfzhm.Length >= 10 && gzdw.Length != 0 && lxdh.Length != 0 && pictureBox1.Image != null)
                    {

                        strSql = "select value from table_canshu where type=1";
                        SqlDataReader sdr = dbConn.GetDataReader(strSql);
                        if (sdr.Read())
                        {
                            tjbh = sdr.GetValue(0).ToString();
                        }
                        string str_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

                        strSql = "insert into table_renyuan(xm,xb,nl,sfzhm,lxdh,gzdw,hylbdl,sfzzp,djrq,sfsh,tjbh,sfsfz) "
                            + "values('" + xm + "','" + xb + "'," + nl + ",'" + sfzhm + "','" + lxdh + "','" + gzdw + "','" + hylbdl + "','" + sfzzp + "','" + djrq + "',0,'" + tjbh + "'," + ifsfz.ToString() + ")";
                        al.Add(strSql);
                        strSql = "insert into table_tjjg(tjbh,ganyan,liji,shanghan,feijiehe,pifubing,qita) " +
                            "values('" + tjbh + "','" + gy + "','" + lj + "','" + sh + "','" + fjh + "','" + pfb + "','" + qt + "')";
                        al.Add(strSql);
                        strSql = "update table_canshu set value=value+1 where type=1";
                        al.Add(strSql);
                        if (dbConn.GetTransaction(al))
                        {
                            MessageBox.Show("保存成功！");
                            label1.Text = tjbh.ToString();
                            //button6.Enabled = true;
                            pictureBox8.Enabled = true;
                        }


                    }
                    else
                    {

                        MessageBox.Show("请把登记表单填写完整！");


                        return;
                    }

                }
                else
                {
                    if (xm.Length != 0 && nl.Length != 0 && sfzhm.Length >= 10 && gzdw.Length != 0 && lxdh.Length != 0 && pictureBox1.Image != null)
                    {
                        strSql = "update table_renyuan set xm='" + xm.ToString() + "',xb='" + xb.ToString() + "',nl=" + nl.ToString()
                       + ",sfzhm='" + sfzhm.ToString() + "',gzdw='" + gzdw.ToString() + "',lxdh='" + lxdh + "',hylbdl='" + hylbdl.ToString() + "',"
                       + "sfzzp='" + sfzzp.ToString() + "',djrq='" + djrq.ToString() + "'"
                       + " where tjbh='" + label1.Text + "'";
                        al.Add(strSql);
                        strSql = "update table_tjjg set ganyan='" + txt_gy.Text + "',liji='" + txt_lj.Text + "',shanghan='" + txt_sh.Text + "',feijiehe='" + txt_fjh.Text + "',pifubing='" + txt_pfb.Text + "',qita='" + txt_qt.Text + "' "
                       + " where tjbh='" + label1.Text + "'";
                        al.Add(strSql);
                        if (dbConn.GetTransaction(al))
                        {
                            MessageBox.Show("保存成功！");

                            pictureBox8.Enabled = true;
                        }

                    }
                    else
                    {
                        MessageBox.Show("请把登记表单填写完整！"); return;
                    }

                }
            }
            else
            {
                MessageBox.Show("输入身份证号码不合法！");
                txt_sfzhm.Focus();
            }

        }
    //    public string getCERTID(string ORDERTYPE, string NAME, string IDCARD, string GENDER, string AGE, string sfzzp, string lbzp, string hylbdl, string hylbxl, string tjbh)
    //    {
    //        if (ifsfz == 1)
    //        {
    //            str_Signature_xml = @"<VEHICLE></VEHICLE>"
    //+ "<ORDER></ORDER>"
    //+ "<ORDERTYPE>" + ORDERTYPE + "</ORDERTYPE>"
    //+ "<NAME>" + NAME + "</NAME>"
    //+ "<IDCARD>" + IDCARD + "</IDCARD>"
    //+ "<GENDER>" + GENDER + "</GENDER>"
    //+ "<AGE>" + AGE + "</AGE>"
    //+ "<ETHNIC>01</ETHNIC>"
    //+ "<HEAD>" + sfzzp + "</HEAD>" + "<CAPHEAD></CAPHEAD>"
    //+ "<FACE>" + lbzp + "</FACE>" + "<MOBILE></MOBILE>"
    //+ "<OPENID></OPENID>"
    //+ "<TYPE>" + hylbdl + "</TYPE>"
    //+ "<INDUSTRY>" + hylbxl + "</INDUSTRY>"
    //+ "<DEGREE></DEGREE>"
    //+ "<ORDERTIME>" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "</ORDERTIME>"
    //+ "<ENROLLTIME>" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "</ENROLLTIME>"
    //+ "<ENROLLNO>" + tjbh + "</ENROLLNO>";
    //        }
    //        else
    //        {
    //            str_Signature_xml = @"<VEHICLE></VEHICLE>"
    //   + "<ORDER></ORDER>"
    //   + "<ORDERTYPE>" + ORDERTYPE + "</ORDERTYPE>"
    //   + "<NAME>" + NAME + "</NAME>"
    //   + "<IDCARD>" + IDCARD + "</IDCARD>"
    //   + "<GENDER>" + GENDER + "</GENDER>"
    //   + "<AGE>" + AGE + "</AGE>"
    //   + "<ETHNIC>01</ETHNIC>"
    //   + "<HEAD></HEAD>" + "<CAPHEAD>" + sfzzp + "</CAPHEAD>"
    //   + "<FACE>" + lbzp + "</FACE>" + "<MOBILE></MOBILE>"
    //   + "<OPENID></OPENID>"
    //   + "<TYPE>" + hylbdl + "</TYPE>"
    //   + "<INDUSTRY>" + hylbxl + "</INDUSTRY>"
    //   + "<DEGREE></DEGREE>"
    //   + "<ORDERTIME>" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "</ORDERTIME>"
    //   + "<ENROLLTIME>" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "</ENROLLTIME>"
    //   + "<ENROLLNO>" + tjbh + "</ENROLLNO>";
    //        }
    //        string xml = clsPublic.QueryWebServiceBySign("C00300", str_Signature_xml);
    //     //   MessageBox.Show(xml);
    //        return clsPublic.getXmlNode(xml, "CERTID");
    //    }
        public void FillData()
        {
            try
            {
                /*
                byte[] name = new byte[30];
                int length = 30;
                CVRSDK.GetPeopleName(ref name[0], ref length);
                //MessageBox.Show();
                byte[] number = new byte[30];
                length = 36;
                CVRSDK.GetPeopleIDCode(ref number[0], ref length);
                byte[] people = new byte[30];
                length = 3;
                CVRSDK.GetPeopleNation(ref people[0], ref length);
                byte[] validtermOfStart = new byte[30];
                length = 16;
                CVRSDK.GetStartDate(ref validtermOfStart[0], ref length);
                byte[] birthday = new byte[30];
                length = 16;
                CVRSDK.GetPeopleBirthday(ref birthday[0], ref length);
                byte[] address = new byte[30];
                length = 70;
                CVRSDK.GetPeopleAddress(ref address[0], ref length);
                byte[] validtermOfEnd = new byte[30];
                length = 16;
                CVRSDK.GetEndDate(ref validtermOfEnd[0], ref length);
                byte[] signdate = new byte[30];
                length = 30;
                CVRSDK.GetDepartment(ref signdate[0], ref length);
                byte[] sex = new byte[30];
                length = 3;
                CVRSDK.GetPeopleSex(ref sex[0], ref length);

                byte[] samid = new byte[32];
                CVRSDK.CVR_GetSAMID(ref samid[0]);
                */
                byte[] imgData = new byte[40960];
                int length = 40960;
                CVRSDK.GetBMPData(ref imgData[0], ref length);
                MemoryStream myStream = new MemoryStream();
                for (int i = 0; i < length; i++)
                {
                    myStream.WriteByte(imgData[i]);
                }
                System.Drawing.Image myImage = System.Drawing.Image.FromStream(myStream);
                //pictureBoxPhoto.Image = myImage;
                pictureBox1.Image = myImage;

                //pictureBox1.ImageLocation = Application.StartupPath + "\\zp.bmp";
                byte[] name = new byte[128];
                length = 128;
                CVRSDK.GetPeopleName(ref name[0], ref length);

                //byte[] cnName = new byte[128];
                //length = 128;
                //CVRSDK.GetPeopleChineseName(ref cnName[0], ref length);

                byte[] number = new byte[128];
                length = 128;
                CVRSDK.GetPeopleIDCode(ref number[0], ref length);

                byte[] peopleNation = new byte[128];
                length = 128;
                CVRSDK.GetPeopleNation(ref peopleNation[0], ref length);

                //byte[] peopleNationCode = new byte[128];
                //length = 128;
                //CVRSDK.GetNationCode(ref peopleNationCode[0], ref length);

                byte[] validtermOfStart = new byte[128];
                length = 128;
                CVRSDK.GetStartDate(ref validtermOfStart[0], ref length);

                byte[] birthday = new byte[128];
                length = 128;
                CVRSDK.GetPeopleBirthday(ref birthday[0], ref length);

                byte[] address = new byte[128];
                length = 128;
                CVRSDK.GetPeopleAddress(ref address[0], ref length);

                byte[] validtermOfEnd = new byte[128];
                length = 128;
                CVRSDK.GetEndDate(ref validtermOfEnd[0], ref length);

                byte[] signdate = new byte[128];
                length = 128;
                CVRSDK.GetDepartment(ref signdate[0], ref length);

                byte[] sex = new byte[128];
                length = 128;
                CVRSDK.GetPeopleSex(ref sex[0], ref length);

                byte[] samid = new byte[128];
                CVRSDK.CVR_GetSAMID(ref samid[0]);

                bool bCivic = true;
                //byte[] certType = new byte[32];
                //length = 32;
                //CVRSDK.GetCertType(ref certType[0], ref length);

                //string strType = System.Text.Encoding.ASCII.GetString(certType);
                //int nStart = strType.IndexOf("I");
                //if (nStart != -1) bCivic = false;

                if (bCivic)
                {
                    cbo_xb.SelectedItem = System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
                    txt_sfzhm.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
                    txt_nl.Text = getAge(txt_sfzhm.Text);
                    txt_xm.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(name).Replace("\0", "").Trim();

                    //labelCnName.Visible = false;
                    //labelAddress.Visible = true;
                    //labelName.Text = "姓名：" + System.Text.Encoding.GetEncoding("GB2312").GetString(name);
                    //labelSex.Text = "性别：" + System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
                    //labelNation.Text = "民族：" + System.Text.Encoding.GetEncoding("GB2312").GetString(peopleNation).Replace("\0", "").Trim();
                    //labelNationCode.Text = "民族代码：" + System.Text.Encoding.GetEncoding("GB2312").GetString(peopleNationCode).Replace("\0", "").Trim();
                    //labelBirthday.Text = "出生日期：" + System.Text.Encoding.GetEncoding("GB2312").GetString(birthday).Replace("\0", "").Trim();
                    //labelIdCardNo.Text = "身份证号：" + System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
                    //labelAddress.Text = "地址：" + System.Text.Encoding.GetEncoding("GB2312").GetString(address).Replace("\0", "").Trim();
                    //labelDepartment.Text = "签发机关：" + System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();
                    //labelValidDate.Text = "有效期限：" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim() + "-" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();
                    //labelSamID.Text = "安全模块号：" + System.Text.Encoding.GetEncoding("GB2312").GetString(samid).Replace("\0", "").Trim();
                }
                else
                {
                    cbo_xb.SelectedItem = System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
                    txt_sfzhm.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
                    txt_nl.Text = getAge(txt_sfzhm.Text);
                    txt_xm.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(name).Replace("\0", "").Trim();

                    //labelCnName.Visible = true;
                    //labelAddress.Visible = false;
                    //labelName.Text = "姓名：" + System.Text.Encoding.GetEncoding("GB2312").GetString(name).Replace("\0", "").Trim();
                    //labelCnName.Text = "中文姓名：" + System.Text.Encoding.GetEncoding("GB2312").GetString(cnName).Replace("\0", "").Trim();
                    //labelSex.Text = "性别：" + System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
                    //labelNation.Text = "国籍：" + System.Text.Encoding.GetEncoding("GB2312").GetString(peopleNation).Replace("\0", "").Trim();
                    //labelNationCode.Text = "国籍代码：" + System.Text.Encoding.GetEncoding("GB2312").GetString(peopleNationCode).Replace("\0", "").Trim();
                    //labelBirthday.Text = "出生日期：" + System.Text.Encoding.GetEncoding("GB2312").GetString(birthday).Replace("\0", "").Trim();
                    //labelIdCardNo.Text = "证件号码：" + System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
                    //labelDepartment.Text = "签发机关：" + System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();
                    //labelValidDate.Text = "有效期限：" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim() + "-" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();
                    //labelSamID.Text = "安全模块号：" + System.Text.Encoding.GetEncoding("GB2312").GetString(samid).Replace("\0", "").Trim();
                }


                //lblAddress.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(address).Replace("\0", "").Trim();
                // lblSex.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(sex).Replace("\0", "").Trim();
                //lblDept.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(signdate).Replace("\0", "").Trim();
                // lblIdCard.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(number).Replace("\0", "").Trim();
               
               // MessageBox.Show(System.Text.Encoding.GetEncoding("GB2312").GetString(people).Trim());
               // cbo_mz.SelectedItem = System.Text.Encoding.GetEncoding("GB2312").GetString(people).Replace("\0", "").Trim();
                // lblNation.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(people).Replace("\0", "").Trim();
                //label11.Text = "安全模块号：" + System.Text.Encoding.GetEncoding("GB2312").GetString(samid).Replace("\0", "").Trim();
                //lblValidDate.Text = System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfStart).Replace("\0", "").Trim() + "-" + System.Text.Encoding.GetEncoding("GB2312").GetString(validtermOfEnd).Replace("\0", "").Trim();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /* private void btnSubmit_Click(object sender, EventArgs e)
          {
              string filePath = this.label2.Text.ToString();

              if (filePath.Length == 0)
              {
                  MessageBox.Show("请选择图片！");
                  return;
              }
              if (gl_studentid.Length == 0)
              {
                  MessageBox.Show("请选择要修改的记录！");
              }

              string sql = "update student set img=@img where studentid=@studentid";

              SqlParameter p1 = new SqlParameter("@img", SqlDbType.Image);

              FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
              byte[] bytes = new byte[fs.Length];
              fs.Seek(0, SeekOrigin.Begin);  //定义开始读文件的位置，为0位置  SeekOrigin.Begin意为设置0为一个开始标志位
              fs.Read(bytes, 0, (int)fs.Length);
              p1.Value = bytes;

              SqlParameter p2 = new SqlParameter("@studentid", gl_studentid);

              using (SqlConnection conn = new SqlConnection(sqlconn))
              {
                  conn.Open();
                  SqlCommand cmd = new SqlCommand();
                  cmd.Connection = conn;
                  cmd.CommandText = sql;
                  cmd.Parameters.Add(p1);
                  cmd.Parameters.Add(p2);

                  cmd.ExecuteNonQuery();

                  fs.Close();
                  fs.Dispose();

                  MessageBox.Show("图片修改成功！");

                  this.LoadPicture();

              }
          }*/
        //    private void button2_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog ofd = new OpenFileDialog();
        //    //为对话框设置标题
        //    ofd.Title = "请选择上传的图片";
        //    //设置筛选的图片格式
        //    ofd.Filter = "图片格式|*.jpg";
        //    //设置是否允许多选
        //    ofd.Multiselect = false;
        //    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        //获得文件的完整路径（包括名字后后缀）
        //        string filePath = ofd.FileName;
        //        //将文件路径显示在文本框中
        //        label2.Text = filePath;
        //        //找到文件名比如“1.jpg”前面的那个“\”的位置
        //        int position = filePath.LastIndexOf("\\");
        //        //从完整路径中截取出来文件名“1.jpg”
        //        string fileName = filePath.Substring(position + 1);
        //        fileName = DateTime.Now.ToString("yyyyMMdd24hhmmss") + ".jpg";
        //        //读取选择的文件，返回一个流
        //        using (Stream stream = ofd.OpenFile())
        //        {
        //            //创建一个流，用来写入得到的文件流（注意：创建一个名为“Images”的文件夹，如果是用相对路径，必须在这个程序的Degug目录下创建
        //            //如果是绝对路径，放在那里都行，我用的是相对路径）
        //            using (FileStream fs = new FileStream("D:/Images/" + fileName, FileMode.CreateNew))
        //            {
        //                //将得到的文件流复制到写入流中
        //                stream.C.CopyTo(fs);
        //                //将写入流中的数据写入到文件中
        //                fs.Flush();
        //            }
        //            //PictrueBOx 显示该图片，此时这个图片已经被复制了一份在Images文件夹下，就相当于上传
        //            //至于上传到别的地方你再更改思路就行，这里只是演示过程
        //            pictureBox1.ImageLocation = "D:/Images/" + fileName;
        //            label2.Text = pictureBox1.ImageLocation;
        //            // pbShow.ImageLocation = @"./Images/" + fileName;
        //        }


        //    }

        //}

        private void button5_Click(object sender, EventArgs e)
        {
            //button3_Click(sender, e);
            label1.Text = "";
            txt_xm.Text = "";
            cbo_xb.SelectedIndex = 0;
            txt_nl.Text = "";
            txt_sfzhm.Text = "";
            //textBox4.Text = "";
            txt_lxdh.Text = "";
            pictureBox1.Image = null;
            str_picBase64_1 = "";
            str_picBase64_2 = "";
            ifsfz = 0;
            FrmVideo.bidui = false;
            cbo_hylbdl.SelectedIndex = 0;
            txt_gy.Text = "/";
            txt_lj.Text = "/";
            txt_sh.Text = "/";
            txt_fjh.Text = "/";
            txt_pfb.Text = "/";
            txt_qt.Text = "/";
            str_picBase64_1 = "";
            str_picBase64_2 = "";
            FrmVideo.str_picBase64_1 = "";
            FrmVideo.str_picBase64_2 = "";
            //button6.Enabled = false;
            pictureBox8.Enabled = false;
            txt_xm.Enabled = true;
            cbo_xb.Enabled = true;
            txt_nl.Enabled = true;
            txt_sfzhm.Enabled = true;
            pictureBox3.Enabled = true;
            pictureBox3.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            txt_xm.Text = "";
            cbo_xb.SelectedIndex = 0;
            txt_nl.Text = "";
            txt_sfzhm.Text = "";
            //textBox4.Text = "";
            txt_lxdh.Text = "";
            pictureBox1.Image = null;
            str_picBase64_1 = "";
            str_picBase64_2 = "";
            ifsfz = 0;
            FrmVideo.bidui = false;
            cbo_hylbdl.SelectedIndex = 0;
            txt_gy.Text = "/";
            txt_lj.Text = "/";
            txt_sh.Text = "/";
            txt_fjh.Text = "/";
            txt_pfb.Text = "/";
            txt_qt.Text = "/";
             str_picBase64_1 = "";
             str_picBase64_2 = "";
            FrmVideo.str_picBase64_1 = "";
            FrmVideo.str_picBase64_2 = "";
            txt_gzdw.Text = "";
            //button6.Enabled = false;
            pictureBox8.Enabled = false;
            txt_xm.Enabled = true;
            cbo_xb.Enabled = true;
            txt_nl.Enabled = true;
            txt_sfzhm.Enabled = true;
            pictureBox3.Enabled = true;
            pictureBox3.Visible = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmTjjglr f = new FrmTjjglr();
            f.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmJcjl f = new FrmJcjl();
            f.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (DbConn.sfptqrmyy == "0")
            {
                FrmVideo1 f = new FrmVideo1();
                f.ShowDialog();

                pictureBox1.Image = FrmVideo1.bitmap;
                if (File.Exists(@".\zp.bmp"))
                {
                    File.Delete(@".\zp.bmp");
                }
                if (FrmVideo1.bitmap != null)
                {
                    FrmVideo1.bitmap.Save(@".\zp.bmp");
                }
            }
            else { 
            openFileDialog1.Title = "图片文件";
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "图片文件(*.BMP; *.JPG; *.JEPG)|*.jpg;*jpeg;*bmp";//| *.BMP; *.JPG; *.GIF | All files(*.*) | *.*
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog1.FileName);
                    

                        //PictureBox控件显示图片
                        pictureBox1.ImageLocation = openFileDialog1.FileName;
                        //绝对路径
                        string image = openFileDialog1.FileName;
                        //  是指XXX.jpg
                        string picpath = openFileDialog1.SafeFileName;
                        if (File.Exists(@".\zp.bmp"))
                        {
                            File.Delete(@".\zp.bmp");
                        }
                        File.Copy(openFileDialog1.FileName, @".\zp.bmp");
                    
                }

            }


        }

        private void button6_Click_1(object sender, EventArgs e)
        {

            FrmSelectDY2 f = new FrmSelectDY2();
            f.ShowDialog();
            if (FrmSelectDY2.i_flag == 0)
            { return; }
            else if (FrmSelectDY2.i_flag == 1)
            {
                PaperSize pkCustomSize = new PaperSize("First Custom Size", 827, 1170);
                PDoc.DefaultPageSettings.PaperSize = pkCustomSize;
                ((System.Windows.Forms.Form)Pvd).StartPosition = FormStartPosition.CenterScreen;
                ((System.Windows.Forms.Form)Pvd).Width = 827;
                ((System.Windows.Forms.Form)Pvd).Height = 1170;
                ((System.Windows.Forms.Form)Pvd).Icon = this.Icon;
                Pvd.Document = PDoc;
                Pvd.ShowDialog();
               
            }
            else if (FrmSelectDY2.i_flag == 2)
            {

                PaperSize pkCustomSize = new PaperSize("First Custom Size", 827, 1170);
                PDoc2.DefaultPageSettings.PaperSize = pkCustomSize;
                ((System.Windows.Forms.Form)Pvd).StartPosition = FormStartPosition.CenterScreen;
                ((System.Windows.Forms.Form)Pvd).Width = 827;
                ((System.Windows.Forms.Form)Pvd).Height = 1170;
                ((System.Windows.Forms.Form)Pvd).Icon = this.Icon;
                Pvd.Document = PDoc2;
                Pvd.ShowDialog();
            }
           
           // PDoc.Print();
        }

        private void PDoc_PrintPage(object sender, PrintPageEventArgs e)
        {

            Font objFont = new Font("方正小标宋简体", 22, FontStyle.Regular);
            Font objFont2 = new Font("宋体", 12, FontStyle.Regular);
            Font objFont3 = new Font("宋体", 9, FontStyle.Regular);
            Brush objBrush = Brushes.Black;
            Pen objPen = new Pen(objBrush);
            objPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Pen objPen1 = new Pen(objBrush);
            objPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            Psd.PageSettings = new System.Drawing.Printing.PageSettings();
            Psd.PageSettings.Margins.Left = 40;
            Psd.PageSettings.Margins.Top = 100;
            //Psd.PageSettings.Margins.Right = 20;
            //Psd.PageSettings.Margins.Bottom = 10;

            int nLeft = 94;
            int nTop = 295;
            int nline = 30;
            
            #region 画线框
            e.Graphics.DrawLine(objPen, nLeft, nTop, nLeft+650, nTop);//上横
            e.Graphics.DrawLine(objPen, nLeft, nTop + 2 * nline, nLeft+650, nTop + 2 * nline);//第一行横
            e.Graphics.DrawLine(objPen, nLeft + 60, nTop + nline, nLeft + 650, nTop + nline);//第一行横中分横
            e.Graphics.DrawLine(objPen, nLeft, nTop, nLeft, nTop + 19 * nline);//左竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(1.4), nTop, nLeft + getyc(1.4), nTop + 2 * nline);//第一行第一竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(3.7), nTop, nLeft + getyc(3.7), nTop + 2 * nline);//第一行第二竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(5.6), nTop, nLeft + getyc(5.6), nTop + 2 * nline);//第一行第三竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(7.5), nTop, nLeft + getyc(7.5), nTop + 2 * nline);//第一行第四竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(10.2), nTop, nLeft + getyc(10.2), nTop + 4 * nline);//第一行第五竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(12.2), nTop, nLeft + getyc(12.2), nTop + 2 * nline);//第一行第六竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(14.2), nTop, nLeft + getyc(14.2), nTop + 2 * nline);//第一行第七竖
            e.Graphics.DrawLine(objPen, nLeft + 650, nTop, nLeft + 650, nTop + 19 * nline);//右竖
            e.Graphics.DrawLine(objPen, nLeft + 55, nTop + 2 * nline, nLeft + 55, nTop + 6 * nline);//体征右边的竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(8.4), nTop + 2 * nline, nLeft + getyc(8.4), nTop + 4 * nline);//肝左边的竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(10.2), nTop + 2 * nline, nLeft + getyc(10.2), nTop + 4 * nline);//肝右边的竖
            e.Graphics.DrawLine(objPen, nLeft + 60, nTop + 3 * nline, nLeft + 650, nTop + 3 * nline);//肝下面的横
            e.Graphics.DrawLine(objPen, nLeft + 60, nTop + 4 * nline, nLeft + 650, nTop + 4 * nline);//脾下面的横
            e.Graphics.DrawLine(objPen, nLeft + 60, nTop + 5 * nline, nLeft + 650, nTop + 5 * nline);//皮肤下面的横
            e.Graphics.DrawLine(objPen, nLeft, nTop + 6 * nline, nLeft + 650, nTop + 6 * nline);//其他下面的横
            e.Graphics.DrawLine(objPen, nLeft + getyc(8.4), nTop + 5 * nline, nLeft + getyc(8.4), nTop + 6 * nline);//签名医师左边竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(13.2), nTop + 5 * nline, nLeft + getyc(13.2), nTop + 6 * nline);//签名医师右边竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(2.7), nTop + 2 * nline, nLeft + getyc(2.7), nTop + 8 * nline);//心右边竖
            e.Graphics.DrawLine(objPen, nLeft, nTop + 8* nline, nLeft + 650, nTop + 8 * nline);//胸片下面的横
            e.Graphics.DrawLine(objPen, nLeft + getyc(1.7), nTop + 8 * nline, nLeft + getyc(1.7), nTop + 16 * nline);//生化右边的竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(1.7), nTop + 9 * nline, nLeft +650, nTop + 9 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(3), nTop + 10 * nline, nLeft + 650, nTop + 10 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(1.7), nTop + 11 * nline, nLeft + 650, nTop + 11 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(3), nTop + 12 * nline, nLeft +650, nTop + 12 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(3), nTop + 13 * nline, nLeft + 650, nTop + 13 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(1.7), nTop + 14 * nline, nLeft + 650, nTop + 14 * nline);//
            e.Graphics.DrawLine(objPen, nLeft, nTop + 16 * nline, nLeft + 650, nTop + 16 * nline);//
            e.Graphics.DrawLine(objPen, nLeft, nTop + 19 * nline, nLeft + 650, nTop + 19 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(3), nTop + 9 * nline, nLeft + getyc(3), nTop + 14 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(8.4), nTop + 8 * nline, nLeft + getyc(8.4), nTop + 14 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(13.2), nTop + 8 * nline, nLeft + getyc(13.2), nTop + 14 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(7.5), nTop + 16 * nline, nLeft + getyc(7.5), nTop + 19 * nline);//
            e.Graphics.DrawLine(objPen1, nLeft, nTop + getyc(16), nLeft +650, nTop + getyc(16));//
            #endregion
            #region 写字
            if (DbConn.Sfxwzyy == "1")
            {
                e.Graphics.DrawString("广东省食品从业人员健康检查表", objFont, Brushes.Black, getyc(5.2), getyc(3.0));
                e.Graphics.DrawImage(pictureBox1.Image, nLeft + 5, getyc(3.0), 100, 120);

            }
            else
            {
                e.Graphics.DrawString("广东省食品从业人员健康检查表", objFont, Brushes.Black, getyc(5), getyc(4.5));

            }
            e.Graphics.DrawString("检查日期："+dtp_djrq.Value.ToString("  yyyy年  MM月  dd日")+"    单位："+txt_gzdw.Text, objFont2, Brushes.Black, nLeft+5, 250);
            e.Graphics.DrawString("姓名："+txt_xm.Text+"       性别："+cbo_xb.SelectedItem.ToString()+"        年龄："+txt_nl.Text+"岁" + "        联系电话：" + txt_lxdh.Text, objFont2, Brushes.Black, nLeft+5, 275);
            e.Graphics.DrawString("既往\n病史", objFont2, Brushes.Black, nLeft + 12, nTop + 12);
            e.Graphics.DrawString("病   名", objFont2, Brushes.Black, nLeft + 72, nTop + 10);
            e.Graphics.DrawString("肝炎", objFont2, Brushes.Black, nLeft + 160, nTop + 10);
            e.Graphics.DrawString("痢疾", objFont2, Brushes.Black, nLeft + 238, nTop + 10);
            e.Graphics.DrawString("伤寒", objFont2, Brushes.Black, nLeft + 330, nTop + 10);
            e.Graphics.DrawString("肺结核", objFont2, Brushes.Black, nLeft + 420, nTop + 10);
            e.Graphics.DrawString("皮肤病", objFont2, Brushes.Black, nLeft + 488, nTop + 10);
            e.Graphics.DrawString("其他", objFont2, Brushes.Black, nLeft + 585, nTop + 10);
            e.Graphics.DrawString("患病时间", objFont2, Brushes.Black, nLeft + 68, nTop + 40);
            e.Graphics.DrawString("体\n\n\n征", objFont2, Brushes.Black, nLeft + 20, nTop + 85);
            e.Graphics.DrawString("心", objFont2, Brushes.Black, nLeft + 73, nTop + 67);
            e.Graphics.DrawString("脾", objFont2, Brushes.Black, nLeft + 73, nTop + 100);
            e.Graphics.DrawString("皮肤", objFont2, Brushes.Black, nLeft + 65, nTop + 125);
            e.Graphics.DrawString("其他", objFont2, Brushes.Black, nLeft + 65, nTop + 155);
            e.Graphics.DrawString("肝", objFont2, Brushes.Black, nLeft + 355, nTop + 67);
            e.Graphics.DrawString("肺", objFont2, Brushes.Black, nLeft + 355, nTop + 100);
            e.Graphics.DrawString("渗出性皮肤病", objFont2, Brushes.Black, nLeft + 110, nTop + 125);
            e.Graphics.DrawString("化脓性皮肤病", objFont2, Brushes.Black, nLeft + 250, nTop + 125);
            e.Graphics.DrawString("其他", objFont2, Brushes.Black, nLeft + 400, nTop + 125);
            e.Graphics.DrawString("医 师 签 名", objFont2, Brushes.Black, nLeft + 400, nTop + 155);
            e.Graphics.DrawString("X线胸透或", objFont2, Brushes.Black, nLeft + 12, nTop + 195);
            e.Graphics.DrawString("胸部拍片", objFont2, Brushes.Black, nLeft + 14, nTop + 215);
            e.Graphics.DrawString("医师签名：", objFont2, Brushes.Black, nLeft + 420, nTop + 220);
            e.Graphics.DrawString("检查项目", objFont2, Brushes.Black, nLeft + 160, nTop + 250);
            e.Graphics.DrawString("检查结果", objFont2, Brushes.Black, nLeft + 390, nTop + 250);
            e.Graphics.DrawString("检查师签名", objFont2, Brushes.Black, nLeft + 545, nTop + 250);
            e.Graphics.DrawString("大便\n培养", objFont2, Brushes.Black, nLeft + 74, nTop + 285);
            e.Graphics.DrawString("肝\n功\n能", objFont2, Brushes.Black, nLeft + 82, nTop + 352);
            e.Graphics.DrawString("细菌性和阿米巴性痢病杆菌", objFont2, Brushes.Black, nLeft + 120, nTop + 280);
            e.Graphics.DrawString("伤寒或副伤寒", objFont2, Brushes.Black, nLeft + 170, nTop + 307);
            e.Graphics.DrawString("谷丙转氨酶", objFont2, Brushes.Black, nLeft + 180, nTop + 338);
            e.Graphics.DrawString("HAV-IgM※", objFont2, Brushes.Black, nLeft + 190, nTop + 367);
            e.Graphics.DrawString("HEV-IgM※", objFont2, Brushes.Black, nLeft + 190, nTop + 397);
            e.Graphics.DrawString("其他", objFont2, Brushes.Black, nLeft + 80, nTop + 427);
            e.Graphics.DrawString("实化\n验验\n室单\n检附\n查后", objFont2, Brushes.Black, nLeft + 12, nTop + 310);
            e.Graphics.DrawString("检 查 结 论：", objFont2, Brushes.Black, nLeft + 9, nTop + 485);
            e.Graphics.DrawString("主检医师签名：", objFont2, Brushes.Black, nLeft + 80, nTop + 550);
            e.Graphics.DrawString("体检机构意见：", objFont2, Brushes.Black, nLeft + 300, nTop + 485);
            e.Graphics.DrawString("（公章）    年   月   日", objFont2, Brushes.Black, nLeft + 440, nTop + 550);
            draw(nLeft + getyc(5.5), nTop + getyc(3.2), 15, objPen, e);
            draw(nLeft + getyc(9.05), nTop + getyc(3.2), 15, objPen, e);
            draw(nLeft + getyc(11.2), nTop + getyc(3.2), 15, objPen, e);
            e.Graphics.DrawString("此表用于广东省食品生产经营从业人员的健康检查", objFont3, Brushes.Black, nLeft + 10, nTop + 575);
            e.Graphics.DrawString("※说明：发现谷丙转氨酶异常的，方可做HAV-IgM、HEV-IgM2个指标", objFont3, Brushes.Black, nLeft + 10, nTop + 590);
            e.Graphics.DrawString("编号："+label1.Text, objFont3, Brushes.Black, nLeft + 10, nTop + 640);

            e.Graphics.DrawString("广东省食品从业人员健康检查回执", objFont2, Brushes.Black, nLeft + 138, nTop + 670);
            e.Graphics.DrawString("检查日期："+dtp_djrq.Value.ToString("        yyyy年    MM月    dd日"), objFont3, Brushes.Black, nLeft + 10, nTop + 700);
            e.Graphics.DrawString("姓名："+txt_xm.Text+"      性别："+cbo_xb.SelectedItem.ToString()+"        单位："+txt_gzdw.Text, objFont3, Brushes.Black, nLeft + 10, nTop + 720);
            e.Graphics.DrawString("姓名、单位涂改无效，发表之日起限30天内体检有效，超期此表作废。", objFont3, Brushes.Black, nLeft + 10, nTop + 740);
            e.Graphics.DrawString(txt_gy.Text, objFont2, Brushes.Black, nLeft + 150, nTop + 40);
            e.Graphics.DrawString(txt_lj.Text, objFont2, Brushes.Black, nLeft + 225, nTop + 40);
            e.Graphics.DrawString(txt_sh.Text, objFont2, Brushes.Black, nLeft + 315, nTop + 40);
            e.Graphics.DrawString(txt_fjh.Text, objFont2, Brushes.Black, nLeft + 410, nTop + 40);
            e.Graphics.DrawString(txt_pfb.Text, objFont2, Brushes.Black, nLeft + 490, nTop + 40);
            e.Graphics.DrawString(txt_qt.Text, objFont2, Brushes.Black, nLeft + 575, nTop + 40);
           
            #endregion

        }

        public void draw(int x, int y, int length, Pen objPen1, PrintPageEventArgs e)
        {
            e.Graphics.DrawLine(objPen1, x, y, x, y + length);//
            e.Graphics.DrawLine(objPen1, x, y, x + length, y);//
            e.Graphics.DrawLine(objPen1, x + length, y, x + length, y + length);//
            e.Graphics.DrawLine(objPen1, x, y + length, x + length, y + length);//
        }
        public int getyc(double d)
        {
            double b = Convert.ToDouble(d / 2.54);
            int a = (int)Math.Round(b * 100, 0);
            return a;
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            button8_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //txt_xm.Text = "林杭";
            //ClassDLL.CoporationReg();
            //cbo_xb.SelectedIndex = 1;
            //txt_sfzhm.Text = "440825198710090039";
            //txt_nl.Text = getAge(txt_sfzhm.Text);
            //pictureBox1.ImageLocation = @"D:\Images\2017121424042926.jpg";
            //ClsBase64 c = new ClsBase64();
            //MessageBox.Show(c.ImgToBase64String(@"D:\Images\2017121424042926.jpg"));
            //string str= c.ImgToBase64String(@"D:\Images\2017121424042926.jpg");
            // MessageBox.Show(str);
            try
            {
                if ((iRetCOM == 1) || (iRetUSB == 1))
                {

                    int authenticate = CVRSDK.CVR_Authenticate();
                    if (authenticate == 1)
                    {
                        int readContent = CVRSDK.CVR_Read_Content(4);
                        if (readContent == 1)
                        {
                            //this.label10.Text = "读卡操作成功！";
                            FillData();
                            if (txt_sfzhm.Text != "")
                            {
                                getDetail(txt_sfzhm.Text);
                            }
                            ifsfz = 1;
                            pictureBox3.Visible = false;
                            txt_xm.Enabled = false;
                            cbo_xb.Enabled = false;
                            txt_nl.Enabled = false;
                            txt_sfzhm.Enabled = false;
                        }
                        else
                        {
                            //this.label10.Text = "读卡操作失败！";
                            MessageBox.Show("读卡操作失败！");

                            pictureBox3.Visible = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("未放卡或卡片放置不正确");
                    }
                }
                else
                {
                    MessageBox.Show("身份证读卡器连接失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// 验证身份证合理性  
        /// </summary>  
        /// <param name="Id"></param>  
        /// <returns></returns>  
        public static bool CheckIDCard(string idNumber)
        {
            if (idNumber.Length == 18)
            {
                bool check = CheckIDCard18(idNumber);
                return check;
            }
            else if (idNumber.Length == 15)
            {
                bool check = CheckIDCard15(idNumber);
                return check;
            }
            else if (idNumber.Length != 15&& idNumber.Length != 18)
            {
                DialogResult dir = MessageBox.Show("您输入的身份证号码不规范，是否保存？","警告",MessageBoxButtons.YesNo);
                if (dir == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        private static bool CheckIDCard18(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber.Remove(17), out n) == false
                || n < Math.Pow(10, 16) || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = idNumber.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != idNumber.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }
            return true;//符合GB11643-1999标准  
        }


        /// <summary>  
        /// 16位身份证号码验证  
        /// </summary>  
        private static bool CheckIDCard15(string idNumber)
        {
            long n = 0;
            if (long.TryParse(idNumber, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(idNumber.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = idNumber.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            return true;
        }

        public string getAge(string str)
        {
            try
            {
                string birthday = "";
                if (str.Length != 18)
                { return "0"; }
                birthday = str.Substring(6, 4) + "-" + str.Substring(10, 2) + "-" + str.Substring(12, 2);
                if (int.Parse(str.Substring(6, 4)) < 1900 || int.Parse(str.Substring(6, 4)) > 2017)
                { return "0"; }

                int year_1 = DateTime.Parse(birthday).Year;
                int year_2 = DateTime.Now.Year;
                if (year_1 >= year_2)
                {
                    return "0";
                }
                return (year_2 - year_1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法自动计算年龄，请手动修改！");
                return "0";
            }
        }
        /// <summary>  

        public void getDetail(string str)
        {
            strSql = "select top 1 lxdh,gzdw,hylbdl,"
                +"table_tjjg.ganyan,table_tjjg.liji,table_tjjg.shanghan,table_tjjg.feijiehe,table_tjjg.pifubing,table_tjjg.qita "
                +" from table_renyuan,table_tjjg "
                +" where table_renyuan.tjbh = table_tjjg.tjbh "
                +" and sfzhm = '"+str+"'"
                + " order by table_renyuan.tjbh desc";
            SqlDataReader sqlDataReader = dbConn.GetDataReader(strSql);
            if (sqlDataReader.Read())
            {
                txt_lxdh.Text = sqlDataReader.GetValue(0).ToString();
                txt_gzdw.Text = sqlDataReader.GetValue(1).ToString();
                //if (sqlDataReader.GetValue(2).ToString() == "Z")
                //{ cbo_hylbdl.SelectedIndex = cbo_hylbdl.Items.Count - 1; }
                //else
                //{
                //    cbo_hylbdl.SelectedIndex = int.Parse(sqlDataReader.GetValue(2).ToString()) - 1;
                //}
                cbo_hylbdl.SelectedItem = sqlDataReader.GetValue(2).ToString();
                txt_gy.Text = sqlDataReader.GetValue(3).ToString();
                txt_lj.Text = sqlDataReader.GetValue(4).ToString();
                txt_sh.Text = sqlDataReader.GetValue(5).ToString();
                txt_fjh.Text = sqlDataReader.GetValue(6).ToString();
                txt_pfb.Text = sqlDataReader.GetValue(7).ToString();
                txt_qt.Text = sqlDataReader.GetValue(8).ToString();
                label1.Text = "";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void cbo_mz_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void cbo_hylbxl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("nulllll");
            }
            else
            {
                MessageBox.Show(pictureBox1.Image.ToString());
            }
        }

      

        private void txt_nl_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txt_nl_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_sfzhm_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8&&e.KeyChar!='X')
            //{
            //    e.Handled = true;
            //}
        }

        private void txt_lxdh_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void button2_Click_3(object sender, EventArgs e)
        {
        }

        private void txt_sfzhm_TabIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click_4(object sender, EventArgs e)
        {
            try
            {
                frmEdit f = new frmEdit();
                f.ShowDialog();
                if (frmEdit.arrayList.Count == 0)
                {
                    MessageBox.Show("查不到该体检人员信息！");
                    return;
                }
                else
                {
                    arrayList = frmEdit.arrayList;
                    label1.Text = arrayList[0].ToString();
                    txt_xm.Text = arrayList[1].ToString();
                    cbo_xb.SelectedItem = arrayList[2].ToString();
                    txt_nl.Text = arrayList[3].ToString();
                    txt_sfzhm.Text = arrayList[4].ToString();
                    txt_lxdh.Text = arrayList[5].ToString();
                    txt_gzdw.Text = arrayList[6].ToString();
                    cbo_hylbdl.SelectedItem = arrayList[7].ToString();
                    pictureBox1.Image = clsBase64.Base64StringToImage(arrayList[8].ToString());
                    txt_gy.Text = arrayList[9].ToString();
                    txt_lj.Text = arrayList[10].ToString();
                    txt_sh.Text = arrayList[11].ToString();
                    txt_fjh.Text = arrayList[12].ToString();
                    txt_pfb.Text = arrayList[13].ToString();
                    txt_qt.Text = arrayList[14].ToString();
                    dtp_djrq.Value = DateTime.Parse(arrayList[16].ToString());
                    if (arrayList[15].ToString() == "1")
                    {
                        pictureBox3.Visible = false;
                        txt_xm.Enabled = false;
                        cbo_xb.Enabled = false;
                        txt_nl.Enabled = false;
                        txt_sfzhm.Enabled = false;
                    }
                    else
                    {
                        pictureBox3.Visible = true;
                        txt_xm.Enabled = true;
                        cbo_xb.Enabled = true;
                        txt_nl.Enabled = true;
                        txt_sfzhm.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            button2_Click_4(sender,e);
        }

        private void PDoc2_PrintPage(object sender, PrintPageEventArgs e)
        {
            //strSql = "select ";
            Font objFont = new Font("方正小标宋简体", 22, FontStyle.Regular);
            Font objFont2 = new Font("宋体", 16, FontStyle.Regular);
            Font objFont3 = new Font("宋体", 13, FontStyle.Regular);
            Font objFont4 = new Font("宋体", 20, FontStyle.Bold|FontStyle.Underline);
            Brush objBrush = Brushes.Black;
            Pen objPen = new Pen(objBrush);
            objPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Pen objPen1 = new Pen(objBrush);
            objPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            Psd.PageSettings = new System.Drawing.Printing.PageSettings();
            Psd.PageSettings.Margins.Left = 40;
            Psd.PageSettings.Margins.Top = 100;
            int i_x = 0;
            double i_y =-0.3;
            if (DbConn.Sfxwzyy == "1")
            {
                #region 肝功能字

                //e.Graphics.DrawString("广东省食品从业人员体检\n      检验科申请单", objFont2, Brushes.Black, getyc(2), getyc(1+i_y));
                DrawString("徐闻县中医院食品从业人员体检\n检验科检查申请单", objFont2,new Rectangle(0, getyc(1 + i_y),getyc(10.5), getyc(1.5)),e);
                e.Graphics.DrawString("体检编号：" + label1.Text, objFont3, Brushes.Black, getyc(0.4), getyc(3 + i_y));
                //e.Graphics.DrawString("工作单位："+txt_gzdw.Text, objFont3, Brushes.Black, getyc(0.4), getyc(4.5));
                e.Graphics.DrawString("姓名：" + txt_xm.Text, objFont3, Brushes.Black, getyc(0.4), getyc(4.2 + i_y));
                e.Graphics.DrawString("性别：" + cbo_xb.SelectedItem.ToString(), objFont3, Brushes.Black, getyc(0.4), getyc(5.4 + i_y));
                e.Graphics.DrawString("年龄：" + txt_nl.Text + "岁", objFont3, Brushes.Black, getyc(6), getyc(5.4 + i_y));
                e.Graphics.DrawString("检查项目：" + "谷丙转氨酶、HAV-IgM*、HEV-IgM*", objFont3, Brushes.Black, getyc(0.4), getyc(6.6 + i_y));
                
                e.Graphics.DrawString("项目价格：" + "71.00元", objFont3, Brushes.Black, getyc(0.4 ), getyc(8.5 + i_y));
                e.Graphics.DrawString("申请负责人：" + FrmLogin.str_yhxm, objFont3, Brushes.Black, getyc(0.4), getyc(10.2 + i_y));
                e.Graphics.DrawString("申请日期：" + dtp_djrq.Value.ToString("yyyy年MM月dd日"), objFont3, Brushes.Black, getyc(3.5), getyc(12.5 + i_y));
                e.Graphics.DrawString("盖章", objFont3, Brushes.Black, getyc(6), getyc(11.5 + i_y));
                e.Graphics.DrawString("请凭本单据到：\n[" + clsPublic.getAdress("肝功能") + "]进行检查", objFont3, Brushes.Black, getyc(0.4), getyc(13.5 + i_y));
                #endregion
                #region 大便培养字
                double d_add = 10.5;

                // e.Graphics.DrawString("广东省食品从业人员体检\n      检查科申请单", objFont2, Brushes.Black, getyc(2 + d_add), getyc(1+i_y));
                DrawString("徐闻县中医院食品从业人员体检\n检验科检查申请单", objFont2, new Rectangle(getyc(10.5), getyc(1 + i_y), getyc(10.5), getyc(1.5)), e);

                e.Graphics.DrawString("体检编号：" + label1.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(3 + i_y));
                //e.Graphics.DrawString("工作单位：" + txt_gzdw.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(4.5));
                e.Graphics.DrawString("姓名：" + txt_xm.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(4.2 + i_y));
                e.Graphics.DrawString("性别：" + cbo_xb.SelectedItem.ToString(), objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(5.4 + i_y));
                e.Graphics.DrawString("年龄：" + txt_nl.Text + "岁", objFont3, Brushes.Black, getyc(6 + d_add), getyc(5.4 + i_y));
                e.Graphics.DrawString("检查项目：" + "伤寒或副伤寒、痢疾杆菌", objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(6.6 + i_y));

                e.Graphics.DrawString("项目价格：" + "60.00元", objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(8.5  + i_y));
                e.Graphics.DrawString("申请负责人：" + FrmLogin.str_yhxm, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(10.2 + i_y));
                e.Graphics.DrawString("申请日期：" + dtp_djrq.Value.ToString("yyyy年MM月dd日"), objFont3, Brushes.Black, getyc(3.5 + d_add), getyc(12.5 + i_y));
                e.Graphics.DrawString("盖章", objFont3, Brushes.Black, getyc(6 + d_add), getyc(11.5 + i_y));
                e.Graphics.DrawString("请凭本单据到：\n[" + clsPublic.getAdress("大便培养") + "]进行检查", objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(13.5 + i_y));
                #endregion

                double d_add2 = 14.8;

                #region 收费单

                DrawString("徐闻县中医院\n体检中心收费单", objFont2, new Rectangle(0, getyc(14.8+1+i_y), getyc(10.5), getyc(1.5)), e);

                e.Graphics.DrawString("体检编号：" + label1.Text, objFont3, Brushes.Black, getyc(0.8), getyc(3 + d_add2 + i_y));
                e.Graphics.DrawString("日    期：" + dtp_djrq.Value.ToString("yyyy年MM月dd日"), objFont3, Brushes.Black, getyc(0.8), getyc(4.2 + d_add2 + i_y));
                e.Graphics.DrawString("姓    名：" + txt_xm.Text, objFont3, Brushes.Black, getyc(0.8), getyc(5.5 + d_add2 + i_y));
                e.Graphics.DrawString("性    别：" + cbo_xb.SelectedItem.ToString() , objFont3, Brushes.Black, getyc(0.8), getyc(6.8 + d_add2 + i_y));
                e.Graphics.DrawString("年    龄：" + txt_nl.Text + "岁", objFont3, Brushes.Black, getyc(0.8), getyc(8.1 + d_add2 + i_y));
                e.Graphics.DrawString("体 检 费：", objFont3, Brushes.Black, getyc(0.8), getyc(9.4 + d_add2 + i_y));

                e.Graphics.DrawString(DbConn.tjf+"元", objFont4, Brushes.Black, getyc(3), getyc(9.2 + d_add2 + i_y));
                //e.Graphics.DrawString("姓名：" + txt_xm.Text, objFont3, Brushes.Black, getyc(0.8), getyc(4.5 + d_add2 + i_y));
                //e.Graphics.DrawString("性别：" + cbo_xb.SelectedItem.ToString(), objFont3, Brushes.Black, getyc(0.4), getyc(6 + d_add2 + i_y));
                //e.Graphics.DrawString("年龄：" + txt_nl.Text + "岁", objFont3, Brushes.Black, getyc(6), getyc(6 + d_add2 + i_y));
                //e.Graphics.DrawString("体检费：150.00元", objFont3, Brushes.Black, getyc(0.4), getyc(8 + d_add2 + i_y));
                e.Graphics.DrawString("请凭本单据到：\n[" + clsPublic.getAdress("体征") + "]缴费", objFont3, Brushes.Black, getyc(0.4), getyc(11.5 + d_add2 + i_y));
                //e.Graphics.DrawLine(objPen, getyc(0.8), getyc(4.2 + d_add2), getyc(9.2), getyc(4.2 + d_add2));
                //e.Graphics.DrawLine(objPen, getyc(0.8), getyc(5.4 + d_add2), getyc(9.2), getyc(5.4 + d_add2));
                //e.Graphics.DrawLine(objPen, getyc(0.8), getyc(6.6 + d_add2), getyc(9.2), getyc(6.6 + d_add2));
                //e.Graphics.DrawLine(objPen, getyc(0.8), getyc(7.2 + d_add2), getyc(9.2), getyc(7.2 + d_add2));
                //e.Graphics.DrawLine(objPen, getyc(0.8), getyc(4.2 + d_add2), getyc(0.8), getyc(7.2 + d_add2));
                //e.Graphics.DrawLine(objPen, getyc(9.2), getyc(4.2 + d_add2), getyc(9.2), getyc(7.2 + d_add2));


                #endregion

                #region 胸片字


                DrawString("徐闻县中医院食品从业人员体检\n放射科检查申请单", objFont2, new Rectangle(getyc(10.5), getyc(14.8+1+i_y), getyc(10.5), getyc(1.5)), e);

                e.Graphics.DrawString("体检编号：" + label1.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(3 + d_add2 + i_y));
                //e.Graphics.DrawString("工作单位：" + txt_gzdw.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(4.5 + d_add2));
                e.Graphics.DrawString("姓名：" + txt_xm.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(4.2 + d_add2 + i_y));
                e.Graphics.DrawString("性别：" + cbo_xb.SelectedItem.ToString(), objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(5.4 + d_add2 + i_y));
                e.Graphics.DrawString("年龄：" + txt_nl.Text + "岁", objFont3, Brushes.Black, getyc(6 + d_add), getyc(5.4 + d_add2 + i_y));
                e.Graphics.DrawString("检查项目：" + "胸部正位片", objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(6.6 + d_add2 + i_y));
                e.Graphics.DrawString("项目价格：" + "54.00元", objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(8.5 + d_add2 + i_y));
                e.Graphics.DrawString("申请负责人：" + FrmLogin.str_yhxm, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(10.2 + d_add2 + i_y));
                e.Graphics.DrawString("申请日期：" + dtp_djrq.Value.ToString("yyyy年MM月dd日"), objFont3, Brushes.Black, getyc(3.5 + d_add), getyc(12.5 + d_add2 + i_y));
                e.Graphics.DrawString("盖章", objFont3, Brushes.Black, getyc(6 + d_add), getyc(11.5 + d_add2 + i_y));
                e.Graphics.DrawString("请凭本单据到：\n[" + clsPublic.getAdress("胸片") + "]进行检查", objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(13.5 + d_add2 + i_y));
                #endregion
                #region 划线
                e.Graphics.DrawLine(objPen1, getyc(21.2 / 2), 0, getyc(21.2 / 2), getyc(29.6));
                e.Graphics.DrawLine(objPen1, getyc(0), getyc(29.6 / 2), getyc(21), getyc(29.6 / 2));
                #endregion

            }
            else
            {
                #region 肝功能字

                e.Graphics.DrawString("广东省食品从业人员体检\n      检验科申请单", objFont2, Brushes.Black, getyc(2), getyc(1 + i_y));

                e.Graphics.DrawString("体检编号：" + label1.Text, objFont3, Brushes.Black, getyc(0.4), getyc(3 + i_y));
                //e.Graphics.DrawString("工作单位："+txt_gzdw.Text, objFont3, Brushes.Black, getyc(0.4), getyc(4.5));
                e.Graphics.DrawString("姓名：" + txt_xm.Text, objFont3, Brushes.Black, getyc(0.4), getyc(4.5 + i_y));
                e.Graphics.DrawString("性别：" + cbo_xb.SelectedItem.ToString(), objFont3, Brushes.Black, getyc(0.4), getyc(6 + i_y));
                e.Graphics.DrawString("年龄：" + txt_nl.Text + "岁", objFont3, Brushes.Black, getyc(6), getyc(6 + i_y));
                e.Graphics.DrawString("检查项目：" + "肝功能", objFont3, Brushes.Black, getyc(0.4), getyc(8 + i_y));
                e.Graphics.DrawString("申请负责人：" + FrmLogin.str_yhxm, objFont3, Brushes.Black, getyc(0.4), getyc(10 + i_y));
                e.Graphics.DrawString("申请日期：" + dtp_djrq.Value.ToString("yyyy年MM月dd日"), objFont3, Brushes.Black, getyc(3.5), getyc(12.5 + i_y));
                e.Graphics.DrawString("盖章", objFont3, Brushes.Black, getyc(6), getyc(11.5 + i_y));
                e.Graphics.DrawString("请凭本单据到：\n" + clsPublic.getAdress("肝功能") + "进行检查", objFont3, Brushes.Black, getyc(0.4), getyc(13.5 + i_y));
                #endregion
                #region 大便培养字
                double d_add = 10.5;

                e.Graphics.DrawString("广东省食品从业人员体检\n      检查科申请单", objFont2, Brushes.Black, getyc(2 + d_add), getyc(1 + i_y));

                e.Graphics.DrawString("体检编号：" + label1.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(3 + i_y));
                //e.Graphics.DrawString("工作单位：" + txt_gzdw.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(4.5));
                e.Graphics.DrawString("姓名：" + txt_xm.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(4.5 + i_y));
                e.Graphics.DrawString("性别：" + cbo_xb.SelectedItem.ToString(), objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(6 + i_y));
                e.Graphics.DrawString("年龄：" + txt_nl.Text + "岁", objFont3, Brushes.Black, getyc(6 + d_add), getyc(6 + i_y));
                e.Graphics.DrawString("检查项目：" + "大便培养", objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(8 + i_y));
                e.Graphics.DrawString("申请负责人：" + FrmLogin.str_yhxm, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(10 + i_y));
                e.Graphics.DrawString("申请日期：" + dtp_djrq.Value.ToString("yyyy年MM月dd日"), objFont3, Brushes.Black, getyc(3.5 + d_add), getyc(12.5 + i_y));
                e.Graphics.DrawString("盖章", objFont3, Brushes.Black, getyc(6 + d_add), getyc(11.5 + i_y));
                e.Graphics.DrawString("请凭本单据到：\n" + clsPublic.getAdress("大便培养") + "进行检查", objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(13.5 + i_y));
                #endregion

                double d_add2 = 14.8;

                #region 体征字

                e.Graphics.DrawString("广东省食品从业人员体检\n      检查申请单", objFont2, Brushes.Black, getyc(2), getyc(1 + d_add2 + i_y));

                e.Graphics.DrawString("体检编号：" + label1.Text, objFont3, Brushes.Black, getyc(0.4), getyc(3 + d_add2 + i_y));
                //e.Graphics.DrawString("工作单位：" + txt_gzdw.Text, objFont3, Brushes.Black, getyc(0.4), getyc(4.5 + d_add2));
                e.Graphics.DrawString("姓名：" + txt_xm.Text, objFont3, Brushes.Black, getyc(0.4), getyc(4.5 + d_add2 + i_y));
                e.Graphics.DrawString("性别：" + cbo_xb.SelectedItem.ToString(), objFont3, Brushes.Black, getyc(0.4), getyc(6 + d_add2 + i_y));
                e.Graphics.DrawString("年龄：" + txt_nl.Text + "岁", objFont3, Brushes.Black, getyc(6), getyc(6 + d_add2 + i_y));
                e.Graphics.DrawString("检查项目：" + "体征", objFont3, Brushes.Black, getyc(0.4), getyc(8 + d_add2 + i_y));
                e.Graphics.DrawString("申请负责人：" + FrmLogin.str_yhxm, objFont3, Brushes.Black, getyc(0.4), getyc(10 + d_add2 + i_y));
                e.Graphics.DrawString("申请日期：" + dtp_djrq.Value.ToString("yyyy年MM月dd日"), objFont3, Brushes.Black, getyc(3.5), getyc(12.5 + d_add2 + i_y));
                e.Graphics.DrawString("盖章", objFont3, Brushes.Black, getyc(6), getyc(11.5 + d_add2 + i_y));
                e.Graphics.DrawString("请凭本单据到：\n" + clsPublic.getAdress("体征") + "进行检查", objFont3, Brushes.Black, getyc(0.4), getyc(13.5 + d_add2 + i_y));
                #endregion


                #region 胸片字


                e.Graphics.DrawString("广东省食品从业人员体检\n      检查申请单", objFont2, Brushes.Black, getyc(2 + d_add), getyc(1 + d_add2 + i_y));

                e.Graphics.DrawString("体检编号：" + label1.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(3 + d_add2 + i_y));
                //e.Graphics.DrawString("工作单位：" + txt_gzdw.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(4.5 + d_add2));
                e.Graphics.DrawString("姓名：" + txt_xm.Text, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(4.5 + d_add2 + i_y));
                e.Graphics.DrawString("性别：" + cbo_xb.SelectedItem.ToString(), objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(6 + d_add2 + i_y));
                e.Graphics.DrawString("年龄：" + txt_nl.Text + "岁", objFont3, Brushes.Black, getyc(6 + d_add), getyc(6 + d_add2 + i_y));
                e.Graphics.DrawString("检查项目：" + "胸部正位片", objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(8 + d_add2 + i_y));
                e.Graphics.DrawString("申请负责人：" + FrmLogin.str_yhxm, objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(10 + d_add2 + i_y));
                e.Graphics.DrawString("申请日期：" + dtp_djrq.Value.ToString("yyyy年MM月dd日"), objFont3, Brushes.Black, getyc(3.5 + d_add), getyc(12.5 + d_add2 + i_y));
                e.Graphics.DrawString("盖章", objFont3, Brushes.Black, getyc(6 + d_add), getyc(11.5 + d_add2 + i_y));
                e.Graphics.DrawString("请凭本单据到：\n" + clsPublic.getAdress("胸片") + "进行检查", objFont3, Brushes.Black, getyc(0.4 + d_add), getyc(13.5 + d_add2 + i_y));
                #endregion
                #region 划线
                e.Graphics.DrawLine(objPen1, getyc(21 / 2), 0, getyc(21 / 2), getyc(29.6));
                e.Graphics.DrawLine(objPen1, getyc(0), getyc(29.6 / 2), getyc(21), getyc(29.6 / 2));
                #endregion
            }

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            PaperSize pkCustomSize = new PaperSize("First Custom Size", 827, 1170);
            PDoc2.DefaultPageSettings.PaperSize = pkCustomSize;
            ((System.Windows.Forms.Form)Pvd).StartPosition = FormStartPosition.CenterScreen;
            ((System.Windows.Forms.Form)Pvd).Width = 827;
            ((System.Windows.Forms.Form)Pvd).Height = 1170;
            ((System.Windows.Forms.Form)Pvd).Icon = this.Icon;
            Pvd.Document = PDoc2;
            Pvd.ShowDialog();
        }
        public void DrawString(string str, Font objFont, Rectangle rectangle, PrintPageEventArgs e)
        {
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            sf.LineAlignment = StringAlignment.Center;

            //  e.Graphics.DrawString(str, objFont, Brushes.Black, new Rectangle(0, 20, 827, 40), sf);
            e.Graphics.DrawString(str, objFont, Brushes.Black, rectangle, sf);


        }
        public void DrawString2(string str, Font objFont, Rectangle rectangle, PrintPageEventArgs e)
        {
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;

            sf.LineAlignment = StringAlignment.Center;

            //  e.Graphics.DrawString(str, objFont, Brushes.Black, new Rectangle(0, 20, 827, 40), sf);
            e.Graphics.DrawString(str, objFont, Brushes.Black, rectangle, sf);


        }
        public void DrawString3(string str, Font objFont, Rectangle rectangle, PrintPageEventArgs e)
        {
            sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;

            sf.LineAlignment = StringAlignment.Center;

            //  e.Graphics.DrawString(str, objFont, Brushes.Black, new Rectangle(0, 20, 827, 40), sf);
            e.Graphics.DrawString(str, objFont, Brushes.Black, rectangle, sf);


        }
        private void Pvd_Load(object sender, EventArgs e)
        {

        }


        //扫描结构：
        [StructLayout(LayoutKind.Sequential, Size = 16, CharSet = CharSet.Ansi)]
        public struct IDCARD_ALL
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
            public char name;     //姓名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
            public char sex;      //性别
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public char people;    //民族，护照识别时此项为空
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public char birthday;   //出生日期
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 70)]
            public char address;  //地址，在识别护照时导出的是国籍简码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 36)]
            public char number;  //地址，在识别护照时导出的是国籍简码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 30)]
            public char signdate;   //签发日期，在识别护照时导出的是有效期至 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public char validtermOfStart;  //有效起始日期，在识别护照时为空
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public char validtermOfEnd;  //有效截止日期，在识别护照时为空
        }



    }
}

