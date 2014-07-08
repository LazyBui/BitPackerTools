using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	public partial class AssertTest {
		private class ExceptionTest : Exception { }
		private class DerivedExceptionTest : ExceptionTest { }

		[TestMethod]
		public void Fail() {
			Assert.ThrowsExact<AssertionException>(() => Assert.Fail());
		}

		[TestMethod]
		public void True() {
			Assert.DoesNotThrow(() => Assert.True(true));
			Assert.ThrowsExact<AssertionException>(() => Assert.True(false));
		}

		[TestMethod]
		public void False() {
			Assert.DoesNotThrow(() => Assert.False(false));
			Assert.ThrowsExact<AssertionException>(() => Assert.False(true));
		}

		[TestMethod]
		public void Throws() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.Throws<AssertionException>(null));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.Throws(typeof(AssertionException), null));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.Throws(null, () => { }));
			Assert.ThrowsExact<ArgumentException>(() => Assert.Throws(typeof(AssertTest), () => { }));
			Assert.DoesNotThrow(() => Assert.Throws(typeof(Exception), () => { throw new Exception(); }));
			Assert.DoesNotThrow(() => Assert.Throws(typeof(DerivedExceptionTest), () => { throw new DerivedExceptionTest(); }));
			Assert.DoesNotThrow(() => Assert.Throws(typeof(ExceptionTest), () => { throw new DerivedExceptionTest(); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.Throws(typeof(DerivedExceptionTest), () => { throw new ExceptionTest(); }));

			Assert.DoesNotThrow(() => Assert.Throws<ArgumentException>(() => { throw new ArgumentException("Testing"); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.Throws<ArgumentException>(() => { }));
			Assert.ThrowsExact<AssertionException>(() => Assert.Throws<NotSupportedException>(() => { throw new ArgumentNullException(); }));

			Assert.ThrowsExact<AssertionException>(() => Assert.Throws(typeof(NotSupportedException), () => { throw new ArgumentNullException(); }));
			Assert.DoesNotThrow(() => Assert.Throws(typeof(ArgumentException), () => { throw new ArgumentNullException(); }));
			Assert.DoesNotThrow(() => Assert.Throws(typeof(ArgumentException), () => { throw new ArgumentException("Testing"); }));

			try {
				Assert.ThrowsExact<ArgumentNullException>(() => { throw new InvalidOperationException("Testing"); });
			}
			catch (AssertionException e) {
				if (e.InnerException.GetType() != typeof(InvalidOperationException)) {
					// This should not occur
					throw;
				}
			}
			catch (Exception) {
				// This should not occur
				throw;
			}
		}

		[TestMethod]
		public void ThrowsExact() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.ThrowsExact<AssertionException>(null));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.ThrowsExact(typeof(AssertionException), null));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.ThrowsExact(null, () => { }));
			Assert.ThrowsExact<ArgumentException>(() => Assert.ThrowsExact(typeof(AssertTest), () => { }));
			Assert.DoesNotThrow(() => Assert.ThrowsExact(typeof(Exception), () => { throw new Exception(); }));
			Assert.DoesNotThrow(() => Assert.ThrowsExact(typeof(DerivedExceptionTest), () => { throw new DerivedExceptionTest(); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsExact(typeof(ExceptionTest), () => { throw new DerivedExceptionTest(); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsExact(typeof(DerivedExceptionTest), () => { throw new ExceptionTest(); }));

			Assert.DoesNotThrow(() => Assert.ThrowsExact<ArgumentException>(() => { throw new ArgumentException("Testing"); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsExact<ArgumentException>(() => { }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsExact<NotSupportedException>(() => { throw new ArgumentNullException(); }));

			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsExact(typeof(NotSupportedException), () => { throw new ArgumentNullException(); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsExact(typeof(ArgumentException), () => { throw new ArgumentNullException(); }));
			Assert.DoesNotThrow(() => Assert.ThrowsExact(typeof(ArgumentException), () => { throw new ArgumentException("Testing"); }));

			try {
				Assert.ThrowsExact<ArgumentNullException>(() => { throw new InvalidOperationException("Testing"); });
			}
			catch (AssertionException e) {
				if (e.InnerException.GetType() != typeof(InvalidOperationException)) {
					// This should not occur
					throw;
				}
			}
			catch (Exception) {
				// This should not occur
				throw;
			}
		}

		[TestMethod]
		public void DoesNotThrow() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.DoesNotThrow(null));
			Assert.ThrowsExact<AssertionException>(() => Assert.DoesNotThrow(() => { throw new ArgumentException("Testing"); }));
			Assert.DoesNotThrow(() => Assert.DoesNotThrow(() => { }));
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
