﻿using System;

namespace MonoTests.Portable.Xaml
{
	static class Compat
	{

		#if PCL

		#if PCL136
		public const string Version = "pcl136";
		#elif PCL259
		public const string Version = "pcl259";
		#endif

		public const string Namespace = "Portable.Xaml";
		public const string Prefix = "px";
		#else
		public const string Version = "net_4_5";
		public const string Namespace = "System.Xaml";
		public const string Prefix = "sx";
		#endif


		public static string Fixup(this string str)
		{
			#if PCL
			return str;
			#else
			return str
				.Replace ("Portable.Xaml.Markup", "System.Windows.Markup")
				.Replace ("Portable.Xaml", Namespace);
			#endif
		}

		public static string UpdateXml(this string str)
		{
			return str.Replace ("net_4_0", Compat.Version)
				.Replace ("clr-namespace:Portable.Xaml;assembly=Portable.Xaml", $"clr-namespace:{Compat.Namespace};assembly={Compat.Namespace}")
				.Replace ($" px:", $" {Compat.Prefix}:")
				.Replace ($"xmlns:px", $"xmlns:{Compat.Prefix}")
				.Replace ("\r", "")
				.Replace("\n", "\r\n");
		}

        public static bool IsMono
        {
            get { return Type.GetType("Mono.Runtime", false) != null; }
        }
	}
}

