using System;

namespace BitPackerTools.Serialization {
	/// <summary>
	/// Represents an exclusion from the bit packer de/serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class PackedBitExcludeAttribute : Attribute { }
}
