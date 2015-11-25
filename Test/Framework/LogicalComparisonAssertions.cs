using System;
using System.Collections;
using System.Collections.Generic;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that the specified value compares either greater or equal to a specified lower bound.
		/// </summary>
		public static void GreaterThanEqual<TValue>(TValue value, TValue lowerBound, AssertionException exception = null) where TValue : IComparable<TValue> {
			if (lowerBound == null) throw new ArgumentNullException(nameof(lowerBound));
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (lowerBound.CompareTo(value) > 0) throw exception ?? new AssertionException("Value fell below expected range: {0}", value);
		}

		/// <summary>
		/// Asserts that the specified value compares strictly greater to a specified lower bound.
		/// </summary>
		public static void GreaterThan<TValue>(TValue value, TValue lowerBound, AssertionException exception = null) where TValue : IComparable<TValue> {
			if (lowerBound == null) throw new ArgumentNullException(nameof(lowerBound));
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (lowerBound.CompareTo(value) >= 0) throw exception ?? new AssertionException("Value fell below expected range: {0}", value);
		}

		/// <summary>
		/// Asserts that the specified value compares either less or equal to a specified upper bound.
		/// </summary>
		public static void LessThanEqual<TValue>(TValue value, TValue upperBound, AssertionException exception = null) where TValue : IComparable<TValue> {
			if (upperBound == null) throw new ArgumentNullException(nameof(upperBound));
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (upperBound.CompareTo(value) < 0) throw exception ?? new AssertionException("Value fell above expected range: {0}", value);
		}

		/// <summary>
		/// Asserts that the specified value compares strictly less to a specified upper bound.
		/// </summary>
		public static void LessThan<TValue>(TValue value, TValue upperBound, AssertionException exception = null) where TValue : IComparable<TValue> {
			if (upperBound == null) throw new ArgumentNullException(nameof(upperBound));
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (upperBound.CompareTo(value) <= 0) throw exception ?? new AssertionException("Value fell above expected range: {0}", value);
		}

		/// <summary>
		/// Asserts that the specified value compares either greater or equal to a specified lower bound AND less or equal to a specified upper bound.
		/// </summary>
		public static void InRangeEqual<TValue>(TValue value, TValue lowerBound, TValue upperBound, AssertionException exception = null) where TValue : IComparable<TValue> {
			if (lowerBound == null) throw new ArgumentNullException(nameof(lowerBound));
			if (upperBound == null) throw new ArgumentNullException(nameof(upperBound));
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (lowerBound.CompareTo(value) > 0) throw exception ?? new AssertionException("Value fell below expected range: {0}", value);
			if (upperBound.CompareTo(value) < 0) throw exception ?? new AssertionException("Value fell above expected range: {0}", value);
		}

		/// <summary>
		/// Asserts that the specified value compares strictly greater to a specified lower bound AND strictly less to a specified upper bound.
		/// </summary>
		public static void InRange<TValue>(TValue value, TValue lowerBound, TValue upperBound, AssertionException exception = null) where TValue : IComparable<TValue> {
			if (lowerBound == null) throw new ArgumentNullException(nameof(lowerBound));
			if (upperBound == null) throw new ArgumentNullException(nameof(upperBound));
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (lowerBound.CompareTo(value) >= 0) throw exception ?? new AssertionException("Value fell below expected range: {0}", value);
			if (upperBound.CompareTo(value) <= 0) throw exception ?? new AssertionException("Value fell above expected range: {0}", value);
		}

		/// <summary>
		/// Asserts that the specified value compares either greater or equal to a specified upper bound AND less or equal to a specified lower bound.
		/// </summary>
		public static void NotInRangeEqual<TValue>(TValue value, TValue lowerBound, TValue upperBound, AssertionException exception = null) where TValue : IComparable<TValue> {
			if (lowerBound == null) throw new ArgumentNullException(nameof(lowerBound));
			if (upperBound == null) throw new ArgumentNullException(nameof(upperBound));
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (lowerBound.CompareTo(value) < 0 && upperBound.CompareTo(value) > 0) throw exception ?? new AssertionException("Value fell into range");
		}

		/// <summary>
		/// Asserts that the specified value compares strictly greater to a specified upper bound AND strictly less to a specified lower bound.
		/// </summary>
		public static void NotInRange<TValue>(TValue value, TValue lowerBound, TValue upperBound, AssertionException exception = null) where TValue : IComparable<TValue> {
			if (lowerBound == null) throw new ArgumentNullException(nameof(lowerBound));
			if (upperBound == null) throw new ArgumentNullException(nameof(upperBound));
			if (value == null) throw new ArgumentNullException(nameof(value));
			if (lowerBound.CompareTo(value) <= 0 && upperBound.CompareTo(value) >= 0) throw exception ?? new AssertionException("Value fell into range");
		}

		/// <summary>
		/// Asserts that two specified objects are equal given a specific comparer.
		/// </summary>
		public static void Equal<TValue>(TValue left, TValue right, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (!IsEqual(left, right, comparer)) throw exception ?? new AssertionException("Expected equal values");
		}

		/// <summary>
		/// Asserts that two specified objects are equal given a specific comparer.
		/// </summary>
		public static void Equal(object left, object right, IEqualityComparer comparer, AssertionException exception = null) {
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (!IsEqual(left, right, comparer)) throw exception ?? new AssertionException("Expected equal values");
		}

		/// <summary>
		/// Asserts that two specified objects are equal.
		/// null and null are considered equal.
		/// The types must also be consistent in order to compare equal (so you cannot compare sbyte with int, for example).
		/// </summary>
		public static void Equal(object left, object right, AssertionException exception = null) {
			if (!IsEqual(left, right)) throw exception ?? new AssertionException("Expected equal values");
		}

		/// <summary>
		/// Asserts that two specified objects are not equal given a specific comparer.
		/// </summary>
		public static void NotEqual<TValue>(TValue left, TValue right, IEqualityComparer<TValue> comparer, AssertionException exception = null) {
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (IsEqual(left, right, comparer)) throw exception ?? new AssertionException("Expected unequal values");
		}

		/// <summary>
		/// Asserts that two specified objects are not equal given a specific comparer.
		/// </summary>
		public static void NotEqual(object left, object right, IEqualityComparer comparer, AssertionException exception = null) {
			if (comparer == null) throw new ArgumentNullException(nameof(comparer));
			if (IsEqual(left, right, comparer)) throw exception ?? new AssertionException("Expected unequal values");
		}

		/// <summary>
		/// Asserts that two specified objects are not equal.
		/// null and null are considered equal.
		/// The types must also be consistent in order to compare equal (so this assertion would be relatively useless if you were comparing sbyte with int, for example).
		/// </summary>
		public static void NotEqual(object left, object right, AssertionException exception = null) {
			if (IsEqual(left, right)) throw exception ?? new AssertionException("Expected unequal values");
		}
	}
}
