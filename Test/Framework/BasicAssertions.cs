using System;

namespace Test {
	internal partial class Assert {
		public static readonly Action Failure = () => Fail(null);

		/// <summary>
		/// Asserts that the test has failed.
		/// </summary>
		public static void Fail(AssertionException exception = null) {
			throw exception ?? new AssertionException("Test has failed");
		}

		/// <summary>
		/// Asserts that a condition is true.
		/// </summary>
		public static void True(bool condition, AssertionException exception = null) {
			if (!condition) throw exception ?? new AssertionException("Condition was false");
		}

		/// <summary>
		/// Asserts that a condition is true.
		/// </summary>
		public static void True(Func<bool> condition, AssertionException exception = null) {
			if (condition == null) throw new ArgumentNullException(nameof(condition));
			if (!condition()) throw exception ?? new AssertionException("Condition was false");
		}

		/// <summary>
		/// Asserts that a condition is false.
		/// </summary>
		public static void False(bool condition, AssertionException exception = null) {
			if (condition) throw exception ?? new AssertionException("Condition was true");
		}

		/// <summary>
		/// Asserts that a condition is false.
		/// </summary>
		public static void False(Func<bool> condition, AssertionException exception = null) {
			if (condition == null) throw new ArgumentNullException(nameof(condition));
			if (condition()) throw exception ?? new AssertionException("Condition was true");
		}

		/// <summary>
		/// Asserts that a specific exception or a more derived type is thrown by the action.
		/// </summary>
		public static void Throws<TException>(Action action, AssertionException exception = null) where TException : Exception {
			if (action == null) throw new ArgumentNullException(nameof(action));
			Throws(typeof(TException), action, exception: exception);
		}

		/// <summary>
		/// Asserts that a specific exception or a more derived type is thrown by the action.
		/// </summary>
		public static void Throws(Type type, Action action, AssertionException exception = null) {
			if (action == null) throw new ArgumentNullException(nameof(action));
			if (type == null) throw new ArgumentNullException(nameof(type));
			if (type != typeof(Exception) && !type.IsSubclassOf(typeof(Exception))) throw new ArgumentException("Type must derive from Exception", nameof(type));

			bool thrown = false;
			try {
				action();
			}
			catch (Exception e) {
				thrown = true;
				Type thrownType = e.GetType();
				if (thrownType != type && !thrownType.IsSubclassOf(type)) {
					throw exception ?? AssertionException.GenerateWithInnerException(e, "Exception type was unexpected");
				}
			}

			if (!thrown) throw exception ?? new AssertionException("Expected exception was not thrown");
		}

		/// <summary>
		/// Asserts that a specific exception is thrown by the action.
		/// </summary>
		public static void ThrowsExact<TException>(Action action, AssertionException exception = null) where TException : Exception {
			if (action == null) throw new ArgumentNullException(nameof(action));
			ThrowsExact(typeof(TException), action, exception: exception);
		}

		/// <summary>
		/// Asserts that a specific exception is thrown by the action.
		/// </summary>
		public static void ThrowsExact(Type type, Action action, AssertionException exception = null) {
			if (action == null) throw new ArgumentNullException(nameof(action));
			if (type == null) throw new ArgumentNullException(nameof(type));
			if (type != typeof(Exception) && !type.IsSubclassOf(typeof(Exception))) throw new ArgumentException("Type must derive from Exception", nameof(type));

			bool thrown = false;
			try {
				action();
			}
			catch (Exception e) {
				thrown = true;
				if (e.GetType() != type) {
					throw exception ?? AssertionException.GenerateWithInnerException(e, "Exception type was unexpected");
				}
			}

			if (!thrown) throw exception ?? new AssertionException("Expected exception was not thrown");
		}

		/// <summary>
		/// Asserts that an exception thrown by the action will have a specific InnerException or a more derived type.
		/// </summary>
		public static void ThrowsInnerException<TException>(Action action, AssertionException exception = null) where TException : Exception {
			if (action == null) throw new ArgumentNullException(nameof(action));
			ThrowsInnerException(typeof(TException), action, exception: exception);
		}

		/// <summary>
		/// Asserts that an exception thrown by the action will have a specific InnerException or a more derived type.
		/// </summary>
		public static void ThrowsInnerException(Type type, Action action, AssertionException exception = null) {
			if (action == null) throw new ArgumentNullException(nameof(action));
			if (type == null) throw new ArgumentNullException(nameof(type));
			if (type != typeof(Exception) && !type.IsSubclassOf(typeof(Exception))) throw new ArgumentException("Type must derive from Exception", nameof(type));

			bool thrown = false;
			try {
				action();
			}
			catch (Exception e) {
				thrown = true;
				if (e.InnerException == null) {
					throw exception ?? AssertionException.GenerateWithInnerException(e, "Exception did not have an InnerException");
				}

				Type thrownType = e.InnerException.GetType();
				if (thrownType != type && !thrownType.IsSubclassOf(type)) {
					throw exception ?? AssertionException.GenerateWithInnerException(e, "InnerException type was unexpected");
				}
			}

			if (!thrown) throw exception ?? new AssertionException("No exception was thrown");
		}

		/// <summary>
		/// Asserts that an exception thrown by the action will have a specific InnerException.
		/// </summary>
		public static void ThrowsInnerExceptionExact<TException>(Action action, AssertionException exception = null) where TException : Exception {
			if (action == null) throw new ArgumentNullException(nameof(action));
			ThrowsInnerExceptionExact(typeof(TException), action, exception: exception);
		}

		/// <summary>
		/// Asserts that an exception thrown by the action will have a specific InnerException.
		/// </summary>
		public static void ThrowsInnerExceptionExact(Type type, Action action, AssertionException exception = null) {
			if (action == null) throw new ArgumentNullException(nameof(action));
			if (type == null) throw new ArgumentNullException(nameof(type));
			if (type != typeof(Exception) && !type.IsSubclassOf(typeof(Exception))) throw new ArgumentException("Type must derive from Exception", nameof(type));

			bool thrown = false;
			try {
				action();
			}
			catch (Exception e) {
				thrown = true;
				if (e.InnerException == null) {
					throw exception ?? AssertionException.GenerateWithInnerException(e, "Exception did not have an InnerException");
				}

				if (e.InnerException.GetType() != type) {
					throw exception ?? AssertionException.GenerateWithInnerException(e, "InnerException type was unexpected");
				}
			}

			if (!thrown) throw exception ?? new AssertionException("No exception was thrown");
		}

		/// <summary>
		/// Asserts that an action does not throw at all.
		/// </summary>
		public static void DoesNotThrow(Action action, AssertionException exception = null) {
			if (action == null) throw new ArgumentNullException(nameof(action));

			try {
				action();
			}
			catch (Exception e) {
				throw exception ?? AssertionException.GenerateWithInnerException(e, "Unexpected exception");
			}
		}
	}
}
