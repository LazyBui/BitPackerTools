using System;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that an object is null.
		/// </summary>
		public static void Null(object value, AssertionException exception = null) {
			if (value != null) throw exception ?? new AssertionException("Object was not null");
		}

		/// <summary>
		/// Asserts that an object is not null.
		/// </summary>
		public static void NotNull(object value, AssertionException exception = null) {
			if (value == null) throw exception ?? new AssertionException("Object was null");
		}

		/// <summary>
		/// Asserts that a specified object reference is the same as another specified object reference.
		/// </summary>
		public static void Same<TValue>(TValue left, TValue right, AssertionException exception = null) where TValue : class {
			if (!object.ReferenceEquals(left, right)) throw exception ?? new AssertionException("Expected equal references");
		}

		/// <summary>
		/// Asserts that a specified object reference is not the same as another specified object reference.
		/// </summary>
		public static void NotSame<TValue>(TValue left, TValue right, AssertionException exception = null) where TValue : class {
			if (object.ReferenceEquals(left, right)) throw exception ?? new AssertionException("Expected different references");
		}
	}
}
