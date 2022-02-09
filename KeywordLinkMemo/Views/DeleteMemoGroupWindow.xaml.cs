using KeywordLinkMemo.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace KeywordLinkMemo.Views
{
    public interface IDeleteMemoGroupReceiver
    {
        void ReceiveDeleteMemoGroup(Models.MemoGroup group);
    }

    /// <summary>
    /// Interaction logic for DeleteMemoGroupWindow.xaml
    /// </summary>
    public partial class DeleteMemoGroupWindow : Window
    {
        private IDeleteMemoGroupReceiver _receiver;

        private Models.MemoGroup _selectedMemoGroup;

        public DeleteMemoGroupWindow(IDeleteMemoGroupReceiver receiver, ObservableCollection<Models.MemoGroup> memoGroups)
        {
            InitializeComponent();

            //DataContext = new DeleteMemoGroupWindowViewModel();
            _receiver = receiver;
            var vm = (DeleteMemoGroupWindowViewModel)DataContext;
            vm.MemoGroups = memoGroups;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMemoGroup != null)
            {
                _receiver.ReceiveDeleteMemoGroup(_selectedMemoGroup);
                Close();
            }
            else
            {
                MessageBox.Show("グループが選択されていません。", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SelectMemoGroupListView.SelectedItem == null)
            {
                return;
            }

            var item = (Models.MemoGroup)SelectMemoGroupListView.SelectedItem;
            _selectedMemoGroup = item;
        }
    }
}
