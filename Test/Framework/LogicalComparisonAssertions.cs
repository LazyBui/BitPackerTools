using System;
using System.Collections;
using System.Collections.Generic;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that the specified value compares either greater or equal to a specified lower bound.
		/// </summary>
		public static void GreaterThanEqual<TValue>(TValue pValue, TValue pLowerBound, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null) throw new ArgumentNullException(nameof(pLowerBound));
			if (pValue == null) throw new ArgumentNullException(nameof(pValue));
			if (pLowerBound.CompareTo(pValue) > 0) throw pException ?? new AssertionException("Value fell below expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares strictly greater to a specified lower bound.
		/// </summary>
		public static void GreaterThan<TValue>(TValue pValue, TValue pLowerBound, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null) throw new ArgumentNullException(nameof(pLowerBound));
			if (pValue == null) throw new ArgumentNullException(nameof(pValue));
			if (pLowerBound.CompareTo(pValue) >= 0) throw pException ?? new AssertionException("Value fell below expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares either less or equal to a specified upper bound.
		/// </summary>
		public static void LessThanEqual<TValue>(TValue pValue, TValue pUpperBound, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pUpperBound == null) throw new ArgumentNullException(nameof(pUpperBound));
			if (pValue == null) throw new ArgumentNullException(nameof(pValue));
			if (pUpperBound.CompareTo(pValue) < 0) throw pException ?? new AssertionException("Value fell above expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares strictly less to a specified upper bound.
		/// </summary>
		public static void LessThan<TValue>(TValue pValue, TValue pUpperBound, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pUpperBound == null) throw new ArgumentNullException(nameof(pUpperBound));
			if (pValue == null) throw new ArgumentNullException(nameof(pValue));
			if (pUpperBound.CompareTo(pValue) <= 0) throw pException ?? new AssertionException("Value fell above expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares either greater or equal to a specified lower bound AND less or equal to a specified upper bound.
		/// </summary>
		public static void InRangeEqual<TValue>(TValue pValue, TValue pLowerBound, TValue pUpperBound, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null) throw new ArgumentNullException(nameof(pLowerBound));
			if (pUpperBound == null) throw new ArgumentNullException(nameof(pUpperBound));
			if (pValue == null) throw new ArgumentNullException(nameof(pValue));
			if (pLowerBound.CompareTo(pValue) > 0) throw pException ?? new AssertionException("Value fell below expected range: {0}", pValue);
			if (pUpperBound.CompareTo(pValue) < 0) throw pException ?? new AssertionException("Value fell above expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares strictly greater to a specified lower bound AND strictly less to a specified upper bound.
		/// </summary>
		public static void InRange<TValue>(TValue pValue, TValue pLowerBound, TValue pUpperBound, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null) throw new ArgumentNullException(nameof(pLowerBound));
			if (pUpperBound == null) throw new ArgumentNullException(nameof(pUpperBound));
			if (pValue == null) throw new ArgumentNullException(nameof(pValue));
			if (pLowerBound.CompareTo(pValue) >= 0) throw pException ?? new AssertionException("Value fell below expected range: {0}", pValue);
			if (pUpperBound.CompareTo(pValue) <= 0) throw pException ?? new AssertionException("Value fell above expected range: {0}", pValue);
		}

		/// <summary>
		/// Asserts that the specified value compares either greater or equal to a specified upper bound AND less or equal to a specified lower bound.
		/// </summary>
		public static void NotInRangeEqual<TValue>(TValue pValue, TValue pLowerBound, TValue pUpperBound, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null) throw new ArgumentNullException(nameof(pLowerBound));
			if (pUpperBound == null) throw new ArgumentNullException(nameof(pUpperBound));
			if (pValue == null) throw new ArgumentNullException(nameof(pValue));
			if (pLowerBound.CompareTo(pValue) < 0 && pUpperBound.CompareTo(pValue) > 0) throw pException ?? new AssertionException("Value fell into range");
		}

		/// <summary>
		/// Asserts that the specified value compares strictly greater to a specified upper bound AND strictly less to a specified lower bound.
		/// </summary>
		public static void NotInRange<TValue>(TValue pValue, TValue pLowerBound, TValue pUpperBound, AssertionException pException = null) where TValue : IComparable<TValue> {
			if (pLowerBound == null) throw new ArgumentNullException(nameof(pLowerBound));
			if (pUpperBound == null) throw new ArgumentNullException(nameof(pUpperBound));
			if (pValue == null) throw new ArgumentNullException(nameof(pValue));
			if (pLowerBound.CompareTo(pValue) <= 0 && pUpperBound.CompareTo(pValue) >= 0) throw pException ?? new AssertionException("Value fell into range");
		}

		/// <summary>
		/// Asserts that two specified objects are equal given a specific comparer.
		/// </summary>
		public static void Equal<TValue>(TValue pLeft, TValue pRight, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pComparer == null) throw new ArgumentNullException(nameof(pComparer));
			if (!IsEqual(pLeft, pRight, pComparer)) throw pException ?? new AssertionException("Expected equal values");
		}

		/// <summary>
		/// Asserts that two specified objects are equal given a specific comparer.
		/// </summary>
		public static void Equal(object pLeft, object pRight, IEqualityComparer pComparer, AssertionException pException = null) {
			if (pComparer == null) throw new ArgumentNullException(nameof(pComparer));
			if (!IsEqual(pLeft, pRight, pComparer)) throw pException ?? new AssertionException("Expected equal values");
		}

		/// <summary>
		/// Asserts that two specified objects are equal.
		/// null and null are considered equal.
		/// The types must also be consistent in order to compare equal (so you cannot compare sbyte with int, for example).
		/// </summary>
		public static void Equal(object pLeft, object pRight, AssertionException pException = null) {
			if (!IsEqual(pLeft, pRight)) throw pException ?? new AssertionException("Expected equal values");
		}

		/// <summary>
		/// Asserts that two specified objects are not equal given a specific comparer.
		/// </summary>
		public static void NotEqual<TValue>(TValue pLeft, TValue pRight, IEqualityComparer<TValue> pComparer, AssertionException pException = null) {
			if (pComparer == null) throw new ArgumentNullException(nameof(pComparer));
			if (IsEqual(pLeft, pRight, pComparer)) throw pException ?? new AssertionException("Expected unequal values");
		}

		/// <summary>
		/// Asserts that two specified objects are not equal given a specific comparer.
		/// </summary>
		public static void NotEqual(object pLeft, object pRight, IEqualityComparer pComparer, AssertionException pException = null) {
			if (pComparer == null) throw new ArgumentNullException(nameof(pComparer));
			if (IsEqual(pLeft, pRight, pComparer)) throw pException ?? new AssertionException("Expected unequal values");
		}

		/// <summary>
		/// Asserts that two specified objects are not equal.
		/// null and null are considered equal.
		/// The types must also be consistent in order to compare equal (so this assertion would be relatively useless if you were comparing sbyte with int, for example).
		/// </summary>
		public static void NotEqual(object pLeft, object pRight, AssertionException pException = null) {
			if (IsEqual(pLeft, pRight)) throw pException ?? new AssertionException("Expected unequal values");
		}
	}
}
