using System;
using System.Linq;

namespace BitPackerTools {
	internal static class Extensions {
		public static bool Implements<TInterface>(this Type pType) where TInterface : class {
			return pType.GetInterfaces().Any(i => i == typeof(TInterface));
		}
	}
}