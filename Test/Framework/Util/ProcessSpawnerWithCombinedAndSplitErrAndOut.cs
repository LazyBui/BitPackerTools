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
		/// <param name="pFileName">The name of the file to execute.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(string pFileName) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(pFileName);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(pFileName);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file and command line arguments.
		/// </summary>
		/// <param name="pFileName">The name of the file to execute.</param>
		/// <param name="pArguments">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(string pFileName, params object[] pArguments) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(pFileName, pArguments);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(pFileName, pArguments);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file, command line escaper, and command line arguments.
		/// </summary>
		/// <param name="pFileName">The name of the file to execute.</param>
		/// <param name="pEscaper">The command line escaper to produce a command line argument string from the command line arguments.</param>
		/// <param name="pArguments">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(string pFileName, ICommandLineArgumentEscaper pEscaper, params object[] pArguments) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(pFileName, pEscaper, pArguments);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(pFileName, pEscaper, pArguments);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file.
		/// </summary>
		/// <param name="pFile">The <see cref="FileInfo" /> carrying information about the file to execute.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(FileInfo pFile) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(pFile);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(pFile);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file and command line arguments.
		/// </summary>
		/// <param name="pFile">The <see cref="FileInfo" /> carrying information about the file to execute.</param>
		/// <param name="pArguments">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(FileInfo pFile, params object[] pArguments) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(pFile, pArguments);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(pFile, pArguments);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" /> class with a specified file, command line escaper, and command line arguments.
		/// </summary>
		/// <param name="pFile">The <see cref="FileInfo" /> carrying information about the file to execute.</param>
		/// <param name="pEscaper">The command line escaper to produce a command line argument string from the command line arguments.</param>
		/// <param name="pArguments">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithCombinedAndSplitErrAndOut(FileInfo pFile, ICommandLineArgumentEscaper pEscaper, params object[] pArguments) {
			m_split = new ProcessSpawnerWithSplitErrAndOut(pFile, pEscaper, pArguments);
			m_combined = new ProcessSpawnerWithCombinedErrAndOut(pFile, pEscaper, pArguments);
		}

		private ProcessResult ProduceResult(ProcessResult pSplitResult, ProcessResult pCombinedResult) {
			if (pSplitResult.ExitCode != pCombinedResult.ExitCode) {
				throw new InvalidDataException("Exit codes were not consistent between the two executions");
			}

			// Take the larger run time in order to populate this information
			bool useSplitTimes =
				(pSplitResult.ExitTime - pSplitResult.StartTime) >
				(pCombinedResult.ExitTime - pCombinedResult.StartTime);

			return new ProcessResult(
				pFullOutput: pSplitResult.FullOutput,
				pFullError: pSplitResult.FullError,
				pFullBuffer: pCombinedResult.FullBuffer,
				pExitCode: pCombinedResult.ExitCode,
				pStartTime:
					useSplitTimes ?
					pSplitResult.StartTime :
					pCombinedResult.StartTime,
				pExitTime:
					useSplitTimes ?
					pSplitResult.ExitTime :
					pCombinedResult.ExitTime,
				pPrivilegedProcessorTime:
					pSplitResult.PrivilegedProcessorTime > pCombinedResult.PrivilegedProcessorTime ?
					pSplitResult.PrivilegedProcessorTime :
					pCombinedResult.PrivilegedProcessorTime,
				pUserProcessorTime:
					pSplitResult.UserProcessorTime > pCombinedResult.UserProcessorTime ?
					pSplitResult.UserProcessorTime :
					pCombinedResult.UserProcessorTime,
				pTotalProcessorTime:
					pSplitResult.TotalProcessorTime > pCombinedResult.TotalProcessorTime ?
					pSplitResult.TotalProcessorTime :
					pCombinedResult.TotalProcessorTime,
				pPeakPagedMemorySize: Math.Max(pSplitResult.PeakPagedMemorySize, pCombinedResult.PeakPagedMemorySize),
				pPeakVirtualMemorySize: Math.Max(pSplitResult.PeakVirtualMemorySize, pCombinedResult.PeakVirtualMemorySize),
				pPeakWorkingSet: Math.Max(pSplitResult.PeakWorkingSet, pCombinedResult.PeakWorkingSet)
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
