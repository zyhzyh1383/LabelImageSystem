using System;

namespace Zach.PaddleDetection
{
    /// <summary>
    /// 识别配置
    /// </summary>
    public class PaddleModelConfig
    {
        /// <summary>
        /// 安全帽模型路径
        /// </summary>
        public static readonly string Hat = AppContext.BaseDirectory + "\\model\\faster_rcnn_r50_vd_fpn_ssld_2x_coco_Hat";

        /// <summary>
        /// 吸烟模型路径
        /// </summary>
        public static readonly string Smoking = AppContext.BaseDirectory + "\\model\\faster_rcnn_r50_vd_fpn_ssld_2x_coco_Smoking";

        /// <summary>
        /// 多标签模型路径
        /// </summary>
        public static readonly string MultiLabel = AppContext.BaseDirectory + "\\model\\faster_rcnn_r50_vd_fpn_ssld_2x_coco_MultiLabel";

    }

}