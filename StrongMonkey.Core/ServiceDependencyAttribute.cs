using System;

namespace StrongMonkey.Core
{
	// TODO: This should, at some point, include a version
	[AttributeUsage (AttributeTargets.Class, AllowMultiple=true, Inherited=true)]
	public sealed class ServiceDependencyAttribute : Attribute
	{
		/* public properties */
		public Type Type {
			get { return type; }
			set { type = value; }
		}

		public bool Optional {
			get { return optional; }
			set { optional = value; }
		}

		/* public methods */
		public ServiceDependencyAttribute (Type type) : this (type, false) { }

		public ServiceDependencyAttribute (Type type, bool optional)
		{
			this.type = type;
			this.optional = optional;
		}

		/* private fields */
		private Type type = null;
		private bool optional = false;
	}
}
