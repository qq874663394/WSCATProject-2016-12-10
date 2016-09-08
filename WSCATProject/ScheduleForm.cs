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
        /// 接收的状态值
        /// </summary>
        private int _state;
        /// <summary>
        /// 仓库出库的类型
        /// </summary>
        private string _canku;
        /// <summary>
        /// 模块类型
        /// </summary>
        public string Mokualiex
        {
            get{ return _Mokualiex; }

            set {_Mokualiex = value;}
        }
        /// <summary>
        /// 接收状态值
        /// </summary>
        public int State
        {
            get{return _state; }
            set{ _state = value;}
        }
        /// <summary>
        /// 仓库出库的类型
        /// </summary>
        public string Canku
        {
            get {return _canku;}
            set { _canku = value;}
        }

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.DefaultRowHeight = 40;
            this.superGridControl1.PrimaryGrid.ShowRowHeaders = false;
        
            //控制列的高度不能拖动
           // datagridUser.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //控制列的宽度不能拖动
           // datagridUser.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        
            switch (_Mokualiex)
            {
                case "用户资料":
                    break;
                case "权限分配":
                    break;
                case "仓库资料":
                    break;
                case "货品资料":
                    break;
                case "供应商资料":
                    break;
                case "物料信息":
                    break;
                case "仓库系统":
                  
                    if (_state ==0)
                    {
                        InStorage();
                        GridRow gr = superGridControl1.PrimaryGrid.NewRow();
                        gr.Cells["weiruku"].CellStyles.Default.Image = Properties.Resources.yellow大;
                        gr.Cells["bufunruk"].CellStyles.Default.Image = Properties.Resources.green;
                        gr.Cells["yiruku"].CellStyles.Default.Image = Properties.Resources.green;
                        superGridControl1.PrimaryGrid.Rows.Add(gr);
                    }
                    if (_state == 1)
                    {
                        InStorage();
                        GridRow gr = superGridControl1.PrimaryGrid.NewRow();
                        gr.Cells["weiruku"].CellStyles.Default.Image = Properties.Resources.red;
                        gr.Cells["bufunruk"].CellStyles.Default.Image = Properties.Resources.yellow大;
                        gr.Cells["yiruku"].CellStyles.Default.Image = Properties.Resources.green;
                        superGridControl1.PrimaryGrid.Rows.Add(gr);
                    }
                    if (_state == 2)
                    {
                        InStorage();
                        GridRow gr = superGridControl1.PrimaryGrid.NewRow();
                        gr.Cells["weiruku"].CellStyles.Default.Image = Properties.Resources.red;
                        gr.Cells["bufunruk"].CellStyles.Default.Image = Properties.Resources.red;
                        gr.Cells["yiruku"].CellStyles.Default.Image = Properties.Resources.yellow大;
                        superGridControl1.PrimaryGrid.Rows.Add(gr); ;
                    }
                    break;
                case "销售系统":
                    break;
                case "售后系统":
                    break;
                case "采购系统":
                    break;
                case "财务系统":
                    break;
                case "考勤系统":
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 初始化出库模块的窗体
        /// </summary>
        private void InStorage()
        {
            if (_canku=="入库开单")
            {
                GridColumn gc = null;
                gc = new GridColumn();
                gc.Name = "weiruku";
                gc.HeaderText = "未入库";
                gc.EditorType = typeof(GridImageEditControl);//转换可以显示图片的控件
                gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
                gc.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                gc.Visible = true;
                superGridControl1.PrimaryGrid.Columns.Add(gc);

                gc = new GridColumn();
                gc.Name = "bufunruk";
                gc.HeaderText = "部分入库";
                gc.EditorType = typeof(GridImageEditControl);
                gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
                gc.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                gc.Visible = true;
                superGridControl1.PrimaryGrid.Columns.Add(gc);

                gc = new GridColumn();
                gc.Name = "yiruku";
                gc.HeaderText = "已入库";
                gc.EditorType = typeof(GridImageEditControl);
                gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
                gc.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                gc.Visible = true;
                superGridControl1.PrimaryGrid.Columns.Add(gc);
            }
            if (_canku=="出库开单")
            {
                GridColumn gc = null;
                gc = new GridColumn();
                gc.Name = "weiruku";
                gc.HeaderText = "未出库";
                gc.EditorType = typeof(GridImageEditControl);
                gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
                gc.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                gc.Visible = true;
                superGridControl1.PrimaryGrid.Columns.Add(gc);

                gc = new GridColumn();
                gc.Name = "bufunruk";
                gc.HeaderText = "部分出库";
                gc.EditorType = typeof(GridImageEditControl);
                gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
                gc.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                gc.Visible = true;
                superGridControl1.PrimaryGrid.Columns.Add(gc);

                gc = new GridColumn();
                gc.Name = "yiruku";
                gc.HeaderText = "已出库";
                gc.EditorType = typeof(GridImageEditControl);
                gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
                gc.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                gc.Visible = true;
                superGridControl1.PrimaryGrid.Columns.Add(gc);
            }
        
        }

        private void ScheduleForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
        }
    }
}
