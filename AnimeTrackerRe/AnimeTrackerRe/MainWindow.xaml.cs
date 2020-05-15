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

namespace AnimeTrackerRe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var graphics = System.Drawing.Graphics.FromHwnd(IntPtr.Zero);
            var pixelWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            var pixelHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            var pixelToDPI = 96.0 / graphics.DpiX;
            this.Width = pixelWidth * pixelToDPI;
            this.Height = pixelHeight * pixelToDPI;
            this.Left = 0;
            this.Top = 0;
            this.WindowState = WindowState.Normal;
        }

        private void AnimeListTab_Loaded(object sender, RoutedEventArgs e) { }
        //private void AnimeViwerTab_Loaded(object sender, RoutedEventArgs e) { }
    }
}
