using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombinatorialOptimization.Graph.structure;
using CombinatorialOptimization.Util;

namespace CombinatorialOptimization.Graph.stronglyConnectedComponent {
	class StronglyConnectedComponent {
		private const int UNVISITED = -1;
		private const int VISITED = -2;

		/// <summary>
		/// 正方向の後順探索で各接点に訪問する
		/// </summary>
		/// <param name="graph">グラフ</param>
		/// <returns>ノード→訪問順配列と、訪問順→ノード配列</returns>
		private static (int[] orderArray, int[] invOrderArray) Visit(DirectedAdjacencyList graph) {
			// 作業領域を確保
			int[] order_array = new int[graph.NodeNum];
			int[] inv_order_array = new int[graph.NodeNum];
			for (int i = 0; i < graph.NodeNum; i++) { order_array[i] = UNVISITED; }

			int order = 0;
			Stack visit_stack = new Stack(graph.NodeNum);
			for (int i = 0; i < graph.NodeNum; i++) {
				// 未訪問のノードから始める
				if (order_array[i] != UNVISITED) { continue; }
				visit_stack.Clear();

				visit_stack.Push(i);
				order_array[i] = VISITED;
				while (visit_stack.Count != 0) {
					int v = visit_stack.Peak();

					// vから到達できる未訪問のノードに訪問する
					bool move = false;
					LinkList edgeList = graph.GetOutLinkedEdgeList(v);
					for (LinkNode node = edgeList.head; node != null; node = node.next) {
						int w = GraphUtil.GetOpposite(v, graph.EdgeList[node.data]);
						if (order_array[w] == UNVISITED) {
							visit_stack.Push(w);
							order_array[w] = VISITED;
							move = true;
						}
					}

					// どこにも移動できなかったら後順探索に従って番号付け
					if (!move) {
						visit_stack.Pop();
						order_array[v] = order;
						inv_order_array[order] = v;
						order++;
					}
				}
			}

			return (order_array, inv_order_array);
		}

		/// <summary>
		/// 逆方向の前順探索で各接点に訪問する
		/// </summary>
		/// <param name="graph">グラフ</param>
		/// <param name="invOrderArray">訪問順→ノード配列</param>
		/// <returns>ノード→クラスタID配列</returns>
		public static int[] AntiVisit(DirectedAdjacencyList graph, int[] invOrderArray) {
			int[] result = new int[graph.NodeNum];
			for (int i = 0; i < result.Length; i++) { result[i] = UNVISITED; }

			int cluster_id = 0;
			Stack visit_stack = new Stack(graph.NodeNum);
			for (int i = 0; i < graph.NodeNum; i++) {
				// i番目に大きい訪問順を持つノードから始める
				int s = invOrderArray[(graph.NodeNum - 1) - i];
				// 訪問済みならスキップ
				if (result[s] != UNVISITED) { continue; }
				visit_stack.Clear();

				visit_stack.Push(s);
				result[s] = cluster_id;
				while (visit_stack.Count != 0) {
					int v = visit_stack.Pop();

					// 反有向木において、vから到達できる未訪問のノードに訪問する
					LinkList edgeList = graph.GetInLinkedEdgeList(v);
					for (LinkNode node = edgeList.head; node != null; node = node.next) {
						int w = GraphUtil.GetOpposite(v, graph.EdgeList[node.data]);
						if (result[w] == UNVISITED) {
							visit_stack.Push(w);
							result[w] = cluster_id;
						}
					}
				}
				cluster_id++;
			}

			return result;
		}

		/// <summary>
		/// 有向グラフの強連結成分を判定する
		/// </summary>
		/// <param name="graph">グラフ</param>
		/// <returns>ノード→クラスタID配列</returns>
		public static int[] Calc(DirectedAdjacencyList graph) {
			var results = Visit(graph);
			int[] cluster_array = AntiVisit(graph, results.invOrderArray);

			return cluster_array;
		}

		/// <summary>
		/// テスト用メインメソッド
		/// </summary>
		public static void Main(string[] args) {
			AdjacencyList graph = GRPReader.ReadGraph(Common.INSTANCE_PATH + @"scc_sample2.grp");
			if (!graph.IsDirected) {
				Common.ErrorExit("StronglyConnectedComponentアルゴリズムは、有向辺グラフにのみ適用できます");
			}

			int[] cluster_array = Calc((DirectedAdjacencyList)graph);
			foreach (int node in cluster_array) { Console.WriteLine(node); }
		}
	}
}
