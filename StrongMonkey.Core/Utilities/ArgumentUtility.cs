using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace StrongMonkey.Core.Utilities
{
	/// <summary>
	/// Helper class to verify arguments.
	/// <remarks>None of these methods will ever raise an exception.</remarks>
	/// </summary>
	public static class ArgumentUtility
	{
		/// <summary>
		/// Check if the number of items in the array is equal to the expected number.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="expectedRank"></param>
		/// <returns></returns>
		public static bool IsDifferentRank (Array array, int expectedRank)
		{
			if (array == null)
				return false;
			return array.Rank == expectedRank;
		}

		/// <summary>
		/// Check if the number of items in the array is less then or equal to the expected number.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="maxRange"></param>
		/// <returns></returns>
		public static bool IsRankInRange (Array array, int maxRange)
		{
			if (array == null)
				return false;
			return IsInRange (array.Rank, 0, maxRange);
		}

		/// <summary>
		/// Check if the number of items in the array is between the two expected numbers.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="rangeStart"></param>
		/// <param name="rangeStop"></param>
		/// <returns></returns>
		public static bool IsRankInRange (Array array, int rangeStart, int rangeStop)
		{
			if (array == null)
				return false;
			return IsInRange (array.Rank, rangeStart, rangeStop);
		}

		/// <summary>
		/// Check if a given number is greater then or equal and less then or equal to a range.
		/// x &gt;= y && x &lt;= z
		/// </summary>
		/// <param name="number"></param>
		/// <param name="rangeStart"></param>
		/// <param name="rangeStop"></param>
		/// <returns></returns>
		public static bool IsInRange (int number, int rangeStart, int rangeStop)
		{
			return number >= rangeStart && number <= rangeStop;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="number"></param>
		/// <param name="rangeStart"></param>
		/// <param name="rangeStop"></param>
		/// <returns></returns>
		public static bool IsInRange (float number, float rangeStart, float rangeStop)
		{
			return number >= rangeStart && number <= rangeStop;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="number"></param>
		/// <param name="rangeStart"></param>
		/// <param name="rangeStop"></param>
		/// <returns></returns>
		public static bool IsInRange (double number, double rangeStart, double rangeStop)
		{
			return number >= rangeStart && number <= rangeStop;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="item"></param>
		/// <param name="rangeStart"></param>
		/// <param name="rangeStop"></param>
		/// <returns></returns>
		public static bool IsInRange<U> (U item, U rangeStart, U rangeStop) where U : IComparable<U>
		{
			if (item == null || rangeStart == null || rangeStop == null)
				return false;
			return rangeStart.CompareTo (item) >= 0 && rangeStop.CompareTo (item) <= 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="rangeStart"></param>
		/// <param name="rangeStop"></param>
		/// <returns></returns>
		public static bool IsInRange (object item, IComparable rangeStart, IComparable rangeStop)
		{
			if (item == null || rangeStart == null || rangeStop == null)
				return false;
			return rangeStart.CompareTo (item) >= 0 && rangeStop.CompareTo (item) <= 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="obj1"></param>
		/// <param name="obj2"></param>
		/// <returns></returns>
		public static bool IsEqual<U> (U obj1, U obj2) where U : IComparable<U>
		{
			if (obj1 == null || obj2 == null)
				return false;
			return obj1.CompareTo (obj2) == 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="obj1"></param>
		/// <param name="obj2"></param>
		/// <returns></returns>
		public static bool IsEqual (IComparable obj1, object obj2)
		{
			if (obj1 == null || obj2 == null)
				return false;
			return obj1.CompareTo (obj2) == 0;
		}

		/// <summary>
		/// Check if a given number is greater then and less then a range.
		/// x &gt; y && x &lt; z
		/// </summary>
		/// <param name="number"></param>
		/// <param name="rangeStart"></param>
		/// <param name="rangeStop"></param>
		/// <returns></returns>
		public static bool IsInExclusiveRange (int number, int rangeStart, int rangeStop)
		{
			return number > rangeStart && number < rangeStop;
		}

		/// <summary>
		/// Check if a given number is greater then or equal to and less then a range.
		/// x &gt;= y && x &lt; z
		/// </summary>
		/// <param name="number"></param>
		/// <param name="rangeStart"></param>
		/// <param name="rangeStop"></param>
		/// <returns></returns>
		public static bool IsInExclusiveUpperRange (int number, int rangeStart, int rangeStop)
		{
			return number >= rangeStart && number < rangeStop;
		}

		/// <summary>
		/// Check if a given number is greater then and less then or equal to a range.
		/// x &gt; y && x &lt;= z
		/// </summary>
		/// <param name="number"></param>
		/// <param name="rangeStart"></param>
		/// <param name="rangeStop"></param>
		/// <returns></returns>
		public static bool IsInExclusiveLowerRange (int number, int rangeStart, int rangeStop)
		{
			return number > rangeStart && number <= rangeStop;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="enumerable"></param>
		/// <returns></returns>
		public static bool IsEmpty<U> (IEnumerable<U> enumerable)
		{
			if (enumerable != null)
				return IsEmpty (enumerable.GetEnumerator ());
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="enumerator"></param>
		/// <returns></returns>
		public static bool IsEmpty<U> (IEnumerator<U> enumerator)
		{
			if (enumerator != null) {
				enumerator.Reset ();
				return !enumerator.MoveNext ();
			}
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="enumerable"></param>
		/// <returns></returns>
		public static bool IsEmpty (IEnumerable enumerable)
		{
			if (enumerable != null)
				return IsEmpty (enumerable.GetEnumerator ());
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="enumerator"></param>
		/// <returns></returns>
		public static bool IsEmpty (IEnumerator enumerator)
		{
			if (enumerator != null) {
				enumerator.Reset ();
				return !enumerator.MoveNext ();
			}
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsDefaultValue<U> (U value) where U : IComparable<U>
		{
			//no null check required
			return default (U).CompareTo (value) == 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool IsNullable (object obj)
		{
			var type = obj.GetType ();

			return (type.IsGenericType
			        && type.GetGenericTypeDefinition ().Equals (typeof (Nullable<>)));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool IsNullable<U> (object obj) where U : struct
		{
			if (obj == null)
				return false;

			return obj is Nullable<U>;
		}

		/// <summary>
		/// Check if the 2 objects are from the same type.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool IsDifferentType (object item, object other)
		{
			if (item == null || other == null)
				return true;
			return item.GetType () == other.GetType ();
		}

		/// <summary>
		/// Check if the object is from the given type.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsDifferentType (object item, Type type)
		{
			if (item == null || type == null)
				return true;
			return item.GetType () == type;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		/// <param name="other"></param>
		/// <returns></returns>
		public static bool IsCompatibleType (object item, object other)
		{
			if (item == null || other == null)
				return false;
			return other.GetType ().IsAssignableFrom (item.GetType ());
		}

		/// <summary>
		/// Check if a given item can be assigned to a specific type.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsCompatibleType (object item, Type type)
		{
			if (item == null || type == null)
				return false;
			return type.IsAssignableFrom (item.GetType ());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsValidEnumValue<U> (object value) where U : struct
		{
			if (value == null)
				return false;

			return Enum.IsDefined (typeof (U), value);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsValidEnumValue<U> (string value) where U : struct
		{
			if (value == null || value.Length == 0)
				return false;

			return IsValidEnumValue<U> (value, false);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="value"></param>
		/// <param name="ignoreCase"></param>
		/// <returns></returns>
		public static bool IsValidEnumValue<U> (string value, bool ignoreCase) where U : struct
		{
			if (value == null || value.Length == 0)
				return false;
			try {
				U u = (U)Enum.Parse (typeof (U), value, ignoreCase);
				return IsValidEnumValue<U> (u);
			} catch (InvalidCastException) {
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="U"></typeparam>
		/// <param name="collection"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsInCollection<U> (ICollection<U> collection, U value)
		{
			if (collection == null || value == null)
				return false;
			return collection.Contains (value);
		}
	}
}