using System;

using Java.Interop;

using NUnit.Framework;

namespace Java.InteropTests
{
	[TestFixture]
	public class JniTypeTest {

		[Test]
		public void Sanity ()
		{
			using (var Integer_class = new JniType ("java/lang/Integer")) {
				var Integer_ctor        = Integer_class.GetConstructor ("(I)V");
				var Integer_intValue    = Integer_class.GetInstanceMethod ("intValue", "()I");
				using (var o = Integer_class.NewObject (Integer_ctor, new JValue (42))) {
					int v = Integer_intValue.InvokeIntMethod (o);
					Assert.AreEqual (42, v);
				}
			}
		}

		[Test, ExpectedException (typeof (JniException))]
		public void InvalidSignatureThrowsJniException ()
		{
			using (var Integer_class = new JniType ("java/lang/Integer")) {
				Integer_class.GetConstructor ("(C)V");
			}
		}
	}
}
