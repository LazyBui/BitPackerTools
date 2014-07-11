using System;

namespace BitPackerTools {
	internal static class PackedBitMasks {
		private static byte[] sBitMasks;

		public static byte GetNarrowingMask(int pBitCount) {
			return sBitMasks[pBitCount];
		}

		public static byte GenerateWideningMask(int pBitsToGet, int pStartOffset) {
			byte bitMask = sBitMasks[pBitsToGet];
			bitMask <<= (Constants.BitsInByte - pBitsToGet);
			bitMask >>= pStartOffset;
			return bitMask;
		}

		static PackedBitMasks() {
			// Bit 0 isn't used
			int maskCount = Constants.ByteSizeInBits + 1;
			sBitMasks = new byte[maskCount];

			for (int i = 1; i < maskCount; i++) {
				byte bitMask = 0;
				int bitCount = 0;
				for (byte j = 1; bitCount < i; j <<= 1, bitCount++) {
					bitMask |= j;
				}
				sBitMasks[i] = bitMask;
			}
		}
	}
}
