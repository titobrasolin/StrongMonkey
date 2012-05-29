using System;
using System.Collections.Generic;

using Gtk;
using Glade;

using StrongMonkey.Core;
using StrongMonkey.Client;
using StrongMonkey.Client.GtkGui.Windows;
//using StrongMonkey.Gui;
//using StrongMonkey.Gui.GtkGui;
//using StrongMonkey.Protocol;
//using StrongMonkey.Protocol.Gui;

// using Anculus.Core;

namespace StrongMonkey.Client.GtkGui.Utilities
{
	public static class StrongMonkeyUtility
	{
		private static MainWindow _mainWindow;
//		private static TransferWindow _transferWindow;
//		private static DebugWindow _debugWindow;
		
		public const string Version = "0.8";
		
		static StrongMonkeyUtility ()
		{
			
		}

		internal static void Initialize (MainWindow mainWindow)
		{
			_mainWindow = mainWindow;
//			_transferWindow = new TransferWindow ();
//			_debugWindow = new DebugWindow ();
		}

		public static MainWindow MainWindow
		{
			get { return _mainWindow; }
		}
		
//		public static TransferWindow TransferWindow
//		{
//			get { return _transferWindow; }
//		}
//		
//		public static DebugWindow DebugWindow
//		{
//			get { return _debugWindow; }
//		}
		
//		public static ISessionWidget<Widget> GetSessionWidget (ISession session)
//		{
//			ThrowUtility.ThrowIfNull ("session", session);
//
//			return _mainWindow.GetSessionWidget (session);
//		}
//
//		public static ISessionWidget<Widget> ActiveSessionWidget
//		{
//			get { return GetSessionWidget (SessionUtility.ActiveSession); }
//		}
		
		public static void Shutdown ()
		{
//			_transferWindow.Destroy ();
//			_debugWindow.Destroy ();
			_mainWindow.Shutdown ();
		}
	}
}