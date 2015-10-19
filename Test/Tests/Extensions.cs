using System;
using System.Reflection;
using BitPackerTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
	[TestClass]
	public class ExtensionsTest {
		internal interface ITestImplement { }
		internal class TestNoInterface { }
		internal class TestInterface : ITestImplement { }

		[TestMethod]
		public void Implements() {
			Type t = null;
			Assert.ThrowsExact<ArgumentNullException>(() => t.Implements<ITestImplement>());

			t = typeof(TestNoInterface);
			Assert.DoesNotThrow(() => t.Implements<ITestImplement>());
			Assert.False(t.Implements<ITestImplement>());

			t = typeof(TestInterface);
			Assert.DoesNotThrow(() => t.Implements<ITestImplement>());
			Assert.True(t.Implements<ITestImplement>());
		}

		internal class TestingAttribute : Attribute { }
		internal class DifferentAttribute : Attribute { }
		internal class MultiAttributeProperty { [Testing, Different] public int Test { get; set; } }
		internal class DifferentAttributeProperty { [Different] public int Test { get; set; } }
		internal class TestingAttributeProperty { [Testing] public int Test { get; set; } }
		internal class NoAttributeProperty { public int Test { get; set; } }

		[TestMethod]
		public void HasAttribute() {
			Func<Type, PropertyInfo> getProperty = (type) => type.GetProperty("Test", BindingFlags.Public | BindingFlags.Instance);
			PropertyInfo prop = null;
			bool result = false;

			Assert.ThrowsExact<ArgumentNullException>(() => prop.HasAttribute<TestingAttribute>());

			prop = getProperty(typeof(NoAttributeProperty));
			Assert.DoesNotThrow(() => result = prop.HasAttribute<TestingAttribute>());
			Assert.False(result);

			Assert.DoesNotThrow(() => result = prop.HasAttribute<DifferentAttribute>());
			Assert.False(result);

			prop = getProperty(typeof(TestingAttributeProperty));
			Assert.DoesNotThrow(() => result = prop.HasAttribute<TestingAttribute>());
			Assert.True(result);

			Assert.DoesNotThrow(() => result = prop.HasAttribute<DifferentAttribute>());
			Assert.False(result);

			prop = getProperty(typeof(DifferentAttributeProperty));
			Assert.DoesNotThrow(() => result = prop.HasAttribute<TestingAttribute>());
			Assert.False(result);

			Assert.DoesNotThrow(() => result = prop.HasAttribute<DifferentAttribute>());
			Assert.True(result);

			prop = getProperty(typeof(MultiAttributeProperty));
			Assert.DoesNotThrow(() => result = prop.HasAttribute<TestingAttribute>());
			Assert.True(result);

			Assert.DoesNotThrow(() => result = prop.HasAttribute<DifferentAttribute>());
			Assert.True(result);
		}
	}
}
