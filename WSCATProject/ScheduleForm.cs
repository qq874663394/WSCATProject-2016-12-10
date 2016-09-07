using DataGridViewProcessEditTest;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject
{
    public partial class ScheduleForm : Form
    {
        public ScheduleForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 模块类型
        /// </summary>
        private string _Mokualiex;
        /// <summary>
        /// 模块类型
        /// </summary>
        public string Mokualiex
        {
            get{ return _Mokualiex; }

            set {_Mokualiex = value;}
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            GridRow gr = superGridControl1.PrimaryGrid.NewRow();
            gr.Cells["gridColumn2"].CellStyles.Default.Image = Properties.Resources.red;
            this.gridColumn2.EditorType = typeof(GridImageEditControl);
            gr.Cells["gridColumn1"].EditorType = typeof(GridImageEditControl);
            gr.Cells["gridColumn1"].CellStyles.Default.Image = Properties.Resources.green大;
            superGridControl1.PrimaryGrid.Rows.Add(gr);
            superGridControl1.PrimaryGrid.DefaultRowHeight = 40;

        }
    }
}
