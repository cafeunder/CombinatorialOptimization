namespace CombinatorialOptimization.Util {
	public class DynamicArray<Type> {
		// 次にデータを格納する場所
		private int tail;
		// バッファ
		private Type[] buffer;
		// バッファに格納されているデータの数
		public int Count { get; private set; }

		public DynamicArray(int bufferSize = 10) {
			this.tail = 0;
			this.Count = 0;
			this.buffer = new Type[bufferSize];
		}

		/// <summary>
		/// データを追加する
		/// </summary>
		/// <param name="data">追加するデータ</param>
		/// <returns>追加したデータ</returns>
		public Type Add(Type data) {
			if (this.Count >= this.buffer.Length) {
				// サイズ = 現在のサイズ / 2にしてbufferを作り直す
				Type[] temp = new Type[this.buffer.Length + (this.buffer.Length / 2 + 1)];
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
		public Type Get(int index) {
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
		public Type Remove(int index) {
			if (index > this.Count - 1) {
				Common.ErrorExit("配列の境界外です。");
			}

			Type result = this.buffer[index];
			this.buffer[index] = this.buffer[this.tail - 1];
			this.tail--;
			return result;
		}
	}
}
