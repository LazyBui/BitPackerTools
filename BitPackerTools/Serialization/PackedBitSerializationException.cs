using System;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Represents a de/serialization error.
	/// </summary>
	public class PackedBitSerializationException : Exception {
		public PackedBitSerializationException() : base() { }
		public PackedBitSerializationException(string message) : base(message) { }
	}
}
