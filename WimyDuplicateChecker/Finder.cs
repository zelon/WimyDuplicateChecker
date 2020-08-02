using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace WimyDuplicateChecker
{
	class Finder : IDuplicateResult
	{
		private MainJob mainJob_;
		private IFinderCallback callback_ = null;
		private bool go_on_ = false;
		private bool found_duplicate_ = false;
		private readonly List<string> directories_;
		private readonly string searchPattern_;
		private readonly long file_size_bytes_limit_;

		public Finder(IFinderCallback callback, List<string> directories,
					  string searchPattern, long fileSizeLimit)
		{
			mainJob_ = new MainJob(this);
			go_on_ = true;

			callback_ = callback;
			directories_ = directories;
			searchPattern_ = searchPattern;
			file_size_bytes_limit_ = fileSizeLimit;
		}

		public void Stop()
		{
			callback_.SetStatusBar("Stopped");
			callback_ = new NullFinderCallback();
			go_on_ = false;
		}

		private long GetFileSize(string fileName)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			return fileInfo.Length;
		}

		public void Start()
		{
			found_duplicate_ = false;
			callback_.SetStatusBar("Checking...");

			FileListCollector fileListCollector = new FileListCollector(directories_, searchPattern_);
			foreach (string fileName in fileListCollector.GetFileNames())
			{
				if (go_on_ == false)
				{
					break;
				}
				if (GetFileSize(fileName) < file_size_bytes_limit_)
				{
					continue;
				}
				mainJob_.Add(fileName);
			}
			if (go_on_)
			{
				callback_.SetStatusBar("All done");
				callback_.AppendOutput("All done");

				if (found_duplicate_ == false)
				{
					callback_.AppendOutput("Cannot find any duplicated files");
				}
			}
			callback_.OnFinished();
		}

		public void OnDuplicated(string firstFileName, string newDuplicatedFileName)
		{
			found_duplicate_ = true;
			System.Diagnostics.Debug.WriteLine("{0} is duplicated to {1}", newDuplicatedFileName, firstFileName);
			callback_.OnFind(firstFileName, newDuplicatedFileName);
		}
	}
}
