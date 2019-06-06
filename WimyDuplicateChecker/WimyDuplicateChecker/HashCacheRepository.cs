using System.Collections.Concurrent;
using System.IO;

namespace WimyDuplicateChecker
{
    class HashCacheRepository
    {
        private static HashCacheRepository instance_ = null;
        private static readonly string kCacheFilename = "wimy_duplicate_checker.cache";
        private static readonly char kHashSplitter = '|';
        private readonly ConcurrentDictionary<string, string> md5Map_;
        private TextWriter textWriter_;

        public static HashCacheRepository GetInstance()
        {
            if (instance_ == null)
            {
                instance_ = new HashCacheRepository();
            }
            return instance_;
        }

        private HashCacheRepository()
        {
            md5Map_ = new ConcurrentDictionary<string, string>();
            if (File.Exists(kCacheFilename))
            {
                using (var textReader = File.OpenText(kCacheFilename))
                {
                    while (true)
                    {
                        string line = textReader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        if (line.Length < 3)
                        {
                            break;
                        }
                        string[] result = line.Split('|');
                        System.Diagnostics.Debug.Assert(result.Length == 2);
                        string filename = result[0];
                        string hash = result[1];
                        md5Map_.TryAdd(filename, hash);
                    }
                }
            }
            textWriter_ = new StreamWriter(kCacheFilename, append: true);
        }

        public string FindHash(string fileName)
        {
            if (md5Map_.TryGetValue(fileName, out string hash))
            {
                return hash;
            }
            return null;
        }

        public void AddHash(string fileName, string hash)
        {
            if (md5Map_.TryAdd(fileName, hash) == false)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            string line = string.Format("{0}{1}{2}", fileName, kHashSplitter, hash);
            textWriter_.WriteLine(line);
            textWriter_.Flush();
        }

        public void ClearAllHash()
        {
            md5Map_.Clear();
            textWriter_.Close();
            textWriter_ = new StreamWriter(kCacheFilename);
            textWriter_.Flush();
        }
    }
}
