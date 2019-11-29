using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AForge;
using AForge.Controls;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;

namespace congye_pe
{
    public partial class FrmVideo : Form
    {

        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        public int selectedDeviceIndex = 0;
        public static string fileDir = "";
        Bitmap bitmap1 = null;
        public static string str_picBase64_1 = "";
        public static string str_picBase64_2 = "";
        ClsBase64 clsBase64 = null;
        public static Boolean bidui = false;

        int ifsfz1 = 0;
        int i_index = 0;
        public FrmVideo()
        {
            InitializeComponent();
        }
        public FrmVideo(Bitmap bitmap,int ifsfz)
        {
            InitializeComponent();
           this.bitmap1 = bitmap;
            this.ifsfz1 = ifsfz;
            clsBase64 = new ClsBase64();
        }

      
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            i_index = 1;
            //FrmVideo1 f = new FrmVideo1(i_index);
            //f.ShowDialog();

            pictureBox1.Image = FrmVideo1.bitmap;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            i_index = 2;
            FrmVideo1 f = new FrmVideo1(pictureBox1.Image, i_index);
            f.ShowDialog();
            pictureBox2.Image = FrmVideo1.bitmap;
            //pictureBox2.Image = pictureBox1.Image;
        }

        private void FrmVideo_Load(object sender, EventArgs e)
        {

        }

        private void FrmVideo_Load_1(object sender, EventArgs e)
        {
            if (ifsfz1 == 0)
            {
                //button1.Visible = true;
                pictureBox1.Image = clsBase64.Base64StringToImage(str_picBase64_1);
                pictureBox2.Image = clsBase64.Base64StringToImage(str_picBase64_2);

                pictureBox4.Visible = true;
            }
            else
            {
                pictureBox1.Image = bitmap1;
                pictureBox2.Image = clsBase64.Base64StringToImage(str_picBase64_2);
                pictureBox4.Visible = false;
                //button1.Visible = false;
            }
            //pictureBox1.ImageLocation = @"D:\zp.jpg";
            //pictureBox2.ImageLocation = @"D:\2018033012182773.jpg";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            str_picBase64_1 = clsBase64.ImgToBase64String((Bitmap)pictureBox1.Image);
            str_picBase64_2 = clsBase64.ImgToBase64String((Bitmap)pictureBox2.Image);
            bidui = true;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bidui = false;
            this.Close();
        }
    }
}
