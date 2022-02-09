using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KeywordLinkMemo.ViewModels
{
    public class DeleteMemoGroupWindowViewModel : BindableBase
    {
        private ObservableCollection<Models.MemoGroup> _memoGroup;
        public ObservableCollection<Models.MemoGroup> MemoGroups
        {
            get => _memoGroup;
            set => SetProperty(ref _memoGroup, value);
        }

        public DeleteMemoGroupWindowViewModel()
        {

        }
    }
}
