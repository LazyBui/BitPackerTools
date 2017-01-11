using System;

namespace BitPackerTools {
	/// <summary>
	/// Represents a mismatch error in the expected number of bits remaining versus the remaining number of bits in a buffer.
	/// </summary>
	public class InsufficientBitsException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="InsufficientBitsException" /> class.
		/// </summary>
		public InsufficientBitsException() : base("Bit count in buffer was insufficient to fulfill the request.") { }
		/// <summary>
		/// Initializes a new instance of the <see cref="InsufficientBitsException" /> class with a specified error message.
		/// </summary>
		/// <param name="message">The error message.</param>
		public InsufficientBitsException(string message) : base(message) { }
	}
}
