using System;
using Gtk;

using StrongMonkey.Client.GtkGui.Utilities;

namespace StrongMonkey.Client.GtkGui.Windows
{
	public partial class MainWindow : Gtk.Window
	{
		public event EventHandler VisibleChanged;

		public MainWindow () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		public static void ExecuteEntryPoint (params object[] args)
		{
			Application.Init ();
			
//			ThreadUtility.Dispatcher = new GtkThreadDispatcher ();
//			
//			GtkUtility.Initialize ();
//			ClientUtility.Initialize();
//			EmoticonUtility.Initialize();
//			SoundSetUtility.Initialize();
//			VideoUtility.Initialize ();
//			
			StrongMonkeyUtility.Initialize (new MainWindow ());
//			
//			WindowUtility<Widget>.Initialize(new GtkWindowUtility());
//			
//			GtkActivityUtility.Initialize();
//			
//			GLib.ExceptionManager.UnhandledException += delegate (GLib.UnhandledExceptionArgs ea) {
//				Exception ex = ea.ExceptionObject as Exception;
//				
//				Log.Fatal (ex, "Uncaught GLib exception.");
//			};
			
			Application.Run ();
		}

		internal void Shutdown ()
		{
			SaveWindowLocation ();
//			GalaxiumUtility.TransferWindow.SaveWindowLocation ();
			
//			WindowUtility<Widget>.CloseAll ();
			
//			EmoticonUtility.Shutdown ();
//			SoundSetUtility.Shutdown ();
//			ClientUtility.Shutdown ();
//			AddinUtility.Shutdown ();
			
//			_main_window.Destroy ();
//			GalaxiumUtility.TransferWindow.Destroy ();
			
			Application.Quit ();
		}

		private void SaveWindowLocation ()
		{
//			if (_main_window.Visible) {
//				int x, y, w, h;
//				_main_window.GetSize (out w, out h);
//				_main_window.GetPosition (out x, out y);
//				
//				_config.SetInt ("X", x);
//				_config.SetInt ("Y", y);
//				_config.SetInt ("W", w);
//				_config.SetInt ("H", h);
//			}
		}
	}
}

