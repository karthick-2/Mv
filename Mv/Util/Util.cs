﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Mv.Util
{
    public class Utili : IDisposable, IUtil
    {
        public Bitmap ResizeImage(Stream streamImage, int resizeWidth, int resizeHeight)
        {

            System.Drawing.Image originalPic = System.Drawing.Image.FromStream(streamImage);

            int height = resizeHeight, width = resizeWidth;
            int originalW = originalPic.Width, originalH = originalPic.Height;
            float percentW, percentH, percent;

            percentW = (float)resizeWidth / (float)originalW;
            percentH = (float)resizeHeight / (float)originalH;
            if (percentH < percentW)
            {
                percent = percentH;
                width = (int)(originalW * percent);
            }
            else
            {
                percent = percentW;
                height = (int)(originalH * percent);
            }

            Bitmap thumbnailBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            thumbnailBitmap.SetResolution(originalPic.HorizontalResolution, originalPic.VerticalResolution);

            using (Graphics g = Graphics.FromImage(thumbnailBitmap))
            {
                g.Clear(Color.White);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalPic,
                    new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, originalW, originalH),
                    GraphicsUnit.Pixel);
            }
            return thumbnailBitmap;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //_DB.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}