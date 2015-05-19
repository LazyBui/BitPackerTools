using System;

namespace BitPackerTools {
	internal static class CodePath {
		public static Exception Unreachable { get { return new InvalidOperationException("This code location should be unreachable."); } }
	}
}
