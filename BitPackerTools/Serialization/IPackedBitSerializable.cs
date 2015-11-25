using System;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Defines methods that conform to the bit packing serialization interface.
	/// </summary>
	public interface IPackedBitSerializable {
		void Deserialize(PackedBitReader reader);
		void Serialize(PackedBitWriter writer);
	}
}
