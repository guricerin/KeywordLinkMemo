using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using KeywordLinkMemo.ViewModels;
using Prism.Services.Dialogs;

namespace KeywordLinkMemo.Views
{
    public interface ISelectedMemoGroupReceiver
    {
        void ReceiveSelectedMemoGroup(Models.MemoGroup group);
    }

    /// <summary>
    /// SelectMemoGroupWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectMemoGroupWindow : IDialogWindow
    {
        public IDialogResult Result { get; set; }

        private ISelectedMemoGroupReceiver _receiver;

        private Models.MemoGroup _selectedMemoGroup;

        public SelectMemoGroupWindow(ISelectedMemoGroupReceiver receiver, ObservableCollection<Models.MemoGroup> memoGroups)
        {
            InitializeComponent();

            DataContext = new SelectMemoGroupViewModel();
            _receiver = receiver;
            var vm = (SelectMemoGroupViewModel)DataContext;
            vm.MemoGroups = memoGroups;
        }

        private void DecisionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMemoGroup != null)
            {
                _receiver.ReceiveSelectedMemoGroup(_selectedMemoGroup);
            }
            Close();
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
