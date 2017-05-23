namespace CombinatorialOptimization.src.util {
	/// <summary>
	/// LinkListのノードを表すクラス
	/// </summary>
	class LinkNode {
		// データ
		private int data;
		// 前のノード 無いならnull
		public LinkNode prev { get; set; }
		// 次のノード 無いならnull
		public LinkNode next { get; set; }

		public LinkNode(int data, LinkNode prev = null, LinkNode next = null) {
			this.data = data;
			this.prev = prev;
			this.next = next;
			
			if (prev != null) {
				prev.next = this;
			}
			if (next != null) {
				next.prev = this;
			}
		}
	}
}
