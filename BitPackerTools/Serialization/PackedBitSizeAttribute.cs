using System;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Represents a bit size and signedness for bit packer de/serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class PackedBitSizeAttribute : Attribute {
		public int BitCount { get; set; }
		public bool Signed { get; set; }

		public PackedBitSizeAttribute(int pBitCount) {
			BitCount = pBitCount;
		}
	}
}
