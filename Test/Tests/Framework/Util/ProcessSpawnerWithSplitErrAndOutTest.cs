using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class ProcessSpawnerWithSplitErrAndOutTest {
		[TestMethod]
		[TestCategory("Framework.Util")]
		public void Constructor() {
			// File only
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as string)) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as FileInfo)) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(string.Empty)) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinary)) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinaryInfo)) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersed)) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersedInfo)) { } });

			// File/args only
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as string, null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as FileInfo, null as object[])) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(string.Empty, null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinary, null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinaryInfo, null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as string, "abc")) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as FileInfo, "abc")) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(string.Empty, "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinary, "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinaryInfo, "abc")) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersed, null as object[])) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersedInfo, null as object[])) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersed, "abc")) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersedInfo, "abc")) { } });

			// File/escaper/args
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as string, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as FileInfo, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(string.Empty, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinary, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinaryInfo, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as string, null, null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as FileInfo, null, null as object[])) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(string.Empty, null, null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinary, null, null as object[])) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinaryInfo, null, null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as string, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as FileInfo, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(string.Empty, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinary, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinaryInfo, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as string, null, "abc")) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(null as FileInfo, null, "abc")) { } });
			Assert.ThrowsExact<ArgumentException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(string.Empty, null, "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinary, null, "abc")) { } });
			Assert.ThrowsExact<FileNotFoundException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.InvalidBinaryInfo, null, "abc")) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersed, null, null as object[])) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersedInfo, null, null as object[])) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersed, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersedInfo, new WindowsCommandLineArgumentEscaper(), null as object[])) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersed, null, "abc")) { } });
			Assert.ThrowsExact<ArgumentNullException>(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersedInfo, null, "abc")) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersed, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
			Assert.DoesNotThrow(() => { using (new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersedInfo, new WindowsCommandLineArgumentEscaper(), "abc")) { } });
		}

		[TestMethod]
		[TestCategory("Framework.Util")]
		public void SimpleInterspersed() {
			ProcessResult result = null;
			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithSplitErrAndOut(TestApplications.SimpleInterspersedInfo)) {
					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.Equal(result.ExitCode, 3);
			Assert.GreaterThan(result.PeakPagedMemorySize, 0);
			Assert.GreaterThan(result.PeakVirtualMemorySize, 0);
			Assert.GreaterThan(result.PeakWorkingSet, 0);
			Assert.NotEqual(result.StartTime, DateTime.MinValue);
			Assert.NotEqual(result.ExitTime, DateTime.MinValue);
			Assert.GreaterThan(result.ExitTime - result.StartTime, TimeSpan.Zero);
			Assert.Null(result.FullOutput);
			Assert.NotNull(result.FullStd);
			Assert.NotNull(result.FullError);
			Assert.Equal(result.FullStd, @"def
def
def
def
abcdef");
			Assert.Equal(result.FullError, @"abc
abc
abc
abc
abcdef");
		}

		[TestMethod]
		[TestCategory("Framework.Util")]
		public void BlankLinesInterspersed() {
			ProcessResult result = null;
			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithSplitErrAndOut(TestApplications.BlankLinesInterspersedInfo)) {
					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.Null(result.FullOutput);
			Assert.NotNull(result.FullStd);
			Assert.NotNull(result.FullError);
			Assert.Equal(result.ExitCode, 0);
			Assert.GreaterThan(result.PeakPagedMemorySize, 0);
			Assert.GreaterThan(result.PeakVirtualMemorySize, 0);
			Assert.GreaterThan(result.PeakWorkingSet, 0);
			Assert.NotEqual(result.StartTime, DateTime.MinValue);
			Assert.NotEqual(result.ExitTime, DateTime.MinValue);
			Assert.GreaterThan(result.ExitTime - result.StartTime, TimeSpan.Zero);
			Assert.Equal(result.FullStd, @"def

def

def
def");
			Assert.Equal(result.FullError, @"abc

abc
abc

abc");
		}

		[TestMethod]
		[TestCategory("Framework.Util")]
		public void NoOutput() {
			ProcessResult result = null;
			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithSplitErrAndOut(TestApplications.NoOutputInfo)) {
					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.Equal(result.ExitCode, 0);
			Assert.GreaterThan(result.PeakPagedMemorySize, 0);
			Assert.GreaterThan(result.PeakVirtualMemorySize, 0);
			Assert.GreaterThan(result.PeakWorkingSet, 0);
			Assert.NotEqual(result.StartTime, DateTime.MinValue);
			Assert.NotEqual(result.ExitTime, DateTime.MinValue);
			Assert.GreaterThan(result.ExitTime - result.StartTime, TimeSpan.Zero);
			Assert.Null(result.FullOutput);
			Assert.NotNull(result.FullStd);
			Assert.NotNull(result.FullError);
			Assert.Equal(result.FullStd, @"");
			Assert.Equal(result.FullError, @"");
		}

		[TestMethod]
		[TestCategory("Framework.Util")]
		public void NakedInput() {
			ProcessResult result = null;
			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithSplitErrAndOut(TestApplications.NakedInputInfo)) {
					process.OnInputRequested += (buf, writer) => {
						if (!string.IsNullOrEmpty(buf) && buf != "acb876") {
							Assert.Fail();
						}
						writer.WriteLine("acb876");
						return ProcessInputHandleResult.Handled;
					};

					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.NotNull(result.FullStd);
			Assert.NotNull(result.FullError);
			Assert.Equal(result.FullStd, @"acb876");
			Assert.Equal(result.FullError, @"");
		}

		[TestMethod]
		[TestCategory("Framework.Util")]
		public void PlusTwoNumber() {
			ProcessResult result = null;
			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithSplitErrAndOut(TestApplications.PlusTwoNumberInfo)) {
					process.OnInputRequested += (buf, writer) => {
						if (string.IsNullOrEmpty(buf)) {
							return ProcessInputHandleResult.Ignored;
						}
						if (buf == "878") {
							return ProcessInputHandleResult.Ignored;
						}
						if (buf != "Please enter a number...") {
							Assert.Fail();
							return ProcessInputHandleResult.Ignored;
						}
						writer.WriteLine("876");
						return ProcessInputHandleResult.Handled;
					};

					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.NotNull(result.FullStd);
			Assert.NotNull(result.FullError);
			Assert.Equal(result.FullStd, @"Please enter a number...
878");
			Assert.Equal(result.FullError, @"");

			Assert.DoesNotThrow(() => {
				using (var process = new ProcessSpawnerWithSplitErrAndOut(TestApplications.PlusTwoNumberInfo)) {
					process.OnInputRequested += (buf, writer) => {
						if (string.IsNullOrEmpty(buf)) {
							return ProcessInputHandleResult.Ignored;
						}
						if (buf == "Bad number") {
							return ProcessInputHandleResult.Ignored;
						}
						if (buf != "Please enter a number...") {
							Assert.Fail();
							return ProcessInputHandleResult.Ignored;
						}
						writer.WriteLine("abc");
						return ProcessInputHandleResult.Handled;
					};

					result = process.Run();
				}
			});
			Assert.NotNull(result);
			Assert.NotNull(result.FullStd);
			Assert.NotNull(result.FullError);
			Assert.Equal(result.FullStd, @"Please enter a number...");
			Assert.Equal(result.FullError, @"Bad number");
		}
	}
}
