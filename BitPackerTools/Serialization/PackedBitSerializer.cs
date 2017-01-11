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
		/// <param name="serializeType">The type to serialize from/deserialize to.</param>
		/// <exception cref="ArgumentNullException"><paramref name="serializeType" /> is null.</exception>
		/// <exception cref="ArgumentException"><paramref name="serializeType" /> has no properties with bit attributes, has properties with multiple types of bit attributes, has type(s) that are unsupported by the library, or has bit values that are missing or overlapping.</exception>
		public PackedBitSerializer(Type serializeType) {
			if (serializeType == null) throw new ArgumentNullException(nameof(serializeType));
			SerializeType = serializeType;
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

		private static IEnumerable<PropertyInfo> GetProperties(Type serializeType) {
			if (serializeType == null) throw CodePath.Unreachable;

			return serializeType.
				GetProperties(BindingFlags.Public | BindingFlags.Instance).
				Where(p => {
					return
						p.HasAttribute<PackedBitRangeAttribute>() ||
						p.HasAttribute<PackedBitSizeAttribute>() ||
						p.HasAttribute<PackedBitOrderAttribute>();
				});
		}

		private static void ValidateType(Type serializeType, out bool implementsInterface, out bool hasSize, out bool hasRange, out bool hasOrder) {
			if (serializeType == null) throw CodePath.Unreachable;

			if (!serializeType.Implements<IPackedBitSerializable>()) {
				// We'll have to test the type to see if it fits our rubric
				var properties = GetProperties(serializeType);
				if (properties == null || !properties.Any()) {
					throw new ArgumentException($"Type must have at least one property with {nameof(PackedBitSizeAttribute)}, {nameof(PackedBitRangeAttribute)}, or {nameof(PackedBitOrderAttribute)}", nameof(serializeType));
				}

				bool hasSizeAttribute = properties.Any(p => p.HasAttribute<PackedBitSizeAttribute>());
				bool hasRangeAttribute = properties.Any(p => p.HasAttribute<PackedBitRangeAttribute>());
				bool hasOrderAttribute = properties.Any(p => p.HasAttribute<PackedBitOrderAttribute>());
				int setCount = new[] {
					hasSizeAttribute,
					hasRangeAttribute,
					hasOrderAttribute
				}.Count(c => c);

				if (setCount == 0) {
					// GetProperties is assumed to only return properties for which the attributes are applicable
					// And if there are none, there should be an error earlier
					throw CodePath.Unreachable;
				}

				if (setCount > 1) {
					throw new ArgumentException($"{nameof(PackedBitSizeAttribute)}, {nameof(PackedBitRangeAttribute)}, and {nameof(PackedBitOrderAttribute)} are all mutually incompatible; please choose a single one for the entire class", nameof(serializeType));
				}

				if (hasOrderAttribute) {
					properties = properties.OrderBy(p => p.GetCustomAttribute<PackedBitOrderAttribute>().LowBit);
					ValidateOrder(serializeType, properties);
				}
				else if (hasRangeAttribute) {
					properties = properties.OrderBy(p => p.GetCustomAttribute<PackedBitRangeAttribute>().LowBit);
					ValidateRange(serializeType, properties);
				}
				else if (hasSizeAttribute) {
					ValidatePropertyTypes(serializeType, properties);
				}
				else throw CodePath.Unreachable;

				implementsInterface = false;
				hasOrder = hasOrderAttribute;
				hasSize = hasSizeAttribute;
				hasRange = hasRangeAttribute;
			}
			else {
				implementsInterface = true;
				hasOrder = false;
				hasSize = false;
				hasRange = false;
			}
		}

		private static void ValidateProperty(Type serializeType, PropertyInfo property) {
			if (!sValidTypes.Contains(property.PropertyType)) {
				throw new ArgumentException(
					$"Property type for property {property.Name} must be one of the following: " +
					string.Join(", ", sValidTypes.Select(t => t.Name).ToArray()),
					nameof(serializeType));
			}
		}

		private static void ValidatePropertyTypes(Type serializeType, IEnumerable<PropertyInfo> properties) {
			foreach (PropertyInfo prop in properties) {
				ValidateProperty(serializeType, prop);
			}
		}

		private static void ValidateOrder(Type serializeType, IEnumerable<PropertyInfo> properties) {
			var bitsUsed = new HashSet<int>();
			int maxBit = 0;
			int minBit = int.MaxValue;

			foreach (PropertyInfo prop in properties) {
				ValidateProperty(serializeType, prop);
				PackedBitOrderAttribute bitOrderAttr = prop.GetCustomAttribute<PackedBitOrderAttribute>();

				// Validation
				if (bitOrderAttr.LowBit <= 0) throw new ArgumentException($"Bits must be expressed as 1 or above ({prop.Name})", nameof(serializeType));
				for (int i = 0; i < bitOrderAttr.BitCount; i++) {
					if (bitsUsed.Contains(bitOrderAttr.LowBit + i)) throw new ArgumentException($"Overlapping bit value in range ({prop.Name}): {bitOrderAttr.LowBit + i}", nameof(serializeType));
					bitsUsed.Add(bitOrderAttr.LowBit + i);
				}

				maxBit = Math.Max(maxBit, bitOrderAttr.LowBit + (bitOrderAttr.BitCount - 1));
				minBit = Math.Min(minBit, bitOrderAttr.LowBit);
			}

			ValidateFullRange(serializeType, bitsUsed, minBit, maxBit);
		}

		private static void ValidateRange(Type serializeType, IEnumerable<PropertyInfo> properties) {
			var bitsUsed = new HashSet<int>();
			int maxBit = 0;
			int minBit = int.MaxValue;

			foreach (PropertyInfo prop in properties) {
				ValidateProperty(serializeType, prop);
				PackedBitRangeAttribute bitRangeAttr = prop.GetCustomAttribute<PackedBitRangeAttribute>();

				// Validation
				if (bitRangeAttr.HasHighBit) {
					if (bitRangeAttr.LowBit <= 0 || bitRangeAttr.HighBit <= 0) throw new ArgumentException($"Bits must be expressed as 1 or above ({prop.Name})", nameof(serializeType));
					if (bitRangeAttr.HighBit < bitRangeAttr.LowBit) throw new ArgumentException($"HighBit must be higher than LowBit ({prop.Name})", nameof(serializeType));

					for (int i = bitRangeAttr.LowBit; i <= bitRangeAttr.HighBit; i++) {
						if (bitsUsed.Contains(i)) throw new ArgumentException($"Overlapping bit value in range ({prop.Name}): {i}", nameof(serializeType));
						bitsUsed.Add(i);
					}

					maxBit = Math.Max(maxBit, bitRangeAttr.HighBit);
					minBit = Math.Min(minBit, bitRangeAttr.LowBit);
				}
				else {
					int bitValue = bitRangeAttr.LowBit;
					if (bitValue <= 0) throw new ArgumentException($"Bits must be expressed as 1 or above ({prop.Name})", nameof(serializeType));
					if (bitsUsed.Contains(bitValue)) throw new ArgumentException($"Overlapping bit value in range ({prop.Name}): {bitValue}", nameof(serializeType));
					bitsUsed.Add(bitValue);

					maxBit = Math.Max(maxBit, bitValue);
					minBit = Math.Min(minBit, bitValue);
				}
			}

			ValidateFullRange(serializeType, bitsUsed, minBit, maxBit);
		}

		private static void ValidateFullRange(Type serializeType, HashSet<int> values, int min, int max) {
			if (values.Count != (max - (min - 1))) {
				throw new ArgumentException(
					string.Format(
						"Missing bit value(s) from range: {0}",
						string.Join(
							", ",
							Enumerable.Range(min, max - (min - 1)).
							Where(v => !values.Contains(v)).
							Select(v => v.ToString()).
							ToArray())),
					nameof(serializeType));
			}
		}

		/// <summary>
		/// Deserializes a PackedBitReader stream to an object.
		/// </summary>
		/// <param name="reader">The stream to deserialize.</param>
		/// <returns>An object populated by the stream.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="reader" /> is null.</exception>
		public object Deserialize(PackedBitReader reader) {
			if (object.ReferenceEquals(reader, null)) throw new ArgumentNullException(nameof(reader));

			object ret = Activator.CreateInstance(SerializeType);
			if (ImplementsInterface) {
				((IPackedBitSerializable)ret).Deserialize(reader);
			}
			else {
				var properties = GetProperties(SerializeType);

				if (HasOrder) {
					properties = properties.OrderBy(p => p.GetCustomAttribute<PackedBitOrderAttribute>().LowBit);
					ReadImperativeBits(reader, ret, properties, (prop) => {
						PackedBitOrderAttribute bitOrderAttr = prop.GetCustomAttribute<PackedBitOrderAttribute>();
						return new BitPair() {
							Count = bitOrderAttr.BitCount,
							Signed = bitOrderAttr.Signed,
						};
					});
				}
				else if (HasRange) {
					properties = properties.OrderBy(p => p.GetCustomAttribute<PackedBitRangeAttribute>().LowBit);
					ReadImperativeBits(reader, ret, properties, (prop) => {
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
					ReadImperativeBits(reader, ret, properties, (prop) => {
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
		/// <param name="writer">The stream to serialize to.</param>
		/// <param name="value">The object to serialize.</param>
		/// <exception cref="ArgumentNullException"><paramref name="writer" /> is null.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="value" /> is null.</exception>
		public void Serialize(PackedBitWriter writer, object value) {
			if (object.ReferenceEquals(writer, null)) throw new ArgumentNullException(nameof(writer));
			if (object.ReferenceEquals(value, null)) throw new ArgumentNullException(nameof(value));

			if (ImplementsInterface) {
				((IPackedBitSerializable)value).Serialize(writer);
			}
			else {
				var properties = GetProperties(SerializeType);

				if (HasOrder) {
					properties = properties.OrderBy(p => (p.GetCustomAttribute<PackedBitOrderAttribute>() ?? new PackedBitOrderAttribute(0)).LowBit);
					WriteImperativeBits(writer, value, properties, (prop) => {
						PackedBitOrderAttribute bitOrderAttr = prop.GetCustomAttribute<PackedBitOrderAttribute>();
						return new BitPair() {
							Count = bitOrderAttr.BitCount,
							Signed = bitOrderAttr.Signed,
						};
					});
				}
				else if (HasRange) {
					properties = properties.OrderBy(p => (p.GetCustomAttribute<PackedBitRangeAttribute>() ?? new PackedBitRangeAttribute(0)).LowBit);
					WriteImperativeBits(writer, value, properties, (prop) => {
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
					WriteImperativeBits(writer, value, properties, (prop) => {
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

		private void WriteImperativeBits(PackedBitWriter writer, object value, IEnumerable<PropertyInfo> properties, Func<PropertyInfo, BitPair> getBitPair) {
			foreach (PropertyInfo prop in properties) {
				if (prop.PropertyType == typeof(bool)) writer.Write((bool)prop.GetValue(value, null));
				else {
					BitPair write = getBitPair(prop);
					int bitRange = write.Count;
					if (write.Signed) {
						if (prop.PropertyType == typeof(sbyte)) writer.WriteSigned(bitRange, (sbyte)prop.GetValue(value, null));
						else if (prop.PropertyType == typeof(short)) writer.WriteSigned(bitRange, (short)prop.GetValue(value, null));
						else if (prop.PropertyType == typeof(int)) writer.WriteSigned(bitRange, (int)prop.GetValue(value, null));
						else if (prop.PropertyType == typeof(long)) writer.WriteSigned(bitRange, (long)prop.GetValue(value, null));
						else throw CodePath.Unreachable;
					}
					else {
						if (prop.PropertyType == typeof(sbyte)) writer.Write(bitRange, (sbyte)prop.GetValue(value, null));
						else if (prop.PropertyType == typeof(short)) writer.Write(bitRange, (short)prop.GetValue(value, null));
						else if (prop.PropertyType == typeof(int)) writer.Write(bitRange, (int)prop.GetValue(value, null));
						else if (prop.PropertyType == typeof(long)) writer.Write(bitRange, (long)prop.GetValue(value, null));
						else if (prop.PropertyType == typeof(byte)) writer.Write(bitRange, (byte)prop.GetValue(value, null));
						else if (prop.PropertyType == typeof(ushort)) writer.Write(bitRange, (ushort)prop.GetValue(value, null));
						else if (prop.PropertyType == typeof(uint)) writer.Write(bitRange, (uint)prop.GetValue(value, null));
						else if (prop.PropertyType == typeof(ulong)) writer.Write(bitRange, (ulong)prop.GetValue(value, null));
						else throw CodePath.Unreachable;
					}
				}
			}
		}

		private void ReadImperativeBits(PackedBitReader reader, object value, IEnumerable<PropertyInfo> properties, Func<PropertyInfo, BitPair> getBitPair) {
			foreach (PropertyInfo prop in properties) {
				if (prop.PropertyType == typeof(bool)) prop.SetValue(value, reader.ReadBool(), null);
				else {
					BitPair read = getBitPair(prop);
					int bitRange = read.Count;
					if (read.Signed) {
						if (prop.PropertyType == typeof(sbyte)) prop.SetValue(value, reader.ReadSignedInt8(bitRange), null);
						else if (prop.PropertyType == typeof(short)) prop.SetValue(value, reader.ReadSignedInt16(bitRange), null);
						else if (prop.PropertyType == typeof(int)) prop.SetValue(value, reader.ReadSignedInt32(bitRange), null);
						else if (prop.PropertyType == typeof(long)) prop.SetValue(value, reader.ReadSignedInt64(bitRange), null);
						else throw CodePath.Unreachable;
					}
					else {
						if (prop.PropertyType == typeof(sbyte)) prop.SetValue(value, reader.ReadInt8(bitRange), null);
						else if (prop.PropertyType == typeof(short)) prop.SetValue(value, reader.ReadInt16(bitRange), null);
						else if (prop.PropertyType == typeof(int)) prop.SetValue(value, reader.ReadInt32(bitRange), null);
						else if (prop.PropertyType == typeof(long)) prop.SetValue(value, reader.ReadInt64(bitRange), null);
						else if (prop.PropertyType == typeof(byte)) prop.SetValue(value, reader.ReadUInt8(bitRange), null);
						else if (prop.PropertyType == typeof(ushort)) prop.SetValue(value, reader.ReadUInt16(bitRange), null);
						else if (prop.PropertyType == typeof(uint)) prop.SetValue(value, reader.ReadUInt32(bitRange), null);
						else if (prop.PropertyType == typeof(ulong)) prop.SetValue(value, reader.ReadUInt64(bitRange), null);
						else throw CodePath.Unreachable;
					}
				}
			}
		}
	}
}
