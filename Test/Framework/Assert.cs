using System;
using System.Collections;
using System.Collections.Generic;

namespace Test {
	/// <summary>
	/// A collection of functions that establish fundamental properties about code.
	/// </summary>
	internal sealed partial class Assert {
		private Assert() { }

		private static Type sCollectionType = typeof(ICollection);
		private static Type sCollectionGenericType = typeof(ICollection<>);
		private static bool IsCollectionType(Type pType) { return sCollectionType.IsAssignableFrom(pType) || sCollectionGenericType.IsAssignableFrom(pType); }
		private static bool IsEqual<TValue>(TValue pLeft, TValue pRight, IEqualityComparer<TValue> pComparer) {
			if (pComparer != null) return pComparer.Equals(pLeft, pRight);
			return IsEqual(pLeft, pRight);
		}
		private static bool IsEqual(object pLeft, object pRight, IEqualityComparer pComparer) {
			if (pComparer != null) return pComparer.Equals(pLeft, pRight);
			return IsEqual(pLeft, pRight);
		}
		private static bool IsEqual(object pLeft, object pRight) {
			if (pLeft == null && pRight == null) return true;
			if (pLeft == null || pRight == null) return false;
			if (object.ReferenceEquals(pLeft, pRight)) return true;

			Type left = pLeft.GetType();
			Type right = pRight.GetType();
			bool isCollectionType = IsCollectionType(left);
			// If only one is a collection, it's obvious that they're unequal
			if (isCollectionType != IsCollectionType(right)) return false;

			if (isCollectionType) {
				// This is potentially tenuous, since it means List<int> { 1, 2, 3 } and int[] { 1, 2, 3 } are unequal
				// I would personally say that's an expected outcome
				if (left != right) return false;

				var leftCollection = (pLeft as ICollection);
				var rightCollection = (pRight as ICollection);
				if (leftCollection.Count != rightCollection.Count) return false;

				var leftIter = leftCollection.GetEnumerator();
				var rightIter = rightCollection.GetEnumerator();
				while (leftIter.MoveNext() && rightIter.MoveNext()) {
					if (!IsEqual(leftIter.Current, rightIter.Current)) {
						return false;
					}
				}
			}
			else {
				var equalityChecker = pLeft as IComparable;
				if (equalityChecker != null) {
					if (equalityChecker.CompareTo(pRight) != 0) return false;
				}
				else if (!pLeft.Equals(pRight)) return false;
			}

			return true;
		}
	}
}
