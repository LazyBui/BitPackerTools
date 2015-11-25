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
		public int LowBit { get; private set; }
		public int HighBit { get; private set; }
		public bool HasHighBit { get; private set; }
		public bool Signed { get; set; }

		public PackedBitRangeAttribute(int bitIndex) {
			LowBit = bitIndex;
		}

		public PackedBitRangeAttribute(int lowBit, int highBit) {
			LowBit = lowBit;
			HighBit = highBit;
			HasHighBit = true;
		}
	}
}
