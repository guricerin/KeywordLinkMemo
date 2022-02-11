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

        public string GroupName { get; }

        public MemoItem(string path)
        {
            FilePath = path;
            GroupName = Directory.GetParent(path).Name;
        }

        public string Content()
        {
            return File.ReadAllText(FilePath);
        }
    }
}
