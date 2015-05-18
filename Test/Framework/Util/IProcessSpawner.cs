using System;

namespace Test {
	/// <summary>
	/// Represents an execution of a <see cref="System.Diagnostics.Process" /> object and obtains meaningful information from it to allow assertions on execution properties.
	/// </summary>
	internal interface IProcessSpawner : IDisposable {
		/// <summary>
		/// Indicates that the process has started.
		/// </summary>
		bool Started { get; }
		/// <summary>
		/// Indicates that the process has completed.
		/// </summary>
		bool Exited { get; }
		/// <summary>
		/// Is called when the process is asking for input.
		/// Frequently, this will be called with a buffer of string.Empty, output from the program, or output based on user input.
		/// All of these states should be accounted for.
		/// </summary>
		event ProcessInputDelegate OnInputRequested;

		/// <summary>
		/// Executes the specified process.
		/// </summary>
		/// <returns>A <see cref="Test.ProcessResult" /> object representing the execution.</returns>
		ProcessResult Run();
	}
}
