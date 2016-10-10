using BaseLayer;
using DevComponents.DotNetBar.SuperGrid;
using HelperUtility;
using InterfaceLayer.Base;
using InterfaceLayer.Warehouse;
using LogicLayer;
using LogicLayer.Base;
using LogicLayer.Purchase;
using LogicLayer.Warehouse;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Warehouse
{
    public partial class TestVoidForm : Form
    {
        private int _clickRowIndex;

        public int ClickRowIndex
        {
            get
            {
                return _clickRowIndex;
            }

            set
            {
                _clickRowIndex = value;
            }
        }

        public TestVoidForm()
        {
            InitializeComponent();
        }

        private void TestVoidForm_Load(object sender, EventArgs e)
        {
            //name.SortMode = DataGridViewColumnSortMode.NotSortable; //排序模式
            superGridControl1.PrimaryGrid.SortCycle = SortCycle.AscDesc;    //排序方式范围
            superGridControl1.PrimaryGrid.AddSort(superGridControl1.PrimaryGrid.Columns[0], SortDirection.Ascending);//设置排序列和排序方式

            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;//显示行号
            InitColumns();  //初始化列
            InitDataGridView(); //统计行

            //DGV数据源
            ClientInterface ci = new ClientInterface();
            dataGridView1.DataSource = ci.GetList(999, "");
            dataGridView1.AutoGenerateColumns = false;
            dataGridView2.DataSource = ci.GetList(999, "");
            dataGridView2.AutoGenerateColumns = false;
        }
        public string ExSwitch(string warehouseInResult)
        {
            switch (warehouseInResult)
            {
                case "-1":
                    return "错误代码:4001;拼接连接字符串时出现异常,请尝试重新插入数据.";
                case "-2":
                    return "错误代码:4002;建立查询字符串参数时出现异常";
                case "-3":
                    return "错误代码:4003;对参数赋值时出现异常,请检查输入";
                case "-4":
                    return "错误代码:4004;尝试打开数据库连接时出错,请检查服务器连接";
                case "-5":
                    return "错误代码:4005;对数据库新增数据时未能增加任何数据";
                case "-6":
                    return "错误代码:4006;对数据库新增数据的方法失效,未能增加任何行";
                case "-7":
                    return "错误代码:4007;检查到传入的参数为空,无法进行新增操作";
                default:
                    return "未知错误";
            }
        }
        /// <summary>
        /// 初始化列
        /// </summary>
        public void InitColumns()
        {
            GridColumn gc = null;
            gc = new GridColumn();
            gc.DataPropertyName = "name";
            gc.Name = "name";
            gc.HeaderText = "name";
            gc.Width = 120;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControl1.PrimaryGrid.Columns.Add(gc);

            gc = new GridColumn();
            gc.DataPropertyName = "code";
            gc.Name = "code";
            gc.HeaderText = "code";
            gc.Width = 120;
            gc.AutoSizeMode = ColumnAutoSizeMode.Fill;
            superGridControl1.PrimaryGrid.Columns.Add(gc);
        }
        private void InitDataGridView()
        {
            //新增一行 用于给客户操作
            superGridControl1.PrimaryGrid.NewRow(true);
            //最后一行做统计行
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.
                Rows[superGridControl1.PrimaryGrid.Rows.Count - 1];
            gr.ReadOnly = true;
            gr.CellStyles.Default.Background.Color1 = Color.SkyBlue;
            gr.Cells["name"].Value = "99999";
            gr.Cells["code"].Value = "99999";
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool newAdd = false;
            GridRow gr = (GridRow)superGridControl1.PrimaryGrid.Rows[ClickRowIndex];
            GridItemsCollection _gridItemCollection = superGridControl1.PrimaryGrid.Rows;//获取所有行

            foreach (GridRow item in _gridItemCollection)
            {
                if (item.Cells["code"].Value == null)
                {
                    newAdd = true;
                    continue;
                }
                if (item.Cells["code"].Value.Equals(dataGridView1.Rows[e.RowIndex].Cells["code"].Value))//双击code和所有行的code相同时
                {
                    item.Cells["name"].Value = dataGridView1.Rows[e.RowIndex].Cells["name"].Value;  //修改
                    item.Cells["code"].Value = dataGridView1.Rows[e.RowIndex].Cells["code"].Value;
                    newAdd = false;
                    break;
                }
                else
                {
                    newAdd = true;
                }
            }
            if (newAdd == true)
            {
                GridRow gr1 = new GridRow();
                //无法保证每次都对应
                superGridControl1.PrimaryGrid.Rows.Insert(0, 
                    new GridRow(
                    dataGridView1.Rows[e.RowIndex].Cells["name"].Value, 
                    dataGridView1.Rows[e.RowIndex].Cells["code"].Value

                    )
                    );
            }
        }

        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            ClickRowIndex = e.GridCell.RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void superGridControl1_CellClick(object sender, GridCellClickEventArgs e)
        {
        }
        //    #region Table合并
        //    //WarehouseInInterface wi = new WarehouseInInterface();
        //    //WarehouseOutInterface wo = new WarehouseOutInterface();
        //    //DataTable widt = wi.GetListToIn(0).Tables[0];
        //    //DataTable wodt = wo.GetListToOut(0).Tables[0];
        //    //wodt.Merge(widt); 
        //    #endregion
        ////当单元格编辑器值更改后发生
        //private void superGridControl1_EditorValueChanged(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        //{
        //    string text = e.EditControl.EditorValue.ToString();
        //}
    }
}