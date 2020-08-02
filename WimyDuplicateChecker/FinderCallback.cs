
namespace WimyDuplicateChecker
{
    public interface IFinderCallback
    {
        void OnFinished();
        void OnFind(string firstFileName, string newDuplicatedFileName);
        void SetStatusBar(string message);
        void AppendOutput(string message);
    }

    public class NullFinderCallback : IFinderCallback
    {
        public void OnFinished() { }
        public void OnFind(string a, string b) { }
        public void SetStatusBar(string message) { }
        public void AppendOutput(string message) { }
    }
}
