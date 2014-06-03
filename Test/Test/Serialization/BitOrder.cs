using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BitPackerTools;
using BitPackerTools.Serialization;
using System.Reflection;

namespace Test {
	[TestClass]
	public class BitOrderTest {
		internal class BitPackingTest {
			[PackedBitOrder(1)] public bool ShouldBeTrue1 { get; set; }
			[PackedBitOrder(2)] public bool ShouldBeTrue2 { get; set; }
			[PackedBitOrder(3)] public bool ShouldBeTrue3 { get; set; }
			[PackedBitOrder(4)] public bool ShouldBeTrue4 { get; set; }
			[PackedBitOrder(5)] public bool ShouldBeTrue5 { get; set; }
			[PackedBitOrder(6)] public bool ShouldBeTrue6 { get; set; }
			[PackedBitOrder(7)] public bool ShouldBeTrue7 { get; set; }
			[PackedBitOrder(8)] public bool ShouldBeTrue8 { get; set; }
			[PackedBitOrder(9)] public bool ShouldBeTrue9 { get; set; }
			[PackedBitOrder(10)] public bool ShouldBeTrue10 { get; set; }
			[PackedBitOrder(11)] public bool ShouldBeTrue11 { get; set; }
			[PackedBitOrder(12)] public bool ShouldBeTrue12 { get; set; }
			[PackedBitOrder(13)] public bool ShouldBeTrue13 { get; set; }
			[PackedBitOrder(14)] public bool ShouldBeTrue14 { get; set; }
			[PackedBitOrder(15)] public bool ShouldBeTrue15 { get; set; }
			[PackedBitOrder(16)] public bool ShouldBeTrue16 { get; set; }
			[PackedBitOrder(17)] public bool ShouldBeTrue17 { get; set; }
			[PackedBitOrder(18)] public bool ShouldBeTrue18 { get; set; }
			[PackedBitOrder(19)] public bool ShouldBeTrue19 { get; set; }
			[PackedBitOrder(20)] public bool ShouldBeTrue20 { get; set; }
			[PackedBitOrder(21)] public bool ShouldBeTrue21 { get; set; }
			[PackedBitOrder(22)] public bool ShouldBeTrue22 { get; set; }
			[PackedBitOrder(23)] public bool ShouldBeTrue23 { get; set; }
			[PackedBitOrder(24)] public bool ShouldBeTrue24 { get; set; }
			[PackedBitOrder(25)] public bool ShouldBeTrue25 { get; set; }
			[PackedBitOrder(26)] public bool ShouldBeTrue26 { get; set; }
			[PackedBitOrder(27)] public bool ShouldBeTrue27 { get; set; }
			[PackedBitOrder(28)] public bool ShouldBeTrue28 { get; set; }
			[PackedBitOrder(29)] public bool ShouldBeTrue29 { get; set; }
			[PackedBitOrder(30)] public bool ShouldBeTrue30 { get; set; }
			[PackedBitOrder(31)] public bool ShouldBeTrue31 { get; set; }
			[PackedBitOrder(32)] public bool ShouldBeTrue32 { get; set; }
			[PackedBitOrder(33)] public bool ShouldBeTrue33 { get; set; }
			[PackedBitOrder(34)] public bool ShouldBeTrue34 { get; set; }
			[PackedBitOrder(35)] public bool ShouldBeTrue35 { get; set; }
			[PackedBitOrder(36)] public bool ShouldBeTrue36 { get; set; }
			[PackedBitOrder(37)] public bool ShouldBeTrue37 { get; set; }
			[PackedBitOrder(38)] public bool ShouldBeTrue38 { get; set; }
			[PackedBitOrder(39)] public bool ShouldBeTrue39 { get; set; }
			[PackedBitOrder(40)] public bool ShouldBeTrue40 { get; set; }
			[PackedBitOrder(41)] public bool ShouldBeTrue41 { get; set; }
			[PackedBitOrder(42)] public bool ShouldBeTrue42 { get; set; }
			[PackedBitOrder(43)] public bool ShouldBeFalse1 { get; set; }
			[PackedBitOrder(44)] public bool ShouldBeTrue43 { get; set; }
			[PackedBitOrder(45)] public bool ShouldBeFalse2 { get; set; }
			[PackedBitOrder(46)] public bool ShouldBeTrue44 { get; set; }
			[PackedBitOrder(47)] public bool ShouldBeTrue45 { get; set; }
			[PackedBitOrder(48)] public bool ShouldBeTrue46 { get; set; }
			[PackedBitOrder(49)] public bool ShouldBeTrue47 { get; set; }
			[PackedBitOrder(50)] public bool ShouldBeFalse3 { get; set; }
			[PackedBitOrder(51)] public bool ShouldBeTrue48 { get; set; }
			[PackedBitOrder(52)] public bool ShouldBeTrue49 { get; set; }
			[PackedBitOrder(53)] public bool ShouldBeTrue50 { get; set; }
			[PackedBitOrder(54)] public bool ShouldBeTrue51 { get; set; }
			[PackedBitOrder(55)] public bool ShouldBeTrue52 { get; set; }
			[PackedBitOrder(56)] public bool ShouldBeTrue53 { get; set; }
			[PackedBitOrder(57)] public bool ShouldBeTrue54 { get; set; }
			[PackedBitOrder(58)] public bool ShouldBeTrue55 { get; set; }
			[PackedBitOrder(59)] public bool ShouldBeFalse4 { get; set; }
			[PackedBitOrder(60, 5)] public byte ShouldBe0 { get; set; }
			[PackedBitOrder(65)] public bool ShouldBeTrue56 { get; set; }
			[PackedBitOrder(66)] public bool ShouldBeTrue57 { get; set; }
			[PackedBitOrder(67)] public bool ShouldBeFalse5 { get; set; }
			[PackedBitOrder(68)] public bool ShouldBeFalse6 { get; set; }
			[PackedBitOrder(69, 3)] public byte ShouldBe4 { get; set; }
			[PackedBitOrder(72)] public bool ShouldBeTrue58 { get; set; }
			[PackedBitOrder(73)] public bool ShouldBeTrue59 { get; set; }
			[PackedBitOrder(74)] public bool ShouldBeTrue60 { get; set; }
			[PackedBitOrder(75)] public bool ShouldBeTrue61 { get; set; }
			[PackedBitOrder(76)] public bool ShouldBeTrue62 { get; set; }
			[PackedBitOrder(77)] public bool ShouldBeTrue63 { get; set; }
			[PackedBitOrder(78)] public bool ShouldBeTrue64 { get; set; }
			[PackedBitOrder(79)] public bool ShouldBeFalse7 { get; set; }
			[PackedBitOrder(80)] public bool ShouldBeTrue65 { get; set; }
			[PackedBitOrder(81)] public bool ShouldBeTrue66 { get; set; }
			[PackedBitOrder(82)] public bool ShouldBeFalse8 { get; set; }
			[PackedBitOrder(83)] public bool ShouldBeTrue67 { get; set; }
			[PackedBitOrder(84)] public bool ShouldBeFalse9 { get; set; }
			[PackedBitOrder(85)] public bool ShouldBeFalse10 { get; set; }
			[PackedBitOrder(86)] public bool ShouldBeFalse11 { get; set; }
			[PackedBitOrder(87)] public bool ShouldBeTrue68 { get; set; }
			[PackedBitOrder(88)] public bool ShouldBeTrue69 { get; set; }
			[PackedBitOrder(89, 32)] public uint ShouldBe0Second { get; set; }
			[PackedBitOrder(121, 4)] public byte ShouldBe0Third { get; set; }
			[PackedBitOrder(125)] public bool ShouldBeTrue70 { get; set; }
			[PackedBitOrder(126)] public bool ShouldBeTrue71 { get; set; }
			[PackedBitOrder(127)] public bool ShouldBeFalse12 { get; set; }
			[PackedBitOrder(128)] public bool ShouldBeTrue72 { get; set; }
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
