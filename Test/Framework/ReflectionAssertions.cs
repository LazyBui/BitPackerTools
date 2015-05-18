using System;
using System.Reflection;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that the type of a specified object matches the specified type.
		/// </summary>
		public static void IsType<TType>(object pObj, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException(nameof(pObj));
			if (pObj.GetType() != typeof(TType)) throw pException ?? new AssertionException("Given type: {0}\nExpected type: {1}", pObj.GetType(), typeof(TType));
		}

		/// <summary>
		/// Asserts that the type of a specified object matches the specified type.
		/// </summary>
		public static void IsType(object pObj, Type pType, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException(nameof(pObj));
			if (pType == null) throw new ArgumentNullException(nameof(pType));
			if (pObj.GetType() != pType) throw pException ?? new AssertionException("Given type: {0}\nExpected type: {1}", pObj.GetType(), pType);
		}

		/// <summary>
		/// Asserts that the type of a specified object does not match the specified type.
		/// </summary>
		public static void IsNotType<TType>(object pObj, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException(nameof(pObj));
			if (pObj.GetType() == typeof(TType)) throw pException ?? new AssertionException("Type is the specified type");
		}

		/// <summary>
		/// Asserts that the type of a specified object does not match the specified type.
		/// </summary>
		public static void IsNotType(object pObj, Type pType, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException(nameof(pObj));
			if (pType == null) throw new ArgumentNullException(nameof(pType));
			if (pObj.GetType() == pType) throw pException ?? new AssertionException("Type is the specified type");
		}

		/// <summary>
		/// Asserts that the type of a specified object is assignable from the specified type.
		/// </summary>
		public static void IsAssignableFromType<TType>(object pObj, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException(nameof(pObj));
			if (!pObj.GetType().IsAssignableFrom(typeof(TType))) throw pException ?? new AssertionException("Type is not assignable from the specificed type");
		}

		/// <summary>
		/// Asserts that the type of a specified object is assignable from the specified type.
		/// </summary>
		public static void IsAssignableFromType(object pObj, Type pType, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException(nameof(pObj));
			if (pType == null) throw new ArgumentNullException(nameof(pType));
			if (!pObj.GetType().IsAssignableFrom(pType)) throw pException ?? new AssertionException("Type is not assignable from the specificed type");
		}

		/// <summary>
		/// Asserts that the type of a specified object is not assignable from the specified type.
		/// </summary>
		public static void IsNotAssignableFromType<TType>(object pObj, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException(nameof(pObj));
			if (pObj.GetType().IsAssignableFrom(typeof(TType))) throw pException ?? new AssertionException("Type is assignable from the specified type");
		}

		/// <summary>
		/// Asserts that the type of a specified object is not assignable from the specified type.
		/// </summary>
		public static void IsNotAssignableFromType(object pObj, Type pType, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException(nameof(pObj));
			if (pType == null) throw new ArgumentNullException(nameof(pType));
			if (pObj.GetType().IsAssignableFrom(pType)) throw pException ?? new AssertionException("Type is assignable from the specified type");
		}
	}
}
