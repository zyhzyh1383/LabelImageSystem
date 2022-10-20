using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Zach.Util
{
    public class ImageHelper
    {
        public static List<int> GetImageSize(string filename)
        {
            Image image = Image.FromFile(filename);
            Size size = new Size(image.Width, image.Height);
            var list = new List<int>();
            list.Add(size.Width);
            list.Add(size.Height);
            image.Dispose();
            return list;
        }
    }
}
