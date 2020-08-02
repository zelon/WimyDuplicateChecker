using System.Collections.Generic;
using System.Threading;

namespace WimyDuplicateChecker
{
    class FinderThread
    {
        private readonly Finder finder_ = null;
        private readonly Thread thread_ = null;

        public FinderThread (IFinderCallback callback, List<string> directories,
                      string searchPattern, long fileSizeLimit)
        {
            finder_ = new Finder(callback, directories, searchPattern, fileSizeLimit);
            thread_ = new Thread(new ThreadStart(ThreadFunc));
            thread_.Priority = ThreadPriority.BelowNormal;
        }

        public void Start()
        {
            thread_.Start();
        }

        private void ThreadFunc()
        {
            finder_.Start();
        }

        public void Stop()
        {
            finder_.Stop();
        }
    }
}
