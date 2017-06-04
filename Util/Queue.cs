using System;
using System.Reflection;

namespace CombinatorialOptimization.Util {
	/// <summary>
	/// キューを表すクラス
	/// </summary>
	public class Queue {
		// 次にデキューされる位置
		private int head;
		// 最後にエンキューした位置
		private int tail;
		// バッファ
		private int[] buffer;
		// バッファに格納されているデータの数
		public int Count { get; private set; }

		public Queue(int bufferSize = 10) {
			this.head = 0;
			this.tail = -1; 
			this.Count = 0;
			this.buffer = new int[bufferSize];
		}

		/// <summary>
		/// データをエンキューする
		/// </summary>
		/// <param name="data">データ</param>
		public int Enqueue(int data) {
			// データの数がバッファを超えたら
			if (this.Count >= this.buffer.Length) {
				// サイズ = 現在のサイズ / 2にしてbufferを作り直す
				int[] temp = new int[this.buffer.Length + (this.buffer.Length / 2 + 1)];
				// 値をコピー
				for (int i = 0; i < this.buffer.Length; i++) {
					temp[i] = this.buffer[(this.head + i) % this.buffer.Length];
				}
				// headとtailの位置を正す
				this.head = 0;
				this.tail = this.buffer.Length - 1;
				this.buffer = temp;
			}

			// データを入れる場所はtailの次
			this.tail = (this.tail + 1) % this.buffer.Length;
			this.buffer[this.tail] = data;
			this.Count++;

			return data;
		}

		/// <summary>
		/// データをデキューする
		/// </summary>
		/// <returns>データ</returns>
		public int Dequeue() {
			if (this.Count == 0) {
				Common.ErrorExit("データの個数が0のときにDequeue()が呼び出されました。");
			}

			int result = this.buffer[this.head];
			this.head = (this.head + 1) % this.buffer.Length;
			this.Count--;
			return result;
		}

		/// <summary>
		/// データがあるならデキューする
		/// </summary>
		/// <param name="data">データを格納するout変数</param>
		/// <returns>データがあるか？</returns>
		public bool CheckDequeue(out int data) {
			data = this.buffer[this.head];
			if (this.Count == 0) {
				return false;
			}

			this.head = (this.head + 1) % this.buffer.Length;
			this.Count--;
			return true;
		}

		/// <summary>
		/// 次にデキューされるデータを確認する
		/// </summary>
		/// <returns>次にデキューされるデータ</returns>
		public int Peak() {
			if (this.Count == 0) {
				Common.ErrorExit("データの個数が0のときにPeak()が呼び出されました。");
			}

			return this.buffer[this.head];
		}

		/// <summary>
		/// データを全て破棄する
		/// </summary>
		public void Clear() {
			this.head = 0;
			this.tail = -1; 
			this.Count = 0;
		}

		/// <summary>
		/// データがあるなら次にデキューされるデータを確認する
		/// </summary>
		/// <param name="data">次にデキューされるデータを格納するout変数</param>
		/// <returns>データがあるか？</returns>
		public bool CheckPeak(out int data) {
			data = this.buffer[this.head];
			if (this.Count == 0) {
				return false;
			}

			return true;
		}
	}
}
