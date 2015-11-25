using System;

namespace Test {
	/// <summary>
	/// The exception that all assertions will throw if the assertion fails.
	/// </summary>
	internal sealed class AssertionException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="Test.AssertionException" /> class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public AssertionException(string message) : base(message) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="Test.AssertionException" /> class with a specified formatted error message.
		/// </summary>
		/// <param name="format">The format string that describes the error.</param>
		/// <param name="args">The objects to pass to the format string.</param>
		public AssertionException(string format, params object[] args) : base(string.Format(format, args)) { }
		
		private AssertionException(string message, Exception innerException) : base(message, innerException) { }

		/// <summary>
		/// Creates an AssertionException with an InnerException populated.
		/// </summary>
		/// <param name="innerException">The desired InnerException.</param>
		/// <param name="message">The message that describes the error.</param>
		/// <returns>An AssertionException with an InnerException populated.</returns>
		public static AssertionException GenerateWithInnerException(Exception innerException, string message) {
			return new AssertionException(message, innerException);
		}

		/// <summary>
		/// Creates an AssertionException with an InnerException populated.
		/// </summary>
		/// <param name="innerException">The desired InnerException.</param>
		/// <param name="format">The format string that describes the error.</param>
		/// <param name="args">The objects to pass to the format string.</param>
		/// <returns>An AssertionException with an InnerException populated.</returns>
		public static AssertionException GenerateWithInnerException(Exception innerException, string format, params object[] args) {
			return new AssertionException(string.Format(format, args), innerException);
		}
	}
}
