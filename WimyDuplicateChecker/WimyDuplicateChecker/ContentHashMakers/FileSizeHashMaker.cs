using System.IO;

namespace WimyDuplicateChecker.ContentHashMakers
{
	public class FileSizeHashMaker : IContentHashMaker
	{
		public string GetHash(string fileName)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			return fileInfo.Length.ToString();
		}
	}
}
