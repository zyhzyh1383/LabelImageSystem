using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using LabelImageSystem.Shapes;
using Zach.Util;
using Zach.Loger;
using LabelImageSystem.Labelme;

namespace LabelImageSystem.Json
{
    public class JsonInterface
    {
        /// <summary>
        /// 标定信息结构体
        /// </summary>
        public RootObject gRootObject;

        /// <summary>
        /// 加载json标注文件
        /// </summary>
        /// <param name="jsonFileName"></param>
        /// <returns></returns>
        public DrawShapes LoadJsonFileName(string jsonFileName)
        {
            try
            {
                if (!DirFileHelper.IsExistFile(jsonFileName)) return null;
                var jsonStr = DirFileHelper.ReadAllText(jsonFileName);
                if (string.IsNullOrEmpty(jsonStr)) return null;
                gRootObject = new RootObject();
                LabelmeEntity labelmeEntity = jsonStr.ToObject<LabelmeEntity>();
                gRootObject.filename = labelmeEntity.imagePath;
                gRootObject.size = labelmeEntity.size;
                gRootObject.file_attributes = labelmeEntity.file_attributes ?? ConfigContext.file_attributes;
                gRootObject.regions = new List<Regions>();
                foreach (var shape in labelmeEntity.shapes)
                {
                    Regions region = new Regions();
                    region.region_attributes = new Region_attributes();
                    region.shape_attributes = new Shape_attributes();
                    region.region_attributes.type = shape.label;
                    region.shape_attributes.name = shape.shape_type;
                    if ("polygon" == region.shape_attributes.name)//多边形区域
                    {
                        region.shape_attributes.all_points_x = new List<int>();
                        region.shape_attributes.all_points_y = new List<int>();
                        foreach (var p in shape.points)
                        {
                            region.shape_attributes.all_points_x.Add(p[0].ToInt());
                            region.shape_attributes.all_points_y.Add(p[1].ToInt());
                        }
                    }
                    else if ("rectangle" == region.shape_attributes.name)//矩形区域
                    {
                        region.shape_attributes.x = shape.points[0][0].ToInt();
                        region.shape_attributes.y = shape.points[0][1].ToInt();
                        region.shape_attributes.width = shape.points[1][0].ToInt() - shape.points[0][0].ToInt();
                        region.shape_attributes.height = shape.points[1][1].ToInt() - shape.points[0][1].ToInt();
                    }
                    gRootObject.regions.Add(region);
                }
                return RootObjectConvertToDrawShapes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RootObject转换成DrawShapes
        /// </summary>
        /// <returns></returns>
        public DrawShapes RootObjectConvertToDrawShapes()
        {
            DrawShapes drawShapes = new DrawShapes();
            for (int i = 0; i < gRootObject.regions.Count; i++)
            {
                if ("polygon" == gRootObject.regions[i].shape_attributes.name)
                {
                    PolygonObj shape = new PolygonObj();
                    shape.m_objName = gRootObject.regions[i].region_attributes.type;
                    for (int m = 0; m < gRootObject.regions[i].shape_attributes.all_points_x.Count; m++)
                    {
                        Point pt = new Point(gRootObject.regions[i].shape_attributes.all_points_x[m], gRootObject.regions[i].shape_attributes.all_points_y[m]);
                        shape.vPtOriImage.Add(pt);
                    }
                    drawShapes.m_vList.Add(shape);
                }
                else if ("rectangle" == gRootObject.regions[i].shape_attributes.name)
                {
                    RectangleObj shape = new RectangleObj();
                    shape.m_objName = gRootObject.regions[i].region_attributes.type;
                    shape.m_ptStartOri.X = gRootObject.regions[i].shape_attributes.x;
                    shape.m_ptStartOri.Y = gRootObject.regions[i].shape_attributes.y;
                    shape.m_ptEndOri.X = gRootObject.regions[i].shape_attributes.x + gRootObject.regions[i].shape_attributes.width;
                    shape.m_ptEndOri.Y = gRootObject.regions[i].shape_attributes.y + gRootObject.regions[i].shape_attributes.height;
                    drawShapes.m_vList.Add(shape);
                }
            }
            return drawShapes;
        }

        /// <summary>
        /// 创建标定信息结构体
        /// </summary>
        /// <param name="drawShapes"></param>
        /// <param name="imageFilePath"></param>
        /// <param name="imageFileSize"></param>
        public void CreateRootObj(DrawShapes drawShapes, string imageFilePath, int imageFileSize)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(imageFilePath);
            if (fileInfo != null && fileInfo.Exists) imageFileSize = (int)fileInfo.Length;
            gRootObject = new RootObject();
            gRootObject.filename = imageFilePath.Substring(imageFilePath.LastIndexOf('\\') + 1);
            gRootObject.size = imageFileSize.ToString();
            gRootObject.file_attributes = ConfigContext.file_attributes;
            gRootObject.regions = new List<Regions>();
            for (int i = 0; i < drawShapes.m_vList.Count; ++i)
            {
                Regions region = new Regions();
                region.region_attributes = new Region_attributes();
                region.shape_attributes = new Shape_attributes();
                region.region_attributes.type = drawShapes.m_vList[i].m_objName;
                if (ShapeTypeIndexes.Rect == drawShapes.m_vList[i].m_ShapeType)
                {
                    region.shape_attributes.name = "rectangle";
                    RectangleObj rect = (RectangleObj)drawShapes.m_vList[i];
                    region.shape_attributes.x = rect.m_ptStartOri.X < rect.m_ptEndOri.X ? rect.m_ptStartOri.X : rect.m_ptEndOri.X;
                    region.shape_attributes.y = rect.m_ptStartOri.Y < rect.m_ptEndOri.Y ? rect.m_ptStartOri.Y : rect.m_ptEndOri.Y;
                    region.shape_attributes.width = Math.Abs(rect.m_ptStartOri.X - rect.m_ptEndOri.X);
                    region.shape_attributes.height = Math.Abs(rect.m_ptStartOri.Y - rect.m_ptEndOri.Y);
                }
                else if (ShapeTypeIndexes.Ploy == drawShapes.m_vList[i].m_ShapeType)
                {
                    region.shape_attributes.name = "polygon";
                    region.shape_attributes.all_points_x = new List<int>();
                    region.shape_attributes.all_points_y = new List<int>();
                    PolygonObj poly = (PolygonObj)drawShapes.m_vList[i];
                    for (int m = 0; m < poly.vPoint.Count; m++)
                    {
                        region.shape_attributes.all_points_x.Add(poly.vPtOriImage[m].X);
                        region.shape_attributes.all_points_y.Add(poly.vPtOriImage[m].Y);
                    }
                }
                gRootObject.regions.Add(region);
            }
        }

        /// <summary>
        /// 创建json文件
        /// </summary>
        /// <param name="imageFilePath">图片文件路径</param>
        /// <param name="jsonFileSaveName">json文件保存路径</param>
        public void CreateJsonFile(string imageFilePath, string jsonFileSaveName)
        {
            if (gRootObject.regions.Count == 0)
            {
                if (DirFileHelper.IsExistFile(Path.GetFullPath(jsonFileSaveName)))
                {
                    File.Delete(Path.GetFullPath(jsonFileSaveName));
                }
            }
            try
            {
                LabelmeEntity labelmeEntity = GetLabelmeEntity(imageFilePath, gRootObject);
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
                File.WriteAllText(jsonFileSaveName, labelmeEntity.ToJson(), utf8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取LabelmeEntity
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="rootObject"></param>
        /// <returns></returns>
        private LabelmeEntity GetLabelmeEntity(string imageFilePath, RootObject rootObject)
        {
            var labelmeEntity = new Labelme.LabelmeEntity();
            labelmeEntity.version = ConfigContext.LabelmeVersion;
            labelmeEntity.flags = new Labelme.Flag();
            labelmeEntity.imagePath = rootObject.filename;
            labelmeEntity.size = rootObject.size;
            labelmeEntity.file_attributes = rootObject.file_attributes;
            var guid = Guid.NewGuid().ToString();
            var tempFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}temp\\{guid}{DirFileHelper.GetExtension(imageFilePath)}";
            File.Copy(imageFilePath, tempFilePath);
            labelmeEntity.imageData = Base64ConvenrtHelper.FileToBase64(Path.GetFullPath(tempFilePath));
            File.Delete(tempFilePath);
            var whList = ImageHelper.GetImageSize(Path.GetFullPath(imageFilePath));
            if (whList.Count == 2)
            {
                labelmeEntity.imageWidth = whList[0];
                labelmeEntity.imageHeight = whList[1];
            }
            labelmeEntity.shapes = new List<Labelme.Shape>();
            for (int i = 0; i < rootObject.regions.Count; i++)
            {
                var shape = new Labelme.Shape
                {
                    label = rootObject.regions[i].region_attributes.type,
                    group_id = null,
                    shape_type = rootObject.regions[i].shape_attributes.name,
                    flags = new Labelme.Flag()
                };
                shape.points = new List<List<double>>();
                if ("polygon" == rootObject.regions[i].shape_attributes.name)
                {
                    var tempAllPointsX = rootObject.regions[i].shape_attributes.all_points_x.ToArray();
                    var tempAllPointsY = rootObject.regions[i].shape_attributes.all_points_y.ToArray();
                    for (int j = 0; j < tempAllPointsX.Length; j++)
                    {
                        var polygonPoint = new List<double>();
                        polygonPoint.Add(tempAllPointsX[j]);
                        polygonPoint.Add(tempAllPointsY[j]);
                        shape.points.Add(polygonPoint);
                    }
                }
                else if ("rectangle" == rootObject.regions[i].shape_attributes.name)
                {
                    var rectanglePoint1 = new List<double>();
                    rectanglePoint1.Add(rootObject.regions[i].shape_attributes.x);
                    rectanglePoint1.Add(rootObject.regions[i].shape_attributes.y);
                    shape.points.Add(rectanglePoint1);
                    var rectanglePoint2 = new List<double>();
                    rectanglePoint2.Add(rootObject.regions[i].shape_attributes.x + rootObject.regions[i].shape_attributes.width);
                    rectanglePoint2.Add(rootObject.regions[i].shape_attributes.y + rootObject.regions[i].shape_attributes.height);
                    shape.points.Add(rectanglePoint2);
                    shape.x = rootObject.regions[i].shape_attributes.x;
                    shape.y = rootObject.regions[i].shape_attributes.y;
                    shape.width = rootObject.regions[i].shape_attributes.width;
                    shape.height = rootObject.regions[i].shape_attributes.height;
                }
                labelmeEntity.shapes.Add(shape);
            }
            return labelmeEntity;
        }

        /// <summary>
        /// 保存形状列表掉配置文件
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <param name="drawShapes"></param>
        /// <param name="jsonFileSaveName"></param>
        /// <param name="filesize"></param>
        public void SaveShapesAndJsonFile(string imageFilePath, DrawShapes drawShapes, string jsonFileSaveName, int filesize)
        {
            CreateRootObj(drawShapes, imageFilePath, filesize);
            CreateJsonFile(imageFilePath, jsonFileSaveName);
        }
    }
}
