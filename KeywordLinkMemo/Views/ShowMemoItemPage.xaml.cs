using Ganss.Text;
using KeywordLinkMemo.ViewModels;
using Prism.Events;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            var memoGroup = item.Group;
            var memoItem = item.Item;

            var itemNames = memoGroup.MemoItemNames();
            var content = memoItem.Content();
            var aho = new AhoCorasick(itemNames);
            var results = aho.Search(content).ToList();
            var ss = results.Select(x => $"index : {x.Index}, word : {x.Word}");
            var s = string.Join("\n", ss);
            MessageBox.Show($"result\n{s}");
        }
    }
}
