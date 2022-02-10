using KeywordLinkMemo.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace KeywordLinkMemo.Views
{
    /// <summary>
    /// Interaction logic for EditMemoItemPage
    /// </summary>
    public partial class EditMemoItemPage : UserControl
    {
        public EditMemoItemPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (EditMemoItemPageViewModel)DataContext;
            vm.Save();
            MessageBox.Show("上書きしました。", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ReloadButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (EditMemoItemPageViewModel)DataContext;
            vm.Reload();
        }
    }
}
