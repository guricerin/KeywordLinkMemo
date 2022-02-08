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
        public Models.MemoGroup SelectedMemoGroup;

        public ObservableCollection<Models.MemoGroup> MemoGroups { get; set; }

        public SelectMemoGroupViewModel()
        {

        }
    }
}
