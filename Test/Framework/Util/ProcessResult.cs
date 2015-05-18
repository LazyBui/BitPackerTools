using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Test {
	/// <summary>
	/// The result of a <see cref="System.Diagnostics.Process" /> execution.
	/// </summary>
	internal sealed class ProcessResult {
		/// <summary>
		/// Gets the full stdout stream.
		/// </summary>
		public string FullOutput { get; private set; }
		/// <summary>
		/// Gets the full stderr stream.
		/// </summary>
		public string FullError { get; private set; }
		/// <summary>
		/// Gets the full output as you would expect to see in a console execution.
		/// </summary>
		public string FullBuffer { get; private set; }
		/// <summary>
		/// Gets the exit code.
		/// </summary>
		public int ExitCode { get; private set; }
		/// <summary>
		/// Gets the start time of the process.
		/// </summary>
		public DateTime StartTime { get; private set; }
		/// <summary>
		/// Gets the exit time of the process.
		/// </summary>
		public DateTime ExitTime { get; private set; }
		/// <summary>
		/// Gets the amount of time the process has spent running code inside the operating system core.
		/// </summary>
		public TimeSpan PrivilegedProcessorTime { get; private set; }
		/// <summary>
		/// Gets the amount of time the process has spent running code inside the application.
		/// </summary>
		public TimeSpan UserProcessorTime { get; private set; }
		/// <summary>
		/// Gets the sum of UserProcessorTime and PrivilegedProcessorTime.
		/// </summary>
		public TimeSpan TotalProcessorTime { get; private set; }
		/// <summary>
		/// Gets the maximum amount of memory used in the virtual memory paging file.
		/// </summary>
		public long PeakPagedMemorySize { get; private set; }
		/// <summary>
		/// Gets the maximum amount of virtual memory used by the process (should be greater than or equal to PeakPagedMemorySize).
		/// </summary>
		public long PeakVirtualMemorySize { get; private set; }
		/// <summary>
		/// Gets the maximum amount of physical memory used by the process.
		/// </summary>
		public long PeakWorkingSet { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessResult" /> class with the specified information.
		/// </summary>
		/// <param name="pFullOutput">The full stdout stream.</param>
		/// <param name="pFullError">The full stderr stream.</param>
		/// <param name="pFullBuffer">The full output as you would expect to see in a console execution.</param>
		/// <param name="pExitCode">The exit code.</param>
		/// <param name="pStartTime">The start time of the process.</param>
		/// <param name="pExitTime">The exit time of the process.</param>
		/// <param name="pPrivilegedProcessorTime">The amount of time the process has spent running code inside the operating system core.</param>
		/// <param name="pUserProcessorTime">The amount of time the process has spent running code inside the application.</param>
		/// <param name="pTotalProcessorTime">The sum of UserProcessorTime and PrivilegedProcessorTime.</param>
		/// <param name="pPeakPagedMemorySize">The maximum amount of memory used in the virtual memory paging file.</param>
		/// <param name="pPeakVirtualMemorySize">The maximum amount of virtual memory used by the process (should be greater than or equal to PeakPagedMemorySize).</param>
		/// <param name="pPeakWorkingSet">The maximum amount of physical memory used by the process.</param>
		public ProcessResult(
			string pFullOutput,
			string pFullError,
			string pFullBuffer,
			int pExitCode,
			DateTime pStartTime,
			DateTime pExitTime,
			TimeSpan pPrivilegedProcessorTime,
			TimeSpan pUserProcessorTime,
			TimeSpan pTotalProcessorTime,
			long pPeakPagedMemorySize,
			long pPeakVirtualMemorySize,
			long pPeakWorkingSet
		) {
			FullOutput = pFullOutput;
			FullError = pFullError;
			FullBuffer = pFullBuffer;
			ExitCode = pExitCode;
			StartTime = pStartTime;
			ExitTime = pExitTime;
			PrivilegedProcessorTime = pPrivilegedProcessorTime;
			UserProcessorTime = pUserProcessorTime;
			TotalProcessorTime = pTotalProcessorTime;
			PeakPagedMemorySize = pPeakPagedMemorySize;
			PeakVirtualMemorySize = pPeakVirtualMemorySize;
			PeakWorkingSet = pPeakWorkingSet;
		}
	}
}
