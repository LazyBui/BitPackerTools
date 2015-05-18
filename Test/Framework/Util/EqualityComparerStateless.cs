using System;
using System.Collections.Generic;

namespace Test {
	/// <summary>
	/// Allows equality comparison based on an arbitrary property of class.
	/// </summary>
	/// <typeparam name="TKey">The type that comparison will be done on.</typeparam>
	/// <typeparam name="TValue">The type of the property that will compared for equality.</typeparam>
	internal sealed class EqualityComparerStateless<TKey, TValue> : IEqualityComparer<TKey> where TValue : IEquatable<TValue> {
		private Func<TKey, TValue> mSelector = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.EqualityComparerStateless&lt;TKey, TValue&gt;" /> class with a specified selector delegate.
		/// </summary>
		/// <param name="pSelector">The selector that determines which property to obtain from an object.</param>
		public EqualityComparerStateless(Func<TKey, TValue> pSelector) {
			if (pSelector == null) throw new ArgumentNullException(nameof(pSelector));
			mSelector = pSelector;
		}

		/// <summary>
		/// Determines whether the specified objects are equal based on a selected property.
		/// </summary>
		/// <param name="x">The first object of type TKey to compare.</param>
		/// <param name="y">The second object of type TKey to compare.</param>
		/// <returns>true if the selected property of the specified objects are equal; otherwise, false.</returns>
		public bool Equals(TKey x, TKey y) {
			return mSelector(x).Equals(mSelector(y));
		}

		/// <summary>
		/// Returns a hash code for the specified object based on a selected property.
		/// </summary>
		/// <param name="obj">The TKey for which a hash code is to be returned.</param>
		/// <returns>A hash code for the selected property of the specified object.</returns>
		public int GetHashCode(TKey obj) {
			return mSelector(obj).GetHashCode();
		}
	}
}
