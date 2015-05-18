using System;

namespace Test {
	internal partial class Assert {
		public static readonly Action Failure = () => Fail(null);

		/// <summary>
		/// Asserts that the test has failed.
		/// </summary>
		public static void Fail(AssertionException pException = null) {
			throw pException ?? new AssertionException("Test has failed");
		}

		/// <summary>
		/// Asserts that a condition is true.
		/// </summary>
		public static void True(bool pCondition, AssertionException pException = null) {
			if (!pCondition) throw pException ?? new AssertionException("Condition was false");
		}

		/// <summary>
		/// Asserts that a condition is true.
		/// </summary>
		public static void True(Func<bool> pCondition, AssertionException pException = null) {
			if (pCondition == null) throw new ArgumentNullException(nameof(pCondition));
			if (!pCondition()) throw pException ?? new AssertionException("Condition was false");
		}

		/// <summary>
		/// Asserts that a condition is false.
		/// </summary>
		public static void False(bool pCondition, AssertionException pException = null) {
			if (pCondition) throw pException ?? new AssertionException("Condition was true");
		}

		/// <summary>
		/// Asserts that a condition is false.
		/// </summary>
		public static void False(Func<bool> pCondition, AssertionException pException = null) {
			if (pCondition == null) throw new ArgumentNullException(nameof(pCondition));
			if (pCondition()) throw pException ?? new AssertionException("Condition was true");
		}

		/// <summary>
		/// Asserts that a specific exception or a more derived type is thrown by the action.
		/// </summary>
		public static void Throws<TException>(Action pAction, AssertionException pException = null) where TException : Exception {
			if (pAction == null) throw new ArgumentNullException(nameof(pAction));
			Throws(typeof(TException), pAction, pException: pException);
		}

		/// <summary>
		/// Asserts that a specific exception or a more derived type is thrown by the action.
		/// </summary>
		public static void Throws(Type pType, Action pAction, AssertionException pException = null) {
			if (pAction == null) throw new ArgumentNullException(nameof(pAction));
			if (pType == null) throw new ArgumentNullException(nameof(pType));
			if (pType != typeof(Exception) && !pType.IsSubclassOf(typeof(Exception))) throw new ArgumentException("Type must derive from Exception", nameof(pType));

			bool thrown = false;
			try {
				pAction();
			}
			catch (Exception e) {
				thrown = true;
				Type thrownType = e.GetType();
				if (thrownType != pType && !thrownType.IsSubclassOf(pType)) {
					throw pException ?? AssertionException.GenerateWithInnerException(e, "Exception type was unexpected");
				}
			}

			if (!thrown) throw pException ?? new AssertionException("Expected exception was not thrown");
		}

		/// <summary>
		/// Asserts that a specific exception is thrown by the action.
		/// </summary>
		public static void ThrowsExact<TException>(Action pAction, AssertionException pException = null) where TException : Exception {
			if (pAction == null) throw new ArgumentNullException(nameof(pAction));
			ThrowsExact(typeof(TException), pAction, pException: pException);
		}

		/// <summary>
		/// Asserts that a specific exception is thrown by the action.
		/// </summary>
		public static void ThrowsExact(Type pType, Action pAction, AssertionException pException = null) {
			if (pAction == null) throw new ArgumentNullException(nameof(pAction));
			if (pType == null) throw new ArgumentNullException(nameof(pType));
			if (pType != typeof(Exception) && !pType.IsSubclassOf(typeof(Exception))) throw new ArgumentException("Type must derive from Exception", nameof(pType));

			bool thrown = false;
			try {
				pAction();
			}
			catch (Exception e) {
				thrown = true;
				if (e.GetType() != pType) {
					throw pException ?? AssertionException.GenerateWithInnerException(e, "Exception type was unexpected");
				}
			}

			if (!thrown) throw pException ?? new AssertionException("Expected exception was not thrown");
		}

		/// <summary>
		/// Asserts that an exception thrown by the action will have a specific InnerException or a more derived type.
		/// </summary>
		public static void ThrowsInnerException<TException>(Action pAction, AssertionException pException = null) where TException : Exception {
			if (pAction == null) throw new ArgumentNullException(nameof(pAction));
			ThrowsInnerException(typeof(TException), pAction, pException: pException);
		}

		/// <summary>
		/// Asserts that an exception thrown by the action will have a specific InnerException or a more derived type.
		/// </summary>
		public static void ThrowsInnerException(Type pType, Action pAction, AssertionException pException = null) {
			if (pAction == null) throw new ArgumentNullException(nameof(pAction));
			if (pType == null) throw new ArgumentNullException(nameof(pType));
			if (pType != typeof(Exception) && !pType.IsSubclassOf(typeof(Exception))) throw new ArgumentException("Type must derive from Exception", nameof(pType));

			bool thrown = false;
			try {
				pAction();
			}
			catch (Exception e) {
				thrown = true;
				if (e.InnerException == null) {
					throw pException ?? AssertionException.GenerateWithInnerException(e, "Exception did not have an InnerException");
				}

				Type thrownType = e.InnerException.GetType();
				if (thrownType != pType && !thrownType.IsSubclassOf(pType)) {
					throw pException ?? AssertionException.GenerateWithInnerException(e, "InnerException type was unexpected");
				}
			}

			if (!thrown) throw pException ?? new AssertionException("No exception was thrown");
		}

		/// <summary>
		/// Asserts that an exception thrown by the action will have a specific InnerException.
		/// </summary>
		public static void ThrowsInnerExceptionExact<TException>(Action pAction, AssertionException pException = null) where TException : Exception {
			if (pAction == null) throw new ArgumentNullException(nameof(pAction));
			ThrowsInnerExceptionExact(typeof(TException), pAction, pException: pException);
		}

		/// <summary>
		/// Asserts that an exception thrown by the action will have a specific InnerException.
		/// </summary>
		public static void ThrowsInnerExceptionExact(Type pType, Action pAction, AssertionException pException = null) {
			if (pAction == null) throw new ArgumentNullException(nameof(pAction));
			if (pType == null) throw new ArgumentNullException(nameof(pType));
			if (pType != typeof(Exception) && !pType.IsSubclassOf(typeof(Exception))) throw new ArgumentException("Type must derive from Exception", nameof(pType));

			bool thrown = false;
			try {
				pAction();
			}
			catch (Exception e) {
				thrown = true;
				if (e.InnerException == null) {
					throw pException ?? AssertionException.GenerateWithInnerException(e, "Exception did not have an InnerException");
				}

				if (e.InnerException.GetType() != pType) {
					throw pException ?? AssertionException.GenerateWithInnerException(e, "InnerException type was unexpected");
				}
			}

			if (!thrown) throw pException ?? new AssertionException("No exception was thrown");
		}

		/// <summary>
		/// Asserts that an action does not throw at all.
		/// </summary>
		public static void DoesNotThrow(Action pAction, AssertionException pException = null) {
			if (pAction == null) throw new ArgumentNullException(nameof(pAction));

			try {
				pAction();
			}
			catch (Exception e) {
				throw pException ?? AssertionException.GenerateWithInnerException(e, "Unexpected exception");
			}
		}
	}
}
