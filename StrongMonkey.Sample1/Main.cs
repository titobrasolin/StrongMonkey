using System;
using Gtk;
using Mono.Addins;
namespace StrongMonkey.Sample1
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			AddinManager.Initialize();
			AddinManager.Registry.Update();

			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}
