using System;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Defines methods that conform to the bit packing serialization interface.
	/// </summary>
	public interface IPackedBitSerializable {
		/// <summary>
		/// Deserializes data from the specified reader.
		/// </summary>
		/// <param name="reader">The stream of data to deserialize from.</param>
		void Deserialize(PackedBitReader reader);

		/// <summary>
		/// Serializes data to the specified writer.
		/// </summary>
		/// <param name="writer">The stream of data to serialize to.</param>
		void Serialize(PackedBitWriter writer);
	}
}
