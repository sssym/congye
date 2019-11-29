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
using System.Drawing.Drawing2D;  

namespace congye_pe
{
    public partial class FrmRenYuan : Form
    {
        DbConn dbConn = null;
        string strSql = "";
        ArrayList al = null;
        SqlDataAdapter sqlDataAdapter = null;
        DataSet dataSet = null;
        SqlDataReader sqlDataReader = null;
        ClsBase64 clsBase64 = null;
        int index =0;
        string str_xm = "";
        string str_xb = "";
        string str_sfz = "";
        string str_tjrq = "";
        string str_zplj = "";
        string str_tjbh = "";
        string str_dybs = "";
        Font font = new Font("宋体", 22);
            Rectangle displayRect;
            Bitmap backImage;
            List<Control> list = new List<Control>();
            Font var_Font = new Font("Arial", 13, FontStyle.Bold);//定义字符串的样式  
            Rectangle rect = new Rectangle(40, 40, 300, 300);//定义一个矩形  
        public FrmRenYuan()
        {
            InitializeComponent();
            dbConn = new DbConn();
            clsBase64 = new ClsBase64();
        }

        private void shuaxin_Click(object sender, EventArgs e)
        {
            strSql = "select  tjbh as 体检编号,xm as 姓名, "
                +"xb as 性别,sfzhm as 身份证号码,djrq as 登记日期,sfzzp from table_renyuan where sfsh=1 and 1=1 and (isdelete!=1 or isdelete is null) ";
            if (comboBox1.SelectedIndex == 0)
            {
                strSql = strSql + " and tjbh like '%" + textBox2.Text + "%'";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                strSql = strSql + " and xm like '%" + textBox2.Text + "%'";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                strSql = strSql + " and gzdw like '%" + textBox2.Text + "%'";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                strSql = strSql + " and sfzhm like '%" + textBox2.Text + "%'";
            }
            if (checkBox1.Checked)
            {
                
                    strSql = strSql + " and (djrq>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and djrq<'" + dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd") + "')";

            }
            strSql = strSql + " order by tjbh desc";
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            dataGridView1.Columns["sfzzp"].Visible = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = false;
            }

        }

