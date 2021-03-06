﻿using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace WimyDuplicateChecker.ContentHashMakers
{
	class FullContentHashChecker : IContentHashMaker
	{
		public string GetHash(string fileName)
		{
            var cacheManager = HashCacheRepository.GetInstance();
            string hash = cacheManager.FindHash(fileName);
            if (hash != null)
            {
                return hash;
            }
			System.Diagnostics.Debug.WriteLine("Calc FullContentHash");
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(fileName))
				{
					byte[] bytes = md5.ComputeHash(stream);
                    string calculatedHash = System.BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
                    Debug.Assert(calculatedHash.Length == 32);
                    cacheManager.AddHash(fileName, calculatedHash);
                    return calculatedHash;
				}
			}
		}
	}
}
