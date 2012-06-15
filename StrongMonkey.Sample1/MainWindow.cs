using System;
using Gtk;
using Mono.Addins;
using StrongMonkey.Core;
using CookComputing.XmlRpc;
using System.Net;
using System.Collections.Generic;
using System.Text;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		ServiceSettings.Default.CleanURL = true;
		ServiceSettings.Default.DrupalURL = "http://www.strongmonkey.net";
		ServiceSettings.Default.EndPoint = "services/xmlrpc";

		DrupalConnection.Login("admin", "Titbra.21");
		XmlRpcStruct geo = DrupalConnection.GeocoderIndex();

		string res = DrupalConnection.GeocoderRetrieve("yahoo", "Via Brotto 10, 35128, Padova, Italy", "json");

		view.AppendColumn (
			"Operation",
			new Gtk.CellRendererText (),
			"text",
			0
		);
 
		view.AppendColumn (
			"Enabled",
			new Gtk.CellRendererText (),
			"text",
			1
		);

		view.Model = this.MyTreeStore;
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	Gtk.TreeStore myTreeStore;

	Gtk.TreeStore MyTreeStore {
		get {
			if (myTreeStore == null) {
				myTreeStore = new Gtk.TreeStore (typeof(string), typeof(string));

				XmlRpcStruct idx = DrupalConnection.DefinitionIndex ();
				XmlRpcStruct resources = (XmlRpcStruct)idx ["resources"];

				Gtk.TreeIter iter;
				XmlRpcStruct operations;
				// TODO: DISPLAY ACTIONS, RELATIONSHIPS AND OPERATIONS.
				foreach (string resKey in resources.Keys) {
					iter = myTreeStore.AppendValues (resKey);
					operations = ((XmlRpcStruct)resources [resKey]) ["operations"] as XmlRpcStruct;
					if (operations == null) {
						operations = ((XmlRpcStruct)resources [resKey]) ["actions"] as XmlRpcStruct;
					}
					foreach (string opKey in operations.Keys) {
						myTreeStore.AppendValues (iter, opKey, ((int)((XmlRpcStruct)operations [opKey]) ["enabled"] > 0).ToString ().ToLower ());
					}
				}
			}
			return myTreeStore;
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
