using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KeywordLinkMemo.ViewModels
{
    public class EditMemoItemPageViewModel : BindableBase, INavigationAware
    {
        private Models.MemoItem _memoItem;
        public Models.MemoItem MemoItem
        {
            get => _memoItem;
            set => SetProperty(ref _memoItem, value);
        }

        private string _content;
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private string _prevContent;

        public EditMemoItemPageViewModel()
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            MemoItem = navigationContext.Parameters["MemoItem"] as Models.MemoItem;
            Content = File.ReadAllText(MemoItem.FilePath);
            _prevContent = Content;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void Save()
        {
            File.WriteAllText(MemoItem.FilePath, Content);
            _prevContent = Content;
        }

        public void Reload()
        {
            Content = _prevContent;
        }
    }
}
