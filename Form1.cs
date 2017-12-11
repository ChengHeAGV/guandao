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
        //实时位置线程
        Thread ThreadRealTime;
        //网格绘制线程
        Thread ThreadDrawGrid;
        //画图绘制线程
        Thread ThreadDrawMap;

        private class UpdateDraws
        {
            //更新已经画好的地图
            public static bool UpdateDrawedMap = false;
            //更新正在绘制的地图
            public static bool UpdateDrawingMap = false;
            //更新网格
            public static bool UpdateGrid = false;
            //更新实时位置地图
            public static bool UpdateRealTimeMap = false;
        }

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
            //string df = "287j89WTx81y2881y282j88WTx83y234j88WTx83y235j88WTx83y224j88W";
            //df = GetValue(df,"T","W");
            //图层初始化
            Draw_Init();

            //注册鼠标滚轮事件
            this.pictureBox_location.MouseWheel += new MouseEventHandler(pictureBox_location_MouseWheel);

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

            //实时绘图线程
            ThreadRealTime = new Thread(Thread_DrawRealTimePoint);
            ThreadRealTime.Start();

            //网格绘制线程
            ThreadDrawGrid = new Thread(Thread_DrawGrid);
            ThreadDrawGrid.Start();

            //画图绘制线程
            ThreadDrawMap = new Thread(Thread_DrawMap);
            ThreadDrawMap.Start();
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

        //中心点坐标,以最小物理单位CM计数
        private class PointCenter
        {
            /// <summary>
            /// 初始化的中心坐标
            /// </summary>
            public static Point Init;
            /// <summary>
            /// 当前中心坐标
            /// </summary>
            public static Point Now;
            /// <summary>
            /// 移动前的中心坐标
            /// </summary>
            public static Point MoveAgo;
        }

        //

        //绘图模式
        bool MapMode = false;

        //捕获值
        int Grid = 25;//单位，像素

        //空格键按下
        bool KeySpace = false;
        // bool 

        //图层初始化
        void Draw_Init()
        {
            //获取绘图区域Size
            MapSize = pictureBoxBackGround.Size;

            //初始化中心点坐标,地图中心
            PointCenter.Init = new Point(2, 2);
            PointCenter.Now = PointCenter.Init;
            PointCenter.MoveAgo = PointCenter.Init;

            //初始化坐标集合，添加原点坐标
            DrawPointList.Add(new Point(0, 0));

            label_Grid.Text = "Grid:" + Grid.ToString();

            pictureBoxBackGround.BackColor = Color.Black;

            // 更新网格
            UpdateDraws.UpdateGrid = true;

            #region 表层设置透明
            pictureBoxForce.BackColor = Color.Transparent;
            pictureBoxForce.Parent = pictureBoxBackGround;

            pictureBox_location.BackColor = Color.Transparent;
            pictureBox_location.Parent = pictureBoxForce;
            #endregion
            //RealTimePointList.Add(new Point3D(0, 0, 0));
        }

        //实时位置绘制
        private void Thread_DrawRealTimePoint()
        {
            //当实时坐标变化时重新绘制
            while (true)
            {
                Thread.Sleep(20);
                if (UpdateDraws.UpdateRealTimeMap)
                {
                    UpdateDraws.UpdateRealTimeMap = false;
                    //当前位置指示器直径
                    int Radius = 20;

                    Bitmap bmp = new Bitmap(MapSize.Width, MapSize.Height);
                    Graphics g = Graphics.FromImage(bmp);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Brush bush = new SolidBrush(Color.OrangeRed);//填充的颜色

                    //MapSize.Width / Grid / 2 * Grid, MapSize.Height - MapSize.Height / Grid / 2 * Grid

                    //绘制线段
                    if (RealTimePointList.Count >= 2)
                    {
                        for (int i = 0; i < RealTimePointList.Count - 1; i++)
                        {
                            int Drawed_X = (int)(RealTimePointList[i].X * Grid + PointCenter.Now.X * Grid);
                            int Drawed_Y = (int)(MapSize.Height - RealTimePointList[i].Y * Grid - PointCenter.Now.Y * Grid);

                            int Drawed_X1 = (int)(RealTimePointList[i + 1].X * Grid + PointCenter.Now.X * Grid);
                            int Drawed_Y1 = (int)(MapSize.Height - RealTimePointList[i + 1].Y * Grid - PointCenter.Now.Y * Grid);

                            g.DrawLine(new Pen(Color.White, 3), Drawed_X, Drawed_Y, Drawed_X1, Drawed_Y1);
                        }
                    }
                    //绘制位置指示器
                    if (RealTimePointList.Count > 0)
                    {
                        //画三角形
                        float d = 45;
                        float angle = RealTimePointList[RealTimePointList.Count - 1].Z;
                        Point center = new Point();
                        Point[] point = new Point[3];
                        center.X = (int)(RealTimePointList[RealTimePointList.Count - 1].X * Grid + PointCenter.Now.X * Grid);
                        center.Y = (int)(MapSize.Height - RealTimePointList[RealTimePointList.Count - 1].Y * Grid - PointCenter.Now.Y * Grid); ;

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
                        //g.DrawLine(new Pen(Color.Red, 1), center.X, center.Y, point[0].X, point[0].Y);

                        //画圆
                        bush = new SolidBrush(Color.Orange);//填充的颜色
                        g.FillEllipse(bush, center.X - Radius, center.Y - Radius, Radius * 2, Radius * 2);
                    }

                    g.Dispose();
                    this.Invoke(new MethodInvoker(delegate
                    {
                        pictureBox_location.Image = bmp;
                    }));
                    GC.Collect();
                }
            }
        }

        //网格绘制线程
        private void Thread_DrawMap()
        {
            while (true)
            {
                Thread.Sleep(20);
                if (UpdateDraws.UpdateGrid)
                {
                    UpdateDraws.UpdateGrid = false;
                    //获取绘图区域Size
                    MapSize = pictureBoxBackGround.Size;

                    #region 背景图层绘制网格
                    Bitmap bmp = new Bitmap(MapSize.Width, MapSize.Height);

                    Graphics g = Graphics.FromImage(bmp);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    //画笔为灰色
                    Pen pen = new Pen(Color.LightGray);
                    //画虚线
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                    #region 绘制网格
                    int x1, y1, x2, y2;
                    if (Grid >= 10)
                    {
                        //横线
                        for (int i = 0; i < (MapSize.Height) / Grid + 1; i++)
                        {
                            x1 = 0 + PointMove.MovePix.X;
                            y1 = MapSize.Height - Grid * i + PointMove.MovePix.Y;
                            x2 = MapSize.Width/Grid*Grid + PointMove.MovePix.X;
                            y2 = MapSize.Height - Grid * i + PointMove.MovePix.Y;
                            g.DrawLine(pen, x1, y1, x2, y2);
                        }
                        //竖线
                        for (int i = 0; i < MapSize.Width / Grid + 1; i++)
                        {
                            x1 = Grid * i + PointMove.MovePix.X;
                            y1 = MapSize.Height%Grid + PointMove.MovePix.Y;
                            x2 = Grid * i + PointMove.MovePix.X;
                            y2 = MapSize.Height  + PointMove.MovePix.Y;
                            g.DrawLine(pen, x1, y1, x2, y2);
                        }
                    }
                    #endregion

                    #region 绘制中心XY轴坐标箭头
                    pen = new Pen(Color.Yellow);
                    int lineLength = 50;

                    //X轴
                    g.DrawLine(pen, PointCenter.Now.X * Grid - lineLength + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid + PointMove.MovePix.Y, PointCenter.Now.X * Grid + lineLength + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid + PointMove.MovePix.Y);
                    //箭头上部分
                    g.DrawLine(pen, PointCenter.Now.X * Grid + (int)(lineLength / 1.5) + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid - lineLength / 6 + PointMove.MovePix.Y, PointCenter.Now.X * Grid + lineLength + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid + PointMove.MovePix.Y);
                    //箭头下部分
                    g.DrawLine(pen, PointCenter.Now.X * Grid + (int)(lineLength / 1.5) + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid + lineLength / 6 + PointMove.MovePix.Y, PointCenter.Now.X * Grid + lineLength + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid + PointMove.MovePix.Y);

                    //Y轴
                    g.DrawLine(pen, PointCenter.Now.X * Grid + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid - lineLength + PointMove.MovePix.Y, PointCenter.Now.X * Grid + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid + lineLength + PointMove.MovePix.Y);
                    //箭头左部分
                    g.DrawLine(pen, PointCenter.Now.X * Grid - lineLength / 6 + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid - (int)(lineLength / 1.5) + PointMove.MovePix.Y, PointCenter.Now.X * Grid + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid - lineLength + PointMove.MovePix.Y);
                    //箭头右部分
                    g.DrawLine(pen, PointCenter.Now.X * Grid + lineLength / 6 + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid - (int)(lineLength / 1.5) + PointMove.MovePix.Y, PointCenter.Now.X * Grid + PointMove.MovePix.X, MapSize.Height - PointCenter.Now.Y * Grid - lineLength + PointMove.MovePix.Y);
                    #endregion

                    g.Dispose();
                    this.Invoke(new MethodInvoker(delegate
                    {
                        pictureBoxBackGround.Image = bmp;
                    }));
                    GC.Collect();
                    #endregion
                }
            }
        }

        //画图绘制线程
        int DrawPointLength = 1;
        private void Thread_DrawGrid()
        {
            while (true)
            {
                Thread.Sleep(20);

                //当前光标位置
                int x = NowPoint.X * Grid + PointCenter.Now.X * Grid ;
                int y = MapSize.Height - NowPoint.Y * Grid - PointCenter.Now.Y * Grid ;

                //临时公用坐标变量
                //Point point = new Point(); 

                if (MapMode && UpdateDraws.UpdateDrawingMap)
                {
                    UpdateDraws.UpdateDrawingMap = false;

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
                            int Drawed_X = (int)(DrawPointList[i].X * Grid + PointCenter.Now.X * Grid) + PointMove.MovePix.X;
                            int Drawed_Y = (int)(MapSize.Height - DrawPointList[i].Y * Grid - PointCenter.Now.Y * Grid) + PointMove.MovePix.Y;
                            int Drawed_X1 = (int)(DrawPointList[i + 1].X * Grid + PointCenter.Now.X * Grid) + PointMove.MovePix.X;
                            int Drawed_Y1 = (int)(MapSize.Height - DrawPointList[i + 1].Y * Grid - PointCenter.Now.Y * Grid) + PointMove.MovePix.Y;

                            //画已经绘制的线段
                            g.DrawLine(new Pen(Color.White, 2), Drawed_X, Drawed_Y, Drawed_X1, Drawed_Y1);
                            //画小叉号
                            g.DrawLine(new Pen(Color.Gray, 1), Drawed_X - snap / 4, Drawed_Y - snap / 4, Drawed_X + snap / 4, Drawed_Y + snap / 4);
                            g.DrawLine(new Pen(Color.Gray, 1), Drawed_X + snap / 4, Drawed_Y - snap / 4, Drawed_X - snap / 4, Drawed_Y + snap / 4);
                        }
                    }

                    //画正在绘制的线段
                    if (DrawPointList.Count >= 1)
                    {
                        int Drawed_X = (int)(DrawPointList[DrawPointList.Count - 1].X * Grid + PointCenter.Now.X * Grid) + PointMove.MovePix.X;
                        int Drawed_Y = (int)(MapSize.Height - DrawPointList[DrawPointList.Count - 1].Y * Grid - PointCenter.Now.Y * Grid) + PointMove.MovePix.Y;
                        g.DrawLine(new Pen(Color.White, 2), Drawed_X, Drawed_Y, x, y);
                    }
                    g.Dispose();
                    this.Invoke(new MethodInvoker(delegate
                    {
                        pictureBoxForce.Image = bmp;
                    }));
                    GC.Collect();
                }
                else if (UpdateDraws.UpdateDrawedMap)
                {
                    UpdateDraws.UpdateDrawedMap = false;

                    DrawPointLength = DrawPointList.Count;
                    Bitmap bmp = new Bitmap(MapSize.Width, MapSize.Height);
                    Graphics g = Graphics.FromImage(bmp);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Brush bush = new SolidBrush(Color.Red);//填充的颜色

                    int r = 6;
                    //画已经绘制的线段
                    if (DrawPointList.Count > 1)
                    {
                        for (int i = 0; i < DrawPointList.Count - 1; i++)
                        {
                            int Drawed_X = (int)(DrawPointList[i].X * Grid + PointCenter.Now.X * Grid) + PointMove.MovePix.X;
                            int Drawed_Y = (int)(MapSize.Height - DrawPointList[i].Y * Grid - PointCenter.Now.Y * Grid) + PointMove.MovePix.Y;
                            int Drawed_X1 = (int)(DrawPointList[i + 1].X * Grid + PointCenter.Now.X * Grid) + PointMove.MovePix.X;
                            int Drawed_Y1 = (int)(MapSize.Height - DrawPointList[i + 1].Y * Grid - PointCenter.Now.Y * Grid) + PointMove.MovePix.Y;

                            if (mouseWheel)
                            {
                                //画已经绘制的线段
                                g.DrawLine(new Pen(Color.Blue, 3), Drawed_X, Drawed_Y, Drawed_X1, Drawed_Y1);
                                //在拐点处画圆
                                if (Grid >= 10)
                                    g.FillEllipse(bush, Drawed_X - r, Drawed_Y - r, 2 * r, 2 * r);
                            }
                            else
                            {
                                //画已经绘制的线段
                                g.DrawLine(new Pen(Color.Blue, 3), Drawed_X, Drawed_Y, Drawed_X1, Drawed_Y1);
                                //在拐点处画圆
                                if (Grid >= 10)
                                    g.FillEllipse(bush, Drawed_X - r, Drawed_Y - r, 2 * r, 2 * r);
                            }
                        }
                        //画最后一个点
                        if (Grid >= 10)
                        {
                            int Drawed_X2 = (int)(DrawPointList[DrawPointList.Count - 1].X * Grid + PointCenter.Now.X * Grid) + PointMove.MovePix.X;
                            int Drawed_Y2 = (int)(MapSize.Height - DrawPointList[DrawPointList.Count - 1].Y * Grid - PointCenter.Now.Y * Grid) + PointMove.MovePix.Y;
                            g.FillEllipse(bush, Drawed_X2 - r, Drawed_Y2 - r, 2 * r, 2 * r);
                        }
                    }
                    else
                    if (DrawPointList.Count == 1)
                    {
                        //在拐点处画圆
                        int i = DrawPointList.Count - 1;
                        int Drawed_X = (int)(DrawPointList[i].X * Grid + PointCenter.Now.X * Grid) + PointMove.MovePix.X;
                        int Drawed_Y = (int)(MapSize.Height - DrawPointList[i].Y * Grid - PointCenter.Now.Y * Grid) + PointMove.MovePix.Y;
                        if (mouseWheel)
                            g.FillEllipse(bush, Drawed_X - r, Drawed_Y - r, 2 * r, 2 * r);
                        else
                            g.FillEllipse(bush, Drawed_X - r, Drawed_Y - r, 2 * r, 2 * r);

                    }

                    //绘制移动后和移动前坐标之间的虚线
                    if (KeySpace)
                    {
                        int Drawed_X;
                        int Drawed_Y;
                        if (DrawPointList.Count > 0)
                        {
                            Drawed_X = (int)(DrawPointList[0].X * Grid + PointCenter.Now.X * Grid);
                            Drawed_Y = (int)(MapSize.Height - DrawPointList[0].Y * Grid - PointCenter.Now.Y * Grid);
                        }
                        else
                        {
                            Drawed_X = (int)((NowPoint.X + 1) * Grid);
                            Drawed_Y = (int)(MapSize.Height - (NowPoint.Y + 1) * Grid);
                        }

                        int Drawed_X1 = (int)(PointCenter.MoveAgo.X * Grid);
                        int Drawed_Y1 = (int)(MapSize.Height - PointCenter.MoveAgo.Y * Grid);

                        g.DrawLine(new Pen(Color.White, 1), Drawed_X, Drawed_Y, Drawed_X1, Drawed_Y1);
                    }

                    mouseWheel = false;
                    g.Dispose();
                    this.Invoke(new MethodInvoker(delegate
                    {
                        pictureBoxForce.Image = bmp;
                    }));
                    GC.Collect();
                }
            }
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
                    //更新路径长度显示
                    labelRouteLength.Text = DrawPointList.Count.ToString();

                    //重绘界面
                    if (MapMode)
                        UpdateDraws.UpdateDrawingMap = true;
                    else
                        UpdateDraws.UpdateDrawedMap = true;

                    labelRouteLength.Text = DrawPointList.Count.ToString();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                //退出绘图模式
                MapMode = false;
                UpdateDraws.UpdateDrawedMap = true;
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

                    //更新路径长度显示
                    labelRouteLength.Text = DrawPointList.Count.ToString();

                    //重绘界面
                    if (MapMode)
                        UpdateDraws.UpdateDrawingMap = true;
                    else
                        UpdateDraws.UpdateDrawedMap = true;

                    labelRouteLength.Text = DrawPointList.Count.ToString();
                }
            }
        }

        //上一个实时坐标
        Point3D RealTimePoint = new Point3D();
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                try
                {
                    Thread.Sleep(10);
                    Byte[] DataReceivedBuf = new Byte[10000];
                    serialPort1.Read(DataReceivedBuf, 0, serialPort1.BytesToRead);
                    serialPort1.DiscardInBuffer();//清空缓冲区
                    string str = System.Text.Encoding.Default.GetString(DataReceivedBuf).Replace("\0", "");
                    string strr = str;
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

                            if (RealTimePointList.Count == 0)
                            {
                                //更新实时坐标
                                RealTimePointList.Add(new Point3D(x, y, j));
                                RealTimePoint = RealTimePointList[0];
                                UpdateDraws.UpdateRealTimeMap = true;
                            }
                            else
                            if (RealTimePointList.Count == 1)
                            {
                                //更新实时坐标
                                RealTimePointList.Add(new Point3D(x, y, j));
                                RealTimePoint = RealTimePointList[1];
                                UpdateDraws.UpdateRealTimeMap = true;
                            }
                            else
                            {
                                int x11 = (int)(Math.Abs(RealTimePoint.X - x));
                                int y11 = (int)(Math.Abs(RealTimePoint.Y - y));
                                int j11 = (int)(Math.Abs(RealTimePoint.Z - j));
                                if (x11 < 50 && y11 < 50)
                                {
                                    //更新实时坐标
                                    RealTimePointList.Add(new Point3D(x, y, j));
                                    RealTimePoint = RealTimePointList[RealTimePointList.Count - 1];
                                }
                                UpdateDraws.UpdateRealTimeMap = true;
                            }

                            this.Invoke(new MethodInvoker(delegate
                            {
                                //显示
                                labelRealTimeXYZ.Text = string.Format("X:{0}Y:{1}Z:{2}", x, y, j);
                            }));
                        }
                    }
                }
                catch (TimeoutException ex)//超时处理  
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        private class PointMove
        {
            //移动的像素点
            public static Point MovePix = new Point(0, 0);
            //当前光标位置，像素位置
            public static Point NowAirrowPix = new Point(0, 0);

            //第一次
            public static bool First = true;
        }

        private void pictureBox_location_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = MapSize.Height - e.Y;

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
            x = x / Grid - 1;
            y = y / Grid - 1;

            if (MapMode)
            {
                //计算偏差移动造成的偏差
                x -= PointCenter.Now.X - PointCenter.Init.X;
                y -= PointCenter.Now.Y - PointCenter.Init.Y;
            }

            //空格键按下了
            if (KeySpace)
            {
                if (PointMove.First)
                {
                    PointMove.First = false;

                    PointMove.NowAirrowPix.X = e.X;
                    PointMove.NowAirrowPix.Y = MapSize.Height - e.Y;
                }

                PointMove.MovePix.X -= PointMove.NowAirrowPix.X - e.X;
                PointMove.MovePix.Y += PointMove.NowAirrowPix.Y - (MapSize.Height - e.Y);

                labelTest.Text = string.Format("X:{0},Y:{1}", PointMove.MovePix.X, PointMove.MovePix.Y);

                PointMove.NowAirrowPix.X = e.X;
                PointMove.NowAirrowPix.Y = MapSize.Height - e.Y;

                UpdateDraws.UpdateDrawedMap = true;
               // UpdateDraws.UpdateRealTimeMap = true;
                UpdateDraws.UpdateGrid = true;
            }

            NowPoint.X = x;
            NowPoint.Y = y;

            //显示坐标到界面
            label_XY.Text = "X:" + x.ToString() + " Y:" + y.ToString();

            //重新加载界面
            if (MapMode)
                UpdateDraws.UpdateDrawingMap = true;
        }

        public void pictureBox_location_MouseWheel(object sender, MouseEventArgs e)
        {
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

            //根据偏差重置中心点坐标
            if (Grid != 1 && Grid != 400)
            {
                PointCenter.Now.X += (MapSize.Width / Grid / 2 - NowPoint.X) / 1;
                PointCenter.Now.Y += (MapSize.Height / Grid / 2 - NowPoint.Y) / 1;
            }

            //重绘网格
            UpdateDraws.UpdateGrid = true;
            //重绘实时坐标
            UpdateDraws.UpdateRealTimeMap = true;
            //重绘线段
            if (MapMode)
                UpdateDraws.UpdateDrawingMap = true;
            else
                UpdateDraws.UpdateDrawedMap = true;
        }

        private void pictureBox_location_MouseClick(object sender, MouseEventArgs e)
        {
            if (MapMode == false)
            {
                return;
            }
            int x = e.X;
            int y = MapSize.Height - e.Y;

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
            x = x / Grid - 1;
            y = y / Grid - 1;

            if (MapMode)
            {
                //计算偏差移动造成的偏差
                x -= PointCenter.Now.X - PointCenter.Init.X;
                y -= PointCenter.Now.Y - PointCenter.Init.Y;
            }

            NowPoint.X = x;
            NowPoint.Y = y;

            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                //单击了鼠标左键,添加坐标到坐标集合
                DrawPointList.Add(NowPoint);
                labelRouteLength.Text = DrawPointList.Count.ToString();
            }
            else if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                //单击了鼠标右键，退出绘图模式
                MapMode = false;
                UpdateDraws.UpdateDrawedMap = true;
            }

            labelRouteLength.Text = DrawPointList.Count.ToString();
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
            }
            try
            {
                serialPort1.PortName = comboBox1_PortName.SelectedItem.ToString();
                serialPort1.Open();
                button1_openclose.Text = "关闭串口";
                label5_LED.ForeColor = Color.Red;

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

        //手动添加坐标
        private void button_add_point_Click(object sender, EventArgs e)
        {
            try
            {
                //添加点
                int x = int.Parse(textBox_AddX.Text.Trim());
                int y = int.Parse(textBox_AddY.Text.Trim());

                DrawPointList.Add(new Point(x, y));
                labelRouteLength.Text = DrawPointList.Count.ToString();

                //更新界面
                MapMode = false;
                UpdateDraws.UpdateDrawedMap = true;
            }
            catch
            {
            }

        }

        private void dataGridView_RealTimeXY_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //显示序号在HeaderCell上
            //for (int i = 0; i < this.dataGridView_RealTimeXY.Rows.Count; i++)
            //{
            //    DataGridViewRow r = this.dataGridView_RealTimeXY.Rows[i];
            //    r.HeaderCell.Value = string.Format("{0}", i + 1);
            //}
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭实时位置绘图进程
            ThreadRealTime.Abort();
            //关闭网格绘制线程
            ThreadDrawGrid.Abort();
            //关闭画图绘制线程
            ThreadDrawMap.Abort();
        }

        private void button_DrawRoute_Click(object sender, EventArgs e)
        {
            MapMode = true;
        }

        private void pictureBox_location_MouseDown(object sender, MouseEventArgs e)
        {
            //移动地图
            if (e.Button == MouseButtons.Right)
            {
                if (MapMode == false)
                {
                    KeySpace = true;
                    PointMove.First = true;
                    //设置光标为手型
                    this.Cursor = Cursors.Hand;
                    PointCenter.MoveAgo.X = PointCenter.Now.X;
                    PointCenter.MoveAgo.Y = PointCenter.Now.Y;
                }
            }
        }

        private void pictureBox_location_MouseUp(object sender, MouseEventArgs e)
        {
            //移动地图
            if (KeySpace)
            {
                UpdateDraws.UpdateGrid = true;
                UpdateDraws.UpdateDrawedMap = true;
            }
            KeySpace = false;
            //设置光标为箭头
            this.Cursor = Cursors.Arrow;
        }

        //窗口尺寸变化时更新界面
        private void Form1_Resize(object sender, EventArgs e)
        {
            //重绘地图
            UpdateDraws.UpdateGrid = true;
            UpdateDraws.UpdateRealTimeMap = true;
            if (MapMode)
                UpdateDraws.UpdateDrawingMap = true;
            else
                UpdateDraws.UpdateDrawedMap = true;
        }


        #region 按钮事件
        //清除轨迹
        private void buttonClearRealTime_Click(object sender, EventArgs e)
        {
            //清空实时坐标
            RealTimePointList.Clear();
            labelRealTimeXYZ.Text = "X:0 Y:0 Z:0";

            //重绘实时位置
            UpdateDraws.UpdateRealTimeMap = true;
        }

        //加载轨迹
        private void buttonLoadRealTime_Click(object sender, EventArgs e)
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
                        j = 0; RealTimePointList.Clear();
                        XmlElement xe = (XmlElement)xnk;
                        XmlNodeList xnm = xe.ChildNodes;
                        foreach (XmlNode xnkk in xnm)
                        {
                            XmlElement xee = (XmlElement)xnkk;
                            XmlNodeList xng = xee.ChildNodes;
                            foreach (XmlNode xnkkk in xng)
                            {
                                RealTimePointList.Add(new Point3D(int.Parse(xnkkk.ChildNodes[1].ChildNodes.Item(3).InnerText.ToString()), int.Parse(xnkkk.ChildNodes[2].ChildNodes.Item(3).InnerText.ToString()), 0));
                            }
                            j++;
                        }
                    }
                }
                //重绘实时位置
                UpdateDraws.UpdateRealTimeMap = true;
            }
        }

        //保存轨迹
        private void buttonSaveRealTime_Click(object sender, EventArgs e)
        {
            if (RealTimePointList.Count > 1)
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
                        for (int j = 0; j < RealTimePointList.Count; j++)
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
                            writer.WriteElementString("当前值", RealTimePointList[j].X.ToString());
                            writer.WriteElementString("设定值", RealTimePointList[j].X.ToString());
                            writer.WriteElementString("描述", "X坐标，单位CM");
                            writer.WriteEndElement();

                            writer.WriteStartElement("参数2");
                            writer.WriteElementString("参数号", "3");
                            writer.WriteElementString("参数名", "Y");
                            writer.WriteElementString("当前值", RealTimePointList[j].Y.ToString());
                            writer.WriteElementString("设定值", RealTimePointList[j].Y.ToString());
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

        //清除地图
        private void button_Clear_Click(object sender, EventArgs e)
        {
            //清空地图坐标，加入原点坐标
            DrawPointList.Clear();
            DrawPointList.Add(new Point(0, 0));
            labelRouteLength.Text = DrawPointList.Count.ToString();

            //设置为非绘图模式
            MapMode = false;
            //重绘地图
            UpdateDraws.UpdateDrawedMap = true;
        }

        //加载地图
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
                labelRouteLength.Text = DrawPointList.Count.ToString();
                //重绘地图
                UpdateDraws.UpdateDrawedMap = true;
            }
        }

        //保存路径
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
        #endregion

    }
}