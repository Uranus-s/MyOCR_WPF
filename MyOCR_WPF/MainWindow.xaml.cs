using MyOCR_WPF.Tool;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyOCR_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取屏幕图片
        /// </summary>
        /// <returns></returns>
        public Bitmap GetScreenSnapshot()
        {
            try
            {
                System.Drawing.Rectangle rc = SystemInformation.VirtualScreen;
                var bitmap = new Bitmap(rc.Width, rc.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                using (Graphics memoryGrahics = Graphics.FromImage(bitmap))
                {
                    memoryGrahics.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                }

                return bitmap;
            }

            catch (Exception)
            {

            }
            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var screenSnapshot = GetScreenSnapshot();
            var bmp = ImageProcessing.ToBitmapSource(screenSnapshot);
            bmp.Freeze();


            Clipper clipper = new Clipper();
            clipper.bitmap = screenSnapshot;
            clipper.Background = new ImageBrush(bmp);

            clipper.Show();
        }
    }
}
