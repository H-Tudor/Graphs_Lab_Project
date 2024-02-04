
public class GraphOperations{
	public static int nr_of_nodes;
	public static int[] adj;
	public static int[,] matrix;
	public static bool[] visited;
	
	public static void Main(string[] args){
		adj = new int[nr_of_nodes, nr_of_nodes];
		matrix = new int[nr_of_nodes, nr_of_nodes];
		visited = new bool[nr_of_nodes];
	}

	public static void LoadFromFile(string file_path){
		TextReader reader = new StreamReader(file_path);
		string buffer = reader.ReadLine();
		nr_of_nodes = int.Parse(buffer);
		matrix = new int[n, n];

		while((buffer = reader.ReadLine()) != null){
			string[] nodes = buffer.split(' ');
			int i = int.Parse(nodes[0]);
			int j = int.Parse(nodes[1]);
			int w = nodes.Length == 3 ? int.Parse(nodes[3]) : 1;

			matrix[i, j] = matrix[j, i] = w;
		}
	}

	public static int[] Coloring(int nr_of_colors){
		int[] colors = new int[nr_of_colors]

		for(int i = 0; i < nr_of_nodes; i++){
			colors[i] = -1;
		}

		colors[0] = 0;
		for(int i = 1; i < nr_of_nodes; i++){
			bool[] local = new bool[nr_of_colors];
			for(int j = 0; j < nr_of_nodes; j++){
				if(matrix[i, j] == 1 && colors[j] != -1){
					local[colors[j]] = true;
				}
			}

			int index = 0;
			while(local[index]){
				index++;
			}

			colors[i] = index;
		}

		return colors;
	}

	public static bool CheckConnected(){
		for(int i = 0; i < nr_of_nodes; i++){
			visited[i] = false;
		}

		adj = new int[nr_of_nodes];
		int has_adjacent = -1;
		for(int i = 0; i < nr_of_nodes; i++){
			for(int j = 0; j < nr_of_nodes; j++){
				if(matrix[i,j] == 1){
					has_adjacent = i;
					adj[i] += 1;
				}
			}
		}

		if(has_adjacent == -1){
			return true;
		}

		DepthFirstSearch(has_adjacent);

		for(int i = 0; i < nr_of_nodes; i++){
			if(visited[i] && adj[i] > 0){
				return false;
			}
		}

		return true;
	}

	public static int CheckEulerian(){
		if(!CheckConnected()){
			return 0;
		}

		int odd = 0;
		for(int i = 0; i < nr_of_nodes; i++){
			if(adv[i] % 2 != 0){
				odd++;
			}
		}

		if(odd > 2){
			return 0;
		}

		return (odd == 2) ? 1 : 2;
	}

	public static bool CheckHamiltonian(){
		for(int i = 0; i < nr_of_nodes; i++){
			visited = new bool[nr_of_nodes];
			List<int> path = new List<int>();
			path.Add(i);

			if(PathFind(i) == true){
				return true;
			}
		}

		return false;
	}

	private static bool PathFind(List<int> path){
		int current = path[path.Count() - 1];
		visited[curent] = true;

		for(int j = 0; j < nr_of_nodes; j++){
			if(visited[j] == true || matrix[curent, j] == 0){
				continue;
			}

			path.Add();
			if(path.Count() == nr_of_nodes){
				return true;
			}

			if(PathFind(path) == true){
				return true;
			}

			visited[j] = false;
			path.Remove(j);
		}

		return false;
	}
	
	public int[] DIjkstra(int start_node){
		int[] dist = new int[nr_of_nodes];
		for(int i = 0; i < nr_of_nodes; i++){
			dist[i] = int.MaxValue;
		}
		
		Queue<int> queue = new Queue<int>();
		dist[start_node] = 0;
		queue.Push(start_node);
		
		while(queue.Count() > 0){
			int current = queue.Pop();
			
			for(int j = 0; j < nr_of_nodes; j++){
				if(matrix[current, j] == 0){
					continue;
				}
				
				int neighbour = j;
				if((dist[i] + matrix[i, j]) < dist[j]){
					dist[j] = dist[i] + matrix[i, j];
					queue.Push(j);
				}
			}	
		}
		
		for(int i = 0; i < nr_of_nodes; i++){
			if(dist[i] == int.MaxValue){
				dist[i] = -1
			}
		}
		
		return dist;
	}

	public static void BreathFirstSearch(int start_node){
		visited = new bool[nr_of_nodes];
		Queue<int> queue = new Queue<int>();
		queue.Enqueue(start_node);
		visited[start_node] = true;

		while(queue.Count() > 0){
			int current = queue.Dequeue();

			for(int j = 0; j < nr_of_nodes; j++){
				if(matrix[curent, j] == 1 && !visited[j]){
					queue.Enqueue(j);
				}
			}
		}
	}

	public static void DepthFirstSearch(int node_id){
		if(visited[node_id] == true)
			return;

		visited[node_id] = true;
		for(int j = 0; j < nr_of_nodes; j++){
			if(matrix[node_id, j] == 1)
			DepthFirstSearch(j);
		}
	}
}

public class List<T> where T : IEquatable<T> {

	private int n;
	private T[] v;

	public T this[int i] {
		get { return v[i]; }
		set { v[i] = value; }
	}

	public int Length {
		get { return n; }
		private set { }
	}

	public List() {
		n = 0;
		v = new T[n];
	}

