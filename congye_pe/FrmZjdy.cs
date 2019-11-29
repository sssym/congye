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
    public partial class FrmZjdy : Form
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
        public FrmZjdy()
        {
            InitializeComponent();
            dbConn = new DbConn();
            clsBase64 = new ClsBase64();
        }

        private void shuaxin_Click(object sender, EventArgs e)
        {
            strSql = "select tjbh as 体检编号,xm as 姓名, xb as 性别,sfzhm as 身份证号码,djrq as 体检日期,sfzzp ,case when sfsh=0 then '未审核' else '已审核' end 审核状态,gzdw as 工作单位,lxdh as 联系电话,(case when dycs=0 then '未打印' when dycs>0 then '已打印' else '' end) as 打印标识  from table_renyuan where sfsh=1 and 1=1 and (isdelete!=1 or isdelete is null) ";
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
                if (comboBox2.SelectedIndex == 0)
                {
                    strSql = strSql + " and (djrq>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and djrq<'" + dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd") + "')";

                }
                else if (comboBox2.SelectedIndex == 1)
                {
                    strSql = strSql + " and (dyrq>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and dyrq<'" + dateTimePicker2.Value.AddDays(1).ToString("yyyy-MM-dd") + "')";
                }
            }
            if (radioButton2.Checked)
            {
                strSql = strSql + " and dycs!=0";
            }
            else if (radioButton3.Checked)
            {
                strSql = strSql + " and dycs=0";
            }
            strSql = strSql + " order by tjbh desc";
            sqlDataAdapter = dbConn.GetDataAdapter(strSql);
            dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "table1");
            dataGridView1.DataSource = dataSet.Tables["table1"].DefaultView;
            dataGridView1.Columns[5].Visible = false;
        }

        private void FrmZjdy_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            //checkBox1.Checked = true;
            comboBox2.SelectedIndex = 0;
            radioButton3.Checked = true;
            shuaxin_Click(sender,e);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            { return; }
            else
            {
                FrmSelectDY f = new FrmSelectDY();
                f.ShowDialog();
                index = e.RowIndex;
                str_tjbh = dataGridView1[0, index].Value.ToString();
                
                if (FrmSelectDY.i_check_index == 0)
                {
                    str_dybs = dataGridView1["打印标识", index].Value.ToString();
                    if (str_dybs == "已打印")
                    {
                        DialogResult dir = MessageBox.Show("该项证件已打印，是否重打？", "警告", MessageBoxButtons.YesNo);
                        if (dir == DialogResult.No)
                        {
                            return;
                        }
                    }
                    str_xm = dataGridView1[1, index].Value.ToString();
                    str_xb = dataGridView1[2, index].Value.ToString();
                    str_sfz = dataGridView1[3, index].Value.ToString();
                    str_tjrq = dataGridView1[4, index].Value.ToString();
                    str_zplj = dataGridView1[5, index].Value.ToString();
                    PaperSize pkCustomSize = new PaperSize("First Custom Size", 400, 250);
                    PDoc.DefaultPageSettings.PaperSize = pkCustomSize;
                    ((System.Windows.Forms.Form)Pvd).StartPosition = FormStartPosition.CenterScreen;
                    ((System.Windows.Forms.Form)Pvd).Width = 400;
                    ((System.Windows.Forms.Form)Pvd).Height = 250;
                    ((System.Windows.Forms.Form)Pvd).Icon = this.Icon;
                    Pvd.Document = PDoc;
                    if (MessageBox.Show("确认打印？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (DbConn.Print == "1")
                        {
                            PDoc.Print();
                        }
                        else
                        {
                            Pvd.ShowDialog();//
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (FrmSelectDY.i_check_index == 1)
                {

                    PaperSize pkCustomSize = new PaperSize("First Custom Size", 827, 1170);
                    PDoc1.DefaultPageSettings.PaperSize = pkCustomSize;
                    ((System.Windows.Forms.Form)Pvd1).StartPosition = FormStartPosition.CenterScreen;
                    ((System.Windows.Forms.Form)Pvd1).Width = 827;
                    ((System.Windows.Forms.Form)Pvd1).Height = 1170;
                    ((System.Windows.Forms.Form)Pvd1).Icon = this.Icon;
                    Pvd1.Document = PDoc1;
                     Pvd1.ShowDialog();
                   // PDoc1.Print();
                }
            }

            //  Pvd.ShowDialog();
        }
        private void PDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font objFont = new Font("黑体", 11, FontStyle.Regular);
            Font objFont2 = new Font("宋体",int.Parse(DbConn.fontsize), FontStyle.Regular);
            Font objFont3 = new Font("宋体", 8, FontStyle.Regular);
            Font objFont4 = new Font("宋体", 11, FontStyle.Regular);
            Brush objBrush = Brushes.Black;
            Pen objPen = new Pen(objBrush);
            objPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            Pen objPen1 = new Pen(objBrush);
            objPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
            Psd.PageSettings = new System.Drawing.Printing.PageSettings();
            IntPtr hdc = e.Graphics.GetHdc();
            // 设置字符间距  
            e.Graphics.ReleaseHdc(hdc);

            // e.Graphics.DrawString("广东省食品从业人员健康证明", objFont, Brushes.Black, 50, 10);
            //e.Graphics.DrawString("姓名：" + str_xm, objFont2, Brushes.Black, 6, 90);
            e.Graphics.DrawString( str_xm, objFont2, Brushes.Black,int.Parse( DbConn.x1), int.Parse(DbConn.y1));
            //e.Graphics.DrawString("性别：" + str_xb, objFont2, Brushes.Black, 135, 90);
            e.Graphics.DrawString(str_xb, objFont2, Brushes.Black, int.Parse(DbConn.x2), int.Parse(DbConn.y2));

            // e.Graphics.DrawString("身份证(或其他有效证明)号码:" + str_sfz.Substring(0, 13), objFont2, Brushes.Black, 6, 120);// 
            if (str_sfz.Length < 13)
            {
                e.Graphics.DrawString(str_sfz, objFont2, Brushes.Black, int.Parse(DbConn.x3), int.Parse(DbConn.y3));// 

            }
            else
            {
                e.Graphics.DrawString(str_sfz.Substring(0, 13)+"*****", objFont2, Brushes.Black, int.Parse(DbConn.x3), int.Parse(DbConn.y3));// 

            }// e.Graphics.DrawString("身份证(或其他有效证明)号码:"+str_sfz.Substring(0,13), objFont2, Brushes.Black, 10, 120);
            //e.Graphics.DrawString("体检单位（盖章）：" + "遂溪县中医院", objFont2, Brushes.Black, 6, 150);
            e.Graphics.DrawString(DbConn.HOSNAME, objFont2, Brushes.Black, int.Parse(DbConn.x4), int.Parse(DbConn.y4));
            //e.Graphics.DrawString("体检日期：" + Convert.ToDateTime(str_tjrq).ToString("yyyy年MM月dd日") + "（有效期一年）", objFont2, Brushes.Black, 6, 180);
            // e.Graphics.DrawString("体检日期：" + Convert.ToDateTime(str_tjrq).ToString("yyyy年MM月dd日") + "（有效期一年）", objFont2, Brushes.Black, 6, 180);
            e.Graphics.DrawString( Convert.ToDateTime(str_tjrq).Year.ToString(), objFont2, Brushes.Black, int.Parse(DbConn.x5), int.Parse(DbConn.y5));
            e.Graphics.DrawString(Convert.ToDateTime(str_tjrq).Month.ToString(), objFont2, Brushes.Black, int.Parse(DbConn.x6), int.Parse(DbConn.y6));
            e.Graphics.DrawString(Convert.ToDateTime(str_tjrq).Day.ToString(), objFont2, Brushes.Black, int.Parse(DbConn.x7), int.Parse(DbConn.y7));
            int top = 30; int left = 250;

            System.Drawing.Image image = clsBase64.Base64StringToImage(str_zplj);
            //e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic
            //e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
           e.Graphics.DrawImage(image, int.Parse(DbConn.x8), int.Parse(DbConn.y8), int.Parse(DbConn.gao), int.Parse(DbConn.kuan));
            if (DbConn.Print == "1")
            {
                ArrayList al = new ArrayList();
                strSql = "update table_renyuan set dycs=dycs+1,dyrq=getdate() where tjbh='"+str_tjbh+"'";
                al.Add(strSql);
                strSql = "insert into table_zjdy(tjbh,dyrq,dyyh) values('" + str_tjbh + "',getdate(),'"+FrmLogin.str_yhbm+"')";
                al.Add(strSql);
                dbConn.GetTransaction(al);
            }
            
            //int towidth = 80;
            //int toheight = 130;
            ////缩略图矩形框的像素点
            //int x = 0;
            //int y = 0;
            //int ow = image.Width;
            //int oh = image.Height;
            //MessageBox.Show("宽："+ image.Width.ToString()+"-----高："+image.Height.ToString());
            //if (ow > oh)
            //{
            //    toheight = image.Height * 80 / image.Width;
            //}
            //else
            //{
            //    towidth = image.Width * 130 / image.Height;
            //}
            //System.Drawing.Image bm = new System.Drawing.Bitmap(200, 130);

            //System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bm);
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //g.Clear(System.Drawing.Color.White);
            //g.DrawImage(image, new System.Drawing.Rectangle((20 - towidth) / 2, (130 - toheight) / 2, towidth, toheight), 0, 0, ow, oh, System.Drawing.GraphicsUnit.Pixel);
           // g.DrawImage(image, new System.Drawing.Rectangle(10 / 2, (130 - toheight) / 2, towidth, toheight), 0, 0, ow, oh, System.Drawing.GraphicsUnit.Pixel);
            //e.Graphics.DrawLine(objPen, left + 0, top + 0, left + 0, top + 130);//上横
            //e.Graphics.DrawLine(objPen, left + 0, top + 130, left + 86, top + 130);//上横
            //e.Graphics.DrawLine(objPen, left + 86, top + 0, left + 86, top + 130);//上横
            //e.Graphics.DrawLine(objPen, left + 0, top + 0, left + 86, top + 0);//上横
            //e.Graphics.DrawLine(objPen, 0,  0,  0,217);//上横
            //e.Graphics.DrawLine(objPen, 0, 0, 335, 0);//上横
            //e.Graphics.DrawLine(objPen, 335,0, 335, 217);//上横
            //e.Graphics.DrawLine(objPen, 0, 217, 335, 217);//上横

            //displayRect = pictureBox1.Bounds;
            //backImage = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //pictureBox1.BackColor = Color.Transparent;
            //pictureBox1.BackgroundImage = backImage;
            //this.Paint += new PaintEventHandler(Form15_Paint);
            //foreach (Control control in this.Controls)
            //{
            //    if (control.Bounds.IntersectsWith(displayRect))
            //    {
            //        if (control != pictureBox1)
            //        {
            //            list.Add(control);
            //        }
            //    }
            //}
            /*
            int Circle_Line = 60;//记录圆的直径  
            int Circle_Brush = 2;//记录圆画笔的粗细  
           
            //设置圆的绘制区  
            rect = new Rectangle(135, 135, 45,45);
             
            //e.Graphics.Clear(Color.White);//以白色清空panelCachet画布背景  
            Pen mypen = new Pen(Color.Red, Circle_Brush);//设置画笔的颜色  
            e.Graphics.DrawEllipse(mypen, rect);//绘制圆
              Font _font = new Font("Arial", 6);
              Brush _brush = new SolidBrush(Color.Red);  
         Pen _pen = new Pen(Color.Red, 1f);  
            GraphicsText graphicsText = new GraphicsText();
            graphicsText.Graphics = e.Graphics;

            // 绘制围绕点旋转的文本  
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            graphicsText.DrawString("遂", _font, _brush, new PointF(143, 160), format, -90f);
            graphicsText.DrawString("溪", _font, _brush, new PointF(145, 150), format, -50f);
            graphicsText.DrawString("县", _font, _brush, new PointF(152, 143), format, -20f);
            graphicsText.DrawString("中", _font, _brush, new PointF(162, 143), format, 20f);
            graphicsText.DrawString("医", _font, _brush, new PointF(171, 150), format, 50f);
            graphicsText.DrawString("院", _font, _brush, new PointF(173, 160), format, 90f);
            graphicsText.DrawString("★", _font, _brush, new PointF(158, 165), format, 0f);

            //e.Graphics.DrawString("遂", objFont0, Brushes.Red, 70, 165);
            //e.Graphics.DrawString("溪", objFont0, Brushes.Red, 75, 150);
            //e.Graphics.DrawString("县", objFont0, Brushes.Red, 85, 142);
            //e.Graphics.DrawString("中", objFont0, Brushes.Red, 90, 142);
            //e.Graphics.DrawString("医", objFont0, Brushes.Red, 100, 150);
            //e.Graphics.DrawString("院", objFont0, Brushes.Red, 105, 165);
            */
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
            Psd.PageSettings = new System.Drawing.Printing.PageSettings();
            Psd.PageSettings.Margins.Left = 40;
            Psd.PageSettings.Margins.Top = 100;
            //Psd.PageSettings.Margins.Right = 20;
            //Psd.PageSettings.Margins.Bottom = 10;

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
    }
   

}
