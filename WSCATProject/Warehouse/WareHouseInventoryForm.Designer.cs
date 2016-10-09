namespace WSCATProject.Warehouse
{
    partial class WareHouseInventoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WareHouseInventoryForm));
            this.labelTitle = new DevComponents.DotNetBar.LabelX();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxtitle = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonadd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonclear = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonbianzhi = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonshuaixin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtondayin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtondaochu = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.lblzhangcundate = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cbopandianidea = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
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
            this.gridColumncode = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumnprice = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.panyingMoney = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.pankuiMoney = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.pictureBoxMax = new System.Windows.Forms.PictureBox();
            this.pictureBoxMin = new System.Windows.Forms.PictureBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.picbpandianBarCode = new System.Windows.Forms.PictureBox();
            this.textBoxpandiancode = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxtitle)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbpandianBarCode)).BeginInit();
            this.SuspendLayout();
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
            this.labelTitle.Location = new System.Drawing.Point(509, 8);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(210, 30);
            this.labelTitle.TabIndex = 3;
            this.labelTitle.Text = "商  品  盘  点  表";
            this.labelTitle.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WareHouseInventoryForm_MouseDown);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2010Blue;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))), System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199))))));
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBoxtitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1202, 61);
            this.panel1.TabIndex = 2;
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
            this.pictureBoxtitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WareHouseInventoryForm_MouseDown);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonadd,
            this.toolStripButtonclear,
            this.toolStripButtonbianzhi,
            this.toolStripButtonshuaixin,
            this.toolStripButtondayin,
            this.toolStripButtondaochu,
            this.toolStripButton6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 61);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1202, 70);
            this.toolStrip1.TabIndex = 50;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WareHouseInventoryForm_MouseDown);
            // 
            // toolStripButtonadd
            // 
            this.toolStripButtonadd.Image = global::WSCATProject.Properties.Resources.tianjia;
            this.toolStripButtonadd.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonadd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonadd.Name = "toolStripButtonadd";
            this.toolStripButtonadd.Size = new System.Drawing.Size(41, 67);
            this.toolStripButtonadd.Text = "添加";
            this.toolStripButtonadd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonadd.ToolTipText = "添加商品";
            // 
            // toolStripButtonclear
            // 
            this.toolStripButtonclear.Image = global::WSCATProject.Properties.Resources.shanchu;
            this.toolStripButtonclear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonclear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonclear.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripButtonclear.Name = "toolStripButtonclear";
            this.toolStripButtonclear.Size = new System.Drawing.Size(41, 66);
            this.toolStripButtonclear.Text = "清除";
            this.toolStripButtonclear.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonclear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonclear.ToolTipText = "清除盘点数量（Del）";
            // 
            // toolStripButtonbianzhi
            // 
            this.toolStripButtonbianzhi.Image = global::WSCATProject.Properties.Resources.Paste;
            this.toolStripButtonbianzhi.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonbianzhi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonbianzhi.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripButtonbianzhi.Name = "toolStripButtonbianzhi";
            this.toolStripButtonbianzhi.Size = new System.Drawing.Size(41, 66);
            this.toolStripButtonbianzhi.Text = "编制";
            this.toolStripButtonbianzhi.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtonbianzhi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonbianzhi.ToolTipText = "编制盘点报告";
            this.toolStripButtonbianzhi.Click += new System.EventHandler(this.toolStripButtonbianzhi_Click);
            // 
            // toolStripButtonshuaixin
            // 
            this.toolStripButtonshuaixin.AutoSize = false;
            this.toolStripButtonshuaixin.Image = global::WSCATProject.Properties.Resources.刷新;
            this.toolStripButtonshuaixin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonshuaixin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonshuaixin.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.toolStripButtonshuaixin.Name = "toolStripButtonshuaixin";
            this.toolStripButtonshuaixin.Size = new System.Drawing.Size(41, 67);
            this.toolStripButtonshuaixin.Text = "刷新";
            this.toolStripButtonshuaixin.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
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
            this.toolStripButtondayin.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
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
            this.toolStripButtondaochu.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButtondaochu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtondaochu.ToolTipText = "导出Excel(Ctrl+T)";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = global::WSCATProject.Properties.Resources.guanbi;
            this.toolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(41, 66);
            this.toolStripButton6.Text = "关闭";
            this.toolStripButton6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton6.ToolTipText = "关闭(Ctrl+X)";
            this.toolStripButton6.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // lblzhangcundate
            // 
            this.lblzhangcundate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblzhangcundate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.lblzhangcundate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblzhangcundate.Location = new System.Drawing.Point(806, 79);
            this.lblzhangcundate.Name = "lblzhangcundate";
            this.lblzhangcundate.Size = new System.Drawing.Size(75, 23);
            this.lblzhangcundate.TabIndex = 54;
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
            this.labelX3.Location = new System.Drawing.Point(741, 80);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(70, 23);
            this.labelX3.TabIndex = 53;
            this.labelX3.Text = "账存日期：";
            this.labelX3.Visible = false;
            // 
            // cbopandianidea
            // 
            this.cbopandianidea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbopandianidea.DisplayMember = "Text";
            this.cbopandianidea.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbopandianidea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopandianidea.FormattingEnabled = true;
            this.cbopandianidea.ItemHeight = 15;
            this.cbopandianidea.Location = new System.Drawing.Point(504, 81);
            this.cbopandianidea.Name = "cbopandianidea";
            this.cbopandianidea.Size = new System.Drawing.Size(121, 21);
            this.cbopandianidea.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbopandianidea.TabIndex = 52;
            this.cbopandianidea.SelectedValueChanged += new System.EventHandler(this.cbopandianidea_SelectedValueChanged);
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(443, 81);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 51;
            this.labelX2.Text = "盘点方案：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.superGridControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 131);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1202, 484);
            this.panel2.TabIndex = 9;
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
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumncode);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumnprice);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.panyingMoney);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.pankuiMoney);
            this.superGridControl1.Size = new System.Drawing.Size(1202, 484);
            this.superGridControl1.TabIndex = 1;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.CellValidated += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellValidatedEventArgs>(this.superGridControl1_CellValidated);
            this.superGridControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WareHouseInventoryForm_MouseDown);
            // 
            // storge
            // 
            this.storge.DataPropertyName = "storageName";
            this.storge.HeaderText = "仓库";
            this.storge.Name = "storge";
            this.storge.ReadOnly = true;
            // 
            // daima
            // 
            this.daima.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.daima.DataPropertyName = "materialDaima";
            this.daima.HeaderText = "商品代码";
            this.daima.Name = "daima";
            this.daima.ReadOnly = true;
            // 
            // name
            // 
            this.name.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "商品名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // model
            // 
            this.model.DataPropertyName = "model";
            this.model.HeaderText = "规格型号";
            this.model.Name = "model";
            this.model.ReadOnly = true;
            this.model.Width = 80;
            // 
            // tiaoxingma
            // 
            this.tiaoxingma.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.tiaoxingma.DataPropertyName = "barcode";
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
            // 
            // baozhiqi
            // 
            this.baozhiqi.DataPropertyName = "qualityDate";
            this.baozhiqi.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.baozhiqi.HeaderText = "  保质期   （天）";
            this.baozhiqi.Name = "baozhiqi";
            this.baozhiqi.ReadOnly = true;
            this.baozhiqi.Width = 70;
            // 
            // unit
            // 
            this.unit.DataPropertyName = "unit";
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 80;
            // 
            // zhangcunnumber
            // 
            this.zhangcunnumber.DataPropertyName = "number";
            this.zhangcunnumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.zhangcunnumber.HeaderText = "账存数量";
            this.zhangcunnumber.Name = "zhangcunnumber";
            this.zhangcunnumber.ReadOnly = true;
            // 
            // pandiannumber
            // 
            this.pandiannumber.DataPropertyName = "checkNumber";
            this.pandiannumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.pandiannumber.HeaderText = "盘点数量";
            this.pandiannumber.Name = "pandiannumber";
            // 
            // panyingnumber
            // 
            this.panyingnumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.panyingnumber.HeaderText = "盘盈数量";
            this.panyingnumber.Name = "panyingnumber";
            this.panyingnumber.ReadOnly = true;
            this.panyingnumber.Visible = false;
            // 
            // pankuinumber
            // 
            this.pankuinumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.pankuinumber.HeaderText = "盘亏数量";
            this.pankuinumber.Name = "pankuinumber";
            this.pankuinumber.ReadOnly = true;
            this.pankuinumber.Visible = false;
            // 
            // remark
            // 
            this.remark.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.remark.DataPropertyName = "remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 90;
            // 
            // gridColumncode
            // 
            this.gridColumncode.DataPropertyName = "code";
            this.gridColumncode.HeaderText = "商品code";
            this.gridColumncode.Name = "gridColumncode";
            this.gridColumncode.Visible = false;
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
            this.panyingMoney.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.panyingMoney.HeaderText = "盘盈金额";
            this.panyingMoney.Name = "panyingMoney";
            this.panyingMoney.Visible = false;
            // 
            // pankuiMoney
            // 
            this.pankuiMoney.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.pankuiMoney.HeaderText = "盘亏金额";
            this.pankuiMoney.Name = "pankuiMoney";
            this.pankuiMoney.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.bar1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 615);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1202, 27);
            this.panel3.TabIndex = 56;
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
            this.bar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WareHouseInventoryForm_MouseDown);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxClose.BackColor = System.Drawing.Color.White;
            this.pictureBoxClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxClose.Image = global::WSCATProject.Properties.Resources.clo;
            this.pictureBoxClose.Location = new System.Drawing.Point(1137, 27);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxClose.TabIndex = 48;
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
            this.pictureBoxMax.Location = new System.Drawing.Point(1108, 27);
            this.pictureBoxMax.Name = "pictureBoxMax";
            this.pictureBoxMax.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxMax.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMax.TabIndex = 46;
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
            this.pictureBoxMin.Location = new System.Drawing.Point(1081, 27);
            this.pictureBoxMin.Name = "pictureBoxMin";
            this.pictureBoxMin.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxMin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMin.TabIndex = 47;
            this.pictureBoxMin.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxMin, "最小化");
            this.pictureBoxMin.Click += new System.EventHandler(this.pictureBoxMin_Click);
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(970, 62);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(43, 23);
            this.labelX1.TabIndex = 57;
            this.labelX1.Text = "单号：";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // picbpandianBarCode
            // 
            this.picbpandianBarCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picbpandianBarCode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.picbpandianBarCode.Location = new System.Drawing.Point(1016, 86);
            this.picbpandianBarCode.Name = "picbpandianBarCode";
            this.picbpandianBarCode.Size = new System.Drawing.Size(141, 40);
            this.picbpandianBarCode.TabIndex = 59;
            this.picbpandianBarCode.TabStop = false;
            // 
            // textBoxpandiancode
            // 
            this.textBoxpandiancode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxpandiancode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxpandiancode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxpandiancode.Location = new System.Drawing.Point(1016, 66);
            this.textBoxpandiancode.Name = "textBoxpandiancode";
            this.textBoxpandiancode.ReadOnly = true;
            this.textBoxpandiancode.Size = new System.Drawing.Size(141, 14);
            this.textBoxpandiancode.TabIndex = 60;
            // 
            // WareHouseInventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 642);
            this.Controls.Add(this.textBoxpandiancode);
            this.Controls.Add(this.picbpandianBarCode);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblzhangcundate);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.cbopandianidea);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pictureBoxClose);
            this.Controls.Add(this.pictureBoxMax);
            this.Controls.Add(this.pictureBoxMin);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WareHouseInventoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "录入盘点数据";
            this.Load += new System.EventHandler(this.WareHouseInventoryForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WareHouseInventoryForm_MouseDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxtitle)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbpandianBarCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.PictureBox pictureBoxtitle;
        protected DevComponents.DotNetBar.LabelX labelTitle;
        protected System.Windows.Forms.PictureBox pictureBoxClose;
        public System.Windows.Forms.PictureBox pictureBoxMin;
        public System.Windows.Forms.PictureBox pictureBoxMax;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton toolStripButtonshuaixin;
        protected System.Windows.Forms.ToolStripButton toolStripButtonadd;
        protected System.Windows.Forms.ToolStripButton toolStripButtonclear;
        protected System.Windows.Forms.ToolStripButton toolStripButtondayin;
        protected System.Windows.Forms.ToolStripButton toolStripButtondaochu;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private DevComponents.DotNetBar.LabelX lblzhangcundate;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbopandianidea;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn storge;
        private DevComponents.DotNetBar.SuperGrid.GridColumn daima;
        private DevComponents.DotNetBar.SuperGrid.GridColumn name;
        private DevComponents.DotNetBar.SuperGrid.GridColumn model;
        private DevComponents.DotNetBar.SuperGrid.GridColumn shengchandate;
        private DevComponents.DotNetBar.SuperGrid.GridColumn baozhiqi;
        private DevComponents.DotNetBar.SuperGrid.GridColumn unit;
        private DevComponents.DotNetBar.SuperGrid.GridColumn tiaoxingma;
        private DevComponents.DotNetBar.SuperGrid.GridColumn zhangcunnumber;
        private DevComponents.DotNetBar.SuperGrid.GridColumn pandiannumber;
        private DevComponents.DotNetBar.SuperGrid.GridColumn remark;
        private DevComponents.DotNetBar.SuperGrid.GridColumn panyingnumber;
        private DevComponents.DotNetBar.SuperGrid.GridColumn pankuinumber;
        private System.Windows.Forms.Panel panel3;
        protected System.Windows.Forms.ToolStripButton toolStripButtonbianzhi;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.PictureBox picbpandianBarCode;
        private System.Windows.Forms.TextBox textBoxpandiancode;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumncode;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumnprice;
        private DevComponents.DotNetBar.SuperGrid.GridColumn panyingMoney;
        private DevComponents.DotNetBar.SuperGrid.GridColumn pankuiMoney;
    }
}