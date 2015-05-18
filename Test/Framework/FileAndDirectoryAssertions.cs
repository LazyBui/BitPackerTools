using System;
using System.IO;
using System.Linq;

namespace Test {
	internal partial class Assert {
		/// <summary>
		/// Asserts that a file exists.
		/// </summary>
		public static void FileExists(string pFile, AssertionException pException = null) {
			if (pFile == null) throw new ArgumentNullException(nameof(pFile));
			if (!File.Exists(pFile)) throw pException ?? new AssertionException("File did not exist");
		}

		/// <summary>
		/// Asserts that a file exists.
		/// </summary>
		public static void FileExists(FileInfo pFile, AssertionException pException = null) {
			if (pFile == null) throw new ArgumentNullException(nameof(pFile));
			if (!pFile.Exists) throw pException ?? new AssertionException("File did not exist");
		}

		/// <summary>
		/// Asserts that a file does not exist.
		/// </summary>
		public static void FileNotExists(string pFile, AssertionException pException = null) {
			if (pFile == null) throw new ArgumentNullException(nameof(pFile));
			if (File.Exists(pFile)) throw pException ?? new AssertionException("File exists");
		}

		/// <summary>
		/// Asserts that a file does not exist.
		/// </summary>
		public static void FileNotExists(FileInfo pFile, AssertionException pException = null) {
			if (pFile == null) throw new ArgumentNullException(nameof(pFile));
			if (pFile.Exists) throw pException ?? new AssertionException("File exists");
		}

		/// <summary>
		/// Asserts that two specified files are equal.
		/// This has a very high threshold for equality: byte-by-byte binary equality.
		/// If you need a looser definition of equality, this is unsupported at this time.
		/// </summary>
		public static void FilesEqual(string pFile1, string pFile2, AssertionException pException = null) {
			if (pFile1 == null) throw new ArgumentNullException(nameof(pFile1));
			if (pFile1.Length == 0) throw new ArgumentException("Must be a valid file", nameof(pFile1));
			if (pFile2 == null) throw new ArgumentNullException(nameof(pFile2));
			if (pFile2.Length == 0) throw new ArgumentException("Must be a valid file", nameof(pFile2));

			FileInfo file1 = new FileInfo(pFile1);
			FileInfo file2 = new FileInfo(pFile2);

			FilesEqual(file1, file2);
		}

		/// <summary>
		/// Asserts that two specified files are equal.
		/// This has a very high threshold for equality: byte-by-byte binary equality.
		/// If you need a looser definition of equality, this is unsupported at this time.
		/// </summary>
		public static void FilesEqual(FileInfo pFile1, string pFile2, AssertionException pException = null) {
			if (pFile1 == null) throw new ArgumentNullException(nameof(pFile1));
			if (pFile2 == null) throw new ArgumentNullException(nameof(pFile2));
			if (pFile2.Length == 0) throw new ArgumentException("Must be a valid file", nameof(pFile2));

			FileInfo file2 = new FileInfo(pFile2);

			FilesEqual(pFile1, file2);
		}

		/// <summary>
		/// Asserts that two specified files are equal.
		/// This has a very high threshold for equality: byte-by-byte binary equality.
		/// If you need a looser definition of equality, this is unsupported at this time.
		/// </summary>
		public static void FilesEqual(string pFile1, FileInfo pFile2, AssertionException pException = null) {
			if (pFile1 == null) throw new ArgumentNullException(nameof(pFile1));
			if (pFile1.Length == 0) throw new ArgumentException("Must be a valid file", nameof(pFile1));
			if (pFile2 == null) throw new ArgumentNullException(nameof(pFile2));

			FileInfo file1 = new FileInfo(pFile1);

			FilesEqual(file1, pFile2);
		}

		/// <summary>
		/// Asserts that two specified files are equal.
		/// This has a very high threshold for equality: byte-by-byte binary equality.
		/// If you need a looser definition of equality, this is unsupported at this time.
		/// </summary>
		public static void FilesEqual(FileInfo pFile1, FileInfo pFile2, AssertionException pException = null) {
			if (pFile1 == null) throw new ArgumentNullException(nameof(pFile1));
			if (pFile2 == null) throw new ArgumentNullException(nameof(pFile2));

			if (!pFile1.Exists) throw new ArgumentException("File must exist", nameof(pFile1));
			if (!pFile2.Exists) throw new ArgumentException("File must exist", nameof(pFile2));

			if (pFile1.Length != pFile2.Length) {
				throw pException ?? new AssertionException("Specified files are not equal");
			}

			if (string.Compare(pFile1.FullName, pFile2.FullName, StringComparison.InvariantCultureIgnoreCase) == 0) {
				// If they're the same file, we can safely not do anything else
				return;
			}

			using (FileStream fs1 = pFile1.OpenRead(), fs2 = pFile2.OpenRead()) {
				const int readSize = 65536;
				byte[] buffer1 = new byte[readSize];
				byte[] buffer2 = new byte[readSize];
				do {
					int read1 = fs1.Read(buffer1, 0, readSize);
					int read2 = fs2.Read(buffer2, 0, readSize);
					if (read1 != read2) {
						// ????
						throw new InvalidOperationException("Something strange happened here");
					}
					if (!Enumerable.SequenceEqual(buffer1, buffer2)) {
						throw pException ?? new AssertionException("Specified files are not equal");
					}
					if (read1 < readSize) {
						break;
					}
				} while (true);
			}
		}

