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
		private bool HasSize { get; set; }
		private bool HasRange { get; set; }
		private bool HasOrder { get; set; }
		private bool ImplementsInterface { get; set; }

		private static Type[] sValidTypes = new[] {
			typeof(bool),
			typeof(sbyte), typeof(short), typeof(int), typeof(long),
			typeof(byte), typeof(ushort), typeof(uint), typeof(ulong),
		};

		/// <summary>
		/// Initializes a new instance of the BitPackerTools.Serialization.PackedBitSerializer class with a specified type.
		/// </summary>
		/// <param name="pSerializeType">The type to serialize from/deserialize to.</param>
		public PackedBitSerializer(Type pSerializeType) {
			if (pSerializeType == null) throw new ArgumentNullException(nameof(pSerializeType));
			SerializeType = pSerializeType;
			bool hasRange;
			bool hasOrder;
			bool hasSize;
			bool implementsInterface;
			ValidateType(SerializeType, out implementsInterface, out hasSize, out hasRange, out hasOrder);

			HasRange = hasRange;
			HasOrder = hasOrder;
			HasSize = hasSize;
			ImplementsInterface = implementsInterface;
		}

		private static IEnumerable<PropertyInfo> GetProperties(Type pSerializeType) {
			if (pSerializeType == null) throw CodePath.Unreachable;

			return pSerializeType.
				GetProperties(BindingFlags.Public | BindingFlags.Instance).
				Where(p => {
					return
						p.HasAttribute<PackedBitRangeAttribute>() ||
						p.HasAttribute<PackedBitSizeAttribute>() ||
						p.HasAttribute<PackedBitOrderAttribute>();
				});
		}

		private static void ValidateType(Type pSerializeType, out bool pImplementsInterface, out bool pHasSize, out bool pHasRange, out bool pHasOrder) {
			if (pSerializeType == null) throw CodePath.Unreachable;

			if (!pSerializeType.Implements<IPackedBitSerializable>()) {
				// We'll have to test the type to see if it fits our rubric
				var properties = GetProperties(pSerializeType);
				if (properties == null || !properties.Any()) {
					throw new ArgumentException("Type must have at least one property with PackedBitSizeAttribute, PackedBitRangeAttribute, or PackedBitOrderAttribute", nameof(pSerializeType));
				}

				bool hasSize = properties.Any(p => p.HasAttribute<PackedBitSizeAttribute>());
				bool hasRange = properties.Any(p => p.HasAttribute<PackedBitRangeAttribute>());
				bool hasOrder = properties.Any(p => p.HasAttribute<PackedBitOrderAttribute>());
				int setCount = new[] {
					hasSize,
					hasRange,
					hasOrder
				}.Count(c => c);

				if (setCount == 0) {
					// GetProperties is assumed to only return properties for which the attributes are applicable
					// And if there are none, there should be an error earlier
					throw CodePath.Unreachable;
				}

				if (setCount > 1) {
					throw new ArgumentException("PackedBitSizeAttribute, PackedBitRangeAttribute, and PackedBitOrderAttribute are all mutually incompatible; please choose a single one for the entire class", nameof(pSerializeType));
				}

				if (hasOrder) {
					properties = properties.OrderBy(p => p.GetCustomAttribute<PackedBitOrderAttribute>().LowBit);
					ValidateOrder(pSerializeType, properties);
				}
				else if (hasRange) {
					properties = properties.OrderBy(p => p.GetCustomAttribute<PackedBitRangeAttribute>().LowBit);
					ValidateRange(pSerializeType, properties);
				}
				else if (hasSize) {
					ValidatePropertyTypes(pSerializeType, properties);
				}
				else throw CodePath.Unreachable;

				pImplementsInterface = false;
				pHasOrder = hasOrder;
				pHasSize = hasSize;
				pHasRange = hasRange;
			}
			else {
				pImplementsInterface = true;
				pHasOrder = false;
				pHasSize = false;
				pHasRange = false;
			}
		}

		private static void ValidateProperty(Type pSerializeType, PropertyInfo pProperty) {
			if (!sValidTypes.Contains(pProperty.PropertyType)) {
				throw new ArgumentException($"Property type for property {pProperty.Name} must be one of the following: {string.Join(", ", sValidTypes.Select(t => t.Name))}", nameof(pSerializeType));
			}
		}

		private static void ValidatePropertyTypes(Type pSerializeType, IEnumerable<PropertyInfo> pProperties) {
			foreach (PropertyInfo prop in pProperties) {
				ValidateProperty(pSerializeType, prop);
			}
		}

		private static void ValidateOrder(Type pSerializeType, IEnumerable<PropertyInfo> pProperties) {
			var bitsUsed = new HashSet<int>();
			int maxBit = 0;
			int minBit = int.MaxValue;

			foreach (PropertyInfo prop in pProperties) {
				ValidateProperty(pSerializeType, prop);
				PackedBitOrderAttribute bitOrderAttr = prop.GetCustomAttribute<PackedBitOrderAttribute>();

				// Validation
				if (bitOrderAttr.LowBit <= 0) throw new ArgumentException($"Bits must be expressed as 1 or above ({prop.Name})", nameof(pSerializeType));
				for (int i = 0; i < bitOrderAttr.BitCount; i++) {
					if (bitsUsed.Contains(bitOrderAttr.LowBit + i)) throw new ArgumentException($"Overlapping bit value in range ({prop.Name}): {bitOrderAttr.LowBit + i}", nameof(pSerializeType));
					bitsUsed.Add(bitOrderAttr.LowBit + i);
				}

				maxBit = Math.Max(maxBit, bitOrderAttr.LowBit + (bitOrderAttr.BitCount - 1));
				minBit = Math.Min(minBit, bitOrderAttr.LowBit);
			}

			ValidateFullRange(pSerializeType, bitsUsed, minBit, maxBit);
		}

		private static void ValidateRange(Type pSerializeType, IEnumerable<PropertyInfo> pProperties) {
			var bitsUsed = new HashSet<int>();
			int maxBit = 0;
			int minBit = int.MaxValue;

			foreach (PropertyInfo prop in pProperties) {
				ValidateProperty(pSerializeType, prop);
				PackedBitRangeAttribute bitRangeAttr = prop.GetCustomAttribute<PackedBitRangeAttribute>();

				// Validation
				if (bitRangeAttr.HasHighBit) {
					if (bitRangeAttr.LowBit <= 0 || bitRangeAttr.HighBit <= 0) throw new ArgumentException($"Bits must be expressed as 1 or above ({prop.Name})", nameof(pSerializeType));
					if (bitRangeAttr.HighBit < bitRangeAttr.LowBit) throw new ArgumentException($"HighBit must be higher than LowBit ({prop.Name})", nameof(pSerializeType));

					for (int i = bitRangeAttr.LowBit; i <= bitRangeAttr.HighBit; i++) {
						if (bitsUsed.Contains(i)) throw new ArgumentException($"Overlapping bit value in range ({prop.Name}): {i}", nameof(pSerializeType));
						bitsUsed.Add(i);
					}

					maxBit = Math.Max(maxBit, bitRangeAttr.HighBit);
					minBit = Math.Min(minBit, bitRangeAttr.LowBit);
				}
				else {
					int bitValue = bitRangeAttr.LowBit;
					if (bitValue <= 0) throw new ArgumentException($"Bits must be expressed as 1 or above ({prop.Name})", nameof(pSerializeType));
					if (bitsUsed.Contains(bitValue)) throw new ArgumentException($"Overlapping bit value in range ({prop.Name}): {bitValue}", nameof(pSerializeType));
					bitsUsed.Add(bitValue);

					maxBit = Math.Max(maxBit, bitValue);
					minBit = Math.Min(minBit, bitValue);
				}
			}

			ValidateFullRange(pSerializeType, bitsUsed, minBit, maxBit);
		}

		private static void ValidateFullRange(Type pSerializeType, HashSet<int> pValues, int pMinimum, int pMaximum) {
			if (pValues.Count != (pMaximum - (pMinimum - 1))) {
				throw new ArgumentException(string.Format(
					"Missing bit value(s) from range: {0}",
					string.Join(", ", Enumerable.Range(pMinimum, pMaximum - (pMinimum - 1)).
					Where(v => !pValues.Contains(v)).
					Select(v => v.ToString()))), nameof(pSerializeType));
			}
		}

		/// <summary>
		/// Deserializes a PackedBitReader stream to an object.
		/// </summary>
		/// <param name="pReader">The stream to deserialize.</param>
		/// <returns>An object populated by the stream.</returns>
		public object Deserialize(PackedBitReader pReader) {
			object ret = Activator.CreateInstance(SerializeType);
			if (ImplementsInterface) {
				((IPackedBitSerializable)ret).Deserialize(pReader);
			}
			else {
				var properties = GetProperties(SerializeType);

				if (HasOrder) {
					properties = properties.OrderBy(p => p.GetCustomAttribute<PackedBitOrderAttribute>().LowBit);
					ReadImperativeBits(pReader, ret, properties, (prop) => {
						PackedBitOrderAttribute bitOrderAttr = prop.GetCustomAttribute<PackedBitOrderAttribute>();
						return new BitPair() {
							Count = bitOrderAttr.BitCount,
							Signed = bitOrderAttr.Signed,
						};
					});
				}
				else if (HasRange) {
					properties = properties.OrderBy(p => p.GetCustomAttribute<PackedBitRangeAttribute>().LowBit);
					ReadImperativeBits(pReader, ret, properties, (prop) => {
						PackedBitRangeAttribute bitRangeAttr = prop.GetCustomAttribute<PackedBitRangeAttribute>();
						return new BitPair() {
							Count = bitRangeAttr.HasHighBit ?
								bitRangeAttr.HighBit - (bitRangeAttr.LowBit - 1) :
								1,
							Signed = bitRangeAttr.Signed,
						};
					});
				}
				else if (HasSize) {
					ReadImperativeBits(pReader, ret, properties, (prop) => {
						PackedBitSizeAttribute bitSizeAttr = prop.GetCustomAttribute<PackedBitSizeAttribute>();
						return new BitPair() {
							Count = bitSizeAttr.BitCount,
							Signed = bitSizeAttr.Signed,
						};
					});
				}
				else throw CodePath.Unreachable;
			}
			return ret;
		}

		/// <summary>
		/// Serializes an object into a PackedBitWriter stream.
		/// </summary>
		/// <param name="pWriter">The stream to serialize to.</param>
		/// <param name="pObj">The object to serialize.</param>
		public void Serialize(PackedBitWriter pWriter, object pObj) {
			if (ImplementsInterface) {
				((IPackedBitSerializable)pObj).Serialize(pWriter);
			}
			else {
				var properties = GetProperties(SerializeType);

				if (HasOrder) {
					properties = properties.OrderBy(p => (p.GetCustomAttribute<PackedBitOrderAttribute>() ?? new PackedBitOrderAttribute(0)).LowBit);
					WriteImperativeBits(pWriter, pObj, properties, (prop) => {
						PackedBitOrderAttribute bitOrderAttr = prop.GetCustomAttribute<PackedBitOrderAttribute>();
						return new BitPair() {
							Count = bitOrderAttr.BitCount,
							Signed = bitOrderAttr.Signed,
						};
					});
				}
				else if (HasRange) {
					properties = properties.OrderBy(p => (p.GetCustomAttribute<PackedBitRangeAttribute>() ?? new PackedBitRangeAttribute(0)).LowBit);
					WriteImperativeBits(pWriter, pObj, properties, (prop) => {
						PackedBitRangeAttribute bitRangeAttr = prop.GetCustomAttribute<PackedBitRangeAttribute>();
						return new BitPair() {
							Count = bitRangeAttr.HasHighBit ?
								bitRangeAttr.HighBit - (bitRangeAttr.LowBit - 1) :
								1,
							Signed = bitRangeAttr.Signed,
						};
					});
				}
				else if (HasSize) {
					WriteImperativeBits(pWriter, pObj, properties, (prop) => {
						PackedBitSizeAttribute bitSizeAttr = prop.GetCustomAttribute<PackedBitSizeAttribute>();
						return new BitPair() {
							Count = bitSizeAttr.BitCount,
							Signed = bitSizeAttr.Signed,
						};
					});
				}
				else throw CodePath.Unreachable;
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
						else throw CodePath.Unreachable;
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
						else throw CodePath.Unreachable;
					}
				}
			}
		}

		private void ReadImperativeBits(PackedBitReader pReader, object pObj, IEnumerable<PropertyInfo> pProperties, Func<PropertyInfo, BitPair> pGetBitPair) {
			foreach (PropertyInfo prop in pProperties) {
				if (prop.PropertyType == typeof(bool)) prop.SetValue(pObj, pReader.ReadBool());
				else {
					BitPair read = pGetBitPair(prop);
					int bitRange = read.Count;
					if (read.Signed) {
						if (prop.PropertyType == typeof(sbyte)) prop.SetValue(pObj, pReader.ReadSignedInt8(bitRange));
						else if (prop.PropertyType == typeof(short)) prop.SetValue(pObj, pReader.ReadSignedInt16(bitRange));
						else if (prop.PropertyType == typeof(int)) prop.SetValue(pObj, pReader.ReadSignedInt32(bitRange));
						else if (prop.PropertyType == typeof(long)) prop.SetValue(pObj, pReader.ReadSignedInt64(bitRange));
						else throw CodePath.Unreachable;
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
						else throw CodePath.Unreachable;
					}
				}
			}
		}
	}
}
