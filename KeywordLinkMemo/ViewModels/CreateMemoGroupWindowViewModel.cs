using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KeywordLinkMemo.ViewModels
{
    public class CreateMemoGroupWindowViewModel : BindableBase
    {
        public string MemoGroupName { get; set; } = "";

        public CreateMemoGroupWindowViewModel()
        {

        }
    }
}
