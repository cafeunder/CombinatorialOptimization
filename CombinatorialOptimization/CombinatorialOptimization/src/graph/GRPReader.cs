using System;
using System.IO;
using CombinatorialOptimization.src.graph.structure;

namespace CombinatorialOptimization.src.graph {
	class GRPReader {
		static UndirectedAdjacencyList ReadUndirectedGraph(string filepath) {
			UndirectedAdjacencyList instance = null;
			try {
				StreamReader reader = new StreamReader(filepath);

				int no_of_node = -1;
				int no_of_edge = -1;
				while (!reader.EndOfStream) {
					string[] record = reader.ReadLine().Split(' ');

					// 1フィールド目が属性
					switch (record[0]) {
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
			READ_EDGE_DATA:
				bool failure = false;
				if (no_of_node == -1) {
					Console.WriteLine("エラー(GRPReader) : ノード数の指定がありません。");
					failure = true;
				}
				if (no_of_edge == -1) {
					Console.WriteLine("エラー(GRPReader) : エッジ数の指定がありません。");
					failure = true;
				}
				if (failure) {
					Environment.Exit(1);
				}

				int[][] edge_list = new int[no_of_edge][];
				for (int i = 0; i < no_of_edge; i++) {
					if (reader.EndOfStream) {
						Console.WriteLine("エラー(GRPReader) : NO_OF_EDGEで指定されたエッジ数よりも少ないデータが記載されています。");
						Environment.Exit(1);
					}
					string[] record = reader.ReadLine().Split(' ');
					edge_list[i][0] = int.Parse(record[0]);
					edge_list[i][1] = int.Parse(record[1]);
				}

				if (!reader.EndOfStream) {
					Console.WriteLine("エラー(GRPReader) : NO_OF_EDGEで指定されたエッジ数よりも多いデータが記載されています。");
					Environment.Exit(1);
				}

				instance = new UndirectedAdjacencyList(no_of_node, no_of_edge, edge_list);
			} catch (Exception e) {
				Console.WriteLine(e.StackTrace);
				Environment.Exit(1);
			}

			return instance;
		}
	}
}
