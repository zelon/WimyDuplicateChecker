using System.Collections.Generic;
using System.IO;

namespace WimyDuplicateChecker
{
	public class FileListCollector
	{
		private readonly List<string> directoryNames_;
		private readonly string searchPattern_;

		public FileListCollector(List<string> directoryNames, string searchPattern)
		{
			directoryNames_ = directoryNames;
			searchPattern_ = searchPattern;
		}

		public IEnumerable<string> GetFileNames()
		{
			foreach (string directoryName in directoryNames_)
			{
				if (Directory.Exists(directoryName) == false)
				{
					continue;
				}
				var fileAndDirs = Directory.EnumerateFileSystemEntries(directoryName, searchPattern_);
				foreach (string filename in fileAndDirs)
				{
					if (Directory.Exists(filename) == false)
					{
						yield return filename;
					}
				}

			}
		}
	}
}
