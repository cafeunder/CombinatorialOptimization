using System;
using System.IO;
using System.Reflection;
using CombinatorialOptimization.src.graph.structure;
using CombinatorialOptimization.src.util;

namespace CombinatorialOptimization.src.graph {
	/// <summary>
	/// グラフ定義ファイル(.grp)を読み込むクラス
	/// </summary>
	class GRPReader {
		static AdjacencyList ReadGraph(string filepath) {
			StreamReader reader = new StreamReader(filepath);

			int no_of_node = -1;
			int no_of_edge = -1;
			bool directed = false;
			while (!reader.EndOfStream) {
				string[] record = reader.ReadLine().Split(' ');

				// 1フィールド目が属性
				switch (record[0]) {
				case "TYPE":
					directed = record[2] == "DIRECTED";
					break;
				case "NO_OF_NODE":
					no_of_node = int.Parse(record[2]);
					break;
				case "NO_OF_EDGE":
					no_of_edge = int.Parse(record[2]);
					break;
				case "EDGE_CODE_SECTION":
					goto READ_EDGE_DATA;
				}
			}
		// エッジデータの読み込み
		READ_EDGE_DATA:
			if (no_of_node == -1) { Common.ErrorExit(MethodBase.GetCurrentMethod().Name, "ノード数の指定がありません。"); }
			if (no_of_edge == -1) { Common.ErrorExit(MethodBase.GetCurrentMethod().Name, "エッジ数の指定がありません。"); }

			int[][] edge_list = new int[no_of_edge][];
			for (int i = 0; i < no_of_edge; i++) {
				if (reader.EndOfStream) {
					Common.ErrorExit(MethodBase.GetCurrentMethod().Name, "NO_OF_EDGEで指定されたエッジ数よりも少ないデータが記載されています。");
				}
				string[] record = reader.ReadLine().Split(' ');
				edge_list[i] = new int[2];
				edge_list[i][0] = int.Parse(record[0]);
				edge_list[i][1] = int.Parse(record[1]);
			}

			if (!reader.EndOfStream) {
				Common.ErrorExit(MethodBase.GetCurrentMethod().Name, "NO_OF_EDGEで指定されたエッジ数よりも多いデータが記載されています。");
			}

			AdjacencyList instance;
			if (directed) {
				instance = new DirectedAdjacencyList(no_of_node, no_of_edge, edge_list);
			} else {
				instance = new UndirectedAdjacencyList(no_of_node, no_of_edge, edge_list);
			}
			return instance;
		}

		/// <summary>
		/// テスト用メインメソッド
		/// </summary>
		public static void Main(String[] args) {
			// AdjacencyList graph = ReadGraph(Common.INSTANCE_PATH + @"graph\undirected_sample.grp");
			AdjacencyList graph = ReadGraph(Common.INSTANCE_PATH + @"graph\directed_sample.grp");

			Console.WriteLine("n : " + graph.nodeNum);
			Console.WriteLine("m : " + graph.edgeNum);
			foreach (int[] edge in graph.edgeList) {
				Console.WriteLine(edge[0] + "," + edge[1]);
			}

			Console.WriteLine("\n===== in =====");
			for (int i = 0; i < graph.nodeNum; i++) {
				Console.WriteLine("===" + i + "===");
				for (LinkNode node = graph.GetInLinkedEdgeList(i).head; node != null; node = node.next) {
					Console.WriteLine(node.data);
				}
			}

			Console.WriteLine("\n===== out =====");
			for (int i = 0; i < graph.nodeNum; i++) {
				Console.WriteLine("===" + i + "===");
				for (LinkNode node = graph.GetOutLinkedEdgeList(i).head; node != null; node = node.next) {
					Console.WriteLine(node.data);
				}
			}
		}
	}
}
