using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Threading;
using System.IO;
using Accord.Imaging.Filters;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;
using System.Diagnostics;



namespace congye_pe
{
    public partial class FrmVideo1 : Form
    {
        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;

        private Rectangle[] rectL;
        private Rectangle[] rectR;
        SimilarFace sf = new SimilarFace();
        public int selectedDeviceIndex = 0;
        public static string fileDir = "";
        public string str_path = "";
        public static Bitmap bitmap = null;
        public Bitmap bm_sfz = null;
        private Bitmap imgobj;
        private int i_Ifopen = 0;
        private int i_index = 0;
        private List<string> _cameraList = new List<string>();
        /// <summary>
        /// 视频驱动
        /// </summary>
        private VideoCaptureDevice _videoCaptureDevice;
        private Image ImgLoad { get; set; }
        private Image ImgLoad_Click { get; set; }
        public FrmVideo1(Image image1,int i_index)
        {
            InitializeComponent();
            this.i_index = i_index;
            ImgLoad = Common.ConvertImage(Path.GetFullPath("Resource/MainFrom.png"));
            ImgLoad_Click = Common.ConvertImage(Path.GetFullPath("Resource/MainFrom.png"));
            picLoad.Image = ImgLoad;
            RectanglesMarker marker = new RectanglesMarker(rectL, Color.Fuchsia);
        }
        public FrmVideo1()
        {
            InitializeComponent();
           // this.i_index = i_index;
            ImgLoad = Common.ConvertImage(Path.GetFullPath("Resource/MainFrom.png"));
            ImgLoad_Click = Common.ConvertImage(Path.GetFullPath("Resource/MainFrom.png"));
            picLoad.Image = ImgLoad;
        }
        /// <summary>
        /// 初始化摄像头
        /// </summary>
        private void InitialCamera()
        {
            if (this.cobxCameraList.SelectedItem != null)
            {
                _videoCaptureDevice = new VideoCaptureDevice(this.cobxCameraList.SelectedItem.ToString());
                _videoCaptureDevice.NewFrame += HandNewFrame;
            }
        }
        private void HandNewFrame(object sender, NewFrameEventArgs args)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    if (args != null)
                    {
                        this.picCamera.Image = args.Frame.Clone() as Image;
                        imgobj = args.Frame.Clone() as Bitmap;
                    }
                }));
            }
            catch (Exception exception)
            {
                //throw;
            }

        }
        private void UCDynamicIdenty_Load(object sender, EventArgs e)
        {

        }


        private void picLoad_Click(object sender, System.EventArgs e)
        {
            //videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            //selectedDeviceIndex = 0;
            //videoSource = new VideoCaptureDevice(videoDevices[selectedDeviceIndex].MonikerString);//连接摄像头。
            //videoSource.VideoResolution = videoSource.VideoCapabilities[selectedDeviceIndex];
            //videoSourcePlayer1.VideoSource = videoSource;
            //// set NewFrame event handler
            //videoSourcePlayer1.Start();
            //button1.Enabled = false;
            //button2.Enabled = true;
            //button3.Enabled = true;
            try
            {
                if (i_Ifopen == 1)
                {
                    return;
                }
                else
                {
                    InitialCamera();
                    StartCamera();
                    i_Ifopen = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void StartCamera()
        {
            if (_videoCaptureDevice != null)
            {
                _videoCaptureDevice.Start();
            }
        }
        private void button2_Click(object sender, System.EventArgs e)
        {
            if (i_Ifopen == 1)
            {
                bitmap = (Bitmap)picCamera.Image;

                i_Ifopen = 0;
                _videoCaptureDevice.SignalToStop();
                this.Close();
            }
            else
            {
                MessageBox.Show("未打开摄像头！");
            }


        }

        private void button3_Click(object sender, System.EventArgs e)
        {

            //button1.Enabled = true;
            //button2.Enabled = false;
            //button3.Enabled = false;
            try
            {
                if (i_Ifopen == 1)
                {
                    _videoCaptureDevice.SignalToStop();
                    this.Close();
                }
                else

                { this.Close(); }



            }
            catch (Exception ex)
            {
             //  this.Close();
            }
        }

        private void FrmVideo1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (i_Ifopen == 1)
            {
                _videoCaptureDevice.SignalToStop();
            }
        }

        private void FrmVideo1_Load(object sender, System.EventArgs e)
        {
            try
            {
                //if (i_index == 1)
                //{ pictureBox4.Visible = false; }
                //if (i_index == 2)
                //{
                //    pictureBox4.Visible = true;
                //}
                // 枚举所有视频输入设备
                #region 填充摄像头下拉框
                _cameraList = Common.GetCameraDeviceId();//获取所有USB摄像头硬件Id集合
                if (_cameraList != null && _cameraList.Count > 0)
                {
                    foreach (var item in _cameraList)
                    {
                        cobxCameraList.Items.Add(item);//向下拉框中添加摄像头列表
                    }
                }
                else
                {
                    //如何未获取到USB摄像头则禁用此选择
                    cobxCameraList.Enabled = false;
                }
                #endregion
                cobxCameraList.SelectedIndex = 0;
                label2.Text = "";
                lblSpeed.Text = "";
              //  pictureBox4.Image =
                //button1.Enabled = true;
                //button2.Enabled = false;
                //button3.Enabled = false;
                //pictureBox1.ImageLocation = str_path;


            }
            catch (ApplicationException)
            {
                cobxCameraList.Items.Add("No local capture devices");
                videoDevices = null;
                //button1.Enabled = false;
                //button2.Enabled = false;
                //button3.Enabled = false;
            }
        }
        int waittime = 5;
        /*
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (i_Ifopen == 1&&i_index==2)
                {
                    sf = new SimilarFace();
                    if (waittime > 0)
                    {
                        lblSpeed.Text = string.Format("检测倒计时{0}秒", waittime);
                        waittime -= 1;
                    }
                    else
                    {
                        lblSpeed.Text = string.Format("检测倒计时{0}秒", waittime);
                        waittime = 5;
                        if (imgobj != null)
                            picFeature.Image = PicDetect(imgobj);
                        rectL = PicRectDetect((Bitmap)pictureBox4.Image);
                        rectR = PicRectDetect((Bitmap)picFeature.Image);
                        Bitmap bitL = sf.GetSelectImage(rectL[0], pictureBox4.Image);
                        Bitmap bitR = sf.GetSelectImage(rectR[0], picFeature.Image);
                        label2.Text = (sf.GetSimilarDegree(bitL, bitR) * 100).ToString() + "%";
                        if (sf.GetSimilarDegree(bitL, bitR) > 0.7)
                        {
                            imgobj.Save(@"D:\" + DateTime.Now.ToString("yyyyMMddhhmmss") + (sf.GetSimilarDegree(bitL, bitR) * 100).ToString() + ".jpg");
                        }
                    }
                }
                else
                {
                    label2.Text = "";
                    lblSpeed.Text = "";
                }
            }
            catch (Exception ex)
            {
                label2.Text = "0%";
            }
        }
        */
        public Rectangle[] PicRectDetect(Bitmap bitmap)
        {
            HaarCascade cascade = new FaceHaarCascade();
            HaarObjectDetector detector = new HaarObjectDetector(cascade, 30);
            Bitmap picture = bitmap;
            detector.SearchMode = ObjectDetectorSearchMode.NoOverlap;
            detector.ScalingMode = ObjectDetectorScalingMode.SmallerToGreater;
            detector.ScalingFactor = 1.5f;
            detector.UseParallelProcessing = true;
            Stopwatch sw = Stopwatch.StartNew();
            // Process frame to detect objects
            Rectangle[] objects = detector.ProcessFrame(picture);
            sw.Stop();
            return objects;
        }
        public Bitmap PicDetect(Bitmap bitmap)
        {
            HaarCascade cascade = new FaceHaarCascade();
            HaarObjectDetector detector = new HaarObjectDetector(cascade, 30);
            Bitmap picture = bitmap;
            detector.SearchMode = ObjectDetectorSearchMode.NoOverlap;
            detector.ScalingMode = ObjectDetectorScalingMode.SmallerToGreater;
            detector.ScalingFactor = 1.5f;
            detector.UseParallelProcessing = true;

            Stopwatch sw = Stopwatch.StartNew();

            // Process frame to detect objects
            Rectangle[] objects = detector.ProcessFrame(picture);
            sw.Stop();
            if (objects.Length > 0)
            {
                RectanglesMarker marker = new RectanglesMarker(objects, Color.Fuchsia);
                return marker.Apply(picture);
            }
            return picture;
        }
    }
}
