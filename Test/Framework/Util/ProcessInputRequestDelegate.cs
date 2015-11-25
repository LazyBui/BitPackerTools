using System;
using System.IO;

namespace Test {
	/// <summary>
	/// Represents a request from a process for input.
	/// </summary>
	/// <param name="bufferSinceLastCall">
	/// The current output buffer.
	/// Frequently, this will be called with string.Empty, output from the program, or output based on user input.
	/// All of these states should be accounted for.
	/// </param>
	/// <param name="inputStream">The raw input stream to the process.</param>
	internal delegate ProcessInputHandleResult ProcessInputDelegate(string bufferSinceLastCall, StreamWriter inputStream);
}
