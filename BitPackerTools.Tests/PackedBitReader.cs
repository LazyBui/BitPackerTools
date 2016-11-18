using System;
using System.Text;
using BitPackerTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BitPackerTools.Tests {
	[TestClass]
	public class PackedBitReaderTest {
		private static readonly byte[] sPackedBitWriterOutput = { 0x9E, 0x4D, 0x0D, 0x39, 0x76, 0x08, 0x48, 0x9E, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x66, 0x06, 0x81, 0x9B, 0x99, 0x60, 0x85, 0x36, 0xBC, 0x84, 0x14, 0x00, 0x00, 0x01, 0x91, 0x85, 0xD3, 0x0E, 0x80, 0x10, 0x00, 0x00, 0x01, 0x91, 0x85, 0xD1, 0x87, };

		[TestMethod]
		public void Constructor() {
			Assert.ThrowsExact<ArgumentNullException>(() => new PackedBitReader(null));
			Assert.ThrowsExact<ArgumentException>(() => new PackedBitReader(new byte[0]));
			Assert.DoesNotThrow(() => new PackedBitReader(new byte[] { 0x22 }));
		}

		[TestMethod]
		public void Basics() {
			PackedBitReader reader = null;
			bool boolValue = false;
			sbyte signedByte = 0;
			byte unsignedByte = 0;
			short signedShort = 0;
			ushort unsignedShort = 0;
			int signedInt = 0;
			uint unsignedInt = 0;
			long signedLong = 0;
			ulong unsignedLong = 0;
			string stringValue = null;
			double doubleValue = 0;
			float floatValue = 0;
			bool success = false;
			Assert.DoesNotThrow(() => reader = new PackedBitReader(sPackedBitWriterOutput));

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.False(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.False(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.False(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.False(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(4, out signedByte));
			Assert.True(success);
			Assert.Equal(signedByte, (sbyte)3);

			Assert.DoesNotThrow(() => success = reader.TryRead(4, out unsignedByte));
			Assert.True(success);
			Assert.Equal(unsignedByte, (byte)4);

			Assert.DoesNotThrow(() => success = reader.TryRead(6, out signedShort));
			Assert.True(success);
			Assert.Equal(signedShort, (short)13);

			Assert.DoesNotThrow(() => success = reader.TryRead(6, out unsignedShort));
			Assert.True(success);
			Assert.Equal(unsignedShort, (ushort)14);

			Assert.DoesNotThrow(() => success = reader.TryRead(6, out signedInt));
			Assert.True(success);
			Assert.Equal(signedInt, (int)23);

			Assert.DoesNotThrow(() => success = reader.TryRead(6, out unsignedInt));
			Assert.True(success);
			Assert.Equal(unsignedInt, (uint)24);

			Assert.DoesNotThrow(() => success = reader.TryRead(8, out signedLong));
			Assert.True(success);
			Assert.Equal(signedLong, (long)33);

			Assert.DoesNotThrow(() => success = reader.TryRead(8, out unsignedLong));
			Assert.True(success);
			Assert.Equal(unsignedLong, (ulong)34);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.False(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.False(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.False(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.True(success);
			Assert.True(boolValue);

			Assert.DoesNotThrow(() => success = reader.TryReadSigned(4, out signedByte));
			Assert.True(success);
			Assert.Equal(signedByte, (sbyte)-3);

			Assert.DoesNotThrow(() => success = reader.TryRead(out doubleValue));
			Assert.True(success);
			Assert.Equal(doubleValue, 2.4d);

			Assert.DoesNotThrow(() => success = reader.TryRead(out floatValue));
			Assert.True(success);
			Assert.Equal(floatValue, 88.4f);

			Assert.DoesNotThrow(() => success = reader.TryReadSigned(6, out signedShort));
			Assert.True(success);
			Assert.Equal(signedShort, (short)-13);

			Assert.DoesNotThrow(() => success = reader.TryReadSigned(6, out signedInt));
			Assert.True(success);
			Assert.Equal(signedInt, (int)-23);

			Assert.DoesNotThrow(() => success = reader.TryReadSigned(8, out signedLong));
			Assert.True(success);
			Assert.Equal(signedLong, (long)-33);

			Assert.DoesNotThrow(() => success = reader.TryRead(out stringValue));
			Assert.True(success);
			Assert.Equal(stringValue, "datà");

			Assert.DoesNotThrow(() => success = reader.TryRead(out stringValue, Encoding.ASCII));
			Assert.True(success);
			Assert.Equal(stringValue, "data");

			Assert.DoesNotThrow(() => success = reader.TryRead(2, out signedByte));
			Assert.True(success);
			Assert.Equal(signedByte, (sbyte)3);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.False(success);
		}

		[TestMethod]
		public void ArgumentValidation() {
			var reader = new PackedBitReader(sPackedBitWriterOutput);

			string output;
			Assert.ThrowsExact<ArgumentNullException>(() => reader.TryRead(out output, null));
		}
	}
}
