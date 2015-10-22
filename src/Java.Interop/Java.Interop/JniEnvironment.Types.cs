using System;
using System.Collections.Generic;
using System.Text;

namespace Java.Interop
{
	partial class JniEnvironment {
		static partial class Types {

			readonly    static  KeyValuePair<string, string>[]  BuiltinMappings = new KeyValuePair<string, string>[] {
				new KeyValuePair<string, string>("byte",       "B"),
				new KeyValuePair<string, string>("boolean",    "Z"),
				new KeyValuePair<string, string>("char",       "C"),
				new KeyValuePair<string, string>("double",     "D"),
				new KeyValuePair<string, string>("float",      "F"),
				new KeyValuePair<string, string>("int",        "I"),
				new KeyValuePair<string, string>("long",       "J"),
				new KeyValuePair<string, string>("short",      "S"),
				new KeyValuePair<string, string>("void",       "V"),
			};


			public static JniType GetTypeFromInstance (JniObjectReference reference)
			{
				var lref = JniEnvironment.Types.GetObjectClass (reference);
				if (lref.IsValid)
					return new JniType (ref lref, JniHandleOwnership.Transfer);
				return null;
			}

			public static string GetJniTypeNameFromInstance (JniObjectReference reference)
			{
				var lref = GetObjectClass (reference);
				try {
					return GetJniTypeNameFromClass (lref);
				}
				finally {
					JniEnvironment.References.Dispose (ref lref, JniHandleOwnership.Transfer);
				}
			}

			public static string GetJniTypeNameFromClass (JniObjectReference reference)
			{
				var s = JniEnvironment.Current.Class_getName.CallVirtualObjectMethod (reference);
				return JavaClassToJniType (Strings.ToString (ref s, JniHandleOwnership.Transfer));
			}

			static string JavaClassToJniType (string value)
			{
				for (int i = 0; i < BuiltinMappings.Length; ++i) {
					if (value == BuiltinMappings [i].Key)
						return BuiltinMappings [i].Value;
				}
				return value.Replace ('.', '/');
			}
		}
	}
}

