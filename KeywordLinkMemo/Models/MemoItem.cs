using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace KeywordLinkMemo.Models
{
    /// <summary>
    /// 実態は <exeディレクトリ>/memo/<グループ名>/<項目名.txt> ファイル
    /// </summary>
    public class MemoItem
    {
        public string FilePath { get; }

        public string Name { get { return Path.GetFileNameWithoutExtension(FilePath); } }

        public ObservableCollection<MemoItem> MemoNames { get; set; }

        public MemoItem(string path)
        {
            FilePath = path;
            MemoNames = new ObservableCollection<MemoItem>();
        }

        public void AddItem(string memoFilePath)
        {
            MemoNames.Add(new MemoItem(memoFilePath));
        }
    }
}
