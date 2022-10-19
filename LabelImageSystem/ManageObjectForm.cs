﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Zach.Entity;
using Zach.Service;
using Zach.Util;
using Zach.Util.WinForm;

namespace LabelImageSystem
{
    public partial class ManageObjectForm : Form
    {
        List<ObejctDefineViewModel> m_vObjects;
        ObjectdefineService objectdefineService = new ObjectdefineService();

        public ManageObjectForm()
        {
            InitializeComponent();
            GetOjectDefines(false);
        }

        public void GetOjectDefines(bool bInsertToDgv = false)
        {
            m_vObjects = null;
            m_vObjects = new List<ObejctDefineViewModel>();
            var entityList = objectdefineService.GetList();
            entityList.ToList().ForEach(o =>
            {
                ObejctDefineViewModel obj = new ObejctDefineViewModel();
                obj.ObjID = o.ObjID;
                obj.ObjName = o.ObjName;
                obj.ObjScript = o.ObjScript;
                m_vObjects.Add(obj);
                if (bInsertToDgv)
                {
                    int index = this.dgvObject.Rows.Add();
                    this.dgvObject.Rows[index].Cells[ObjID.Name].Value = obj.ObjID;
                    this.dgvObject.Rows[index].Cells[ObjName.Name].Value = obj.ObjName;
                    this.dgvObject.Rows[index].Cells[ObjScript.Name].Value = obj.ObjScript;
                }
            });
        }


        private void Form_ManageObject_Load(object sender, EventArgs e)
        {
            GetOjectDefines(true);
        }

        public List<ObejctDefineViewModel> GetObjectList()
        {
            return m_vObjects;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageShow.Confirm("确认保存?"))
            {

                    var objectdefines = new List<Objectdefine>();
                    m_vObjects.ForEach(m =>
                    {
                        var temp = new Objectdefine
                        {
                            ObjID = m.ObjID,
                            ObjName = m.ObjName,
                            ObjScript = m.ObjScript,
                        };
                        objectdefines.Add(temp);
                    });
                    try
                    {
                        objectdefineService.Delete(objectdefines);
                        objectdefines = new List<Objectdefine>();
                        foreach (DataGridViewRow dgvr in dgvObject.Rows)
                        {
                            var temp = new Objectdefine
                            {
                                ObjName = dgvr.Cells[ObjName.Name].Value.ToString(),
                                ObjScript = dgvr.Cells[ObjScript.Name].Value.ToString()
                            };
                            objectdefines.Add(temp);
                        }
                        var result = objectdefineService.Insert(objectdefines);
                        GetOjectDefines(false);
                        MessageShow.Show("保存成功");
                    }
                    catch
                    {
                        
                    }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var index = dgvObject.CurrentCell.RowIndex;
            if (index < 0) return;
            var delList = new List<DataGridViewRow>();
            foreach (DataGridViewRow dgvr in dgvObject.Rows)
            {
                var checkvalue = dgvr.Cells[CHKID.Name].Value.ToBool();
                if (checkvalue)
                    delList.Add(dgvr);
            }
            delList.ForEach(d =>
            {
                dgvObject.Rows.Remove(d);
            });
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var index = dgvObject.CurrentCell.RowIndex;
            if (index < 0) return;
            dgvObject.Rows.RemoveAt(index);
        }

        private void 新增ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvObject.Rows.Add();
        }
    }
}
