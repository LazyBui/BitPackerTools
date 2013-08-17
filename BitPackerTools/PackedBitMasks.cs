
namespace BitPackerTools {
	internal static class PackedBitMasks {
		private const int ByteSizeInBits = sizeof(byte) * 8;
		private static byte[] sBitNarrowingMasks;
		private static byte[] sBitWideningMasks;

		public static byte GetNarrowingMask(int pBitCount) {
			return sBitNarrowingMasks[pBitCount];
		}

		public static byte GenerateWideningMask(int pBitsToGet, int pStartOffset) {
			byte bitMask = 0;
			int bitCount = 0;
			for (byte j = 1; bitCount < pBitsToGet; j <<= 1, bitCount++) {
				bitMask |= j;
			}

			bitMask <<= (8 - pBitsToGet);
			bitMask >>= pStartOffset;
			return bitMask;
		}

		static PackedBitMasks() {
			// Bit 0 isn't used
			int maskCount = ByteSizeInBits + 1;
			sBitNarrowingMasks = new byte[maskCount];
			sBitWideningMasks = new byte[maskCount];

			for (int i = 1; i < maskCount; i++) {
				byte bitMask = 0;
				int bitCount = 0;
				for (byte j = 1; bitCount < i; j <<= 1, bitCount++) {
					bitMask |= j;
				}
				sBitNarrowingMasks[i] = bitMask;
				sBitWideningMasks[i] = (byte)(0xFF - bitMask);
			}
		}
	}
}
