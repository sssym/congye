﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;


namespace congye_pe
{
    class SimilarFace
    {
        public SimilarFace()
        {
        }
        public String GetHash(Image img)
        {
            Image image = ReduceSize(img);
            Byte[] grayValues = ReduceColor(image);
            Byte average = CalcAverage(grayValues);
            String reslut = ComputeBits(grayValues, average);
            return reslut;
        }
        // Step 1 : Reduce size to 8*8
        private Image ReduceSize(Image SourceImg, int width = 100, int height = 100)
        {
            Image image = SourceImg.GetThumbnailImage(width, height, () => { return false; }, IntPtr.Zero);
            return image;
        }
        // Step 2 : Reduce Color
        private Byte[] ReduceColor(Image image)
        {
            Bitmap bitMap = new Bitmap(image);
            Byte[] grayValues = new Byte[image.Width * image.Height];
            for (int x = 0; x < image.Width; x++)
                for (int y = 0; y < image.Height; y++)
                {
                    Color color = bitMap.GetPixel(x, y);
                    byte grayValue = (byte)((color.R * 30 + color.G * 59 + color.B * 11) / 100);
                    grayValues[x * image.Width + y] = grayValue;
                }
            return grayValues;
        }
        // Step 3 : Average the colors
        private Byte CalcAverage(byte[] values)
        {
            int sum = 0;
            for (int i = 0; i < values.Length; i++)
                sum += (int)values[i];
            return Convert.ToByte(sum / values.Length);
        }
        // Step 4 : Compute the bits
        private String ComputeBits(byte[] values, byte averageValue)
        {
            char[] result = new char[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] < averageValue)
                    result[i] = '0';
                else
                    result[i] = '1';
            }
            return new String(result);
        }
        // Compare hash
        public static Int32 CalcSimilarDegree(string a, string b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException();
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                    count++;
            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public Bitmap GetSelectImage(Rectangle rect, Image image)
        {
            Bitmap bit = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(bit))
            {
                g.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
            }
            return bit;
        }

        public double GetSimilarDegree(Bitmap bitL, Bitmap bitR)
        {
            string L = GetHash(bitL);
            string R = GetHash(bitR);
            double count = CalcSimilarDegree(L, R);
            return count / L.Length;
        }
    }
}