		/// <summary>
		/// Asserts that two specified files are not equal.
		/// This has a very high threshold for equality: byte-by-byte binary equality.
		/// If you need a looser definition of equality, this is unsupported at this time.
		/// </summary>
		public static void FilesNotEqual(string pFile1, string pFile2, AssertionException pException = null) {
			if (pFile1 == null) throw new ArgumentNullException(nameof(pFile1));
			if (pFile1.Length == 0) throw new ArgumentException("Must be a valid file", nameof(pFile1));
			if (pFile2 == null) throw new ArgumentNullException(nameof(pFile2));
			if (pFile2.Length == 0) throw new ArgumentException("Must be a valid file", nameof(pFile2));

			FileInfo file1 = new FileInfo(pFile1);
			FileInfo file2 = new FileInfo(pFile2);

			FilesNotEqual(file1, file2);
		}

		/// <summary>
		/// Asserts that two specified files are not equal.
		/// This has a very high threshold for equality: byte-by-byte binary equality.
		/// If you need a looser definition of equality, this is unsupported at this time.
		/// </summary>
		public static void FilesNotEqual(FileInfo pFile1, string pFile2, AssertionException pException = null) {
			if (pFile1 == null) throw new ArgumentNullException(nameof(pFile1));
			if (pFile2 == null) throw new ArgumentNullException(nameof(pFile2));
			if (pFile2.Length == 0) throw new ArgumentException("Must be a valid file", nameof(pFile2));

			FileInfo file2 = new FileInfo(pFile2);

			FilesNotEqual(pFile1, file2);
		}

		/// <summary>
		/// Asserts that two specified files are not equal.
		/// This has a very high threshold for equality: byte-by-byte binary equality.
		/// If you need a looser definition of equality, this is unsupported at this time.
		/// </summary>
		public static void FilesNotEqual(string pFile1, FileInfo pFile2, AssertionException pException = null) {
			if (pFile1 == null) throw new ArgumentNullException(nameof(pFile1));
			if (pFile1.Length == 0) throw new ArgumentException("Must be a valid file", nameof(pFile1));
			if (pFile2 == null) throw new ArgumentNullException(nameof(pFile2));

			FileInfo file1 = new FileInfo(pFile1);

			FilesNotEqual(file1, pFile2);
		}

		/// <summary>
		/// Asserts that two specified files are not equal.
		/// This has a very high threshold for equality: byte-by-byte binary equality.
		/// If you need a looser definition of equality, this is unsupported at this time.
		/// </summary>
		public static void FilesNotEqual(FileInfo pFile1, FileInfo pFile2, AssertionException pException = null) {
			if (pFile1 == null) throw new ArgumentNullException(nameof(pFile1));
			if (pFile2 == null) throw new ArgumentNullException(nameof(pFile2));

			if (!pFile1.Exists) throw new ArgumentException("File must exist", nameof(pFile1));
			if (!pFile2.Exists) throw new ArgumentException("File must exist", nameof(pFile2));

			if (pFile1.Length != pFile2.Length) {
				return;
			}

			if (string.Compare(pFile1.FullName, pFile2.FullName, StringComparison.InvariantCultureIgnoreCase) == 0) {
				// If they're the same file, we can safely not do anything else
				throw pException ?? new AssertionException("Specified files are equal");
			}

			using (FileStream fs1 = pFile1.OpenRead(), fs2 = pFile2.OpenRead()) {
				const int readSize = 65536;
				byte[] buffer1 = new byte[readSize];
				byte[] buffer2 = new byte[readSize];
				do {
					int read1 = fs1.Read(buffer1, 0, readSize);
					int read2 = fs2.Read(buffer2, 0, readSize);
					if (read1 != read2) {
						// ????
						throw new InvalidOperationException("Something strange happened here");
					}
					if (!Enumerable.SequenceEqual(buffer1, buffer2)) {
						return;
					}
					if (read1 < readSize) {
						break;
					}
				} while (true);
			}

			throw pException ?? new AssertionException("Specified files are equal");
		}

		/// <summary>
		/// Asserts that a directory exists.
		/// </summary>
		public static void DirectoryExists(string pDirectory, AssertionException pException = null) {
			if (pDirectory == null) throw new ArgumentNullException(nameof(pDirectory));
			if (!Directory.Exists(pDirectory)) throw pException ?? new AssertionException("Directory did not exist");
		}

		/// <summary>
		/// Asserts that a directory exists.
		/// </summary>
		public static void DirectoryExists(DirectoryInfo pDirectory, AssertionException pException = null) {
			if (pDirectory == null) throw new ArgumentNullException(nameof(pDirectory));
			if (!pDirectory.Exists) throw pException ?? new AssertionException("Directory did not exist");
		}

		/// <summary>
		/// Asserts that a directory does not exist.
		/// </summary>
		public static void DirectoryNotExists(string pDirectory, AssertionException pException = null) {
			if (pDirectory == null) throw new ArgumentNullException(nameof(pDirectory));
			if (Directory.Exists(pDirectory)) throw pException ?? new AssertionException("Directory exists");
		}

		/// <summary>
		/// Asserts that a directory does not exist.
		/// </summary>
		public static void DirectoryNotExists(DirectoryInfo pDirectory, AssertionException pException = null) {
			if (pDirectory == null) throw new ArgumentNullException(nameof(pDirectory));
			if (pDirectory.Exists) throw pException ?? new AssertionException("Directory exists");
		}
	}
}
