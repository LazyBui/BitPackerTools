using System;
using System.Linq;
using System.Text;
using BitPackerTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BitPackerTools.Tests {
	[TestClass]
	public class PackedBitWriterTest {
		[TestMethod]
		public void Basics() {
			PackedBitWriter writer = null;
			Assert.DoesNotThrow(() => writer = new PackedBitWriter());
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.Write(false));
			Assert.DoesNotThrow(() => writer.Write(false));
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.Write(false));
			Assert.DoesNotThrow(() => writer.Write(false));
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.Write(4, (sbyte)3));
			Assert.DoesNotThrow(() => writer.Write(4, (byte)4));
			Assert.DoesNotThrow(() => writer.Write(6, (short)13));
			Assert.DoesNotThrow(() => writer.Write(6, (ushort)14));
			Assert.DoesNotThrow(() => writer.Write(6, (int)23));
			Assert.DoesNotThrow(() => writer.Write(6, (uint)24));
			Assert.DoesNotThrow(() => writer.Write(8, (long)33));
			Assert.DoesNotThrow(() => writer.Write(8, (ulong)34));
			Assert.DoesNotThrow(() => writer.Write(false));
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.Write(false));
			Assert.DoesNotThrow(() => writer.Write(false));
			Assert.DoesNotThrow(() => writer.Write(true));
			Assert.DoesNotThrow(() => writer.WriteSigned(4, (sbyte)-3));
			Assert.DoesNotThrow(() => writer.Write(2.4d));
			Assert.DoesNotThrow(() => writer.Write(88.4f));
			Assert.DoesNotThrow(() => writer.WriteSigned(6, (short)-13));
			Assert.DoesNotThrow(() => writer.WriteSigned(6, (int)-23));
			Assert.DoesNotThrow(() => writer.WriteSigned(8, (long)-33));
			Assert.DoesNotThrow(() => writer.Write("datà"));
			Assert.DoesNotThrow(() => writer.Write("data", Encoding.ASCII));
			Assert.DoesNotThrow(() => writer.Write(2, (sbyte)3));

			byte[] result = null;
			// byteQuantity must have an exact bit count in order to be correct
			// Please ensure that the test above produces an exact bit count
			// Please also ensure that PackedBitReader is updated upon changes here
			int byteQuantity = (
				1 + 1 + 1 + 1 + 1 +
				1 + 1 + 1 + 1 + 1 +
				4 + 4 + 6 + 6 + 6 +
				6 + 8 + 8 + 1 + 1 +
				1 + 1 + 1 + 1 + 1 +
				1 + 64 + 32 + 5 + 7 + 7 + 9 +
				// datà should be 5 bytes in UTF8
				// length should always be 32 bits
				32 + 8 + 8 + 8 + 8 + 8 +
				// Since ASCII is 7-bit, should be chars = bytes
				32 + 8 + 8 + 8 + 8 +
				2
			) / Constants.BitsInByte;
			Assert.DoesNotThrow(() => result = writer.ToArray());
			Assert.NotNull(result);
			Assert.Equal(result.Length, byteQuantity);

			var hex = string.Join(
				", ",
				result.Select(v => "0x" + v.ToString("X2")));
		}

		[TestMethod]
		public void ArgumentValidation() {
			PackedBitWriter writer = new PackedBitWriter();

			Assert.ThrowsExact<BitCountException>(() => writer.Write(-1, byte.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(-1, ushort.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(-1, uint.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(-1, ulong.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(0, byte.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(0, ushort.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(0, uint.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(0, ulong.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(65, byte.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(65, ushort.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(65, uint.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(65, ulong.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(-1, sbyte.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(-1, short.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(-1, int.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(-1, long.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(0, sbyte.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(0, short.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(0, int.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(0, long.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(65, sbyte.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(65, short.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(65, int.MaxValue));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(65, long.MaxValue));

			Assert.ThrowsExact<ArgumentNullException>(() => writer.Write(null as string));
			Assert.ThrowsExact<ArgumentNullException>(() => writer.Write(null as string, Encoding.UTF8));
			Assert.ThrowsExact<ArgumentNullException>(() => writer.Write("string", null));

			Assert.ThrowsExact<BitCountException>(() => writer.Write(-1, new byte[] { 0 }));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(0, new byte[] { 0 }));
			Assert.ThrowsExact<BitCountException>(() => writer.Write(65, new byte[] { 0 }));

			Assert.ThrowsExact<ArgumentNullException>(() => writer.Write(1, null as byte[]));
			Assert.ThrowsExact<ArgumentException>(() => writer.Write(1, new byte[0]));
			Assert.ThrowsExact<ArgumentException>(() => writer.Write(1, new byte[0]));
		}
	}
}
