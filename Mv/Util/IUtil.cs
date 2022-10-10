using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Mv.Util
{
    public interface IUtil : IDisposable
    {
        Bitmap ResizeImage(Stream img, int width, int heigth);
    }
}