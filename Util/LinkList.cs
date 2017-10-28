namespace CombinatorialOptimization.Util {
	/// <summary>
	/// リンクリストを表すクラス
	/// </summary>
	public class LinkList {
		// リストの先頭のノード
		public LinkNode head { get; set; }
		// リストの末尾のノード
		public LinkNode tail { get; set; }

		/// <summary>
		/// ノードを追加する
		/// </summary>
		/// <param name="data">ノードのデータ</param>
		/// <param name="prev">追加するノードの前にくるデータ。未指定の場合はheadになる</param>
		public LinkNode AddNode(int data, LinkNode prev = null) {
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

			return add_node;
		}

		/// <summary>
		/// リストの途中にリストを連結する
		/// </summary>
		/// <param name="list">連結するリスト</param>
		/// <param name="prev">連結するリストの前のノード</param>
		public void ConnectList(LinkList list, LinkNode prev = null) {
			if (list.head == null) { return; }
			if (prev == null) {
				LinkNode temp = this.head;
				this.head = list.head;
				if (temp != null) {
					list.tail.next = temp; 
					temp.prev = list.tail;
				} else {
					this.tail = list.tail;
				}
			} else {
				// prev.nextがnullなら、prevはtail
				if (prev.next == null) {
					this.tail = list.tail;
				} else {
					list.tail.next = prev.next;
					prev.next.prev = list.tail;
				}
				list.head.prev = prev;
				prev.next = list.head;
			}
		}

		public static void Main(string[] args) {
			LinkList list1 = new LinkList();
			list1.AddNode(2, list1.AddNode(1, list1.AddNode(0)));

			LinkList list2 = new LinkList();
			list2.AddNode(6, list2.AddNode(5, list2.AddNode(4)));


		}
	}
}
