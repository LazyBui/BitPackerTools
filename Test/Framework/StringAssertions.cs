using System;
using System.Text.RegularExpressions;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that a string matches the regex pattern.
		/// </summary>
		public static void IsMatch(Regex regex, string input, AssertionException exception = null) {
			if (regex == null) throw new ArgumentNullException(nameof(regex));
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (!regex.IsMatch(input)) throw exception ?? new AssertionException("Regex pattern was not matched");
		}

		/// <summary>
		/// Asserts that a string matches the regex pattern.
		/// </summary>
		public static void IsMatch(string regexPattern, string input, AssertionException exception = null) {
			if (regexPattern == null) throw new ArgumentNullException(nameof(regexPattern));
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (!Regex.IsMatch(input, regexPattern)) throw exception ?? new AssertionException("Regex pattern was not matched");
		}

		/// <summary>
		/// Asserts that a string produces the specified count of matches to the regex pattern.
		/// </summary>
		public static void MatchCount(Regex regex, string input, int count, AssertionException exception = null) {
			if (regex == null) throw new ArgumentNullException(nameof(regex));
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (count < 0) throw new ArgumentException("Must be non-negative", nameof(count));
			if (regex.Matches(input).Count != count) throw exception ?? new AssertionException("Regex pattern was not matched the specified number of times: {0}", count);
		}

		/// <summary>
		/// Asserts that a string produces the specified count of matches to the regex pattern.
		/// </summary>
		public static void MatchCount(string regexPattern, string input, int count, AssertionException exception = null) {
			if (regexPattern == null) throw new ArgumentNullException(nameof(regexPattern));
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (count < 0) throw new ArgumentException("Must be non-negative", nameof(count));
			if (Regex.Matches(input, regexPattern).Count != count) throw exception ?? new AssertionException("Regex pattern was not matched the specified number of times: {0}", count);
		}

		/// <summary>
		/// Asserts that a string does not match the regex pattern.
		/// </summary>
		public static void IsNotMatch(Regex regex, string input, AssertionException exception = null) {
			if (regex == null) throw new ArgumentNullException(nameof(regex));
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (regex.IsMatch(input)) throw exception ?? new AssertionException("Regex pattern matched");
		}

		/// <summary>
		/// Asserts that a string does not match the regex pattern.
		/// </summary>
		public static void IsNotMatch(string regexPattern, string input, AssertionException exception = null) {
			if (regexPattern == null) throw new ArgumentNullException(nameof(regexPattern));
			if (input == null) throw new ArgumentNullException(nameof(input));
			if (Regex.IsMatch(input, regexPattern)) throw exception ?? new AssertionException("Regex pattern matched");
		}
	}
}
