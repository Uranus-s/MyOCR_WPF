using MyOCR_WPF.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyOCR_WPF
{
    /// <summary>
    /// Clipper.xaml 的交互逻辑
    /// </summary>
    public partial class Clipper : Window
    {
        public System.Drawing.Bitmap bitmap;
        private Point mouseDown;
        private Point mouseUp;
        private double width;
        private double height;
        private double left;
        private double top;

        public Clipper()
        {
            InitializeComponent();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //通过坐标计算矩形的宽高
                width = mouseDown.X - Mouse.GetPosition(this).X;
                height = mouseDown.Y - Mouse.GetPosition(this).Y;

                //计算矩形的上左边距
                left = mouseDown.X - Mouse.GetPosition(this).X < 0 ? mouseDown.X : Mouse.GetPosition(this).X;
                top = mouseDown.Y - Mouse.GetPosition(this).Y < 0 ? mouseDown.Y : Mouse.GetPosition(this).Y;

                DrawRectangle(Math.Abs(width), Math.Abs(height), left, top);
            }

            
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseDown = Mouse.GetPosition(this);
        }
        /// <summary>
        /// 鼠标抬起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseUp = Mouse.GetPosition(this);

            System.Drawing.Bitmap bit = ImageProcessing.MakeThumbnailImage(bitmap, bitmap.Width, bitmap.Height, Convert.ToInt32(width), Convert.ToInt32(height), Convert.ToInt32(left), Convert.ToInt32(top));

            bit.Save(@"‪C:\Users\L-KAMI\Desktop\Imgs\test.png", System.Drawing.Imaging.ImageFormat.Png);

            this.Close();
        }

        /// <summary>
        /// 绘制矩形的方法
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="left">左边距</param>
        /// <param name="top">上边距</param>
        private void DrawRectangle(double width, double height, double left, double top)
        {
            canvas.Children.Clear();//清除画布

            Rectangle rct = new Rectangle();

            Canvas.SetTop(rct, top);//通过鼠标按下时记录的坐标设置矩形与左上角的距离
            Canvas.SetLeft(rct, left);

            rct.Width = width;
            rct.Height = height;

            rct.Stroke = Brushes.Red;
            rct.StrokeThickness = 1;

            canvas.Children.Add(rct);
        }
    }
}
