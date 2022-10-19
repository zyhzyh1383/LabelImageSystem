using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LabelImageSystem.Shapes
{

    //形状类型定义
    public class ShapeTypeIndexes
    {
        public const int Rect = 0;      //矩形
        public const int Ploy = 1;      //多边形
    }


    /**
     * 绘制图形基础类抽象
     */
    public abstract class DrawBase
    {
        public static Color m_NormalColor = Color.Green;      //正常的颜色
        public static Color m_SelectedColor = Color.Red;      //选中后颜色
        public static int m_HalfGrab = 4;  //凸出显示时点或线的宽度     
        public int m_ShapeType;            //形状类型  参考ShapeTypeIndexes
        public bool m_bIsDrawing;          //是否正在绘制标志
        public int m_nSelPtIndex = -1;     //选中的点索引
        public bool m_bSolid;              //是否粗体显示
        public bool m_bSelect = false;     //是否选中当前目标
        public string m_objName;    //形状目标名称
        public string m_objScript;    //形状目标描述

        //绘图
        public abstract void Draw(Graphics g);

        //是否选中整个区域
        public abstract bool WholeObject(int x, int y);

        //是否选中调整点
        public abstract int AdjustPoint(int x, int y);

        //鼠标按下
        public abstract void MouseDown(Point pt);

        //鼠标按下移动
        public abstract void MouseMove(Point pt);

        //鼠标弹起
        public abstract int MouseUp(Point pt);

        //检查合法性
        public abstract bool CheckValid();

        //绘图到图像坐标变换
        public abstract void ptGtoImage(GraphicAndImageChange change);

        //图像到绘图坐标变换
        public abstract void ptItoGraphic(GraphicAndImageChange change);
    }
}
