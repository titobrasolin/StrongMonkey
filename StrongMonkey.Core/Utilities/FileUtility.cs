using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

using NLog;
// using Anculus.Core;

namespace StrongMonkey.Core.Utilities
{
	public static class FileUtility
	{
		private static Logger Log = LogManager.GetCurrentClassLogger();

		public static bool IsValidFileName (string filename)
		{
			if (filename == null || filename.Length == 0)
				return false;
			
			if (filename.Trim (' ').Length == 0)
				return false;
			
			//see: http://www.answers.com/topic/comparison-of-file-systems
			int length = Encoding.UTF8.GetByteCount (filename);
			if (length > 255)
				return false;

			if (filename.IndexOfAny (Path.GetInvalidFileNameChars ()) >= 0)
				return false;
			
			return true;
		}
		
		public static bool IsValidDirectoryName (string directory)
		{
			if (directory == null || directory.Length == 0)
				return false;
			
			if (directory.Trim (' ').Length == 0)
				return false;
			
			//see: http://www.answers.com/topic/comparison-of-file-systems
			int length = Encoding.UTF8.GetByteCount (directory);
			if (length > 255)
				return false;
			
			if (directory.IndexOfAny (Path.GetInvalidPathChars ()) >= 0)
				return false;
			
			return true;
		}
		
		public static string GetRandomFileName ()
		{
			StringBuilder builder = new StringBuilder ();
			Random random = new Random (DateTime.Now.Millisecond);
			char ch;

			for (int i = 0; i < 10; i++)
			{
				ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
				builder.Append(ch);
			}
			
			return builder.ToString();
		}
		
		public static string GetRandomFileName (string directory)
		{
			string filename = GetRandomFileName ();
			
			while (File.Exists(Path.Combine(directory, filename)))
				filename = GetRandomFileName ();
			
			return Path.Combine(directory, filename);
		}
		
		public static string GetFileChecksum (string filename)
		{
			if (!File.Exists (filename))
				return null;
			
			MD5 md = MD5.Create ();
			using (FileStream stream = File.OpenRead (filename))
			{
				byte[] hash = md.ComputeHash (stream);
				string ret = Convert.ToBase64String (hash);
				return ret.Replace ('/', '-');
			}
		}
		
		static bool ClearFiles (string dir)
		{
			foreach (string file in Directory.GetFiles (dir))
			{
				try
				{
					File.Delete (file);
				}
				catch (Exception ex)
				{
					Log.WarnException(string.Format("Unable to delete file {0}", file), ex);
					return false;
				}
			}
			
			return true;
		}
		
		public static bool ClearDirectory (string dir)
		{
			if (!ClearFiles (dir))
				return false;
			
			foreach (string subdir in Directory.GetDirectories (dir))
			{
				if (!DeleteDirectory (subdir))
					return false;
			}
			
			return true;
		}
		
		public static bool DeleteDirectory (string dir)
		{
			if (!ClearDirectory (dir))
				return false;
			
			try
			{
				Directory.Delete (dir);
			}
			catch (Exception ex)
			{
				Log.WarnException (string.Format("Unable to delete directory {0}", dir), ex);
				return false;
			}
			
			return true;
		}
	}
}