using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Prism.Events;

namespace KeywordLinkMemo.ViewModels
{
    public class AhoItem
    {
        public Models.MemoGroup Group { get; set; }
        public Models.MemoItem Item { get; set; }
    }

    public class ShowMemoItemPageViewModel : BindableBase, INavigationAware
    {
        private string _title = "hoge";
        public string Title
        {
            get => _title;
            private set => SetProperty(ref _title, value);
        }

        private string _content;
        public string Content
        {
            get => _content;
            private set => SetProperty(ref _content, value);
        }

        /// <summary>
        /// VMからViewを操作するための機能。
        /// </summary>
        private IEventAggregator _eventAggregator;

        public ShowMemoItemPageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// true ：インスタンスを使いまわす(画面遷移してもコンストラクタ呼ばれない)
        /// ※コンストラクタは呼ばれないが、Loadedイベントは起きる
        /// false：インスタンスを使いまわさない(画面遷移するとコンストラクタ呼ばれる) 
        /// </summary>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// このviewが表示された状態から切り替わるときに実行される
        /// </summary>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// このviewが表示されるときに実行される
        /// </summary>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var memoGroup = navigationContext.Parameters["MemoGroup"] as Models.MemoGroup;
            var memoItem = navigationContext.Parameters["MemoItem"] as Models.MemoItem;
            Title = memoItem.Name;
            Content = File.ReadAllText(memoItem.FilePath);

            var ahoItem = new AhoItem { Group = memoGroup, Item = memoItem };
            _eventAggregator.GetEvent<PubSubEvent<AhoItem>>().Publish(ahoItem);
        }
    }
}
