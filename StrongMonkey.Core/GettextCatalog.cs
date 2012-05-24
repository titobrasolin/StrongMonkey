using System;
using System.IO;

using Mono.Unix;

namespace StrongMonkey.Core
{
	public static class GettextCatalog
	{
		static GettextCatalog ()
		{
			//variable can be used to override where Gettext looks for the catalogues
			string catalog = System.Environment.GetEnvironmentVariable ("STRONGMONKEY_LOCALE_PATH");

			if (string.IsNullOrEmpty (catalog)) {
				string location = System.Reflection.Assembly.GetExecutingAssembly ().Location;
				location = Path.GetDirectoryName (location);
				// strongmonkey is located at $prefix/lib/strongmonkey/
				// adding "../.." should give us $prefix
				string prefix = Path.Combine (Path.Combine (location, ".."), "..");
				//normalise it
				prefix = Path.GetFullPath (prefix);
				//catalogue is installed to "$prefix/share/locale" by default
				catalog = Path.Combine (Path.Combine (prefix, "share"), "locale");
			}
			
			Catalog.Init ("strongmonkey", catalog);
		}
		
		public static string GetString (string phrase)
		{
			return Catalog.GetString (phrase);
		}
		
		public static string GetString (string phrase, object arg0)
		{
			return string.Format (Catalog.GetString (phrase), arg0);
		}
		
		public static string GetString (string phrase, object arg0, object arg1)
		{
			return string.Format (Catalog.GetString (phrase), arg0, arg1);
		}
		
		public static string GetString (string phrase, object arg0, object arg1, object arg2)
		{
			return string.Format (Catalog.GetString (phrase), arg0, arg1, arg2);
		}
		
		public static string GetString (string phrase, params object[] args)
		{
			return string.Format (Catalog.GetString (phrase), args);
		}
		
		public static string GetPluralString (string singular, string plural, int number)
		{
			return Catalog.GetPluralString (singular, plural, number);
		}
		
		public static string GetPluralString (string singular, string plural, int number, object arg0)
		{
			return string.Format (Catalog.GetPluralString (singular, plural, number), arg0);
		}
		
		public static string GetPluralString (string singular, string plural, int number, object arg0, object arg1)
		{
			return string.Format (Catalog.GetPluralString (singular, plural, number), arg0, arg1);
		}
		
		public static string GetPluralString (string singular, string plural, int number, 
		                                      object arg0, object arg1, object arg2)
		{
			return string.Format (Catalog.GetPluralString (singular, plural, number), arg0, arg1, arg2);
		}
		
		public static string GetPluralString (string singular, string plural, int number, params object[] args)
		{
			return string.Format (Catalog.GetPluralString (singular, plural, number), args);
		}
	}
}