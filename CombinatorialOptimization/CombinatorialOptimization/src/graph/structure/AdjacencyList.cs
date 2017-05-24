using CombinatorialOptimization.src.util;

namespace CombinatorialOptimization.src.graph.structure {
	/// <summary>
	/// 隣接リストを表す抽象クラス
	/// </summary>
	abstract class AdjacencyList {
		// ノードの個数
		public int nodeNum { get; set; }
		// エッジの個数
		public int edgeNum { get; set; }
		// エッジの配列
		public int[][] edgeList { get; set; }

		/// <summary>
		/// nodeに入るエッジのリンクリストを返す。
		/// 無向の場合はnodeに接続する全てのエッジのリンクリストを返す
		/// </summary>
		/// <param name="node">ノードID</param>
		/// <returns>nodeに入るエッジのリンクリスト</returns>
		public abstract LinkList GetInLinkedEdgeList(int node); 

		/// <summary>
		/// nodeから出るエッジのリンクリストを返す。
		/// 無向の場合はnodeに接続する全てのエッジのリンクリストを返す
		/// </summary>
		/// <param name="node">ノードID</param>
		/// <returns>nodeから出るエッジのリンクリスト</returns>
		public abstract LinkList GetOutLinkedEdgeList(int node); 

		/// <summary>
		/// 有向グラフかどうかを返す
		/// </summary>
		/// <returns>有向グラフか？</returns>
		public abstract bool IsDirected();
	}
}
