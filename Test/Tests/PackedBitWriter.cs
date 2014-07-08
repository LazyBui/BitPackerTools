using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitPackerTools;

namespace Test {
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
			Assert.DoesNotThrow(() => writer.WriteSigned(6, (short)-13));
			Assert.DoesNotThrow(() => writer.WriteSigned(6, (int)-23));
			Assert.DoesNotThrow(() => writer.WriteSigned(8, (long)-33));
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
				1 + 5 + 7 + 7 + 9 +
				2
			) / 8;
			Assert.DoesNotThrow(() => result = writer.ToArray());
			Assert.NotNull(result);
			Assert.Equal(result.Length, byteQuantity);
		}
	}
}
