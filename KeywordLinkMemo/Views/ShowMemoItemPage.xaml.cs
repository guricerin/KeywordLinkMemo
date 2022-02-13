using Ganss.Text;
using KeywordLinkMemo.ViewModels;
using Prism.Events;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace KeywordLinkMemo.Views
{
    /// <summary>
    /// Interaction logic for ShowMemoItemPage
    /// </summary>
    public partial class ShowMemoItemPage : UserControl
    {
        public ShowMemoItemPage(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            eventAggregator.GetEvent<PubSubEvent<AhoItem>>().Subscribe(BuildMemoItemContent);
        }

        /// <summary>
        /// キーワードリンクを付与したテキストを構築。
        /// </summary>
        /// <param name="item"></param>
        private void BuildMemoItemContent(AhoItem item)
        {
            MemoItemTextBlock.Inlines.Clear();

            var content = item.Item.Content();
            var aho = new AhoCorasick(item.Group.MemoItemNames());
            var results = aho.Search(content).ToList();

            int resIdx = 0;
            // 前から貪欲。キーワード間に一部重複する語があった場合、テキストの前方が優先される。
            results.OrderBy(x => x.Index);
            for (int l = 0; l < content.Length;)
            {
                if (resIdx < results.Count)
                {
                    if (l == results[resIdx].Index)
                    {
                        // uriのホスト部分を遷移先の項目名とする。
                        var link = new Hyperlink { NavigateUri = new Uri($"http://{results[resIdx].Word}") };
                        link.RequestNavigate += Hyperlink_RequestNavigate;
                        link.Inlines.Add(new Run
                        {
                            Text = results[resIdx].Word,
                        });
                        MemoItemTextBlock.Inlines.Add(link);

                        l += results[resIdx].Word.Length;
                        resIdx++;
                    }
                    else
                    {
                        int range = results[resIdx].Index - l;
                        if (range < 1)
                        {
                            resIdx++;
                            continue;
                        }
                        MemoItemTextBlock.Inlines.Add(new Run
                        {
                            Text = content.Substring(l, range),
                        });

                        l += range;
                    }
                }
                else
                {
                    int range = content.Length - l;
                    MemoItemTextBlock.Inlines.Add(new Run
                    {
                        Text = content.Substring(l, range),
                    });
                    break;
                }
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            e.Handled = true;
            var vm = (ShowMemoItemPageViewModel)DataContext;
            var item = vm.MemoGroup.FetchItemByName(e.Uri.Host);
            vm.HyperlinkEvent?.Invoke(item);
        }
    }
}
