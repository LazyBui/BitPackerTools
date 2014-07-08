using System;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that a floating-point value is the Not a Number value.
		/// </summary>
		public static void IsNaN(float pValue, AssertionException pException = null) {
			if (!float.IsNaN(pValue)) throw pException ?? new AssertionException("pValue was not NaN");
		}

		/// <summary>
		/// Asserts that a floating-point value is the Not a Number value.
		/// </summary>
		public static void IsNaN(double pValue, AssertionException pException = null) {
			if (!double.IsNaN(pValue)) throw pException ?? new AssertionException("pValue was not NaN");
		}

		/// <summary>
		/// Asserts that a floating-point value is not the Not a Number value.
		/// </summary>
		public static void IsNotNaN(float pValue, AssertionException pException = null) {
			if (float.IsNaN(pValue)) throw pException ?? new AssertionException("pValue was NaN");
		}

		/// <summary>
		/// Asserts that a floating-point value is not the Not a Number value.
		/// </summary>
		public static void IsNotNaN(double pValue, AssertionException pException = null) {
			if (double.IsNaN(pValue)) throw pException ?? new AssertionException("pValue was NaN");
		}

		/// <summary>
		/// Asserts that a floating-point value is either a positive or negative infinity value.
		/// </summary>
		public static void IsInfinity(float pValue, AssertionException pException = null) {
			if (!float.IsInfinity(pValue)) throw pException ?? new AssertionException("pValue was not infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is either a positive or negative infinity value.
		/// </summary>
		public static void IsInfinity(double pValue, AssertionException pException = null) {
			if (!double.IsInfinity(pValue)) throw pException ?? new AssertionException("pValue was not infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is not a positive or negative infinity value.
		/// </summary>
		public static void IsNotInfinity(float pValue, AssertionException pException = null) {
			if (float.IsInfinity(pValue)) throw pException ?? new AssertionException("pValue was infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is not a positive or negative infinity value.
		/// </summary>
		public static void IsNotInfinity(double pValue, AssertionException pException = null) {
			if (double.IsInfinity(pValue)) throw pException ?? new AssertionException("pValue was infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a positive infinity value.
		/// </summary>
		public static void IsPositiveInfinity(float pValue, AssertionException pException = null) {
			if (!float.IsPositiveInfinity(pValue)) throw pException ?? new AssertionException("pValue was not +infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a positive infinity value.
		/// </summary>
		public static void IsPositiveInfinity(double pValue, AssertionException pException = null) {
			if (!double.IsPositiveInfinity(pValue)) throw pException ?? new AssertionException("pValue was not +infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a not positive infinity value.
		/// </summary>
		public static void IsNotPositiveInfinity(float pValue, AssertionException pException = null) {
			if (float.IsPositiveInfinity(pValue)) throw pException ?? new AssertionException("pValue was +infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a not positive infinity value.
		/// </summary>
		public static void IsNotPositiveInfinity(double pValue, AssertionException pException = null) {
			if (double.IsPositiveInfinity(pValue)) throw pException ?? new AssertionException("pValue was +infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a negative infinity value.
		/// </summary>
		public static void IsNegativeInfinity(float pValue, AssertionException pException = null) {
			if (!float.IsNegativeInfinity(pValue)) throw pException ?? new AssertionException("pValue was not -infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a negative infinity value.
		/// </summary>
		public static void IsNegativeInfinity(double pValue, AssertionException pException = null) {
			if (!double.IsNegativeInfinity(pValue)) throw pException ?? new AssertionException("pValue was not -infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is not a negative infinity value.
		/// </summary>
		public static void IsNotNegativeInfinity(float pValue, AssertionException pException = null) {
			if (float.IsNegativeInfinity(pValue)) throw pException ?? new AssertionException("pValue was -infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is not a negative infinity value.
		/// </summary>
		public static void IsNotNegativeInfinity(double pValue, AssertionException pException = null) {
			if (double.IsNegativeInfinity(pValue)) throw pException ?? new AssertionException("pValue was -infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is not an infinity value or a Not a Number value.
		/// </summary>
		public static void IsFloatValue(float pValue, AssertionException pException = null) {
			if (float.IsInfinity(pValue) || float.IsNaN(pValue)) throw pException ?? new AssertionException("pValue was not a floating point value");
		}

		/// <summary>
		/// Asserts that a floating-point value is not an infinity value or a Not a Number value.
		/// </summary>
		public static void IsFloatValue(double pValue, AssertionException pException = null) {
			if (double.IsInfinity(pValue) || double.IsNaN(pValue)) throw pException ?? new AssertionException("pValue was not a floating point value");
		}

		/// <summary>
		/// Asserts that a floating-point value is either an infinity value or a Not a Number value.
		/// </summary>
		public static void IsNotFloatValue(float pValue, AssertionException pException = null) {
			if (!float.IsInfinity(pValue) && !float.IsNaN(pValue)) throw pException ?? new AssertionException("pValue was a floating point value");
		}

		/// <summary>
		/// Asserts that a floating-point value is either an infinity value or a Not a Number value.
		/// </summary>
		public static void IsNotFloatValue(double pValue, AssertionException pException = null) {
			if (!double.IsInfinity(pValue) && !double.IsNaN(pValue)) throw pException ?? new AssertionException("pValue was a floating point value");
		}

		/// <summary>
		/// Asserts that a floating-point value is within an delta of another floating point value.
		/// </summary>
		public static void IsWithinDelta(float pValue1, float pValue2, float pDelta, AssertionException pException = null) {
			if (Math.Abs(pValue1 - pValue2) > pDelta) throw pException ?? new AssertionException("pValue was a floating point value");
		}

		/// <summary>
		/// Asserts that a floating-point value is within an delta of another floating point value.
		/// </summary>
		public static void IsWithinDelta(double pValue1, double pValue2, double pDelta, AssertionException pException = null) {
			if (Math.Abs(pValue1 - pValue2) > pDelta) throw pException ?? new AssertionException("pValue was a floating point value");
		}
	}
}
