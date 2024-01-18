namespace lab_final {
	public class GraphOperations {
		public static int nr_of_nodes;
		public static int[] adv;
		public static int[,] adj;
		public static int[,] matrix;
		public static bool[] visited;

		public static void Main(string[] args) {
			adj = new int[nr_of_nodes, nr_of_nodes];
			matrix = new int[nr_of_nodes, nr_of_nodes];
			visited = new bool[nr_of_nodes];
		}

		public static void LoadFromFile(string file_path) {
			TextReader reader = new StreamReader(file_path);
			string buffer = reader.ReadLine();
			nr_of_nodes = int.Parse(buffer);
			matrix = new int[nr_of_nodes, nr_of_nodes];

			while((buffer = reader.ReadLine()) != null) {
				string[] nodes = buffer.Split(' ');
				int i = int.Parse(nodes[0]);
				int j = int.Parse(nodes[1]);
				int w = nodes.Length == 3 ? int.Parse(nodes[3]) : 1;

				matrix[i, j] = matrix[j, i] = w;
			}
		}

		public static void BreathFirstSearch(int start_node) {
			visited = new bool[nr_of_nodes];
			Queue<int> queue = new Queue<int>();
			queue.Enqueue(start_node);
			visited[start_node] = true;

			while(queue.Count() > 0) {
				int current = queue.Dequeue();

				for(int j = 0; j < nr_of_nodes; j++) {
					if(matrix[current, j] == 1 && !visited[j]) {
						queue.Enqueue(j);
					}
				}
			}
		}

		public static void DepthFirstSearch(int node_id) {
			if(visited[node_id] == true)
				return;

			visited[node_id] = true;
			for(int j = 0; j < nr_of_nodes; j++) {
				if(matrix[node_id, j] == 1)
					DepthFirstSearch(j);
			}
		}

		public static int[] Coloring(int nr_of_colors) {
			int[] colors = new int[nr_of_colors];

			for(int i = 0; i < nr_of_nodes; i++) {
				colors[i] = -1;
			}

			colors[0] = 0;
			for(int i = 1; i < nr_of_nodes; i++) {
				bool[] local = new bool[nr_of_colors];
				for(int j = 0; j < nr_of_nodes; j++) {
					if(matrix[i, j] == 1 && colors[j] != -1) {
						local[colors[j]] = true;
					}
				}

				int index = 0;
				while(local[index]) {
					index++;
				}

				colors[i] = index;
			}

			return colors;
		}

		public int[] Dijkstra(int start_node) {
			int[] dist = new int[nr_of_nodes];
			for(int i = 0; i < nr_of_nodes; i++) {
				dist[i] = int.MaxValue;
			}

			Queue<int> queue = new Queue<int>();
			dist[start_node] = 0;
			queue.Enqueue(start_node);

			while(queue.Count() > 0) {
				int current = queue.Dequeue();

				for(int j = 0; j < nr_of_nodes; j++) {
					if(matrix[current, j] == 0) {
						continue;
					}

					int neighbors = j;
					if((dist[current] + matrix[current, j]) < dist[j]) {
						dist[j] = dist[current] + matrix[current, j];
						queue.Enqueue(j);
					}
				}
			}

			for(int i = 0; i < nr_of_nodes; i++) {
				if(dist[i] == int.MaxValue) {
					dist[i] = -1;
				}
			}

			return dist;
		}

		public static bool CheckHamiltonian() {
			for(int i = 0; i < nr_of_nodes; i++) {
				visited = new bool[nr_of_nodes];
				List<int> path = new List<int>();
				path.Add(i);

				if(PathFind(path) == true) {
					return true;
				}
			}

			return false;
		}

		private static bool PathFind(List<int> path) {
			int current = path[path.Count() - 1];
			visited[current] = true;

			for(int j = 0; j < nr_of_nodes; j++) {
				if(visited[j] == true || matrix[current, j] == 0) {
					continue;
				}

				path.Add(j);
				if(path.Count() == nr_of_nodes) {
					return true;
				}

				if(PathFind(path) == true) {
					return true;
				}

				visited[j] = false;
				path.Remove(j);
			}

			return false;
		}

		public static bool CheckConnected() {
			for(int i = 0; i < nr_of_nodes; i++) {
				visited[i] = false;
			}

			adv = new int[nr_of_nodes];
			int has_adjacent = -1;
			for(int i = 0; i < nr_of_nodes; i++) {
				for(int j = 0; j < nr_of_nodes; j++) {
					if(matrix[i, j] == 1) {
						has_adjacent = i;
						adv[i] += 1;
					}
				}
			}

			if(has_adjacent == -1) {
				return true;
			}

			DepthFirstSearch(has_adjacent);

			for(int i = 0; i < nr_of_nodes; i++) {
				if(visited[i] && adv[i] > 0) {
					return false;
				}
			}

			return true;
		}

		public static int CheckEulerian() {
			if(!CheckConnected()) {
				return 0;
			}

			int odd = 0;
			for(int i = 0; i < nr_of_nodes; i++) {
				if(adv[i] % 2 != 0) {
					odd++;
				}
			}

			if(odd > 2) {
				return 0;
			}

			return (odd == 2) ? 1 : 2;
		}
	}
}