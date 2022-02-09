using KeywordLinkMemo.ViewModels;
using System.Windows;

namespace KeywordLinkMemo.Views
{
    public interface ICreateMemoItemNameReceiver
    {
        void ReceiveCreateMemoItemName(string name);
    }

    /// <summary>
    /// Interaction logic for CreateMemoItemWindow.xaml
    /// </summary>
    public partial class CreateMemoItemWindow : Window
    {
        private ICreateMemoItemNameReceiver _receiver;

        public CreateMemoItemWindow(ICreateMemoItemNameReceiver receiver, string memoGroupName)
        {
            InitializeComponent();

            _receiver = receiver;
            var vm = (CreateMemoItemWindowViewModel)DataContext;
            vm.MemoGroupName = memoGroupName;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (CreateMemoItemWindowViewModel)DataContext;
            var name = vm.MemoItemName;
            if (name == "")
            {
                MessageBox.Show("空欄にはできません。", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _receiver.ReceiveCreateMemoItemName(name);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
