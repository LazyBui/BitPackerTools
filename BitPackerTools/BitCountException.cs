using System;

namespace BitPackerTools {
	/// <summary>
	/// Represents an argument error whereby the specified bit count is not valid.
	/// </summary>
	public class BitCountException : ArgumentException {
		internal BitCountException() : base("Bit count exceeded size of data or needs to be at least 1. If this is a signed call, the bit count may not be the max number of bits due to the sign bit (the sign bit must be preserved and is automatically added to the buffer without counting it, i.e. 31 bits is the maximum possible bit count for a signed int).") { }

		internal BitCountException(string message) : base(message) { }
	}
}