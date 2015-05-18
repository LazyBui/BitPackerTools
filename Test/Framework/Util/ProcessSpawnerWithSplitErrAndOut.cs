using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Test {
	/// <summary>
	/// Executes an <see cref="System.Diagnostics.Process" /> object in order to get distinct output for assertion purposes. 
	/// <see cref="System.Diagnostics.Process" /> has a limitation where sequentially consistent output (as you would see in a regular console execution) and distinct output cannot be obtained at the same time and it may be a limitation of Windows itself.
	/// If both outputs are required, please use <see cref="Test.ProcessSpawnerWithCombinedAndSplitErrAndOut" />.
	/// </summary>
	internal sealed class ProcessSpawnerWithSplitErrAndOut : IProcessSpawner {
		private Process m_process = null;
		private StringBuilder m_output = new StringBuilder(1024);
		private StringBuilder m_outputSinceLastInput = null;
		private StringBuilder m_error = new StringBuilder(1024);
		private long m_procPeakPagedMemorySize;
		private long m_procPeakVirtualMemorySize;
		private long m_procPeakWorkingSet;

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

		/// <summary>z
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithSplitErrAndOut" /> class with a specified file.
		/// </summary>
		/// <param name="pFileName">The name of the file to execute.</param>
		public ProcessSpawnerWithSplitErrAndOut(string pFileName) { Initialize(pFileName, null, null); }
		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithSplitErrAndOut" /> class with a specified file and command line arguments.
		/// </summary>
		/// <param name="pFileName">The name of the file to execute.</param>
		/// <param name="pArguments">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithSplitErrAndOut(string pFileName, params object[] pArguments) { Initialize(pFileName, new WindowsCommandLineArgumentEscaper(), pArguments); }
		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithSplitErrAndOut" /> class with a specified file, command line escaper, and command line arguments.
		/// </summary>
		/// <param name="pFileName">The name of the file to execute.</param>
		/// <param name="pEscaper">The command line escaper to produce a command line argument string from the command line arguments.</param>
		/// <param name="pArguments">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithSplitErrAndOut(string pFileName, ICommandLineArgumentEscaper pEscaper, params object[] pArguments) { Initialize(pFileName, pEscaper, pArguments); }
		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithSplitErrAndOut" /> class with a specified file.
		/// </summary>
		/// <param name="pFile">The <see cref="FileInfo" /> carrying information about the file to execute.</param>
		public ProcessSpawnerWithSplitErrAndOut(FileInfo pFile) { Initialize(pFile, null, null); }
		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithSplitErrAndOut" /> class with a specified file and command line arguments.
		/// </summary>
		/// <param name="pFile">The <see cref="FileInfo" /> carrying information about the file to execute.</param>
		/// <param name="pArguments">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithSplitErrAndOut(FileInfo pFile, params object[] pArguments) { Initialize(pFile, new WindowsCommandLineArgumentEscaper(), pArguments); }
		/// <summary>
		/// Initializes a new instance of the <see cref="Test.ProcessSpawnerWithSplitErrAndOut" /> class with a specified file, command line escaper, and command line arguments.
		/// </summary>
		/// <param name="pFile">The <see cref="FileInfo" /> carrying information about the file to execute.</param>
		/// <param name="pEscaper">The command line escaper to produce a command line argument string from the command line arguments.</param>
		/// <param name="pArguments">The command line arguments to pass to the execution of the file.</param>
		public ProcessSpawnerWithSplitErrAndOut(FileInfo pFile, ICommandLineArgumentEscaper pEscaper, params object[] pArguments) { Initialize(pFile, pEscaper, pArguments); }

		private void Initialize(string pFileName, ICommandLineArgumentEscaper pEscaper, params object[] pArguments) {
			if (pFileName == null) throw new ArgumentNullException(nameof(pFileName));
			if (pFileName.Length == 0) throw new ArgumentException("String is blank", nameof(pFileName));
			Initialize(new FileInfo(pFileName), pEscaper, pArguments);
		}

		private void Initialize(FileInfo pFile, ICommandLineArgumentEscaper pEscaper, params object[] pArguments) {
			if (pFile == null) throw new ArgumentNullException(nameof(pFile));
			if (!pFile.Exists) throw new FileNotFoundException("File not found", pFile.FullName);
			if (pArguments != null && pArguments.Any()) {
				if (pEscaper == null) throw new ArgumentNullException(nameof(pEscaper));
			}

			var startInfo = new ProcessStartInfo() {
				FileName = pFile.FullName,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
				RedirectStandardInput = true,
				UseShellExecute = false,
			};

			if (pArguments != null && pArguments.Any()) {
				startInfo.Arguments = pEscaper.Escape(pArguments);
			}

			m_process = new Process() {
				StartInfo = startInfo,
				EnableRaisingEvents = true,
			};

			m_process.OutputDataReceived += (sender, args) => {
				if (args.Data == null) return;
				if (m_outputSinceLastInput != null) {
					if (m_outputSinceLastInput.Length != 0) m_outputSinceLastInput.AppendLine();
					m_outputSinceLastInput.Append(args.Data);
				}
				if (m_output.Length != 0) m_output.AppendLine();
				m_output.Append(args.Data);
			};

			m_process.ErrorDataReceived += (sender, args) => {
				if (args.Data == null) return;
				if (m_error.Length != 0) m_error.AppendLine();
				m_error.Append(args.Data);
			};
		}

		private ProcessResult ProduceResult() {
			return new ProcessResult(
				pFullOutput: m_output.ToString(),
				pFullError: m_error.ToString(),
				pFullBuffer: null,
				pExitCode: m_process.ExitCode,
				pStartTime: m_process.StartTime,
				pExitTime: m_process.ExitTime,
				pPrivilegedProcessorTime: m_process.PrivilegedProcessorTime,
				pUserProcessorTime: m_process.UserProcessorTime,
				pTotalProcessorTime: m_process.TotalProcessorTime,
				pPeakPagedMemorySize: m_procPeakPagedMemorySize,
				pPeakVirtualMemorySize: m_procPeakVirtualMemorySize,
				pPeakWorkingSet: m_procPeakWorkingSet
			);
		}

		/// <summary>
		/// Executes the specified process.
		/// </summary>
		/// <returns>A <see cref="Test.ProcessResult" /> object representing the execution.</returns>
		public ProcessResult Run() {
			return WaitForExit();
		}

		private void Start() {
			if (Exited) throw new InvalidOperationException("Must not execute the process twice");
			if (Started) throw new InvalidOperationException("Must not execute the process twice");
			if (OnInputRequested != null) {
				m_outputSinceLastInput = new StringBuilder(1024);
			}

			Started = true;
			m_process.Start();
			m_process.BeginOutputReadLine();
			m_process.BeginErrorReadLine();
		}

		private ProcessResult WaitForExit() {
			if (Exited) throw new InvalidOperationException("Must not execute the process twice");
			if (!Started) Start();

			while (true) {
				if (!m_process.HasExited) {
					try {
						m_procPeakPagedMemorySize = m_process.PeakPagedMemorySize64;
						m_procPeakVirtualMemorySize = m_process.PeakVirtualMemorySize64;
						m_procPeakWorkingSet = m_process.PeakWorkingSet64;

						if (OnInputRequested != null) {
							foreach (ProcessThread thread in m_process.Threads) {
								if (thread.ThreadState == ThreadState.Wait && thread.WaitReason == ThreadWaitReason.UserRequest) {
									ProcessInputHandleResult result = OnInputRequested(m_outputSinceLastInput.ToString(), m_process.StandardInput);
									if (result == ProcessInputHandleResult.Handled) {
										m_outputSinceLastInput.Clear();
									}
									break;
								}
							}
						}

						m_process.Refresh();
					}
					catch (InvalidOperationException) { break; }
				}
				else break;
			}

			// Allow any outstanding events to finish
			m_process.WaitForExit();

			Exited = true;
			return ProduceResult();
		}

		/// <summary>
		/// Releases all resources used by the current instance of the Test.ProcessSpawnerWithSplitErrAndOut class.
		/// </summary>
		public void Dispose() {
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing) {
			if (disposing) {
				m_process.Dispose();
			}
		}
	}
}
