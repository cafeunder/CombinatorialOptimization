using CombinatorialOptimization.src.util;

namespace CombinatorialOptimization.src.graph.structure {
	/// <summary>
	/// 無向グラフの隣接リストを表すクラス
	/// </summary>
	class UndirectedAdjacencyList {
		private int nodeNum;
		private int edgeNum;
		private int[][] edgeList;
		private LinkList[] linkedEdgeList;

		public UndirectedAdjacencyList(int nodeNum, int edgeNum, int[][] edgeList) {
			this.nodeNum = nodeNum;
			this.edgeNum = edgeNum;
			this.edgeList = new int[edgeNum][];
			this.linkedEdgeList = new LinkList[nodeNum];

			// ノードごとに接続エッジリストを生成
			for (int i = 0; i < nodeNum; i++) {
				this.linkedEdgeList[i] = new LinkList();
			}

			// エッジの値をコピーし、接続エッジリストにエッジIDを追加する
			for (int i = 0; i < edgeNum; i++) {
				this.edgeList[i] = new int[2];
				this.edgeList[i][0] = edgeList[i][0];
				this.edgeList[i][1] = edgeList[i][1];

				// fromのエッジリストにエッジIDを追加
				LinkList list = this.linkedEdgeList[edgeList[i][0]];
				list.AddNode(i, list.tail);

				// toのエッジリストにエッジIDを追加
				list = this.linkedEdgeList[edgeList[i][1]];
				list.AddNode(i, list.tail);
			}
		}
	}
}
