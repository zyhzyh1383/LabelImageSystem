using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelImageSystem
{
    public static class ConfigContext
    {
        public static int shapeType;

        public static string LabelmeVersion;
        public static string file_attributes { get; set; }
        public static string coco { get; set; }
        public static string train { get; set; }
    }

    public class Config
    {
        public int shapeType { get; set; }

        public string LabelmeVersion { get; set; }

        public string file_attributes { get; set; }

        public string coco { get; set; }

        public string train { get; set; }
    }
}
