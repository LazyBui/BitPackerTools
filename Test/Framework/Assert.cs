using System;
using System.Collections;
using System.Collections.Generic;

namespace Test {
	/// <summary>
	/// A collection of functions that establish fundamental properties about code.
	/// </summary>
	internal sealed partial class Assert {
		private Assert() { }

		private static void VerifyNotNullOrEmpty(string value, string argName) {
			if (value == null) throw new ArgumentNullException(argName);
			if (value.Length == 0) throw new ArgumentException("String is blank", argName);
		}

		private static readonly Type sCollectionType = typeof(ICollection);
		private static readonly Type sCollectionGenericType = typeof(ICollection<>);
		private static bool IsCollectionType(Type type) { return sCollectionType.IsAssignableFrom(type) || sCollectionGenericType.IsAssignableFrom(type); }
		private static bool IsEqual<TValue>(TValue left, TValue right, IEqualityComparer<TValue> comparer) {
			if (comparer != null) return comparer.Equals(left, right);
			return IsEqual(left, right);
		}
		private static bool IsEqual(object left, object right, IEqualityComparer comparer) {
			if (comparer != null) return comparer.Equals(left, right);
			return IsEqual(left, right);
		}
		private static bool IsEqual(object left, object right) {
			if (left == null && right == null) return true;
			if (left == null || right == null) return false;
			if (object.ReferenceEquals(left, right)) return true;

			Type typeLeft = left.GetType();
			Type typeRight = right.GetType();
			bool isCollectionType = IsCollectionType(typeLeft);
			// If only one is a collection, it's obvious that they're unequal
			if (isCollectionType != IsCollectionType(typeRight)) return false;

			if (isCollectionType) {
				// This is potentially tenuous, since it means List<int> { 1, 2, 3 } and int[] { 1, 2, 3 } are unequal
				// I would personally say that's an expected outcome
				if (typeLeft != typeRight) return false;

				var leftCollection = left as ICollection;
				var rightCollection = right as ICollection;
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
				var equalityChecker = left as IComparable;
				if (equalityChecker != null) {
					if (equalityChecker.CompareTo(right) != 0) return false;
				}
				else if (!left.Equals(right)) return false;
			}

			return true;
		}
	}
}
