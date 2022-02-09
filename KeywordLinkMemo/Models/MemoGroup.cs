using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace KeywordLinkMemo.Models
{
    /// <summary>
    /// 実態は <exeディレクトリ>/memo/<グループ名> ディレクトリ
    /// </summary>
    public class MemoGroup
    {
        public string DirPath { get; }

        public string Name { get { return Path.GetFileName(DirPath); } }

        public ObservableCollection<MemoItem> MemoItems { get; set; } = new ObservableCollection<MemoItem>();

        public MemoGroup(string path)
        {
            DirPath = path;
        }

        public void AddItem(string memoFilePath)
        {
            MemoItems.Add(new MemoItem(memoFilePath));
        }

        public void DeleteItem(MemoItem item)
        {
            MemoItems.Remove(item);
        }

        public void Clear()
        {
            MemoItems.Clear();
        }
    }
}
