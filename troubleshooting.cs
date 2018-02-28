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
        Point p1 = new Point();
        Point p2 = new Point();
        BitmapImage bi;
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

            bi = new BitmapImage(new Uri(openFileD.FileName));
            
            System.Windows.Media.ImageBrush ib = new ImageBrush(bi);
            ib.Stretch = Stretch.Uniform;
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

        private void btnGetFile_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            window.Title = e.GetPosition(canvas).ToString();
            p1 = e.GetPosition(canvas);
        }

        private void btnGetFile_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            p2 = e.GetPosition(canvas);
            if (canvas.Children.Count > 0)
            {
                canvas.Children.RemoveAt(0);
            }
            Rectangle r = new Rectangle();
            r.Stroke = System.Windows.Media.Brushes.GreenYellow;
            r.Width = p2.X - p1.X;
            r.Height = p2.Y - p1.Y;
            r.StrokeThickness = 2;

            canvas.Children.Add(r);
            Canvas.SetLeft(r, p1.X);
            Canvas.SetTop(r, p1.Y);


            //get pixel
            //            int stride = bi.PixelWidth * 4;
            MessageBox.Show("Bits per pixel: " + bi.Format.BitsPerPixel.ToString());
            int stride = bi.PixelWidth * (bi.Format.BitsPerPixel / 8);
            int size = bi.PixelHeight * stride;
            double di = bi.DpiX;
            byte[] pixels = new byte[size];
            bi.CopyPixels(pixels, stride, 0);

            int totalRed = 0;
            int totalBlue = 0;
            int totalGreen = 0;
            int maxRed = 0;
            int maxBlue = 0;
            int maxGreen = 0;
            int minRed = 255;
            int minBlue = 255;
            int minGreen = 255;
            int counter = 0;
            MessageBox.Show("p1 " + p1.ToString());
            
            MessageBox.Show("resolution: " + bi.DecodePixelHeight.ToString());
            MessageBox.Show("Canvas width = " + canvas.Width.ToString() + " bi width: " + bi.Width.ToString());
            MessageBox.Show("Point 1: " + ((int)((p1.X / canvas.Width) * bi.Width)).ToString() + ", " + ((int)((p1.Y / canvas.Height) * bi.Height)).ToString());
            MessageBox.Show(((int)((p2.X / canvas.Width) * bi.Width)).ToString() + ", " + ((int)((p2.Y / canvas.Height) * bi.Height)).ToString());
            // for (int x = (int)((p1.X/canvas.Width)*bi.Width); x < (int)((p2.X/canvas.Width)*bi.Width); x++)
            for(int x = 1419; x < 1597; x++)
            {
                //                for(int y = (int)((p1.Y/canvas.Height)*bi.Height); y < (int)((p2.Y/canvas.Height)*bi.Height); y++)
                for(int y = 729; y < 927; y++)
                {
                    int index = y * stride + 4 * x;
                    byte blue = pixels[index];
                    byte green = pixels[index + 1];
                    byte red = pixels[index + 2];
                    byte alpha = pixels[index + 3];
                    totalBlue += (int)blue;
                    totalGreen += (int)green;
                    totalRed += (int)red;
                    if ((int)green > maxGreen) maxGreen = (int)green;
                    if ((int)red > maxRed) maxRed = (int)red;
                    if ((int)blue > maxBlue) maxBlue = (int)blue;
                    if ((int)green < minGreen) minGreen = (int)green;
                    if ((int)blue < minBlue) minBlue = (int)blue;
                    if ((int)red < minRed) minRed = (int)red;
                    counter++;
                }
            }

            totalBlue = totalBlue / counter;
            totalGreen = totalGreen / counter;
            totalRed = totalRed / counter;



            
            MessageBox.Show(totalRed.ToString() + ", " + totalGreen.ToString() + ", " + totalBlue.ToString());
            MessageBox.Show("Max: " + maxRed.ToString() + ", " + maxGreen.ToString() + ", " + maxBlue.ToString());
            MessageBox.Show("Min: " + minRed.ToString() + ", " + minGreen.ToString() + ", " + minBlue.ToString());
        }
    }
}
