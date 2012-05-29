using System;
using System.Collections.Generic;

namespace StrongMonkey.Core.Interfaces
{
	public interface IExtensionContext
	{
		object Object { get; set; }
		bool HasObject { get; }
	}
}