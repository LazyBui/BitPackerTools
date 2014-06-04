using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class AssertTest {
		[TestMethod]
		public void True() {
			Assert.DoesNotThrow(() => Assert.True(true));
			Assert.Throws<AssertionException>(() => Assert.True(false));

			Assert.Throws<ArgumentNullException>(() => Assert.True(null));
			Assert.Throws<ArgumentException>(() => Assert.True(new bool[] { }));
			Assert.DoesNotThrow(() => Assert.True(new[] { true, true }));
			Assert.Throws<AssertionException>(() => Assert.True(new[] { true, false }));
			Assert.Throws<AssertionException>(() => Assert.True(new[] { false, false }));
		}

		[TestMethod]
		public void False() {
			Assert.DoesNotThrow(() => Assert.False(false));
			Assert.Throws<AssertionException>(() => Assert.False(true));

			Assert.Throws<ArgumentNullException>(() => Assert.False(null));
			Assert.Throws<ArgumentException>(() => Assert.False(new bool[] { }));
			Assert.DoesNotThrow(() => Assert.False(new[] { false, false }));
			Assert.Throws<AssertionException>(() => Assert.False(new[] { true, false }));
			Assert.Throws<AssertionException>(() => Assert.False(new[] { true, true }));
		}

		[TestMethod]
		public void Null() {
			Assert.DoesNotThrow(() => Assert.Null(null));
			Assert.Throws<AssertionException>(() => Assert.Null(new object()));
		}

		[TestMethod]
		public void NotNull() {
			Assert.DoesNotThrow(() => Assert.NotNull(new object()));
			Assert.Throws<AssertionException>(() => Assert.NotNull(null));
		}

		[TestMethod]
		public void Throws() {
			try {
				Assert.Throws<ArgumentException>(() => { throw new ArgumentException("Testing"); });
			}
			catch (Exception) {
				// This should not occur
				throw;
			}

			try {
				Assert.Throws<ArgumentNullException>(() => { throw new ArgumentException("Testing"); });
			}
			catch (AssertionException e) {
				if (e.InnerException.GetType() != typeof(ArgumentException)) {
					// This should not occur
					throw;
				}
			}
			catch (Exception) {
				// This should not occur
				throw;
			}

			bool thrown = false;
			try {
				Assert.Throws<ArgumentException>(() => { });
			}
			catch (AssertionException) {
				thrown = true;
			}
			catch (Exception) {
				// This should not occur
				throw;
			}
			if (!thrown) throw new AssertionException("Throw did not throw when it should");
		}

		[TestMethod]
		public void DoesNotThrow() {
			bool thrown = false;
			try {
				Assert.DoesNotThrow(() => { throw new ArgumentException("Testing"); });
			}
			catch (Exception) {
				thrown = true;
			}
			if (!thrown) throw new AssertionException("Should have thrown, test failure");

			try {
				Assert.DoesNotThrow(() => { });
			}
			catch (Exception) {
				throw new AssertionException("Should not have thrown, test failure");
			}
		}

		[TestMethod]
		public void IsType() {
			Assert.Throws<AssertionException>(() => Assert.IsType<int>("hello"));
			Assert.Throws<ArgumentNullException>(() => Assert.IsType<string>(null));
			Assert.DoesNotThrow(() => Assert.IsType<int>(1));
		}

		[TestMethod]
		public void IsNotType() {
			Assert.Throws<ArgumentNullException>(() => Assert.IsType<string>(null));
			Assert.Throws<AssertionException>(() => Assert.IsNotType<int>(1));
			Assert.DoesNotThrow(() => Assert.IsNotType<int>("hello"));
		}

		[TestMethod]
		public void GreaterThan() {
			int minValue = 1;
			int value = 2;
			Assert.Throws<ArgumentNullException>(() => Assert.GreaterThan<string>(null, "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.GreaterThan<string>("a", null));
			Assert.DoesNotThrow(() => Assert.GreaterThan(minValue, value));
			Assert.Throws<AssertionException>(() => Assert.GreaterThan(minValue, minValue));
			Assert.Throws<AssertionException>(() => Assert.GreaterThan(minValue, minValue - 5));
		}

		[TestMethod]
		public void GreaterThanEqual() {
			int minValue = 1;
			int value = 2;
			Assert.Throws<ArgumentNullException>(() => Assert.GreaterThanEqual<string>(null, "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.GreaterThanEqual<string>("a", null));
			Assert.DoesNotThrow(() => Assert.GreaterThanEqual(minValue, value));
			Assert.DoesNotThrow(() => Assert.GreaterThanEqual(minValue, minValue));
			Assert.Throws<AssertionException>(() => Assert.GreaterThanEqual(minValue, minValue - 5));
		}

		[TestMethod]
		public void LessThan() {
			int minValue = 1;
			int value = 2;
			Assert.Throws<ArgumentNullException>(() => Assert.LessThan<string>(null, "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.LessThan<string>("a", null));
			Assert.Throws<AssertionException>(() => Assert.LessThan(minValue, value));
			Assert.Throws<AssertionException>(() => Assert.LessThan(minValue, minValue));
			Assert.DoesNotThrow(() => Assert.LessThan(minValue, minValue - 5));
		}

		[TestMethod]
		public void LessThanEqual() {
			int minValue = 1;
			int value = 2;
			Assert.Throws<ArgumentNullException>(() => Assert.LessThanEqual<string>(null, "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.LessThanEqual<string>("a", null));
			Assert.Throws<AssertionException>(() => Assert.LessThanEqual(minValue, value));
			Assert.DoesNotThrow(() => Assert.LessThanEqual(minValue, minValue));
			Assert.DoesNotThrow(() => Assert.LessThanEqual(minValue, minValue - 5));
		}

		[TestMethod]
		public void InRangeEqual() {
			int minValue = 1;
			int maxValue = 10;
			Assert.Throws<ArgumentNullException>(() => Assert.InRangeEqual<string>(null, "z", "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.InRangeEqual<string>("a", null, "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.InRangeEqual<string>("a", "z", null));
			Assert.DoesNotThrow(() => Assert.InRangeEqual(minValue, maxValue, 2));
			Assert.DoesNotThrow(() => Assert.InRangeEqual(minValue, maxValue, minValue));
			Assert.DoesNotThrow(() => Assert.InRangeEqual(minValue, maxValue, maxValue));
			Assert.Throws<AssertionException>(() => Assert.InRangeEqual(minValue, maxValue, minValue - 5));
			Assert.Throws<AssertionException>(() => Assert.InRangeEqual(minValue, maxValue, maxValue + 5));
			Assert.Throws<AssertionException>(() => Assert.InRangeEqual(minValue, maxValue, minValue - 5));
			Assert.Throws<AssertionException>(() => Assert.InRangeEqual(minValue, maxValue, maxValue + 5));
		}

		[TestMethod]
		public void InRange() {
			int minValue = 1;
			int maxValue = 10;
			Assert.Throws<ArgumentNullException>(() => Assert.InRange<string>(null, "z", "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.InRange<string>("a", null, "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.InRange<string>("a", "z", null));
			Assert.DoesNotThrow(() => Assert.InRange(minValue, maxValue, 2));
			Assert.Throws<AssertionException>(() => Assert.InRange(minValue, maxValue, minValue));
			Assert.Throws<AssertionException>(() => Assert.InRange(minValue, maxValue, maxValue));
			Assert.Throws<AssertionException>(() => Assert.InRange(minValue, maxValue, minValue - 5));
			Assert.Throws<AssertionException>(() => Assert.InRange(minValue, maxValue, maxValue + 5));
			Assert.Throws<AssertionException>(() => Assert.InRange(minValue, maxValue, minValue - 5));
			Assert.Throws<AssertionException>(() => Assert.InRange(minValue, maxValue, maxValue + 5));
		}

		[TestMethod]
		public void NotInRangeEqual() {
			int minValue = 1;
			int maxValue = 10;
			Assert.Throws<ArgumentNullException>(() => Assert.NotInRangeEqual<string>(null, "z", "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.NotInRangeEqual<string>("a", null, "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.NotInRangeEqual<string>("a", "z", null));
			Assert.Throws<AssertionException>(() => Assert.NotInRangeEqual(minValue, maxValue, 2));
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
			Assert.Throws<ArgumentNullException>(() => Assert.NotInRange<string>(null, "z", "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.NotInRange<string>("a", null, "z"));
			Assert.Throws<ArgumentNullException>(() => Assert.NotInRange<string>("a", "z", null));
			Assert.Throws<AssertionException>(() => Assert.NotInRange(minValue, maxValue, 2));
			Assert.Throws<AssertionException>(() => Assert.NotInRange(minValue, maxValue, minValue));
			Assert.Throws<AssertionException>(() => Assert.NotInRange(minValue, maxValue, maxValue));
			Assert.DoesNotThrow(() => Assert.NotInRange(minValue, maxValue, minValue - 5));
			Assert.DoesNotThrow(() => Assert.NotInRange(minValue, maxValue, maxValue + 5));
			Assert.DoesNotThrow(() => Assert.NotInRange(minValue, maxValue, minValue - 5));
			Assert.DoesNotThrow(() => Assert.NotInRange(minValue, maxValue, maxValue + 5));
		}

		[TestMethod]
		public void Equal() {
			Assert.DoesNotThrow(() => Assert.Equal(1, 1));
			Assert.Throws<AssertionException>(() => Assert.Equal(1, 0));
			Assert.DoesNotThrow(() => Assert.Equal(null, null));
			Assert.Throws<AssertionException>(() => Assert.Equal(null as string, "hello"));
			Assert.Throws<AssertionException>(() => Assert.Equal(null as string, 0));

			Assert.Throws<AssertionException>(() => Assert.Equal(new[] { 1, 2 }, new[] { 1, 2, 3 }));
			Assert.Throws<AssertionException>(() => Assert.Equal(new[] { 1, 2, 4 }, new[] { 1, 2, 3 }));
			Assert.Throws<AssertionException>(() => Assert.Equal(new[] { 1, 2 }, null));
			Assert.Throws<AssertionException>(() => Assert.Equal(new[] { 1, 2 }, new List<int>() { 1, 2 }));
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
			Assert.Throws<AssertionException>(() => Assert.Equal(arr1, arr2));

			arr2 = new[] {
				new[] { 1, 2 },
				new[] { 2, 1 },
			};
			Assert.DoesNotThrow(() => Assert.Equal(arr1, arr2));
		}

		[TestMethod]
		public void NotEqual() {
			Assert.Throws<AssertionException>(() => Assert.NotEqual(1, 1));
			Assert.DoesNotThrow(() => Assert.NotEqual(1, 0));
			Assert.Throws<AssertionException>(() => Assert.NotEqual(null, null));
			Assert.DoesNotThrow(() => Assert.NotEqual(null, "hello"));
			Assert.DoesNotThrow(() => Assert.NotEqual(null as string, 0));

			Assert.DoesNotThrow(() => Assert.NotEqual(new[] { 1, 2 }, new[] { 1, 2, 3 }));
			Assert.DoesNotThrow(() => Assert.NotEqual(new[] { 1, 2, 4 }, new[] { 1, 2, 3 }));
			Assert.DoesNotThrow(() => Assert.NotEqual(new[] { 1, 2 }, null));
			Assert.DoesNotThrow(() => Assert.NotEqual(new[] { 1, 2 }, new List<int>() { 1, 2 }));
			Assert.Throws<AssertionException>(() => Assert.NotEqual(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }));

			var arr1 = new[] {
				new[] { 1, 2 },
				new[] { 2, 1 },
			};
			Assert.Throws<AssertionException>(() => Assert.NotEqual(arr1, arr1));

			var arr2 = new[] {
				new[] { 2, 1 },
				new[] { 1, 2 },
			};
			Assert.DoesNotThrow(() => Assert.NotEqual(arr1, arr2));

			arr2 = new[] {
				new[] { 1, 2 },
				new[] { 2, 1 },
			};
			Assert.Throws<AssertionException>(() => Assert.NotEqual(arr1, arr2));
		}

		[TestMethod]
		public void Same() {
			Assert.DoesNotThrow(() => Assert.Same<object>(null, null));
			Assert.DoesNotThrow(() => {
				object o = new object();
				Assert.Same(o, o);
			});
			Assert.Throws<AssertionException>(() => Assert.Same(new object(), null));
			Assert.Throws<AssertionException>(() => Assert.Same(new object(), new object()));
		}

		[TestMethod]
		public void NotSame() {
			Assert.Throws<AssertionException>(() => Assert.NotSame<object>(null, null));
			Assert.Throws<AssertionException>(() => {
				object o = new object();
				Assert.NotSame(o, o);
			});
			Assert.DoesNotThrow(() => Assert.NotSame(new object(), null));
			Assert.DoesNotThrow(() => Assert.NotSame(new object(), new object()));
		}

		[TestMethod]
		public void Empty() {
			Assert.Throws<ArgumentNullException>(() => Assert.Empty<int>(null));
			Assert.DoesNotThrow(() => Assert.Empty(new int[] { }));
			Assert.DoesNotThrow(() => Assert.Empty(string.Empty));
			Assert.Throws<AssertionException>(() => Assert.Empty(new[] { 1, 2, 3 }));
		}

		[TestMethod]
		public void NotEmpty() {
			Assert.Throws<ArgumentNullException>(() => Assert.NotEmpty<int>(null));
			Assert.Throws<AssertionException>(() => Assert.NotEmpty(new int[] { }));
			Assert.Throws<AssertionException>(() => Assert.NotEmpty(string.Empty));
			Assert.DoesNotThrow(() => Assert.NotEmpty(new[] { 1, 2, 3 }));
		}

		[TestMethod]
		public void All() {
			Assert.Throws<ArgumentNullException>(() => Assert.All(null as int[], v => v == 0));
			Assert.Throws<ArgumentNullException>(() => Assert.All(new int[] { }, null));
			Assert.Throws<ArgumentException>(() => Assert.All(new int[] { }, v => v == 0));

			Assert.Throws<AssertionException>(() => Assert.All(new int[] { 1, 2, 3, 4, 5 }, v => v <= 3));
			Assert.DoesNotThrow(() => Assert.All(new int[] { 1, 2, 3, 4, 5 }, v => v < 10));
			Assert.Throws<AssertionException>(() => Assert.All(new int[] { 1, 2, 3, 4, 5 }, v => v == 1));
		}

		[TestMethod]
		public void None() {
			Assert.Throws<ArgumentNullException>(() => Assert.None(null as int[], v => v == 0));
			Assert.Throws<ArgumentNullException>(() => Assert.None(new int[] { }, null));
			Assert.Throws<ArgumentException>(() => Assert.None(new int[] { }, v => v == 0));

			Assert.Throws<AssertionException>(() => Assert.None(new int[] { 1, 2, 3, 4, 5 }, v => v >= 3));
			Assert.DoesNotThrow(() => Assert.None(new int[] { 1, 2, 3, 4, 5 }, v => v > 10));
			Assert.Throws<AssertionException>(() => Assert.None(new int[] { 1, 2, 3, 4, 5 }, v => v == 1));
		}

		[TestMethod]
		public void Any() {
			Assert.Throws<ArgumentNullException>(() => Assert.Any(null as int[], v => v == 0));
			Assert.Throws<ArgumentNullException>(() => Assert.Any(new int[] { }, null));
			Assert.Throws<ArgumentException>(() => Assert.Any(new int[] { }, v => v == 0));

			Assert.DoesNotThrow(() => Assert.Any(new int[] { 1, 2, 3, 4, 5 }, v => v >= 3));
			Assert.DoesNotThrow(() => Assert.Any(new int[] { 1, 2, 3, 4, 5 }, v => v < 10));
			Assert.Throws<AssertionException>(() => Assert.Any(new int[] { 1, 2, 3, 4, 5 }, v => v > 10));
			Assert.DoesNotThrow(() => Assert.Any(new int[] { 1, 2, 3, 4, 5 }, v => v == 1));
		}

		[TestMethod]
		public void Only() {
			Assert.Throws<ArgumentNullException>(() => Assert.Only(null as int[], v => v == 0));
			Assert.Throws<ArgumentNullException>(() => Assert.Only(new int[] { }, null));
			Assert.Throws<ArgumentException>(() => Assert.Only(new int[] { }, v => v == 0));

			Assert.Throws<AssertionException>(() => Assert.Only(new int[] { 1, 2, 3, 4, 5 }, v => v >= 3));
			Assert.Throws<AssertionException>(() => Assert.Only(new int[] { 1, 2, 3, 4, 5 }, v => v < 10));
			Assert.Throws<AssertionException>(() => Assert.Only(new int[] { 1, 2, 3, 4, 5 }, v => v > 10));
			Assert.DoesNotThrow(() => Assert.Only(new int[] { 1, 2, 3, 4, 5 }, v => v == 1));
		}

		[TestMethod]
		public void Count() {
			Assert.Throws<ArgumentNullException>(() => Assert.Count(null as int[], 1, v => v == 0));
			Assert.Throws<ArgumentNullException>(() => Assert.Count(new int[] { }, 1, null));
			Assert.Throws<ArgumentException>(() => Assert.Count(new int[] { }, 1, v => v == 0));

			Assert.DoesNotThrow(() => Assert.Count(new int[] { 1, 2, 3, 4, 5 }, 3, v => v >= 3));
			Assert.DoesNotThrow(() => Assert.Count(new int[] { 1, 2, 3, 4, 5 }, 5, v => v < 10));
			Assert.Throws<AssertionException>(() => Assert.Count(new int[] { 1, 2, 3, 4, 5 }, 4, v => v < 10));
			Assert.Throws<AssertionException>(() => Assert.Count(new int[] { 1, 2, 3, 4, 5 }, 6, v => v < 10));
			Assert.Throws<AssertionException>(() => Assert.Count(new int[] { 1, 2, 3, 4, 5 }, 5, v => v > 10));
			Assert.Throws<AssertionException>(() => Assert.Count(new int[] { 1, 2, 3, 4, 5 }, 5, v => v == 1));
		}

		[TestMethod]
		public void Contains() {
			Assert.Throws<ArgumentNullException>(() => Assert.Contains<int>(null, 1));
			Assert.Throws<ArgumentException>(() => Assert.Contains<int>(new int[] { }, 1));
			Assert.Throws<AssertionException>(() => Assert.Contains(new int[] { 2 }, 1));
			Assert.DoesNotThrow(() => Assert.Contains(new int[] { 2 }, 2));
		}

		[TestMethod]
		public void DoesNotContain() {
			Assert.Throws<ArgumentNullException>(() => Assert.DoesNotContain<int>(null, 1));
			Assert.Throws<ArgumentException>(() => Assert.Contains<int>(new int[] { }, 1));
			Assert.DoesNotThrow(() => Assert.DoesNotContain(new int[] { 2 }, 1));
			Assert.Throws<AssertionException>(() => Assert.DoesNotContain(new int[] { 2 }, 2));
		}

		[TestMethod]
		public void ExceptionAttachments() {
			var exception = new AssertionException("Testing {0}", 123);

			try {
				Assert.DoesNotThrow(() => { throw new ArgumentException(); }, exception);
			}
			catch (AssertionException e) {
				Assert.Equal(e.Message, "Testing 123");
			}
		}
	}
}
