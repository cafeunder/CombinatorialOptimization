
using CombinatorialOptimization.Util;

namespace CombinatorialOptimization.Graph.Structure {
	/// <summary>
	/// 隣接リストを表す抽象クラス
	/// </summary>
	abstract class AdjacencyList {
		// ノードの個数
		public int NodeNum { get; protected set; }
		// エッジの個数
		public int EdgeNum { get; protected set; }
		// エッジの配列
		public int[][] EdgeList { get; protected set; }
		// 有向グラフか？
		public abstract bool IsDirected { get; protected set; }

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
	}
}
