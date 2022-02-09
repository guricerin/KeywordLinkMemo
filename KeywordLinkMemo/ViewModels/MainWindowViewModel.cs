using Prism.Mvvm;
using Prism.Commands;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KeywordLinkMemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public const string INDEX_FILE_NAME = "index";

        #region property
        private string _memosPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\'), "memos");
        public string MemosPath
        {
            get { return _memosPath; }
        }

        private ObservableCollection<Models.MemoGroup> _memoGroups = new ObservableCollection<Models.MemoGroup>();
        public ObservableCollection<Models.MemoGroup> MemoGroups
        {
            get { return _memoGroups; }
            set { SetProperty(ref _memoGroups, value); }
        }

        private Models.MemoGroup _selectedMemoGroup;
        public Models.MemoGroup SelectedMemoGroup
        {
            get { return _selectedMemoGroup; }
            set { SetProperty(ref _selectedMemoGroup, value); }
        }

        private Models.MemoItem _selectedMemoItem;
        public Models.MemoItem SelectedMemoItem
        {
            get { return _selectedMemoItem; }
            set { SetProperty(ref _selectedMemoItem, value); }
        }
        #endregion

        public MainWindowViewModel()
        {
            if (!Directory.Exists(MemosPath))
            {
                Directory.CreateDirectory(MemosPath);
                var sample = Path.Combine(MemosPath, "最初のグループ");
                Directory.CreateDirectory(sample);
                File.Create(Path.Combine(sample, INDEX_FILE_NAME));
                File.Create(Path.Combine(sample, "01.txt"));
                File.Create(Path.Combine(sample, "サンプルめも.txt"));
            }

            UpdateMemoGroups();
            _selectedMemoGroup = _memoGroups[0];
        }

        public void UpdateMemoGroups()
        {
            _memoGroups.Clear();

            // いまのところ、グループ（ディレクトリ）は1階層目にしか存在しない想定。
            foreach (var dirPath in Directory.GetDirectories(MemosPath))
            {
                var memoGroup = new Models.MemoGroup(dirPath);
                foreach (var filePath in Directory.GetFiles(dirPath))
                {
                    if (Path.GetExtension(filePath) == ".txt")
                    {
                        memoGroup.AddItem(filePath);
                    }
                }
                _memoGroups.Add(memoGroup);
            }
        }

        public void UpdateSelectedMemoGroup()
        {
            SelectedMemoGroup.Clear();
            foreach (var filePath in Directory.GetFiles(SelectedMemoGroup.DirPath))
            {
                if (Path.GetExtension(filePath) == ".txt")
                {
                    SelectedMemoGroup.AddItem(filePath);
                }
            }
        }

        public void DeleteMemoGroup(Models.MemoGroup group)
        {
            if (SelectedMemoGroup?.Name == group.Name)
            {
                SelectedMemoGroup = null;
            }
            _memoGroups.Remove(group);
            Directory.Delete(group.DirPath, true);
        }

        public void AppendIndexFile(string memoItemName)
        {
            var path = Path.Combine(SelectedMemoGroup.DirPath, INDEX_FILE_NAME);
            // ファイルが存在しない場合は作成してくれる。
            File.AppendAllText(path, memoItemName + Environment.NewLine);
        }

        public void DeleteItemInIndexFile(Models.MemoItem item)
        {
            var path = Path.Combine(SelectedMemoGroup.DirPath, INDEX_FILE_NAME);
            var lines = new List<string>();
            foreach (var line in File.ReadLines(path))
            {
                var l = line.Trim();
                if (l != item.Name)
                {
                    lines.Add(l);
                }
            }
            var s = string.Join("\n", lines);
            File.WriteAllText(path, s);
        }

        public void DeleteMemoItem(Models.MemoItem item)
        {
            if (SelectedMemoItem?.Name == item.Name)
            {
                SelectedMemoItem = null;
            }
            DeleteItemInIndexFile(item);
            SelectedMemoGroup.DeleteItem(item);
            File.Delete(item.FilePath);
        }

        #region command
        private DelegateCommand _selectMemoItemCommand;
        public DelegateCommand SelectMemoItemCommand
        {
            get
            {
                return _selectMemoItemCommand = _selectMemoItemCommand ?? new DelegateCommand(() =>
                {

                });
            }
        }
        #endregion
    }
}
