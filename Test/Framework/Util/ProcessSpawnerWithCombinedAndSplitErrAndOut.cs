using System;
using System.IO;

namespace Test {
	/// <summary>
	/// Executes 2 <see cref="System.Diagnostics.Process" /> objects in order to get sequentially consistent output (as you would see in a regular console execution) and distinct output for assertion purposes. 
	/// <see cref="System.Diagnostics.Process" /> has a limitation where these two things cannot be obtained at the same time and it may be a limitation of Windows itself.
	/// The fact that it executes 2 processes means that it is slower than the alternatives.
	/// If you do not need both outputs, it is recommended to use <see cref="Test.ProcessSpawnerWithCombinedErrAndOut" /> or <see cref="Test.ProcessSpawnerWithSplitErrAndOut" />.
	/// </summary>
	internal sealed class ProcessSpawnerWithCombinedAndSplitErrAndOut : IProcessSpawner {
		private ProcessSpawnerWithSplitErrAndOut m_split = null;
		private ProcessSpawnerWithCombinedErrAndOut m_combined = null;

		/// <summary>
		/// Indicates that the process has started.
		/// </summary>
		public bool Started { get; private set; }
		/// <summary>
		/// Indicates that the process has completed.
		/// </summary>
		public bool Exited { get; private set; }
		/// <summary>
		/// Is called when the process is asking for input.
		/// Frequently, this will be called with a buffer of string.Empty, output from the program, or output based on user input.
		/// All of these states should be accounted for.
		/// </summary>
		public event ProcessInputDelegate OnInputRequested;

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file.
		/// </summary>
		/// <param name="file">The name of the file to execute.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(string file) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(file);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(file);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file and command line arguments.
		/// </summary>
		/// <param name="file">The name of the file to execute.</param>
		/// <param name="args">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(string file, params object[] args) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(file, args);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(file, args);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file, command line escaper, and command line arguments.
		/// </summary>
		/// <param name="file">The name of the file to execute.</param>
		/// <param name="escaper">The command line escaper to produce a command line argument string from the command line arguments.</param>
		/// <param name="args">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(string file, ICommandLineArgumentEscaper escaper, params object[] args) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(file, escaper, args);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(file, escaper, args);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> carrying information about the file to execute.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(FileInfo file) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(file);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(file);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file and command line arguments.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> carrying information about the file to execute.</param>
		/// <param name="args">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(FileInfo file, params object[] args) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(file, args);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(file, args);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file, command line escaper, and command line arguments.
		/// </summary>
		/// <param name="file">The <see cref="FileInfo" /> carrying information about the file to execute.</param>
		/// <param name="escaper">The command line escaper to produce a command line argument string from the command line arguments.</param>
		/// <param name="args">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(FileInfo file, ICommandLineArgumentEscaper escaper, params object[] args) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(file, escaper, args);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(file, escaper, args);
		}

		private ProcessResult ProduceResult(ProcessResult splitResult, ProcessResult combinedResult) {
			if (splitResult.ExitCode != combinedResult.ExitCode) {
				throw new InvalidDataException("Exit codes were not consistent between the two executions");
			}

			// Take the larger run time in order to populate this information
			bool useSplitTimes =
				(splitResult.ExitTime - splitResult.StartTime) >
				(combinedResult.ExitTime - combinedResult.StartTime);

			return new ProcessResult(
				stdOutput: splitResult.FullStd,
				errorOutput: splitResult.FullError,
				fullOutput: combinedResult.FullOutput,
				exitCode: combinedResult.ExitCode,
				startTime:
					useSplitTimes ?
					splitResult.StartTime :
					combinedResult.StartTime,
				exitTime:
					useSplitTimes ?
					splitResult.ExitTime :
					combinedResult.ExitTime,
				privilegedProcessorTime:
					splitResult.PrivilegedProcessorTime > combinedResult.PrivilegedProcessorTime ?
					splitResult.PrivilegedProcessorTime :
					combinedResult.PrivilegedProcessorTime,
				userProcessorTime:
					splitResult.UserProcessorTime > combinedResult.UserProcessorTime ?
					splitResult.UserProcessorTime :
					combinedResult.UserProcessorTime,
				totalProcessorTime:
					splitResult.TotalProcessorTime > combinedResult.TotalProcessorTime ?
					splitResult.TotalProcessorTime :
					combinedResult.TotalProcessorTime,
				peakPagedMemorySize: Math.Max(splitResult.PeakPagedMemorySize, combinedResult.PeakPagedMemorySize),
				peakVirtualMemorySize: Math.Max(splitResult.PeakVirtualMemorySize, combinedResult.PeakVirtualMemorySize),
				peakWorkingSet: Math.Max(splitResult.PeakWorkingSet, combinedResult.PeakWorkingSet)
			);
		}

		private void AssociateEvents() {
			if (OnInputRequested != null) {
				m_combined.OnInputRequested += OnInputRequested;
				m_split.OnInputRequested += OnInputRequested;
			}
		}

		/// <summary>
		/// Executes the specified process.
		/// </summary>
		/// <returns>A <see cref="Test.ProcessResult" /> object representing the execution.</returns>
		public ProcessResult Run() {
			if (Exited) throw new InvalidOperationException("Must not execute the process twice");
			if (Started) throw new InvalidOperationException("Must not execute the process twice");

			AssociateEvents();

			Started = true;
			var splitResult = m_split.Run();
			var combinedResult = m_combined.Run();
			Exited = true;
			return ProduceResult(splitResult, combinedResult);
		}

		/// <summary>
		/// Releases all resources used by the current instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class.
		/// </summary>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing) {
			if (disposing) {
				m_split.Dispose();
				m_combined.Dispose();
			}
		}
	}
}
