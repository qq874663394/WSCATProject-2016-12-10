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
            initializeGrid();
        }
        private void initializeGrid()
        {
            GridPanel panel = superGridControl1.PrimaryGrid;
            GridColumn column = panel.Columns["gridColumn1"];
            column.EditorType = typeof(GridStateEditControl);
            GridColumn column1 = panel.Columns["gridColumn2"];
            column1.EditorType = typeof(GridStateEditControl);
            GridColumn column2 = panel.Columns["gridColumn3"];
            column2.EditorType = typeof(GridStateEditControl);
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            GridRow gr = superGridControl1.PrimaryGrid.NewRow();
            gr[0].Value = "1";
            gr[1].Value = "0";
            gr[2].Value = "2";
            superGridControl1.PrimaryGrid.Rows.Add(gr);
        }
    }
}
