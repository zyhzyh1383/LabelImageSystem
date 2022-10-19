using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace LabelImageSystem.Shapes
{
    //绘制形状类
    public class DrawShapes
    {
        public List<DrawBase> m_vList;    //形状列表
        public int m_selType = -1;        //选中状态  -1 未选中  0  选中整个目标  1 选中一个拖动点
        public DrawBase m_CurrentSel;     //当前选中目标
        public DrawBase m_CurrentCover;  //当前指针初几覆盖目标
        private DrawBase m_CurrentOri;    //记录原始位置
        private Point m_RejustStart;      //调整起始参考点位置
        public GraphicAndImageChange m_GIChange;   //图像到绘图坐标变换类

        public DrawShapes()
        {
            m_vList = new List<DrawBase>();
            m_CurrentCover = null;
            m_CurrentSel = null;
            m_CurrentOri = null;
            m_GIChange = null;
        }

        //设置图像在界面上的显示位置和图像实际像素尺寸
        public int SetImageInfo(Rectangle rect, int imageW, int imageH)
        {
            m_GIChange = new GraphicAndImageChange(rect, imageW, imageH);
            return 0;
        }

        //通过目标名称索引目标描述
        public void UpdateObjectScriptInfo(List<ObejctDefineViewModel> m_vObj)
        {
            foreach (DrawBase draw in m_vList)
            {
                for (int i = 0; i < m_vObj.Count; i++)
                {
                    if (draw.m_objName == m_vObj[i].ObjName)
                    {
                        draw.m_objScript = m_vObj[i].ObjScript;
                        break;
                    }
                }
            }
        }

        //所有形状 图像坐标转换为界面坐标
        public int AllShapesImageToGrapic()
        {
            foreach (DrawBase draw in m_vList)
            {
                draw.ptItoGraphic(m_GIChange);
            }
            return 0;
        }

        //删除目标
        public void Remove(DrawBase drawbase)
        {
            m_vList.Remove(drawbase);
        }

        public void Clear()
        {
            m_vList.Clear();
            m_CurrentCover = null;
            m_CurrentSel = null;
            m_CurrentOri = null;
            m_selType = -1;
        }

        //增加目标
        public int Add(DrawBase draw)
        {
            DrawBase newdraw = null;
            if (ShapeTypeIndexes.Rect == draw.m_ShapeType)
            {
                newdraw = new RectangleObj((RectangleObj)draw);
            }
            else if (ShapeTypeIndexes.Ploy == draw.m_ShapeType)
            {
                newdraw = new PolygonObj(((PolygonObj)draw));
            }
            m_vList.Add(newdraw);
            return 0;
        }

        //获取区域目标
        public DrawBase GetWholeObject(int x, int y)
        {
            foreach (DrawBase draw in m_vList)
            {
                if (draw.WholeObject(x, y))
                {
                    return draw;
                }
            }

            return null;
        }

        //获取选中顶点的目标
        public DrawBase GetAdjustPoint(int x, int y)
        {
            foreach (DrawBase draw in m_vList)
            {
                if (-1 != draw.AdjustPoint(x, y))
                {
                    return draw;
                }
            }

            return null;
        }


        public void Draw(Graphics graphics)
        {
            if (this.m_CurrentCover != null)
            {
                m_CurrentCover.m_bSolid = false;
                if (0 == m_selType)   //靠近目标时整个框加粗
                {
                    m_CurrentCover.m_bSolid = true;
                    //m_CurrentOri.Draw(graphics);
                }
                else if (1 == m_selType)  //靠近修改点时加粗该点
                {

                }
            }

            foreach (DrawBase draw in m_vList)
            {
                draw.Draw(graphics);
            }
        }

        //通过点位置获取目标 （点在目标内 或者 在目标顶点附近） 
        public void getObj(Point pt)
        {
            m_selType = -1;
            if (null != m_CurrentCover)
            {
                m_CurrentCover.m_bSolid = false;
            }

            //先判断是否能通过顶点附近拾取目标
            this.m_CurrentCover = this.GetAdjustPoint(pt.X, pt.Y);
            if (this.m_CurrentCover != null)
            {
                m_selType = 1;
            }
            else
            {
                //再判断点是否能通过点在目标内部拾取目标
                this.m_CurrentCover = this.GetWholeObject(pt.X, pt.Y);
                if (this.m_CurrentCover != null)
                {
                    m_selType = 0;
                }
            }

            //当前拾取目标获取成功
            if (this.m_CurrentCover != null)
            {
                //重新建立一个当前目标的备份
                if (ShapeTypeIndexes.Rect == m_CurrentCover.m_ShapeType)
                {
                    m_CurrentOri = new RectangleObj((RectangleObj)m_CurrentCover);
                }
                else if (ShapeTypeIndexes.Ploy == m_CurrentCover.m_ShapeType)
                {
                    m_CurrentOri = new PolygonObj((PolygonObj)m_CurrentCover);
                }

                //记录选取目标起始位置
                this.m_RejustStart = pt;
            }
        }

        //移动目标
        public void moveObj(Point pt)
        {
            if (null != m_CurrentCover)
            {
                if (ShapeTypeIndexes.Rect == m_CurrentOri.m_ShapeType)
                {
                    Point pt1 = ((RectangleObj)m_CurrentOri).m_Start;
                    Point pt2 = ((RectangleObj)m_CurrentOri).m_End;
                    pt1.X += pt.X - m_RejustStart.X;
                    pt1.Y += pt.Y - m_RejustStart.Y;
                    pt2.X += pt.X - m_RejustStart.X;
                    pt2.Y += pt.Y - m_RejustStart.Y;
                    pt1 = m_GIChange.fixPointInRegion(pt1);
                    pt2 = m_GIChange.fixPointInRegion(pt2);
                    ((RectangleObj)m_CurrentCover).Move(pt1, pt2);
                }
                else if (ShapeTypeIndexes.Ploy == m_CurrentOri.m_ShapeType)
                {
                    Point[] vPt = ((PolygonObj)m_CurrentOri).vPoint.ToArray();
                    for (int i = 0; i < vPt.Count(); i++)
                    {
                        vPt[i].X += pt.X - m_RejustStart.X;
                        vPt[i].Y += pt.Y - m_RejustStart.Y;
                        vPt[i] = m_GIChange.fixPointInRegion(vPt[i]);
                    }
                    ((PolygonObj)m_CurrentCover).Move(vPt);
                }
            }
        }

        //拖动目标顶点
        public void reJustObj(Point pt)
        {
            if (null != m_CurrentCover)
            {
                if (ShapeTypeIndexes.Rect == m_CurrentOri.m_ShapeType)
                {
                    Rectangle bound = ((RectangleObj)m_CurrentOri).GetBound();
                    Point tl = Point.Empty;
                    Point rb = Point.Empty;
                    tl = bound.Location;
                    rb.X = bound.Right;
                    rb.Y = bound.Bottom;
                    if (0 == m_CurrentOri.m_nSelPtIndex)
                    { //左上角
                        tl.X = tl.X + pt.X - m_RejustStart.X;
                        tl.Y = tl.Y + pt.Y - m_RejustStart.Y;
                        tl = m_GIChange.fixPointInRegion(tl);
                    }
                    else //右下角
                    {
                        rb.X = rb.X + pt.X - m_RejustStart.X;
                        rb.Y = rb.Y + pt.Y - m_RejustStart.Y;
                        rb = m_GIChange.fixPointInRegion(rb);
                    }

                    ((RectangleObj)m_CurrentCover).Move(tl, rb);
                }
                else if (ShapeTypeIndexes.Ploy == m_CurrentOri.m_ShapeType)
                {

                    if (-1 != m_CurrentOri.m_nSelPtIndex)
                    {
                        Point[] vPt = ((PolygonObj)m_CurrentOri).vPoint.ToArray();
                        vPt[m_CurrentOri.m_nSelPtIndex].X += pt.X - m_RejustStart.X;
                        vPt[m_CurrentOri.m_nSelPtIndex].Y += pt.Y - m_RejustStart.Y;
                        m_GIChange.fixPointInRegion(vPt[m_CurrentOri.m_nSelPtIndex]);
                        ((PolygonObj)m_CurrentCover).Move(vPt);
                    }
                }
            }
        }
    }
}
