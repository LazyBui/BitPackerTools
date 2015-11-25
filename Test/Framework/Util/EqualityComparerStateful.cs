using System;
using System.Collections.Generic;

namespace Test {
	/// <summary>
	/// Allows arbitrary equality comparison between two instances of the same class.
	/// </summary>
	/// <typeparam name="TValue">The type comparison will be done on.</typeparam>
	internal sealed class EqualityComparerStateful<TValue> : IEqualityComparer<TValue> {
		private Func<TValue, TValue, bool> mComparer = null;
		private Func<TValue, int> mHasher = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.EqualityComparerStateful&lt;TValue&gt;" /> class with a specified comparer delegate and optional hasher delegate.
		/// If the hasher is not specified, the hash of all objects is assumed to be 0.
		/// </summary>
		/// <param name="comparer">The comparer that determines whether two objects are equal.</param>
		/// <param name="hasher">The hash function for a particular object.</param>
		public EqualityComparerStateful(Func<TValue, TValue, bool> comparer, Func<TValue, int> hasher = null) {
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (hasher == null) hasher = v => 0;

			mComparer = comparer;
			mHasher = hasher;
		}

		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <param name="x">The first object of type TValue to compare.</param>
		/// <param name="y">The second object of type TValue to compare.</param>
		/// <returns>true if the specified objects are equal; otherwise, false.</returns>
		public bool Equals(TValue x, TValue y) {
			return mComparer(x, y);
		}

		/// <summary>
		/// Returns a hash code for the specified object.
		/// </summary>
		/// <param name="obj">The TValue for which a hash code is to be returned.</param>
		/// <returns>A hash code for the specified object.</returns>
		public int GetHashCode(TValue obj) {
			return mHasher(obj);
		}
	}
}
