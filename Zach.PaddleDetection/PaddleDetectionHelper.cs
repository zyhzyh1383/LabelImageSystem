using OpenCvSharp;
using OpenCvSharp.Extensions;
using Sdcb.PaddleDetection;
using Sdcb.PaddleInference;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace Zach.PaddleDetection
{

    /// <summary>
    /// 模型类型
    /// </summary>
    public enum ModelType
    {
        /// <summary>
        /// 安全帽
        /// </summary>
        Hat = 0,
        /// <summary>
        /// 抽烟
        /// </summary>
        Smoking = 1,
        /// <summary>
        /// 多标签
        /// </summary>
        MultiLabel = 2,
        /// <summary>
        /// 未知
        /// </summary>
        Unknown=3
    }

    /// <summary>
    /// Paddle识别帮助类
    /// </summary>
    public class PaddleDetectionHelper
    {
        /// <summary>
        /// 获取数据集的识别结果
        /// </summary>
        /// <param name="imageName">图片的全路径</param>
        /// <param name="modelDirectory">模型的目录 如果为空 则根据模型类别取默认值</param>
        /// <param name="modelType">模型类别</param>
        /// <param name="confidence">可信度</param>
        /// <param name="useGpu">是否使用gpu 默认为false</param>
        /// <param name="useMkldnn">是否使用mkldnn 默认为false</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static async Task<PaddleDetectionResult> GetDetectionResultListAsync(string imageName = "", string modelDirectory = "",
            ModelType modelType = ModelType.Hat, float confidence = 0.8f, bool useGpu = false, bool useMkldnn = false)
        {
            modelDirectory = InitParamters(imageName, modelDirectory, modelType, useGpu, useMkldnn);

            return await Task.Run(() =>
            {
                return PaddleDetection(imageName, modelDirectory, confidence);
            });
        }

        /// <summary>
        /// 获取数据集的识别结果
        /// </summary>
        /// <param name="imageName">图片的全路径</param>
        /// <param name="modelDirectory">模型的目录 如果为空 则根据模型类别取默认值</param>
        /// <param name="modelType">模型类别</param>
        /// <param name="confidence">可信度</param>
        /// <param name="useGpu">是否使用gpu 默认为false</param>
        /// <param name="useMkldnn">是否使用mkldnn 默认为false</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static PaddleDetectionResult GetDetectionResultList(string imageName = "", string modelDirectory = "",
            ModelType modelType = ModelType.Hat, float confidence = 0.8f, bool useGpu = false, bool useMkldnn = false)
        {
            modelDirectory = InitParamters(imageName, modelDirectory, modelType, useGpu, useMkldnn);
            PaddleDetectionResult pddResult = new PaddleDetectionResult();
            return PaddleDetection(imageName, modelDirectory, confidence);
        }

        /// <summary>
        /// 调用模型并识别数据集
        /// </summary>
        /// <param name="imageName">图片的全路径</param>
        /// <param name="modelDirectory">模型的目录 如果为空 则根据模型类别取默认值</param>
        /// <param name="confidence">可信度</param>
        /// <returns></returns>
        private static PaddleDetectionResult PaddleDetection(string imageName, string modelDirectory, float confidence)
        {
            PaddleDetectionResult pddResult = new PaddleDetectionResult();
            using (PaddleDetector detector = new PaddleDetector(modelDirectory, Path.Combine(modelDirectory, "infer_cfg.yml")))
            {
                try
                {
                    string path = imageName;
                    Mat tempMat = new Mat(path, ImreadModes.Color);
                    var tempResult = detector.Run(tempMat);
                    pddResult.DetectionResult = tempResult.ToList();
                    pddResult.DetectionImage = BitmapConverter.ToBitmap(
                        PaddleDetector.Visualize(
                          tempMat
                        , tempResult.Where(c => c.Confidence > confidence)
                        , detector.Config.LabelList.Length)) as Image;
                }
                catch (Exception ex)
                {
                    pddResult = null;
                }
                return pddResult;
            }
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="modelDirectory"></param>
        /// <param name="modelType"></param>
        /// <param name="useGpu"></param>
        /// <param name="useMkldnn"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        private static string InitParamters(string imageName, string modelDirectory, ModelType modelType, bool useGpu, bool useMkldnn)
        {
            if (string.IsNullOrEmpty(imageName))
                throw new ArgumentNullException("imageName");
            if (string.IsNullOrEmpty(modelDirectory))
            {
                switch (modelType)
                {
                    case ModelType.Hat:
                        modelDirectory = PaddleModelConfig.Hat;
                        break;
                    case ModelType.Smoking:
                        modelDirectory = PaddleModelConfig.Smoking;
                        break;
                    case ModelType.MultiLabel:
                        modelDirectory = PaddleModelConfig.MultiLabel;
                        break;
                    default:
                        //throw new ArgumentException("未知的ModelType");
                        break;
                }
            }
            PaddleConfig.Defaults.UseGpu = useGpu;
            PaddleConfig.Defaults.UseMkldnn = useMkldnn;
            return modelDirectory;
        }
    }
}