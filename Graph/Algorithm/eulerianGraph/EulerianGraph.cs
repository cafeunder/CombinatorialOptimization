using System;
using CombinatorialOptimization.Graph.Structure;
using CombinatorialOptimization.Util;
using CombinatorialOptimization.Graph.Algorithm.graphScanning;

namespace CombinatorialOptimization.Graph.Algorithm.eulerianGraph {
	class EulerianGraph {
		public static bool JudgeEulerian(AdjacencyList graph) {
			// 連結性判定
			int[] cc = GraphScanning.CalcDepth(graph, 0);
			if (cc.Length != graph.NodeNum) {
				return false;
			}

			// 有向グラフ
			if (graph.IsDirected) {
				// 全てのノードにおいて、入次数と出次数が同じならオイラーグラフ
				for (int i = 0; i < graph.NodeNum; i++) {
					// Inリストのノード数が入次数
					LinkList list = graph.GetInLinkedEdgeList(i);
					int in_count = 0;
					for (LinkNode node = list.head; node != null; node = node.next) {
						in_count++;
					}
					// Outリストのノード数が出次数
					list = graph.GetOutLinkedEdgeList(i);
					int out_count = 0;
					for (LinkNode node = list.head; node != null; node = node.next) {
						out_count++;
					}

					// 入次数と出次数が異なるなら終了
					if (in_count != out_count) {
						return false;
					}
				}
			// 無向グラフの場合
			} else {
				// 全てのノードの次数が偶数ならオイラーグラフ
				for (int i = 0; i < graph.NodeNum; i++) {
					// リストのノード数が次数
					LinkList list = graph.GetInLinkedEdgeList(i);
					int count = 0;
					for (LinkNode node = list.head; node != null; node = node.next) {
						count++;
					}
					// 次数が奇数なら終了
					if (count % 2 != 0) {
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// 入力されたオイラーグラフのオイラー路を返す。
		/// 入力がオイラーグラフであるかは判定せず、そうでない場合は正常に動作しない。
		/// </summary>
		/// <param name="graph">オイラーグラフ</param>
		/// <returns>オイラー路</returns>
		public static int[] Calc(AdjacencyList graph) {
			bool[] passed = new bool[graph.EdgeNum];
			// 各ノードから出発できるエッジのリスト
			// current_list[i]: ノードiに接続しているノードのリスト
			LinkNode[] current_list = new LinkNode[graph.NodeNum];
			for (int i = 0; i < graph.NodeNum; i++) {
				current_list[i] = graph.GetOutLinkedEdgeList(i).head;
			}

			// 再帰呼び出し用のスタック
			Stack<LinkNode> v_stack = new Stack<LinkNode>(graph.NodeNum);
			// オイラー路
			LinkList eulerian = new LinkList();
			// 出発点としてノード0を追加しておく
			eulerian.AddNode(0);
			v_stack.Push(eulerian.head);
			// 再帰呼び出し用のスタックが空になるまで
			while (v_stack.CheckPop(out var v)) {
				int x = v.data;
				// この反復で得られるウォーク
				LinkList walk = new LinkList();

				// 行けるノードが無くなるまでウォークを構成する
				while (current_list[x] != null) {
					int edgeID = current_list[x].data;
					current_list[x] = current_list[x].next;
					// ノードから出発できるエッジでも通過済みならウォークにしない
					if (!passed[edgeID]) {
						int y = GraphUtil.GetOpposite(x, graph.EdgeList[edgeID]);
						walk.AddNode(y, walk.tail);
						passed[edgeID] = true;
						//次のノードへ移動
						x = y;
					}
				}


				// 出発点のノードから行けるエッジがあるなら
				if (current_list[v.data] != null) { v_stack.Push(v); }
				// ウォークの途中のノードから行けるエッジがあるなら
				for (LinkNode node = walk.head; node != null; node = node.next) {
					if (current_list[node.data] != null) {
						v_stack.Push(node);
					}
				}

				// 出発点のノードの後ろにウォークを追加する
				eulerian.ConnectList(walk, v);
			}

			// idの配列に変換
			int count = 0;
			int[] result = new int[graph.EdgeNum + 1];
			for (LinkNode node = eulerian.head; node != null; node = node.next) {
				result[count] = node.data;
				count++;
			}

			return result;
		}

		public static void Main(string[] args) {
			AdjacencyList graph = GRPReader.ReadGraph(Common.INSTANCE_PATH + @"eulerian_sample3.grp");

			if (!JudgeEulerian(graph)) {
				Common.ErrorExit("インスタンスはオイラーグラフである必要があります。");
			}
			int[] result = Calc(graph);

			Console.WriteLine("===result===");
			foreach (int node in result) { Console.Write(node + ","); }
			Console.WriteLine();
		}
	}
}
