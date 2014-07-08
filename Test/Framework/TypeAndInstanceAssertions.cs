using System;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that an object is null.
		/// </summary>
		public static void Null(object pObject, AssertionException pException = null) {
			if (pObject != null) throw pException ?? new AssertionException("Object was not null");
		}

		/// <summary>
		/// Asserts that an object is not null.
		/// </summary>
		public static void NotNull(object pObject, AssertionException pException = null) {
			if (pObject == null) throw pException ?? new AssertionException("Object was null");
		}

		/// <summary>
		/// Asserts that a specified object reference is the same as another specified object reference.
		/// </summary>
		public static void Same<TValue>(TValue pLeft, TValue pRight, AssertionException pException = null) where TValue : class {
			if (!object.ReferenceEquals(pLeft, pRight)) throw pException ?? new AssertionException("Expected equal references");
		}

		/// <summary>
		/// Asserts that a specified object reference is not the same as another specified object reference.
		/// </summary>
		public static void NotSame<TValue>(TValue pLeft, TValue pRight, AssertionException pException = null) where TValue : class {
			if (object.ReferenceEquals(pLeft, pRight)) throw pException ?? new AssertionException("Expected equal references");
		}

		/// <summary>
		/// Asserts that the type of a specified object matches the specified type.
		/// </summary>
		public static void IsType<TType>(object pObj, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pObj.GetType() != typeof(TType)) throw pException ?? new AssertionException("Given type: {0}\nExpected type: {1}", pObj.GetType(), typeof(TType));
		}

		/// <summary>
		/// Asserts that the type of a specified object matches the specified type.
		/// </summary>
		public static void IsType(object pObj, Type pType, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pType == null) throw new ArgumentNullException("pType");
			if (pObj.GetType() != pType) throw pException ?? new AssertionException("Given type: {0}\nExpected type: {1}", pObj.GetType(), pType);
		}

		/// <summary>
		/// Asserts that the type of a specified object does not match the specified type.
		/// </summary>
		public static void IsNotType<TType>(object pObj, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pObj.GetType() == typeof(TType)) throw pException ?? new AssertionException("Type is the specified type");
		}

		/// <summary>
		/// Asserts that the type of a specified object does not match the specified type.
		/// </summary>
		public static void IsNotType(object pObj, Type pType, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pType == null) throw new ArgumentNullException("pType");
			if (pObj.GetType() == pType) throw pException ?? new AssertionException("Type is the specified type");
		}

		/// <summary>
		/// Asserts that the type of a specified object is assignable from the specified type.
		/// </summary>
		public static void IsAssignableFromType<TType>(object pObj, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (!pObj.GetType().IsAssignableFrom(typeof(TType))) throw pException ?? new AssertionException("Type is not assignable from the specificed type");
		}

		/// <summary>
		/// Asserts that the type of a specified object is assignable from the specified type.
		/// </summary>
		public static void IsAssignableFromType(object pObj, Type pType, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pType == null) throw new ArgumentNullException("pType");
			if (!pObj.GetType().IsAssignableFrom(pType)) throw pException ?? new AssertionException("Type is not assignable from the specificed type");
		}

		/// <summary>
		/// Asserts that the type of a specified object is not assignable from the specified type.
		/// </summary>
		public static void IsNotAssignableFromType<TType>(object pObj, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pObj.GetType().IsAssignableFrom(typeof(TType))) throw pException ?? new AssertionException("Type is assignable from the specified type");
		}

		/// <summary>
		/// Asserts that the type of a specified object is not assignable from the specified type.
		/// </summary>
		public static void IsNotAssignableFromType(object pObj, Type pType, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pType == null) throw new ArgumentNullException("pType");
			if (pObj.GetType().IsAssignableFrom(pType)) throw pException ?? new AssertionException("Type is assignable from the specified type");
		}
	}
}
