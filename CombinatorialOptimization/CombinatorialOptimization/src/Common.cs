using System;

namespace CombinatorialOptimization.src {
	class Common {
		public const string INSTANCE_PATH = @"..\..\instance\";

		/// <summary>
		/// エラーメッセージを出力してプログラムを終了する
		/// </summary>
		/// <param name="location">エラーが発生した場所</param>
		/// <param name="message">エラーメッセージ</param>
		public static void ErrorExit(string location, string message) {
			Console.WriteLine(message);
			Console.WriteLine("場所 : " + location);
			Console.WriteLine("###Enterを押して終了###");
			Console.ReadLine();
			Environment.Exit(1);
		}
	}
}
