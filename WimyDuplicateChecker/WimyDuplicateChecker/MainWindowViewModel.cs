using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WimyDuplicateChecker
{
    public class DuplicatedResult
    {
        public string Filename1 { get; set; }
        public string Filename2 { get; set; }
    }


    public class MainWindowViewModel : NotifyBase
    {
        public ObservableCollection<DuplicatedResult> DuplicatedList { get; set; }
        public DuplicatedResult SelectedResult { get; set; }
        public DelegateCommand LaunchDetailResult { get; private set; }
        public MainWindowViewModel()
        {
            DuplicatedList = new ObservableCollection<DuplicatedResult>();
            LaunchDetailResult = new DelegateCommand(OnLaunchDetailResult);
        }

        public void AddListViewItem(string firstFileName, string newDuplicatedFileName)
        {
            DuplicatedResult duplicatedResult = new DuplicatedResult() { Filename1 = firstFileName, Filename2 = newDuplicatedFileName };
            DuplicatedList.Add(duplicatedResult);
            NotifyPropertyChanged("DuplicatedList");
        }

        void OnLaunchDetailResult(object sender)
        {
            System.Diagnostics.Debug.Write("here!!!");
        }
    }
}
