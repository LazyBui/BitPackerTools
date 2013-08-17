using System;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Defines methods that conform to the bit packing serialization interface.
	/// </summary>
	public interface IPackedBitSerializable {
		void Deserialize(PackedBitReader pReader);
		void Serialize(PackedBitWriter pReader);
	}
}
