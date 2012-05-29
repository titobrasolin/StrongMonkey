using System;
using Mono.Addins;

namespace StrongMonkey.Core.Extensions
{
	public delegate void EntryPointCallback (params object[] args);

	public class EntryPointExtension : ExtensionNode
	{
		[NodeAttribute]
		private string _class = null;
		
		[NodeAttribute]
		private string _method = null;
		
		private EntryPointCallback _callback;

		public void Execute (params object[] args)
		{
			if (_callback != null)
				_callback (args);
		}
		
		public string Class
		{
			get { return _class; }
		}
		
		public string Method
		{
			get { return _method; }
		}
		
		public EntryPointCallback Callback
		{
			get { return _callback; }
			set { _callback = value; }
		}
	}
}