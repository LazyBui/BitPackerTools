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
		public string FullStd { get; private set; }
		/// <summary>
		/// Gets the full stderr stream.
		/// </summary>
		public string FullError { get; private set; }
		/// <summary>
		/// Gets the full output as you would expect to see in a console execution.
		/// </summary>
		public string FullOutput { get; private set; }
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
		/// <param name="stdOutput">The full stdout stream.</param>
		/// <param name="errorOutput">The full stderr stream.</param>
		/// <param name="fullOutput">The full output as you would expect to see in a console execution.</param>
		/// <param name="exitCode">The exit code.</param>
		/// <param name="startTime">The start time of the process.</param>
		/// <param name="exitTime">The exit time of the process.</param>
		/// <param name="privilegedProcessorTime">The amount of time the process has spent running code inside the operating system core.</param>
		/// <param name="userProcessorTime">The amount of time the process has spent running code inside the application.</param>
		/// <param name="totalProcessorTime">The sum of UserProcessorTime and PrivilegedProcessorTime.</param>
		/// <param name="peakPagedMemorySize">The maximum amount of memory used in the virtual memory paging file.</param>
		/// <param name="peakVirtualMemorySize">The maximum amount of virtual memory used by the process (should be greater than or equal to PeakPagedMemorySize).</param>
		/// <param name="peakWorkingSet">The maximum amount of physical memory used by the process.</param>
		public ProcessResult(
			string stdOutput,
			string errorOutput,
			string fullOutput,
			int exitCode,
			DateTime startTime,
			DateTime exitTime,
			TimeSpan privilegedProcessorTime,
			TimeSpan userProcessorTime,
			TimeSpan totalProcessorTime,
			long peakPagedMemorySize,
			long peakVirtualMemorySize,
			long peakWorkingSet
		) {
			FullStd = stdOutput;
			FullError = errorOutput;
			FullOutput = fullOutput;
			ExitCode = exitCode;
			StartTime = startTime;
			ExitTime = exitTime;
			PrivilegedProcessorTime = privilegedProcessorTime;
			UserProcessorTime = userProcessorTime;
			TotalProcessorTime = totalProcessorTime;
			PeakPagedMemorySize = peakPagedMemorySize;
			PeakVirtualMemorySize = peakVirtualMemorySize;
			PeakWorkingSet = peakWorkingSet;
		}
	}
}
