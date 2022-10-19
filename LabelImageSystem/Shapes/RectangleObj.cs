using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace LabelImageSystem.Shapes
{
    public class RectangleObj : DrawBase
    {
        //矩形左上角和右下角点  绘图坐标
        public Point m_Start;
        public Point m_End;

        //矩形左上角和右下角点  图像坐标
        public Point m_ptStartOri;
        public Point m_ptEndOri;

        public RectangleObj()
        {
            m_ShapeType = ShapeTypeIndexes.Rect;
            m_bIsDrawing = false;
            m_nSelPtIndex = -1;
        }

        public RectangleObj(RectangleObj obj)
        {
            m_ShapeType = ShapeTypeIndexes.Rect;
            m_Start = obj.m_Start;
            m_End = obj.m_End;
            m_ptStartOri = obj.m_ptStartOri;
            m_ptEndOri = obj.m_ptEndOri;
            m_nSelPtIndex = obj.m_nSelPtIndex;
            m_objName = obj.m_objName;
            m_objScript = obj.m_objScript;
        }

        public override void ptGtoImage(GraphicAndImageChange change)
        {
            m_ptStartOri = change.GraphicsToImage(m_Start);
            m_ptEndOri = change.GraphicsToImage(m_End);
        }

        public override void ptItoGraphic(GraphicAndImageChange change)
        {
            m_Start = change.ImageToGraphics(m_ptStartOri);
            m_End = change.ImageToGraphics(m_ptEndOri);
        }

        //获取矩形区域
        public Rectangle GetBound()
        {
            int x = this.m_Start.X < this.m_End.X ? this.m_Start.X : this.m_End.X;
            int y = this.m_Start.Y < this.m_End.Y ? this.m_Start.Y : this.m_End.Y;
            int r = this.m_Start.X < this.m_End.X ? this.m_End.X : this.m_Start.X;
            int b = this.m_Start.Y < this.m_End.Y ? this.m_End.Y : this.m_Start.Y;
            return Rectangle.FromLTRB(x, y, r, b);
        }

        //移动形状位置
        public void Move(Point pt1, Point pt2)
        {
            this.m_Start = pt1;
            this.m_End = pt2;
        }


        public override void Draw(Graphics g)
        {
            //选择画笔
            Color color = m_NormalColor;
            int width = 2;
            if (this.m_bSolid || m_bSelect)
            {
                width = 3;
            }
            if (m_bSelect)
            {
                color = m_SelectedColor;
            }

            Pen pen = new Pen(color, width);


            //画出矩形
            g.DrawLine(pen, m_Start.X, m_Start.Y, m_Start.X, m_End.Y);
            g.DrawLine(pen, m_Start.X, m_Start.Y, m_End.X, m_Start.Y);
            g.DrawLine(pen, m_Start.X, m_End.Y, m_End.X, m_End.Y);
            g.DrawLine(pen, m_End.X, m_Start.Y, m_End.X, m_End.Y);


            //画出正常显示的顶点
            int nNomrWidth = (m_HalfGrab + 1) / 2;
            Point pt = m_Start;
            Rectangle bound = Rectangle.FromLTRB(pt.X - nNomrWidth, pt.Y - nNomrWidth, pt.X + nNomrWidth, pt.Y + nNomrWidth);
            Brush br = new SolidBrush(m_NormalColor);
            g.FillRectangle(br, bound);

            pt = m_End;
            bound = Rectangle.FromLTRB(pt.X - nNomrWidth, pt.Y - nNomrWidth, pt.X + nNomrWidth, pt.Y + nNomrWidth);
            br = new SolidBrush(m_NormalColor);
            g.FillRectangle(br, bound);
            //画出突出显示的顶点
            if (0 == m_nSelPtIndex)
            {
                pt = m_Start;
                bound = Rectangle.FromLTRB(pt.X - m_HalfGrab, pt.Y - m_HalfGrab, pt.X + m_HalfGrab, pt.Y + m_HalfGrab);
                br = new SolidBrush(m_SelectedColor);
                g.FillRectangle(br, bound);
            }
            else if (1 == m_nSelPtIndex)
            {
                pt = m_End;
                bound = Rectangle.FromLTRB(pt.X - m_HalfGrab, pt.Y - m_HalfGrab, pt.X + m_HalfGrab, pt.Y + m_HalfGrab);
                br = new SolidBrush(m_SelectedColor);
                g.FillRectangle(br, bound);
            }

            //显示目标名称
            string words = m_objScript;
            if (null == words)
            {
                words = m_objName;
            }
            if (null != words)
            {
                g.DrawString(words, new Font("Verdana", 12), new SolidBrush(Color.Red), new PointF(m_Start.X, m_Start.Y));
            }
        }

        //选中目标  如果点在矩形内部 即为选中
        public override bool WholeObject(int x, int y)
        {
            int l = this.m_Start.X < this.m_End.X ? this.m_Start.X : this.m_End.X;
            int t = this.m_Start.Y < this.m_End.Y ? this.m_Start.Y : this.m_End.Y;
            int r = this.m_Start.X < this.m_End.X ? this.m_End.X : this.m_Start.X;
            int b = this.m_Start.Y < this.m_End.Y ? this.m_End.Y : this.m_Start.Y;

            Rectangle bound = Rectangle.FromLTRB(l, t, r, b);
            Region reg = new Region(bound);
            return reg.IsVisible(x, y);
        }

        //选中拖动点  如果点在矩形左上 右下点附近 即为选中
        public override int AdjustPoint(int x, int y)
        {
            Rectangle rect1 = new Rectangle(m_Start.X - m_HalfGrab, m_Start.Y - m_HalfGrab, m_HalfGrab * 2, m_HalfGrab * 2);
            Rectangle rect2 = new Rectangle(m_End.X - m_HalfGrab, m_End.Y - m_HalfGrab, m_HalfGrab * 2, m_HalfGrab * 2);

            Region reg1 = new Region(rect1);
            Region reg2 = new Region(rect2);
            m_nSelPtIndex = -1;
            if (reg1.IsVisible(x, y))
            {
                m_nSelPtIndex = 0;
            }
            else if (reg2.IsVisible(x, y))
            {
                m_nSelPtIndex = 1;
            }

            return m_nSelPtIndex;
        }


        public override void MouseDown(Point pt)
        {
            m_Start = pt;
            m_bIsDrawing = true;
        }

        public override void MouseMove(Point pt)
        {
            if (m_bIsDrawing)
            {
                m_End = pt;   //记录鼠标左键按下时位置
            }
        }

        public override int MouseUp(Point pt)
        {
            if (m_bIsDrawing)
            {
                m_End = pt;   //记录鼠标左键按下时位置
                m_bIsDrawing = false;
                return 0;
            }

            return 1;
        }

        public override bool CheckValid()
        {
            if ((m_End.X - m_Start.X) * (m_End.Y - m_Start.Y) < 10)
            {
                return false;
            }

            return true;
        }
    }
}
