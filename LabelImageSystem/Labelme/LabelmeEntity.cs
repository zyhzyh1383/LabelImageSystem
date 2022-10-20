using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelImageSystem.Labelme
{
    public class Flag
    {
    }

    public class Shape
    {
        /// <summary>
        /// 
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<List<double>> points { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string group_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shape_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Flag flags { get; set; }

        public int x { get; set; }

        public int y { get; set; }

        public int width { get; set; }

        public int height { get; set; }
    }

    public class LabelmeEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Flag flags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Shape> shapes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imagePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imageData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int imageHeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int imageWidth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string file_attributes { get; set; }
    }

}
