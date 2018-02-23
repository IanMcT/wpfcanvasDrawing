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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace canvasImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double t = 20;
        double l = 20;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetFile_Click(object sender, RoutedEventArgs e)
        {
            if (canvas.Children.Count > 0)
            {
                canvas.Children.RemoveAt(0);
            }
            Microsoft.Win32.OpenFileDialog openFileD = new Microsoft.Win32.OpenFileDialog();
            openFileD.ShowDialog();

            BitmapImage bi = new BitmapImage(new Uri(openFileD.FileName));
            System.Windows.Media.ImageBrush ib = new ImageBrush(bi);
            canvas.Background = ib;

            Rectangle r = new Rectangle();
            r.Stroke = System.Windows.Media.Brushes.GreenYellow;
            r.Width = 80;
            r.Height = 60;
            r.StrokeThickness = 2;
            
            canvas.Children.Add(r);
            Canvas.SetLeft(r, l);
            Canvas.SetTop(r, t);
            l += 20;
            t += 20;


            //get pixel
            int stride = bi.PixelWidth * 4;
            int size = bi.PixelHeight * stride;
            byte[] pixels = new byte[size];
            bi.CopyPixels(pixels, stride, 0);



            int x = 799;
            int y = 0;
            int index = y * stride + 4 * x;


            byte blue = pixels[index];
            byte green = pixels[index + 1];
            byte red = pixels[index + 2];
            byte alpha = pixels[index + 3];
            MessageBox.Show(red.ToString() + ", " + green.ToString() + ", " + blue.ToString());

        }
    }
}
