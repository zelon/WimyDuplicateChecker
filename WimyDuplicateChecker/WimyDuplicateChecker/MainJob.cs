using System.Collections.Generic;

namespace WimyDuplicateChecker
{
	class FileHavingHashCreator
	{
		private Queue<IContentHashMaker> hashMakers_ = new Queue<IContentHashMaker>();

		public FileHavingHashCreator()
		{
			hashMakers_.Enqueue(new ContentHashMakers.FileSizeHashMaker());
			hashMakers_.Enqueue(new ContentHashMakers.HeaderHashMaker());
			hashMakers_.Enqueue(new ContentHashMakers.TailHashMaker());
			hashMakers_.Enqueue(new ContentHashMakers.FullContentHashChecker());
		}

		public FileHavingHash Create(string filename)
		{
			FileHavingHash result = new FileHavingHash();
			result.Filename = filename;
			foreach (var hashMaker in hashMakers_)
			{
				result.AddHashMaker(hashMaker);
			}
			return result;
		}
	}

	public class FileHavingHash
	{
		private Queue<IContentHashMaker> hashMakers_ = new Queue<IContentHashMaker>();

		public void AddHashMaker(IContentHashMaker hashMaker)
		{
			hashMakers_.Enqueue(hashMaker);
		}

		public string PopHashMakerAndGetHash()
		{
			if (hashMakers_.Count == 0)
			{
				return null;
			}
			IContentHashMaker hashMaker = hashMakers_.Dequeue();
			return hashMaker.GetHash(Filename);
		}

		public string Filename { get; set; }
	}

	public class MainJob
	{
		private Step step_;
		private FileHavingHashCreator havingHashCreator_ = new FileHavingHashCreator();

		public MainJob(IDuplicateResult resultCallback)
		{
			step_ = new Step(resultCallback);
		}

		public void Add(string fileName)
		{
			step_.Add(havingHashCreator_.Create(fileName));
		}
	}
}
