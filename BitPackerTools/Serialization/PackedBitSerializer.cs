using System;
using System.Collections.Generic;
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
				bool hasSize;
				bool hasRange;
				bool hasOrder;
				var props = ValidateType(SerializeType, out hasSize, out hasRange, out hasOrder);

				if (hasOrder) {
					props = props.OrderBy(p => p.GetCustomAttribute<PackedBitOrderAttribute>().LowBit);

					ValidateOrder(props);

					ReadImperativeBits(pReader, ret, props, (prop) => {
						PackedBitOrderAttribute bitOrderAttr = prop.GetCustomAttribute<PackedBitOrderAttribute>();
						return new BitPair() {
							Count = bitOrderAttr.BitCount,
							Signed = bitOrderAttr.Signed,
						};
					});
				}

				if (hasRange) {
					props = props.OrderBy(p => p.GetCustomAttribute<PackedBitRangeAttribute>().LowBit);

					ValidateRange(props);

					ReadImperativeBits(pReader, ret, props, (prop) => {
						PackedBitRangeAttribute bitRangeAttr = prop.GetCustomAttribute<PackedBitRangeAttribute>();
						return new BitPair() {
							Count = bitRangeAttr.HasHighBit ?
								bitRangeAttr.HighBit - (bitRangeAttr.LowBit - 1) :
								1,
							Signed = bitRangeAttr.Signed,
						};
					});
				}

				if (hasSize) {
					ValidateSize(props);

					ReadImperativeBits(pReader, ret, props, (prop) => {
						PackedBitSizeAttribute bitSizeAttr = prop.GetCustomAttribute<PackedBitSizeAttribute>();
						return new BitPair() {
							Count = bitSizeAttr.BitCount,
							Signed = bitSizeAttr.Signed,
						};
					});
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
				bool hasSize;
				bool hasRange;
				bool hasOrder;
				var props = ValidateType(SerializeType, out hasSize, out hasRange, out hasOrder);

				if (hasOrder) {
					props = props.OrderBy(p => (p.GetCustomAttribute<PackedBitOrderAttribute>() ?? new PackedBitOrderAttribute(0)).LowBit);

					ValidateOrder(props);

					WriteImperativeBits(pWriter, pObj, props, (prop) => {
						PackedBitOrderAttribute bitOrderAttr = prop.GetCustomAttribute<PackedBitOrderAttribute>();
						return new BitPair() {
							Count = bitOrderAttr.BitCount,
							Signed = bitOrderAttr.Signed,
						};
					});
				}

				if (hasRange) {
					props = props.OrderBy(p => (p.GetCustomAttribute<PackedBitRangeAttribute>() ?? new PackedBitRangeAttribute(0)).LowBit);

					ValidateRange(props);

					WriteImperativeBits(pWriter, pObj, props, (prop) => {
						PackedBitRangeAttribute bitRangeAttr = prop.GetCustomAttribute<PackedBitRangeAttribute>();
						return new BitPair() {
							Count = bitRangeAttr.HasHighBit ?
								bitRangeAttr.HighBit - (bitRangeAttr.LowBit - 1) :
								1,
							Signed = bitRangeAttr.Signed,
						};
					});
				}

				if (hasSize) {
					ValidateSize(props);

					WriteImperativeBits(pWriter, pObj, props, (prop) => {
						PackedBitSizeAttribute bitSizeAttr = prop.GetCustomAttribute<PackedBitSizeAttribute>();
						return new BitPair() {
							Count = bitSizeAttr.BitCount,
							Signed = bitSizeAttr.Signed,
						};
					});
				}
			}
		}

		private IEnumerable<PropertyInfo> ValidateType(Type pSerializeType, out bool pHasSize, out bool pHasRange, out bool pHasOrder) {
			var props = pSerializeType.
				GetProperties(BindingFlags.Public | BindingFlags.Instance).
				Where(p => {
					return
						p.HasAttribute<PackedBitRangeAttribute>() ||
						p.HasAttribute<PackedBitSizeAttribute>() ||
						p.HasAttribute<PackedBitOrderAttribute>();
				});

			pHasSize = props.Any(p => p.HasAttribute<PackedBitSizeAttribute>());
			pHasRange = props.Any(p => p.HasAttribute<PackedBitRangeAttribute>());
			pHasOrder = props.Any(p => p.HasAttribute<PackedBitOrderAttribute>());
			int setCount = new[] {
					pHasSize,
					pHasRange,
					pHasOrder
				}.Count(c => c);

			if (setCount != 1) throw new ArgumentException("PackedBitSizeAttribute, PackedBitRangeAttribute, and PackedBitOrderAttribute are all mutually incompatible; please choose a single one for the entire class");

			return props;
		}

		private void ValidateSize(IEnumerable<PropertyInfo> pProperties) {
			foreach (PropertyInfo prop in pProperties) {
				if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(double)) {
					throw new ArgumentException("double and string may not be used with bit sizes");
				}
			}
		}

		private void ValidateOrder(IEnumerable<PropertyInfo> pProperties) {
			var bitsUsed = new HashSet<int>();
			int maxBit = 0;
			int minBit = int.MaxValue;
			
			foreach (PropertyInfo prop in pProperties) {
				PackedBitOrderAttribute bitOrderAttr = prop.GetCustomAttribute<PackedBitOrderAttribute>();
				if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(double)) {
					throw new ArgumentException("double and string may not be used with bit ordering");
				}

				// Validation
				if (bitOrderAttr.LowBit <= 0) throw new ArgumentException(string.Format("Bits must be expressed as 1 or above ({0})", prop.Name));
				for (int i = 0; i < bitOrderAttr.BitCount; i++) {
					if (bitsUsed.Contains(bitOrderAttr.LowBit + i)) throw new ArgumentException(string.Format("Overlapping bit value in range ({0}): {1}", prop.Name, bitOrderAttr.LowBit + i));
					bitsUsed.Add(bitOrderAttr.LowBit + i);
				}

				maxBit = Math.Max(maxBit, bitOrderAttr.LowBit + (bitOrderAttr.BitCount - 1));
				minBit = Math.Min(minBit, bitOrderAttr.LowBit);
			}

			ValidateFullRange(bitsUsed, minBit, maxBit);
		}

		private void ValidateRange(IEnumerable<PropertyInfo> pProperties) {
			var bitsUsed = new HashSet<int>();
			int maxBit = 0;
			int minBit = int.MaxValue;

			foreach (PropertyInfo prop in pProperties) {
				PackedBitRangeAttribute bitRangeAttr = prop.GetCustomAttribute<PackedBitRangeAttribute>();
				if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(double)) {
					throw new ArgumentException("double and string may not be used with bit ranges");
				}

				// Validation
				if (bitRangeAttr.HasHighBit) {
					if (bitRangeAttr.LowBit <= 0 || bitRangeAttr.HighBit <= 0) throw new ArgumentException(string.Format("Bits must be expressed as 1 or above ({0})", prop.Name));
					if (bitRangeAttr.HighBit < bitRangeAttr.LowBit) throw new ArgumentException(string.Format("HighBit must be higher than LowBit ({0})", prop.Name)); 

					for (int i = bitRangeAttr.LowBit; i <= bitRangeAttr.HighBit; i++) {
						if (bitsUsed.Contains(i)) throw new ArgumentException(string.Format("Overlapping bit value in range ({0}): {1}", prop.Name, i));
						bitsUsed.Add(i);
					}

					maxBit = Math.Max(maxBit, bitRangeAttr.HighBit);
					minBit = Math.Min(minBit, bitRangeAttr.LowBit);
				}
				else {
					int bitValue = bitRangeAttr.LowBit;
					if (bitValue <= 0) throw new ArgumentException(string.Format("Bits must be expressed as 1 or above ({0})", prop.Name));
					if (bitsUsed.Contains(bitValue)) throw new ArgumentException(string.Format("Overlapping bit value in range ({0}): {1}", prop.Name, bitValue));
					bitsUsed.Add(bitValue);

					maxBit = Math.Max(maxBit, bitValue);
					minBit = Math.Min(minBit, bitValue);
				}
			}

			ValidateFullRange(bitsUsed, minBit, maxBit);
		}

		private void ValidateFullRange(HashSet<int> pValues, int pMinimum, int pMaximum) {
			if (pValues.Count != (pMaximum - (pMinimum - 1))) {
				throw new ArgumentException(string.Format(
					"Missing bit value(s) from range: {0}",
					string.Join(", ", Enumerable.Range(pMinimum, pMaximum - (pMinimum - 1)).
					Where(v => !pValues.Contains(v)).
					Select(v => v.ToString()))));
			}
		}

		private class BitPair {
			public int Count { get; set; }
			public bool Signed { get; set; }
		}

		private void WriteImperativeBits(PackedBitWriter pWriter, object pObj, IEnumerable<PropertyInfo> pProperties, Func<PropertyInfo, BitPair> pGetBitPair) {
			foreach (PropertyInfo prop in pProperties) {
				if (prop.PropertyType == typeof(bool)) pWriter.Write((bool)prop.GetValue(pObj));
				else {
					BitPair write = pGetBitPair(prop);
					int bitRange = write.Count;
					if (write.Signed) {
						if (prop.PropertyType == typeof(sbyte)) pWriter.WriteSigned(bitRange, (sbyte)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(short)) pWriter.WriteSigned(bitRange, (short)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(int)) pWriter.WriteSigned(bitRange, (int)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(long)) pWriter.WriteSigned(bitRange, (long)prop.GetValue(pObj));
						else throw new PackedBitSerializationException("Type is incompatible with bit packing as a signed bit collection");
					}
					else {
						if (prop.PropertyType == typeof(sbyte)) pWriter.Write(bitRange, (sbyte)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(short)) pWriter.Write(bitRange, (short)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(int)) pWriter.Write(bitRange, (int)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(long)) pWriter.Write(bitRange, (long)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(byte)) pWriter.Write(bitRange, (byte)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(ushort)) pWriter.Write(bitRange, (ushort)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(uint)) pWriter.Write(bitRange, (uint)prop.GetValue(pObj));
						else if (prop.PropertyType == typeof(ulong)) pWriter.Write(bitRange, (ulong)prop.GetValue(pObj));
						else throw new PackedBitSerializationException("Type is incompatible with bit packing as an unsigned bit collection");
					}
				}
			}
		}

		private void ReadImperativeBits(PackedBitReader pReader, object pObj, IEnumerable<PropertyInfo> pProperties, Func<PropertyInfo, BitPair> pGetBitPair) {
			foreach (PropertyInfo prop in pProperties) {
				if (prop.Name == "ShouldBeTrue43") {
					var i = 5;
					i++;
				}
				if (prop.PropertyType == typeof(bool)) prop.SetValue(pObj, pReader.ReadBool());
				else {
					BitPair read = pGetBitPair(prop);
					int bitRange = read.Count;
					if (read.Signed) {
						if (prop.PropertyType == typeof(sbyte)) prop.SetValue(pObj, pReader.ReadSignedInt8(bitRange));
						else if (prop.PropertyType == typeof(short)) prop.SetValue(pObj, pReader.ReadSignedInt16(bitRange));
						else if (prop.PropertyType == typeof(int)) prop.SetValue(pObj, pReader.ReadSignedInt32(bitRange));
						else if (prop.PropertyType == typeof(long)) prop.SetValue(pObj, pReader.ReadSignedInt64(bitRange));
						else throw new PackedBitSerializationException("Type is incompatible with bit unpacking as a signed bit collection");
					}
					else {
						if (prop.PropertyType == typeof(sbyte)) prop.SetValue(pObj, pReader.ReadInt8(bitRange));
						else if (prop.PropertyType == typeof(short)) prop.SetValue(pObj, pReader.ReadInt16(bitRange));
						else if (prop.PropertyType == typeof(int)) prop.SetValue(pObj, pReader.ReadInt32(bitRange));
						else if (prop.PropertyType == typeof(long)) prop.SetValue(pObj, pReader.ReadInt64(bitRange));
						else if (prop.PropertyType == typeof(byte)) prop.SetValue(pObj, pReader.ReadUInt8(bitRange));
						else if (prop.PropertyType == typeof(ushort)) prop.SetValue(pObj, pReader.ReadUInt16(bitRange));
						else if (prop.PropertyType == typeof(uint)) prop.SetValue(pObj, pReader.ReadUInt32(bitRange));
						else if (prop.PropertyType == typeof(ulong)) prop.SetValue(pObj, pReader.ReadUInt64(bitRange));
						else throw new PackedBitSerializationException("Type is incompatible with bit unpacking as an unsigned bit collection");
					}
				}
			}
		}
	}
}
