using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	public partial class AssertTest {
		private class ExceptionTest : Exception {
			public ExceptionTest() : base() { }
			public ExceptionTest(Exception pInnerException) : base(string.Empty, pInnerException) { }
		}
		private class DerivedExceptionTest : ExceptionTest {
			public DerivedExceptionTest() : base() { }
			public DerivedExceptionTest(Exception pInnerException) : base(pInnerException) { }
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void Fail() {
			Assert.ThrowsExact<AssertionException>(Assert.Failure);
			Assert.ThrowsExact<AssertionException>(() => Assert.Fail());
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void True() {
			Assert.DoesNotThrow(() => Assert.True(true));
			Assert.ThrowsExact<AssertionException>(() => Assert.True(false));
			Assert.Throws<ArgumentNullException>(() => Assert.True(null));
			Assert.DoesNotThrow(() => Assert.True(() => true));
			Assert.ThrowsExact<AssertionException>(() => Assert.True(() => false));
		}

		[TestMethod]
		[TestCategory("Framework")]
		public void False() {
			Assert.DoesNotThrow(() => Assert.False(false));
			Assert.ThrowsExact<AssertionException>(() => Assert.False(true));
			Assert.Throws<ArgumentNullException>(() => Assert.False(null));
			Assert.DoesNotThrow(() => Assert.False(() => false));
			Assert.ThrowsExact<AssertionException>(() => Assert.False(() => true));
		}

		[TestMethod]
		[TestCategory("Framework")]
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
		[TestCategory("Framework")]
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
		[TestCategory("Framework")]
		public void ThrowsInnerException() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.ThrowsInnerException<AssertionException>(null));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.ThrowsInnerException(typeof(AssertionException), null));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.ThrowsInnerException(null, () => { }));
			Assert.ThrowsExact<ArgumentException>(() => Assert.ThrowsInnerException(typeof(AssertTest), () => { }));
			Assert.DoesNotThrow(() => Assert.ThrowsInnerException(typeof(Exception), () => { throw new Exception(string.Empty, new Exception()); }));
			Assert.DoesNotThrow(() => Assert.ThrowsInnerException(typeof(DerivedExceptionTest), () => { throw new Exception(string.Empty, new DerivedExceptionTest()); }));
			Assert.DoesNotThrow(() => Assert.ThrowsInnerException(typeof(ExceptionTest), () => { throw new Exception(string.Empty, new DerivedExceptionTest()); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerException(typeof(DerivedExceptionTest), () => { throw new Exception(string.Empty, new ExceptionTest()); }));

			Assert.DoesNotThrow(() => Assert.ThrowsInnerException<ArgumentException>(() => { throw new ArgumentException("Testing", new ArgumentException()); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerException<ArgumentException>(() => { }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerException<NotSupportedException>(() => { throw new ArgumentNullException(); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerException<NotSupportedException>(() => { throw new ArgumentNullException("Testing", new ArgumentNullException("Testing")); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerException<ArgumentException>(() => { throw new ArgumentNullException("Testing", new Exception()); }));
			Assert.DoesNotThrow(() => Assert.ThrowsInnerException<ArgumentException>(() => { throw new Exception("Testing", new ArgumentNullException("Testing")); }));

			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerException(typeof(NotSupportedException), () => { throw new ArgumentNullException(); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerException(typeof(ArgumentException), () => { throw new ArgumentNullException(); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerException(typeof(NotSupportedException), () => { throw new ArgumentNullException("Testing", new ArgumentNullException()); }));
			Assert.DoesNotThrow(() => Assert.ThrowsInnerException(typeof(ArgumentException), () => { throw new ArgumentNullException("Testing", new ArgumentNullException()); }));
			Assert.DoesNotThrow(() => Assert.ThrowsInnerException(typeof(ArgumentException), () => { throw new ArgumentException("Testing", new ArgumentException("Testing")); }));

			try {
				Assert.ThrowsInnerException<ArgumentNullException>(() => { throw new InvalidOperationException("Testing"); });
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
		[TestCategory("Framework")]
		public void ThrowsInnerExceptionExact() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.ThrowsInnerExceptionExact<AssertionException>(null));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.ThrowsInnerExceptionExact(typeof(AssertionException), null));
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.ThrowsInnerExceptionExact(null, () => { }));
			Assert.ThrowsExact<ArgumentException>(() => Assert.ThrowsInnerExceptionExact(typeof(AssertTest), () => { }));
			Assert.DoesNotThrow(() => Assert.ThrowsInnerExceptionExact(typeof(Exception), () => { throw new DerivedExceptionTest(new Exception()); }));
			Assert.DoesNotThrow(() => Assert.ThrowsInnerExceptionExact(typeof(DerivedExceptionTest), () => { throw new Exception(string.Empty, new DerivedExceptionTest()); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact(typeof(ExceptionTest), () => { throw new Exception(string.Empty, new DerivedExceptionTest()); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact(typeof(DerivedExceptionTest), () => { throw new Exception(string.Empty, new ExceptionTest()); }));

			Assert.DoesNotThrow(() => Assert.ThrowsInnerExceptionExact<ArgumentException>(() => { throw new Exception("Testing", new ArgumentException("Testing")); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact<ArgumentException>(() => { }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact<NotSupportedException>(() => { throw new ArgumentNullException(); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact<NotSupportedException>(() => { throw new ArgumentNullException("Testing", new ArgumentNullException("Testing")); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact<ArgumentException>(() => { throw new ArgumentNullException("Testing", new Exception()); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact<ArgumentException>(() => { throw new Exception("Testing", new ArgumentNullException("Testing")); }));

			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact(typeof(NotSupportedException), () => { throw new ArgumentNullException(); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact(typeof(ArgumentException), () => { throw new ArgumentNullException(); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact(typeof(NotSupportedException), () => { throw new ArgumentNullException("Testing", new ArgumentNullException()); }));
			Assert.ThrowsExact<AssertionException>(() => Assert.ThrowsInnerExceptionExact(typeof(ArgumentException), () => { throw new ArgumentNullException("Testing", new ArgumentNullException()); }));
			Assert.DoesNotThrow(() => Assert.ThrowsInnerExceptionExact(typeof(ArgumentException), () => { throw new ArgumentException("Testing", new ArgumentException("Testing")); }));

			try {
				Assert.ThrowsInnerExceptionExact<ArgumentNullException>(() => { throw new InvalidOperationException("Testing"); });
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
		[TestCategory("Framework")]
		public void DoesNotThrow() {
			Assert.ThrowsExact<ArgumentNullException>(() => Assert.DoesNotThrow(null));
			Assert.ThrowsExact<AssertionException>(() => Assert.DoesNotThrow(() => { throw new ArgumentException("Testing"); }));
			Assert.DoesNotThrow(() => Assert.DoesNotThrow(() => { }));
		}

		[TestMethod]
		[TestCategory("Framework")]
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
