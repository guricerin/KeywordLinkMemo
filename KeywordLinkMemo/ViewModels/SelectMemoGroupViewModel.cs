using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace KeywordLinkMemo.ViewModels
{
    public class SelectMemoGroupViewModel : BindableBase
    {
        private ObservableCollection<Models.MemoGroup> _memoGroup;
        public ObservableCollection<Models.MemoGroup> MemoGroups
        {
            get => _memoGroup;
            set => SetProperty(ref _memoGroup, value);
        }

        public SelectMemoGroupViewModel()
        {

        }
    }
}
