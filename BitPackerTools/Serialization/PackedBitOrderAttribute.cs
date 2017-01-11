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
		/// <summary>
		/// The index of the lowest bit.
		/// </summary>
		public int LowBit { get; private set; }
		/// <summary>
		/// The quantity of bits.
		/// </summary>
		public int BitCount { get; private set; }
		/// <summary>
		/// Indicates whether or not the value is signed.
		/// </summary>
		public bool Signed { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PackedBitOrderAttribute" /> class.
		/// </summary>
		/// <param name="bitIndex">The index of the lowest bit.</param>
		public PackedBitOrderAttribute(int bitIndex) {
			LowBit = bitIndex;
			BitCount = 1;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PackedBitOrderAttribute" /> class.
		/// </summary>
		/// <param name="bitIndex">The index of the lowest bit.</param>
		/// <param name="bitCount">The quantity of bits.</param>
		public PackedBitOrderAttribute(int bitIndex, int bitCount) {
			LowBit = bitIndex;
			BitCount = bitCount;
		}
	}
}
