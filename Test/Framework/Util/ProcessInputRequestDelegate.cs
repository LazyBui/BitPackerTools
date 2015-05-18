using System;
using System.IO;

namespace Test {
	/// <summary>
	/// Represents a request from a process for input.
	/// </summary>
	/// <param name="pBufferSinceLastCall">
	/// The current output buffer.
	/// Frequently, this will be called with string.Empty, output from the program, or output based on user input.
	/// All of these states should be accounted for.
	/// </param>
	/// <param name="pInputStream">The raw input stream to the process.</param>
	internal delegate ProcessInputHandleResult ProcessInputDelegate(string pBufferSinceLastCall, StreamWriter pInputStream);
}
