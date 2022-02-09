using System.IO;
using System.Windows;
using KeywordLinkMemo.ViewModels;

namespace KeywordLinkMemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISelectedMemoGroupReceiver, ICreateMemoGroupNameReceiver, IDeleteMemoGroupReceiver
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

        private void CreateMemoGroup_Click(object sender, RoutedEventArgs e)
        {
            var win = new CreateMemoGroupWindow(this);
            win.ShowDialog();
        }

        public void ReceiveCreateMemoGroupName(string name)
        {
            var vm = (MainWindowViewModel)DataContext;
            var path = Path.Combine(vm.MemosPath, name);
            if (Directory.Exists(path))
            {
                MessageBox.Show("指定のグループ名はすでに存在しています。", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                Directory.CreateDirectory(path);
                MessageBox.Show("作成しました。", "", MessageBoxButton.OK, MessageBoxImage.Information);
                vm.UpdateMemoGroups();
            }
        }

        private void DeleteMemoGroup_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)DataContext;
            var win = new DeleteMemoGroupWindow(this, vm.MemoGroups);
            win.ShowDialog();
        }

        public void ReceiveDeleteMemoGroup(Models.MemoGroup group)
        {
            var vm = (MainWindowViewModel)DataContext;
            vm.DeleteMemoGroup(group);
            MessageBox.Show("削除しました。", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
