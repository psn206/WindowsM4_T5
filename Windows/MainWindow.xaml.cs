using System;
using System.Collections.Generic;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(openFileDialog.FileName));
                myInkCanvas.Height = bitmapImage.Height;
                myInkCanvas.Width = bitmapImage.Width;
                myInkCanvas.Background = new ImageBrush(bitmapImage);


            }
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Сохранение штрихов конваса в пиксильное изображение
                RenderTargetBitmap bmp = new RenderTargetBitmap(
             (int)myInkCanvas.Width, (int)myInkCanvas.Height, 96d, 96d, PixelFormats.Pbgra32);
                myInkCanvas.Measure(new Size((int)myInkCanvas.Width, (int)myInkCanvas.Height));
                myInkCanvas.Arrange(new Rect(new Size((int)myInkCanvas.Width, (int)myInkCanvas.Height)));
                bmp.Render(myInkCanvas);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                using (FileStream file = File.Create(saveFileDialog.FileName))
                {
                    encoder.Save(file);
                }
            }
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var rezult = System.Windows.MessageBox.Show("Сохранить файл?",
                      "Сообщение",
                       MessageBoxButton.YesNo,
                       MessageBoxImage.Question
                      );
            if (rezult == MessageBoxResult.Yes)
            {
                MenuItem_Click_1(sender, e);
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                colourButton.Background = new SolidColorBrush(Color.FromArgb(dialog.Color.A,
                    dialog.Color.R, dialog.Color.G, dialog.Color.B));
                myInkCanvas.DefaultDrawingAttributes.Color = Color.FromArgb(dialog.Color.A,
                    dialog.Color.R, dialog.Color.G, dialog.Color.B);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((sender as System.Windows.Controls.ComboBox).SelectedItem as TextBlock).Text == "Ластик")
            {
                myInkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }
            else { myInkCanvas.EditingMode = InkCanvasEditingMode.Ink; }
        }
        private void inSize_Click(object sender, RoutedEventArgs e)
        {
            myInkCanvas.Height = Convert.ToDouble(heightConvas.Text);
            myInkCanvas.Width = Convert.ToDouble(widhConvas.Text);
        }
    }
}
