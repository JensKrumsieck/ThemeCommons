using System.Windows;
using ThemeCommons.Controls;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DefaultWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e) => OpenMe.Visibility = (OpenMe.Visibility == Visibility.Collapsed
            ? Visibility.Visible
            : Visibility.Collapsed);
    }
}
