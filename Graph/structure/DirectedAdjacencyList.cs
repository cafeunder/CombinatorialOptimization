
using CombinatorialOptimization.Util;

namespace CombinatorialOptimization.Graph.Structure {
	/// <summary>
	/// 有向グラフの隣接リストを表すクラス
	/// </summary>
	class DirectedAdjacencyList : AdjacencyList {
		private LinkList[] inEdgeList;
		private LinkList[] outEdgeList;
		public override bool IsDirected { get { return true; } protected set { } }

		public DirectedAdjacencyList(int nodeNum, int edgeNum, int[][] edgeList) {
			this.NodeNum = nodeNum;
			this.EdgeNum = edgeNum;
			this.EdgeList = new int[edgeNum][];
			this.inEdgeList = new LinkList[nodeNum];
			this.outEdgeList = new LinkList[nodeNum];

			// ノードごとに接続エッジリストを生成
			for (int i = 0; i < nodeNum; i++) {
				this.inEdgeList[i] = new LinkList();
				this.outEdgeList[i] = new LinkList();
			}

			// エッジの値をコピーし、接続エッジリストにエッジIDを追加する
			for (int i = 0; i < edgeNum; i++) {
				this.EdgeList[i] = new int[2];
				this.EdgeList[i][0] = edgeList[i][0];
				this.EdgeList[i][1] = edgeList[i][1];

				// fromのoutエッジリストにエッジIDを追加
				LinkList list = this.outEdgeList[edgeList[i][0]];
				list.AddNode(i, list.tail);

				// toのinエッジリストにエッジIDを追加
				list = this.inEdgeList[edgeList[i][1]];
				list.AddNode(i, list.tail);
			}
		}

		public override LinkList GetInLinkedEdgeList(int node) {
			return this.inEdgeList[node];
		}
		public override LinkList GetOutLinkedEdgeList(int node) {
			return this.outEdgeList[node];
		}
	}
}
