using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Drawing;
using LabelImageSystem.Shapes;
using System.Xml.Linq;
using Zach.Util;

namespace LabelImageSystem.Json
{
    public class JsonInterface
    {
        public RootObject m_vObj;   //标定信息结构体
        public bool isError = false;

        public DrawShapes Load(string fileName)
        {

            if (!File.Exists(fileName))
            {
                return null;
            }

            StreamReader sr = new StreamReader(fileName, System.Text.Encoding.UTF8);
            string jsonStr = sr.ReadToEnd();
            sr.Close();
            if (string.IsNullOrEmpty(jsonStr))
                return null;

            m_vObj = new RootObject();
            JObject jsonObj = null;
            jsonObj = JObject.Parse(jsonStr);
            if (jsonObj.Count == 1)
                this.isError = true;
            else
            {
                m_vObj.filename = (string)jsonObj["filename"];
                m_vObj.size = (string)jsonObj["size"];
                m_vObj.file_attributes = (string)jsonObj["file_attributes"];
                m_vObj.regions = new List<Regions>();

                JArray jlist = JArray.Parse(jsonObj["regions"].ToString());

                for (int i = 0; i < jlist.Count; ++i)  //遍历JArray  
                {
                    Regions region = new Regions();
                    JObject tempo = JObject.Parse(jlist[i].ToString());
                    region.region_attributes = new Region_attributes();
                    region.shape_attributes = new Shape_attributes();
                    region.region_attributes.type = (string)tempo["region_attributes"]["type"];
                    region.shape_attributes.name = (string)tempo["shape_attributes"]["name"];
                    if ("polygon" == region.shape_attributes.name)
                    {
                        region.shape_attributes.all_points_x = new List<int>();
                        region.shape_attributes.all_points_y = new List<int>();
                        JArray jPtlist = JArray.Parse(tempo["shape_attributes"]["all_points_x"].ToString());
                        for (int npt = 0; npt < jPtlist.Count; ++npt)
                        {
                            region.shape_attributes.all_points_x.Add((int)jPtlist[npt]);
                        }

                        jPtlist = JArray.Parse(tempo["shape_attributes"]["all_points_y"].ToString());
                        for (int npt = 0; npt < jPtlist.Count; ++npt)
                        {
                            region.shape_attributes.all_points_y.Add((int)jPtlist[npt]);
                        }
                    }
                    else if ("rect" == region.shape_attributes.name)
                    {
                        region.shape_attributes.x = (int)tempo["shape_attributes"]["x"];
                        region.shape_attributes.y = (int)tempo["shape_attributes"]["y"];
                        region.shape_attributes.width = (int)tempo["shape_attributes"]["width"];
                        region.shape_attributes.height = (int)tempo["shape_attributes"]["height"];
                    }

                    m_vObj.regions.Add(region);
                }
            }

            return ChangeRootObjToDrawShapes();
        }

        //保存形状列表掉配置文件
        public void Save(string filename, DrawShapes shapes, string fileSavename, int filesize)
        {
            ChangeDrawShapesToRootObj(shapes, filename, filesize);
            MakeUpJosonFile2(fileSavename);
        }

