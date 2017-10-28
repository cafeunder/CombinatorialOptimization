using System;
using CombinatorialOptimization.Graph.Structure;
using CombinatorialOptimization.Util;

namespace CombinatorialOptimization.Graph.Algorithm.graphScanning {
	class GraphScanning {
		/// <summary>
		/// 入力されたgraphにおける、nodeIdから到達可能なノードを返すメソッド。
		/// Stackを用いた深さ優先探索を行う。
		/// </summary>
		/// <param name="graph">グラフ</param>
		/// <param name="nodeId">ノードID</param>
		/// <returns>ノードIDから到達可能な点の配列</returns>
		public static int[] CalcDepth(AdjacencyList graph, int nodeId) {
			// 作業領域を確保
			HashTable results = new HashTable(
				(graph.NodeNum < int.MaxValue / 10) ? graph.NodeNum * 10 : graph.NodeNum);
			Stack<int> nodeS = new Stack<int>(graph.NodeNum);
			bool[] visited = new bool[graph.NodeNum];

			// 初期化
			results.Add(nodeId);
			nodeS.Push(nodeId);
			visited[nodeId] = true;

			// メインループ
			while (nodeS.Count != 0) {
				int v = nodeS.Pop();
				LinkList list = graph.GetOutLinkedEdgeList(v);

				// ノードvから出ているノードを検索
				for (LinkNode node = list.head; node != null; node = node.next) {
					int w = GraphUtil.GetOpposite(v, graph.EdgeList[node.data]);
					results.Add(w);

					if (!visited[w]) {
						visited[w] = true;
						nodeS.Push(w);
					}
				}
			}

			return results.ToArray();
		}

		/// <summary>
		/// 入力されたgraphにおける、nodeIdから到達可能なノードを返すメソッド。
		/// Queueを用いた幅優先探索を行う。
		/// </summary>
		/// <param name="graph">グラフ</param>
		/// <param name="nodeId">ノードID</param>
		/// <returns>ノードIDから到達可能な点の配列</returns>
		public static int[] CalcBreadth(AdjacencyList graph, int nodeId) {
			// 作業領域を確保
			HashTable results = new HashTable(
				(graph.NodeNum < int.MaxValue / 10) ? graph.NodeNum * 10 : graph.NodeNum);
			Queue<int> nodeQ = new Queue<int>(graph.NodeNum);
			bool[] visited = new bool[graph.NodeNum];

			// 初期化
			results.Add(nodeId);
			nodeQ.Enqueue(nodeId);
			visited[nodeId] = true;

			// メインループ
			while (nodeQ.Count != 0) {
				int v = nodeQ.Dequeue();
				LinkList list = graph.GetOutLinkedEdgeList(v);

				// ノードvから出ているノードを検索
				for (LinkNode node = list.head; node != null; node = node.next) {
					int w = GraphUtil.GetOpposite(v, graph.EdgeList[node.data]);
					results.Add(w);

					if (!visited[w]) {
						visited[w] = true;
						nodeQ.Enqueue(w);
					}
				}
			}

			return results.ToArray();
		}

		/// <summary>
		/// テスト用メインメソッド
		/// </summary>
		public static void Main(string[] args) {
			AdjacencyList graph = GRPReader.ReadGraph(Common.INSTANCE_PATH + @"tree_sample.grp");

			int[] result = GraphScanning.CalcBreadth(graph, 0);
			Console.WriteLine("===result===");
			foreach (int node in result) { Console.Write(node + ","); }
			Console.WriteLine();
		}
	}
}
