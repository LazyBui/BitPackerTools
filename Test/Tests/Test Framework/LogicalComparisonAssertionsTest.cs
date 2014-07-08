using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	public partial class AssertTest {
		[TestMethod]
		public void GreaterThan() {
			int minValue = 1;
			int value = 2;
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.GreaterThan<string>(null, "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.GreaterThan<string>("a", null));
			Assert.DoesNotThrow(() => Assert.GreaterThan(minValue, value));
			Assert.ThrowsExact<AssertionException>(() => Assert.GreaterThan(minValue, minValue));
			Assert.ThrowsExact<AssertionException>(() => Assert.GreaterThan(minValue, minValue - 5));
		}

		[TestMethod]
		public void GreaterThanEqual() {
			int minValue = 1;
			int value = 2;
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.GreaterThanEqual<string>(null, "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.GreaterThanEqual<string>("a", null));
			Assert.DoesNotThrow(() => Assert.GreaterThanEqual(minValue, value));
			Assert.DoesNotThrow(() => Assert.GreaterThanEqual(minValue, minValue));
			Assert.ThrowsExact<AssertionException>(() => Assert.GreaterThanEqual(minValue, minValue - 5));
		}

		[TestMethod]
		public void LessThan() {
			int minValue = 1;
			int value = 2;
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.LessThan<string>(null, "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.LessThan<string>("a", null));
			Assert.ThrowsExact<AssertionException>(() => Assert.LessThan(minValue, value));
			Assert.ThrowsExact<AssertionException>(() => Assert.LessThan(minValue, minValue));
			Assert.DoesNotThrow(() => Assert.LessThan(minValue, minValue - 5));
		}

		[TestMethod]
		public void LessThanEqual() {
			int minValue = 1;
			int value = 2;
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.LessThanEqual<string>(null, "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.LessThanEqual<string>("a", null));
			Assert.ThrowsExact<AssertionException>(() => Assert.LessThanEqual(minValue, value));
			Assert.DoesNotThrow(() => Assert.LessThanEqual(minValue, minValue));
			Assert.DoesNotThrow(() => Assert.LessThanEqual(minValue, minValue - 5));
		}

		[TestMethod]
		public void InRangeEqual() {
			int minValue = 1;
			int maxValue = 10;
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.InRangeEqual<string>(null, "z", "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.InRangeEqual<string>("a", null, "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.InRangeEqual<string>("a", "z", null));
			Assert.DoesNotThrow(() => Assert.InRangeEqual(minValue, maxValue, 2));
			Assert.DoesNotThrow(() => Assert.InRangeEqual(minValue, maxValue, minValue));
			Assert.DoesNotThrow(() => Assert.InRangeEqual(minValue, maxValue, maxValue));
			Assert.ThrowsExact<AssertionException>(() => Assert.InRangeEqual(minValue, maxValue, minValue - 5));
			Assert.ThrowsExact<AssertionException>(() => Assert.InRangeEqual(minValue, maxValue, maxValue + 5));
			Assert.ThrowsExact<AssertionException>(() => Assert.InRangeEqual(minValue, maxValue, minValue - 5));
			Assert.ThrowsExact<AssertionException>(() => Assert.InRangeEqual(minValue, maxValue, maxValue + 5));
		}

		[TestMethod]
		public void InRange() {
			int minValue = 1;
			int maxValue = 10;
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.InRange<string>(null, "z", "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.InRange<string>("a", null, "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.InRange<string>("a", "z", null));
			Assert.DoesNotThrow(() => Assert.InRange(minValue, maxValue, 2));
			Assert.ThrowsExact<AssertionException>(() => Assert.InRange(minValue, maxValue, minValue));
			Assert.ThrowsExact<AssertionException>(() => Assert.InRange(minValue, maxValue, maxValue));
			Assert.ThrowsExact<AssertionException>(() => Assert.InRange(minValue, maxValue, minValue - 5));
			Assert.ThrowsExact<AssertionException>(() => Assert.InRange(minValue, maxValue, maxValue + 5));
			Assert.ThrowsExact<AssertionException>(() => Assert.InRange(minValue, maxValue, minValue - 5));
			Assert.ThrowsExact<AssertionException>(() => Assert.InRange(minValue, maxValue, maxValue + 5));
		}

		[TestMethod]
		public void NotInRangeEqual() {
			int minValue = 1;
			int maxValue = 10;
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.NotInRangeEqual<string>(null, "z", "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.NotInRangeEqual<string>("a", null, "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.NotInRangeEqual<string>("a", "z", null));
			Assert.ThrowsExact<AssertionException>(() => Assert.NotInRangeEqual(minValue, maxValue, 2));
			Assert.DoesNotThrow(() => Assert.NotInRangeEqual(minValue, maxValue, minValue));
			Assert.DoesNotThrow(() => Assert.NotInRangeEqual(minValue, maxValue, maxValue));
			Assert.DoesNotThrow(() => Assert.NotInRangeEqual(minValue, maxValue, minValue - 5));
			Assert.DoesNotThrow(() => Assert.NotInRangeEqual(minValue, maxValue, maxValue + 5));
			Assert.DoesNotThrow(() => Assert.NotInRangeEqual(minValue, maxValue, minValue - 5));
			Assert.DoesNotThrow(() => Assert.NotInRangeEqual(minValue, maxValue, maxValue + 5));
		}

		[TestMethod]
		public void NotInRange() {
			int minValue = 1;
			int maxValue = 10;
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.NotInRange<string>(null, "z", "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.NotInRange<string>("a", null, "z"));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.NotInRange<string>("a", "z", null));
			Assert.ThrowsExact<AssertionException>(() => Assert.NotInRange(minValue, maxValue, 2));
			Assert.ThrowsExact<AssertionException>(() => Assert.NotInRange(minValue, maxValue, minValue));
			Assert.ThrowsExact<AssertionException>(() => Assert.NotInRange(minValue, maxValue, maxValue));
			Assert.DoesNotThrow(() => Assert.NotInRange(minValue, maxValue, minValue - 5));
			Assert.DoesNotThrow(() => Assert.NotInRange(minValue, maxValue, maxValue + 5));
			Assert.DoesNotThrow(() => Assert.NotInRange(minValue, maxValue, minValue - 5));
			Assert.DoesNotThrow(() => Assert.NotInRange(minValue, maxValue, maxValue + 5));
		}

		[TestMethod]
		public void Equal() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.Equal(null as object, null as object, null as IEqualityComparer<object>));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.Equal(null as object, null as object, null as IEqualityComparer));

			Assert.DoesNotThrow(() => Assert.Equal(1, 1));
			Assert.ThrowsExact<AssertionException>(() => Assert.Equal(1, 0));
			Assert.DoesNotThrow(() => Assert.Equal(null, null));
			Assert.ThrowsExact<AssertionException>(() => Assert.Equal(null as string, "hello"));
			Assert.ThrowsExact<AssertionException>(() => Assert.Equal(null as string, 0));

			Assert.ThrowsExact<AssertionException>(() => Assert.Equal(new[] { 1, 2 }, new[] { 1, 2, 3 }));
			Assert.ThrowsExact<AssertionException>(() => Assert.Equal(new[] { 1, 2, 4 }, new[] { 1, 2, 3 }));
			Assert.ThrowsExact<AssertionException>(() => Assert.Equal(new[] { 1, 2 }, null));
			Assert.ThrowsExact<AssertionException>(() => Assert.Equal(new[] { 1, 2 }, new List<int>() { 1, 2 }));
			Assert.DoesNotThrow(() => Assert.Equal(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }));

			var arr1 = new[] {
				new[] { 1, 2 },
				new[] { 2, 1 },
			};
			Assert.DoesNotThrow(() => Assert.Equal(arr1, arr1));

			var arr2 = new[] {
				new[] { 2, 1 },
				new[] { 1, 2 },
			};
			Assert.ThrowsExact<AssertionException>(() => Assert.Equal(arr1, arr2));

			arr2 = new[] {
				new[] { 1, 2 },
				new[] { 2, 1 },
			};
			Assert.DoesNotThrow(() => Assert.Equal(arr1, arr2));

			Assert.DoesNotThrow(() => Assert.Equal("abc", "ABC", StringComparer.InvariantCultureIgnoreCase));
			Assert.DoesNotThrow(() => Assert.Equal("abc" as object, "ABC" as object, StringComparer.InvariantCultureIgnoreCase));
			Assert.ThrowsExact<AssertionException>(() => Assert.Equal("abc", "BBB", StringComparer.InvariantCultureIgnoreCase));
			Assert.ThrowsExact<AssertionException>(() => Assert.Equal("abc" as object, "BBB" as object, StringComparer.InvariantCultureIgnoreCase));
		}

		[TestMethod]
		public void NotEqual() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.NotEqual(null as object, null as object, null as IEqualityComparer<object>));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.NotEqual(null as object, null as object, null as IEqualityComparer));

			Assert.ThrowsExact<AssertionException>(() => Assert.NotEqual(1, 1));
			Assert.DoesNotThrow(() => Assert.NotEqual(1, 0));
			Assert.ThrowsExact<AssertionException>(() => Assert.NotEqual(null, null));
			Assert.DoesNotThrow(() => Assert.NotEqual(null, "hello"));
			Assert.DoesNotThrow(() => Assert.NotEqual(null as string, 0));

			Assert.DoesNotThrow(() => Assert.NotEqual(new[] { 1, 2 }, new[] { 1, 2, 3 }));
			Assert.DoesNotThrow(() => Assert.NotEqual(new[] { 1, 2, 4 }, new[] { 1, 2, 3 }));
			Assert.DoesNotThrow(() => Assert.NotEqual(new[] { 1, 2 }, null));
			Assert.DoesNotThrow(() => Assert.NotEqual(new[] { 1, 2 }, new List<int>() { 1, 2 }));
			Assert.ThrowsExact<AssertionException>(() => Assert.NotEqual(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }));

			var arr1 = new[] {
				new[] { 1, 2 },
				new[] { 2, 1 },
			};
			Assert.ThrowsExact<AssertionException>(() => Assert.NotEqual(arr1, arr1));

			var arr2 = new[] {
				new[] { 2, 1 },
				new[] { 1, 2 },
			};
			Assert.DoesNotThrow(() => Assert.NotEqual(arr1, arr2));

			arr2 = new[] {
				new[] { 1, 2 },
				new[] { 2, 1 },
			};
			Assert.ThrowsExact<AssertionException>(() => Assert.NotEqual(arr1, arr2));

			Assert.ThrowsExact<AssertionException>(() => Assert.NotEqual("abc", "ABC", StringComparer.InvariantCultureIgnoreCase));
			Assert.ThrowsExact<AssertionException>(() => Assert.NotEqual("abc" as object, "ABC" as object, StringComparer.InvariantCultureIgnoreCase));
			Assert.DoesNotThrow(() => Assert.NotEqual("abc", "BBB", StringComparer.InvariantCultureIgnoreCase));
			Assert.DoesNotThrow(() => Assert.NotEqual("abc" as object, "BBB" as object, StringComparer.InvariantCultureIgnoreCase));
		}
	}
}
