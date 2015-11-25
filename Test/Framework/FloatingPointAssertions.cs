using System;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that a floating-point value is the Not a Number value.
		/// </summary>
		public static void IsNaN(float value, AssertionException exception = null) {
			if (!float.IsNaN(value)) throw exception ?? new AssertionException("value was not NaN");
		}

		/// <summary>
		/// Asserts that a floating-point value is the Not a Number value.
		/// </summary>
		public static void IsNaN(double value, AssertionException exception = null) {
			if (!double.IsNaN(value)) throw exception ?? new AssertionException("value was not NaN");
		}

		/// <summary>
		/// Asserts that a floating-point value is not the Not a Number value.
		/// </summary>
		public static void IsNotNaN(float value, AssertionException exception = null) {
			if (float.IsNaN(value)) throw exception ?? new AssertionException("value was NaN");
		}

		/// <summary>
		/// Asserts that a floating-point value is not the Not a Number value.
		/// </summary>
		public static void IsNotNaN(double value, AssertionException exception = null) {
			if (double.IsNaN(value)) throw exception ?? new AssertionException("value was NaN");
		}

		/// <summary>
		/// Asserts that a floating-point value is either a positive or negative infinity value.
		/// </summary>
		public static void IsInfinity(float value, AssertionException exception = null) {
			if (!float.IsInfinity(value)) throw exception ?? new AssertionException("value was not infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is either a positive or negative infinity value.
		/// </summary>
		public static void IsInfinity(double value, AssertionException exception = null) {
			if (!double.IsInfinity(value)) throw exception ?? new AssertionException("value was not infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is not a positive or negative infinity value.
		/// </summary>
		public static void IsNotInfinity(float value, AssertionException exception = null) {
			if (float.IsInfinity(value)) throw exception ?? new AssertionException("value was infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is not a positive or negative infinity value.
		/// </summary>
		public static void IsNotInfinity(double value, AssertionException exception = null) {
			if (double.IsInfinity(value)) throw exception ?? new AssertionException("value was infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a positive infinity value.
		/// </summary>
		public static void IsPositiveInfinity(float value, AssertionException exception = null) {
			if (!float.IsPositiveInfinity(value)) throw exception ?? new AssertionException("value was not +infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a positive infinity value.
		/// </summary>
		public static void IsPositiveInfinity(double value, AssertionException exception = null) {
			if (!double.IsPositiveInfinity(value)) throw exception ?? new AssertionException("value was not +infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a not positive infinity value.
		/// </summary>
		public static void IsNotPositiveInfinity(float value, AssertionException exception = null) {
			if (float.IsPositiveInfinity(value)) throw exception ?? new AssertionException("value was +infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a not positive infinity value.
		/// </summary>
		public static void IsNotPositiveInfinity(double value, AssertionException exception = null) {
			if (double.IsPositiveInfinity(value)) throw exception ?? new AssertionException("value was +infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a negative infinity value.
		/// </summary>
		public static void IsNegativeInfinity(float value, AssertionException exception = null) {
			if (!float.IsNegativeInfinity(value)) throw exception ?? new AssertionException("value was not -infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is a negative infinity value.
		/// </summary>
		public static void IsNegativeInfinity(double value, AssertionException exception = null) {
			if (!double.IsNegativeInfinity(value)) throw exception ?? new AssertionException("value was not -infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is not a negative infinity value.
		/// </summary>
		public static void IsNotNegativeInfinity(float value, AssertionException exception = null) {
			if (float.IsNegativeInfinity(value)) throw exception ?? new AssertionException("value was -infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is not a negative infinity value.
		/// </summary>
		public static void IsNotNegativeInfinity(double value, AssertionException exception = null) {
			if (double.IsNegativeInfinity(value)) throw exception ?? new AssertionException("value was -infinity");
		}

		/// <summary>
		/// Asserts that a floating-point value is not an infinity value or a Not a Number value.
		/// </summary>
		public static void IsFloatValue(float value, AssertionException exception = null) {
			if (float.IsInfinity(value) || float.IsNaN(value)) throw exception ?? new AssertionException("value was not a floating point value");
		}

		/// <summary>
		/// Asserts that a floating-point value is not an infinity value or a Not a Number value.
		/// </summary>
		public static void IsFloatValue(double value, AssertionException exception = null) {
			if (double.IsInfinity(value) || double.IsNaN(value)) throw exception ?? new AssertionException("value was not a floating point value");
		}

		/// <summary>
		/// Asserts that a floating-point value is either an infinity value or a Not a Number value.
		/// </summary>
		public static void IsNotFloatValue(float value, AssertionException exception = null) {
			if (!float.IsInfinity(value) && !float.IsNaN(value)) throw exception ?? new AssertionException("value was a floating point value");
		}

		/// <summary>
		/// Asserts that a floating-point value is either an infinity value or a Not a Number value.
		/// </summary>
		public static void IsNotFloatValue(double value, AssertionException exception = null) {
			if (!double.IsInfinity(value) && !double.IsNaN(value)) throw exception ?? new AssertionException("value was a floating point value");
		}

		/// <summary>
		/// Asserts that a floating-point value is within an delta of another floating point value.
		/// </summary>
		public static void IsWithinDelta(float value1, float value2, float delta, AssertionException exception = null) {
			if (Math.Abs(value1 - value2) > delta) throw exception ?? new AssertionException("value was a floating point value");
		}

		/// <summary>
		/// Asserts that a floating-point value is within an delta of another floating point value.
		/// </summary>
		public static void IsWithinDelta(double value1, double value2, double delta, AssertionException exception = null) {
			if (Math.Abs(value1 - value2) > delta) throw exception ?? new AssertionException("value was a floating point value");
		}
	}
}
