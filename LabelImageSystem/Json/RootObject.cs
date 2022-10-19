using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelImageSystem.Json
{
    // 根据 VIA标注信息生成单张图片标注信息结构

    public class Shape_attributes
    {
        public string name { get; set; }
        public List<int> all_points_x { get; set; }
        public List<int> all_points_y { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }


    public class Region_attributes
    {
        public string type { get; set; }
    }

    public class Regions
    {
        public Shape_attributes shape_attributes { get; set; }
        public Region_attributes region_attributes { get; set; }
    }



    public class RootObject
    {
        public string filename { get; set; }
        public string size { get; set; }
        public List<Regions> regions { get; set; }
        public string file_attributes { get; set; }
    }

}
