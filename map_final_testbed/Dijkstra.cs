namespace map_final_testbed {
	static public class Dijkstra {
		public static KeyValuePair<int, int[]> RunDijkstra(Graph graph, int start_node_id, int end_node_id) {
			int current_node, new_distance, min_distance, min_node;
			bool[] visited = [];
			List<int> available = [], path = [];
			Queue<int> queue = new Queue<int>();
			Dictionary<int, int[]> distances = [];

			for(int i = 0; i < graph.NodesCount; i++) {
				distances[i] = i == start_node_id ? [0, 0] : [int.MaxValue, 0];
			}

			queue.Enqueue(start_node_id);
			while(queue.Count > 0) {
				current_node = queue.Dequeue();
				visited[current_node] = true;

				available = [];

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

					// update the temp list of available edges for next step
					available.Add(conn[0]);
				}

				// Select next node
				// The next node is selected from the available *unexplored* nodes,
				// and is the one with the lowest cost
				min_node = -1;
				min_distance = int.MaxValue;
				foreach(int conn in available) {
					if(visited[conn] || distances[conn][0] >= min_distance) {
						continue;
					}

					min_distance = distances[conn][0];
					min_node = conn;
				}

				// if there is a next node to be visited, enqueue it
				if(min_node != -1)
					queue.Enqueue(min_node);
			}

			// return -1 if node has not been reached
			if(distances[end_node_id][0] == int.MaxValue) {
				return new KeyValuePair<int, int[]>(-1, []);
			}

			// construct the path from the end node to the start node
			int distance = distances[end_node_id][0];
			current_node = distances[end_node_id][1];
			while(current_node != -1) {
				path.Add(current_node);
			}

			// return the distance and path
			path.Reverse();
			return  new KeyValuePair<int, int[]>(distance, path.ToArray());
		}
	}
}
