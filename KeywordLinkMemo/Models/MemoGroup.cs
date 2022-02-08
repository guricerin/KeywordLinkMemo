using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace KeywordLinkMemo.Models
{
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
    }
}
