using System;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Represents a bit size and signedness for bit packer de/serialization.
	/// This relies on the order of the class properties.
	/// If you can't or don't want to rely on ordering, please use <see cref="PackedBitRangeAttribute"/> or <see cref="PackedBitOrderAttribute"/> instead.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class PackedBitSizeAttribute : Attribute {
		/// <summary>
		/// The quantity of bits.
		/// </summary>
		public int BitCount { get; private set; }
		/// <summary>
		/// Indicates whether or not the value is signed.
		/// </summary>
		public bool Signed { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PackedBitSizeAttribute" /> class.
		/// </summary>
		/// <param name="bitCount">The quantity of bits.</param>
		public PackedBitSizeAttribute(int bitCount = 1) {
			BitCount = bitCount;
		}
	}
}
