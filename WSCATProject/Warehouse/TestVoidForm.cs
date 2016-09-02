﻿using BaseLayer;
using LogicLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSCATProject.Warehouse
{
    public partial class TestVoidForm : Form
    {
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
            #region 查询
            ClientLogic cl = new ClientLogic();
            comboBox1.ValueMember = "code";
            comboBox1.DisplayMember = "name";
            try
            {
                comboBox1.DataSource = cl.SelClient(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ExSwitch(ex.Message));
            }
            #endregion

            #region 新增
            string sql = @"insert into T_BaseArea(code,name,parentId) 
values(888B83C9D6D4D5D2D4DCD6D1D5D2D4D0D5DCD3DC,'888B83C9D6D4D5D2D4DCD6D1D5D2D4D0D5DCD3DC','888B83C9D6D4D5D2D4DCD6D1D5D2D4D0D5DCD3DC')";
            try
            {
                int result = DbHelperSQL.ExecuteSql(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ExSwitch(ex.Message));
            }
            #endregion
        }
    }
}