﻿using System;
using System.Collections.Generic;
using BitPackerTools;
using BitPackerTools.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = TestLib.Framework.Assert;

namespace BitPackerTools.Tests.Serialization {
	[TestClass]
	public class PackedBitSerializerTest {
		internal class NonImplementingType { }

		internal class NonImplementingTypeWithProperty { public int A { get; set; } }

		internal class ImplementingType : IPackedBitSerializable {
			public void Deserialize(PackedBitReader reader) {
				throw new NotImplementedException();
			}
			public void Serialize(PackedBitWriter reader) {
				throw new NotImplementedException();
			}
		}

		internal class ImplementingTypeWithProperty : IPackedBitSerializable {
			public int A { get; set; }
			public void Deserialize(PackedBitReader reader) {
				throw new NotImplementedException();
			}
			public void Serialize(PackedBitWriter reader) {
				throw new NotImplementedException();
			}
		}

		internal class ImplementingTypeWithStringProperty : IPackedBitSerializable {
			public string A { get; set; }
			public void Deserialize(PackedBitReader reader) {
				throw new NotImplementedException();
			}
			public void Serialize(PackedBitWriter reader) {
				throw new NotImplementedException();
			}
		}

		internal class NonImplementingTypeWithAttribute {
			[PackedBitSize]
			public int A { get; set; }
		}

		internal class NonImplementingTypeWithConflictingAttribute {
			[PackedBitRange(0)]
			public int A { get; set; }
			[PackedBitSize]
			public int B { get; set; }
		}

		internal class NonImplementingTypeWithString {
			[PackedBitSize]
			public int A { get; set; }
			[PackedBitSize]
			public string B { get; set; }
		}

		internal class NonImplementingTypeWithList {
			[PackedBitSize]
			public int A { get; set; }
			[PackedBitSize]
			public List<string> B { get; set; }
		}

		[TestMethod]
		public void Constructor() {
			Assert.ThrowsExact<ArgumentNullException>(() => new PackedBitSerializer(null));
			Assert.ThrowsExact<ArgumentException>(() => new PackedBitSerializer(typeof(NonImplementingType)));
			Assert.ThrowsExact<ArgumentException>(() => new PackedBitSerializer(typeof(NonImplementingTypeWithProperty)));
			Assert.DoesNotThrow(() => new PackedBitSerializer(typeof(ImplementingType)));
			Assert.DoesNotThrow(() => new PackedBitSerializer(typeof(ImplementingTypeWithProperty)));
			Assert.DoesNotThrow(() => new PackedBitSerializer(typeof(NonImplementingTypeWithAttribute)));
			Assert.ThrowsExact<ArgumentException>(() => new PackedBitSerializer(typeof(NonImplementingTypeWithConflictingAttribute)));
			Assert.ThrowsExact<ArgumentException>(() => new PackedBitSerializer(typeof(NonImplementingTypeWithString)));
			Assert.ThrowsExact<ArgumentException>(() => new PackedBitSerializer(typeof(NonImplementingTypeWithList)));
			Assert.DoesNotThrow(() => new PackedBitSerializer(typeof(ImplementingTypeWithStringProperty)));
		}

		[TestMethod]
		public void ArgumentValidation() {
			var inst = new PackedBitSerializer(typeof(ImplementingType));

			Assert.ThrowsExact<ArgumentNullException>(() => inst.Deserialize(null));
			Assert.ThrowsExact<ArgumentNullException>(() => inst.Serialize(null, true));
			Assert.ThrowsExact<ArgumentNullException>(() => inst.Serialize(new PackedBitWriter(), null as string));
		}
	}
}
