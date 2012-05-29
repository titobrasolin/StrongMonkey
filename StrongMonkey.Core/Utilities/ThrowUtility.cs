using System;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;

namespace StrongMonkey.Core.Utilities
{
	public static class ThrowUtility
	{
		[DebuggerHidden]
		public static void ThrowIfNull (string name, object value)
		{
			if (value == null)
				throw new ArgumentNullException (name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfArrayElementIsNull (string name, Array array)
		{
			if (array == null)
				throw new ArgumentNullException (name);

			foreach (object obj in array)
				if (obj == null)
					throw new ArgumentException (String.Format ("Array {0} contains an empty element.", name));
		}
		
		[DebuggerHidden]
		public static void ThrowIfArrayIsEmpty (string name, Array array)
		{
			if (array == null)
				throw new ArgumentNullException (name);

			if (array.Length == 0)
				throw new ArgumentException (String.Format ("Array {0} cannot be empty.", name));
		}
		
		/// <summary>
		/// Throws when the array is null, empty or contains values that are null
		/// </summary>
		/// <param name="name"></param>
		/// <param name="array"></param>
		[DebuggerHidden]
		public static void ThrowIfInvalidArray (string name, Array array)
		{
			ThrowIfArrayElementIsNull (name, array);
			ThrowIfArrayIsEmpty (name, array);
		}
		
		[DebuggerHidden]
		public static void ThrowIfLessThenZero (string name, int value)
		{
			if (value < 0)
				throw new ArgumentException ("Argument must be >= 0.", name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfLessThenOne (string name, int value)
		{
			if (value < 1)
				throw new ArgumentException ("Argument must be > 0.", name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfLessThenMinusOne (string name, int value)
		{
			if (value < -1)
				throw new ArgumentException ("Argument must be >= -1.", name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfLessThenZero (string name, long value)
		{
			if (value < 0L)
				throw new ArgumentException ("Argument must be >= 0.", name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfLessThenOne (string name, long value)
		{
			if (value < 1L)
				throw new ArgumentException ("Argument must be > 0.", name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfLessThenMinusOne (string name, long value)
		{
			if (value < -1L)
				throw new ArgumentException ("Argument must be >= -1.", name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfLessThenZero (string name, double value)
		{
			if (value < 0.0)
				throw new ArgumentException ("Argument must be >= 0.", name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfLessThenOne (string name, double value)
		{
			if (value < 1.0)
				throw new ArgumentException ("Argument must be > 0.", name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfLessThenMinusOne (string name, double value)
		{
			if (value < -1.0)
				throw new ArgumentException ("Argument must be >= -1.", name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfEmpty (string name, string value)
		{
			if (string.IsNullOrEmpty (value))
				throw new ArgumentException ("Argument cannot be null or an empty string.", name);
		}
		
		[DebuggerHidden]
		public static void ThrowIfDefaultValue<U> (string name, U value) where U : IComparable<U>
		{
			if (ArgumentUtility.IsDefaultValue<U> (value))
				throw new ArgumentException (
					String.Format (
						CultureInfo.InvariantCulture,
						"Argument must be different then the default value '{0}'.", default (U).ToString ())
						, name
				);
		}
		
		[DebuggerHidden]
		public static void ThrowIfInvalidEnumValue<U> (string name, object value) where U : struct
		{
			if (!ArgumentUtility.IsValidEnumValue<U> (value))
				throw new ArgumentException (
					String.Format (
						CultureInfo.InvariantCulture,
						"Argument '{0}' is not a valid value for enum type '{1}'.", value.ToString (), typeof (U).ToString ())
						, name
				);
		}
		
		[DebuggerHidden]
		public static void ThrowIfInvalidEnumValue<U> (string name, string value, bool ignoreCase) where U : struct
		{
			if (!ArgumentUtility.IsValidEnumValue<U> (value, ignoreCase))
				throw new ArgumentException (
					String.Format (
						CultureInfo.InvariantCulture,
						"Argument '{0}' is not a valid value for enum type '{1}'.", value, typeof (U).ToString ())
						, name
				);
		}
		
		[DebuggerHidden]
		public static void ThrowIfInvalidEnumValue<U> (string name, string value) where U : struct
		{
			ThrowIfInvalidEnumValue<U> (name, value, false);
		}
		
		[DebuggerHidden]
		public static void ThrowIfTrue (string message, bool value)
		{
			if (value == true)
				throw new ArgumentException (message);
		}
		
		[DebuggerHidden]
		public static void ThrowIfFalse (string message, bool value)
		{
			if (value == false)
				throw new ArgumentException (message);
		}
		
		[DebuggerHidden]
		public static void ThrowIfNotInRange (string name, int number, int rangeStart, int rangeStop)
		{
			if (!ArgumentUtility.IsInRange (number, rangeStart, rangeStop))
				throw new ArgumentException (
					String.Format (
						CultureInfo.InvariantCulture,
						"Value '{0}' is not in the range '{1}' - '{2}'.", number, rangeStart, rangeStop)
						, name
				);
		}
		
		[DebuggerHidden]
		public static void ThrowIfNotInRange (string name, float number, float rangeStart, float rangeStop)
		{
			if (!ArgumentUtility.IsInRange (number, rangeStart, rangeStop))
				throw new ArgumentException (
					String.Format (
						CultureInfo.InvariantCulture,
						"Value '{0}' is not in the range '{1}' - '{2}'.", number, rangeStart, rangeStop)
						, name
				);
		}
		
		[DebuggerHidden]
		public static void ThrowIfNotInRange (string name, double number, double rangeStart, double rangeStop)
		{
			if (!ArgumentUtility.IsInRange (number, rangeStart, rangeStop))
				throw new ArgumentException (
					String.Format (
						CultureInfo.InvariantCulture,
						"Value '{0}' is not in the range '{1}' - '{2}'.", number, rangeStart, rangeStop)
						, name
				);
		}
		
		[DebuggerHidden]
		public static void ThrowIfInvalidFileName (string filename)
		{
			ThrowIfEmpty ("filename", filename);
			if (!FileUtility.IsValidFileName (filename)) {
				throw new IOException (String.Format ("Invalid filename '{0}'", filename));
			}
		}
		
		[DebuggerHidden]
		public static void ThrowIfInvalidDirectoryName (string directory)
		{
			ThrowIfEmpty ("directory", directory);
			if (!FileUtility.IsValidDirectoryName (directory)) {
				throw new IOException (String.Format ("Invalid filename '{0}'", directory));
			}
		}
		
		[DebuggerHidden]
		public static void ThrowDuplicateException (Type cls, string method)
		{
			throw new ApplicationException (
				String.Format ("Duplicate object. (Class: {0}, Method: {1})", cls.FullName, method)
			);
		}

		[DebuggerHidden]
		public static void ThrowIfInvalidType<T> (string argument, object obj)
		{
			if (!(obj is T))
				throw new ArgumentException ("Argument must be " + typeof (T).Name, argument);
		}
	}
}