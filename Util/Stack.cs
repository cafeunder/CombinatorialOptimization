using System;

namespace CombinatorialOptimization.Util {
	public class Stack {
		// 次にpopされる位置
		private int top;
		// バッファ
		private int[] buffer;
		// バッファに格納されているデータの数
		public int Count { get { return this.top + 1;} }

		public Stack(int bufferSize = 10) {
			this.top = -1;
			this.buffer = new int[bufferSize];
		}

		/// <summary>
		/// データをプッシュする
		/// </summary>
		/// <param name="data">データ</param>
		public int Push(int data) {
			// データの数がバッファを超えたら
			if (this.Count >= this.buffer.Length) {
				// サイズ = 現在のサイズ / 2にしてbufferを作り直す
				int[] temp = new int[this.buffer.Length + (this.buffer.Length / 2 + 1)];
				// 値をコピー
				for (int i = 0; i < this.buffer.Length; i++) {
					temp[i] = this.buffer[i];
				}
				this.buffer = temp;
			}

			this.top++;
			this.buffer[this.top] = data;

			return data;
		}

		/// <summary>
		/// データをポップする
		/// </summary>
		/// <returns>データ</returns>
		public int Pop() {
			if (this.Count == 0) {
				Common.ErrorExit("データの個数が0のときにPop()が呼び出されました。");
			}
			int result = this.buffer[this.top];
			this.top--;
			return result;
		}

		/// <summary>
		/// データがあるならポップする
		/// </summary>
		/// <param name="data">データを格納するout変数</param>
		/// <returns>データがあるか？</returns>
		public bool CheckPop(out int data) {
			if (this.Count == 0) {
				data = -1;
				return false;
			}

			data = this.buffer[this.top];
			this.top--;
			return true;
		}

		/// <summary>
		/// 次にポップされるデータを確認する
		/// </summary>
		/// <returns>次にポップされるデータ</returns>
		public int Peak() {
			if (this.Count == 0) {
				Common.ErrorExit("データの個数が0のときにPeak()が呼び出されました。");
			}

			return this.buffer[this.top];
		}

		/// <summary>
		/// データがあるなら次にデキューされるデータを確認する
		/// </summary>
		/// <param name="data">次にデキューされるデータを格納するout変数</param>
		/// <returns>データがあるか？</returns>
		public bool CheckPeak(out int data) {
			if (this.Count == 0) {
				data = -1;
				return false;
			}

			data = this.buffer[this.top];
			return true;
		}

		/// <summary>
		/// データを全て破棄する
		/// </summary>
		public void Clear() {
			this.top = -1;
		}

		public static void Main(string[] args) {
			Stack stack = new Stack(2);

			stack.Push(1);
			foreach (int i in stack.buffer) { Console.Write(i + ","); }
			Console.WriteLine();
			Console.WriteLine("Peak : " + stack.Peak());

			stack.Push(2);
			foreach (int i in stack.buffer) { Console.Write(i + ","); }
			Console.WriteLine();
			Console.WriteLine("Peak : " + stack.Peak());

			stack.Push(3);
			foreach (int i in stack.buffer) { Console.Write(i + ","); }
			Console.WriteLine();
			Console.WriteLine("Peak : " + stack.Peak());

			Console.WriteLine("Pop : " + stack.Pop());
			foreach (int i in stack.buffer) { Console.Write(i + ","); }
			Console.WriteLine();
			Console.WriteLine("Peak : " + stack.Peak());

			Console.WriteLine("Pop : " + stack.Pop());
			foreach (int i in stack.buffer) { Console.Write(i + ","); }
			Console.WriteLine();
			Console.WriteLine("Peak : " + stack.Peak());

			Console.WriteLine("Pop : " + stack.Pop());
			foreach (int i in stack.buffer) { Console.Write(i + ","); }
			Console.WriteLine();

			int a;
			Console.WriteLine("Peak : " + stack.CheckPeak(out a));
		}
	}
}
