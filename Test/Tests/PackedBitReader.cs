using System;
using BitPackerTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class PackedBitReaderTest {
		[TestMethod]
		public void Constructor() {
			Assert.ThrowsExact<ArgumentNullException>(() => new PackedBitReader(null));
			Assert.ThrowsExact<ArgumentException>(() => new PackedBitReader(new byte[] { }));
			Assert.DoesNotThrow(() => new PackedBitReader(new byte[] { 0x22 }));
		}

		[TestMethod]
		public void Basics() {
			PackedBitReader reader = null;
			// This is the same as the output of the PackedBitWriter test, so should have the same outputs as the inputs
			byte[] raw = { 0x9E, 0x4D, 0x0D, 0x39, 0x76, 0x08, 0x48, 0x9E, 0x67, 0x36, 0xBC, 0x87, };
			bool boolValue = false;
			sbyte signedByte = 0;
			byte unsignedByte = 0;
			short signedShort = 0;
			ushort unsignedShort = 0;
			int signedInt = 0;
			uint unsignedInt = 0;
			long signedLong = 0;
			ulong unsignedLong = 0;
			bool success = false;
			Assert.DoesNotThrow(() => reader = new PackedBitReader(raw));

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

			Assert.DoesNotThrow(() => success = reader.TryReadSigned(6, out signedShort));
			Assert.True(success);
			Assert.Equal(signedShort, (short)-13);

			Assert.DoesNotThrow(() => success = reader.TryReadSigned(6, out signedInt));
			Assert.True(success);
			Assert.Equal(signedInt, (int)-23);

			Assert.DoesNotThrow(() => success = reader.TryReadSigned(8, out signedLong));
			Assert.True(success);
			Assert.Equal(signedLong, (long)-33);

			Assert.DoesNotThrow(() => success = reader.TryRead(2, out signedByte));
			Assert.True(success);
			Assert.Equal(signedByte, (sbyte)3);

			Assert.DoesNotThrow(() => success = reader.TryRead(out boolValue));
			Assert.False(success);
		}
	}
}
