using System.Collections.Generic;

namespace WimyDuplicateChecker
{
	public class Step
	{
		private readonly IDuplicateResult resultCallback_;
		private FileHavingHash startFileHavingHash_;
		private readonly Dictionary<string, Step> dictionary_ = new Dictionary<string, Step>();
		private readonly List<FileHavingHash> sameFileHavingHashs_ = new List<FileHavingHash>();

		public Step(IDuplicateResult resultCallback)
		{
			resultCallback_ = resultCallback;
		}

		public void Add(FileHavingHash fileHavingHash)
		{
			if (dictionary_.Count == 0)
			{
				if (startFileHavingHash_ == null)
				{
					startFileHavingHash_ = fileHavingHash;
					return;
				}
				FileHavingHash startFileHavingHash = startFileHavingHash_;
				string startHash = startFileHavingHash.PopHashMakerAndGetHash();
				if (startHash == null)
				{
					resultCallback_.OnDuplicated(startFileHavingHash_.Filename, fileHavingHash.Filename);
					return;
				}
				Step nextStep = new Step(resultCallback_);
				nextStep.Add(startFileHavingHash);
				dictionary_.Add(startHash, nextStep);
			}

			string hash = fileHavingHash.PopHashMakerAndGetHash();
			if (dictionary_.ContainsKey(hash) == false)
			{
				Step nextStep = new Step(resultCallback_);
				nextStep.Add(fileHavingHash);
				dictionary_.Add(hash, nextStep);
			}
			else
			{
				dictionary_[hash].Add(fileHavingHash);
			}
		}
	}
}
