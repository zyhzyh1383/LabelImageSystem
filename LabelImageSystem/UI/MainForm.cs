using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using LabelImageSystem.Json;
using LabelImageSystem.Shapes;
using Zach.Util;
using System.Diagnostics;
using System.Text;
using Zach.Util.WinForm;
using System.Threading.Tasks;
using LabelImageSystem.Labelme;
using System.Linq;
using Zach.Loger;
namespace LabelImageSystem
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 当前选中形状类型
        /// </summary>
        private int m_shapeType;
        /// <summary>
        /// 控制切换图片时是否自动保存配置文件
        /// </summary>
        private bool m_bAutoSave;
        /// <summary>
        /// 当前选择中文件名称
        /// </summary>
        private string m_currentFileName;
        /// <summary>
        /// 当前选择中文件索引
        /// </summary>
        public int m_currentFileNameItemIndex;
        /// <summary>
        /// 当前选择的node
        /// </summary>
        public TreeNode m_currentNode;
        /// <summary>
        /// 目标定义列表
        /// </summary>
        List<ObejctDefineViewModel> m_vObjectDefine;
        /// <summary>
        /// 当前选中目标索引 对应于m_vObjectDefine
        /// </summary>
        private int m_ObjectIndex;
        /// <summary>
        /// json文件读写接口
        /// </summary>
        JsonInterface m_JsonIT = new JsonInterface();
        /// <summary>
        /// coco的输出路径
        /// </summary>
        private string gCoCoOutPutDir;
        /// <summary>
        /// 训练集的输出路径
        /// </summary>
        private string gTrainOutPutDir;

        private DrawShapes m_ShpesShow = new DrawShapes();
        private DrawBase m_curDraw = null;
        private Rectangle m_vPicShowRect;
        private Log _log = LogFactory.GetLogger(typeof(MainForm));

        public MainForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            矩形形状ToolStripMenuItem.Click += 矩形形状ToolStripMenuItem_Click;
            多边形形状ToolStripMenuItem.Click += 多边形形状ToolStripMenuItem_Click;
            m_shapeType = ConfigContext.shapeType;
            m_ObjectIndex = 0;
            m_bAutoSave = true;
        }


        /// <summary>
        /// 窗体加载Load事件 初始化
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            InitTreeNode();
            GetObjectDefine();
            initObjectShapeButtons();
            UpdateAutoSaveState();
            SystemTask();
        }
        /// <summary>
        /// 获取标签类型
        /// </summary>
        private void GetObjectDefine()
        {
            ManageObjectForm formOjbject = new ManageObjectForm();
            m_vObjectDefine = formOjbject.GetObjectList();
        }
        /// <summary>
        /// 初始化目录树
        /// </summary>
        private void InitTreeNode()
        {
            try
            {
                TreeNode rootNode = new TreeNode("我的电脑", IconIndexes.MyComputer, IconIndexes.MyComputer);  ///实例化TreeNode类   载入显示 选择显示
                rootNode.Tag = "我的电脑";                            //树节点数据
                rootNode.Text = "我的电脑";                           //树节点标签内容
                this.directoryTree.Nodes.Add(rootNode);               //树中添加根目录

                var myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);          //显示MyDocuments(我的文档)结点 获取计算机我的文档文件夹
                TreeNode DocNode = new TreeNode(myDocuments);
                DocNode.Tag = "我的文档";                            //设置结点名称
                DocNode.Text = "我的文档";
                DocNode.ImageIndex = IconIndexes.MyDocuments;         //设置获取结点显示图片
                DocNode.SelectedImageIndex = IconIndexes.MyDocuments; //设置选择显示图片
                rootNode.Nodes.Add(DocNode);                          //rootNode目录下加载节点
                DocNode.Nodes.Add("");
                foreach (string drive in Environment.GetLogicalDrives())//循环遍历计算机所有逻辑驱动器名称(盘符)
                {
                    var dir = new DriveInfo(drive);            //实例化DriveInfo对象 命名空间System.IO
                    switch (dir.DriveType)           //判断驱动器类型
                    {
                        case System.IO.DriveType.Fixed:        //仅取固定磁盘盘符 Removable-U盘 
                            {
                                TreeNode tNode = new TreeNode(dir.Name.Split(':')[0]);     //Split仅获取盘符字母
                                tNode.Name = dir.Name;
                                tNode.Tag = tNode.Name;
                                tNode.ImageIndex = IconIndexes.FixedDrive;         //获取结点显示图片
                                tNode.SelectedImageIndex = IconIndexes.FixedDrive; //选择显示图片
                                directoryTree.Nodes.Add(tNode);                    //加载驱动节点
                                tNode.Nodes.Add("");
                            }
                            break;
                    }
                }
                rootNode.Expand(); //展开树状视图
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }
        /// <summary>
        /// 系统任务
        /// </summary>
        private void SystemTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    lblCurrentTime.Text = $"当前时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                    await Task.Delay(1000);
                }
            });
        }

        /// <summary>
        /// 根据目标列表 创建radioButton
        /// </summary>
        private void initObjectShapeButtons()
        {
            try
            {
                int x = 0;
                int width = 20;
                int row = 10;

                RadioButton rdb = null;
                groupBox_objTypes.Controls.Clear();
                for (int i = 0; i < m_vObjectDefine.Count; i++)
                {
                    if (width > 720)
                    {
                        row += 10;
                        width = 20;
                    }
                    rdb = new RadioButton();
                    rdb.Name = "radioButton_obj" + i;
                    rdb.Text = m_vObjectDefine[i].ObjScript;
                    rdb.Location = new System.Drawing.Point(width, 2 * row);
                    rdb.Click += new EventHandler(rdb_ClickChanged);
                    x = rdb.Text.Length;
                    rdb.Size = new System.Drawing.Size(5 * 20 + 30, 16);
                    width = rdb.Location.X + rdb.Size.Width + 10;
                    groupBox_objTypes.Controls.Add(rdb);
                }

                Control control = this.FindControl("radioButton_obj" + m_ObjectIndex.ToString(), groupBox_objTypes);
                if (null != control)
                {
                    ((RadioButton)control).Checked = true;
                }
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }

        /// <summary>
        /// 响应目标类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void rdb_ClickChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rbt = (RadioButton)sender;
                string name = rbt.Name;
                m_ObjectIndex = int.Parse(name.Substring("radioButton_obj".Length));
                if (null != m_ShpesShow && null != m_ShpesShow.m_CurrentSel)
                {
                    m_ShpesShow.m_CurrentSel.m_objName = m_vObjectDefine[m_ObjectIndex].ObjName.Clone() as string;
                    m_ShpesShow.m_CurrentSel.m_objScript = m_vObjectDefine[m_ObjectIndex].ObjScript.Clone() as string;
                    pictureBox1.Invalidate();
                }
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }

        Control FindControl(string controlName, Control fatherControl)
        {
            foreach (Control c in fatherControl.Controls)
            {
                if (c.Name == controlName)
                {
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// 重写以按Delete键删除当前对象
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Delete)
            {
                if (m_ShpesShow.m_CurrentSel != null)
                {
                    m_ShpesShow.Remove(m_ShpesShow.m_CurrentSel);
                    m_ShpesShow.m_CurrentSel = null;
                    m_ShpesShow.m_selType = -1;
                    pictureBox1.Invalidate();
                }
            }
            return base.ProcessDialogKey(keyData);
        }


        private void directoryTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.Expand();
        }

        private void directoryTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeViewItems.Add(e.Node, chklbPictures, GetShowType());
        }
        private void directoryTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewItems.Add(e.Node, chklbPictures, GetShowType());
        }

        private void rdoState_Click(object sender, EventArgs e)
        {
            var rdo = sender as RadioButton;
            FilterImgs(rdo.Text);
        }

        private void FilterImgs(string str)
        {
            TreeViewItems.ShowType showType = TreeViewItems.ShowType.All;
            switch (str)
            {
                case "全部":
                    showType = TreeViewItems.ShowType.All;
                    break;
                case "未标注":
                    showType = TreeViewItems.ShowType.Not;
                    break;
                case "已标注":
                    showType = TreeViewItems.ShowType.Ok;
                    break;
                default:
                    break;
            }
            var node = m_currentNode;
            if (node == null) return;
            TreeViewItems.Add(node, chklbPictures, showType);
        }

        public TreeViewItems.ShowType GetShowType()
        {
            if (rdoAll.Checked)
                return TreeViewItems.ShowType.All;
            else if (rdoNot.Checked)
                return TreeViewItems.ShowType.Not;
            else
                return TreeViewItems.ShowType.Ok;
        }

        private int GetImagePositon()
        {
            try
            {
                if (pictureBox1.Image == null) return 0;

                int originalWidth = this.pictureBox1.Image.Width;
                int originalHeight = this.pictureBox1.Image.Height;

                PropertyInfo rectangleProperty = this.pictureBox1.GetType().GetProperty("ImageRectangle", BindingFlags.Instance | BindingFlags.NonPublic);
                m_vPicShowRect = (Rectangle)rectangleProperty.GetValue(this.pictureBox1, null);

                if (m_ShpesShow != null)
                {
                    m_ShpesShow.SetImageInfo(m_vPicShowRect, originalWidth, originalHeight);
                }
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
            return 0;
        }


        private void 标签类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageObjectForm mof = new ManageObjectForm();
            mof.ShowDialog();
            m_vObjectDefine = new ManageObjectForm().GetObjectList();
            initObjectShapeButtons();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (null != m_ShpesShow)
            {
                m_ShpesShow.Draw(e.Graphics);
            }

            if (null != m_curDraw && m_curDraw.m_bIsDrawing)
            {
                m_curDraw.Draw(e.Graphics);
            }
        }

        private bool IsPointInPictureArea(Point pt, Rectangle rectPic)
        {
            if (null == m_vPicShowRect)
                return false;
            Region reg = new Region(rectPic);
            return reg.IsVisible(pt.X, pt.Y);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {

                base.OnMouseDown(e);
                if (!IsPointInPictureArea(e.Location, m_vPicShowRect))
                {
                    return;
                }

                if (e.Button == MouseButtons.Left)            //处理选中状态
                {
                    if (m_ShpesShow.m_CurrentCover != null)
                    {
                        if (m_ShpesShow.m_CurrentSel != null && m_ShpesShow.m_CurrentSel != m_ShpesShow.m_CurrentCover)
                        {
                            m_ShpesShow.m_CurrentSel.m_bSelect = false;
                        }
                        m_ShpesShow.m_CurrentSel = m_ShpesShow.m_CurrentCover;
                        m_ShpesShow.m_CurrentCover.m_bSelect = true;
                        pictureBox1.Invalidate();
                        return;
                    }
                    else
                    {
                        if (m_ShpesShow.m_CurrentSel != null)
                        {
                            m_ShpesShow.m_CurrentSel.m_bSelect = false;
                            m_ShpesShow.m_CurrentSel = null;
                            pictureBox1.Invalidate();
                            return;
                        }
                    }
                }

                if (e.Button == MouseButtons.Right)
                {
                    if (m_ShpesShow.m_CurrentSel != null)
                    {
                        m_ShpesShow.m_CurrentSel.m_bSelect = false;
                        pictureBox1.Invalidate();
                        return;
                    }
                }

                //处理画图时左键点击
                if (ShapeTypeIndexes.Rect == m_shapeType)
                {
                    if (null == m_ShpesShow.m_CurrentCover && (null == this.m_curDraw || false == m_curDraw.m_bIsDrawing))
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            m_curDraw = new RectangleObj();
                            m_curDraw.MouseDown(e.Location);
                            m_curDraw.m_objName = m_vObjectDefine[m_ObjectIndex].ObjName.Clone() as string;
                            m_curDraw.m_objScript = m_vObjectDefine[m_ObjectIndex].ObjScript.Clone() as string;
                        }
                    }
                }
                else if (ShapeTypeIndexes.Ploy == m_shapeType)
                {
                    if (null == m_ShpesShow.m_CurrentCover)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            if (null == m_curDraw)
                            {
                                m_curDraw = new PolygonObj();
                                m_curDraw.m_objName = m_vObjectDefine[m_ObjectIndex].ObjName.Clone() as string;
                                m_curDraw.m_objScript = m_vObjectDefine[m_ObjectIndex].ObjScript.Clone() as string;
                            }

                            m_curDraw.MouseDown(e.Location);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }



        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPointInPictureArea(e.Location, m_vPicShowRect))
            {
                return;
            }
            try
            {

                if (this.pictureBox1.Capture)
                {
                    if (this.m_ShpesShow != null && null == m_curDraw && null != m_ShpesShow.m_CurrentCover)
                    {
                        if (0 == m_ShpesShow.m_selType)
                        {
                            m_ShpesShow.moveObj(e.Location);
                        }
                        else
                        {
                            m_ShpesShow.reJustObj(e.Location);
                        }
                        pictureBox1.Invalidate();
                        return;
                    }
                }

                if (null != m_curDraw && true == m_curDraw.m_bIsDrawing)
                {
                    if (ShapeTypeIndexes.Rect == m_shapeType)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            m_curDraw.MouseMove(e.Location);
                            pictureBox1.Invalidate();
                            return;
                        }
                    }
                    else if (ShapeTypeIndexes.Ploy == m_shapeType)
                    {
                        m_curDraw.MouseMove(e.Location);
                        pictureBox1.Invalidate();
                        return;
                    }
                }

                if (null != m_ShpesShow)
                {
                    m_ShpesShow.getObj(e.Location);

                    if (1 == m_ShpesShow.m_selType)
                    {
                        this.Cursor = Cursors.Cross;
                        pictureBox1.Invalidate();
                    }
                    else if (0 == m_ShpesShow.m_selType)
                    {
                        this.Cursor = Cursors.SizeAll;
                        pictureBox1.Invalidate();
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        pictureBox1.Invalidate();
                    }
                }

            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {


                if (!IsPointInPictureArea(e.Location, m_vPicShowRect))
                {
                    return;
                }

                if (ShapeTypeIndexes.Rect == m_shapeType)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (null != m_curDraw && 0 == m_curDraw.MouseUp(e.Location))
                        {
                            if (m_curDraw.CheckValid())
                            {
                                m_curDraw.ptGtoImage(m_ShpesShow.m_GIChange);
                                this.m_ShpesShow.Add(m_curDraw);
                            }
                            //m_curDraw.m_bIsDrawing = false;
                            m_curDraw = null;
                        }
                    }
                }
                else if (ShapeTypeIndexes.Ploy == m_shapeType)
                {
                    if (e.Button == MouseButtons.Right && null != m_curDraw)
                    {
                        m_curDraw.MouseUp(e.Location);
                        if (m_curDraw.CheckValid())
                        {
                            m_curDraw.ptGtoImage(m_ShpesShow.m_GIChange);
                            this.m_ShpesShow.Add(m_curDraw);
                        }
                        ((PolygonObj)m_curDraw).vPoint.Clear();
                        m_curDraw = null;
                        //m_curDraw.m_bIsDrawing = false;
                    }
                }

                if (e.Button == MouseButtons.Left)
                {
                    if (null != m_ShpesShow.m_CurrentCover)
                    {
                        m_ShpesShow.m_CurrentCover.ptGtoImage(m_ShpesShow.m_GIChange);
                    }
                }

                pictureBox1.Invalidate();
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }

        private void 矩形形状ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_shapeType = ShapeTypeIndexes.Rect;
        }

        private void 多边形形状ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_shapeType = ShapeTypeIndexes.Ploy;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            GetImagePositon();
            if (null != m_ShpesShow)
            {
                m_ShpesShow.AllShapesImageToGrapic();
            }

        }

        private void SaveLabelInfo()
        {
            try
            {
                if (m_currentFileName != null)
                {
                    string jsonName = m_currentFileName.Substring(0, m_currentFileName.LastIndexOf('.')) + ".json";
                    m_JsonIT.SaveShapesAndJsonFile(m_currentFileName, m_ShpesShow, jsonName, 0);
                    this.chklbPictures.SetItemCheckState(m_currentFileNameItemIndex, CheckState.Checked);
                }
            }
            catch
            {

            }
        }

        private void ToolStripMenuItem_SaveLabel_Click(object sender, EventArgs e)
        {
            SaveLabelInfo();
        }

        private void UpdateAutoSaveState()
        {
            this.ToolStripMenuItem_StartAuto.Checked = m_bAutoSave;
            this.ToolStripMenuItem_StopAuto.Checked = !m_bAutoSave;

            Font oldFont;
            Font newFont;
            oldFont = this.ToolStripMenuItem_AutoSave.Font;

            if (m_bAutoSave)
            {
                ToolStripMenuItem_AutoSave.Text = "切换图片自动保存: 启用";
                if (!oldFont.Bold)
                {
                    newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);
                    this.ToolStripMenuItem_AutoSave.Font = newFont;
                }
            }
            else
            {
                ToolStripMenuItem_AutoSave.Text = "切换图片自动保存: 停用";
                if (oldFont.Bold)
                {
                    newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);
                    this.ToolStripMenuItem_AutoSave.Font = newFont;
                }
            }
        }
        private void ToolStripMenuItem_StartAuto_Click(object sender, EventArgs e)
        {
            m_bAutoSave = true;
            UpdateAutoSaveState();
        }

        private void ToolStripMenuItem_StopAuto_Click(object sender, EventArgs e)
        {
            m_bAutoSave = false;
            UpdateAutoSaveState();
        }

        private void 数据集转cocoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var inputDir = DirFileHelper.GetDirectoryName(m_currentFileName);
                CoCoConfigForm cocoForm = new CoCoConfigForm(inputDir);
                cocoForm.DirEvent += CocoForm_DirEvent;
                cocoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }

        private void CocoForm_DirEvent(string inputDir, string outputDir, List<ObejctDefineViewModel> obejctDefineViewModelLists)
        {
            try
            {

                if (obejctDefineViewModelLists.Count == 0 || inputDir.IsEmpty() || outputDir.IsEmpty())
                {
                    return;
                }
                var labels = $"{AppDomain.CurrentDomain.BaseDirectory}fileTemplate\\labels.txt";
                var labelsContent = DirFileHelper.ReadAllText(labels);
                if (labelsContent.IsEmpty())
                {
                    return;
                }
                StringBuilder sb = new StringBuilder();
                obejctDefineViewModelLists.ForEach(i => sb.AppendLine(i.ObjName));
                var labelsWriteContent = labelsContent.Replace("@label", sb.ToString());
                var labelsSavePath = $"{AppDomain.CurrentDomain.BaseDirectory + "labels.txt"}";
                File.WriteAllText(labelsSavePath, labelsWriteContent, new System.Text.UTF8Encoding(false));
                gCoCoOutPutDir = outputDir;
                var coco = ConfigContext.coco.Replace("@input_dir", inputDir).Replace("@output_dir", outputDir);
                if (DirFileHelper.IsExistDirectory(outputDir))
                {
                    if (!MessageShow.Confirm("此次操作会覆盖原有输出目录的coco文件夹,确认继续?", "警告框"))
                    {
                        return;
                    }
                    DirFileHelper.DeleteDir(outputDir);
                }
                AddOperationMsg($"数据集转coco命令:{Environment.NewLine}{coco}");
                InvokePython(coco);
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }


        private void 训练cocoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TrainConfigForm trainForm = new TrainConfigForm(gCoCoOutPutDir);
                trainForm.TrainConfigEvent += TrainForm_TrainConfigEvent;
                trainForm.ShowDialog();
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }

        private void TrainForm_TrainConfigEvent(int epoch, int numClasses, bool useGpu, string datasetDir, string saveDir, string pretrain_weights)
        {
            gTrainOutPutDir = saveDir;
            if (epoch == 0 || numClasses == 0 || datasetDir.IsEmpty())
            {
                return;
            }

            var faster_rcnn_r50_vd_fpn_ssld_2x_coco = $"{AppDomain.CurrentDomain.BaseDirectory}fileTemplate\\faster_rcnn_r50_vd_fpn_ssld_2x_coco.yml";
            var faster_rcnn_r50_vd_fpn_ssld_2x_coco_content = DirFileHelper.ReadAllText(faster_rcnn_r50_vd_fpn_ssld_2x_coco);
            var faster_rcnn_r50_vd_fpn_ssld_2x_coco_write_content = faster_rcnn_r50_vd_fpn_ssld_2x_coco_content.Replace("@pretrain_weights", pretrain_weights).Replace("@epoch", epoch.ToString());
            var faster_rcnn_r50_vd_fpn_ssld_2x_coco_savePath = $"{AppDomain.CurrentDomain.BaseDirectory}PaddleDetection\\configs\\faster_rcnn\\faster_rcnn_r50_vd_fpn_ssld_2x_coco.yml";
            File.WriteAllText(faster_rcnn_r50_vd_fpn_ssld_2x_coco_savePath, faster_rcnn_r50_vd_fpn_ssld_2x_coco_write_content, new System.Text.UTF8Encoding(false));

            var coco_detection = $"{AppDomain.CurrentDomain.BaseDirectory}fileTemplate\\coco_detection.yml";
            var coco_detection_content = DirFileHelper.ReadAllText(coco_detection);
            var coco_detection_write_content = coco_detection_content.Replace("@num_classes", numClasses.ToString()).Replace("@dataset_dir", datasetDir);
            var coco_detection_content_savePath = $"{AppDomain.CurrentDomain.BaseDirectory}PaddleDetection\\configs\\datasets\\coco_detection.yml";
            File.WriteAllText(coco_detection_content_savePath, coco_detection_write_content, new System.Text.UTF8Encoding(false));

            var runtime = $"{AppDomain.CurrentDomain.BaseDirectory}fileTemplate\\runtime.yml";
            var runtime_content = DirFileHelper.ReadAllText(runtime);
            var runtime_write_content = runtime_content.Replace("@use_gpu", useGpu.ToString()).Replace("@save_dir", saveDir);
            var runtime_content_savePath = $"{AppDomain.CurrentDomain.BaseDirectory}PaddleDetection\\configs\\runtime.yml";
            File.WriteAllText(runtime_content_savePath, runtime_write_content, new System.Text.UTF8Encoding(false));

            AddOperationMsg($"训练coco命令::{Environment.NewLine}{ConfigContext.train}");
            InvokePython(ConfigContext.train);
        }

        private void 导出模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // "export": "python PaddleDetection/tools/export_model.py -c PaddleDetection/configs/faster_rcnn/faster_rcnn_r50_vd_fpn_ssld_2x_coco.yml -o weights=@save_dir/faster_rcnn_r50_vd_fpn_ssld_2x_coco/model_final.pdparams --output_dir=@output_dir"
                if (gTrainOutPutDir.IsEmpty())
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.Description = "请选择要导出的模型路径";
                    if (dialog.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }
                    gTrainOutPutDir = dialog.SelectedPath;
                }
                ConfigContext.export = ConfigContext.export.Replace("@save_dir", gTrainOutPutDir).Replace("@output_dir", $"{gTrainOutPutDir.Replace("output", "inference_model")}");
                AddOperationMsg($"导出模型命令:{Environment.NewLine}{ConfigContext.export}");
                InvokePython(ConfigContext.export);
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }
        /// <summary>
        /// 调佣Python脚本
        /// </summary>
        /// <param name="command"></param>
        private void InvokePython(string command)
        {
            Process p;
            string writeLine = command;
            p = new Process();
            //设置要启动的应用程序
            p.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            p.StartInfo.UseShellExecute = false;
            // 接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardInput = true;
            //输出信息
            p.StartInfo.RedirectStandardOutput = true;
            // 输出错误
            p.StartInfo.RedirectStandardError = true;
            //不显示程序窗口
            p.StartInfo.CreateNoWindow = true;
            p.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            //启动程序
            p.Start();
            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(writeLine);
            p.StandardInput.AutoFlush = true;
            p.BeginOutputReadLine();
        }

        private void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                StringBuilder sb = new StringBuilder(this.textBox_Process.Text);
                this.textBox_Process.Text = sb.AppendLine(outLine.Data).ToString();
                this.textBox_Process.SelectionStart = this.textBox_Process.Text.Length;
                this.textBox_Process.ScrollToCaret();
            }
        }


        private void AddOperationMsg(string msg)
        {
            StringBuilder sb = new StringBuilder(this.textBox_Process.Text);
            this.textBox_Process.Text = sb.AppendLine(msg).ToString();
            this.textBox_Process.SelectionStart = this.textBox_Process.Text.Length;
            this.textBox_Process.ScrollToCaret();
        }

        private void chklbPictures_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var index = chklbPictures.SelectedIndex;

                if (index == -1) return;

                var name = chklbPictures.Items[index].ToString();

                if (name.IsNullOrWhiteSpace()) return;

                if (m_bAutoSave && m_currentFileName != null)
                {
                    SaveLabelInfo();
                }

                if (File.Exists(name))
                {
                    m_currentFileName = name;

                    m_currentFileNameItemIndex = index;

                    this.pictureBox1.Image = Image.FromFile(name);

                    string jsonFileName = m_currentFileName.Substring(0, m_currentFileName.LastIndexOf('.')) + ".json";
                    m_ShpesShow = m_JsonIT.LoadJsonFileName(jsonFileName);

                    if (null == m_ShpesShow)
                    {
                        m_ShpesShow = new DrawShapes();
                    }

                    GetImagePositon();
                    m_ShpesShow.AllShapesImageToGrapic();
                    m_ShpesShow.UpdateObjectScriptInfo(m_vObjectDefine);
                    pictureBox1.Invalidate();
                }
                else
                {
                    this.pictureBox1.Image = null;
                    m_currentFileName = null;
                    m_ShpesShow.Clear();
                }
            }
            catch (Exception ex)
            {
                _log.WriteFormatError(ex);
                MessageShow.Show(ex.Message);
            }
        }

        /// <summary>
        /// 自定义类TreeViewItems 调用其Add(TreeNode e)方法加载子目录
        /// </summary>
        public class TreeViewItems
        {
            public enum ShowType
            {
                All,
                Not,
                Ok
            }
            public static void Add(TreeNode e, CheckedListBox chklbPictures, ShowType showType)
            {
                //try..catch异常处理
                try
                {
                    //判断"我的电脑"Tag 上面加载的该结点没指定其路径
                    if (e.Tag.ToString() != "我的电脑")
                    {
                        e.Nodes.Clear();                               //清除空节点再加载子节点
                        TreeNode tNode = e;                            //获取选中\展开\折叠结点
                        string path = tNode.Name;                      //路径             
                        if (e.Tag.ToString() == "我的文档")            //获取"我的文档"路径
                        {
                            path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //获取计算机我的文档文件夹
                        }
                        string[] dics = Directory.GetDirectories(path);               //获取指定目录中的子目录名称并加载结点
                        foreach (string dic in dics)
                        {
                            TreeNode subNode = new TreeNode(new DirectoryInfo(dic).Name); //实例化
                            subNode.Name = new DirectoryInfo(dic).FullName;               //完整目录
                            subNode.Tag = subNode.Name;
                            subNode.ImageIndex = IconIndexes.ClosedFolder;       //获取节点显示图片
                            subNode.SelectedImageIndex = IconIndexes.OpenFolder; //选择节点显示图片
                            tNode.Nodes.Add(subNode);
                            subNode.Nodes.Add("");                               //加载空节点 实现+号
                        }

                        string[] files = Directory.GetFiles(path, "*.*");
                        var fileJsons = files.Where(f => f.EndsWith(".json") == true).ToList();
                        chklbPictures.Items.Clear();
                        foreach (string file in files)
                        {
                            if (DirFileHelper.GetExtension(file).ToUpper() == ".JPG")
                            {
                                var bChecked = false;
                                var temp = new DirectoryInfo(file);
                                if (fileJsons.FindAll(
                                    f => DirFileHelper.GetDirectoryName(f) == DirFileHelper.GetDirectoryName(file)
                                && DirFileHelper.GetFileNameNoExtension(f) == DirFileHelper.GetFileNameNoExtension(temp.Name)
                                ).Count > 0)
                                {
                                    bChecked = true;
                                }
                                if (showType == ShowType.Not)
                                {
                                    if (bChecked)
                                        continue;
                                }
                                else if (showType == ShowType.Ok)
                                {
                                    if (!bChecked)
                                        continue;
                                }
                                chklbPictures.Items.Add(temp.FullName, bChecked);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageShow.Show(ex.Message);
                }
            }
        }

        private void chklbPictures_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var oldValue = this.chklbPictures.GetItemCheckState(e.Index);
            if (e.Index == this.chklbPictures.SelectedIndex)
            {
                e.NewValue = oldValue;
            }
        }

        private void directoryTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            m_currentNode = directoryTree.SelectedNode;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MessageShow.Confirm("确定退出工具?"))
            {
                e.Cancel = true;
            }
        }
    }
}
