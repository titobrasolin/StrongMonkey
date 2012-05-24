using System;
using NLog;
// using Anculus.Core;

namespace StrongMonkey.Startup
{
	public static class GLibLogging
	{
		private static Logger Log = LogManager.GetCurrentClassLogger();
		static bool enabled;
		
		static uint gtkLogHandle;
		static uint gdkLogHandle;
		static uint glibLogHandle;
		
		static Delegate exceptionManagerHook;
		
		public static bool Enabled
		{
			get { return enabled; }
			set
			{
				if (enabled == value)
					return;
				
				enabled = value;
				if (value) {
					HookExceptionManager ();
					gtkLogHandle  = GLib.Log.SetLogHandler ("Gtk",  GLib.LogLevelFlags.All, LogFunc);
					gdkLogHandle  = GLib.Log.SetLogHandler ("Gdk",  GLib.LogLevelFlags.All, LogFunc);
					glibLogHandle = GLib.Log.SetLogHandler ("GLib", GLib.LogLevelFlags.All, LogFunc);
				} else {
					UnhookExceptionManager ();
					GLib.Log.RemoveLogHandler ("Gtk", gtkLogHandle);
					GLib.Log.RemoveLogHandler ("Gdk", gdkLogHandle);
					GLib.Log.RemoveLogHandler ("GLib", glibLogHandle);
				}
			}
		}
		
		static void LogFunc (string logDomain, GLib.LogLevelFlags logLevel, string message)
		{
			System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace (2, true);
			string msg = string.Format ("{0}-{1}: {2}\nStack trace: {3}{4}", 
				logDomain, logLevel, message, Environment.NewLine, trace.ToString ());
			
			switch (logLevel) {
			case GLib.LogLevelFlags.Debug:
				Log.Debug (msg);
				break;
			case GLib.LogLevelFlags.Info:
				Log.Info (msg);
				break;
			case GLib.LogLevelFlags.Warning:
				Log.Warn (msg);
				break;
			case GLib.LogLevelFlags.Error:
				Log.Error (msg);
				break;
			case GLib.LogLevelFlags.Critical:
			default:
				Log.Fatal (msg);
				break;
			}	
		}
		
		static void HookExceptionManager ()
		{
			if (exceptionManagerHook != null)
				return;
			
			Type t = typeof(GLib.Object).Assembly.GetType ("GLib.ExceptionManager");
			if (t == null)
				return;
			
			System.Reflection.EventInfo ev = t.GetEvent ("UnhandledException");
			Type delType = typeof(GLib.Object).Assembly.GetType ("GLib.UnhandledExceptionHandler");
			System.Reflection.MethodInfo met = typeof (GLibLogging).GetMethod ("OnUnhandledException", 
				System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
			exceptionManagerHook = Delegate.CreateDelegate (delType, met);
			ev.AddEventHandler (null, exceptionManagerHook);
		}
		
		static void UnhookExceptionManager ()
		{
			if (exceptionManagerHook == null)
				return;
			
			Type t = typeof(GLib.Object).Assembly.GetType ("GLib.ExceptionManager");
			System.Reflection.EventInfo ev = t.GetEvent ("UnhandledException");
			ev.RemoveEventHandler (null, exceptionManagerHook);
			exceptionManagerHook = null;
		}
		
		internal static void OnUnhandledException (UnhandledExceptionEventArgs args)
		{
			Log.ErrorException ("Unhandled exception in GLib event handler.", (Exception) args.ExceptionObject);
		}
	}
}