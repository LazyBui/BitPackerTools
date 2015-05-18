using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	public partial class AssertTest {
		[TestMethod]
		[TestCategory("Framework")]
		public void IsType() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsType<string>(null));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsType<int>("hello"));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsType<int?>(1));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsType<int>(1L));
			Assert.DoesNotThrow(() => Assert.IsType<int>(1));

			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsType(null, typeof(string)));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsType("hello", null));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsType("hello", typeof(int)));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsType(1, typeof(int?)));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsType(1L, typeof(int)));
			Assert.DoesNotThrow(() => Assert.IsType(1, typeof(int)));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void IsNotType() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsNotType<string>(null));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsNotType<int>(1));
			Assert.DoesNotThrow(() => Assert.IsNotType<int?>(1));
			Assert.DoesNotThrow(() => Assert.IsNotType<int>(1L));
			Assert.DoesNotThrow(() => Assert.IsNotType<int>("hello"));

			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsNotType(null, typeof(string)));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsNotType("hello", null));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsNotType(1, typeof(int)));
			Assert.DoesNotThrow(() => Assert.IsNotType(1, typeof(int?)));
			Assert.DoesNotThrow(() => Assert.IsNotType(1L, typeof(int)));
			Assert.DoesNotThrow(() => Assert.IsNotType("hello", typeof(int)));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void IsAssignableFromType() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsAssignableFromType<string>(null));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsAssignableFromType<int>("hello"));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsAssignableFromType<int>(1L));
			Assert.DoesNotThrow(() => Assert.IsAssignableFromType<int>(new int?(1)));
			Assert.DoesNotThrow(() => Assert.IsAssignableFromType<int>(1));

			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsAssignableFromType(null, typeof(string)));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsAssignableFromType("hello", null));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsAssignableFromType("hello", typeof(int)));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsAssignableFromType(1L, typeof(int)));
			Assert.DoesNotThrow(() => Assert.IsAssignableFromType(new int?(1), typeof(int)));
			Assert.DoesNotThrow(() => Assert.IsAssignableFromType(1, typeof(int)));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void IsNotAssignableFromType() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsNotAssignableFromType<string>(null));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsNotAssignableFromType<int>(1));
			Assert.DoesNotThrow(() => Assert.IsNotAssignableFromType<long>(1));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsNotAssignableFromType<int>(new int?(1)));
			Assert.DoesNotThrow(() => Assert.IsNotAssignableFromType<int>("hello"));

			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsNotAssignableFromType(null, typeof(string)));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.IsNotAssignableFromType("hello", null));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsNotAssignableFromType(1, typeof(int)));
			Assert.DoesNotThrow(() => Assert.IsNotAssignableFromType(1, typeof(long)));
			Assert.ThrowsExact<AssertionException>(() => Assert.IsNotAssignableFromType(new int?(1), typeof(int)));
			Assert.DoesNotThrow(() => Assert.IsNotAssignableFromType("hello", typeof(int)));
		}
	}
}
