namespace map_final_testbed {
	internal class Program {
		static void Main(string[] args) {
			//Graph graph_1 = Engine.LoadGraph(filename: "input_1.txt", debug: true, mode: true);
			Graph graph_2 = Engine.LoadGraph(filename: "input_2.txt", mode:false);

			//Engine.SaveGraph(graph_1, filename: "../../../output_1.txt", mode: false);
			Engine.SaveGraph(graph_2, filename: "../../../output_2.txt");

			Engine.Start_DepthFirstSearch(graph_2, start_node_id: 1, debug: true);
			Engine.Start_BreathFirstSearch(graph_2, start_node_id: 1, debug: true);

			Engine.GraphColoring(graph_2, debug: true);
			Engine.Dijkstra(graph_2, start_node_id: 0, end_node_id: 1, debug:true);
		}
	}
}
