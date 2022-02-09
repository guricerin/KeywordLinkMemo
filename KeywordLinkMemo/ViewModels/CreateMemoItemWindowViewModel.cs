using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeywordLinkMemo.ViewModels
{
    public class CreateMemoItemWindowViewModel : BindableBase
    {
        private string _memoGroupName;
        public string MemoGroupName
        {
            get => "グループ名 : " + _memoGroupName;
            set => SetProperty(ref _memoGroupName, value);
        }

        private string _memoItemName;
        public string MemoItemName
        {
            get => _memoItemName;
            set => SetProperty(ref _memoItemName, value);
        }

        public CreateMemoItemWindowViewModel()
        {

        }
    }
}
