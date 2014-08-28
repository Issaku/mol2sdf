// Xamarin Studio on MAcOSX 10.9
using System;
using System.IO;
using System.Text ;  // for Encoding
using System.Collections.Generic;
using System.Collections;

namespace mol2sdf
{
	class MainClass
	{
		public static void Main(string[] s)
		{
			StringBuilder sb = new StringBuilder ();
			string file_tmp = String.Empty;
			string dirname = String.Empty;

			for (int N = 1; N < Environment.GetCommandLineArgs ().Length; N++) {
				file_tmp = Environment.GetCommandLineArgs () [N];

				dirname = Path.GetDirectoryName(file_tmp);
				string[] files = System.IO.Directory.GetFiles(dirname, "*.mol", System.IO.SearchOption.AllDirectories);

				foreach (var file in files) {
					if (File.Exists (file)) {
						StreamReader reader = new StreamReader (file, Encoding.Default);
						sb.AppendLine(reader.ReadToEnd());
						sb.AppendLine (">  <ID>");
						sb.AppendLine (Path.GetFileName(file));
						sb.AppendLine ();
						sb.AppendLine ("$$$$");
						reader.Close ();
					} 
				}
			}
				
			DateTime dt = DateTime.Now;
			string dtString = dt.ToString ("yyyyMMddHHmmss");

			StreamWriter writer = new StreamWriter(dirname + Path.DirectorySeparatorChar + "mol2sdf_" + dtString + ".sdf",
				false,  // 上書き （ true = 追加 ）
				Encoding.UTF8) ;

			writer.Write(sb.ToString()) ;
			writer.Close() ;
			Console.WriteLine ("finished");
		}
	}
}
