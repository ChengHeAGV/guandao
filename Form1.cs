using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI.DataVisualization.Charting;
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
        /// <summary>
        /// 获得字符串中开始和结束字符串中间得值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="s">开始</param>
        /// <param name="e">结束</param>
        /// <returns></returns> 
        public static string GetValue(string str, string s, string e)
        {
            Regex rg = new Regex("(?<=(" + s + "))[.\\s\\S]*?(?=(" + e + "))", RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(str).Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //图层初始化
            Draw_Init();

            //注册鼠标滚轮事件
            this.pictureBox_location.MouseWheel += new MouseEventHandler(pictureBox_location_MouseWheel);
            dataGridView_Draw.DataSource = DrawPointList.ToArray();

            #region 串口初始化
            button1_openclose.Text = "打开串口";
            if (Ini.Read("串口配置", "BaudRate") != "null")
                comboBox2_BaudRate.SelectedIndex = int.Parse(Ini.Read("串口配置", "BaudRate"));
            else
                comboBox2_BaudRate.SelectedIndex = 2;

            string[] str;

            str = SerialPort.GetPortNames();
            if (str.Length != 0)
            {
                comboBox1_PortName.Items.AddRange(str);
                try
                {
                    if (Ini.Read("串口配置", "PortName") != "null")
                    {
                        serialPort1.PortName = Ini.Read("串口配置", "PortName");
                        comboBox1_PortName.SelectedItem = serialPort1.PortName;
                    }
                    else
                    {
                        serialPort1.PortName = comboBox1_PortName.SelectedItem.ToString();
                    }

                    serialPort1.BaudRate = int.Parse(comboBox2_BaudRate.SelectedItem.ToString());
                    serialPort1.Open();
                    serialPort1.DataReceived += serialPort1_DataReceived;
                    button1_openclose.Text = "关闭串口";
                    label5_LED.ForeColor = Color.Red;
                }
                catch (Exception)
                {
                    MessageBox.Show("打开串口失败,串口被占用或选择正确的串口!", "错误提示");
                }
            }
            #endregion
        }

        Size MapSize;

        //绘制的坐标集合
        List<Point> DrawPointList = new List<Point>();
        //撤销的坐标集合
        List<Point> DrawPointListDelete = new List<Point>();

        //实时反馈坐标集合
        List<Point3D> RealTimePointList = new List<Point3D>();

        //光标当前位置
        Point NowPoint;

        //绘图模式
        bool MapMode = false;

        //捕获值
        int Grid = 35;//单位，像素

        //图层初始化
        void Draw_Init()
        {


            //初始化坐标集合，添加原点坐标
            DrawPointList.Add(new Point(0, 0));

            label_Grid.Text = "Grid:" + Grid.ToString();

            // 背景图层绘制网格
            Draw_Grid();

            #region 表层设置透明
            pictureBoxForce.BackColor = Color.Transparent;
            pictureBoxForce.Parent = pictureBoxBackGround;

            pictureBox_location.BackColor = Color.Transparent;
            pictureBox_location.Parent = pictureBoxForce;
            #endregion
            //RealTimePointList.Add(new Point3D(10, 10, 145));

            //绘制定位圆圈
            Draw_location(true);

        }

        //绘制网格
        void Draw_Grid()
        {
            //获取绘图区域
            MapSize = pictureBoxBackGround.Size;

            #region 背景图层绘制网格
            Bitmap bmp = new Bitmap(MapSize.Width, MapSize.Height);
            if (Grid >= 10)
            {
                Graphics g = Graphics.FromImage(bmp);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //画笔为灰色
                Pen pen = new Pen(Color.LightGray);
                //画虚线
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                //在图片上画线
                //横线
                for (int i = 0; i < MapSize.Height / Grid + 1; i++)
                {
                    g.DrawLine(pen, 0, MapSize.Height - Grid * i, MapSize.Width, MapSize.Height - Grid * i);
                }
                //竖线
                for (int i = 0; i < MapSize.Width / Grid + 1; i++)
                {
                    g.DrawLine(pen, Grid * i, 0, Grid * i, MapSize.Height);
                }
                g.Dispose();
            }
            pictureBoxBackGround.Image = bmp;
            pictureBoxBackGround.BackColor = Color.Black;
            #endregion
        }

        //绘制实时位置
        void Draw_location(bool dis)
        {
            //当前位置指示器直径
            int Radius = 30;

            Bitmap bmp = new Bitmap(MapSize.Width, MapSize.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush bush = new SolidBrush(Color.Orange);//填充的颜色

            //画已经绘制的线段
            if (RealTimePointList.Count >= 2)
            {
                for (int i = 0; i < RealTimePointList.Count - 1; i++)
                {
                    //画已经绘制的线段
                    g.DrawLine(new Pen(Color.White, 3), RealTimePointList[i].X * Grid, MapSize.Height - RealTimePointList[i].Y * Grid, RealTimePointList[i + 1].X * Grid, MapSize.Height - RealTimePointList[i + 1].Y * Grid);
                }
            }

            //绘制当前位置指示器
            if (dis && RealTimePointList.Count > 0)
            {
                //画三角形
                float d = 70;
                float angle = RealTimePointList[RealTimePointList.Count - 1].Z;
                Point center = new Point();
                Point[] point = new Point[3];
                center.X = (int)(RealTimePointList[RealTimePointList.Count - 1].X) * Grid;
                center.Y = (int)(MapSize.Height - (RealTimePointList[RealTimePointList.Count - 1].Y) * Grid);

                point[0].X = (int)(center.X + (float)(Math.Cos(Math.PI * (angle / 180.0)) * d));
                point[0].Y = (int)(center.Y - (float)(Math.Sin(Math.PI * (angle / 180.0)) * d));



                double Q1 = angle;
                double Q2 = Math.Acos(Radius / d) * 180 / Math.PI;
                double Q3 = Q1 + Q2;

                point[1].X = (int)(center.X + Radius * Math.Cos(Math.PI * (Q3 / 180.0)));
                point[1].Y = (int)(center.Y - Radius * Math.Sin(Math.PI * (Q3 / 180.0)));

                double q1 = angle;
                double q2 = Math.Acos(Radius / d) * 180 / Math.PI;
                double q3 = 360 - (q2 - q1);
                point[2].X = (int)(center.X + Radius * Math.Cos(Math.PI * (q3 / 180.0)));
                point[2].Y = (int)(center.Y - Radius * Math.Sin(Math.PI * (q3 / 180.0)));

                g.FillPolygon(bush, point);

                //画角度线

                g.DrawLine(new Pen(Color.Red, 3), center.X, center.Y, point[0].X, point[0].Y);

                //画圆
                g.FillEllipse(bush, center.X - Radius, center.Y - Radius, Radius * 2, Radius * 2);
            }

            g.Dispose();
            pictureBox_location.Image = bmp;
        }

        //改变Grid
        bool mouseWheel = false;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.Z)
            {
                //恢复撤销
                if (DrawPointListDelete.Count > 0)
                {
                    //将准备恢复的坐标赋给坐标集合
                    DrawPointList.Add(DrawPointListDelete[DrawPointListDelete.Count - 1]);
                    //删除最后一个坐标
                    DrawPointListDelete.RemoveAt(DrawPointListDelete.Count - 1);
                    //更新DataGridView显示
                    dataGridView_Draw.DataSource = DrawPointList.ToArray();
                    dataGridView_DrawDelete.DataSource = DrawPointListDelete.ToArray();
                    dataGridView_Draw.FirstDisplayedScrollingRowIndex = dataGridView_Draw.RowCount - 1;
                    dataGridView_DrawDelete.FirstDisplayedScrollingRowIndex = dataGridView_DrawDelete.RowCount - 1;

                    //重绘界面
                    ReloadUI();

                    label1.Text = DrawPointList.Count.ToString();
                    label2.Text = DrawPointListDelete.ToString();
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
                if (DrawPointList.Count > 1)
                {
                    //将准备删除的坐标赋给撤销集合
                    DrawPointListDelete.Add(DrawPointList[DrawPointList.Count - 1]);
                    //删除最后一个坐标
                    DrawPointList.RemoveAt(DrawPointList.Count - 1);
                    //更新DataGridView显示
                    dataGridView_Draw.DataSource = DrawPointList.ToArray();
                    dataGridView_DrawDelete.DataSource = DrawPointListDelete.ToArray();
                    dataGridView_Draw.FirstDisplayedScrollingRowIndex = dataGridView_Draw.RowCount - 1;
                    dataGridView_DrawDelete.FirstDisplayedScrollingRowIndex = dataGridView_DrawDelete.RowCount - 1;

                    //重绘界面
                    ReloadUI();
                    label1.Text = DrawPointList.Count.ToString();
                    label2.Text = DrawPointListDelete.ToString();
                }
            }
        }

        //重新加载界面
        void ReloadUI()
        {
            int x = NowPoint.X * Grid;
            int y = MapSize.Height - NowPoint.Y * Grid;

            if (MapMode)
            {
                Bitmap bmp = new Bitmap(MapSize.Width, MapSize.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Brush bush = new SolidBrush(Color.Black);//填充的颜色

                //画定位十字线
                int snap = 50;
                g.DrawLine(new Pen(Color.DarkCyan, 1), x, y - snap, x, y + snap);//竖线
                g.DrawLine(new Pen(Color.DarkCyan, 1), x - snap, y, x + snap, y);//横线

                //画定位小交叉线
                g.DrawLine(new Pen(Color.LightGray, 1), x - snap / 3, y - snap / 3, x + snap / 3, y + snap / 3);
                g.DrawLine(new Pen(Color.LightGray, 1), x + snap / 3, y - snap / 3, x - snap / 3, y + snap / 3);

                //重绘已绘制的线段,并在拐点处画小叉号
                if (DrawPointList.Count >= 2)
                {
                    for (int i = 0; i < DrawPointList.Count - 1; i++)
                    {
                        //画已经绘制的线段
                        g.DrawLine(new Pen(Color.White, 2), DrawPointList[i].X * Grid, MapSize.Height - DrawPointList[i].Y * Grid, DrawPointList[i + 1].X * Grid, MapSize.Height - DrawPointList[i + 1].Y * Grid);
                        //画小叉号
                        g.DrawLine(new Pen(Color.Gray, 1), DrawPointList[i].X * Grid - snap / 4, MapSize.Height - DrawPointList[i].Y * Grid - snap / 4, DrawPointList[i].X * Grid + snap / 4, MapSize.Height - DrawPointList[i].Y * Grid + snap / 4);
                        g.DrawLine(new Pen(Color.Gray, 1), DrawPointList[i].X * Grid + snap / 4, MapSize.Height - DrawPointList[i].Y * Grid - snap / 4, DrawPointList[i].X * Grid - snap / 4, MapSize.Height - DrawPointList[i].Y * Grid + snap / 4);
                    }
                }

                //画正在绘制的线段
                if (DrawPointList.Count >= 1)
                {
                    g.DrawLine(new Pen(Color.White, 2), DrawPointList[DrawPointList.Count - 1].X * Grid, MapSize.Height - DrawPointList[DrawPointList.Count - 1].Y * Grid, x, y);
                }

                g.Dispose();
                pictureBoxForce.Image = bmp;
            }
            else
            {
                Bitmap bmp = new Bitmap(MapSize.Width, MapSize.Height);
                Graphics g = Graphics.FromImage(bmp);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Brush bush = new SolidBrush(Color.Red);//填充的颜色

                int r = 6;
                //画已经绘制的线段
                if (DrawPointList.Count >= 2)
                {
                    for (int i = 0; i < DrawPointList.Count - 1; i++)
                    {

                        if (mouseWheel)
                        {
                            //画已经绘制的线段
                            g.DrawLine(new Pen(Color.Blue, 3), DrawPointList[i].X * Grid, MapSize.Height - DrawPointList[i].Y * Grid, DrawPointList[i + 1].X * Grid, MapSize.Height - DrawPointList[i + 1].Y * Grid);
                            //在拐点处画圆
                            g.FillEllipse(bush, (DrawPointList[i].X * Grid) - r, MapSize.Height - (DrawPointList[i].Y * Grid) - r, 2 * r, 2 * r);
                        }
                        else
                        {
                            //画已经绘制的线段
                            g.DrawLine(new Pen(Color.Blue, 3), DrawPointList[i].X * Grid, MapSize.Height - DrawPointList[i].Y * Grid, DrawPointList[i + 1].X * Grid, MapSize.Height - DrawPointList[i + 1].Y * Grid);
                            //在拐点处画圆
                            g.FillEllipse(bush, DrawPointList[i].X * Grid - r, MapSize.Height - DrawPointList[i].Y * Grid - r, 2 * r, 2 * r);
                        }
                    }
                }
                if (DrawPointList.Count != 0)
                {
                    //在拐点处画圆
                    int i = DrawPointList.Count - 1;
                    if (mouseWheel)
                        g.FillEllipse(bush, (DrawPointList[i].X * Grid) - r, (MapSize.Height - DrawPointList[i].Y * Grid - r), 2 * r, 2 * r);
                    else
                        g.FillEllipse(bush, DrawPointList[i].X * Grid - r, MapSize.Height - DrawPointList[i].Y * Grid - r, 2 * r, 2 * r);

                }
                mouseWheel = false;
                g.Dispose();
                pictureBoxForce.Image = bmp;
            }
        }

        private void button_saveRoute_Click(object sender, EventArgs e)
        {
            if (DrawPointList.Count > 1)
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
                    for (int i = 0; i < 101; i++)
                    {
                        writer.WriteStartElement("参数" + i.ToString());
                        writer.WriteElementString("参数号", i.ToString());
                        writer.WriteElementString("参数名", "");
                        if (i == 99)//路径数
                        {
                            writer.WriteElementString("当前值", "1");
                            writer.WriteElementString("设定值", "1");
                        }
                        else if (i == 96)//站点包含信息数
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
                        for (int j = 0; j < DrawPointList.Count; j++)
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
                            writer.WriteElementString("当前值", DrawPointList[j].X.ToString());
                            writer.WriteElementString("设定值", DrawPointList[j].X.ToString());
                            writer.WriteElementString("描述", "X坐标，单位CM");
                            writer.WriteEndElement();

                            writer.WriteStartElement("参数2");
                            writer.WriteElementString("参数号", "3");
                            writer.WriteElementString("参数名", "Y");
                            writer.WriteElementString("当前值", DrawPointList[j].Y.ToString());
                            writer.WriteElementString("设定值", DrawPointList[j].Y.ToString());
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
                            if (j == (DrawPointList.Count - 1))
                            {
                                writer.WriteElementString("当前值", "1");
                                writer.WriteElementString("设定值", "1");
                            }
                            else
                            {
                                writer.WriteElementString("当前值", "0");
                                writer.WriteElementString("设定值", "0");
                            }

                            writer.WriteElementString("描述", "0:保持 1:停止 2:前进");
                            writer.WriteEndElement();

                            for (int k = 0; k < 5; k++)
                            {
                                writer.WriteStartElement("参数" + (k + 7).ToString());
                                writer.WriteElementString("参数号", (k + 8).ToString());
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

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            Byte[] InputBuf = new Byte[128];
            try
            {
                Thread.Sleep(20);
                serialPort1.Read(InputBuf, 0, serialPort1.BytesToRead);                                //读取缓冲区的数据直到“}”即0x7D为结束符  
                string str = System.Text.Encoding.Default.GetString(InputBuf);

                str = GetValue(str, "T", "W");

                if (str.Length > 0)
                {
                    str += "W";//方便获取
                    string x1 = GetValue(str, "x", "y");
                    string y1 = GetValue(str, "y", "j");
                    string j1 = GetValue(str, "j", "W");

                    if (x1.Length > 0 && y1.Length > 0 && j1.Length > 0)
                    {
                        //获取有效坐标
                        int x = int.Parse(x1);
                        int y = int.Parse(y1);
                        int j = int.Parse(j1);

                        //设置坐标
                        if (x < 0)
                        {
                            x = 0;
                        }
                        if (y < 0)
                        {
                            y = 0;
                        }

                        if (j < 0)
                        {
                            j = 0;
                        }
                        this.Invoke(new MethodInvoker(delegate
                        {
                            //更新实时坐标
                            RealTimePointList.Add(new Point3D(x, y, j));
                            dataGridView_RealTimeXY.DataSource = RealTimePointList.ToArray();
                            dataGridView_RealTimeXY.FirstDisplayedScrollingRowIndex = dataGridView_RealTimeXY.RowCount - 1;

                            //绘制定位圆圈
                            Draw_location(true);
                        }));
                    }
                }
            }
            catch (TimeoutException ex)         //超时处理  
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox_location_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;



            #region 计算捕获坐标
            //计算最近坐标点
            int xval = x % Grid;
            int yval = y % Grid;
            if (xval > (Grid / 2))
                x += Grid - xval;
            else
                x -= xval;
            if (yval > (Grid / 2))
                y += Grid - yval;
            else
                y -= yval;
            #endregion

            y = MapSize.Height / Grid - y / Grid;
            NowPoint.X = x / Grid;
            NowPoint.Y = y;

            //显示坐标到界面
            label_XY.Text = "X:" + x.ToString() + " Y:" + y.ToString();
            label_True_XY.Text = "X:" + (NowPoint.X).ToString() + "CM Y:" + (NowPoint.Y).ToString() + "CM";
            //重新加载界面
            ReloadUI();

            //绘制定位圆圈
            //Draw_location((NowPoint.X) * Grid, size.Height - (NowPoint.Y) * Grid, true);
        }

        public void pictureBox_location_MouseWheel(object sender, MouseEventArgs e)
        {

            int x = e.X;
            int y = e.Y;

            mouseWheel = true;


            if (e.Delta > 0)
            {
                if (Grid > 5)
                {
                    Grid -= 5;
                }
                else if (Grid > 1)
                {
                    Grid -= 1;
                }
            }
            else
            {
                if (Grid < 400)
                {
                    if (Grid < 5)
                    {
                        Grid += 1;
                    }
                    else
                        Grid += 5;
                }
            }
            label_Grid.Text = "Grid:" + Grid.ToString();

            //更新原点坐标
            //point[0] = new Point(0, size.Height / Grid);

            //重绘网格
            Draw_Grid();
            //重绘线段
            ReloadUI();

            //绘制实际路线
            Draw_location(true);
        }

        private void pictureBox_location_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            MapMode = true;

            #region 计算捕获坐标
            //计算最近坐标点
            int xval = x % Grid;
            int yval = y % Grid;
            if (xval > (Grid / 2))
                x += Grid - xval;
            else
                x -= xval;
            if (yval > (Grid / 2))
                y += Grid - yval;
            else
                y -= yval;
            #endregion
            y = MapSize.Height / Grid - y / Grid;
            NowPoint.X = x / Grid;
            NowPoint.Y = y;

            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                //单击了鼠标左键,添加坐标到坐标集合
                DrawPointList.Add(NowPoint);
                dataGridView_Draw.DataSource = DrawPointList.ToArray();
                dataGridView_Draw.FirstDisplayedScrollingRowIndex = dataGridView_Draw.RowCount - 1;
            }
            else if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                //单击了鼠标右键，退出绘图模式
                MapMode = false;
                ReloadUI();
            }

            label1.Text = DrawPointList.Count.ToString();
            label2.Text = DrawPointListDelete.Count.ToString();
        }

        private void pictureBox_location_MouseEnter(object sender, EventArgs e)
        {
            //控件获取焦点后滚轮事件才有效
            this.pictureBox_location.Focus();
        }

        private void button1_openclose_Click(object sender, EventArgs e)
        {
            try
            {
                if (button1_openclose.Text == "打开串口")
                {
                    serialPort1.PortName = comboBox1_PortName.SelectedItem.ToString();
                    serialPort1.BaudRate = int.Parse(comboBox2_BaudRate.SelectedItem.ToString());
                    serialPort1.Open();
                    serialPort1.DataReceived += serialPort1_DataReceived;
                    button1_openclose.Text = "关闭串口";
                    label5_LED.ForeColor = Color.Red;
                }
                else
                {
                    serialPort1.Close();
                    button1_openclose.Text = "打开串口";
                    label5_LED.ForeColor = Color.Black;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("打开串口失败,串口被占用或选择正确的串口!", "错误提示");
            }
        }

        private void comboBox2_BaudRate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            serialPort1.BaudRate = int.Parse(comboBox2_BaudRate.SelectedItem.ToString());
            //存储PortName
            Ini.Write("串口配置", "BaudRate", comboBox2_BaudRate.SelectedIndex.ToString());
        }

        private void comboBox1_PortName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                try
                {
                    serialPort1.PortName = comboBox1_PortName.SelectedItem.ToString();
                    serialPort1.Open();

                    //存储PortName
                    Ini.Write("串口配置", "PortName", serialPort1.PortName);
                }
                catch (Exception)
                {
                    serialPort1.Close();
                    button1_openclose.Text = "打开串口";
                    label5_LED.ForeColor = Color.Black;
                    MessageBox.Show("打开串口失败,串口被占用或选择正确的串口!", "错误提示");
                }
            }
        }

        private void button_loadmap_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "文本文件|*.xml";
            if (file.ShowDialog() == DialogResult.OK)
            {
                ////清空子节点
                //node_parment.Nodes.Clear();
                //node_lujingparment.Nodes.Clear();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(file.FileName);

                XmlNode xn = xmlDoc.SelectSingleNode("Flash参数");

                XmlNodeList xnn = xn.ChildNodes;

                int j = 0;
                foreach (XmlNode xnk in xnn)
                {

                    if (xnk.Name == "路径参数")
                    {
                        j = 0; DrawPointList.Clear();
                        XmlElement xe = (XmlElement)xnk;
                        XmlNodeList xnm = xe.ChildNodes;
                        foreach (XmlNode xnkk in xnm)
                        {
                            XmlElement xee = (XmlElement)xnkk;
                            XmlNodeList xng = xee.ChildNodes;
                            foreach (XmlNode xnkkk in xng)
                            {
                                DrawPointList.Add(new Point(int.Parse(xnkkk.ChildNodes[1].ChildNodes.Item(3).InnerText.ToString()), int.Parse(xnkkk.ChildNodes[2].ChildNodes.Item(3).InnerText.ToString())));
                            }
                            j++;
                        }
                    }
                }
                dataGridView_Draw.DataSource = DrawPointList.ToArray();
            }
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {

            DrawPointList.Clear();
            dataGridView_Draw.DataSource = DrawPointList.ToArray();

            //重新加载界面
            ReloadUI();

            //绘制实际路线
            Draw_location(false);
        }

        //手动添加坐标
        private void button_add_point_Click(object sender, EventArgs e)
        {
            try
            {
                //添加点
                int x = int.Parse(textBox_AddX.Text.Trim());
                int y = int.Parse(textBox_AddY.Text.Trim());

                DrawPointList.Add(new Point(x, y));
                dataGridView_Draw.DataSource = DrawPointList.ToArray();

                //更新界面
                ReloadUI();
            }
            catch
            {
            }

        }
    }
}