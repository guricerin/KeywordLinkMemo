using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace KeywordLinkMemo.ViewModels
{
    public class ShowMemoItemPageViewModel : BindableBase, INavigationAware
    {
        private string _content;
        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public ShowMemoItemPageViewModel()
        {

        }

        // true ：インスタンスを使いまわす(画面遷移してもコンストラクタ呼ばれない)
        // false：インスタンスを使いまわさない(画面遷移するとコンストラクタ呼ばれる)
        // ※コンストラクタは呼ばれないが、Loadedイベントは起きる
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// このviewが表示された状態から切り替わるときに実行される
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        /// <summary>
        /// このviewが表示されるときに実行される
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var memoItem = navigationContext.Parameters["MemoItem"] as Models.MemoItem;
            Content = File.ReadAllText(memoItem.FilePath);
        }
    }
}
