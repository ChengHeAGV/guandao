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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBoxBackGround = new System.Windows.Forms.PictureBox();
            this.pictureBoxForce = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackGround)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForce)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxForce);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxBackGround);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1163, 592);
            this.splitContainer1.SplitterDistance = 925;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureBoxBackGround
            // 
            this.pictureBoxBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxBackGround.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBackGround.Name = "pictureBoxBackGround";
            this.pictureBoxBackGround.Size = new System.Drawing.Size(925, 592);
            this.pictureBoxBackGround.TabIndex = 0;
            this.pictureBoxBackGround.TabStop = false;
            // 
            // pictureBoxForce
            // 
            this.pictureBoxForce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxForce.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxForce.Name = "pictureBoxForce";
            this.pictureBoxForce.Size = new System.Drawing.Size(925, 592);
            this.pictureBoxForce.TabIndex = 1;
            this.pictureBoxForce.TabStop = false;
            this.pictureBoxForce.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxForce_MouseClick);
            this.pictureBoxForce.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxForce_MouseMove);
            this.pictureBoxForce.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBoxForce_PreviewKeyDown);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("宋体", 25F);
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(234, 592);
            this.textBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 592);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackGround)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxForce)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBoxForce;
        private System.Windows.Forms.PictureBox pictureBoxBackGround;
        private System.Windows.Forms.TextBox textBox1;
    }
}

