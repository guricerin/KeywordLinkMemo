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
                File.Create(Path.Combine(sample, "index"));
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
                foreach(var filePath in Directory.GetFiles(dirPath))
                {
                    if(Path.GetExtension(filePath) == ".txt")
                    {
                        memoGroup.AddItem(filePath);
                    }
                }
                _memoGroups.Add(memoGroup);
            }
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
