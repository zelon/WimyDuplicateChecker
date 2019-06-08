using System.IO;

namespace WimyDuplicateChecker.ContentHashMakers
{
	class HeaderHashMaker : IContentHashMaker
	{
		public string GetHash(string fileName)
		{
			using (var streamReader = new StreamReader(fileName))
			{
				return streamReader.ReadLine();
			}
		}
	}
}
