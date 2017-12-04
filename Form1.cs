using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        Point[] point;
        //点坐标个数
        int length = 0;
        //图层初始化
        void Draw_Init()
        {
            size = pictureBoxForce.Size;
            point = new Point[1000];

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

            Bitmap bmp = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Brush bush = new SolidBrush(Color.Black);//填充的颜色

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



            //在图片上画圆
            //g.FillEllipse(bush, x - r, y - r, 2 * r, 2 * r);

            //g.DrawLine(new Pen(Color.Red), 0,1454, withx, heithty);//坐标
            //g.DrawLine(new Pen(Color.Red, 5), 0, 0, x, y);//坐标

            g.Dispose();
            pictureBoxForce.Image = bmp;
        }

        private void pictureBoxForce_MouseClick(object sender, MouseEventArgs e)
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

            point[length++] = new Point(x, y);
            textBox1.Text = string.Empty;
            textBox1.AppendText("point.Length:" + length.ToString() + "\r\n");
            for (int i = 0; i < length; i++)
            {
                textBox1.AppendText( i.ToString() + " x:" + (point[i].X/snapx).ToString() + ",y:" + (point[i].Y/snapy).ToString() + "\r\n");
            }
        }

        private void pictureBoxForce_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }
    }
}
