using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeywordLinkMemo.ViewModels
{
    public class CreateMemoGroupWindowViewModel : BindableBase
    {
        private string _memoGroupName;
        public string MemoGroupName
        {
            get => _memoGroupName;
            set => SetProperty(ref _memoGroupName, value);
        }

        public CreateMemoGroupWindowViewModel()
        {

        }
    }
}
