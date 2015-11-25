using System;
using System.Linq;
using System.Reflection;

namespace BitPackerTools {
	internal static class Extensions {
		public static bool Implements<TInterface>(this Type @this) where TInterface : class {
			if (@this == null) throw new ArgumentNullException(nameof(@this));
			return @this.GetInterfaces().Any(i => i == typeof(TInterface));
		}

		public static bool HasAttribute<TAttribute>(this PropertyInfo @this) where TAttribute : Attribute {
			if (@this == null) throw new ArgumentNullException(nameof(@this));
			return @this.GetCustomAttribute<TAttribute>() != null;
		}
	}
}