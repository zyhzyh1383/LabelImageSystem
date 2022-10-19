using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace LabelImageSystem.Shapes
{
    public class PolygonObj : DrawBase
    {
        public List<Point> vPoint;       //多边形点列表 绘图上
        public List<Point> vPtOriImage;  //多边形点列表 图像上
        public Point m_Ptmouse;       //鼠标位置

        public PolygonObj()
        {
            m_ShapeType = ShapeTypeIndexes.Ploy;
            m_bIsDrawing = false;
            m_bSolid = false;
            m_bIsDrawing = false;
            vPoint = new List<Point>();
            vPtOriImage = new List<Point>();
            m_nSelPtIndex = -1;
        }

        public PolygonObj(PolygonObj obj)
        {
            m_ShapeType = ShapeTypeIndexes.Ploy;
            vPoint = new List<Point>(obj.vPoint.ToArray());
            vPtOriImage = new List<Point>(obj.vPtOriImage.ToArray());
            m_nSelPtIndex = obj.m_nSelPtIndex;
            m_objName = obj.m_objName;
            m_objScript = obj.m_objScript;
        }

        public override void ptGtoImage(GraphicAndImageChange change)
        {
            vPtOriImage.Clear();
            foreach (Point pt in vPoint)
            {
                vPtOriImage.Add(change.GraphicsToImage(pt));
            }
        }

        public override void ptItoGraphic(GraphicAndImageChange change)
        {
            vPoint.Clear();
            foreach (Point pt in vPtOriImage)
            {
                vPoint.Add(change.ImageToGraphics(pt));
            }
        }

        public override void Draw(Graphics g)
        {
            if (vPoint.Count() < 1)
                return;

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

            //绘制多边形
            if (m_bIsDrawing)  //正在绘制的多边形
            {
                for (int i = 0; i < vPoint.Count - 1; i++)
                {
                    g.DrawLine(pen, vPoint[i].X, vPoint[i].Y, vPoint[i + 1].X, vPoint[i + 1].Y);
                }
                g.DrawLine(pen, vPoint[vPoint.Count - 1].X, vPoint[vPoint.Count - 1].Y, m_Ptmouse.X, m_Ptmouse.Y);
            }
            else   //绘制完成的多边形
            {
                g.DrawPolygon(pen, vPoint.ToArray());
            }

            //绘制多边形的顶点
            for (int i = 0; i < vPoint.Count; i++)
            {
                Point pt = vPoint[i];
                int size;

                Brush br = null;
                if (m_nSelPtIndex == i)
                {
                    br = new SolidBrush(m_SelectedColor);
                    size = m_HalfGrab;
                }
                else
                {
                    br = new SolidBrush(m_NormalColor);
                    size = (m_HalfGrab + 1) / 2;
                }
                Rectangle bound = Rectangle.FromLTRB(pt.X - size, pt.Y - size, pt.X + size, pt.Y + size);
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
                g.DrawString(words, new Font("Verdana", 12), new SolidBrush(Color.Red), new PointF(vPoint[0].X, vPoint[0].Y));
            }
            
        }

        //选中目标  如果点在矩形内部 即为选中
        public override bool WholeObject(int x, int y)
        {
            return IsInPolygon(new Point(x, y), vPoint);
        }

        //选中拖动点  如果点在任一顶点附近 即为选中
        public override int AdjustPoint(int x, int y)
        {
            m_nSelPtIndex = -1;
            for (int i = 0; i < vPoint.Count; i++)
            {
                Point pt = vPoint[i];
                Rectangle rect = new Rectangle(pt.X - m_HalfGrab, pt.Y - m_HalfGrab, m_HalfGrab * 2, m_HalfGrab * 2);
                Region reg = new Region(rect);
                if (reg.IsVisible(x, y))
                {
                    m_nSelPtIndex = i;
                    break;
                }
            }

            return m_nSelPtIndex;
        }

        public void Move(Point[] ptlist)
        {
            vPoint = new List<Point>(ptlist);
        }


        public override void MouseDown(Point pt)
        {
            if (false == m_bIsDrawing)
            {
                m_bIsDrawing = true;
            }

            vPoint.Add(pt);
            m_Ptmouse = pt;
        }

        public override void MouseMove(Point pt)
        {
            if (m_bIsDrawing)
            {
                m_Ptmouse = pt;
            }
        }

        public override int MouseUp(Point pt)
        {
            if (m_bIsDrawing)
            {
                m_bIsDrawing = false;
            }

            return 1;
        }

        public override bool CheckValid()
        {
            if (vPoint.Count < 3)
            {
                return false;
            }

            return true;
        }


        //判断点是否在多边形内部， 通过相交法判定
        public static bool IsInPolygon(Point checkPoint, List<Point> polygonPoints)
        {
            int counter = 0;
            int i;
            double xinters;
            Point p1, p2;
            int pointCount = polygonPoints.Count;
            p1 = polygonPoints[0];
            for (i = 1; i <= pointCount; i++)
            {
                p2 = polygonPoints[i % pointCount];
                if (checkPoint.Y > Math.Min(p1.Y, p2.Y)//校验点的Y大于线段端点的最小Y 
                    && checkPoint.Y <= Math.Max(p1.Y, p2.Y))//校验点的Y小于线段端点的最大Y 
                {
                    if (checkPoint.X <= Math.Max(p1.X, p2.X))//校验点的X小于等线段端点的最大X(使用校验点的左射线判断). 
                    {
                        if (p1.Y != p2.Y)//线段不平行于X轴 
                        {
                            xinters = (checkPoint.Y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;
                            if (p1.X == p2.X || checkPoint.X <= xinters)
                            {
                                counter++;
                            }
                        }
                    }

                }
                p1 = p2;
            }

            if (counter % 2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}
