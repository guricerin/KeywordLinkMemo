using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeywordLinkMemo.ViewModels
{
    public class DeleteMemoItemWindowViewModel : BindableBase
    {
        private Models.MemoGroup _memoGroup;
        public Models.MemoGroup MemoGroup
        {
            get => _memoGroup;
            set => SetProperty(ref _memoGroup, value);
        }

        public DeleteMemoItemWindowViewModel()
        {

        }
    }
}
