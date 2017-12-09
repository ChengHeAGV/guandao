namespace guandao
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox_location = new System.Windows.Forms.PictureBox();
            this.pictureBoxForce = new System.Windows.Forms.PictureBox();
            this.pictureBoxBackGround = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.textBox_RealTimeXY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView_Draw = new System.Windows.Forms.DataGridView();
            this.dataGridView_DrawDelete = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.button_DrawRoute = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button_add_point = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_loadmap = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_AddY = new System.Windows.Forms.TextBox();
            this.button_saveRoute = new System.Windows.Forms.Button();
            this.textBox_AddX = new System.Windows.Forms.TextBox();
            this.label_True_XY = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label5_LED = new System.Windows.Forms.Label();
            this.button1_openclose = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2_BaudRate = new System.Windows.Forms.ComboBox();
            this.comboBox1_PortName = new System.Windows.Forms.ComboBox();
            this.label_Grid = new System.Windows.Forms.Label();
            this.label_XY = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.form1BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_location)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Draw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DrawDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox_location);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxForce);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxBackGround);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1471, 728);
            this.splitContainer1.SplitterDistance = 1169;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureBox_location
            // 
            this.pictureBox_location.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_location.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox_location.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_location.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_location.Name = "pictureBox_location";
            this.pictureBox_location.Size = new System.Drawing.Size(1169, 728);
            this.pictureBox_location.TabIndex = 6;
            this.pictureBox_location.TabStop = false;
            this.pictureBox_location.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_location_MouseClick);
            this.pictureBox_location.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_location_MouseDown);
            this.pictureBox_location.MouseEnter += new System.EventHandler(this.pictureBox_location_MouseEnter);
            this.pictureBox_location.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_location_MouseMove);
            this.pictureBox_location.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_location_MouseUp);
            // 
            // pictureBoxForce
            // 
            this.pictureBoxForce.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxForce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxForce.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxForce.Name = "pictureBoxForce";
            this.pictureBoxForce.Size = new System.Drawing.Size(1169, 728);
            this.pictureBoxForce.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxForce.TabIndex = 8;
            this.pictureBoxForce.TabStop = false;
            // 
            // pictureBoxBackGround
            // 
            this.pictureBoxBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxBackGround.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBackGround.Name = "pictureBoxBackGround";
            this.pictureBoxBackGround.Size = new System.Drawing.Size(1169, 728);
            this.pictureBoxBackGround.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxBackGround.TabIndex = 0;
            this.pictureBoxBackGround.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textBox_RealTimeXY);
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            this.splitContainer2.Panel1.Controls.Add(this.label9);
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView_Draw);
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView_DrawDelete);
            this.splitContainer2.Panel1.Controls.Add(this.label8);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button_DrawRoute);
            this.splitContainer2.Panel2.Controls.Add(this.label10);
            this.splitContainer2.Panel2.Controls.Add(this.button_add_point);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.button_Clear);
            this.splitContainer2.Panel2.Controls.Add(this.button_loadmap);
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.textBox_AddY);
            this.splitContainer2.Panel2.Controls.Add(this.button_saveRoute);
            this.splitContainer2.Panel2.Controls.Add(this.textBox_AddX);
            this.splitContainer2.Size = new System.Drawing.Size(298, 728);
            this.splitContainer2.SplitterDistance = 259;
            this.splitContainer2.TabIndex = 1;
            // 
            // textBox_RealTimeXY
            // 
            this.textBox_RealTimeXY.Font = new System.Drawing.Font("宋体", 16F);
            this.textBox_RealTimeXY.Location = new System.Drawing.Point(6, 152);
            this.textBox_RealTimeXY.Multiline = true;
            this.textBox_RealTimeXY.Name = "textBox_RealTimeXY";
            this.textBox_RealTimeXY.Size = new System.Drawing.Size(280, 96);
            this.textBox_RealTimeXY.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 18F);
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 24);
            this.label7.TabIndex = 15;
            this.label7.Text = "绘制坐标";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 18F);
            this.label9.Location = new System.Drawing.Point(3, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 24);
            this.label9.TabIndex = 18;
            this.label9.Text = "实时坐标";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView_Draw
            // 
            this.dataGridView_Draw.AllowUserToAddRows = false;
            this.dataGridView_Draw.AllowUserToDeleteRows = false;
            this.dataGridView_Draw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Draw.Location = new System.Drawing.Point(3, 32);
            this.dataGridView_Draw.Name = "dataGridView_Draw";
            this.dataGridView_Draw.ReadOnly = true;
            this.dataGridView_Draw.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView_Draw.RowTemplate.Height = 23;
            this.dataGridView_Draw.Size = new System.Drawing.Size(292, 29);
            this.dataGridView_Draw.TabIndex = 14;
            this.dataGridView_Draw.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridView_Draw_RowStateChanged);
            // 
            // dataGridView_DrawDelete
            // 
            this.dataGridView_DrawDelete.AllowUserToAddRows = false;
            this.dataGridView_DrawDelete.AllowUserToDeleteRows = false;
            this.dataGridView_DrawDelete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_DrawDelete.Location = new System.Drawing.Point(3, 91);
            this.dataGridView_DrawDelete.Name = "dataGridView_DrawDelete";
            this.dataGridView_DrawDelete.ReadOnly = true;
            this.dataGridView_DrawDelete.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView_DrawDelete.RowTemplate.Height = 23;
            this.dataGridView_DrawDelete.Size = new System.Drawing.Size(295, 31);
            this.dataGridView_DrawDelete.TabIndex = 15;
            this.dataGridView_DrawDelete.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridView_DrawDelete_RowStateChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 18F);
            this.label8.Location = new System.Drawing.Point(2, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 24);
            this.label8.TabIndex = 16;
            this.label8.Text = "删除的坐标";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_DrawRoute
            // 
            this.button_DrawRoute.BackColor = System.Drawing.Color.Transparent;
            this.button_DrawRoute.BackgroundImage = global::guandao.Properties.Resources.chart_line_128px_1186412_easyicon_net;
            this.button_DrawRoute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_DrawRoute.FlatAppearance.BorderSize = 0;
            this.button_DrawRoute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_DrawRoute.Location = new System.Drawing.Point(79, 77);
            this.button_DrawRoute.Name = "button_DrawRoute";
            this.button_DrawRoute.Size = new System.Drawing.Size(49, 38);
            this.button_DrawRoute.TabIndex = 14;
            this.button_DrawRoute.UseVisualStyleBackColor = false;
            this.button_DrawRoute.Click += new System.EventHandler(this.button_DrawRoute_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 20F);
            this.label10.Location = new System.Drawing.Point(11, 82);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 27);
            this.label10.TabIndex = 15;
            this.label10.Text = "绘制:";
            // 
            // button_add_point
            // 
            this.button_add_point.Location = new System.Drawing.Point(219, 3);
            this.button_add_point.Name = "button_add_point";
            this.button_add_point.Size = new System.Drawing.Size(67, 35);
            this.button_add_point.TabIndex = 13;
            this.button_add_point.Text = "添加坐标";
            this.button_add_point.UseVisualStyleBackColor = true;
            this.button_add_point.Click += new System.EventHandler(this.button_add_point_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 18F);
            this.label6.Location = new System.Drawing.Point(116, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "Y:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_Clear
            // 
            this.button_Clear.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button_Clear.Location = new System.Drawing.Point(0, 336);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(298, 43);
            this.button_Clear.TabIndex = 8;
            this.button_Clear.Text = "清除地图";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_loadmap
            // 
            this.button_loadmap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button_loadmap.Location = new System.Drawing.Point(0, 379);
            this.button_loadmap.Name = "button_loadmap";
            this.button_loadmap.Size = new System.Drawing.Size(298, 43);
            this.button_loadmap.TabIndex = 7;
            this.button_loadmap.Text = "加载路径";
            this.button_loadmap.UseVisualStyleBackColor = true;
            this.button_loadmap.Click += new System.EventHandler(this.button_loadmap_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 18F);
            this.label5.Location = new System.Drawing.Point(22, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "X:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_AddY
            // 
            this.textBox_AddY.Font = new System.Drawing.Font("宋体", 18F);
            this.textBox_AddY.Location = new System.Drawing.Point(156, 3);
            this.textBox_AddY.Name = "textBox_AddY";
            this.textBox_AddY.Size = new System.Drawing.Size(55, 35);
            this.textBox_AddY.TabIndex = 10;
            // 
            // button_saveRoute
            // 
            this.button_saveRoute.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button_saveRoute.Location = new System.Drawing.Point(0, 422);
            this.button_saveRoute.Name = "button_saveRoute";
            this.button_saveRoute.Size = new System.Drawing.Size(298, 43);
            this.button_saveRoute.TabIndex = 2;
            this.button_saveRoute.Text = "保存路径";
            this.button_saveRoute.UseVisualStyleBackColor = true;
            this.button_saveRoute.Click += new System.EventHandler(this.button_saveRoute_Click);
            // 
            // textBox_AddX
            // 
            this.textBox_AddX.Font = new System.Drawing.Font("宋体", 18F);
            this.textBox_AddX.Location = new System.Drawing.Point(59, 3);
            this.textBox_AddX.Name = "textBox_AddX";
            this.textBox_AddX.Size = new System.Drawing.Size(55, 35);
            this.textBox_AddX.TabIndex = 9;
            // 
            // label_True_XY
            // 
            this.label_True_XY.AutoSize = true;
            this.label_True_XY.Font = new System.Drawing.Font("宋体", 18F);
            this.label_True_XY.Location = new System.Drawing.Point(207, 12);
            this.label_True_XY.Name = "label_True_XY";
            this.label_True_XY.Size = new System.Drawing.Size(166, 24);
            this.label_True_XY.TabIndex = 5;
            this.label_True_XY.Text = "X:8888 Y:8888";
            this.label_True_XY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20F);
            this.label1.Location = new System.Drawing.Point(1208, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 20F);
            this.label2.Location = new System.Drawing.Point(1363, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.label5_LED);
            this.splitContainer3.Panel2.Controls.Add(this.button1_openclose);
            this.splitContainer3.Panel2.Controls.Add(this.label4);
            this.splitContainer3.Panel2.Controls.Add(this.label_True_XY);
            this.splitContainer3.Panel2.Controls.Add(this.label3);
            this.splitContainer3.Panel2.Controls.Add(this.comboBox2_BaudRate);
            this.splitContainer3.Panel2.Controls.Add(this.comboBox1_PortName);
            this.splitContainer3.Panel2.Controls.Add(this.label_Grid);
            this.splitContainer3.Panel2.Controls.Add(this.label_XY);
            this.splitContainer3.Panel2.Controls.Add(this.label2);
            this.splitContainer3.Panel2.Controls.Add(this.label1);
            this.splitContainer3.Size = new System.Drawing.Size(1471, 777);
            this.splitContainer3.SplitterDistance = 728;
            this.splitContainer3.TabIndex = 1;
            // 
            // label5_LED
            // 
            this.label5_LED.AutoSize = true;
            this.label5_LED.BackColor = System.Drawing.SystemColors.Control;
            this.label5_LED.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5_LED.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5_LED.Location = new System.Drawing.Point(728, 23);
            this.label5_LED.Name = "label5_LED";
            this.label5_LED.Size = new System.Drawing.Size(17, 12);
            this.label5_LED.TabIndex = 16;
            this.label5_LED.Text = "●";
            // 
            // button1_openclose
            // 
            this.button1_openclose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1_openclose.Location = new System.Drawing.Point(761, 18);
            this.button1_openclose.Name = "button1_openclose";
            this.button1_openclose.Size = new System.Drawing.Size(96, 23);
            this.button1_openclose.TabIndex = 11;
            this.button1_openclose.Text = "打开串口";
            this.button1_openclose.UseVisualStyleBackColor = true;
            this.button1_openclose.Click += new System.EventHandler(this.button1_openclose_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(415, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "串  口:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(575, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "波特率:";
            // 
            // comboBox2_BaudRate
            // 
            this.comboBox2_BaudRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox2_BaudRate.AutoCompleteCustomSource.AddRange(new string[] {
            "9600",
            "19200",
            "115200"});
            this.comboBox2_BaudRate.FormattingEnabled = true;
            this.comboBox2_BaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "115200",
            "256000",
            "921600"});
            this.comboBox2_BaudRate.Location = new System.Drawing.Point(627, 19);
            this.comboBox2_BaudRate.Name = "comboBox2_BaudRate";
            this.comboBox2_BaudRate.Size = new System.Drawing.Size(97, 20);
            this.comboBox2_BaudRate.TabIndex = 13;
            this.comboBox2_BaudRate.SelectionChangeCommitted += new System.EventHandler(this.comboBox2_BaudRate_SelectionChangeCommitted);
            // 
            // comboBox1_PortName
            // 
            this.comboBox1_PortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1_PortName.FormattingEnabled = true;
            this.comboBox1_PortName.Location = new System.Drawing.Point(467, 18);
            this.comboBox1_PortName.Name = "comboBox1_PortName";
            this.comboBox1_PortName.Size = new System.Drawing.Size(97, 20);
            this.comboBox1_PortName.TabIndex = 12;
            this.comboBox1_PortName.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_PortName_SelectionChangeCommitted);
            // 
            // label_Grid
            // 
            this.label_Grid.AutoSize = true;
            this.label_Grid.Font = new System.Drawing.Font("宋体", 12F);
            this.label_Grid.Location = new System.Drawing.Point(131, 18);
            this.label_Grid.Name = "label_Grid";
            this.label_Grid.Size = new System.Drawing.Size(56, 16);
            this.label_Grid.TabIndex = 4;
            this.label_Grid.Text = "Grid:5";
            // 
            // label_XY
            // 
            this.label_XY.AutoSize = true;
            this.label_XY.Font = new System.Drawing.Font("宋体", 12F);
            this.label_XY.Location = new System.Drawing.Point(13, 18);
            this.label_XY.Name = "label_XY";
            this.label_XY.Size = new System.Drawing.Size(112, 16);
            this.label_XY.TabIndex = 2;
            this.label_XY.Text = "X:8888 Y:8888";
            this.label_XY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM10";
            this.serialPort1.ReceivedBytesThreshold = 6;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(guandao.Form1);
            // 
            // form1BindingSource1
            // 
            this.form1BindingSource1.DataSource = typeof(guandao.Form1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 777);
            this.Controls.Add(this.splitContainer3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_location)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackGround)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Draw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_DrawDelete)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBoxBackGround;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_saveRoute;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label_Grid;
        private System.Windows.Forms.Label label_XY;
        private System.Windows.Forms.Label label_True_XY;
        private System.Windows.Forms.PictureBox pictureBox_location;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.PictureBox pictureBoxForce;
        private System.Windows.Forms.Label label5_LED;
        private System.Windows.Forms.Button button1_openclose;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2_BaudRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1_PortName;
        private System.Windows.Forms.Button button_loadmap;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.Button button_add_point;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_AddY;
        private System.Windows.Forms.TextBox textBox_AddX;
        private System.Windows.Forms.DataGridView dataGridView_Draw;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.BindingSource form1BindingSource1;
        private System.Windows.Forms.DataGridView dataGridView_DrawDelete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_RealTimeXY;
        private System.Windows.Forms.Button button_DrawRoute;
        private System.Windows.Forms.Label label10;
    }
}

