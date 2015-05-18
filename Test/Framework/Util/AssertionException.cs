using System;

namespace Test {
	/// <summary>
	/// The exception that all assertions will throw if the assertion fails.
	/// </summary>
	internal sealed class AssertionException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="Test.AssertionException" /> class with a specified error message.
		/// </summary>
		/// <param name="pMessage">The message that describes the error.</param>
		public AssertionException(string pMessage) : base(pMessage) { }
		/// <summary>
		/// Initializes a new instance of the <see cref="Test.AssertionException" /> class with a specified formatted error message.
		/// </summary>
		/// <param name="pFormat">The format string that describes the error.</param>
		/// <param name="pArgs">The objects to pass to the format string.</param>
		public AssertionException(string pFormat, params object[] pArgs) : base(string.Format(pFormat, pArgs)) { }
		
		private AssertionException(string pMessage, Exception pInnerException) : base(pMessage, pInnerException) { }

		/// <summary>
		/// Creates an AssertionException with an InnerException populated.
		/// </summary>
		/// <param name="pInnerException">The desired InnerException.</param>
		/// <param name="pMessage">The message that describes the error.</param>
		/// <returns>An AssertionException with an InnerException populated.</returns>
		public static AssertionException GenerateWithInnerException(Exception pInnerException, string pMessage) {
			new Exception("testing");
			return new AssertionException(pMessage, pInnerException);
		}

		/// <summary>
		/// Creates an AssertionException with an InnerException populated.
		/// </summary>
		/// <param name="pInnerException">The desired InnerException.</param>
		/// <param name="pFormat">The format string that describes the error.</param>
		/// <param name="pArgs">The objects to pass to the format string.</param>
		/// <returns>An AssertionException with an InnerException populated.</returns>
		public static AssertionException GenerateWithInnerException(Exception pInnerException, string pFormat, params object[] pArgs) {
			new Exception("testing");
			return new AssertionException(string.Format(pFormat, pArgs), pInnerException);
		}
	}
}
