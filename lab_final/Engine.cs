namespace lab_final {
	public static class Engine {
		// Graph Functional
		private static bool[] visited = null;
		public static Graph static_graph;
		public static bool graph_mode = false;

		/// <summary>
		/// Loads a graph file from the system in a given format with additional debug information
		/// 
		/// The graph file format is either edge list
		///	
		/// ```
		///	nr_of_nodes
		///	node_0_X node_0_Y
		///	...
		///	node_(n-1)_X node_(n-1).Y
		///	
		/// node_start node_end weight
		/// ...
		/// node_start node_end weight
		///	```
		///	
		/// or
		/// 
		/// ```
		/// nr_of_nodes
		///	node_0_X node_0_Y
		///	...
		///	node_(n-1)_X node_(n-1).Y
		///	
		/// 00_weight 01_weight ... 0n_weight
		/// 10_weight 11_weight ... 1n_weight
		///    ...       ...    ...    ...
		/// n0_weight n1_weight ... nn_weight
		/// ```
		/// 
		/// </summary>
		/// <param name="filename">The filename from where to take the input, for GUI application leave the default value unless you implemented a file select mechanism</param>
		/// <param name="mode">whether or not to read an adjacency matrix file (true) or a edge list file (false)</param>
		/// <param name="debug">whether or not to display to console the additional debug information</param>
		/// <returns></returns>
		/// <exception cref="Exception">Generic exception for various file formating errors</exception>
		public static Graph LoadGraph(string filename = "input.txt", bool mode = true, bool debug = false) {
			if(debug)
				Console.WriteLine($"Input File: {filename}");

			TextReader reader = new StreamReader(filename);

			int nr_of_nodes;
			if(int.TryParse(reader.ReadLine(), out nr_of_nodes) == false) {
				throw new Exception("Invalid Input File Format - nr of nodes");
			}

			if(debug)
				Console.WriteLine($"No. of nodes: {nr_of_nodes}");

			List<Node> nodes = new List<Node>();
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

				nodes.Add(new Node(node_x, node_y));
			}

			if(debug)
				Console.WriteLine("Graph Loading Mode: " + (mode ? "adjacency matrix" : "edge_list"));

			reader.ReadLine();
			if(mode) {
				int[,] adjacency_matrix = new int[nr_of_nodes, nr_of_nodes];
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

				return new Graph(nodes, adjacency_matrix);
			} else {
				List<int[]> edge_list = new List<int[]>();

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
				return new Graph(nodes, edge_list);
			}
		}

		public static void SaveGraph(Graph graph, string filename = "output.txt", bool mode = true) {
			TextWriter writer = new StreamWriter(filename);

			writer.WriteLine(graph.NodesCount.ToString());
			foreach(Node node in graph.nodes) {
				writer.WriteLine($"{node.x} {node.y}");
			}

			writer.WriteLine();
			if(mode) {
				for(int i = 0; i < graph.NodesCount; i++) {
					for(int j = 0; j < graph.NodesCount; j++) {
						writer.Write($"{graph.adjacency_matrix[i, j]} ");
					}
					writer.Write("\n");
				}
			} else {
				foreach(int[] edge in graph.edge_list) {
					writer.WriteLine($"{edge[0]} {edge[1]} {edge[2]}");
				}
			}

			writer.Close();
		}

		private static int NodeToIndex(Graph graph, Node? start_node = null, int start_node_id = -1) {
			if(start_node != null) {
				if(graph.nodes.Contains(start_node) == false) {
					throw new Exception($"Node {start_node} is not a know node of graph {graph}");
				}

				start_node_id = graph.nodes.IndexOf(start_node);
			} else if(start_node_id == -1) {
				throw new Exception("Neither Current node or start_node_id provided");
			}

			return start_node_id;
		}

		public static void Start_DepthFirstSearch(Graph graph, Node? start_node = null, int start_node_id = -1, bool debug = false) {
			start_node_id = NodeToIndex(graph, start_node, start_node_id);

			if(debug) {
				Console.WriteLine($"\nDepth First Search: start from {start_node_id}");
				form.listBox1.Items.Clear();
				form.listBox1.Items.Add("Depth First Search:");
			}

			color_index = 0;
			visited = new bool[graph.NodesCount];
			DepthFirstSearch(graph, start_node_id, debug);
			Console.WriteLine();
			DrawGraph(graph);
			visited = new bool[graph.NodesCount];
		}

		public static void Start_BreathFirstSearch(Graph graph, Node? start_node = null, int start_node_id = -1, bool debug = false) {
			start_node_id = NodeToIndex(graph, start_node, start_node_id);

			if(debug) {
				Console.WriteLine($"\nBreath First Search: start from {start_node_id}");
				form.listBox1.Items.Clear();
				form.listBox1.Items.Add("Breath First Search:");
			}

			color_index = 0;
			visited = new bool[graph.NodesCount];
			BreathFirstSearch(graph, start_node_id, debug);
			Console.WriteLine();
			DrawGraph(graph);
			visited = new bool[graph.NodesCount];
		}

		private static void DepthFirstSearch(Graph graph, int start_node_id, bool debug = false) {
			if(visited[start_node_id] == true)
				return;

			if(debug) {
				Console.Write($"{start_node_id} ");
				form.listBox1.Items.Add(start_node_id);
			}

			graph.nodes[start_node_id].color = ROGVAIV[color_index];
			color_index++;
			color_index %= ROGVAIV.Length;
			visited[start_node_id] = true;
			for(int j = 0; j < graph.NodesCount; j++) {
				if(j == start_node_id)
					continue;

				if(graph.adjacency_matrix[start_node_id, j] != 0)
					DepthFirstSearch(graph, start_node_id: j, debug: debug);
			}
		}

		private static void BreathFirstSearch(Graph graph, int start_node_id, bool debug = false) {
			Queue<int> queue = new Queue<int>();
			queue.Enqueue(start_node_id);
			visited[start_node_id] = true;

			int current, next;
			bool check_1, check_2;

			while(queue.Count() > 0) {
				current = queue.Dequeue();

				if(debug) {
					Console.Write($"{current} ");
					form.listBox1.Items.Add(current);
				}


				graph.nodes[current].color = ROGVAIV[color_index];
				color_index++;
				color_index %= ROGVAIV.Length;
				foreach(int[] edge in graph.edge_list) {
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

		public static int[] GraphColoring(Graph graph, int colors_pool_size = 3, bool debug = false) {
			int[] nodes_colors = new int[graph.NodesCount];

			for(int i = 0; i < graph.NodesCount; i++) {
				nodes_colors[i] = -1;
			}

			nodes_colors[0] = 0;
			for(int i = 1; i < graph.NodesCount; i++) {
				bool[] local = new bool[colors_pool_size];
				for(int j = 0; j < graph.NodesCount; j++) {
					if(graph.adjacency_matrix[i, j] != 0 && nodes_colors[j] != -1) {
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
				for(int i = 0; i < graph.NodesCount; i++) {
					Console.WriteLine($"{i}: {nodes_colors[i]}");
				}
			}

			return nodes_colors;
		}

		public static KeyValuePair<int, int[]> Dijkstra(Graph graph, int start_node_id, int end_node_id, bool debug = false) {
			int iter = 0;
			int current_node, new_distance, min_distance, min_node;
			bool[] visited = new bool[7];
			List<int> available = [], path = [];
			Queue<int> queue = new Queue<int>();
			Dictionary<int, int[]> distances = [];

			if(debug)
				Console.WriteLine("\nDijkstra");

			for(int i = 0; i < graph.NodesCount; i++) {
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
				foreach(int[] conn in graph.adjacency_dict[current_node]) {
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
				for(int i = 0; i < graph.NodesCount; i++) {
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


		// Graph Graphical
		public static PictureBox canvas;
		public static Bitmap bitmap;
		public static Graphics graphics;
		public static Random rnd = new Random();
		public static MainForm form;

		private static int node_radius = 15;
		private static Pen edge_color = Pens.Blue;

		private static int color_index = 0;
		public static Color[] ROGVAIV = [Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet];

		public static void DrawGraph(Graph graph, bool mode = true) {
			Font string_font = new Font("Arial", 14);
			SolidBrush brush = new SolidBrush(Color.Blue);
			if(mode) {
				for(int i = 0; i < graph.adjacency_matrix.GetLength(0) - 1; i++) {
					for(int j = i + 1; j < graph.adjacency_matrix.GetLength(1); j++) {
						if(graph.adjacency_matrix[i, j] != 0) {
							graphics.DrawLine(edge_color, graph.nodes[i].x, graph.nodes[i].y, graph.nodes[j].x, graph.nodes[j].y);
							graphics.DrawString(
								graph.adjacency_matrix[i, j].ToString(),
								string_font, brush,
								(graph.nodes[i].x + graph.nodes[j].x) / 2,
								(graph.nodes[i].y + graph.nodes[j].y) / 2
							);
						}
					}
				}
			} else {
				foreach(int[] edge in graph.edge_list) {
					int i = edge[0];
					int j = edge[1];

					graphics.DrawLine(edge_color, graph.nodes[i].x, graph.nodes[i].y, graph.nodes[j].x, graph.nodes[j].y);
					graphics.DrawString(
						edge[2].ToString(), 
						string_font, brush, 
						(graph.nodes[i].x + graph.nodes[j].x) / 2, 
						(graph.nodes[i].y + graph.nodes[j].y) / 2
					);

				}
			}

			brush.Color = Color.Red;
			for(int i = 0; i < graph.NodesCount; i++) {
				brush.Color = graph.nodes[i].color;
				graphics.DrawString(i.ToString(), string_font, brush, graph.nodes[i].x - node_radius * 2, graph.nodes[i].y - node_radius * 2);
				graphics.FillEllipse(brush, graph.nodes[i].x - node_radius / 2, graph.nodes[i].y - node_radius / 2, node_radius, node_radius);
			}

			canvas.Image = bitmap;
		}

		public static void RandomGraphNodesColors(Graph graph) {
			foreach(Node node in graph.nodes) {
				node.color = Color.FromArgb(Engine.rnd.Next(256), Engine.rnd.Next(256), Engine.rnd.Next(256));
			}
		}
	}
}
