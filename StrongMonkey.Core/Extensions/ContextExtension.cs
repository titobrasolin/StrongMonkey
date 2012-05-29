using System;
using Mono.Addins;

using StrongMonkey.Core.Interfaces;

namespace StrongMonkey.Core.Extensions
{
	public class ContextExtension : ExtensionNode
	{
		private IExtensionContext _context;
		
		public IExtensionContext Context { get { return _context; } set { _context = value; } }
		
		public ContextExtension (IExtensionContext context): base()
		{
			_context = context;
		}
		
		public ContextExtension () : this(null) { }
	}
}
