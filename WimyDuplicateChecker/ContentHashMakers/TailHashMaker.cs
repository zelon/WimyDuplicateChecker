using System.IO;

namespace WimyDuplicateChecker.ContentHashMakers
{
	class TailHashMaker : IContentHashMaker
	{
		private static readonly long reverseLength = 10;

		public string GetHash(string fileName)
		{
			using (var streamReader = new StreamReader(fileName))
			{
				long testPosition = streamReader.BaseStream.Length - reverseLength;
				if (testPosition < 0)
				{
					testPosition = 0;
				}
				streamReader.BaseStream.Position = testPosition;
				return streamReader.ReadLine();
			}
		}
	}
}
