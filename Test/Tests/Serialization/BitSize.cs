using System;
using System.Linq;
using System.Reflection;
using BitPackerTools;
using BitPackerTools.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class BitSizeTest {
		internal class BitPackingTest {
			[PackedBitSize] public bool ShouldBeTrue1 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue2 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue3 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue4 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue5 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue6 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue7 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue8 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue9 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue10 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue11 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue12 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue13 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue14 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue15 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue16 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue17 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue18 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue19 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue20 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue21 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue22 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue23 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue24 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue25 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue26 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue27 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue28 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue29 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue30 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue31 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue32 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue33 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue34 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue35 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue36 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue37 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue38 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue39 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue40 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue41 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue42 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse1 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue43 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse2 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue44 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue45 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue46 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue47 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse3 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue48 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue49 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue50 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue51 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue52 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue53 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue54 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue55 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse4 { get; set; }
			[PackedBitSize(5)] public byte ShouldBe0 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue56 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue57 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse5 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse6 { get; set; }
			[PackedBitSize(3)] public byte ShouldBe4 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue58 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue59 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue60 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue61 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue62 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue63 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue64 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse7 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue65 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue66 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse8 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue67 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse9 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse10 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse11 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue68 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue69 { get; set; }
			[PackedBitSize(32)] public uint ShouldBe0Second { get; set; }
			[PackedBitSize(4)] public byte ShouldBe0Third { get; set; }
			[PackedBitSize] public bool ShouldBeTrue70 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue71 { get; set; }
			[PackedBitSize] public bool ShouldBeFalse12 { get; set; }
			[PackedBitSize] public bool ShouldBeTrue72 { get; set; }
		}

		[TestMethod]
		public void LargeClassTest() {
			byte[] array = {
				0xFF, 0xFF, 0xFF, 0xFF,
				0xFF, 0xD7, 0xBF, 0xC0,
				0xC9, 0xFD, 0xA3, 0x00,
				0x00, 0x00, 0x00, 0x0D,
			};

			var reader = new PackedBitReader(array);
			var serializer = new PackedBitSerializer(typeof(BitPackingTest));
			var result = serializer.Deserialize(reader) as BitPackingTest;

			var trueValues = typeof(BitPackingTest).
				GetProperties(BindingFlags.Instance | BindingFlags.Public).
				Where(p => p.Name.StartsWith("ShouldBeTrue")).
				Select(p => Tuple.Create(p.Name, (bool)p.GetValue(result)));

			Assert.All(trueValues, tv => tv.Item2);

			var falseValues = typeof(BitPackingTest).
				GetProperties(BindingFlags.Instance | BindingFlags.Public).
				Where(p => p.Name.StartsWith("ShouldBeFalse")).
				Select(p => Tuple.Create(p.Name, (bool)p.GetValue(result)));

			Assert.All(falseValues, fv => !fv.Item2);

			Assert.Equal(result.ShouldBe0, (byte)0);
			Assert.Equal(result.ShouldBe0Second, (uint)0);
			Assert.Equal(result.ShouldBe0Third, (byte)0);
			Assert.Equal(result.ShouldBe4, (byte)4);
		}
	}
}
