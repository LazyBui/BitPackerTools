using System;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Represents a bit index range and signedness for bit packer de/serialization.
	/// This does not rely on the order of the class properties.
	/// If you can rely on ordering and would prefer a simpler interface, please use <see cref="PackedBitSizeAttribute"/> instead.
	/// Bit indexes are 1-based.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class PackedBitRangeAttribute : Attribute {
		/// <summary>
		/// The lowest bit.
		/// </summary>
		public int LowBit { get; private set; }
		/// <summary>
		/// The highest bit.
		/// </summary>
		public int HighBit { get; private set; }
		/// <summary>
		/// Indicates whether or not the high bit is specified.
		/// </summary>
		public bool HasHighBit { get; private set; }
		/// <summary>
		/// Indicates whether or not the value is signed.
		/// </summary>
		public bool Signed { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PackedBitRangeAttribute" /> class.
		/// </summary>
		/// <param name="bitIndex">The index of the lowest bit.</param>
		public PackedBitRangeAttribute(int bitIndex) {
			LowBit = bitIndex;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PackedBitRangeAttribute" /> class.
		/// </summary>
		/// <param name="lowBit">The index of the lowest bit.</param>
		/// <param name="highBit">The index of the highest bit.</param>
		public PackedBitRangeAttribute(int lowBit, int highBit) {
			LowBit = lowBit;
			HighBit = highBit;
			HasHighBit = true;
		}
	}
}
