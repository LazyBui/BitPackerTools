using System;

namespace BitPackerTools {
	/// <summary>
	/// Represents a mismatch error in the expected number of bits remaining versus the remaining number of bits in a buffer.
	/// </summary>
	public class InsufficientBitsException : Exception {
		internal InsufficientBitsException() : base("Bit count in buffer was insufficient to fulfill the request.") { }

		internal InsufficientBitsException(string message) : base(message) { }
	}
}
