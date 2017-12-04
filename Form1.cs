using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace guandao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //图层初始化
            Draw_Init();
        }

        Size size;
        //已绘制点坐标集合
        Point[] point;//原始坐标
        Point[] NewPoint;//新坐标
        //点坐标个数
        int length = 1;
        //光标当前位置
        Point NowPoint;

        //已经撤销的步数
        int CancelStep = 0;

        //绘图模式
        bool MapMode = false;


        //图层初始化
        void Draw_Init()
        {
            size = pictureBoxForce.Size;
            point = new Point[1000];
            NewPoint = new Point[1000];
            point[0] = new Point(0, size.Height);

            #region 背景图层绘制网格
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //画笔为灰色
            Pen pen = new Pen(Color.LightGray);
            //画虚线
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;

            //在图片上画线
            int num = 30;//横线
            int snap_x = size.Width / num;
            int snap_y = size.Height / num;

            for (int i = 0; i < num + 1; i++)
            {
                g.DrawLine(pen, 0, snap_y * i, size.Width, snap_y * i);//坐标
            }
            num = 50;//竖线
            snap_x = size.Width / num;
            snap_y = size.Height / num;
            for (int i = 0; i < num + 1; i++)
            {
                g.DrawLine(pen, snap_x * i, 0, snap_x * i, size.Height);//坐标
            }
            g.Dispose();
            pictureBoxBackGround.Image = bmp;
            pictureBoxBackGround.BackColor = Color.White;
            #endregion

            #region 表层设置透明
            pictureBoxForce.BackColor = Color.Transparent;
            pictureBoxForce.Parent = pictureBoxBackGround;
            #endregion
        }

        private void pictureBoxForce_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            #region 计算自动归位坐标
            //计算最近坐标点
            int snapx = size.Width / 50;
            int snapy = size.Height / 30;

            int xval = x % snapx;
            int yval = y % snapy;
            if (xval > (snapx / 2))
                x += snapx - xval;
            else
                x -= xval;
            if (yval > (snapy / 2))
                y += snapy - yval;
            else
                y -= yval;
            #endregion

            NowPoint.X = x;
            NowPoint.Y = y;

            //重新加载界面
            ReloadUI();
        }

        private void pictureBoxForce_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            MapMode = true;

            #region 计算自动归位坐标
            //计算最近坐标点
            int snapx = size.Width / 50;
            int snapy = size.Height / 30;

            int xval = x % snapx;
            int yval = y % snapy;
            if (xval > (snapx / 2))
                x += snapx - xval;
            else
                x -= xval;
            if (yval > (snapy / 2))
                y += snapy - yval;
            else
                y -= yval;
            #endregion

            NowPoint.X = x;
            NowPoint.Y = y;

            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                //单击了鼠标左键,添加点
                point[length++] = new Point(x, y);
                NewPoint[length - 1].X = point[length - 1].X / snapx;
                NewPoint[length - 1].Y = 30 + 1 - point[length - 1].Y / snapy;

                textBox1.Text = string.Empty;
                textBox1.AppendText("point.Length:" + length.ToString() + "\r\n");
                for (int i = 0; i < length; i++)
                {
                    textBox1.AppendText(i.ToString() + " x:" + NewPoint[i].X.ToString() + ",y:" + (NewPoint[i].Y).ToString() + "\r\n");
                }
            }
            else if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                //单击了鼠标右键，退出绘图模式
                MapMode = false;
                ReloadUI();
            }

            label1.Text = length.ToString();
            label2.Text = CancelStep.ToString();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.Z)
            {
                //恢复撤销
                if (CancelStep > 1)
                {
                    CancelStep--;
                    length++;
                    ReloadUI();
                    label1.Text = length.ToString();
                    label2.Text = CancelStep.ToString();
                }
            }
            else
                if (e.KeyCode == Keys.Escape)
            {
                //退出绘图模式
                MapMode = false;
                ReloadUI();
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                //撤销
                if (length > 1)
                {
                    length--;
                    CancelStep++;
                    //重新加载界面
                    ReloadUI();
                    label1.Text = length.ToString();
                    label2.Text = CancelStep.ToString();
                }
            }
        }

        //重新加载界面
        void ReloadUI()
        {
            int x = NowPoint.X;
            int y = NowPoint.Y;
            if (MapMode)
            {
                Bitmap bmp = new Bitmap(size.Width, size.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Brush bush = new SolidBrush(Color.Black);//填充的颜色

                //画定位十字线
                int snap = size.Width / 30;
                g.DrawLine(new Pen(Color.DarkCyan, 1), x, y - snap, x, y + snap);//竖线
                g.DrawLine(new Pen(Color.DarkCyan, 1), x - snap, y, x + snap, y);//横线

                //画定位小交叉线
                g.DrawLine(new Pen(Color.LightGray, 1), x - snap / 3, y - snap / 3, x + snap / 3, y + snap / 3);
                g.DrawLine(new Pen(Color.LightGray, 1), x + snap / 3, y - snap / 3, x - snap / 3, y + snap / 3);

                //画已经绘制的线段,并在拐点处画小叉号
                if (length >= 2)
                {
                    for (int i = 0; i < length - 1; i++)
                    {
                        //画已经绘制的线段
                        g.DrawLine(new Pen(Color.Blue, 3), point[i], point[i + 1]);
                        //画小叉号
                        g.DrawLine(new Pen(Color.Gray, 1), point[i].X - snap / 4, point[i].Y - snap / 4, point[i].X + snap / 4, point[i].Y + snap / 4);
                        g.DrawLine(new Pen(Color.Gray, 1), point[i].X + snap / 4, point[i].Y - snap / 4, point[i].X - snap / 4, point[i].Y + snap / 4);
                    }
                }

                //画正在绘制的线段
                if (length >= 1)
                {
                    g.DrawLine(new Pen(Color.Blue, 3), point[length - 1], new Point(x, y));
                }

                g.Dispose();
                pictureBoxForce.Image = bmp;
            }
            else
            {
                Bitmap bmp = new Bitmap(size.Width, size.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Brush bush = new SolidBrush(Color.Black);//填充的颜色


                //画已经绘制的线段
                if (length >= 2)
                {
                    for (int i = 0; i < length - 1; i++)
                    {
                        //画已经绘制的线段
                        g.DrawLine(new Pen(Color.Blue, 3), point[i], point[i + 1]);

                        //在拐点处画圆
                        int r = 8;
                        g.FillEllipse(bush, point[i].X - r, point[i].Y - r, 2 * r, 2 * r);
                    }
                }
                if (length != 0)
                {
                    //在拐点处画圆
                    int r = 8;
                    int i = length - 1;
                    g.FillEllipse(bush, point[i].X - r, point[i].Y - r, 2 * r, 2 * r);
                }

                g.Dispose();
                pictureBoxForce.Image = bmp;
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void button_saveRoute_Click(object sender, EventArgs e)
        {
            if (length > 1)
            {
                SaveFileDialog file1 = new SaveFileDialog();
                file1.Filter = "文本文件|*.xml";
                if (file1.ShowDialog() == DialogResult.OK)
                {
                    XmlTextWriter writer = new XmlTextWriter(file1.FileName, null);
                    //使用自动缩进便于阅读
                    writer.Formatting = Formatting.Indented;

                    //写入根元素
                    writer.WriteStartElement("Flash参数");

                    writer.WriteStartElement("系统参数");
                    //加入子元素
                    for (int i = 0; i < 100; i++)
                    {
                        writer.WriteStartElement("参数" + i.ToString());
                        writer.WriteElementString("参数号", i.ToString());
                        writer.WriteElementString("参数名", "");
                        if (i == 99)//路径数
                        {
                            writer.WriteElementString("当前值", "1");
                            writer.WriteElementString("设定值", "1");
                        }
                        else if(i == 96)//站点包含信息数
                        {
                            writer.WriteElementString("当前值", "10");
                            writer.WriteElementString("设定值", "10");
                        }
                        else
                        {
                            writer.WriteElementString("当前值", "0");
                            writer.WriteElementString("设定值", "0");
                        }

                        writer.WriteElementString("描述", "");
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();


                    writer.WriteStartElement("路径参数");
                    //加入子元素
                    for (int i = 0; i < 1; i++)
                    {
                        writer.WriteStartElement("路径" + (i + 1).ToString());
                        for (int j = 0; j < length; j++)
                        {
                            writer.WriteStartElement("站点" + (j + 1).ToString());
                            //站点信息  

                            writer.WriteStartElement("参数0");
                            writer.WriteElementString("参数号", "1");
                            writer.WriteElementString("参数名", " ");
                            writer.WriteElementString("当前值", "0");
                            writer.WriteElementString("设定值", "0");
                            writer.WriteElementString("描述", "X坐标，单位CM");
                            writer.WriteEndElement();

                            writer.WriteStartElement("参数1");
                            writer.WriteElementString("参数号", "2");
                            writer.WriteElementString("参数名", "X");
                            writer.WriteElementString("当前值", NewPoint[j].X.ToString());
                            writer.WriteElementString("设定值", NewPoint[j].X.ToString());
                            writer.WriteElementString("描述", "X坐标，单位CM");
                            writer.WriteEndElement();

                            writer.WriteStartElement("参数2");
                            writer.WriteElementString("参数号", "3");
                            writer.WriteElementString("参数名", "Y");
                            writer.WriteElementString("当前值", NewPoint[j].Y.ToString());
                            writer.WriteElementString("设定值", NewPoint[j].Y.ToString());
                            writer.WriteElementString("描述", "Y坐标，单位CM");
                            writer.WriteEndElement();


                            for (int k = 0; k < 2; k++)
                            {
                                writer.WriteStartElement("参数" + (k + 3).ToString());
                                writer.WriteElementString("参数号", (k + 4).ToString());
                                writer.WriteElementString("参数名", "未定义");
                                writer.WriteElementString("当前值", "0");
                                writer.WriteElementString("设定值", "0");
                                writer.WriteElementString("描述", "");
                                writer.WriteEndElement();
                            }

                            writer.WriteStartElement("参数5");
                            writer.WriteElementString("参数号", "6");
                            writer.WriteElementString("参数名", "动作");
                            writer.WriteElementString("当前值", "0");
                            writer.WriteElementString("设定值", "0");
                            writer.WriteElementString("描述", "0:保持 1:停止 2:前进");
                            writer.WriteEndElement();

                            for (int k = 0; k < 4; k++)
                            {
                                writer.WriteStartElement("参数" + (k + 6).ToString());
                                writer.WriteElementString("参数号", (k + 7).ToString());
                                writer.WriteElementString("参数名", "未定义");
                                writer.WriteElementString("当前值", "0");
                                writer.WriteElementString("设定值", "0");
                                writer.WriteElementString("描述", "");
                                writer.WriteEndElement();
                            }

                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    //关闭根元素，并书写结束标签
                    writer.WriteEndElement();
                    //将XML写入文件并且关闭XmlTextWriter
                    writer.Close();
                }
            }
            else
            {
                MessageBox.Show("没有路径可保存到PC！", "提示");
            }
        }
    }
}
