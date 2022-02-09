using System.IO;
using System.Windows;
using KeywordLinkMemo.ViewModels;

namespace KeywordLinkMemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISelectedMemoGroupReceiver, ICreateMemoGroupNameReceiver, IDeleteMemoGroupReceiver, ICreateMemoItemNameReceiver, IDeleteMemoItemReceiver
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
                File.Create(Path.Combine(path, MainWindowViewModel.INDEX_FILE_NAME));
                vm.UpdateMemoGroups();
                MessageBox.Show("作成しました。", "", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void MemoItemsListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var vm = (MainWindowViewModel)DataContext;
            MessageBox.Show(vm.SelectedMemoItem.Name);
        }

        private void CreateMemoItem_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)DataContext;
            if (vm.SelectedMemoGroup == null)
            {
                MessageBox.Show("作成項目を含めるグループを選択してください。", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var win = new CreateMemoItemWindow(this, vm.SelectedMemoGroup.Name);
            win.ShowDialog();
        }

        public void ReceiveCreateMemoItemName(string name)
        {
            var vm = (MainWindowViewModel)DataContext;
            var path = Path.Combine(vm.SelectedMemoGroup.DirPath, name);
            path = $"{path}.txt";
            if (File.Exists(path))
            {
                MessageBox.Show("指定の項目はすでに存在しています。", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                File.Create(path);
                vm.AppendIndexFile(name);
                vm.UpdateSelectedMemoGroup();
                MessageBox.Show("作成しました。", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteMemoItem_Click(object sender, RoutedEventArgs e)
        {
            var vm = (MainWindowViewModel)DataContext;
            if (vm.SelectedMemoGroup == null)
            {
                MessageBox.Show("削除したい項目が含まれるグループを選択してください。", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var win = new DeleteMemoItemWindow(this, vm.SelectedMemoGroup);
            win.ShowDialog();
        }

        public void ReceiveDeleteMemoItem(Models.MemoItem item)
        {
            var vm = (MainWindowViewModel)DataContext;
            vm.DeleteMemoItem(item);
            MessageBox.Show("削除しました。", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
