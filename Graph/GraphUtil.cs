using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinatorialOptimization.Graph {
	class GraphUtil {
		/// <summary>
		/// エッジを表す配列edgeを受け取り、nodeではない方のノードを返す
		/// </summary>
		/// <param name="node">ノードID。ただし、edge[0],edge[1]のどちらかに含まれている必要がある</param>
		/// <param name="edge">エッジを表す配列</param>
		/// <returns>edge[0],edge[1]のうち、nodeでない方</returns>
		public static int GetOpposite(int node, int[] edge) {
			return (edge[0] == node) ? edge[1] : edge[0];
		}
	}
}
