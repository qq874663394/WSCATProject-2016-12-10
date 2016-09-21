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
            this.toolStripButtonnew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonsave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonhou = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonshuaixin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtondayin = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtondaochu = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxtitle)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
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
            this.labelTitle.Text = "商品盘点表";
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
            this.toolStripButtonnew,
            this.toolStripButtonsave,
            this.toolStripButtonhou,
            this.toolStripButtonshuaixin,
            this.toolStripButtondayin,
            this.toolStripButtondaochu,
            this.toolStripButton6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 61);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1202, 60);
            this.toolStrip1.TabIndex = 50;
            this.toolStrip1.Text = "toolStrip1";
            this.toolTip1.SetToolTip(this.toolStrip1, "排序");
            this.toolStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WareHouseInventoryForm_MouseDown);
            // 
            // toolStripButtonnew
            // 
            this.toolStripButtonnew.Image = global::WSCATProject.Properties.Resources.tianjia;
            this.toolStripButtonnew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonnew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonnew.Name = "toolStripButtonnew";
            this.toolStripButtonnew.Size = new System.Drawing.Size(41, 57);
            this.toolStripButtonnew.Text = "添加";
            this.toolStripButtonnew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonnew.ToolTipText = "添加商品";
            // 
            // toolStripButtonsave
            // 
            this.toolStripButtonsave.Image = global::WSCATProject.Properties.Resources.shanchu;
            this.toolStripButtonsave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonsave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonsave.Name = "toolStripButtonsave";
            this.toolStripButtonsave.Size = new System.Drawing.Size(41, 57);
            this.toolStripButtonsave.Text = "清除";
            this.toolStripButtonsave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonsave.ToolTipText = "清除盘点数量（Del）";
            // 
            // toolStripButtonhou
            // 
            this.toolStripButtonhou.Image = global::WSCATProject.Properties.Resources.盘点;
            this.toolStripButtonhou.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonhou.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonhou.Name = "toolStripButtonhou";
            this.toolStripButtonhou.Size = new System.Drawing.Size(41, 57);
            this.toolStripButtonhou.Text = "编制";
            this.toolStripButtonhou.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonhou.ToolTipText = "编制盘点报告";
            // 
            // toolStripButtonshuaixin
            // 
            this.toolStripButtonshuaixin.AutoSize = false;
            this.toolStripButtonshuaixin.Image = global::WSCATProject.Properties.Resources.刷新;
            this.toolStripButtonshuaixin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonshuaixin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonshuaixin.Name = "toolStripButtonshuaixin";
            this.toolStripButtonshuaixin.Size = new System.Drawing.Size(41, 57);
            this.toolStripButtonshuaixin.Text = "刷新";
            this.toolStripButtonshuaixin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtonshuaixin.ToolTipText = "刷新（F5）";
            // 
            // toolStripButtondayin
            // 
            this.toolStripButtondayin.Image = global::WSCATProject.Properties.Resources.daying1;
            this.toolStripButtondayin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtondayin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtondayin.Name = "toolStripButtondayin";
            this.toolStripButtondayin.Size = new System.Drawing.Size(41, 57);
            this.toolStripButtondayin.Text = "打印";
            this.toolStripButtondayin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtondayin.ToolTipText = "打印盘点表(Ctrl+P)";
            // 
            // toolStripButtondaochu
            // 
            this.toolStripButtondaochu.Image = global::WSCATProject.Properties.Resources.countExc;
            this.toolStripButtondaochu.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtondaochu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtondaochu.Name = "toolStripButtondaochu";
            this.toolStripButtondaochu.Size = new System.Drawing.Size(75, 57);
            this.toolStripButtondaochu.Text = "导出Excel";
            this.toolStripButtondaochu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButtondaochu.ToolTipText = "导出Excel(Ctrl+T)";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = global::WSCATProject.Properties.Resources.guanbi;
            this.toolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(41, 57);
            this.toolStripButton6.Text = "关闭";
            this.toolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton6.ToolTipText = "关闭(Ctrl+X)";
            this.toolStripButton6.Click += new System.EventHandler(this.pictureBox8_Click);
            // 
            // labelX4
            // 
            this.labelX4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(1080, 78);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(75, 23);
            this.labelX4.TabIndex = 54;
            this.labelX4.Text = "labelX4";
            // 
            // labelX3
            // 
            this.labelX3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(1015, 79);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(70, 23);
            this.labelX3.TabIndex = 53;
            this.labelX3.Text = "账存日期：";
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 15;
            this.comboBoxEx1.Location = new System.Drawing.Point(787, 79);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 52;
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(726, 79);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 51;
            this.labelX2.Text = "盘点方案：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.superGridControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1202, 494);
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
            this.superGridControl1.Size = new System.Drawing.Size(1202, 494);
            this.superGridControl1.TabIndex = 1;
            this.superGridControl1.Text = "superGridControl1";
            this.toolTip1.SetToolTip(this.superGridControl1, "添加商品");
            this.superGridControl1.CellValidated += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellValidatedEventArgs>(this.superGridControl1_CellValidated);
            this.superGridControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WareHouseInventoryForm_MouseDown);
            // 
            // storge
            // 
            this.storge.HeaderText = "仓库";
            this.storge.Name = "storge";
            this.storge.ReadOnly = true;
            // 
            // daima
            // 
            this.daima.HeaderText = "商品代码";
            this.daima.Name = "daima";
            this.daima.ReadOnly = true;
            // 
            // name
            // 
            this.name.HeaderText = "商品名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // model
            // 
            this.model.HeaderText = "规格型号";
            this.model.Name = "model";
            this.model.ReadOnly = true;
            // 
            // tiaoxingma
            // 
            this.tiaoxingma.HeaderText = "条形码";
            this.tiaoxingma.Name = "tiaoxingma";
            this.tiaoxingma.ReadOnly = true;
            this.tiaoxingma.Width = 150;
            // 
            // shengchandate
            // 
            this.shengchandate.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.shengchandate.HeaderText = "生产/采购日期";
            this.shengchandate.Name = "shengchandate";
            this.shengchandate.ReadOnly = true;
            this.shengchandate.Width = 80;
            // 
            // baozhiqi
            // 
            this.baozhiqi.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.baozhiqi.HeaderText = "保质期（天 ）";
            this.baozhiqi.Name = "baozhiqi";
            this.baozhiqi.ReadOnly = true;
            this.baozhiqi.Width = 50;
            // 
            // unit
            // 
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            this.unit.ReadOnly = true;
            this.unit.Width = 60;
            // 
            // zhangcunnumber
            // 
            this.zhangcunnumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.zhangcunnumber.HeaderText = "账存数量";
            this.zhangcunnumber.Name = "zhangcunnumber";
            this.zhangcunnumber.ReadOnly = true;
            this.zhangcunnumber.Width = 80;
            // 
            // pandiannumber
            // 
            this.pandiannumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.pandiannumber.HeaderText = "盘点数量";
            this.pandiannumber.Name = "pandiannumber";
            this.pandiannumber.Width = 80;
            // 
            // panyingnumber
            // 
            this.panyingnumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.panyingnumber.HeaderText = "盘盈数量";
            this.panyingnumber.Name = "panyingnumber";
            this.panyingnumber.ReadOnly = true;
            this.panyingnumber.Width = 80;
            // 
            // pankuinumber
            // 
            this.pankuinumber.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridDoubleInputEditControl);
            this.pankuinumber.HeaderText = "盘亏数量";
            this.pankuinumber.Name = "pankuinumber";
            this.pankuinumber.ReadOnly = true;
            this.pankuinumber.Width = 80;
            // 
            // remark
            // 
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            this.remark.Width = 90;
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
            // pictureBox8
            // 
            this.pictureBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox8.BackColor = System.Drawing.Color.White;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox8.Image = global::WSCATProject.Properties.Resources.clo;
            this.pictureBox8.Location = new System.Drawing.Point(1137, 27);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(20, 20);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 48;
            this.pictureBox8.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox8, "关闭");
            this.pictureBox8.Click += new System.EventHandler(this.pictureBox8_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox6.BackColor = System.Drawing.Color.White;
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox6.Image = global::WSCATProject.Properties.Resources.zuidahua1;
            this.pictureBox6.Location = new System.Drawing.Point(1108, 27);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(20, 20);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 46;
            this.pictureBox6.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox6, "最大化");
            this.pictureBox6.Click += new System.EventHandler(this.pictureBox6_Click);
            this.pictureBox6.MouseEnter += new System.EventHandler(this.pictureBox6_MouseEnter);
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.BackColor = System.Drawing.Color.White;
            this.pictureBox7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox7.Image = global::WSCATProject.Properties.Resources.small;
            this.pictureBox7.Location = new System.Drawing.Point(1081, 27);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(20, 20);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 47;
            this.pictureBox7.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox7, "最小化");
            this.pictureBox7.Click += new System.EventHandler(this.pictureBox7_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = global::WSCATProject.Properties.Resources.caozuo;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton7.Text = "编制";
            // 
            // WareHouseInventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 642);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.comboBoxEx1);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox7);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        protected System.Windows.Forms.ToolStripButton toolStripButton7;
        public System.Windows.Forms.PictureBox pictureBoxtitle;
        protected DevComponents.DotNetBar.LabelX labelTitle;
        protected System.Windows.Forms.PictureBox pictureBox8;
        public System.Windows.Forms.PictureBox pictureBox7;
        public System.Windows.Forms.PictureBox pictureBox6;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton toolStripButtonshuaixin;
        protected System.Windows.Forms.ToolStripButton toolStripButtonnew;
        protected System.Windows.Forms.ToolStripButton toolStripButtonsave;
        protected System.Windows.Forms.ToolStripButton toolStripButtondayin;
        protected System.Windows.Forms.ToolStripButton toolStripButtondaochu;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
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
        protected System.Windows.Forms.ToolStripButton toolStripButtonhou;
    }
}