using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Mono.Posix;
using NLog;
using StrongMonkey.Core;
using StrongMonkey.Core.Utilities;

namespace StrongMonkey.Startup
{
	class MainClass
	{
		private static Logger Log = LogManager.GetCurrentClassLogger();
#if DEBUG
		internal const string DefaultAppName = "StrongMonkeyDebug";
#else
		internal const string DefaultAppName = "StrongMonkey";
#endif
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			Console.WriteLine (String.Empty);
			
			bool debug = true;
			string appName = DefaultAppName;
			
			foreach (string arg in args)
			{
				if (arg.StartsWith ("--debug"))
				{
					debug = !arg.EndsWith ("false");
					continue;
				}
				if (arg.StartsWith ("--appName="))
				{
					appName = arg.Substring (10);
					continue;
				}
			}
			
			if (debug)
				Console.WriteLine ("Running in DEBUG mode, appName {0}", appName);
			
			try {
				new MainClass (debug, appName);
			} catch (Exception e) {
				Console.WriteLine ("Fatal exception while running StrongMonkey.");
				
				Exception loop = e;
				while (loop != null) {
					Console.WriteLine ("{0}: {1}", loop.GetType().Name, loop.Message);
					Console.WriteLine (loop.StackTrace);
		
					loop = loop.InnerException;
				}

				throw e;
			}
		}

		public MainClass (bool debug, string appName)
		{
			GLibLogging.Enabled = true;
			
			Assembly exe = typeof (MainClass).Assembly;
			
			string configDir = Path.GetFullPath (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.ApplicationData), appName));
			string lockFile = Path.Combine (configDir, "pid.lock");

			bool instanceRunning = DetectInstances (lockFile, appName);
			if (instanceRunning) {
				Gtk.Application.Init ();

				Gtk.MessageDialog md = new Gtk.MessageDialog (null, Gtk.DialogFlags.Modal, Gtk.MessageType.Warning, Gtk.ButtonsType.Close,
					GettextCatalog.GetString ("An instance of StrongMonkey with configuration profile '{0}' is already running.{1}If you really want to run 2 seperate instances, use the \"--appName=StrongMonkeyXXX\" command line parameter",
						appName, Environment.NewLine));
				md.Run ();
				md.Destroy ();
				md.Dispose ();

				md.Close += delegate(object sender, EventArgs e) {
					Gtk.Application.Quit ();
				};

				Gtk.Application.Run ();
			} else {
				CoreUtility.Initialize (exe, appName, debug);
				WriteInstancePid (lockFile);
				
				AddinUtility.Initialize ();
			}
		}

		private void WriteInstancePid (string lockFile)
		{
			try {
				Process currentProcess = Process.GetCurrentProcess ();
				if (File.Exists (lockFile))
					File.Delete (lockFile);

				File.WriteAllText (lockFile, currentProcess.Id.ToString ());
			} catch (Exception e) {
				Log.ErrorException ("WriteInstancePid", e);
			}
		}

		private bool DetectInstances (string lockFile, string appName)
		{
			if (File.Exists (lockFile)) {
				string content = File.ReadAllText (lockFile);
				int pid = -1;
				if (int.TryParse (content, out pid)) {
					//found process id, make sure the process is still running

					Process other = null;
					try {
						other = Process.GetProcessById (pid);
					} catch {}

					if (other == null)
						return false;

					string otherAppName = GetAppNameFromProcess (other);
					
					if (Environment.OSVersion.Platform == PlatformID.Unix) {
						//case sensitive
						return appName == otherAppName;
					} else {
						//case insensitive
						return String.Compare (appName, otherAppName, true) == 0;
					}
				}
			}
			return false;
		}

		private string GetAppNameFromProcess (Process p)
		{
			if (String.IsNullOrEmpty (p.StartInfo.Arguments))
				return DefaultAppName;

			string[] args = p.StartInfo.Arguments.Split (new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string arg in args) {
				if (arg.StartsWith ("--appName="))
					return arg.Substring (10);
			}

			return DefaultAppName;
		}
	}
}
