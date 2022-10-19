using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LabelImageSystem.Shapes
{
    /**
     * 类旨在实现界面图像坐标到实际图像坐标的相互转换 
     */
    public class GraphicAndImageChange
    {
        public Rectangle imagePos;  //图像区域在面板上的位置
        public int nImageW;         //图像宽
        public int nImageH;         //图像高
        public float fRateX;        //图像缩放比例
        public float fRateY;        //图像缩放比例

        public GraphicAndImageChange(Rectangle picRect, int PicW, int PicH)
        {
            imagePos = new Rectangle(picRect.X, picRect.Y, picRect.Width, picRect.Height);
            nImageW = PicW;
            nImageH = PicH;
            fRateX = ((float)PicW) / imagePos.Width;
            fRateY = ((float)PicH) / imagePos.Height;
        }

        /**
         * 界面图像坐标转换到实际图像坐标 
         */
        public Point  GraphicsToImage(Point pt)
        {
            Point newPt = new Point();
            int x = pt.X - imagePos.X;
            int y = pt.Y - imagePos.Y;
            newPt.X = (int)(x * fRateX + 0.5);
            newPt.Y = (int)(y * fRateY + 0.5);
            return newPt;
        }

        /**
         * 实际图像坐标转换到界面图像坐标 
         */
        public Point ImageToGraphics(Point pt)
        {
            Point newPt = new Point();
            int x = (int)(pt.X / fRateX + 0.5);
            int y = (int)(pt.Y / fRateY + 0.5);
            x = x < imagePos.Width ? x : imagePos.Width - 1;
            y = y < imagePos.Height ? y : imagePos.Height - 1;
            newPt.X = x + imagePos.X;
            newPt.Y = y + imagePos.Y;
            return newPt;
        }

        /**
        * 将点限定在图像显示区域矩形内  防止跑到边界外
        */
        public Point fixPointInRegion(Point pt)
        {
            pt.X = pt.X > imagePos.Left ? pt.X : imagePos.Left;
            pt.Y = pt.Y > imagePos.Top ? pt.Y : imagePos.Top;
            if (null != imagePos)
            {
                pt.X = pt.X < imagePos.Right ? pt.X : imagePos.Right - 1;
                pt.Y = pt.Y < imagePos.Bottom ? pt.Y : imagePos.Bottom - 1;
            }
            return pt;
        }
    }
}
