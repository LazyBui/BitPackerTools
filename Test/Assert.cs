using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Test {
	public class AssertionException : Exception {
		public AssertionException(string pMessage) : base(pMessage) { }
		public AssertionException(string pMessage, Exception pInnerException) : base(pMessage, pInnerException) { }
	}

	public enum Inclusivity {
		Inclusive,
		Exclusive,
	}

	public static class Assert {
		public static void True(bool pCondition) {
			if (!pCondition) throw new AssertionException("Condition was false");
		}

		public static void True(IEnumerable<bool> pSequence) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Any(c => !c)) throw new AssertionException("At least one condition was false");
		}

		public static void False(bool pCondition) {
			if (pCondition) throw new AssertionException("Condition was true");
		}

		public static void False(IEnumerable<bool> pSequence) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Any(c => c)) throw new AssertionException("At least one condition was true");
		}

		public static void Null(object pObject) {
			if (pObject != null) throw new AssertionException("Object was not null");
		}

		public static void NotNull(object pObject) {
			if (pObject == null) throw new AssertionException("Object was null");
		}

		public static void Throws<TException>(Action pAction) where TException : Exception {
			bool thrown = false;
			try {
				pAction();
			}
			catch (Exception e) {
				thrown = true;
				if (e.GetType() != typeof(TException)) {
					throw new AssertionException("Exception type was unexpected", e);
				}
			}

			if (!thrown) throw new AssertionException("Expected exception was not thrown");
		}

		public static void DoesNotThrow(Action pAction) {
			try {
				pAction();
			}
			catch (Exception e) {
				throw new AssertionException("Unexpected exception", e);
			}
		}

		public static void IsType<TType>(object pObj) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pObj.GetType() != typeof(TType)) throw new AssertionException(string.Format("Given type: {0}\nExpected type: {1}", pObj.GetType(), typeof(TType)));
		}

		public static void IsNotType<TType>(object pObj) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pObj.GetType() == typeof(TType)) throw new AssertionException("Type is the specified type");
		}

		public static void GreaterThanEqual<TValue>(TValue pLowerBound, TValue pValue) where TValue : IComparable<TValue> {
			if (pLowerBound == null) throw new ArgumentNullException("Lower bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pLowerBound.CompareTo(pValue) > 0) throw new AssertionException(string.Format("Value fell below expected range: {0}", pValue));
		}

		public static void GreaterThan<TValue>(TValue pLowerBound, TValue pValue) where TValue : IComparable<TValue> {
			if (pLowerBound == null) throw new ArgumentNullException("Lower bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pLowerBound.CompareTo(pValue) >= 0) throw new AssertionException(string.Format("Value fell below expected range: {0}", pValue));
		}

		public static void LessThanEqual<TValue>(TValue pUpperBound, TValue pValue) where TValue : IComparable<TValue> {
			if (pUpperBound == null) throw new ArgumentNullException("Upper bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pUpperBound.CompareTo(pValue) < 0) throw new AssertionException(string.Format("Value fell above expected range: {0}", pValue));
		}

		public static void LessThan<TValue>(TValue pUpperBound, TValue pValue) where TValue : IComparable<TValue> {
			if (pUpperBound == null) throw new ArgumentNullException("Upper bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pUpperBound.CompareTo(pValue) <= 0) throw new AssertionException(string.Format("Value fell above expected range: {0}", pValue));
		}

		public static void InRange<TValue>(TValue pLowerBound, TValue pUpperBound, TValue pValue, Inclusivity pInclusivePolicy = Inclusivity.Inclusive) where TValue : IComparable<TValue> {
			if (pLowerBound == null || pUpperBound == null) throw new ArgumentNullException("Lower bound and upper bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pInclusivePolicy == Inclusivity.Inclusive) {
				if (pLowerBound.CompareTo(pValue) > 0) throw new AssertionException(string.Format("Value fell below expected range: {0}", pValue));
				if (pUpperBound.CompareTo(pValue) < 0) throw new AssertionException(string.Format("Value fell above expected range: {0}", pValue));
			}
			else {
				if (pLowerBound.CompareTo(pValue) >= 0) throw new AssertionException(string.Format("Value fell below expected range: {0}", pValue));
				if (pUpperBound.CompareTo(pValue) <= 0) throw new AssertionException(string.Format("Value fell above expected range: {0}", pValue));
			}
		}

		public static void NotInRange<TValue>(TValue pLowerBound, TValue pUpperBound, TValue pValue, Inclusivity pInclusivePolicy = Inclusivity.Inclusive) where TValue : IComparable<TValue> {
			if (pLowerBound == null || pUpperBound == null) throw new ArgumentNullException("Lower bound and upper bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pInclusivePolicy == Inclusivity.Inclusive) {
				if (pLowerBound.CompareTo(pValue) <= 0 && pUpperBound.CompareTo(pValue) >= 0) throw new AssertionException("Value fell into range");
			}
			else {
				if (pLowerBound.CompareTo(pValue) < 0 && pUpperBound.CompareTo(pValue) > 0) throw new AssertionException("Value fell into range");
			}
		}

		public static void Same<TValue>(TValue pLeft, TValue pRight) where TValue : class {
			if (!object.ReferenceEquals(pLeft, pRight)) throw new AssertionException("Expected equal references");
		}

		public static void NotSame<TValue>(TValue pLeft, TValue pRight) where TValue : class {
			if (object.ReferenceEquals(pLeft, pRight)) throw new AssertionException("Expected equal references");
		}

		public static void Empty<TValue>(IEnumerable<TValue> pSequence) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pSequence.Any()) throw new AssertionException("Expected no elements");
		}

		public static void NotEmpty<TValue>(IEnumerable<TValue> pSequence) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new AssertionException("Expected elements");
		}

		public static void All<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pSequence.All(pPredicate)) throw new AssertionException("Expected all elements to match predicate");
		}

		public static void Any<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pSequence.Any(pPredicate)) throw new AssertionException("Expected at least 1 element to match predicate");
		}

		public static void None<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Any(pPredicate)) throw new AssertionException("Expected no elements to match predicate");
		}

		public static void Contains<TValue>(IEnumerable<TValue> pSequence, TValue pValue) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Contains(pValue)) throw new AssertionException("Expected value is missing");
		}

		public static void DoesNotContain<TValue>(IEnumerable<TValue> pSequence, TValue pValue) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pSequence.Contains(pValue)) throw new AssertionException("Expected value is present");
		}

		private static Type sCollectionType = typeof(ICollection);
		private static Type sCollectionGenericType = typeof(ICollection<>);
		private static bool IsCollectionType(Type pType) { return sCollectionType.IsAssignableFrom(pType) || sCollectionGenericType.IsAssignableFrom(pType); }

		public static void Equal(object pLeft, object pRight) {
			if (pLeft == null && pRight == null) return;
			if (pLeft == null || pRight == null) throw new AssertionException("Expected equal values");
			if (object.ReferenceEquals(pLeft, pRight)) return;
			Type left = pLeft.GetType();
			Type right = pRight.GetType();
			bool isCollectionType = IsCollectionType(left);
			// If only one is a collection, it's obvious that they're unequal
			if (isCollectionType != IsCollectionType(right)) throw new AssertionException("Expected equal values");

			if (isCollectionType) {
				if (left != right) throw new AssertionException("Expected equal values");

				var leftCollection = (pLeft as ICollection);
				var rightCollection = (pRight as ICollection);
				if (leftCollection.Count != rightCollection.Count) throw new AssertionException("Expected equal values");

				var leftIter = leftCollection.GetEnumerator();
				var rightIter = rightCollection.GetEnumerator();
				while (leftIter.MoveNext() && rightIter.MoveNext()) {
					Equal(leftIter.Current, rightIter.Current);
				}
			}
			else {
				var equalityChecker = pLeft as IComparable;
				if (equalityChecker != null) {
					if (equalityChecker.CompareTo(pRight) != 0) throw new AssertionException("Expected equal values");
				}
				else if (!pLeft.Equals(pRight)) throw new AssertionException("Expected equal values");
			}
		}

		public static void NotEqual(object pLeft, object pRight) {
			if (pLeft == null && pRight == null) throw new AssertionException("Expected unequal values");
			if (pLeft == null || pRight == null) return;
			if (object.ReferenceEquals(pLeft, pRight)) throw new AssertionException("Expected unequal values");

			Type left = pLeft.GetType();
			Type right = pRight.GetType();
			bool isCollectionType = IsCollectionType(left);
			// If only one is a collection, it's obvious that they're unequal
			if (isCollectionType != IsCollectionType(right)) return;

			if (isCollectionType) {
				if (left != right) return;

				var leftCollection = (pLeft as ICollection);
				var rightCollection = (pRight as ICollection);
				if (leftCollection.Count != rightCollection.Count) return;

				var leftIter = leftCollection.GetEnumerator();
				var rightIter = rightCollection.GetEnumerator();
				bool foundUnequal = false;
				while (leftIter.MoveNext() && rightIter.MoveNext()) {
					try {
						Equal(leftIter.Current, rightIter.Current);
					}
					catch (AssertionException) {
						foundUnequal = true;
						break;
					}
				}
				if (!foundUnequal) throw new AssertionException("Expected unequal values");
			}
			else {
				var equalityChecker = pLeft as IComparable;
				if (equalityChecker != null) {
					if (equalityChecker.CompareTo(pRight) == 0) throw new AssertionException("Expected unequal values");
				}
				else if (pLeft.Equals(pRight)) throw new AssertionException("Expected unequal values");
			}
		}
	}
}
