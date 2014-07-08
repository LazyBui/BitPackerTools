using System;
using System.Text.RegularExpressions;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that a string matches the regex pattern.
		/// </summary>
		public static void IsMatch(Regex pRegex, string pInput, AssertionException pException = null) {
			if (pRegex == null) throw new ArgumentNullException("pRegex");
			if (pInput == null) throw new ArgumentNullException("pInput");
			if (!pRegex.IsMatch(pInput)) throw pException ?? new AssertionException("Regex pattern was not matched");
		}

		/// <summary>
		/// Asserts that a string matches the regex pattern.
		/// </summary>
		public static void IsMatch(string pRegexPattern, string pInput, AssertionException pException = null) {
			if (pRegexPattern == null) throw new ArgumentNullException("pRegexPattern");
			if (pInput == null) throw new ArgumentNullException("pInput");
			if (!Regex.IsMatch(pInput, pRegexPattern)) throw pException ?? new AssertionException("Regex pattern was not matched");
		}

		/// <summary>
		/// Asserts that a string produces the specified count of matches to the regex pattern.
		/// </summary>
		public static void MatchCount(Regex pRegex, string pInput, int pCount, AssertionException pException = null) {
			if (pRegex == null) throw new ArgumentNullException("pRegex");
			if (pInput == null) throw new ArgumentNullException("pInput");
			if (pCount < 0) throw new ArgumentException("Negative pCount values are not valid");
			if (pRegex.Matches(pInput).Count != pCount) throw pException ?? new AssertionException("Regex pattern was not matched the specified number of times: {0}", pCount);
		}

		/// <summary>
		/// Asserts that a string produces the specified count of matches to the regex pattern.
		/// </summary>
		public static void MatchCount(string pRegexPattern, string pInput, int pCount, AssertionException pException = null) {
			if (pRegexPattern == null) throw new ArgumentNullException("pRegexPattern");
			if (pInput == null) throw new ArgumentNullException("pInput");
			if (pCount < 0) throw new ArgumentException("Negative pCount values are not valid");
			if (Regex.Matches(pInput, pRegexPattern).Count != pCount) throw pException ?? new AssertionException("Regex pattern was not matched the specified number of times: {0}", pCount);
		}

		/// <summary>
		/// Asserts that a string does not match the regex pattern.
		/// </summary>
		public static void IsNotMatch(Regex pRegex, string pInput, AssertionException pException = null) {
			if (pRegex == null) throw new ArgumentNullException("pRegex");
			if (pInput == null) throw new ArgumentNullException("pInput");
			if (pRegex.IsMatch(pInput)) throw pException ?? new AssertionException("Regex pattern matched");
		}

		/// <summary>
		/// Asserts that a string does not match the regex pattern.
		/// </summary>
		public static void IsNotMatch(string pRegexPattern, string pInput, AssertionException pException = null) {
			if (pRegexPattern == null) throw new ArgumentNullException("pRegexPattern");
			if (pInput == null) throw new ArgumentNullException("pInput");
			if (Regex.IsMatch(pInput, pRegexPattern)) throw pException ?? new AssertionException("Regex pattern matched");
		}
	}
}
