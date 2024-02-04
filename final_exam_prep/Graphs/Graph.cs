namespace final_exam_prep.Graphs {
	internal class Graph {

		public List<Node> nodes;
		public int[,] adjacency_matrix;
		public List<int[]> edge_list;
		public Dictionary<int, List<int[]>> edge_dict;
		private bool[] visited;

		public int NodesCount { get { return nodes.Count; } }

		/// Input / Output

		public Graph(string filename, bool mode = false, bool debug = false) {
			if(debug)
				Console.WriteLine($"Input File: {filename}");

			TextReader reader = new StreamReader(filename);

			int nr_of_nodes;
			if(int.TryParse(reader.ReadLine(), out nr_of_nodes) == false) {
				throw new Exception("Invalid Input File Format - nr of nodes");
			}

			if(debug)
				Console.WriteLine($"No. of nodes: {nr_of_nodes}");

			nodes = new List<Node>();
			for(int i = 0; i < nr_of_nodes; i++) {
				string[] node_specs = reader.ReadLine().Split(' ');
				int node_x, node_y;
				bool parse_1, parse_2;

				parse_1 = int.TryParse(node_specs[0], out node_x);
				parse_2 = int.TryParse(node_specs[1], out node_y);

				if(!parse_1 || !parse_2) {
					throw new Exception($"Invalid Input File Format - node {i} format ({parse_1}, {parse_2})");
				}

				if(debug)
					Console.WriteLine($"Node {i + 1}: {node_x}, {node_y}");

				nodes[i] = new Node(node_x, node_y);
			}

			if(debug)
				Console.WriteLine("Graph Loading Mode: " + (mode ? "adjacency matrix" : "edge_list"));

			reader.ReadLine();
			if(mode) {
				adjacency_matrix = new int[nr_of_nodes, nr_of_nodes];
				string line;
				for(int i = 0; i < nr_of_nodes; i++) {
					line = reader.ReadLine();
					string[] node_connections = line.Split(' ');

					if(node_connections.Length != nr_of_nodes) {
						throw new Exception($"Invalid Input File Format - invalid connections nr for node {i}");
					}

					int connection;
					for(int j = 0; j < nr_of_nodes; j++) {
						if(!int.TryParse(node_connections[j], out connection)) {
							throw new Exception($"Invalid Input File Format - error parsing node connection {j}");
						}
						adjacency_matrix[i, j] = connection;

						if(debug)
							Console.Write($"{connection} ");
					}

					if(debug)
						Console.WriteLine();
				}

				reader.Close();
				GuardRail(nodes, adjacency_matrix);

				GetEdgeListFromAdjacencyMatrix(adjacency_matrix);
				GetAdjacencyDictFromEdgeList(nodes, this.edge_list);
			} else {
				edge_list = new List<int[]>();

				string line;
				int counter = 0;
				while((line = reader.ReadLine()) != null) {
					if(line == "") {
						continue;
					}

					if(debug)
						Console.WriteLine($"--{line}");

					string[] connection = line.Split(' ');
					if(connection.Length != 3) {
						throw new Exception($"Invalid Input File Format - error parsing edge '{counter}: {line}': insufficient parameters ({connection.Length})");
					}


					int start_node_id, end_node, weight;
					bool parse_1, parse_2, parse_3;

					parse_1 = int.TryParse(connection[0], out start_node_id);
					parse_2 = int.TryParse(connection[1], out end_node);
					parse_3 = int.TryParse(connection[2], out weight);

					if(!parse_1 || !parse_2 || !parse_3) {
						throw new Exception($"Invalid Input File Format - error parsing edge '{counter}: {line}' ({parse_1}, {parse_2}, {parse_3})");
					}

					counter++;
					edge_list.Add([start_node_id, end_node, weight]);

					if(debug)
						Console.WriteLine($"{start_node_id} <-{weight}-> {end_node}");
				}

				reader.Close();
				GetAdjacencyMatrixFromEdgeList(nodes, edge_list);
				GetAdjacencyDictFromEdgeList(this.nodes, this.edge_list);
			}
		}

		public void SaveGraph(string filename = "output.txt", bool mode = true) {
			TextWriter writer = new StreamWriter(filename);

			writer.WriteLine(NodesCount.ToString());
			foreach(Node node in nodes) {
				writer.WriteLine($"{node.x} {node.y}");
			}

			writer.WriteLine();
			if(mode) {
				for(int i = 0; i < NodesCount; i++) {
					for(int j = 0; j < NodesCount; j++) {
						writer.Write($"{adjacency_matrix[i, j]} ");
					}
					writer.Write("\n");
				}
			} else {
				foreach(int[] edge in edge_list) {
					writer.WriteLine($"{edge[0]} {edge[1]} {edge[2]}");
				}
			}

			writer.Close();
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

		/// Connection Transformations

		private void GetEdgeListFromAdjacencyMatrix(int[,] adjacency_matrix) {
			edge_list = new List<int[]>();

			for(int i = 0; i < adjacency_matrix.GetLength(0) - 1; i++) {
				for(int j = i + 1; j < adjacency_matrix.GetLength(1); j++) {
					if(adjacency_matrix[i, j] != 0) {
						edge_list.Add([i, j, adjacency_matrix[i, j]]);
					}
				}
			}
		}

		private void  GetAdjacencyMatrixFromEdgeList(List<Node> nodes, List<int[]> edge_list) {
			adjacency_matrix = new int[NodesCount, NodesCount];

			foreach(int[] edge in edge_list) {
				adjacency_matrix[edge[0], edge[1]] = edge[2];
				adjacency_matrix[edge[1], edge[0]] = edge[2];
			}
		}

		private void GetAdjacencyDictFromEdgeList(List<Node> nodes, List<int[]> edge_list) {
			edge_dict = [];

			for(int i = 0; i < nodes.Count(); i++) {
				edge_dict[i] = [];
			}

			foreach(int[] edge in edge_list) {
				edge_dict[edge[0]].Add([edge[1], edge[2]]);
				edge_dict[edge[1]].Add([edge[0], edge[2]]);
			}
		}

		/// Graph Search Start

		private int NodeToIndex(Node? start_node = null, int start_node_id = -1) {
			if(start_node != null) {
				if(nodes.Contains(start_node) == false) {
					throw new Exception($"Node {start_node} is not a know node of graph {this}");
				}

				start_node_id = nodes.IndexOf(start_node);
			} else if(start_node_id == -1) {
				throw new Exception("Neither Current node or start_node_id provided");
			}

			return start_node_id;
		}

		public void Start_DepthFirstSearch(Node? start_node = null, int start_node_id = -1, bool recursive = false, bool debug = false) {
			start_node_id = NodeToIndex(start_node, start_node_id);

			if(debug)
				Console.WriteLine($"\nDepth First Search: start from {start_node_id}");

			visited = new bool[NodesCount];

			if(recursive)
				DepthFirstSearch_Recursive(start_node_id, debug);
			else
				DepthFirstSearch_Stack(start_node_id, debug);

			Console.WriteLine();
			visited = new bool[NodesCount];
		}

		public void Start_BreathFirstSearch(Node? start_node = null, int start_node_id = -1, bool debug = false) {
			start_node_id = NodeToIndex(start_node, start_node_id);

			if(debug)
				Console.WriteLine($"\nBreath First Search: start from {start_node_id}");

			visited = new bool[NodesCount];
			BreathFirstSearch_Queue(start_node_id, debug);
			Console.WriteLine();
			visited = new bool[NodesCount];
		}

		/// Graph Search Methods

		private void DepthFirstSearch_Recursive(int start_node_id, bool debug = false) {
			if(visited[start_node_id] == true)
				return;

			if(debug)
				Console.Write($"{start_node_id} ");

			visited[start_node_id] = true;
			for(int j = 0; j < NodesCount; j++) {
				if(j == start_node_id)
					continue;

				if(adjacency_matrix[start_node_id, j] != 0)
					DepthFirstSearch_Recursive(start_node_id: j, debug: debug);
			}
		}

		private void DepthFirstSearch_Stack(int start_node_id, bool debug = false) {
			Stack<int> queue = new Stack<int>();
			queue.Push(start_node_id);
			visited[start_node_id] = true;

			int current, next;
			bool check_1, check_2;

			while(queue.Count() > 0) {
				current = queue.Pop();

				if(debug)
					Console.Write($"{current} ");

				foreach(int[] edge in edge_list) {
					if(edge[2] == 0 || (edge[0] != current && edge[1] != current))
						continue;

					check_1 = edge[0] == current && visited[edge[1]] == false;
					check_2 = edge[1] == current && visited[edge[0]] == false;

					if(check_1) {
						next = edge[1];
					} else if(check_2) {
						next = edge[0];
					} else {
						continue;
					}

					visited[next] = true;
					queue.Push(next);
				}
			}
		}

		private void BreathFirstSearch_Queue(int start_node_id, bool debug = false) {
			Queue<int> queue = new Queue<int>();
			queue.Enqueue(start_node_id);
			visited[start_node_id] = true;

			int current, next;
			bool check_1, check_2;

			while(queue.Count() > 0) {
				current = queue.Dequeue();

				if(debug)
					Console.Write($"{current} ");

				foreach(int[] edge in edge_list) {
					if(edge[2] == 0 || (edge[0] != current && edge[1] != current))
						continue;

					check_1 = edge[0] == current && visited[edge[1]] == false;
					check_2 = edge[1] == current && visited[edge[0]] == false;

					if(check_1) {
						next = edge[1];
					} else if(check_2) {
						next = edge[0];
					} else {
						continue;
					}

					visited[next] = true;
					queue.Enqueue(next);
				}
			}
		}

		/// Higher Order Methods

		public int[] GraphColoring(int colors_pool_size = 3, bool debug = false) {
			int[] nodes_colors = new int[NodesCount];

			for(int i = 0; i < NodesCount; i++) {
				nodes_colors[i] = -1;
			}

			nodes_colors[0] = 0;
			for(int i = 1; i < NodesCount; i++) {
				bool[] local = new bool[colors_pool_size];
				for(int j = 0; j < NodesCount; j++) {
					if(adjacency_matrix[i, j] != 0 && nodes_colors[j] != -1) {
						local[nodes_colors[j]] = true;
					}
				}

				int index = 0;
				while(local[index])
					index++;

				nodes_colors[i] = index;
			}

			if(debug) {
				Console.WriteLine("\nNodes Coloring starting from node 0");
				for(int i = 0; i < NodesCount; i++) {
					Console.WriteLine($"{i}: {nodes_colors[i]}");
				}
			}

			return nodes_colors;
		}

		public KeyValuePair<int, int[]> Dijkstra(int start_node_id, int end_node_id, bool debug = false) {
			int iter = 0;
			int current_node, new_distance, min_distance, min_node;
			bool[] visited = new bool[7];
			List<int> available = [], path = [];
			Queue<int> queue = new Queue<int>();
			Dictionary<int, int[]> distances = [];

			if(debug)
				Console.WriteLine("\nDijkstra");

			for(int i = 0; i < NodesCount; i++) {
				distances[i] = i == start_node_id ? [0, 0] : [int.MaxValue, 0];
			}

			queue.Enqueue(start_node_id);
			while(queue.Count > 0) {
				current_node = queue.Dequeue();
				if(current_node == -1) {
					break;
				}

				visited[current_node] = true;
				available = [];

				if(debug)
					Console.WriteLine($"Updating Costs: (iteration {iter})");

				// Update available distances
				// connection[0] - destination node
				// connection[1] - weight
				// distances[0] - weight
				// distances[1] - prev node
				foreach(int[] conn in edge_dict[current_node]) {
					new_distance = distances[current_node][0] + conn[1];

					if(new_distance >= distances[conn[0]][0]) {
						continue;
					}

					// update the cost of the node
					distances[conn[0]] = [new_distance, current_node];

					if(debug)
						Console.WriteLine($"{conn[0]} {new_distance} ({current_node})");

					// update the temp list of available edges for next step
					available.Add(conn[0]);
				}

				// Select next node
				// The next node is selected from the available *unexplored* nodes,
				// and is the one with the lowest cost
				min_node = -1;
				min_distance = int.MaxValue;
				for(int i = 0; i < NodesCount; i++) {
					if(visited[i] || distances[i][0] >= min_distance) {
						continue;
					}

					min_distance = distances[i][0];
					min_node = i;
				}

				// add the next node to be visited, default is -1 which will exit loop on next iteration
				queue.Enqueue(min_node);

				if(debug)
					Console.WriteLine($"Next Node: {min_node}");
			}

			// return -1 if node has not been reached
			if(distances[end_node_id][0] == int.MaxValue) {
				return new KeyValuePair<int, int[]>(-1, []);
			}

			// construct the path from the end node to the start node
			int distance = distances[end_node_id][0];
			current_node = distances[end_node_id][1];

			if(debug) {
				Console.WriteLine("Path Traceback (Node @ Distance)");
				Console.WriteLine($"{end_node_id} @ {distances[end_node_id][0]}");
			}

			while(current_node != start_node_id && current_node != -1) {
				if(debug)
					Console.WriteLine($"{current_node} @ {distances[current_node][0]}");

				path.Add(current_node);
				current_node = distances[current_node][1];
			}
			if(debug)
				Console.WriteLine("--- End Traceback ---");

			// return the distance and path
			path.Reverse();
			path.Insert(0, start_node_id);
			path.Add(end_node_id);
			if(debug) {
				Console.WriteLine($"\nDistance from {start_node_id} to {end_node_id} is {distance}");
				Console.WriteLine("Path Forward Pass (Node @ Distance)");
				foreach(int node in path) {
					Console.WriteLine($"{node} @ {distances[node][0]}");
				}
				Console.WriteLine("--- End Forward Pass ---");
			}

			return new KeyValuePair<int, int[]>(distance, path.ToArray());
		}

		// TODO: Implement Kruskal's Algorithm

	}
}
