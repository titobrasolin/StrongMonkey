using System;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Globalization;

// using Anculus.Core;

namespace StrongMonkey.Core.Utilities
{
	public static class CoreUtility
	{
		private static bool _isInitialized;
		
		private static string _applicationDirectory;
		private static string _applicationName;
	//	private static string _applicationThread;
		
		private static string _configDirectory;
		private static string _dataDirectory;
		private static string _localeDirectory;
		private static string _translationDomain;
		
		private static bool _debug;
		
		public static string ApplicationName
		{
			get { return _applicationName; }
		}
		
		public static string ApplicationDirectory
		{
			get { return _applicationDirectory; }
		}
		
		public static bool Debug
		{
			get { return _debug; }
		}
		
		public static string DataDirectory
		{
			get { return _dataDirectory; }
			set
			{
				ThrowIfNotInitialized ();
				ThrowUtility.ThrowIfEmpty ("DataDirectory", value);

				_dataDirectory = Path.GetFullPath (value);
			}
		}
		
		public static string ConfigurationDirectory
		{
			get { return _configDirectory; }
			set
			{
				ThrowIfNotInitialized ();
				
				ThrowUtility.ThrowIfEmpty ("ConfigurationDirectory", value);
				
				_configDirectory = Path.GetFullPath (value);
			}
		}
		
		public static string AddinConfigurationDirectory
		{
			get { return Path.Combine(_configDirectory, "addins"); }
		}
		
		public static string LocaleDirectory
		{
			get { return _localeDirectory; }
		}
		
		public static string TranslationDomain
		{
			get { return _translationDomain; }
		}
		
		public static void Initialize (string applicationDirectory, string applicationName, bool debug)
		{
			if (!_isInitialized)
			{
				_debug = debug;
				
				_applicationName = applicationName;
				_translationDomain = applicationName.ToLower ();
				
				if (_configDirectory == null)
				{
					ThrowUtility.ThrowIfInvalidDirectoryName (_applicationName);
					_configDirectory = Path.GetFullPath (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData), _applicationName));
				}
				
				if (!Directory.Exists (_configDirectory))
					Directory.CreateDirectory (_configDirectory);
				
				//Log.AddBackend (new GalaxiumLogBackend ());
				//FIXME: the XmlLogBackend throws access violation exceptions on some occasions, so commented for now
				//Log.AddBackend (new XmlLogBackend (Path.Combine (ConfigurationDirectory, "log.xml"), false));
				
//				if (!_debug)
//					Log.MinimalLogLevel = LogLevel.Warning;
				
				ThrowUtility.ThrowIfEmpty ("applicationDirectory", applicationDirectory);
				ThrowUtility.ThrowIfEmpty ("applicationName", applicationName);
				
				System.Threading.Thread.CurrentThread.Name = applicationName;
				
				if (!FileUtility.IsValidDirectoryName (applicationName))
					throw new ArgumentException ("The application name must be a valid directory name.", "applicationName");
				
				if (!Directory.Exists (applicationDirectory))
					throw new FileNotFoundException ("applicationDirectory must be an existing directory");
				
				_applicationDirectory = Path.GetFullPath (applicationDirectory);
				
//				Log.Info ("Using application directory: "+_applicationDirectory);
				
				LoadConfigSection ();
				
				if (string.IsNullOrEmpty (_dataDirectory) || (!Directory.Exists (_dataDirectory)))
					_dataDirectory = Path.GetFullPath (Path.Combine (_applicationDirectory, "Data"));
								
				if (string.IsNullOrEmpty (_localeDirectory))
					_localeDirectory = Path.Combine (_dataDirectory, "Locale");
				
//				Log.Debug ("Data Directory: {0}", _dataDirectory);
				
				_isInitialized = true;
			}
			else
			{
//				Log.Warn ("Initialize can only be called once.");
			}
		}
		
		public static void Initialize (Assembly startAssembly, string applicationName, bool debug)
		{
			ThrowUtility.ThrowIfNull ("startAssembly", startAssembly);
			
			string applicationDirectory = Path.GetDirectoryName (startAssembly.Location);
			
			Initialize (applicationDirectory, applicationName, debug);
		}
		
		public static string GetApplicationSubDirectory (string subdirectoryName)
		{
			ThrowIfNotInitialized ();
			
			ThrowUtility.ThrowIfEmpty ("subdirectoryName", subdirectoryName);
			
			return Path.GetFullPath (Path.Combine (_applicationDirectory, subdirectoryName));
		}
		
		public static void SetRelativeDataDirectory (string directory)
		{
			ThrowIfNotInitialized ();
			
			ThrowUtility.ThrowIfEmpty ("directory", directory);
			
			_dataDirectory = Path.GetFullPath (Path.Combine (_applicationDirectory, directory));
		}
		
		public static string GetDataSubDirectory (string subdirectoryName)
		{
			ThrowIfNotInitialized ();
			
			ThrowUtility.ThrowIfInvalidDirectoryName (subdirectoryName);
			
			return Path.GetFullPath (Path.Combine (_dataDirectory, subdirectoryName));
		}
		
		public static string GetDataDirectoryFile (string filename)
		{
			ThrowIfNotInitialized ();
			
			ThrowUtility.ThrowIfInvalidFileName (filename);
			
			return Path.GetFullPath (Path.Combine (_dataDirectory, filename));
		}
		
		public static string GetConfigurationSubDirectory (string subdirectoryName)
		{
			ThrowIfNotInitialized ();
			
			ThrowUtility.ThrowIfInvalidDirectoryName (subdirectoryName);
			
			return Path.GetFullPath (Path.Combine (_configDirectory, subdirectoryName));
		}
		
		public static string GetConfigurationDirectoryFile (string filename)
		{
			ThrowIfNotInitialized ();
			
			ThrowUtility.ThrowIfInvalidFileName (filename);
			
			return Path.GetFullPath (Path.Combine (_configDirectory, filename));
		}
		
		private static void ThrowIfNotInitialized ()
		{
			if (!_isInitialized)
				throw new ApplicationException ("You must call CoreUtility.Initialize before using any other method!");
		}
		
		private static void LoadConfigSection ()
		{
			if (ConfigurationManager.AppSettings["LocaleDirectory"] != null)
				_localeDirectory = ConfigurationManager.AppSettings["LocaleDirectory"].ToString ();
			
			if (ConfigurationManager.AppSettings["DataDirectory"] != null)
				_dataDirectory = ConfigurationManager.AppSettings["DataDirectory"].ToString ();
		}
	}
}