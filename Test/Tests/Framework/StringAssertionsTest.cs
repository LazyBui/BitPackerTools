using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	public partial class AssertTest {
		[TestMethod]
		[TestCategory("Framework")]
		public void IsMatch() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsMatch(null as Regex, string.Empty));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsMatch(new Regex(string.Empty), null));

			Assert.ThrowsExact<AssertionException>(() => Assert.IsMatch("abc", "cba"));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsMatch(new Regex("abc"), "cba"));
			Assert.DoesNotThrow(() => Assert.IsMatch("abc", "abc"));
			Assert.DoesNotThrow(() => Assert.IsMatch(new Regex("abc"), "abc"));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void IsNotMatch() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsNotMatch(null as Regex, string.Empty));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsNotMatch(new Regex(string.Empty), null));

			Assert.DoesNotThrow(() => Assert.IsNotMatch("abc", "cba"));
			Assert.DoesNotThrow(() => Assert.IsNotMatch(new Regex("abc"), "cba"));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsNotMatch("abc", "abc"));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsNotMatch(new Regex("abc"), "abc"));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void MatchCount() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.MatchCount(null as Regex, string.Empty, 1));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.MatchCount(new Regex(string.Empty), null, 1));
			Assert.ThrowsExact<ArgumentException>(() => Assert.MatchCount(new Regex(string.Empty), string.Empty, -1));

			Assert.DoesNotThrow(() => Assert.MatchCount(new Regex("abc"), "abcabc", 2));
			Assert.ThrowsExact<AssertionException>(() => Assert.MatchCount(new Regex("abc"), "abcabc", 1));
			Assert.ThrowsExact<AssertionException>(() => Assert.MatchCount(new Regex("abc"), "abcabc", 3));
			Assert.ThrowsExact<AssertionException>(() => Assert.MatchCount(new Regex("abc"), "cba", 1));
			Assert.ThrowsExact<AssertionException>(() => Assert.MatchCount(new Regex("abc"), "cba", 2));
			Assert.DoesNotThrow(() => Assert.MatchCount(new Regex("abc"), "cba", 0));
			Assert.DoesNotThrow(() => Assert.MatchCount("abc", "abcabc", 2));
			Assert.ThrowsExact<AssertionException>(() => Assert.MatchCount("abc", "abcabc", 1));
			Assert.ThrowsExact<AssertionException>(() => Assert.MatchCount("abc", "abcabc", 3));
			Assert.ThrowsExact<AssertionException>(() => Assert.MatchCount("abc", "cba", 1));
			Assert.ThrowsExact<AssertionException>(() => Assert.MatchCount("abc", "cba", 2));
			Assert.DoesNotThrow(() => Assert.MatchCount("abc", "cba", 0));
		}
	}
}
