using System;
using System.Linq;
using System.Reflection;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Serializes and deserializes objects into and from bit packed byte arrays.
	/// The BitPackerTools.Serialization.PackedBitSerializer enables you to control how objects are encoded into packed byte arrays.
	/// </summary>
	public class PackedBitSerializer {
		private Type SerializeType { get; set; }

		/// <summary>
		/// Initializes a new instance of the BitPackerTools.Serialization.PackedBitSerializer class with a specified type.
		/// </summary>
		/// <param name="pSerializeType">The type to serialize from/deserialize to.</param>
		public PackedBitSerializer(Type pSerializeType) {
			SerializeType = pSerializeType;
		}

		/// <summary>
		/// Deserializes a PackedBitReader stream to an object.
		/// </summary>
		/// <param name="pReader">The stream to deserialize.</param>
		/// <returns>An object populated by the stream.</returns>
		public object Deserialize(PackedBitReader pReader) {
			object ret = Activator.CreateInstance(SerializeType);
			if (SerializeType.Implements<IPackedBitSerializable>()) {
				((IPackedBitSerializable)ret).Deserialize(pReader);
			}
			else {
				foreach (PropertyInfo prop in SerializeType.GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
					if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(double) || prop.PropertyType == typeof(bool)) {
						PackedBitExcludeAttribute attr = prop.GetCustomAttribute<PackedBitExcludeAttribute>();
						if (attr != null) continue;

						if (prop.PropertyType == typeof(string)) prop.SetValue(ret, pReader.ReadString());
						else if (prop.PropertyType == typeof(double)) prop.SetValue(ret, pReader.ReadDouble());
						else if (prop.PropertyType == typeof(bool)) prop.SetValue(ret, pReader.ReadBool());
					}
					else {
						PackedBitSizeAttribute attr = prop.GetCustomAttribute<PackedBitSizeAttribute>();
						if (attr == null) continue;

						if (attr.Signed) {
							if (prop.PropertyType == typeof(sbyte)) prop.SetValue(ret, pReader.ReadSignedInt8(attr.BitCount));
							else if (prop.PropertyType == typeof(short)) prop.SetValue(ret, pReader.ReadSignedInt16(attr.BitCount));
							else if (prop.PropertyType == typeof(int)) prop.SetValue(ret, pReader.ReadSignedInt32(attr.BitCount));
							else if (prop.PropertyType == typeof(long)) prop.SetValue(ret, pReader.ReadSignedInt64(attr.BitCount));
							else throw new PackedBitSerializationException("Type is incompatible with bit packing as signed");
						}
						else {
							if (prop.PropertyType == typeof(sbyte)) prop.SetValue(ret, pReader.ReadInt8(attr.BitCount));
							else if (prop.PropertyType == typeof(short)) prop.SetValue(ret, pReader.ReadInt16(attr.BitCount));
							else if (prop.PropertyType == typeof(int)) prop.SetValue(ret, pReader.ReadInt32(attr.BitCount));
							else if (prop.PropertyType == typeof(long)) prop.SetValue(ret, pReader.ReadInt64(attr.BitCount));
							else if (prop.PropertyType == typeof(byte)) prop.SetValue(ret, pReader.ReadUInt8(attr.BitCount));
							else if (prop.PropertyType == typeof(ushort)) prop.SetValue(ret, pReader.ReadUInt16(attr.BitCount));
							else if (prop.PropertyType == typeof(uint)) prop.SetValue(ret, pReader.ReadUInt32(attr.BitCount));
							else if (prop.PropertyType == typeof(ulong)) prop.SetValue(ret, pReader.ReadUInt64(attr.BitCount));
							else if (prop.PropertyType.IsArray && prop.PropertyType == typeof(byte)) prop.SetValue(ret, pReader.ReadBytes(attr.BitCount));
							else throw new PackedBitSerializationException("Type is incompatible with bit packing as unsigned");
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// Serializes an object into a PackedBitWriter stream.
		/// </summary>
		/// <param name="pWriter">The stream to serialize to.</param>
		/// <param name="pObj">The object to serialize.</param>
		public void Serialize(PackedBitWriter pWriter, object pObj) {
			if (SerializeType.Implements<IPackedBitSerializable>()) {
				((IPackedBitSerializable)pObj).Serialize(pWriter);
			}
			else {
				foreach (PropertyInfo prop in SerializeType.GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
					if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(double) || prop.PropertyType == typeof(bool)) {
						PackedBitExcludeAttribute attr = prop.GetCustomAttribute<PackedBitExcludeAttribute>();
						if (attr != null) continue;
						if (prop.PropertyType == typeof(string)) pWriter.Write((string)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(double)) pWriter.Write((double)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(bool)) pWriter.Write((bool)prop.GetValue(pObj));
					}
					else {
						PackedBitSizeAttribute attr = prop.GetCustomAttribute<PackedBitSizeAttribute>();
						if (attr == null) continue;

						if (attr.Signed) {
							if (prop.PropertyType == typeof(sbyte)) pWriter.WriteSigned(attr.BitCount, (sbyte)prop.GetValue(pObj));
							else if (prop.PropertyType == typeof(short)) pWriter.WriteSigned(attr.BitCount, (short)prop.GetValue(pObj));
							else if (prop.PropertyType == typeof(int)) pWriter.WriteSigned(attr.BitCount, (int)prop.GetValue(pObj));
							else if (prop.PropertyType == typeof(long)) pWriter.WriteSigned(attr.BitCount, (long)prop.GetValue(pObj));
							else throw new PackedBitSerializationException("Type is incompatible with bit packing as signed");
						}
						else {
							if (prop.PropertyType == typeof(sbyte)) pWriter.Write(attr.BitCount, (sbyte)prop.GetValue(pObj));
							else if (prop.PropertyType == typeof(short)) pWriter.Write(attr.BitCount, (short)prop.GetValue(pObj));
							else if (prop.PropertyType == typeof(int)) pWriter.Write(attr.BitCount, (int)prop.GetValue(pObj));
							else if (prop.PropertyType == typeof(long)) pWriter.Write(attr.BitCount, (long)prop.GetValue(pObj));
							else if (prop.PropertyType == typeof(byte)) pWriter.Write(attr.BitCount, (byte)prop.GetValue(pObj));
							else if (prop.PropertyType == typeof(ushort)) pWriter.Write(attr.BitCount, (ushort)prop.GetValue(pObj));
							else if (prop.PropertyType == typeof(uint)) pWriter.Write(attr.BitCount, (uint)prop.GetValue(pObj));
							else if (prop.PropertyType == typeof(ulong)) pWriter.Write(attr.BitCount, (ulong)prop.GetValue(pObj));
							else if (prop.PropertyType.IsArray && prop.PropertyType == typeof(byte)) pWriter.Write(attr.BitCount, (byte[])prop.GetValue(pObj));
							else throw new PackedBitSerializationException("Type is incompatible with bit packing as unsigned");
						}
					}
				}
			}
		}
	}
}
