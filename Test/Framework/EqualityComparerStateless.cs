using System;
using System.Collections.Generic;

namespace Test {
	internal sealed class EqualityComparerStateless<TKey, TValue> : IEqualityComparer<TKey> {
		private Func<TKey, TValue> mSelector = null;

		public EqualityComparerStateless(Func<TKey, TValue> pSelector) {
			if (pSelector == null) throw new ArgumentNullException("pSelector");
			mSelector = pSelector;
		}

		public bool Equals(TKey x, TKey y) {
			return mSelector(x).Equals(mSelector(y));
		}

		public int GetHashCode(TKey obj) {
			return mSelector(obj).GetHashCode();
		}
	}
}
