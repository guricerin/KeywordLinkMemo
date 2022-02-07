using System.Windows;

namespace KeywordLinkMemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var exepath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            MessageBox.Show(exepath);
        }
    }
}