        public JObject MakeUpJosonFile(string filename)
        {
            if (0 == m_vObj.regions.Count)
            {
                if (System.IO.File.Exists(Path.GetFullPath(filename)))
                {
                    File.Delete(Path.GetFullPath(filename));
                }
                return null;
            }

            var rootObj = new JObject { { "filename", m_vObj.filename }, { "size", m_vObj.size.ToString() } };
            rootObj.Add("file_attributes", "@Qingshui Cheng@Labixiaoxindian @ All Right Reserved!");

            var regArray = new JArray();
            for (int i = 0; i < m_vObj.regions.Count; i++)
            {
                var region = new JObject();
                var regattri = new JObject { { "type", m_vObj.regions[i].region_attributes.type } };
                region.Add("region_attributes", regattri);

                var shape = new JObject { { "name", m_vObj.regions[i].shape_attributes.name } };
                if ("polygon" == m_vObj.regions[i].shape_attributes.name)
                {
                    var arrX = new JArray(m_vObj.regions[i].shape_attributes.all_points_x.ToList());
                    var arrY = new JArray(m_vObj.regions[i].shape_attributes.all_points_y.ToList());
                    shape.Add("all_points_x", arrX);
                    shape.Add("all_points_y", arrY);
                }
                else if ("rect" == m_vObj.regions[i].shape_attributes.name)
                {
                    shape.Add("x", m_vObj.regions[i].shape_attributes.x);
                    shape.Add("y", m_vObj.regions[i].shape_attributes.y);
                    shape.Add("width", m_vObj.regions[i].shape_attributes.width);
                    shape.Add("height", m_vObj.regions[i].shape_attributes.height);
                }
                region.Add("shape_attributes", shape);
                regArray.Add(region);
            }

            rootObj.Add("regions", regArray);

            try
            {
                FileStream nFile = new FileStream(filename, FileMode.Create);
                StreamWriter writer = new StreamWriter(nFile, Encoding.UTF8);
                writer.Write(rootObj.ToString());
                writer.Close();
                nFile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rootObj;
        }

        public DrawShapes ChangeRootObjToDrawShapes()
        {
            DrawShapes drawShapes = new DrawShapes();
            for (int i = 0; i < m_vObj.regions.Count; i++)
            {
                if ("polygon" == m_vObj.regions[i].shape_attributes.name)
                {
                    PolygonObj shape = new PolygonObj();
                    shape.m_objName = m_vObj.regions[i].region_attributes.type;
                    for (int m = 0; m < m_vObj.regions[i].shape_attributes.all_points_x.Count; m++)
                    {
                        Point pt = new Point(m_vObj.regions[i].shape_attributes.all_points_x[m], m_vObj.regions[i].shape_attributes.all_points_y[m]);
                        shape.vPtOriImage.Add(pt);
                    }
                    drawShapes.m_vList.Add(shape);
                }
                else if ("rect" == m_vObj.regions[i].shape_attributes.name)
                {
                    RectangleObj shape = new RectangleObj();
                    shape.m_objName = m_vObj.regions[i].region_attributes.type;
                    shape.m_ptStartOri.X = m_vObj.regions[i].shape_attributes.x;
                    shape.m_ptStartOri.Y = m_vObj.regions[i].shape_attributes.y;
                    shape.m_ptEndOri.X = m_vObj.regions[i].shape_attributes.x + m_vObj.regions[i].shape_attributes.width;
                    shape.m_ptEndOri.Y = m_vObj.regions[i].shape_attributes.y + m_vObj.regions[i].shape_attributes.height;
                    drawShapes.m_vList.Add(shape);
                }
            }

            return drawShapes;
        }

        public void ChangeDrawShapesToRootObj(DrawShapes drawShapes, string filename, int filesize)
        {
            System.IO.FileInfo fileInfo = null;
            try
            {
                fileInfo = new System.IO.FileInfo(filename);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // 如果文件存在
            if (fileInfo != null && fileInfo.Exists)
            {
                filesize = (int)fileInfo.Length;
            }


            m_vObj = new RootObject();
            m_vObj.filename = filename.Substring(filename.LastIndexOf('\\') + 1);
            m_vObj.size = filesize.ToString();
            m_vObj.file_attributes = "@chengqingshui@citic";
            m_vObj.regions = new List<Regions>();

            for (int i = 0; i < drawShapes.m_vList.Count; ++i)  //遍历JArray  
            {
                Regions region = new Regions();
                region.region_attributes = new Region_attributes();
                region.shape_attributes = new Shape_attributes();
                region.region_attributes.type = drawShapes.m_vList[i].m_objName;

                if (ShapeTypeIndexes.Rect == drawShapes.m_vList[i].m_ShapeType)
                {
                    region.shape_attributes.name = "rect";
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
                m_vObj.regions.Add(region);
            }
        }

        public void MakeUpJosonFile2(string filename)
        {
            if (0 == m_vObj.regions.Count)
            {
                if (System.IO.File.Exists(Path.GetFullPath(filename)))
                {
                    File.Delete(Path.GetFullPath(filename));
                }
                //return null;
            }

            var labelmeEntity = new Labelme.LabelmeEntity();
            labelmeEntity.version = "5.0.1";
            labelmeEntity.flags = new Labelme.Flag();
            labelmeEntity.imagePath = m_vObj.filename;
            labelmeEntity.imageData = Base64ConvenrtHelper.FileToBase64(Path.GetFullPath(m_vObj.filename));
            labelmeEntity.imageHeight = 0;
            labelmeEntity.imageWidth = 0;
            labelmeEntity.shapes = new List<Labelme.Shape>();
            var labelTypes = m_vObj.regions.Select(r => r.region_attributes.type).Distinct().ToList();
            foreach (string labelType in labelTypes)
            {
                var shape = new Labelme.Shape
                {
                    flags = new Labelme.Flag(),
                    group_id = null,
                    label = labelType,
                    shape_type = "rectangle"
                };
                shape.points = new List<List<double>>();
                var tempList = m_vObj.regions.FindAll(r => r.region_attributes.type == labelType);
                foreach (var temp in tempList)
                {
                    var pointList = new List<double>();
                    pointList.Add(temp.shape_attributes.x);
                    pointList.Add(temp.shape_attributes.y);
                    shape.points.Add(pointList);
                }
                labelmeEntity.shapes.Add(shape);
            }
            try
            {
                FileStream nFile = new FileStream(filename, FileMode.Create);
                StreamWriter writer = new StreamWriter(nFile, Encoding.UTF8);
                writer.Write(labelmeEntity.ToJson());
                writer.Close();
                nFile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //for (int i = 0; i < m_vObj.regions.Count; i++)
            //{
            //    var shapes = new Labelme.Shapes
            //    {
            //        flags = null,
            //        group_id = null,
            //        label = m_vObj.regions[i].region_attributes.type,
            //        shape_type = m_vObj.regions[i].shape_attributes.name == "rect" ? "rectangle" : "rectangle"
            //    };
            //    shapes.points = new List<List<double>>();

            //    //    var region = new JObject();
            //    //    var regattri = new JObject { { "type", m_vObj.regions[i].region_attributes.type } };
            //    //    region.Add("region_attributes", regattri);

            //    //    var shape = new JObject { { "name", m_vObj.regions[i].shape_attributes.name } };
            //    //    if ("polygon" == m_vObj.regions[i].shape_attributes.name)
            //    //    {
            //    //        var arrX = new JArray(m_vObj.regions[i].shape_attributes.all_points_x.ToList());
            //    //        var arrY = new JArray(m_vObj.regions[i].shape_attributes.all_points_y.ToList());
            //    //        shape.Add("all_points_x", arrX);
            //    //        shape.Add("all_points_y", arrY);
            //    //    }
            //    //    else if ("rect" == m_vObj.regions[i].shape_attributes.name)
            //    //    {
            //    //        shape.Add("x", m_vObj.regions[i].shape_attributes.x);
            //    //        shape.Add("y", m_vObj.regions[i].shape_attributes.y);
            //    //        shape.Add("width", m_vObj.regions[i].shape_attributes.width);
            //    //        shape.Add("height", m_vObj.regions[i].shape_attributes.height);
            //    //    }
            //    //    region.Add("shape_attributes", shape);
            //    //    regArray.Add(region);
            //    //}

            //    //rootObj.Add("regions", regArray);

            //    //try
            //    //{
            //    //    FileStream nFile = new FileStream(filename, FileMode.Create);
            //    //    StreamWriter writer = new StreamWriter(nFile, Encoding.UTF8);
            //    //    writer.Write(rootObj.ToString());
            //    //    writer.Close();
            //    //    nFile.Close();
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    throw ex;
            //    //}

            //    //return rootObj;
            //}
        }

    }
}
