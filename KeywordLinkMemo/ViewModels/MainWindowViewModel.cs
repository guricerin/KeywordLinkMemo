using Prism.Mvvm;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KeywordLinkMemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region property
        private string _title = "Keyword Link Memo";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _memosPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'), "memos");
        public string MemosPath
        {
            get { return _memosPath; }
        }

        private List<Models.MemoGroup> _memoGroups = new List<Models.MemoGroup>();
        public List<Models.MemoGroup> MemoGroups
        {
            get { return _memoGroups; }
        }
        #endregion

        public MainWindowViewModel()
        {
            if (!Directory.Exists(MemosPath))
            {
                Directory.CreateDirectory(MemosPath);
                var sample = Path.Combine(MemosPath, "最初のグループ");
                Directory.CreateDirectory(sample);
                File.Create(Path.Combine(sample, "index"));
                File.Create(Path.Combine(sample, "01.txt"));
            }

            UpdateMemoGroupsTreeView();
        }

        public void UpdateMemoGroupsTreeView()
        {
            foreach(var dirPath in Directory.GetDirectories(MemosPath))
            {
                var dirName = Path.GetFileName(dirPath);
                var memoGroup = new Models.MemoGroup(dirName);
                foreach(var filePath in Directory.GetFiles(dirPath))
                {
                    if(Path.GetExtension(filePath) == ".txt")
                    {
                        var fileName = Path.GetFileNameWithoutExtension(filePath);
                        memoGroup.MemoNames.Add(new Models.MemoGroup(fileName));
                    }
                }
                _memoGroups.Add(memoGroup);
            }
        }

        #region command
        #endregion
    }
}
