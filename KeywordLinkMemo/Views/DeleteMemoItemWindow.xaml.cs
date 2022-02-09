using KeywordLinkMemo.ViewModels;
using System.Windows;

namespace KeywordLinkMemo.Views
{
    public interface IDeleteMemoItemReceiver
    {
        void ReceiveDeleteMemoItem(Models.MemoItem item);
    }

    /// <summary>
    /// Interaction logic for DeleteMemoItemWindow.xaml
    /// </summary>
    public partial class DeleteMemoItemWindow : Window
    {
        private IDeleteMemoItemReceiver _receiver;

        private Models.MemoItem _selectedMemoItem;

        public DeleteMemoItemWindow(IDeleteMemoItemReceiver receiver, Models.MemoGroup memoGroup)
        {
            InitializeComponent();

            _receiver = receiver;
            var vm = (DeleteMemoItemWindowViewModel)DataContext;
            vm.MemoGroup = memoGroup;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMemoItem != null)
            {
                _receiver.ReceiveDeleteMemoItem(_selectedMemoItem);
                Close();
            }
            else
            {
                MessageBox.Show("項目が選択されていません。", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SelectMemoItemListView.SelectedItem == null)
            {
                return;
            }

            var item = (Models.MemoItem)SelectMemoItemListView.SelectedItem;
            _selectedMemoItem = item;
        }
    }
}
