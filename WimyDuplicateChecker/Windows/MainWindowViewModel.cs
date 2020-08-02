using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WimyDuplicateChecker
{
    public class DuplicatedResult
    {
        public string Filename1 { get; set; }
        public string Filename2 { get; set; }
        public bool IsSelected { get; set; }
    }


    public class MainWindowViewModel : NotifyBase
    {
        public ObservableCollection<DuplicatedResult> DuplicatedList { get; set; }
        public DuplicatedResult SelectedResult { get; set; }
        public DelegateCommand LaunchDetailResult { get; private set; }
        public DelegateCommand DeleteFilename1 { get; private set; }
        public DelegateCommand DeleteFilename2 { get; private set; }
        public MainWindowViewModel()
        {
            DuplicatedList = new ObservableCollection<DuplicatedResult>();
            LaunchDetailResult = new DelegateCommand(OnLaunchDetailResult);
            DeleteFilename1 = new DelegateCommand(OnDeleteFilename1);
            DeleteFilename2 = new DelegateCommand(OnDeleteFilename2);
        }

        public void AddListViewItem(string firstFileName, string newDuplicatedFileName)
        {
            DuplicatedResult duplicatedResult = new DuplicatedResult() { Filename1 = firstFileName, Filename2 = newDuplicatedFileName, IsSelected = false };
            DuplicatedList.Add(duplicatedResult);
            NotifyPropertyChanged("DuplicatedList");
        }

        void OnLaunchDetailResult(object sender)
        {
            if (SelectedResult == null)
            {
                return;
            }
            DetailWindow detailWindow = new DetailWindow(SelectedResult.Filename1, SelectedResult.Filename2);
            detailWindow.Show();
        }

        void OnDeleteFilename1(object sender)
        {
            List<DuplicatedResult> removedList = new List<DuplicatedResult>();
            foreach (var i in DuplicatedList)
            {
                if (i.IsSelected)
                {
                    removedList.Add(i);
                    System.Diagnostics.Debug.WriteLine("Filename1: {0}", i.Filename1);
                    System.IO.File.Delete(i.Filename1);
                }
            }

            foreach (var removed in removedList)
            {
                DuplicatedList.Remove(removed);
            }
            NotifyPropertyChanged("DuplicatedList");
        }

        void OnDeleteFilename2(object sender)
        {
            System.Collections.Generic.List<DuplicatedResult> removedList = new List<DuplicatedResult>();
            foreach (var i in DuplicatedList)
            {
                if (i.IsSelected)
                {
                    removedList.Add(i);
                    System.Diagnostics.Debug.WriteLine("Filename2: {0}", i.Filename2);
                    System.IO.File.Delete(i.Filename2);
                }
            }

            foreach (var removed in removedList)
            {
                DuplicatedList.Remove(removed);
            }
            NotifyPropertyChanged("DuplicatedList");
        }
    }
}
