using System;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Represents a bit size and signedness for bit packer de/serialization.
	/// This relies on the order of the class properties.
	/// If you can't or don't want to rely on ordering, please use <see cref="PackedBitRangeAttribute"/> or <see cref="PackedBitOrderAttribute"/> instead.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class PackedBitSizeAttribute : Attribute {
		public int BitCount { get; private set; }
		public bool Signed { get; set; }

		public PackedBitSizeAttribute(int pBitCount = 1) {
			BitCount = pBitCount;
		}
	}
}
