using System;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Represents a bit index, count, and signedness for bit packer de/serialization.
	/// This does not rely on the order of the class properties.
	/// If you can rely on ordering and would prefer a simpler interface, please use <see cref="PackedBitSizeAttribute"/> instead.
	/// Bit indexes are 1-based.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class PackedBitOrderAttribute : Attribute {
		public int LowBit { get; private set; }
		public int BitCount { get; private set; }
		public bool Signed { get; set; }

		public PackedBitOrderAttribute(int bitIndex) {
			LowBit = bitIndex;
			BitCount = 1;
		}

		public PackedBitOrderAttribute(int bitIndex, int bitCount) {
			LowBit = bitIndex;
			BitCount = bitCount;
		}
	}
}
