using Mono.Addins;

namespace StrongMonkey.Core
{
	[Mono.Addins.TypeExtensionPoint ("/Core/Services")]
	public interface IService
	{
		bool Initialize ();
	}
}
