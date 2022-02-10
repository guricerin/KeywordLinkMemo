using KeywordLinkMemo.ViewModels;
using System.Windows;
using System.IO;

namespace KeywordLinkMemo.Views
{
    public interface ICreateMemoGroupNameReceiver
    {
        void ReceiveCreateMemoGroupName(string name);
    }

    /// <summary>
    /// Interaction logic for CreateMemoGroupWindow.xaml
    /// </summary>
    public partial class CreateMemoGroupWindow : Window
    {
        private ICreateMemoGroupNameReceiver _receiver;

        public CreateMemoGroupWindow(ICreateMemoGroupNameReceiver receiver)
        {
            InitializeComponent();

            _receiver = receiver;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = (CreateMemoGroupWindowViewModel)DataContext;
            var name = vm.MemoGroupName;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("空欄にはできません。", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            _receiver.ReceiveCreateMemoGroupName(name);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
