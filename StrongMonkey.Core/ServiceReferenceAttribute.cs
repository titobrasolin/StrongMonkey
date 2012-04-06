using System;

namespace StrongMonkey.Core
{
	[AttributeUsage (AttributeTargets.Field)]
	public sealed class ServiceReferenceAttribute : Attribute
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
		public ServiceReferenceAttribute () : this (null, false) { }

		public ServiceReferenceAttribute (bool optional)
			: this (null, optional) { }

		public ServiceReferenceAttribute (Type type)
			: this (type, false) { }

		public ServiceReferenceAttribute (Type type, bool optional)
		{
			this.type = type;
			this.optional = optional;
		}

		/* private fields */
		private Type type = null;
		private bool optional = false;
	}
}
