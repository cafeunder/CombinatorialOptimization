namespace CombinatorialOptimization.src.util {
	/// <summary>
	/// リンクリストを表すクラス
	/// </summary>
	class LinkList {
		// リストの先頭のノード
		public LinkNode head { get; set; }
		// リストの末尾のノード
		public LinkNode tail { get; set; }

		/// <summary>
		/// ノードを追加する
		/// </summary>
		/// <param name="data">ノードのデータ</param>
		/// <param name="prev">追加するノードの前にくるデータ。未指定の場合はheadになる</param>
		public void AddNode(int data, LinkNode prev = null) {
			LinkNode add_node;
			if (prev != null) {
				add_node = new LinkNode(data, prev, prev.next);
			} else {
				add_node = new LinkNode(data);

				// prevが無いのでヘッドとする
				if (this.head == null) {
					this.head = add_node;
					this.tail = add_node;
				} else {
					add_node.next = this.head;
					this.head.prev = add_node;
					this.head = add_node;
				}
			}

			// ここまでの操作でnextが存在しないなら、add_nodeは末尾
			if (add_node.next == null) {
				this.tail = add_node;
			}
		}
	}
}