        private void FrmZjdy_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            //checkBox1.Checked = true;
            shuaxin_Click(sender,e);
        }

       
        protected void Form15_Paint(object sender, PaintEventArgs e)
        {
            Redraw();
        }

        private void Redraw()
        {
            Graphics g = Graphics.FromImage(backImage);
            foreach (Control control in list)
            {
                Bitmap temp = new Bitmap(control.Width, control.Height);
                control.DrawToBitmap(temp, new Rectangle(System.Drawing.Point.Empty, control.Size));
                Rectangle rect = control.Bounds;
                rect.Intersect(displayRect);
                Rectangle rect2 = rect;
                rect.X = rect.X - control.Left;
                rect.Y = rect.Y - control.Top;
                rect2.X = rect2.X - pictureBox1.Left;
                rect2.Y = rect2.Y - pictureBox1.Top;
                g.DrawImage(temp, rect2, rect, GraphicsUnit.Pixel);
                temp.Dispose();
            }
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(DbConn.HOSNAME,font , Brushes.Red, 0, 35);
            g.DrawEllipse(Pens.Red, new Rectangle(0, 20, 98, 60));
            g.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void panelCachet_Paint(object sender, PaintEventArgs e)
        {
           // int Circle_Line = 0;//记录圆的直径  
            int Circle_Brush = 4;//记录圆画笔的粗细  
           // if (panelCachet.Width >= panelCachet.Height)//panelCachet的宽度大于高度  
           // {
           //     Circle_Line = panelCachet.Height;//设置高度为圆的直径  
           // }
           // else
           // {
           //     Circle_Line = panelCachet.Width;//设置宽度为圆的直径  
           // }
           // //设置圆的绘制区  
           // rect = new Rectangle(Circle_Line / 2 + Circle_Brush, Circle_Brush, Circle_Line - Circle_Brush * 2, Circle_Line - Circle_Brush * 2);
           // Font star_Font = new Font("Arial", 30, FontStyle.Regular);//设置星号的字体样式  
           //// string star_Str = "★";//设置星号的字符串  
            //Graphics g = this.panelCachet.CreateGraphics();//实例化Graphic类  
           // g.SmoothingMode = SmoothingMode.AntiAlias;//消除绘制图形的锯齿，平滑  
           // g.Clear(Color.White);//以白色清空panelCachet画布背景  
           // Pen mypen = new Pen(Color.Red, Circle_Brush);//设置画笔的颜色  
           // g.DrawEllipse(mypen, rect);//绘制圆  
           
           // SizeF var_Size = new SizeF(rect.Width, rect.Width);//实例化一个SizeF  
           // //var_Size = g.MeasureString(star_Str, star_Font);//对星号字符串进行宽度和长度测量  
           // //g.DrawString(star_Str, star_Font, mypen.Brush, new PointF(rect.Width + Circle_Brush - var_Size.Width / 2F, rect.Height / 2F - var_Size.Width / 2F));//在指定的位置绘制星号  
           // var_Size = g.MeasureString("专用章", var_Font);//对指定字符串进行宽度和长度测量  
           // g.DrawString("专用章", var_Font, mypen.Brush, new PointF(rect.Width + Circle_Brush - var_Size.Width / 2F, rect.Height / 2F + var_Size.Height * 2));
           // string tempstr = "中医院";
           // int len = tempstr.Length;
           // float angle = 0 + (0 - len * 20) / 2;//设置文字的旋转角度，以20度的弧度旋转  
           // for (int i = 0; i < len; i++)//旋转每一个字  
           // {
           //     //将指定的平移添加到g的变换矩阵前  
           //     g.TranslateTransform((Circle_Line + Circle_Brush / 2), (Circle_Line + Circle_Brush / 2) / 2);
           //     g.RotateTransform(angle);//将指定的旋转用于Graphic类的变换矩阵  
           //     Brush mybrush = Brushes.Red;//定义画笔  
           //     g.DrawString(tempstr.Substring(i, 1), var_Font, mybrush, 60, 0);//显示旋转每一个文字  
           //     g.ResetTransform();//将Graphics类的全局变换矩阵重置为单位矩阵  
           //     angle += 20;//设置下一个文字的显示角度  
           // }
            rect = new Rectangle(95, 150, 45, 45);
            Font star_Font = new Font("Arial", 30, FontStyle.Regular);//设置星号的字体样式  
            // string star_Str = "★";//设置星号的字符串  
            Graphics g = this.CreateGraphics();//实例化Graphic类  
            g.SmoothingMode = SmoothingMode.AntiAlias;//消除绘制图形的锯齿，平滑  
            //e.Graphics.Clear(Color.White);//以白色清空panelCachet画布背景  
            Pen mypen = new Pen(Color.Red, Circle_Brush);//设置画笔的颜色  
            g.DrawEllipse(mypen, rect);//绘制圆



            string tempstr = DbConn.HOSNAME;
            int len = tempstr.Length;
            float angle = 90 + (90 - len * 20) / 2;//设置文字的旋转角度，以20度的弧度旋转  
            for (int i = 0; i < len; i++)//旋转每一个字  
            {
                //将指定的平移添加到g的变换矩阵前  
                e.Graphics.TranslateTransform(0, 0);
                e.Graphics.RotateTransform(angle);//将指定的旋转用于Graphic类的变换矩阵  
                Brush mybrush = Brushes.Red;//定义画笔  
                e.Graphics.DrawString(tempstr.Substring(i, 1), star_Font, mybrush, 6, 140);//显示旋转每一个文字  
                e.Graphics.ResetTransform();//将Graphics类的全局变换矩阵重置为单位矩阵  
                angle += 20;//设置下一个文字的显示角度  
            }  
        }

        private void PDoc1_PrintPage(object sender, PrintPageEventArgs e)
        {
            al = new ArrayList();
            al = getList(str_tjbh);
            Font objFont = new Font("方正小标宋简体", 22, FontStyle.Regular);
            Font objFont2 = new Font("宋体", 12, FontStyle.Regular);
            Font objFont3 = new Font("宋体", 9, FontStyle.Regular);
            Brush objBrush = Brushes.Black;
            Pen objPen = new Pen(objBrush);
            objPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Pen objPen1 = new Pen(objBrush);
            objPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
           

            int nLeft = 94;
            int nTop = 295;
            int nline = 30;
            int nrow = 45;

            #region 画线框
            e.Graphics.DrawLine(objPen, nLeft, nTop, nLeft + 650, nTop);//上横
            e.Graphics.DrawLine(objPen, nLeft, nTop + 2 * nline, nLeft + 650, nTop + 2 * nline);//第一行横
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
            e.Graphics.DrawLine(objPen, nLeft, nTop + 8 * nline, nLeft + 650, nTop + 8 * nline);//胸片下面的横
            e.Graphics.DrawLine(objPen, nLeft + getyc(1.7), nTop + 8 * nline, nLeft + getyc(1.7), nTop + 16 * nline);//生化右边的竖
            e.Graphics.DrawLine(objPen, nLeft + getyc(1.7), nTop + 9 * nline, nLeft + 650, nTop + 9 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(3), nTop + 10 * nline, nLeft + 650, nTop + 10 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(1.7), nTop + 11 * nline, nLeft + 650, nTop + 11 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(3), nTop + 12 * nline, nLeft + 650, nTop + 12 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(3), nTop + 13 * nline, nLeft + 650, nTop + 13 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(1.7), nTop + 14 * nline, nLeft + 650, nTop + 14 * nline);//
            e.Graphics.DrawLine(objPen, nLeft, nTop + 16 * nline, nLeft + 650, nTop + 16 * nline);//
            e.Graphics.DrawLine(objPen, nLeft, nTop + 19 * nline, nLeft + 650, nTop + 19 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(3), nTop + 9 * nline, nLeft + getyc(3), nTop + 14 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(8.4), nTop + 8 * nline, nLeft + getyc(8.4), nTop + 14 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(13.2), nTop + 8 * nline, nLeft + getyc(13.2), nTop + 14 * nline);//
            e.Graphics.DrawLine(objPen, nLeft + getyc(7.5), nTop + 16 * nline, nLeft + getyc(7.5), nTop + 19 * nline);//
            e.Graphics.DrawLine(objPen1, nLeft, nTop + getyc(16), nLeft + 650, nTop + getyc(16));//
            #endregion
            #region 写字

            e.Graphics.DrawString("广东省食品从业人员健康检查表", objFont, Brushes.Black, getyc(5), getyc(4.5));
            e.Graphics.DrawString("检查日期：" + DateTime.Parse(al[0].ToString()).ToString("  yyyy年  MM月  dd日") + "    单位：" + al[1].ToString(), objFont2, Brushes.Black, nLeft + 5, 250);
            e.Graphics.DrawString("姓名：" + al[2].ToString() + "       性别：" + al[3].ToString() + "        年龄：" + al[4].ToString() + "岁", objFont2, Brushes.Black, nLeft + 5, 275);
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
            e.Graphics.DrawString("编号：" + al[36].ToString(), objFont3, Brushes.Black, nLeft + 10, nTop + 640);

            e.Graphics.DrawString("广东省食品从业人员健康检查回执", objFont2, Brushes.Black, nLeft + 138, nTop + 670);
            e.Graphics.DrawString("检查日期：" + DateTime.Parse(al[0].ToString()).ToString("        yyyy年    MM月    dd日"), objFont3, Brushes.Black, nLeft + 10, nTop + 700);
            e.Graphics.DrawString("姓名：" + al[2].ToString() + "      性别：" + al[3].ToString() + "        单位：" + al[1].ToString(), objFont3, Brushes.Black, nLeft + 10, nTop + 720);
            e.Graphics.DrawString("姓名、单位涂改无效，发表之日起限30天内体检有效，超期此表作废。", objFont3, Brushes.Black, nLeft + 10, nTop + 740);
            //e.Graphics.DrawString(txt_gy.Text, objFont2, Brushes.Black, nLeft + 150, nTop + 40);
            //e.Graphics.DrawString(txt_lj.Text, objFont2, Brushes.Black, nLeft + 225, nTop + 40);
            //e.Graphics.DrawString(txt_sh.Text, objFont2, Brushes.Black, nLeft + 315, nTop + 40);
            //e.Graphics.DrawString(txt_fjh.Text, objFont2, Brushes.Black, nLeft + 410, nTop + 40);
            //e.Graphics.DrawString(txt_pfb.Text, objFont2, Brushes.Black, nLeft + 490, nTop + 40);
            //e.Graphics.DrawString(txt_qt.Text, objFont2, Brushes.Black, nLeft + 575, nTop + 40);

            #endregion
            #region 填写结果数据

            e.Graphics.DrawString(al[5].ToString(), objFont2, Brushes.Black, nLeft + 150, nTop + 40);//肝炎
            e.Graphics.DrawString(al[6].ToString(), objFont2, Brushes.Black, nLeft + 225, nTop + 40);//痢疾
            e.Graphics.DrawString(al[7].ToString(), objFont2, Brushes.Black, nLeft + 315, nTop + 40);//伤寒
            e.Graphics.DrawString(al[8].ToString(), objFont2, Brushes.Black, nLeft + 410, nTop + 40);//肺结核
            e.Graphics.DrawString(al[9].ToString(), objFont2, Brushes.Black, nLeft + 490, nTop + 40);//皮肤病
            e.Graphics.DrawString(al[10].ToString(), objFont2, Brushes.Black, nLeft + 575, nTop + 40);//其他
            e.Graphics.DrawString(al[11].ToString(), objFont2, Brushes.Black, nLeft + 120, nTop + 67);//心
            e.Graphics.DrawString(al[12].ToString(), objFont2, Brushes.Black, nLeft + 410, nTop + 67);//肝
            e.Graphics.DrawString(al[13].ToString(), objFont2, Brushes.Black, nLeft + 120, nTop + 100);//脾
            e.Graphics.DrawString(al[14].ToString(), objFont2, Brushes.Black, nLeft + 410, nTop + 100);//肺
            if (al[15].ToString() == "True")
            {
                e.Graphics.DrawString("√", objFont2, Brushes.Black, nLeft + 215, nTop + 125);//皮肤1
            }
            if (al[16].ToString() == "True")
            {
                e.Graphics.DrawString("√", objFont2, Brushes.Black, nLeft + 350, nTop + 125);//皮肤2
            }
            if (al[17].ToString() == "True")
            {
                e.Graphics.DrawString("√", objFont2, Brushes.Black, nLeft + 439, nTop + 125);//皮肤3
            }
            e.Graphics.DrawString(al[18].ToString(), objFont2, Brushes.Black, nLeft + 120, nTop + 155);//体征其他
            e.Graphics.DrawString(al[19].ToString(), objFont2, Brushes.Black, nLeft + 535, nTop + 155);//医师签名
            e.Graphics.DrawString(al[20].ToString(), objFont2, Brushes.Black, nLeft + 120, nTop + 195);//胸片
            e.Graphics.DrawString(al[21].ToString(), objFont2, Brushes.Black, nLeft + 505, nTop + 220);//医师签名
            e.Graphics.DrawString(al[22].ToString(), objFont2, Brushes.Black, nLeft + 355, nTop + 280);//化验1
            e.Graphics.DrawString(al[23].ToString(), objFont2, Brushes.Black, nLeft + 535, nTop + 280);//医师签名
            e.Graphics.DrawString(al[24].ToString(), objFont2, Brushes.Black, nLeft + 355, nTop + 307);//化验2
            e.Graphics.DrawString(al[25].ToString(), objFont2, Brushes.Black, nLeft + 535, nTop + 307);//医师签名
            e.Graphics.DrawString(al[26].ToString(), objFont2, Brushes.Black, nLeft + 355, nTop + 338);//化验3
            e.Graphics.DrawString(al[27].ToString(), objFont2, Brushes.Black, nLeft + 535, nTop + 338);//医师签名
            e.Graphics.DrawString(al[28].ToString(), objFont2, Brushes.Black, nLeft + 355, nTop + 367);//化验4
            e.Graphics.DrawString(al[29].ToString(), objFont2, Brushes.Black, nLeft + 535, nTop + 367);//医师签名
            e.Graphics.DrawString(al[30].ToString(), objFont2, Brushes.Black, nLeft + 355, nTop + 397);//化验5
            e.Graphics.DrawString(al[31].ToString(), objFont2, Brushes.Black, nLeft + 535, nTop + 397);//医师签名
            e.Graphics.DrawString(al[32].ToString(), objFont2, Brushes.Black, nLeft + 120, nTop + 445);//化验其他
            e.Graphics.DrawString(al[33].ToString(), objFont2, Brushes.Black, nLeft + 73, nTop + 520);//检查结论
            e.Graphics.DrawString(al[34].ToString(), objFont2, Brushes.Black, nLeft + 200, nTop + 550);//医师签名
            e.Graphics.DrawString(al[35].ToString(), objFont2, Brushes.Black, nLeft + 355, nTop + 520);//体检机构
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
        public ArrayList getList(string str_tjbh)
        {
          ArrayList  al1 = new ArrayList();
            strSql = "select djrq, gzdw, xm, xb,nl, table_tjjg.ganyan,liji,shanghan,feijiehe,"
                + "pifubing,qita,xin,gan,[pi],fei,pifu1,pifu2,pifu3,tizhengqita,ys1,xiongpian,"
                + "ys2,dabian1,ys3,dabian2,ys4,gangongneng1,ys5,gangongneng2,ys6,gangongneng3,ys7,"
                + "huayanqita,jcjl,ys8,jcjgyj,table_renyuan.tjbh from table_renyuan,table_tjjg "
                + "where table_renyuan.tjbh=table_tjjg.tjbh "
                + "and table_renyuan.tjbh='" + str_tjbh + "'";
            sqlDataReader = dbConn.GetDataReader(strSql);
            if (sqlDataReader.Read())
            {
                SqlDataAdapter sqlDataAdapter = dbConn.GetDataAdapter(strSql);
                DataSet dataset = new DataSet();
                sqlDataAdapter.Fill(dataset, "table1");
                for (int i = 0; i < dataset.Tables["table1"].Columns.Count ; i++)
                {
                    al.Add(sqlDataReader.GetValue(i).ToString());
                }
            }
            return al;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string strData = "";
            strSql = "select sfzzp from table_renyuan where id=51";
            string str2 = "";
            SqlDataReader sd = dbConn.GetDataReader(strSql);
            if (sd.Read())
            {
                strData = sd.GetValue(0).ToString();
            }
            FileStream fsWrite = new FileStream(@".\11.jpg", FileMode.OpenOrCreate, FileAccess.Write);
            byte[] buf = new byte[strData.Length / 2];
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = Convert.ToByte(strData.Substring(i * 2, 2), 16);
                fsWrite.Write(buf, 0, buf.Length);
            }
            fsWrite.Flush();
            fsWrite.Close();

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            shuaxin_Click(sender,e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                al = new ArrayList();
                dataGridView1.EndEdit();
                DialogResult dir = MessageBox.Show("确定批量登记选中人员？", "提示", MessageBoxButtons.YesNo);


                if (dir == DialogResult.Yes)
                {
                    strSql = "select value from table_canshu where type=1";
                    SqlDataReader sdr = dbConn.GetDataReader(strSql);
                    sdr.Read();
                    str_tjbh = (int.Parse(sdr.GetValue(0).ToString()) - 1).ToString();

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                       // MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                        Boolean flag = Convert.ToBoolean(checkCell.Value);
                        

                        if (flag)
                        {
                            string str_date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            str_tjbh = (int.Parse(str_tjbh) + 1).ToString();
                            strSql = "insert into table_renyuan(xm,xb,nl,sfzhm,lxdh,gzdw,hylbdl,sfzzp,djrq,sfsh,tjbh,sfsfz) "
                                + "select xm,xb,nl,sfzhm,lxdh,gzdw,hylbdl,sfzzp,'" + str_date + "',0,'" + str_tjbh + "',sfsfz from table_renyuan where tjbh='" + dataGridView1.Rows[i].Cells["体检编号"].Value.ToString() + "'";
                            al.Add(strSql);
                            strSql = "insert into table_tjjg(tjbh,ganyan,liji,shanghan,feijiehe,pifubing,qita) " +
                               "select '" + str_tjbh + "',ganyan,liji,shanghan,feijiehe,pifubing,qita from table_tjjg where tjbh='" + dataGridView1.Rows[i].Cells["体检编号"].Value.ToString() + "'";
                            al.Add(strSql);
                            strSql = "update table_canshu set value=value+1 where type=1";
                            al.Add(strSql);
                        }
                    }
                    if (dbConn.GetTransaction(al))
                    {
                        MessageBox.Show("批量登记成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.EndEdit();
        }
    }
   

}
