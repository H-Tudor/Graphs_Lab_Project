namespace map_final_testbed {
	public class Graph {
		public List<Node> nodes;
		public List<int[]> edge_list;
		public int[,] adjacency_matrix;
		public Dictionary<int, List<int[]>> adjacency_dict;

		public int NodesCount { get { return nodes.Count; } }

		public Graph(List<Node> nodes, int[,] adjacency_matrix) {
			GuardRail(nodes, adjacency_matrix);

			this.nodes = nodes;
			this.adjacency_matrix = adjacency_matrix;
			this.edge_list = GetEdgeListFromAdjacencyMatrix(adjacency_matrix);
			this.adjacency_dict = GetAdjacencyDictFromEdgeList(this.nodes, this.edge_list);
		}

		public Graph(List<Node> nodes, List<int[]> edge_list) {
			this.nodes = nodes;
			this.edge_list = edge_list;
			this.adjacency_matrix = GetAdjacencyMatrixFromEdgeList(nodes, edge_list);
			this.adjacency_dict = GetAdjacencyDictFromEdgeList(this.nodes, this.edge_list);
		}

		private void GuardRail(List<Node> nodes, int[,] adjacency_matrix) {
			int nodes_count = nodes.Count();
			int adj_matrix_r0 = adjacency_matrix.GetLength(0);
			int adj_matrix_r1 = adjacency_matrix.GetLength(1);

			if(adj_matrix_r0 != adj_matrix_r1) {
				throw new Exception($"Adjacency Matrix is not square ({adj_matrix_r0}, {adj_matrix_r1})");
			}

			if(nodes_count != adj_matrix_r0) {
				throw new Exception($"The number of nodes differs from the length of the adjacency matrix ({nodes_count} {adj_matrix_r0})");
			}
		}

		private List<int[]> GetEdgeListFromAdjacencyMatrix(int[,] adjacency_matrix) {
			List<int[]> edge_list = new List<int[]>();

			for(int i = 0; i < adjacency_matrix.GetLength(0) - 1; i++) {
				for(int j = i + 1; j < adjacency_matrix.GetLength(1); j++) {
					if(adjacency_matrix[i, j] != 0) {
						edge_list.Add([i, j, adjacency_matrix[i, j]]);
					}
				}
			}

			return edge_list;
		}

		private int[,] GetAdjacencyMatrixFromEdgeList(List<Node> nodes, List<int[]> edge_list) {
			int[,] adjacency_matrix = new int[nodes.Count, nodes.Count];

			foreach(int[] edge in edge_list) {
				adjacency_matrix[edge[0], edge[1]] = edge[2];
				adjacency_matrix[edge[1], edge[0]] = edge[2];
			}

			return adjacency_matrix;
		}

		private Dictionary<int, List<int[]>> GetAdjacencyDictFromEdgeList(List<Node> nodes, List<int[]> edge_list) {
			Dictionary<int, List<int[]>> adj_dict = [];

			for(int i = 0; i < nodes.Count(); i++) {
				adj_dict[i] = [];
			}

			foreach(int[] edge in edge_list) {
				adj_dict[edge[0]].Add([edge[1], edge[2]]);
				adj_dict[edge[1]].Add([edge[0], edge[2]]);
			}

			return adj_dict;
		}
	}
}
