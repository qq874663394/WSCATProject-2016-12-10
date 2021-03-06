﻿namespace WSCATProject.Warehouse
{
    partial class WareHouseInventoryReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WareHouseInventoryReportForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.pictureBoxMax = new System.Windows.Forms.PictureBox();
            this.pictureBoxMin = new System.Windows.Forms.PictureBox();
            this.labelTitle = new DevComponents.DotNetBar.LabelX();
            this.pictureBoxtitle = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonProfit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoss = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonShuaiXin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDaYing = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDaoChu = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.superGridControlShangPing = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.storge = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.daima = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.name = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.model = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.tiaoxingma = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.shengchandate = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.baozhiqi = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.unit = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.zhangcunnumber = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.pandiannumber = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.panyingnumber = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.pankuinumber = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.remark = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumnprice = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.panyingMoney = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.pankuiMoney = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumncode = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.lblzhangcundate = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cboPanDianIdea = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxpandiancode = new System.Windows.Forms.TextBox();
            this.picbpandianBarCode = new System.Windows.Forms.PictureBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxtitle)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbpandianBarCode)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBoxClose);
            this.panel1.Controls.Add(this.pictureBoxMax);
            this.panel1.Controls.Add(this.pictureBoxMin);
            this.panel1.Controls.Add(this.labelTitle);
            this.panel1.Controls.Add(this.pictureBoxtitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1202, 61);
            this.panel1.TabIndex = 3;
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxClose.BackColor = System.Drawing.Color.White;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxClose.Image = global::WSCATProject.Properties.Resources.clo;
            this.pictureBoxClose.Location = new System.Drawing.Point(1144, 28);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxClose.TabIndex = 51;
            this.pictureBoxClose.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxClose, "关闭");
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // pictureBoxMax
            // 
            this.pictureBoxMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMax.BackColor = System.Drawing.Color.White;
            this.pictureBoxMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxMax.Image = global::WSCATProject.Properties.Resources.zuidahua1;
            this.pictureBoxMax.Location = new System.Drawing.Point(1115, 28);
            this.pictureBoxMax.Name = "pictureBoxMax";
            this.pictureBoxMax.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxMax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMax.TabIndex = 49;
            this.pictureBoxMax.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxMax, "最大化");
            this.pictureBoxMax.Click += new System.EventHandler(this.pictureBoxMax_Click);
            this.pictureBoxMax.MouseEnter += new System.EventHandler(this.pictureBoxMax_MouseEnter);
            // 
            // pictureBoxMin
            // 
            this.pictureBoxMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMin.BackColor = System.Drawing.Color.White;
            this.pictureBoxMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxMin.Image = global::WSCATProject.Properties.Resources.small;
            this.pictureBoxMin.Location = new System.Drawing.Point(1088, 28);
            this.pictureBoxMin.Name = "pictureBoxMin";
            this.pictureBoxMin.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMin.TabIndex = 50;
            this.pictureBoxMin.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxMin, "最小化");
            this.pictureBoxMin.Click += new System.EventHandler(this.pictureBoxMin_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.BackColor = System.Drawing.Color.DeepSkyBlue;
            // 
            // 
            // 
            this.labelTitle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelTitle.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(452, 8);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(287, 30);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "商  品  盘  点  报  告  单";
            this.labelTitle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.superGridControlShangPing_MouseDown);
            // 
            // pictureBoxtitle
            // 
            this.pictureBoxtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxtitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBoxtitle.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxtitle.Image")));
            this.pictureBoxtitle.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxtitle.Name = "pictureBoxtitle";
            this.pictureBoxtitle.Size = new System.Drawing.Size(1205, 61);
            this.pictureBoxtitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxtitle.TabIndex = 1;
            this.pictureBoxtitle.TabStop = false;
            this.pictureBoxtitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.superGridControlShangPing_MouseDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonProfit,
            this.toolStripButtonLoss,
            this.toolStripButtonShuaiXin,
            this.toolStripButtonDaYing,
            this.toolStripButtonDaoChu,
            this.toolStripButtonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 61);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1202, 70);
            this.toolStrip1.TabIndex = 51;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.superGridControlShangPing_MouseDown);
            // 
            // toolStripButtonProfit
            // 
            this.toolStripButtonProfit.Image = global::WSCATProject.Properties.Resources.盘点;
            this.toolStripButtonProfit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonProfit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProfit.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripButtonProfit.Name = "toolStripButtonProfit";
            this.toolStripButtonProfit.Size = new System.Drawing.Size(73, 66);
            this.toolStripButtonProfit.Text = "盘盈单(&Y)";
            this.toolStripButtonProfit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonProfit.ToolTipText = "输出盘盈单";
            this.toolStripButtonProfit.Click += new System.EventHandler(this.toolStripButtonProfit_Click);
            // 
            // toolStripButtonLoss
            // 
            this.toolStripButtonLoss.Image = global::WSCATProject.Properties.Resources.盘点;
            this.toolStripButtonLoss.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonLoss.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoss.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripButtonLoss.Name = "toolStripButtonLoss";
            this.toolStripButtonLoss.Size = new System.Drawing.Size(72, 66);
            this.toolStripButtonLoss.Text = "盘亏单(&L)";
            this.toolStripButtonLoss.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonLoss.ToolTipText = "输出盘亏单";
            this.toolStripButtonLoss.Click += new System.EventHandler(this.toolStripButtonLoss_Click);
            // 
            // toolStripButtonShuaiXin
            // 
            this.toolStripButtonShuaiXin.AutoSize = false;
            this.toolStripButtonShuaiXin.Image = global::WSCATProject.Properties.Resources.刷新;
            this.toolStripButtonShuaiXin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonShuaiXin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonShuaiXin.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.toolStripButtonShuaiXin.Name = "toolStripButtonShuaiXin";
            this.toolStripButtonShuaiXin.Size = new System.Drawing.Size(72, 67);
            this.toolStripButtonShuaiXin.Text = "刷新(&F5)";
            this.toolStripButtonShuaiXin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonShuaiXin.ToolTipText = "刷新（F5）";
            // 
            // toolStripButtonDaYing
            // 
            this.toolStripButtonDaYing.Image = global::WSCATProject.Properties.Resources.daying1;
            this.toolStripButtonDaYing.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonDaYing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDaYing.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripButtonDaYing.Name = "toolStripButtonDaYing";
            this.toolStripButtonDaYing.Size = new System.Drawing.Size(60, 66);
            this.toolStripButtonDaYing.Text = "打印(&P)";
            this.toolStripButtonDaYing.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDaYing.ToolTipText = "打印盘点表(Ctrl+P)";
            // 
            // toolStripButtonDaoChu
            // 
            this.toolStripButtonDaoChu.Image = global::WSCATProject.Properties.Resources.countExc;
            this.toolStripButtonDaoChu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonDaoChu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDaoChu.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripButtonDaoChu.Name = "toolStripButtonDaoChu";
            this.toolStripButtonDaoChu.Size = new System.Drawing.Size(93, 66);
            this.toolStripButtonDaoChu.Text = "导出Excel(&T)";
            this.toolStripButtonDaoChu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonDaoChu.ToolTipText = "导出Excel(Ctrl+T)";
            // 
            // toolStripButtonClose
            // 
            this.toolStripButtonClose.Image = global::WSCATProject.Properties.Resources.guanbi;
            this.toolStripButtonClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClose.Name = "toolStripButtonClose";
            this.toolStripButtonClose.Size = new System.Drawing.Size(60, 67);
            this.toolStripButtonClose.Text = "关闭(&X)";
            this.toolStripButtonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonClose.ToolTipText = "关闭(Ctrl+X)";
            this.toolStripButtonClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.superGridControlShangPing);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 131);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1202, 511);
            this.panel2.TabIndex = 52;
            // 
            // superGridControlShangPing
            // 
            this.superGridControlShangPing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControlShangPing.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControlShangPing.Location = new System.Drawing.Point(0, 0);
            this.superGridControlShangPing.Name = "superGridControlShangPing";
            // 
            // 
            // 
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.storge);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.daima);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.name);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.model);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.tiaoxingma);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.shengchandate);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.baozhiqi);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.unit);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.zhangcunnumber);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.pandiannumber);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.panyingnumber);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.pankuinumber);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.remark);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.gridColumnprice);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.panyingMoney);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.pankuiMoney);
            this.superGridControlShangPing.PrimaryGrid.Columns.Add(this.gridColumncode);
            this.superGridControlShangPing.PrimaryGrid.RowHeaderIndexOffset = 1;
            this.superGridControlShangPing.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControlShangPing.Size = new System.Drawing.Size(1202, 511);
            this.superGridControlShangPing.TabIndex = 1;
            this.superGridControlShangPing.Text = "superGridControlShangPing";
            this.superGridControlShangPing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.superGridControlShangPing_MouseDown);
            // 
            // storge
            // 
            this.storge.DataPropertyName = "stockName";
            this.storge.HeaderText = "仓库";
            this.storge.Name = "storge";
            this.storge.ReadOnly = true;
            // 
            // daima
            // 
            this.daima.DataPropertyName = "materialDaima";
            this.daima.HeaderText = "商品代码";
            this.daima.Name = "daima";
            this.daima.ReadOnly = true;
            // 
            // name
            // 
            this.name.DataPropertyName = "materialName";
            this.name.HeaderText = "商品名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // model
            // 
            this.model.DataPropertyName = "materialModel";
            this.model.HeaderText = "规格型号";
            this.model.Name = "model";
            this.model.ReadOnly = true;
            // 
            // tiaoxingma
            // 
            this.tiaoxingma.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.tiaoxingma.DataPropertyName = "barCode";
            this.tiaoxingma.HeaderText = "条形码";
            this.tiaoxingma.Name = "tiaoxingma";
            this.tiaoxingma.ReadOnly = true;
            this.tiaoxingma.Width = 150;
            // 
            // shengchandate
            // 
            this.shengchandate.DataPropertyName = "productionDate";
            this.shengchandate.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.shengchandate.HeaderText = "生产/采购日期";
            this.shengchandate.Name = "shengchandate";
            this.shengchandate.ReadOnly = true;
            this.shengchandate.Width = 80;
            // 
            // baozhiqi
            // 
            this.baozhiqi.DataPropertyName = "qualityDate";
            this.baozhiqi.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.baozhiqi.HeaderText = "保质期（天 ）";
            this.baozhiqi.Name = "baozhiqi";
            this.baozhiqi.ReadOnly = true;
            this.baozhiqi.Width = 50;
            // 
            // unit
            // 
            this.unit.DataPropertyName = "materialUnit";
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 60;
            // 
            // zhangcunnumber
            // 
            this.zhangcunnumber.DataPropertyName = "curNumber";
            this.zhangcunnumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.zhangcunnumber.HeaderText = "账存数量";
            this.zhangcunnumber.Name = "zhangcunnumber";
            this.zhangcunnumber.ReadOnly = true;
            this.zhangcunnumber.Width = 80;
            // 
            // pandiannumber
            // 
            this.pandiannumber.DataPropertyName = "checkNumber";
            this.pandiannumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.pandiannumber.HeaderText = "盘点数量";
            this.pandiannumber.Name = "pandiannumber";
            this.pandiannumber.ReadOnly = true;
            this.pandiannumber.Width = 80;
            // 
            // panyingnumber
            // 
            this.panyingnumber.DataPropertyName = "profitNumber";
            this.panyingnumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.panyingnumber.HeaderText = "盘盈数量";
            this.panyingnumber.Name = "panyingnumber";
            this.panyingnumber.ReadOnly = true;
            this.panyingnumber.Width = 80;
            // 
            // pankuinumber
            // 
            this.pankuinumber.DataPropertyName = "lossNumber";
            this.pankuinumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.pankuinumber.HeaderText = "盘亏数量";
            this.pankuinumber.Name = "pankuinumber";
            this.pankuinumber.ReadOnly = true;
            this.pankuinumber.Width = 80;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 90;
            // 
            // gridColumnprice
            // 
            this.gridColumnprice.DataPropertyName = "price";
            this.gridColumnprice.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.gridColumnprice.HeaderText = "单价";
            this.gridColumnprice.Name = "gridColumnprice";
            this.gridColumnprice.Visible = false;
            // 
            // panyingMoney
            // 
            this.panyingMoney.DataPropertyName = "profitMoney";
            this.panyingMoney.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.panyingMoney.HeaderText = "盘盈金额";
            this.panyingMoney.Name = "panyingMoney";
            this.panyingMoney.Visible = false;
            // 
            // pankuiMoney
            // 
            this.pankuiMoney.DataPropertyName = "lossMoney";
            this.pankuiMoney.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.pankuiMoney.HeaderText = "盘亏金额";
            this.pankuiMoney.Name = "pankuiMoney";
            this.pankuiMoney.Visible = false;
            // 
            // gridColumncode
            // 
            this.gridColumncode.DataPropertyName = "materialCode";
            this.gridColumncode.HeaderText = "商品code";
            this.gridColumncode.Name = "gridColumncode";
            this.gridColumncode.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.bar1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 615);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1202, 27);
            this.panel3.TabIndex = 57;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(177)))), ((int)(((byte)(238)))));
            this.bar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Bottom;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1202, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 55;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            this.bar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.superGridControlShangPing_MouseDown);
            // 
            // lblzhangcundate
            // 
            this.lblzhangcundate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblzhangcundate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.lblzhangcundate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblzhangcundate.Location = new System.Drawing.Point(835, 80);
            this.lblzhangcundate.Name = "lblzhangcundate";
            this.lblzhangcundate.Size = new System.Drawing.Size(75, 23);
            this.lblzhangcundate.TabIndex = 60;
            this.lblzhangcundate.Text = "labelX4";
            this.lblzhangcundate.Visible = false;
            // 
            // labelX3
            // 
            this.labelX3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(770, 80);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(70, 23);
            this.labelX3.TabIndex = 59;
            this.labelX3.Text = "账存日期：";
            this.labelX3.Visible = false;
            // 
            // cboPanDianIdea
            // 
            this.cboPanDianIdea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPanDianIdea.DisplayMember = "Text";
            this.cboPanDianIdea.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPanDianIdea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPanDianIdea.FormattingEnabled = true;
            this.cboPanDianIdea.ItemHeight = 15;
            this.cboPanDianIdea.Location = new System.Drawing.Point(532, 81);
            this.cboPanDianIdea.Name = "cboPanDianIdea";
            this.cboPanDianIdea.Size = new System.Drawing.Size(121, 21);
            this.cboPanDianIdea.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboPanDianIdea.TabIndex = 58;
            this.cboPanDianIdea.SelectedValueChanged += new System.EventHandler(this.cboPanDianIdea_SelectedValueChanged);
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(462, 80);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(67, 23);
            this.labelX2.TabIndex = 61;
            this.labelX2.Text = "盘点方案：";
            // 
            // textBoxpandiancode
            // 
            this.textBoxpandiancode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxpandiancode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxpandiancode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxpandiancode.ForeColor = System.Drawing.Color.Gray;
            this.textBoxpandiancode.Location = new System.Drawing.Point(1023, 111);
            this.textBoxpandiancode.Name = "textBoxpandiancode";
            this.textBoxpandiancode.ReadOnly = true;
            this.textBoxpandiancode.Size = new System.Drawing.Size(141, 14);
            this.textBoxpandiancode.TabIndex = 64;
            // 
            // picbpandianBarCode
            // 
            this.picbpandianBarCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picbpandianBarCode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.picbpandianBarCode.Location = new System.Drawing.Point(1023, 65);
            this.picbpandianBarCode.Name = "picbpandianBarCode";
            this.picbpandianBarCode.Size = new System.Drawing.Size(141, 40);
            this.picbpandianBarCode.TabIndex = 63;
            this.picbpandianBarCode.TabStop = false;
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(977, 61);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(43, 23);
            this.labelX1.TabIndex = 62;
            this.labelX1.Text = "单号：";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // WareHouseInventoryReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 642);
            this.Controls.Add(this.textBoxpandiancode);
            this.Controls.Add(this.picbpandianBarCode);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.lblzhangcundate);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.cboPanDianIdea);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WareHouseInventoryReportForm";
            this.Text = "商品盘点报告单";
            this.Activated += new System.EventHandler(this.WareHouseInventoryReportForm_Activated);
            this.Load += new System.EventHandler(this.WareHouseInventoryReportForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WareHouseInventoryReportForm_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxtitle)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbpandianBarCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox pictureBoxtitle;
        protected DevComponents.DotNetBar.LabelX labelTitle;
        protected System.Windows.Forms.PictureBox pictureBoxClose;
        public System.Windows.Forms.PictureBox pictureBoxMax;
        public System.Windows.Forms.PictureBox pictureBoxMin;
        private System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton toolStripButtonProfit;
        protected System.Windows.Forms.ToolStripButton toolStripButtonLoss;
        protected System.Windows.Forms.ToolStripButton toolStripButtonShuaiXin;
        protected System.Windows.Forms.ToolStripButton toolStripButtonDaYing;
        protected System.Windows.Forms.ToolStripButton toolStripButtonDaoChu;
        private System.Windows.Forms.ToolStripButton toolStripButtonClose;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControlShangPing;
        private DevComponents.DotNetBar.SuperGrid.GridColumn storge;
        private DevComponents.DotNetBar.SuperGrid.GridColumn daima;
        private DevComponents.DotNetBar.SuperGrid.GridColumn name;
        private DevComponents.DotNetBar.SuperGrid.GridColumn model;
        private DevComponents.DotNetBar.SuperGrid.GridColumn tiaoxingma;
        private DevComponents.DotNetBar.SuperGrid.GridColumn shengchandate;
        private DevComponents.DotNetBar.SuperGrid.GridColumn baozhiqi;
        private DevComponents.DotNetBar.SuperGrid.GridColumn unit;
        private DevComponents.DotNetBar.SuperGrid.GridColumn zhangcunnumber;
        private DevComponents.DotNetBar.SuperGrid.GridColumn pandiannumber;
        private DevComponents.DotNetBar.SuperGrid.GridColumn panyingnumber;
        private DevComponents.DotNetBar.SuperGrid.GridColumn pankuinumber;
        private DevComponents.DotNetBar.SuperGrid.GridColumn remark;
        private System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.LabelX lblzhangcundate;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboPanDianIdea;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBoxpandiancode;
        private System.Windows.Forms.PictureBox picbpandianBarCode;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumnprice;
        private DevComponents.DotNetBar.SuperGrid.GridColumn panyingMoney;
        private DevComponents.DotNetBar.SuperGrid.GridColumn pankuiMoney;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumncode;
    }
}