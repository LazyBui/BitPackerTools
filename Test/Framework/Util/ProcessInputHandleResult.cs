using System;
using System.IO;

namespace Test {
	/// <summary>
	/// Represents whether input was handled.
	/// </summary>
	internal enum ProcessInputHandleResult {
		/// <summary>
		/// Indicates that input was given and the output buffer should be cleared.
		/// </summary>
		Handled,
		/// <summary>
		/// Indicates that no input was given and the output buffer should not be cleared.
		/// </summary>
		Ignored,
	}
}
