namespace CombinatorialOptimization.Util {
	public class DynamicArray {
		// 次にデータを格納する場所
		private int tail;
		// バッファ
		private int[] buffer;
		// バッファに格納されているデータの数
		public int Count { get; private set; }

		public DynamicArray(int bufferSize = 10) {
			this.tail = 0;
			this.Count = 0;
			this.buffer = new int[bufferSize];
		}

		/// <summary>
		/// データを追加する
		/// </summary>
		/// <param name="data">追加するデータ</param>
		/// <returns>追加したデータ</returns>
		public int Add(int data) {
			if (this.Count >= this.buffer.Length) {
				// サイズ = 現在のサイズ / 2にしてbufferを作り直す
				int[] temp = new int[this.buffer.Length + (this.buffer.Length / 2 + 1)];
				// 値をコピー
				for (int i = 0; i < this.buffer.Length; i++) {
					temp[i] = this.buffer[i];
				}

				this.buffer = temp;
			}

			this.buffer[this.tail] = data;
			this.tail++;
			this.Count++;
			return data;
		}

		/// <summary>
		/// インデックスを指定してデータを取得する
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>データ</returns>
		public int Get(int index) {
			if (index > this.Count - 1) {
				Common.ErrorExit("配列の境界外です。");
			}
			return this.buffer[index];
		}

		/// <summary>
		/// 指定したインデックスのデータを削除する
		/// </summary>
		/// <param name="index">インデックス</param>
		/// <returns>削除したデータ</returns>
		public int Remove(int index) {
			if (index > this.Count - 1) {
				Common.ErrorExit("配列の境界外です。");
			}

			int result = this.buffer[index];
			this.buffer[index] = this.buffer[this.tail - 1];
			this.tail--;
			return result;
		}
	}
}
