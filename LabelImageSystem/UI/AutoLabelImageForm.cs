using LabelImageSystem.Json;
using LabelImageSystem.Labelme;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zach.Loger;
using Zach.PaddleDetection;
using Zach.Util;
using Zach.Util.WinForm;

namespace LabelImageSystem
{
    public partial class AutoLabelImageForm : Form
    {
        private Log _log = LogFactory.GetLogger(typeof(AutoLabelImageForm));

        private readonly object _lock = new object();

        private int count = 0;

        MyProgressBar PBress;

        private bool runFlag = false;

        public AutoLabelImageForm()
        {
            InitializeComponent();
            PBress = new MyProgressBar()
            {
                Location = new Point(102, 109),
                Size = new Size(585, 30),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            this.Controls.Add(PBress);
        }

        private void btnModelDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择模型的路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtModelDir.Text = dialog.SelectedPath;
            }
        }

        private void btnImageDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择数据集的路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtImageDir.Text = dialog.SelectedPath;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var imageFiles = DirFileHelper.GetFileNames(txtImageDir.Text, "*.jpg", false);
            if (imageFiles.Length == 0)
            {
                MessageShow.Show("指定数据集目录下没有jpg格式的图片");
                return;
            }
            count = 0;
            PBress.Maximum = imageFiles.Length;
            var jsonFiles = DirFileHelper.GetFileNames(txtImageDir.Text, "*.json", false).ToList();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            runFlag = true;
            TaskHelper.RunTask(imageFiles.ToList(), (imgFile) =>
            {
                if (!runFlag)
                {
                    return;
                }
                if (jsonFiles.FindAll(f => DirFileHelper.GetDirectoryName(f) == DirFileHelper.GetDirectoryName(imgFile)
                && DirFileHelper.GetFileNameNoExtension(f) == DirFileHelper.GetFileNameNoExtension(imgFile)).Count > 0)
                {
                    count++;
                    PBress.Value = PBress.Value % 100 + 1;
                }
                else
                {
                    GetLabelmeEntityAndSaveJsonFile(imgFile);
                }
            }, 2, false);
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            if (PBress.Maximum!=PBress.Value)
            {
                if (MessageShow.Confirm("识别未完成，确认提前结束?"))
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    runFlag = false;
                }
            }
        }

        /// <summary>
        /// 获取labelme格式并保存成json文件
        /// </summary>
        /// <param name="imgFile"></param>
        private void GetLabelmeEntityAndSaveJsonFile(string imgFile)
        {
            try
            {
                var modelDir = txtModelDir.Text.Trim();
                var result = PaddleDetectionHelper.GetDetectionResultList(imgFile, modelDir, ModelType.Unknown, Convert.ToSingle(0.8), false, false);
                var resultList = result.DetectionResult.Where(f => f.Confidence > Convert.ToSingle(0.8f)).ToList();
                LabelmeEntity labelmeEntity = new LabelmeEntity();
                labelmeEntity.version = ConfigContext.LabelmeVersion;
                labelmeEntity.imagePath = Path.GetFileName(imgFile);
                labelmeEntity.size = new System.IO.FileInfo(imgFile).Length.ToString();
                labelmeEntity.file_attributes = ConfigContext.file_attributes;
                var guid = Guid.NewGuid().ToString();
                var tempFilePath = $"{AppDomain.CurrentDomain.BaseDirectory}tempAutoLabel\\{guid}{DirFileHelper.GetExtension(imgFile)}";
                File.Copy(imgFile, tempFilePath);
                labelmeEntity.imageData = Base64ConvenrtHelper.FileToBase64(Path.GetFullPath(tempFilePath));
                File.Delete(tempFilePath);
                var whList = ImageHelper.GetImageSize(Path.GetFullPath(imgFile));
                if (whList.Count == 2)
                {
                    labelmeEntity.imageWidth = whList[0];
                    labelmeEntity.imageHeight = whList[1];
                }
                labelmeEntity.shapes = new List<Shape>();
                foreach (var detectionResult in resultList)
                {
                    var shape = new Labelme.Shape
                    {
                        label = detectionResult.LabelName,
                        group_id = null,
                        shape_type = "rectangle",
                        flags = new Labelme.Flag()
                    };
                    shape.points = new List<List<double>>();
                    var rectanglePoint1 = new List<double>();
                    rectanglePoint1.Add(detectionResult.Rect.TopLeft.X);
                    rectanglePoint1.Add(detectionResult.Rect.TopLeft.Y);
                    shape.points.Add(rectanglePoint1);
                    var rectanglePoint2 = new List<double>();
                    rectanglePoint2.Add(detectionResult.Rect.BottomRight.X);
                    rectanglePoint2.Add(detectionResult.Rect.BottomRight.Y);
                    shape.points.Add(rectanglePoint2);
                    shape.x = detectionResult.Rect.X;
                    shape.y = detectionResult.Rect.Y;
                    shape.width = detectionResult.Rect.Width;
                    shape.height = detectionResult.Rect.Height;
                    labelmeEntity.shapes.Add(shape);
                }
                System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding(false);
                var jsonFileSaveName = $"{Path.GetDirectoryName(imgFile)}\\{Path.GetFileNameWithoutExtension(imgFile)}.json";
                File.WriteAllText(jsonFileSaveName, labelmeEntity.ToJson(), utf8);
                lock (_lock)
                {
                    count = count + 1;
                    PBress.Value = count;
                    if (PBress.Maximum == count)
                    {
                        btnStart.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }

    }
}
