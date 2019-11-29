using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
//using System.Threading.Tasks;
using AForge.Video.DirectShow;
namespace congye_pe
{
    class Common
    {
        /// <summary>
        /// 将指定位置矩形框绘制在图片上
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image DrawImage(Image bitmap, int x, int y, int with, int height)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                var rectangle = new Rectangle(x, y, with, height);
                g.DrawRectangle(new Pen(Color.Red, 2), rectangle);
            }
            return bitmap;
        }
        public static Image DrawImage(Image bitmap, Rectangle rect)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                var rectangle = rect;
                g.DrawRectangle(new Pen(Color.Red, 2), rectangle);
            }
            return bitmap;
        }

        public static Image DrawImage(Image bitmap, int x, int y, int with, int height, string strPrintInfo)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                var rectangle = new Rectangle(x, y, with, height);
                g.DrawRectangle(new Pen(Color.Red, 2), rectangle);
                Font f = new Font("宋体", 20, FontStyle.Bold, GraphicsUnit.Point);
                g.DrawString(strPrintInfo, f, Brushes.Green, new PointF(5, 5));
            }
            return bitmap;
        }
        public static Image DrawImage(Image bitmap, Rectangle rect, string strPrintInfo)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                var rectangle = rect;
                g.DrawRectangle(new Pen(Color.Red, 2), rectangle);
                Font f = new Font("宋体", 20, FontStyle.Bold, GraphicsUnit.Point);
                g.DrawString(strPrintInfo, f, Brushes.Green, new PointF(5, 5));
            }
            return bitmap;
        }
        /// <summary>
        /// 文件转换为BYTE
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] GetFilebyte(string filename)
        {
            if (File.Exists(filename))
            {
                FileStream stream = new FileStream(filename, FileMode.Open);
                byte[] bts = new byte[stream.Length];
                stream.Read(bts, 0, bts.Length);
                stream.Close();
                stream.Dispose();
                return bts;
            }
            return null;
        }
        public static byte[] GetFilebyte(Image img)
        {
            MemoryStream stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            return stream.ToArray();
        }
        public static byte[] GetFilebyte(Image img, System.Drawing.Imaging.ImageFormat format)
        {
            MemoryStream stream = new MemoryStream();
            img.Save(stream, format);
            return stream.ToArray();
        }
        public static Image ConvertImage(string filename)
        {
            byte[] bts = GetFilebyte(filename);
            if (bts == null)
                return null;
            return Image.FromStream(new MemoryStream(bts));
        }

        #region 方法
        /// <summary>
        /// 获取已插USB摄像头硬件Id
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCameraDeviceId()
        {
            List<string> _cameraList = new List<string>();
            FilterInfoCollection _filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);//获取所有已插USB摄像头驱动信息
            if (_filterInfoCollection != null && _filterInfoCollection.Count > 0)
            {
                for (int i = 0; i < _filterInfoCollection.Count; i++)
                {
                    _cameraList.Add(_filterInfoCollection[i].MonikerString); //向集合中添加USB摄像头硬件Id
                }
                _cameraList.Remove(""); //移出空项
                return _cameraList;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
