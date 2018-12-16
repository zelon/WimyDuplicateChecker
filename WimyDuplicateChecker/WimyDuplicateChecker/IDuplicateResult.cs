
namespace WimyDuplicateChecker
{
	public interface IDuplicateResult
	{
		void OnDuplicated(string firstFileName, string newDuplicatedFileName);
	}
}
