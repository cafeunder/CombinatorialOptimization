
using CombinatorialOptimization.Util;

namespace CombinatorialOptimization.Graph.structure {
	/// <summary>
	/// 無向グラフの隣接リストを表すクラス
	/// </summary>
	class UndirectedAdjacencyList : AdjacencyList {
		private LinkList[] linkedEdgeList;
		public override bool IsDirected { get { return false; } protected set { } }

		public UndirectedAdjacencyList(int nodeNum, int edgeNum, int[][] edgeList) {
			this.NodeNum = nodeNum;
			this.EdgeNum = edgeNum;
			this.EdgeList = new int[edgeNum][];
			this.linkedEdgeList = new LinkList[nodeNum];

			// ノードごとに接続エッジリストを生成
			for (int i = 0; i < nodeNum; i++) {
				this.linkedEdgeList[i] = new LinkList();
			}

			// エッジの値をコピーし、接続エッジリストにエッジIDを追加する
			for (int i = 0; i < edgeNum; i++) {
				this.EdgeList[i] = new int[2];
				this.EdgeList[i][0] = edgeList[i][0];
				this.EdgeList[i][1] = edgeList[i][1];

				// fromのエッジリストにエッジIDを追加
				LinkList list = this.linkedEdgeList[edgeList[i][0]];
				list.AddNode(i, list.tail);

				// toのエッジリストにエッジIDを追加
				list = this.linkedEdgeList[edgeList[i][1]];
				list.AddNode(i, list.tail);
			}
		}

		public override LinkList GetInLinkedEdgeList(int node) {
			return this.linkedEdgeList[node];
		}
		public override LinkList GetOutLinkedEdgeList(int node) {
			return this.linkedEdgeList[node];
		}
	}
}
