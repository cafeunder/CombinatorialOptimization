using System;

namespace CombinatorialOptimization.Util {
	public class Common {
		public const string INSTANCE_PATH = @"..\..\instance\";

		/// <summary>
		/// エラーメッセージを出力してプログラムを終了する
		/// </summary>
		/// <param name="location">エラーが発生した場所</param>
		/// <param name="message">エラーメッセージ</param>
		public static void ErrorExit(string message) {
			throw new Exception(message);
		}
	}
}
