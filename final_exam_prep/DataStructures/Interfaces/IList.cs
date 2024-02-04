namespace final_exam_prep.DataStructures.Interfaces {
	internal interface IList<T> {
		public string ToString();

		public void AddBeginning(T x);

		public void AddEnding(T x);

		public T RemoveBeginning();

		public T RemoveEnding();

		public void RemoveAll(T x);

		public void RemoveAt(int x);
	}
}
