using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitPackerTools;
using BitPackerTools.Serialization;
using System.Reflection;

namespace Test {
	[TestClass]
	public class BitRangeTest {
		internal class BitPackingTest {
			[PackedBitRange(1)] public bool ShouldBeTrue1 { get; set; }
			[PackedBitRange(2)] public bool ShouldBeTrue2 { get; set; }
			[PackedBitRange(3)] public bool ShouldBeTrue3 { get; set; }
			[PackedBitRange(4)] public bool ShouldBeTrue4 { get; set; }
			[PackedBitRange(5)] public bool ShouldBeTrue5 { get; set; }
			[PackedBitRange(6)] public bool ShouldBeTrue6 { get; set; }
			[PackedBitRange(7)] public bool ShouldBeTrue7 { get; set; }
			[PackedBitRange(8)] public bool ShouldBeTrue8 { get; set; }
			[PackedBitRange(9)] public bool ShouldBeTrue9 { get; set; }
			[PackedBitRange(10)] public bool ShouldBeTrue10 { get; set; }
			[PackedBitRange(11)] public bool ShouldBeTrue11 { get; set; }
			[PackedBitRange(12)] public bool ShouldBeTrue12 { get; set; }
			[PackedBitRange(13)] public bool ShouldBeTrue13 { get; set; }
			[PackedBitRange(14)] public bool ShouldBeTrue14 { get; set; }
			[PackedBitRange(15)] public bool ShouldBeTrue15 { get; set; }
			[PackedBitRange(16)] public bool ShouldBeTrue16 { get; set; }
			[PackedBitRange(17)] public bool ShouldBeTrue17 { get; set; }
			[PackedBitRange(18)] public bool ShouldBeTrue18 { get; set; }
			[PackedBitRange(19)] public bool ShouldBeTrue19 { get; set; }
			[PackedBitRange(20)] public bool ShouldBeTrue20 { get; set; }
			[PackedBitRange(21)] public bool ShouldBeTrue21 { get; set; }
			[PackedBitRange(22)] public bool ShouldBeTrue22 { get; set; }
			[PackedBitRange(23)] public bool ShouldBeTrue23 { get; set; }
			[PackedBitRange(24)] public bool ShouldBeTrue24 { get; set; }
			[PackedBitRange(25)] public bool ShouldBeTrue25 { get; set; }
			[PackedBitRange(26)] public bool ShouldBeTrue26 { get; set; }
			[PackedBitRange(27)] public bool ShouldBeTrue27 { get; set; }
			[PackedBitRange(28)] public bool ShouldBeTrue28 { get; set; }
			[PackedBitRange(29)] public bool ShouldBeTrue29 { get; set; }
			[PackedBitRange(30)] public bool ShouldBeTrue30 { get; set; }
			[PackedBitRange(31)] public bool ShouldBeTrue31 { get; set; }
			[PackedBitRange(32)] public bool ShouldBeTrue32 { get; set; }
			[PackedBitRange(33)] public bool ShouldBeTrue33 { get; set; }
			[PackedBitRange(34)] public bool ShouldBeTrue34 { get; set; }
			[PackedBitRange(35)] public bool ShouldBeTrue35 { get; set; }
			[PackedBitRange(36)] public bool ShouldBeTrue36 { get; set; }
			[PackedBitRange(37)] public bool ShouldBeTrue37 { get; set; }
			[PackedBitRange(38)] public bool ShouldBeTrue38 { get; set; }
			[PackedBitRange(39)] public bool ShouldBeTrue39 { get; set; }
			[PackedBitRange(40)] public bool ShouldBeTrue40 { get; set; }
			[PackedBitRange(41)] public bool ShouldBeTrue41 { get; set; }
			[PackedBitRange(42)] public bool ShouldBeTrue42 { get; set; }
			[PackedBitRange(43)] public bool ShouldBeFalse1 { get; set; }
			[PackedBitRange(44)] public bool ShouldBeTrue43 { get; set; }
			[PackedBitRange(45)] public bool ShouldBeFalse2 { get; set; }
			[PackedBitRange(46)] public bool ShouldBeTrue44 { get; set; }
			[PackedBitRange(47)] public bool ShouldBeTrue45 { get; set; }
			[PackedBitRange(48)] public bool ShouldBeTrue46 { get; set; }
			[PackedBitRange(49)] public bool ShouldBeTrue47 { get; set; }
			[PackedBitRange(50)] public bool ShouldBeFalse3 { get; set; }
			[PackedBitRange(51)] public bool ShouldBeTrue48 { get; set; }
			[PackedBitRange(52)] public bool ShouldBeTrue49 { get; set; }
			[PackedBitRange(53)] public bool ShouldBeTrue50 { get; set; }
			[PackedBitRange(54)] public bool ShouldBeTrue51 { get; set; }
			[PackedBitRange(55)] public bool ShouldBeTrue52 { get; set; }
			[PackedBitRange(56)] public bool ShouldBeTrue53 { get; set; }
			[PackedBitRange(57)] public bool ShouldBeTrue54 { get; set; }
			[PackedBitRange(58)] public bool ShouldBeTrue55 { get; set; }
			[PackedBitRange(59)] public bool ShouldBeFalse4 { get; set; }
			[PackedBitRange(60, 64)] public byte ShouldBe0 { get; set; }
			[PackedBitRange(65)] public bool ShouldBeTrue56 { get; set; }
			[PackedBitRange(66)] public bool ShouldBeTrue57 { get; set; }
			[PackedBitRange(67)] public bool ShouldBeFalse5 { get; set; }
			[PackedBitRange(68)] public bool ShouldBeFalse6 { get; set; }
			[PackedBitRange(69, 71)] public byte ShouldBe4 { get; set; }
			[PackedBitRange(72)] public bool ShouldBeTrue58 { get; set; }
			[PackedBitRange(73)] public bool ShouldBeTrue59 { get; set; }
			[PackedBitRange(74)] public bool ShouldBeTrue60 { get; set; }
			[PackedBitRange(75)] public bool ShouldBeTrue61 { get; set; }
			[PackedBitRange(76)] public bool ShouldBeTrue62 { get; set; }
			[PackedBitRange(77)] public bool ShouldBeTrue63 { get; set; }
			[PackedBitRange(78)] public bool ShouldBeTrue64 { get; set; }
			[PackedBitRange(79)] public bool ShouldBeFalse7 { get; set; }
			[PackedBitRange(80)] public bool ShouldBeTrue65 { get; set; }
			[PackedBitRange(81)] public bool ShouldBeTrue66 { get; set; }
			[PackedBitRange(82)] public bool ShouldBeFalse8 { get; set; }
			[PackedBitRange(83)] public bool ShouldBeTrue67 { get; set; }
			[PackedBitRange(84)] public bool ShouldBeFalse9 { get; set; }
			[PackedBitRange(85)] public bool ShouldBeFalse10 { get; set; }
			[PackedBitRange(86)] public bool ShouldBeFalse11 { get; set; }
			[PackedBitRange(87)] public bool ShouldBeTrue68 { get; set; }
			[PackedBitRange(88)] public bool ShouldBeTrue69 { get; set; }
			[PackedBitRange(89, 120)] public uint ShouldBe0Second { get; set; }
			[PackedBitRange(121, 124)] public byte ShouldBe0Third { get; set; }
			[PackedBitRange(125)] public bool ShouldBeTrue70 { get; set; }
			[PackedBitRange(126)] public bool ShouldBeTrue71 { get; set; }
			[PackedBitRange(127)] public bool ShouldBeFalse12 { get; set; }
			[PackedBitRange(128)] public bool ShouldBeTrue72 { get; set; }
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
