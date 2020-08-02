using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

namespace WimyDuplicateChecker
{
    public partial class MainWindow : Window, IFinderCallback
    {
        private delegate void TextChanger(string msg);
        private const string kTextStart = "Start";
        private const string kTextStop = "Stop";
        private FinderThread finderThread_ = null;

        public MainWindow()
        {
            InitializeComponent();

            directories_.AllowDrop = true;

            DataContext = new MainWindowViewModel();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        public void OnFind(string firstFileName, string newDuplicatedFileName)
        {
            if (log_.Dispatcher.CheckAccess())
            {
                string msg = string.Format("a:{0},b:{1}", firstFileName, newDuplicatedFileName);
                AppendOutput(msg);
                System.Diagnostics.Debug.WriteLine(msg);

                MainWindowViewModel viewModel = (MainWindowViewModel)(DataContext);
                viewModel.AddListViewItem(firstFileName, newDuplicatedFileName);
            }
            else
            {
                log_.Dispatcher.Invoke(new Action(() => { OnFind(firstFileName, newDuplicatedFileName); }));
            }
        }

        private void OnBrowse(object sender, RoutedEventArgs e)
        {
            //directories_.Items.Add("sdfsdf");
        }

        private void OnAddDirectory(string directory)
        {
            directories_.Items.Add(directory);
        }

        private void OnDropDirectory(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (path != null)
                {
                    OnAddDirectory(path[0]);
                }
            }
            else
            {
                MessageBox.Show("Cannot drop it");
            }
        }

        private void OnStart(object sender, RoutedEventArgs e)
        {
            if (finderThread_ != null)
            {
                finderThread_.Stop();
                return;
            }
            log_.Text = "";
            SetControlButtonText(kTextStop);

            List<string> directories = new List<string>();
            foreach (var item in directories_.Items)
            {
                directories.Add(item.ToString());
            }
            int file_size_bytes_limit = int.Parse(filesize_.Text) * 1024 * 1024;
            finderThread_ = new FinderThread(this, directories, search_pattern_.Text, file_size_bytes_limit);
            finderThread_.Start();
        }

        private void Stop()
        {
            if (finderThread_ == null)
            {
                return;
            }
            finderThread_.Stop();
            finderThread_ = null;
            SetControlButtonText(kTextStart);
            SetStatusBar("Stopped");
        }

        public void OnFinished()
        {
            SetControlButtonText(kTextStart);
            finderThread_ = null;
        }

        public void SetStatusBar(string msg)
        {
            if (status_bar_.Parent.Dispatcher.CheckAccess())
            {
                status_bar_.Content = msg;
            }
            else
            {
                status_bar_.Parent.Dispatcher.Invoke(new TextChanger(this.SetStatusBar), new Object[] { msg });
            }
        }

        public void AppendOutput(string msg)
        {
            if (log_.Dispatcher.CheckAccess())
            {
                const int kMaxOutputLength = 2048 * 10;
                if (log_.Text.Length > kMaxOutputLength)
                {
                    log_.Text = log_.Text.Substring(kMaxOutputLength / 2);
                }
                log_.AppendText(msg + "\r\n");
            }
            else
            {
                log_.Dispatcher.Invoke(new TextChanger(this.AppendOutput), new Object[] { msg });
            }
        }

        private void SetControlButtonText(string text)
        {
            if (start_button_.Dispatcher.CheckAccess())
            {
                start_button_.Content = text;
            }
            else
            {
                start_button_.Dispatcher.Invoke(new TextChanger(this.SetControlButtonText), new Object[] { text });
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Stop();
        }
    }
}
