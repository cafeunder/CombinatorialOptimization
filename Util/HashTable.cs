
using System;
using CombinatorialOptimization.Util;

namespace CombinatorialOptimization.Util {
	/// <summary>
	/// 関数がデータの剰余である、オープンアドレス方式のハッシュテーブル
	/// </summary>
	public class HashTable {
		private const int DELETED = int.MaxValue;
		private const int NULL = int.MinValue;
		private int size;
		private int[] table;
		public int Count { get; set; }

		/// <summary>
		/// コンストラクタ。
		/// 格納されるであろうデータの数よりも、sizeは大きい値とすべきである。
		/// </summary>
		/// <param name="size"></param>
		public HashTable(int size) {
			this.size = size;
			this.table = new int[size];
			for (int i = 0; i < this.table.Length; i++) { this.table[i] = NULL; }
		}

		/// <summary>
		/// データを格納する
		/// </summary>
		/// <param name="data">格納するデータ</param>
		public void Add(int data) {
			if (this.Count >= this.table.Length) {
				Common.ErrorExit("ハッシュテーブルのサイズを超えてAddが呼び出されました。");
			}

			int hashcode = data % this.size;
			// シノニムが発生しなくなるまで、コードをずらす
			while (this.table[hashcode] != DELETED && this.table[hashcode] != NULL) {
				if (this.table[hashcode] == data) { return; }
				hashcode++;
			}

			// ハッシュコードの位置にデータを格納
			this.table[hashcode] = data;
			this.Count++;
		}

		/// <summary>
		/// ハッシュテーブルからdataを削除する
		/// </summary>
		/// <param name="data">削除するデータ</param>
		public void Remove(int data) {
			if (this.Count == 0) {
				return;
			}

			int hashcode = data % this.size;
			// NULLに到達するまで値を調べる
			while (this.table[hashcode] != NULL) {
				if (this.table[hashcode] == data) {
					this.table[hashcode] = DELETED;
					this.Count--;
					return;
				}
				hashcode++;
			}
		}

		/// <summary>
		/// ハッシュテーブルにデータが含まれているかどうか
		/// </summary>
		/// <param name="data">データ</param>
		/// <returns>含まれているか？</returns>
		public bool IsContains(int data) {
			int hashcode = data % this.size;
			// NULLに到達するまで値を調べる
			while (this.table[hashcode] != NULL) {
				if (this.table[hashcode] == data) { return true; }
				hashcode++;
			}

			return false;
		}

		/// <summary>
		/// 格納されているデータを配列にして返す
		/// </summary>
		/// <returns>データの配列</returns>
		public int[] ToArray() {
			int index = 0;
			int[] result = new int[this.Count];

			foreach (int e in this.table) {
				if (e != NULL && e != DELETED) {
					result[index] = e;
					index++;
				}
			}

			return result;
		}

		/// <summary>
		/// テスト用メインメソッド
		/// </summary>
		public static void Main(string[] args) {
			HashTable table = new HashTable(4);

			table.Add(1024);
			foreach (int e in table.table) { Console.Write(e + ","); }
			Console.WriteLine();
			Console.WriteLine("count : " + table.Count);

			table.Add(1024);
			foreach (int e in table.table) { Console.Write(e + ","); }
			Console.WriteLine();
			Console.WriteLine("count : " + table.Count);

			table.Add(7);
			foreach (int e in table.table) { Console.Write(e + ","); }
			Console.WriteLine();
			Console.WriteLine("count : " + table.Count);

			table.Add(0);
			foreach (int e in table.table) { Console.Write(e + ","); }
			Console.WriteLine();
			Console.WriteLine("count : " + table.Count);

			Console.WriteLine(table.IsContains(1024));
			Console.WriteLine(table.IsContains(7));
			Console.WriteLine(table.IsContains(0));

			table.Remove(1024);
			foreach (int e in table.table) { Console.Write(e + ","); }
			Console.WriteLine();
			Console.WriteLine("count : " + table.Count);

			Console.WriteLine(table.IsContains(7));
			Console.WriteLine(table.IsContains(0));

			table.Add(4);
			foreach (int e in table.table) { Console.Write(e + ","); }
			Console.WriteLine();
			Console.WriteLine("count : " + table.Count);

			int[] array = table.ToArray();
			foreach (int e in array) { Console.Write(e + ","); }
			Console.WriteLine();
			Console.WriteLine("count : " + table.Count);
		}
	}
}
