using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace congye_pe
{
    class ClsBase64
    {
        public string ImgToBase64String(Bitmap bmp1)
        {
            try
            {
                //Bitmap bmp = new Bitmap(Imagefilename);
                Bitmap bmp = bmp1;
                //this.pictureBox1.Image = bmp;
                //FileStream fs = new FileStream((Imagefilename + ".txt", FileMode.Create);
                //StreamWriter sw = new StreamWriter(fs);
                //System.Drawing.Imaging.ImageFormat if1 = bmp.RawFormat;
                //if (if1 == null)
                //{
                //    if1 = System.Drawing.Imaging.ImageFormat.Jpeg;
                //}
                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
               

                //sw.Close();
                //fs.Close();
                // MessageBox.Show("转换成功!");
                return strbaser64;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

       
        //base64编码的文本 转为    图片
        public Bitmap Base64StringToImage(string txtFileName)
        {
            try
            {
                //FileStream ifs = new FileStream(txtFileName, FileMode.Open, FileAccess.Read);
                //StreamReader sr = new StreamReader(ifs);

                //String inputStr = sr.ReadToEnd();
                //byte[] arr = Convert.FromBase64String(inputStr);
                byte[] arr = Convert.FromBase64String(txtFileName);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                //bmp.Save(txtFileName + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //bmp.Save(txtFileName + ".bmp", ImageFormat.Bmp);
                //bmp.Save(txtFileName + ".gif", ImageFormat.Gif);
                //bmp.Save(txtFileName + ".png", ImageFormat.Png);
                //ms.Close();
                //sr.Close();
                //ifs.Close();
                
                //if (File.Exists(txtFileName))
                //{
                //    File.Delete(txtFileName);
                //}
                return bmp;
                //MessageBox.Show("转换成功！");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string Encodebase64(string code)
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

        public string Decodebase64(string code)
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

    }
}
