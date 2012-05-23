using System;
using Gtk;
using Mono.Addins;
using StrongMonkey.Core;
using CookComputing.XmlRpc;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		ServiceSettings.Default.CleanURL = true;
		ServiceSettings.Default.DrupalURL = "http://strongmonkey.net";
		ServiceSettings.Default.EndPoint = "services/xmlrpc";

		// Create a column with title Artist and bind its renderer to model column 0
		view.AppendColumn (
			"Resource",
			new Gtk.CellRendererText (),
			"text",
			0
		);
 
		// Create a column with title 'Song Title' and bind its renderer to model column 1
		view.AppendColumn (
			"Description",
			new Gtk.CellRendererText (),
			"text",
			1
		);

		view.NodeStore = Store;
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	Gtk.NodeStore store;

	Gtk.NodeStore Store {
		get {
			if (store == null) {
				store = new Gtk.NodeStore (typeof(MyTreeNode));
				XmlRpcStruct idx = DrupalConnection.DefinitionIndex ();
				XmlRpcStruct resources = idx ["resources"] as XmlRpcStruct;

				foreach (string resKey in resources.Keys) {
					store.AddNode (new MyTreeNode (
					resKey,
					""
					)
					);
				}
			}
			return store;
		}
	}

	IDrupalConnection drupalConnection;

	public IDrupalConnection DrupalConnection {
		get {
			if (drupalConnection == null) {
				drupalConnection = AddinManager.GetExtensionObjects<IDrupalConnection> () [0];
			}
			return drupalConnection;
		}
	}

	[Gtk.TreeNode (ListOnly=true)]
	public class MyTreeNode : Gtk.TreeNode
	{
		string resource;
		string description;
 
		public MyTreeNode (string resource, string description)
		{
			this.resource = resource;
			this.description = description;
		}
 
		[Gtk.TreeNodeValue (Column=0)]
		public string Resource { get { return resource; } }
 
		[Gtk.TreeNodeValue (Column=1)]
		public string Description { get { return description; } }
	}
}
