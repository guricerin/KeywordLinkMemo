using Prism.Mvvm;
using Prism.Commands;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Regions;

namespace KeywordLinkMemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region property
        public IRegionManager RegionManager { get; }
        public DelegateCommand<string> NavigateToShowmemoItemPageCommand { get; }

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

        public MainWindowViewModel(IRegionManager regionManager)
        {
            RegionManager = regionManager;
            NavigateToShowmemoItemPageCommand = new DelegateCommand<string>((string pageName) =>
            {
                RegionManager.RequestNavigate("MemoItemRegion", pageName);
            });

            if (!Directory.Exists(MemosPath))
            {
                Directory.CreateDirectory(MemosPath);
                var sample = Path.Combine(MemosPath, "最初のグループ");
                Directory.CreateDirectory(sample);
                File.Create(Path.Combine(sample, "サンプルめも.txt"));
            }

            UpdateMemoGroups();
            _selectedMemoGroup = _memoGroups[0];
        }

        public void NavigateToShowMemoItemPage()
        {
            if (SelectedMemoItem == null) return;

            var param = new NavigationParameters();
            param.Add("MemoGroup", SelectedMemoGroup);
            param.Add("MemoItem", SelectedMemoItem);
            Action<Models.MemoItem> f = (x) =>
            {
                SelectedMemoItem = x;
            };
            param.Add("Hyperlink_RequestNavigate", f);
            RegionManager.RequestNavigate("MemoItemRegion", nameof(Views.ShowMemoItemPage), param);
        }

        public void NavigateToBlankPage()
        {
            RegionManager.RequestNavigate("MemoItemRegion", nameof(Views.BlankPage));
        }

        public void NavigateToEditMemoItemPage()
        {
            if (SelectedMemoItem == null) return;

            var param = new NavigationParameters();
            param.Add("MemoItem", SelectedMemoItem);
            RegionManager.RequestNavigate("MemoItemRegion", nameof(Views.EditMemoItemPage), param);
        }

        public void UpdateMemoGroups()
        {
            _memoGroups.Clear();

            // いまのところ、グループ（ディレクトリ）は1階層目にしか存在しない想定。
            foreach (var dirPath in Directory.GetDirectories(MemosPath))
            {
                _memoGroups.Add(new Models.MemoGroup(dirPath));
            }
        }

        public void UpdateSelectedMemoGroup()
        {
            SelectedMemoGroup.Reload();
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

        public void DeleteMemoItem(Models.MemoItem item)
        {
            if (SelectedMemoItem?.Name == item.Name)
            {
                SelectedMemoItem = null;
            }
            SelectedMemoGroup.DeleteItem(item);
            File.Delete(item.FilePath);
        }
    }
}
