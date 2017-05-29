using System;
using CombinatorialOptimization.Graph.structure;
using CombinatorialOptimization.Util;

namespace CombinatorialOptimization.Graph.graphScanning {
	class GraphScanning {
		public static int[] Calc(AdjacencyList graph, int nodeId) {
			// 作業領域を確保
			HashTable results = new HashTable(
				(graph.NodeNum < int.MaxValue / 10) ? graph.NodeNum * 10 : graph.NodeNum);
			Queue nodeQ = new Queue(graph.NodeNum);
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

		public static void Main(string[] args) {
			AdjacencyList graph = GRPReader.ReadGraph(Common.INSTANCE_PATH + @"2group_sample.grp");

			int[] result = GraphScanning.Calc(graph, 0);
			foreach (int node in result) { Console.WriteLine(node); }
		}
	}
}
