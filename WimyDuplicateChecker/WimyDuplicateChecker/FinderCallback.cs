
namespace WimyDuplicateChecker
{
    interface FinderCallback
    {
        void OnFinished();
        void OnFind(string firstFileName, string newDuplicatedFileName);
        void SetStatusBar(string message);
        void AppendOutput(string message);
    }
}
