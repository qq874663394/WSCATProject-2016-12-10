namespace WSCATProject.Sales
{
    partial class SalesOrderReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalesOrderReportForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.pictureBoxMax = new System.Windows.Forms.PictureBox();
            this.pictureBoxMin = new System.Windows.Forms.PictureBox();
            this.labelTitle = new DevComponents.DotNetBar.LabelX();
            this.pictureBoxtitle = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonProfit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonshuaixin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtondayin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtondaochu = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClose = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxtitle)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(1186, 61);
            this.panel1.TabIndex = 4;
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxClose.BackColor = System.Drawing.Color.White;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxClose.Image = global::WSCATProject.Properties.Resources.clo;
            this.pictureBoxClose.Location = new System.Drawing.Point(1128, 28);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxClose.TabIndex = 51;
            this.pictureBoxClose.TabStop = false;
            // 
            // pictureBoxMax
            // 
            this.pictureBoxMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMax.BackColor = System.Drawing.Color.White;
            this.pictureBoxMax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxMax.Image = global::WSCATProject.Properties.Resources.zuidahua1;
            this.pictureBoxMax.Location = new System.Drawing.Point(1099, 28);
            this.pictureBoxMax.Name = "pictureBoxMax";
            this.pictureBoxMax.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxMax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMax.TabIndex = 49;
            this.pictureBoxMax.TabStop = false;
            // 
            // pictureBoxMin
            // 
            this.pictureBoxMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMin.BackColor = System.Drawing.Color.White;
            this.pictureBoxMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxMin.Image = global::WSCATProject.Properties.Resources.small;
            this.pictureBoxMin.Location = new System.Drawing.Point(1072, 28);
            this.pictureBoxMin.Name = "pictureBoxMin";
            this.pictureBoxMin.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMin.TabIndex = 50;
            this.pictureBoxMin.TabStop = false;
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
            this.labelTitle.Size = new System.Drawing.Size(271, 30);
            this.labelTitle.TabIndex = 4;
            this.labelTitle.Text = "销  售  订  单  报  告";
            this.labelTitle.TextAlignment = System.Drawing.StringAlignment.Center;
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
            this.pictureBoxtitle.Size = new System.Drawing.Size(1189, 61);
            this.pictureBoxtitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxtitle.TabIndex = 1;
            this.pictureBoxtitle.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonProfit,
            this.toolStripButtonshuaixin,
            this.toolStripButtondayin,
            this.toolStripButtondaochu,
            this.toolStripButtonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 61);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1186, 70);
            this.toolStrip1.TabIndex = 52;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonProfit
            // 
            this.toolStripButtonProfit.Image = global::WSCATProject.Properties.Resources.盘点;
            this.toolStripButtonProfit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonProfit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProfit.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripButtonProfit.Name = "toolStripButtonProfit";
            this.toolStripButtonProfit.Size = new System.Drawing.Size(41, 66);
            this.toolStripButtonProfit.Text = "查看";
            this.toolStripButtonProfit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonProfit.ToolTipText = "输出盘盈单";
            // 
            // toolStripButtonshuaixin
            // 
            this.toolStripButtonshuaixin.AutoSize = false;
            this.toolStripButtonshuaixin.Image = global::WSCATProject.Properties.Resources.刷新;
            this.toolStripButtonshuaixin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonshuaixin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonshuaixin.Margin = new System.Windows.Forms.Padding(0, 5, 0, -2);
            this.toolStripButtonshuaixin.Name = "toolStripButtonshuaixin";
            this.toolStripButtonshuaixin.Size = new System.Drawing.Size(41, 67);
            this.toolStripButtonshuaixin.Text = "刷新";
            this.toolStripButtonshuaixin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonshuaixin.ToolTipText = "刷新（F5）";
            // 
            // toolStripButtondayin
            // 
            this.toolStripButtondayin.Image = global::WSCATProject.Properties.Resources.daying1;
            this.toolStripButtondayin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtondayin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtondayin.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripButtondayin.Name = "toolStripButtondayin";
            this.toolStripButtondayin.Size = new System.Drawing.Size(41, 66);
            this.toolStripButtondayin.Text = "打印";
            this.toolStripButtondayin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtondayin.ToolTipText = "打印盘点表(Ctrl+P)";
            // 
            // toolStripButtondaochu
            // 
            this.toolStripButtondaochu.Image = global::WSCATProject.Properties.Resources.countExc;
            this.toolStripButtondaochu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtondaochu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtondaochu.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripButtondaochu.Name = "toolStripButtondaochu";
            this.toolStripButtondaochu.Size = new System.Drawing.Size(75, 66);
            this.toolStripButtondaochu.Text = "导出Excel";
            this.toolStripButtondaochu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtondaochu.ToolTipText = "导出Excel(Ctrl+T)";
            // 
            // toolStripButtonClose
            // 
            this.toolStripButtonClose.Image = global::WSCATProject.Properties.Resources.guanbi;
            this.toolStripButtonClose.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClose.Name = "toolStripButtonClose";
            this.toolStripButtonClose.Size = new System.Drawing.Size(41, 67);
            this.toolStripButtonClose.Text = "关闭";
            this.toolStripButtonClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonClose.ToolTipText = "关闭(Ctrl+X)";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.superGridControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 131);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1186, 473);
            this.panel2.TabIndex = 53;
            // 
            // superGridControl1
            // 
            this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.Location = new System.Drawing.Point(0, 0);
            this.superGridControl1.Name = "superGridControl1";
            // 
            // 
            // 
            this.superGridControl1.PrimaryGrid.Columns.Add(this.storge);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.daima);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.name);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.model);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.tiaoxingma);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.shengchandate);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.baozhiqi);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.unit);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.zhangcunnumber);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.pandiannumber);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.panyingnumber);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.pankuinumber);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.remark);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumnprice);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.panyingMoney);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.pankuiMoney);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumncode);
            this.superGridControl1.Size = new System.Drawing.Size(1186, 473);
            this.superGridControl1.TabIndex = 1;
            this.superGridControl1.Text = "superGridControl1";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(983, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 12);
            this.label1.TabIndex = 54;
            this.label1.Text = "共计：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(1031, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 55;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(1102, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 56;
            this.label3.Text = "label3";
            // 
            // SalesOrderReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 604);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SalesOrderReportForm";
            this.Text = "销售订单报告";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxtitle)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.PictureBox pictureBoxClose;
        public System.Windows.Forms.PictureBox pictureBoxMax;
        public System.Windows.Forms.PictureBox pictureBoxMin;
        protected DevComponents.DotNetBar.LabelX labelTitle;
        public System.Windows.Forms.PictureBox pictureBoxtitle;
        private System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton toolStripButtonProfit;
        protected System.Windows.Forms.ToolStripButton toolStripButtonshuaixin;
        protected System.Windows.Forms.ToolStripButton toolStripButtondayin;
        protected System.Windows.Forms.ToolStripButton toolStripButtondaochu;
        private System.Windows.Forms.ToolStripButton toolStripButtonClose;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
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
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumnprice;
        private DevComponents.DotNetBar.SuperGrid.GridColumn panyingMoney;
        private DevComponents.DotNetBar.SuperGrid.GridColumn pankuiMoney;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumncode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}