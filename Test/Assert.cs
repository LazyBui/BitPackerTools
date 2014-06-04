using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Test {
	/// <summary>
	/// A collection of functions that establish fundamental properties about code.
	/// </summary>
	internal static class Assert {
		/// <summary>
		/// Asserts that a condition is true.
		/// </summary>
		public static void True(bool pCondition, AssertionException pException = null) {
			if (!pCondition) throw pException ?? new AssertionException("Condition was false");
		}

		/// <summary>
		/// Asserts that a series of conditions are all true.
		/// </summary>
		public static void True(IEnumerable<bool> pSequence, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Any(c => !c)) throw pException ?? new AssertionException("At least one condition was false");
		}

		/// <summary>
		/// Asserts that a condition is false.
		/// </summary>
		public static void False(bool pCondition, AssertionException pException = null) {
			if (pCondition) throw pException ?? new AssertionException("Condition was true");
		}

		/// <summary>
		/// Asserts that a series of conditions are all false.
		/// </summary>
		public static void False(IEnumerable<bool> pSequence, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Any(c => c)) throw pException ?? new AssertionException("At least one condition was true");
		}

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
		/// Asserts that a specific exception is thrown by the action.
		/// </summary>
		public static void Throws<TException>(Action pAction, AssertionException pException = null) where TException : Exception {
			bool thrown = false;
			try {
				pAction();
			}
			catch (Exception e) {
				thrown = true;
				if (e.GetType() != typeof(TException)) {
					throw pException ?? AssertionException.GenerateWithInnerException(e, "Exception type was unexpected");
				}
			}

			if (!thrown) throw pException ?? new AssertionException("Expected exception was not thrown");
		}

		/// <summary>
		/// Asserts that an action does not throw at all.
		/// </summary>
		public static void DoesNotThrow(Action pAction, AssertionException pException = null) {
			try {
				pAction();
			}
			catch (Exception e) {
				throw pException ?? AssertionException.GenerateWithInnerException(e, "Unexpected exception");
			}
		}

		/// <summary>
		/// Asserts that the type of a specified object matches the specified type.
		/// </summary>
		public static void IsType<TType>(object pObj, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pObj.GetType() != typeof(TType)) throw pException ?? new AssertionException("Given type: {0}\nExpected type: {1}", pObj.GetType(), typeof(TType));
		}

		/// <summary>
		/// Asserts that the type of a specified object does not match the specified type.
		/// </summary>
		public static void IsNotType<TType>(object pObj, AssertionException pException = null) {
			if (pObj == null) throw new ArgumentNullException("pObj");
			if (pObj.GetType() == typeof(TType)) throw pException ?? new AssertionException("Type is the specified type");
		}

		/// <summary>
		/// Asserts that the specified value compares either greater or equal to a specified lower bound.
		/// </summary>
		public static void GreaterThanEqual<TValue>(TValue pLowerBound, TValue pValue, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null) throw new ArgumentNullException("Lower bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pLowerBound.CompareTo(pValue) > 0) throw pException ?? new AssertionException("Value fell below expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares strictly greater to a specified lower bound.
		/// </summary>
		public static void GreaterThan<TValue>(TValue pLowerBound, TValue pValue, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null) throw new ArgumentNullException("Lower bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pLowerBound.CompareTo(pValue) >= 0) throw pException ?? new AssertionException("Value fell below expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares either less or equal to a specified upper bound.
		/// </summary>
		public static void LessThanEqual<TValue>(TValue pUpperBound, TValue pValue, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pUpperBound == null) throw new ArgumentNullException("Upper bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pUpperBound.CompareTo(pValue) < 0) throw pException ?? new AssertionException("Value fell above expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares strictly less to a specified upper bound.
		/// </summary>
		public static void LessThan<TValue>(TValue pUpperBound, TValue pValue, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pUpperBound == null) throw new ArgumentNullException("Upper bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pUpperBound.CompareTo(pValue) <= 0) throw pException ?? new AssertionException("Value fell above expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares either greater or equal to a specified lower bound AND less or equal to a specified upper bound.
		/// </summary>
		public static void InRangeEqual<TValue>(TValue pLowerBound, TValue pUpperBound, TValue pValue, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null || pUpperBound == null) throw new ArgumentNullException("Lower bound and upper bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pLowerBound.CompareTo(pValue) > 0) throw pException ?? new AssertionException("Value fell below expected range: {0}", pValue);
			if (pUpperBound.CompareTo(pValue) < 0) throw pException ?? new AssertionException("Value fell above expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares strictly greater to a specified lower bound AND strictly less to a specified upper bound.
		/// </summary>
		public static void InRange<TValue>(TValue pLowerBound, TValue pUpperBound, TValue pValue, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null || pUpperBound == null) throw new ArgumentNullException("Lower bound and upper bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pLowerBound.CompareTo(pValue) >= 0) throw pException ?? new AssertionException("Value fell below expected range: {0}", pValue);
			if (pUpperBound.CompareTo(pValue) <= 0) throw pException ?? new AssertionException("Value fell above expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares either greater or equal to a specified upper bound AND less or equal to a specified lower bound.
		/// </summary>
		public static void NotInRangeEqual<TValue>(TValue pLowerBound, TValue pUpperBound, TValue pValue, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null || pUpperBound == null) throw new ArgumentNullException("Lower bound and upper bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pLowerBound.CompareTo(pValue) < 0 && pUpperBound.CompareTo(pValue) > 0) throw pException ?? new AssertionException("Value fell into range");
		}

		/// <summary>
		/// Asserts that the specified value compares strictly greater to a specified upper bound AND strictly less to a specified lower bound.
		/// </summary>
		public static void NotInRange<TValue>(TValue pLowerBound, TValue pUpperBound, TValue pValue, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null || pUpperBound == null) throw new ArgumentNullException("Lower bound and upper bound must be populated to constrain a range");
			if (pValue == null) throw new ArgumentNullException("Value must be populated to be in a range");
			if (pLowerBound.CompareTo(pValue) <= 0 && pUpperBound.CompareTo(pValue) >= 0) throw pException ?? new AssertionException("Value fell into range");
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
		/// Asserts that sequence has no elements.
		/// </summary>
		public static void Empty<TValue>(IEnumerable<TValue> pSequence, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pSequence.Any()) throw pException ?? new AssertionException("Expected no elements");
		}

		/// <summary>
		/// Asserts that sequence has one or more elements.
		/// </summary>
		public static void NotEmpty<TValue>(IEnumerable<TValue> pSequence, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw pException ?? new AssertionException("Expected elements");
		}

		/// <summary>
		/// Asserts that an entire sequence with one or more elements matches the predicate.
		/// </summary>
		public static void All<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pSequence.All(pPredicate)) throw pException ?? new AssertionException("Expected all elements to match predicate");
		}

		/// <summary>
		/// Asserts that one or more elements of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void Any<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pSequence.Any(pPredicate)) throw pException ?? new AssertionException("Expected at least 1 element to match predicate");
		}

		/// <summary>
		/// Asserts that exactly one element of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void Only<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count(pPredicate) != 1) throw pException ?? new AssertionException("Expected only 1 element to match predicate");
		}

		/// <summary>
		/// Asserts that exactly a specified number of elements of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void Count<TValue>(IEnumerable<TValue> pSequence, int pCount, Func<TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Count(pPredicate) != pCount) throw pException ?? new AssertionException("Expected {0} elements to match predicate", pCount);
		}

		/// <summary>
		/// Asserts that an no elements of a sequence with one or more elements matches the predicate.
		/// </summary>
		public static void None<TValue>(IEnumerable<TValue> pSequence, Func<TValue, bool> pPredicate, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (pPredicate == null) throw new ArgumentNullException("pPredicate");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Any(pPredicate)) throw pException ?? new AssertionException("Expected no elements to match predicate");
		}

		/// <summary>
		/// Asserts that a sequence contains a specific value.
		/// </summary>
		public static void Contains<TValue>(IEnumerable<TValue> pSequence, TValue pValue, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (!pSequence.Contains(pValue)) throw pException ?? new AssertionException("Expected value is missing");
		}

		/// <summary>
		/// Asserts that a sequence does not contain a specific value.
		/// </summary>
		public static void DoesNotContain<TValue>(IEnumerable<TValue> pSequence, TValue pValue, AssertionException pException = null) {
			if (pSequence == null) throw new ArgumentNullException("pSequence");
			if (!pSequence.Any()) throw new ArgumentException("Expected elements", "pSequence");
			if (pSequence.Contains(pValue)) throw pException ?? new AssertionException("Expected value is present");
		}

		private static Type sCollectionType = typeof(ICollection);
		private static Type sCollectionGenericType = typeof(ICollection<>);
		private static bool IsCollectionType(Type pType) { return sCollectionType.IsAssignableFrom(pType) || sCollectionGenericType.IsAssignableFrom(pType); }

		/// <summary>
		/// Asserts that two specified objects are equal.
		/// null and null are considered equal.
		/// The types must also be consistent in order to compare equal (so you cannot compare sbyte with int, for example).
		/// </summary>
		public static void Equal(object pLeft, object pRight, AssertionException pException = null) {
			if (pLeft == null && pRight == null) return;
			if (pLeft == null || pRight == null) throw pException ?? new AssertionException("Expected equal values");
			if (object.ReferenceEquals(pLeft, pRight)) return;
			Type left = pLeft.GetType();
			Type right = pRight.GetType();
			bool isCollectionType = IsCollectionType(left);
			// If only one is a collection, it's obvious that they're unequal
			if (isCollectionType != IsCollectionType(right)) throw pException ?? new AssertionException("Expected equal values");

			if (isCollectionType) {
				if (left != right) throw pException ?? new AssertionException("Expected equal values");

				var leftCollection = (pLeft as ICollection);
				var rightCollection = (pRight as ICollection);
				if (leftCollection.Count != rightCollection.Count) throw pException ?? new AssertionException("Expected equal values");

				var leftIter = leftCollection.GetEnumerator();
				var rightIter = rightCollection.GetEnumerator();
				while (leftIter.MoveNext() && rightIter.MoveNext()) {
					Equal(leftIter.Current, rightIter.Current);
				}
			}
			else {
				var equalityChecker = pLeft as IComparable;
				if (equalityChecker != null) {
					if (equalityChecker.CompareTo(pRight) != 0) throw pException ?? new AssertionException("Expected equal values");
				}
				else if (!pLeft.Equals(pRight)) throw pException ?? new AssertionException("Expected equal values");
			}
		}

		/// <summary>
		/// Asserts that two specified objects are not equal.
		/// null and null are considered equal.
		/// The types must also be consistent in order to compare equal (so this assertion would be relatively useless if you were comparing sbyte with int, for example).
		/// </summary>
		public static void NotEqual(object pLeft, object pRight, AssertionException pException = null) {
			if (pLeft == null && pRight == null) throw pException ?? new AssertionException("Expected unequal values");
			if (pLeft == null || pRight == null) return;
			if (object.ReferenceEquals(pLeft, pRight)) throw pException ?? new AssertionException("Expected unequal values");

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
				if (!foundUnequal) throw pException ?? new AssertionException("Expected unequal values");
			}
			else {
				var equalityChecker = pLeft as IComparable;
				if (equalityChecker != null) {
					if (equalityChecker.CompareTo(pRight) == 0) throw pException ?? new AssertionException("Expected unequal values");
				}
				else if (pLeft.Equals(pRight)) throw pException ?? new AssertionException("Expected unequal values");
			}
		}
	}
}
