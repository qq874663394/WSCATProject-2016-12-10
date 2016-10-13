using DevComponents.DotNetBar.SuperGrid;
using InterfaceLayer.Sales;
using System;
using System.Data;
using System.Windows.Forms;

namespace WSCATProject.Warehouse
{
    public partial class TestVoidForm : Form
    {
        SalesOrderInterface soif = new SalesOrderInterface();
        public TestVoidForm()
        {
            InitializeComponent();
        }

        private void TestVoidForm_Load(object sender, EventArgs e)
        {
            
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

        private void button1_Click(object sender, EventArgs e)
        {
            #region dgv子父级添加行
            DataTable dt1 = soif.GetSalesJoinSearch("225525");
            DataTable dt2 = soif.GetSalesDetailJoinSearch();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                sgCustomers.PrimaryGrid.Rows.Add(new GridRow(dt1.Rows[i].ItemArray));
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt1.Rows[i]["code"].Equals(dt2.Rows[j]["mainCode"]))
                    {
                        sgCustomers.PrimaryGrid.Rows.Add(new GridRow(dt2.Rows[j].ItemArray));
                        (sgCustomers.PrimaryGrid.Rows[i] as GridRow).Cells["mainCode"].Value = dt2.Rows[j]["MainCode"];
                        //sgCustomers.PrimaryGrid.Rows[i]//dt1.Rows[i]["code"] = "";
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            #endregion
        }
    }
}