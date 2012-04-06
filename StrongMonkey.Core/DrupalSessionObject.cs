using System;
using CookComputing.XmlRpc;
namespace StrongMonkey.Core
{
	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public struct DrupalSessionObject
	{
		public string session_name;
        public string sessid;
		public DrupalUser user;
	}
}
