using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

//using Anculus.Core;
using Mono.Addins;
using Mono.Addins.Setup;

using StrongMonkey.Core.Extensions;
using StrongMonkey.Core.Interfaces;

using NLog;

namespace StrongMonkey.Core.Utilities
{
	public static class AddinUtility
	{
		private static Logger Log = LogManager.GetCurrentClassLogger();

		static bool _isInitialized;
		static SetupService _setupService;
		
		public static SetupService SetupService
		{
			get { return _setupService; }
		}
		
		public static void Initialize ()
		{
			if (!_isInitialized)
			{
				//BaseUtility.CreateDirectoryIfNeeded(CoreUtility.AddinConfigurationDirectory);
				
				//File.WriteAllText(Path.Combine(CoreUtility.AddinConfigurationDirectory, "main.addins"),
				//                  "<Addins><Directory>" + CoreUtility.ApplicationDirectory + "</Directory></Addins>");
				
				Log.Info("Initializing add-in manager...");

				//Environment.SetEnvironmentVariable ("MONO_ADDINS_REGISTRY", CoreUtility.ConfigurationDirectory);
				AddinManager.Initialize(CoreUtility.ConfigurationDirectory);
				AddinManager.AddinLoaded += AddinLoaded;
				
				Log.Info("Updating the add-in registry...");

				AddinManager.Registry.Update (CoreUtility.Debug ? new ConsoleProgressStatus (true) : null);
				
				_setupService = new SetupService (AddinManager.Registry);
				
				_isInitialized = true;
				bool executed = false;
				
				foreach (EntryPointExtension node in AddinManager.GetExtensionNodes ("/StrongMonkey/EntryPoints"))
				{
					Type type = node.Addin.GetType (node.Class);
					
					if (type != null)
					{
						MethodInfo methodInfo = type.GetMethod (node.Method);
						
						if (methodInfo != null)
							node.Callback = Delegate.CreateDelegate (typeof (EntryPointCallback), methodInfo) as EntryPointCallback;
					}
					
					if (node.Callback != null)
					{
						executed = true;
						
						// There is a entry point callback function, execute.
						node.Execute();
					}
				}
				
				if(!executed)
					Log.Error ("No entry point addin found! Application will not run.");
			}
			else
			{
				Log.Warn ("Initialize can only be called once.");
			}
		}
		
		public static void Shutdown ()
		{
			ThrowIfNotInitialized();
			
			_isInitialized = false;
			
			AddinManager.Shutdown();
		}
		
		static void AddinLoaded (object sender, AddinEventArgs args)
		{
			Addin addin = AddinManager.Registry.GetAddin (args.AddinId);
			
			if (!(addin.AddinFile.StartsWith (Path.GetDirectoryName (Assembly.GetEntryAssembly ().Location)) ||
			      addin.AddinFile.StartsWith (CoreUtility.ConfigurationDirectory)))
			{
				Log.Debug (string.Format("Addin not from current install: {0}", addin.AddinFile));
			}
		}
		
		private static void ThrowIfNotInitialized ()
		{
			if (!_isInitialized)
				throw new ApplicationException ("You must call AddinUtility.Initialize before using any other method!");
		}
		
		public static IEnumerable<ContextExtension> GetContextExtensionObjects(string path, IExtensionContext context, Type type)
		{
			foreach(ContextExtension node in AddinManager.GetExtensionObjects(path, type))
			{
				node.Context = context;
				yield return node;
			}
		}
		
		public static IEnumerable<ContextExtension> GetContextExtensionNodes(string path, IExtensionContext context, Type type)
		{
			foreach(ContextExtension node in AddinManager.GetExtensionNodes (path, type))
			{
				node.Context = context;
				yield return node;
			}
		}
		
		public static IEnumerable<ContextExtension> GetContextExtensionNodes(string path, IExtensionContext context)
		{
			AddinManager.Registry.Update(null);
			
			foreach (ContextExtension node in AddinManager.GetExtensionNodes(path))
			{
				node.Context = context;
				yield return node;
			}
		}
		
		public static IEnumerable<ExtensionNode> GetExtensionNodes(string path)
		{
			foreach(ExtensionNode node in AddinManager.GetExtensionNodes(path))
				yield return node;
		}
		
		public static T GetContextExtensionNode<T>(string path, IExtensionContext context, object comparison, bool retfirst) where T : ContextExtension
		{
			foreach (T node in GetContextExtensionNodes(path, context, typeof(T)))
				if (node.Equals(comparison))
				{
					node.Context = context;
					return node;
				}
			
			if (retfirst)
			{
				foreach (T node in GetContextExtensionNodes(path, context, typeof(T)))
				{
					Log.Warn(string.Format("Unable to find {0} '{1}', returning first found", typeof(T).Name, comparison));
					node.Context = context;
					return node;
				}
			}
			
			Log.Warn(string.Format("Unable to find {0}", typeof(T).Name));
			return null;
		}
		
		public static T GetExtensionNode<T>(string path, object comparison, bool retfirst) where T : ExtensionNode
		{
			foreach (T node in GetExtensionNodes(path))
			{
				if (node.Equals(comparison))
					return node;
			}
			
			if (retfirst)
			{
				foreach (T node in GetExtensionNodes(path))
				{
					Log.Warn(string.Format("Unable to find {0} '{1}', returning first found", typeof(T).Name, comparison));
					return node;
				}
			}
			
			Log.Warn(string.Format("Unable to find {0}", typeof(T).Name));
			return null;
		}
	}
}
