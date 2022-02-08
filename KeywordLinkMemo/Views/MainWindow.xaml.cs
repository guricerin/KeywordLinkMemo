using System.Windows;
using KeywordLinkMemo.ViewModels;

namespace KeywordLinkMemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISelectedMemoGroupReceiver
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectMemoGroup_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)DataContext;
            var win = new SelectMemoGroupWindow(this, vm.MemoGroups);
            win.ShowDialog();
        }

        public void ReceiveSelectedMemoGroup(Models.MemoGroup group){
            var vm = (MainWindowViewModel)DataContext;
            vm.SelectedMemoGroup = group;
        }
    }
}
