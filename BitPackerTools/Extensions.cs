using System;
using System.Linq;
using System.Reflection;

namespace BitPackerTools {
	internal static class Extensions {
		public static bool Implements<TInterface>(this Type pType) where TInterface : class {
			if (pType == null) throw new ArgumentNullException(nameof(pType));
			return pType.GetInterfaces().Any(i => i == typeof(TInterface));
		}

		public static bool HasAttribute<TAttribute>(this PropertyInfo pProperty) where TAttribute : Attribute {
			if (pProperty == null) throw new ArgumentNullException(nameof(pProperty));
			return pProperty.GetCustomAttribute<TAttribute>() != null;
		}
	}
}