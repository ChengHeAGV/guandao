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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label_True_XY = new System.Windows.Forms.Label();
            this.button_saveRoute = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label_Grid = new System.Windows.Forms.Label();
            this.label_XY = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxForce = new System.Windows.Forms.PictureBox();
            this.pictureBoxBackGround = new System.Windows.Forms.PictureBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.textBox_port = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackGround)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
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
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBox_port);
            this.splitContainer2.Panel2.Controls.Add(this.label_True_XY);
            this.splitContainer2.Panel2.Controls.Add(this.button_saveRoute);
            this.splitContainer2.Size = new System.Drawing.Size(298, 728);
            this.splitContainer2.SplitterDistance = 363;
            this.splitContainer2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("宋体", 25F);
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(298, 363);
            this.textBox1.TabIndex = 1;
            this.textBox1.WordWrap = false;
            // 
            // label_True_XY
            // 
            this.label_True_XY.AutoSize = true;
            this.label_True_XY.Font = new System.Drawing.Font("宋体", 18F);
            this.label_True_XY.Location = new System.Drawing.Point(19, 17);
            this.label_True_XY.Name = "label_True_XY";
            this.label_True_XY.Size = new System.Drawing.Size(166, 24);
            this.label_True_XY.TabIndex = 5;
            this.label_True_XY.Text = "X:8888 Y:8888";
            this.label_True_XY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_saveRoute
            // 
            this.button_saveRoute.Location = new System.Drawing.Point(23, 65);
            this.button_saveRoute.Name = "button_saveRoute";
            this.button_saveRoute.Size = new System.Drawing.Size(107, 62);
            this.button_saveRoute.TabIndex = 2;
            this.button_saveRoute.Text = "保存路径";
            this.button_saveRoute.UseVisualStyleBackColor = true;
            this.button_saveRoute.Click += new System.EventHandler(this.button_saveRoute_Click);
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
            this.splitContainer3.Panel2.Controls.Add(this.label_Grid);
            this.splitContainer3.Panel2.Controls.Add(this.label_XY);
            this.splitContainer3.Panel2.Controls.Add(this.label2);
            this.splitContainer3.Panel2.Controls.Add(this.label1);
            this.splitContainer3.Size = new System.Drawing.Size(1471, 777);
            this.splitContainer3.SplitterDistance = 728;
            this.splitContainer3.TabIndex = 1;
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
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::guandao.Properties.Resources.demo1;
            this.pictureBox1.Location = new System.Drawing.Point(543, 283);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBoxForce
            // 
            this.pictureBoxForce.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxForce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxForce.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxForce.Name = "pictureBoxForce";
            this.pictureBoxForce.Size = new System.Drawing.Size(1169, 728);
            this.pictureBoxForce.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxForce.TabIndex = 1;
            this.pictureBoxForce.TabStop = false;
            this.pictureBoxForce.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxForce_MouseClick);
            this.pictureBoxForce.MouseEnter += new System.EventHandler(this.pictureBoxForce_MouseEnter);
            this.pictureBoxForce.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxForce_MouseMove);
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
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.PortName = "COM4";
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // textBox_port
            // 
            this.textBox_port.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox_port.Font = new System.Drawing.Font("宋体", 25F);
            this.textBox_port.Location = new System.Drawing.Point(0, 175);
            this.textBox_port.Multiline = true;
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_port.Size = new System.Drawing.Size(298, 186);
            this.textBox_port.TabIndex = 6;
            this.textBox_port.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 777);
            this.Controls.Add(this.splitContainer3);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackGround)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBoxForce;
        private System.Windows.Forms.PictureBox pictureBoxBackGround;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_saveRoute;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label_Grid;
        private System.Windows.Forms.Label label_XY;
        private System.Windows.Forms.Label label_True_XY;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox textBox_port;
    }
}

