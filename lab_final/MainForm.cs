using System.Drawing;
using System.Windows.Forms;

namespace lab_final
{
	public partial class MainForm: Form {
		public MainForm() {
			InitializeComponent();
			InitEngine();
		}

		public void InitEngine() {
			Engine.canvas = this.pictureBox1;
			Engine.bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
			Engine.graphics = Graphics.FromImage(Engine.bitmap);
			Engine.form = this;
		}

		private void button1_Click(object sender, EventArgs e) {
			string filename = Engine.graph_mode ? "input_adj.txt" : "input_edge.txt";
			Engine.static_graph = Engine.LoadGraph(filename: filename, mode: Engine.graph_mode, debug:true);
			Engine.DrawGraph(Engine.static_graph);
			listBox1.Items.Clear();
			listBox1.Items.Add($"Loaded: {filename}");
		}

		private void button2_Click(object sender, EventArgs e) {
			string filename = !Engine.graph_mode ? "output_adj.txt" : "output_edge.txt";
			Engine.SaveGraph(graph: Engine.static_graph, filename: filename, mode: Engine.graph_mode);
			listBox1.Items.Clear();
			listBox1.Items.Add($"Saved: {filename}");
		}

		private void button3_Click(object sender, EventArgs e) {
			int node_start = Engine.rnd.Next(Engine.static_graph.NodesCount);
			Engine.Start_DepthFirstSearch(Engine.static_graph, start_node_id: node_start, debug: true);
		}

		private void button4_Click(object sender, EventArgs e) {
			int node_start = Engine.rnd.Next(Engine.static_graph.NodesCount);
			Engine.Start_BreathFirstSearch(Engine.static_graph, start_node_id: node_start, debug: true);
		}

		private void button5_Click(object sender, EventArgs e) {
			listBox1.Items.Clear();
			int[] res = Engine.GraphColoring(Engine.static_graph, colors_pool_size: 4);
			for(int i = 0; i < res.Length; i++) {
				Engine.static_graph.nodes[i].color = Engine.ROGVAIV[res[i]];
			}
			Engine.DrawGraph(Engine.static_graph);
		}

		private void button6_Click(object sender, EventArgs e) {
			int start = 0, end = 1;
			this.listBox1.Items.Clear();
			KeyValuePair<int, int[]> res = Engine.Dijkstra(Engine.static_graph, start_node_id: start, end_node_id: end);

			foreach(Node n in Engine.static_graph.nodes) {
				n.color = Color.Black;
			}

			this.listBox1.Items.Clear();
			this.listBox1.Items.Add($"Distance: {res.Key}");

			for(int i = 0; i < res.Value.Length; i++) {
				this.listBox1.Items.Add(res.Value[i]);
				Engine.static_graph.nodes[res.Value[i]].color = Engine.ROGVAIV[i];
			}

			Engine.DrawGraph(Engine.static_graph);

		}
	}
}
