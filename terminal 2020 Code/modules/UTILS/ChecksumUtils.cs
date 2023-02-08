using System;
using System.Collections.Generic;
using System.Text;

namespace sorec_gamma.modules.UTILS
{
	public enum HashingAlgoTypes
	{
		MD5,
		SHA1,
		SHA256,
		SHA384,
		SHA512
	}

	public class ChecksumUtils
    {
		public static string GetChecksum(HashingAlgoTypes hashingAlgoType, string filename)
		{
			string result = null;
			try
            {
				using (var hasher = System.Security.Cryptography.HashAlgorithm.Create(hashingAlgoType.ToString()))
				{
					using (var stream = System.IO.File.OpenRead(filename))
					{
						var hash = hasher.ComputeHash(stream);
						result = BitConverter.ToString(hash).Replace("-", "");
					}
				}
			}
			catch (Exception ex)
            {
				ApplicationContext.Logger.Error(string.Format("Checksum utils : ", ex.Message, ex.StackTrace));
            }
			return result;
		}

		public static string GetMD5Checksum(string filename)
		{
			using (var md5 = System.Security.Cryptography.MD5.Create())
			{
				using (var stream = System.IO.File.OpenRead(filename))
				{
					var hash = md5.ComputeHash(stream);
					return BitConverter.ToString(hash).Replace("-", "");
				}
			}
		}
	}
}