	public override string ToString() {
		string result = "[ ";

		for(int i = 0; i < n; i++) {
			result += v[i].ToString() + " ";
		}

		result += "]";
		return result;
	}

	public int Count(int index) => n;

	public void AddBeginning(T x) {
		n++;
		T[] array = new T[n];

		for(int i = 1; i < n; i++) {
			array[i] = v[i - 1];
		}

		array[0] = x;
		v = array;
	}

	public void AddEnding(T x) {
		n++;
		T[] array = new T[n];

		for(int i = 0; i < n - 1; i++) {
			array[i] = v[i];
		}

		array[n - 1] = x;
		v = array;
	}

	public T RemoveBeginning() {
		n--;
		T[] array = new T[n];

		for(int i = 1; i <= n; i++) {
			array[i - 1] = v[i];
		}

		T value = v[0];
		v = array;
		return value;
	}

	public T RemoveEnding() {
		n--;
		T[] array = new T[n];

		for(int i = 0; i < n; i++) {
			array[i] = v[i];
		}

		T value = v[n];
		v = array;
		return value;
	}

	public void RemoveAll(T x) {
		int count = 0;
		for(int i = 0; i < n; i++) {
			if(!v[i].Equals(x)) {
				count++;
			}
		}

		int k = 0;
		T[] array = new T[count];
		for(int i = 0; i < n; i++) {
			if(!v[i].Equals(x)) {
				array[k] = v[i];
				k++;
			}
		}

		n = count;
		v = array;
	}

	public void RemoveAt(int x) {
		int k = 0;
		T[] array = new T[n - 1];

		for(int i = 0; i < n; i++) {
			if(k != x) {
				array[k] = v[i];
				k++;
			}
		}

		n = n - 1;
		v = array;
	}
}

public class Queue<T> where T : IEquatable<T> {

	private int n;
	private T[] v;

	public int Length {
		get { return n; }
		private set { }
	}

	public Queue() {
		n = 0;
		v = new T[n];
	}

	public override string ToString() {
		string result = "[ ";

		for(int i = 0; i < n; i++) {
			result += v[i].ToString() + " ";
		}

		result += "]";
		return result;
	}

	public int Count(int index) => n;


	public void AddEnding(T x) {
		n++;
		T[] array = new T[n];

		for(int i = 0; i < n - 1; i++) {
			array[i] = v[i];
		}

		array[n - 1] = x;
		v = array;
	}

	public T RemoveBeginning() {
		n--;
		T[] array = new T[n];

		for(int i = 1; i <= n; i++) {
			array[i - 1] = v[i];
		}

		T value = v[0];
		v = array;
		return value;
	}
}

public class Stack<T> where T : IEquatable<T> {

	private int n;
	private T[] v;

	public int Length {
		get { return n; }
		private set { }
	}

	public Stack() {
		n = 0;
		v = new T[n];
	}

	public override string ToString() {
		string result = "[ ";

		for(int i = 0; i < n; i++) {
			result += v[i].ToString() + " ";
		}

		result += "]";
		return result;
	}

	public int Count(int index) => n;

	public void AddEnding(T x) {
		n++;
		T[] array = new T[n];

		for(int i = 0; i < n - 1; i++) {
			array[i] = v[i];
		}

		array[n - 1] = x;
		v = array;
	}

	public T RemoveEnding() {
		n--;
		T[] array = new T[n];

		for(int i = 0; i < n; i++) {
			array[i] = v[i];
		}

		T value = v[n];
		v = array;
		return value;
	}
}

public class Misc{
	public static int[] array = new int[10];
	
	public static bool BinarySearch(int left, int right, int search){
		if(left >= rigth){
			return false
		}

		int middle = (left + right) / 2;
		if(search == array[middle]){
			return true;
		}

		if(search < array[middle]){
			return BinarySearch(left, middle, search);
		}

		return BinarySearch(middle, rigth, search);
	}
}

public class GeneticAlgorithm{
	public static int number_of_solutions = 100;
	public static int number_of_survivors = 10;
	public static int rate_of_mutation = 0.3;
	static Random random = new Random();

	public static void NextGeneration() {
		if(generation == null) {
			generation = new List<Solution>();

			for(int i = 0; i < number_of_solutions; i++) {
				generation.Add(new Solution());
			}
			generation.Sort((Solution a, Solution b) => a.Fitness().CompareTo(b.Fitness()));
		}

		SelectSurvivors();
		CrossOverSurvivors();
		MutateSolutions();
		generation.Sort((Solution a, Solution b) => a.Fitness().CompareTo(b.Fitness()));
	}

	private static void SelectSurvivors() {
		generation.Sort((Solution a, Solution b) => a.Fitness().CompareTo(b.Fitness()));
		generation.RemoveRange(number_of_survivors, number_of_solutions - number_of_survivors);
	}

	private static void CrossOverSurvivors() {
		int i, j;
		for(int k = number_of_survivors; k < number_of_solutions; k++) {
			i = random.Next(number_of_survivors);
			j = random.Next(number_of_survivors);
			generation.Add(new Solution(generation[i], generation[j]));
		}
	}

	private static void MutateSolutions() {
		for(int i = 0; i < number_of_solutions; i++) {
			if(random.NextDouble() >= rate_of_mutation) {
				continue;
			}

			// Compute mutation
			// Apply mutation to generation[i] 
		}
	}
}

public class Solution{
	public Solution(){}

	public Solution(Solution a, Solution b){}

	public float Fitness(){}

	public override string ToString(){}
}
