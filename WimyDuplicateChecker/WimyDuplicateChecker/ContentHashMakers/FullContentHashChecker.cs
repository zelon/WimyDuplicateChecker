using System.Security.Cryptography;
using System.IO;

namespace WimyDuplicateChecker.ContentHashMakers
{
	class FullContentHashChecker : IContentHashMaker
	{
		public string GetHash(string fileName)
		{
			System.Diagnostics.Debug.WriteLine("Calc FullContentHash");
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(fileName))
				{
					return md5.ComputeHash(stream).ToString();
				}
			}
		}
	}
}
